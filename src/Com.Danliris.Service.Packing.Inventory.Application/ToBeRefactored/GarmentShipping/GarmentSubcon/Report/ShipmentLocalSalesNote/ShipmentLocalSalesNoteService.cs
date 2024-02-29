using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetterTS;
using System.IO;
using System.Data;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.ShipmentLocalSalesNote
{

    public class ShipmentLocalSalesNoteService : IShipmentLocalSalesNoteService
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IGarmentShippingLocalSalesNoteTSRepository garmentShippingLocalSalesNoteTS;
        private readonly IGarmentShippingLocalSalesNoteTSItemRepository garmentShippingLocalSalesNoteTSItem;
        private readonly IGarmentReceiptSubconPackingListRepository garmentReceiptSubconPackingList;
        private readonly IGarmentReceiptSubconPackingListItemRepository garmentReceiptSubconPackingListItem;
        private readonly IGarmentLocalCoverLetterTSRepository garmentLocalCoverLetterTSRepository;

        public ShipmentLocalSalesNoteService(IServiceProvider serviceProvider)
        {
            this._identityProvider = serviceProvider.GetService<IIdentityProvider>();
            this.serviceProvider = serviceProvider;
            garmentShippingLocalSalesNoteTS = serviceProvider.GetService<IGarmentShippingLocalSalesNoteTSRepository>();
            garmentShippingLocalSalesNoteTSItem = serviceProvider.GetService<IGarmentShippingLocalSalesNoteTSItemRepository>();
            garmentReceiptSubconPackingList = serviceProvider.GetService<IGarmentReceiptSubconPackingListRepository>();
            garmentReceiptSubconPackingListItem = serviceProvider.GetService<IGarmentReceiptSubconPackingListItemRepository>();
            garmentLocalCoverLetterTSRepository = serviceProvider.GetService<IGarmentLocalCoverLetterTSRepository>();
        }

        public async Task<List<ShipmentLocalSalesNoteVM>> GetReportQuery(DateTime dateFrom,DateTime DateTo)
        {
            var query = garmentShippingLocalSalesNoteTS.ReadAll()
                .Where(x => x.Date.AddHours(_identityProvider.TimezoneOffset).Date >= dateFrom.Date 
                && x.Date.AddHours(_identityProvider.TimezoneOffset).Date <= DateTo.Date
                && x.IsDeleted == false);

            var finalQuery = (from a in query
                              join b in garmentShippingLocalSalesNoteTSItem.ReadAll() on a.Id equals b.LocalSalesNoteId
                              join c in garmentReceiptSubconPackingList.ReadAll() on b.PackingListId equals c.Id
                              join d in garmentReceiptSubconPackingListItem.ReadAll() on c.Id equals d.PackingListId
                              join f in garmentLocalCoverLetterTSRepository.ReadAll() on a.Id equals f.LocalSalesNoteId into ls
                              from lcl in ls.DefaultIfEmpty()
                              select new
                              {
                                  LocalCoverLetterNo = lcl == null ? "-" : lcl.NoteNo,
                                  LocalCoverLetterDate = lcl == null ? DateTimeOffset.MinValue : lcl.Date.AddHours(_identityProvider.TimezoneOffset),
                                  BCNo = lcl == null ? "-" : lcl.BCNo,
                                  LocalSalesNoteNo = a.NoteNo,
                                  LocalSalesNoteDate = a.Date.AddHours(_identityProvider.TimezoneOffset),
                                  ShippingStaff = a.CreatedBy,
                                  InvoiceNo = c.InvoiceNo,
                                  InvoiceDate = c.InvoiceDate.AddHours(_identityProvider.TimezoneOffset),
                                  BuyerName = c.BuyerName,
                                  UnitName = d.UnitCode,
                                  ProductCode = d.ComodityCode,
                                  ProductName = d.ComodityName,
                                  Quantity = d.TotalQuantityPackingOut,
                                  UomUnit = d.UomUnit,
                                  DPP = (b.Quantity * b.Price),
                                  PPn = (double)a.VatRate,
                              })
                              .GroupBy(x => new
                               {
                                   x.LocalCoverLetterNo,
                                   x.LocalCoverLetterDate,
                                   x.BCNo,
                                   x.LocalSalesNoteNo,
                                   x.LocalSalesNoteDate,
                                   x.ShippingStaff,
                                   x.InvoiceNo,
                                   x.InvoiceDate,
                                   x.BuyerName,
                                   x.UnitName,
                                   x.ProductCode,
                                   x.ProductName,
                                   x.UomUnit,
                                   x.DPP,
                                   x.PPn
                               }, (key, group) => new ShipmentLocalSalesNoteVM
                               {
                                   LocalCoverLetterNo = key.LocalCoverLetterNo,
                                   LocalCoverLetterDate = key.LocalCoverLetterDate,
                                   BCNo = key.BCNo,
                                   LocalSalesNoteNo = key.LocalSalesNoteNo,
                                   LocalSalesNoteDate = key.LocalSalesNoteDate,
                                   ShippingStaff = key.ShippingStaff,
                                   InvoiceNo = key.InvoiceNo,
                                   InvoiceDate = key.InvoiceDate,
                                   BuyerName = key.BuyerName,
                                   UnitName = key.UnitName,
                                   ProductCode = key.ProductCode,
                                   ProductName = key.ProductName,
                                   Quantity = group.Sum(x => x.Quantity),
                                   UomUnit = key.UomUnit,
                                   DPP = key.DPP,
                                   PPn = key.PPn
                               }).ToList();
            
            for(var i =0; i < finalQuery.Count(); i++)
            {
                finalQuery[i].PPn = (finalQuery[i].PPn / 100) * finalQuery[i].DPP;
                finalQuery[i].Total = finalQuery[i].PPn + finalQuery[i].DPP;
            }

            return finalQuery;
        }

        public async Task<MemoryStream> GetExcel(DateTime dateFrom, DateTime DateTo)
        {
            var query = await GetReportQuery(dateFrom, DateTo);

            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Surat Pengantar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Surat Pengantar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No BC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nota Jual Lokal", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Penjualan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipping Staff", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "DPP", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PPn", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(string) });

            if (query.Count > 0)
            {
                int idx = 1;
                foreach(var a in query)
                {
                    var lclDate = a.LocalCoverLetterDate != DateTimeOffset.MinValue ? a.LocalCoverLetterDate.ToString("dd-MMM-yyyy") : "-";
                    var lsnDate = a.LocalSalesNoteDate.ToString("dd-MMM-yyyy");
                    var invoiceDate = a.InvoiceDate.ToString("dd-MMM-yyyy");
                    result.Rows.Add(idx.ToString(),a.LocalCoverLetterNo, lclDate, a.BCNo,a.LocalSalesNoteNo
                        , lsnDate,a.ShippingStaff,a.InvoiceNo, invoiceDate,a.BuyerName
                        ,a.UnitName,a.ProductCode,a.ProductName,a.Quantity,a.UomUnit
                        ,a.DPP,a.PPn,a.Total);

                    idx++;
                }
            }

            ExcelPackage package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Data");
            var col = (char)('A' + result.Columns.Count);

            sheet.Cells[$"A1:{col}1"].Value = string.Format("LAPORAN SHIPMENT PER NOTA JUAL LOKAL - TERIMA SUBCON");
            sheet.Cells[$"A1:{col}1"].Merge = true;
            sheet.Cells[$"A1:{col}1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A1:{col}1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A1:{col}1"].Style.Font.Bold = true;
            sheet.Cells[$"A2:{col}2"].Value = string.Format("PERIODE TANGGAL " + dateFrom.ToString("dd MMMM yyyy")+ " s.d. " + DateTo.ToString("dd MMMM yyyy"));
            sheet.Cells[$"A2:{col}2"].Merge = true;
            sheet.Cells[$"A2:{col}2"].Style.Font.Bold = true;
            sheet.Cells[$"A2:{col}2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A2:{col}2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Cells["A4"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);
            sheet.Cells.AutoFitColumns();

            sheet.Cells[$"A4:R4"].Style.Font.Bold = true;
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;

        }
    }
}
