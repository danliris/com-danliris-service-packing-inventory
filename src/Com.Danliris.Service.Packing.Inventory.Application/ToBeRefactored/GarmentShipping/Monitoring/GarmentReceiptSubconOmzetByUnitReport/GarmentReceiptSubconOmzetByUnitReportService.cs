using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentReceiptSubconOmzetByUnitReport
{
    public class GarmentReceiptSubconOmzetByUnitReportService : IGarmentReceiptSubconOmzetByUnitReportService
    {
        private readonly IGarmentReceiptSubconPackingListRepository repository;
        private readonly IGarmentReceiptSubconPackingListItemRepository itemrepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityProvider _identityProvider;

        public GarmentReceiptSubconOmzetByUnitReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            repository = serviceProvider.GetService<IGarmentReceiptSubconPackingListRepository>();
            itemrepository = serviceProvider.GetService<IGarmentReceiptSubconPackingListItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentReceiptSubconOmzetByUnitReportViewModel> GetData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryPL = repository.ReadAll();
            var quaryPLItem = itemrepository.ReadAll();
          
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.LocalSalesNoteDate.AddHours(offset).Date >= DateFrom.Date && w.LocalSalesNoteDate.AddHours(offset).Date <= DateTo.Date);

            //queryPL = queryPL.Where(w => w.Omzet == true);

            //List<GarmentReceiptSubconOmzetByUnitReportViewModel> omzetgmt = new List<GarmentReceiptSubconOmzetByUnitReportViewModel>();

            var Query = (from a in queryPL 
                         join b in quaryPLItem on a.Id equals b.PackingListId
                         where a.IsDeleted == false && b.IsDeleted == false
                                && b.UnitCode == (string.IsNullOrWhiteSpace(unit) ? b.UnitCode : unit)
           
                         select new GarmentReceiptSubconOmzetByUnitReportViewModel
                         {
                             LocalSalesNoteNo = a.LocalSalesNoteNo,
                             LocalSalesNoteDate = a.LocalSalesNoteDate,
                             LocalSalesContractNo = a.LocalSalesContractNo,
                             BuyerName = a.BuyerName,
                             BuyerBrandName = b.BuyerBrandName,
                             ComodityName = b.ComodityName,
                             UnitCode = b.UnitCode,
                             PaymentTerm = a.PaymentTerm,
                             RONo = b.RONo,
                             Article = b.Article,
                             SCNo = b.SCNo,
                             PackingOutNo = b.PackingOutNo,
                             Quantity = b.TotalQuantityPackingOut,
                             UOMUnit = b.UomUnit,
                             CurrencyCode = b.Valas,                        
                             Amount = b.TotalQuantityPackingOut * b.Price,
                         }).ToList();

            return Query.OrderBy(w => w.UnitCode).ThenBy(w => w.LocalSalesNoteDate).ThenBy(w => w.LocalSalesNoteNo).ThenBy(w => w.RONo).ToList();

        }

        public ListResult<GarmentReceiptSubconOmzetByUnitReportViewModel> GetReportData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(unit, dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentReceiptSubconOmzetByUnitReportViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(unit, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KONFEKSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO SC LOCAL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PAYMENT TERM", DataType = typeof(string) });
         
            result.Columns.Add(new DataColumn() { ColumnName = "NO PACKING OUT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO RO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER BRAND", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ARTICLE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "COMODITY NAME", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(double) });

            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {

                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET PENERIMAAN SUBCON GARMENT";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("KONFEKSI : {0}", string.IsNullOrWhiteSpace(unit) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().UnitCode));
                    sheet.Cells[$"A3:L3"].Merge = true;
                    sheet.Cells[$"A3:L3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:L3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:L3"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A5"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string LSDate = d.LocalSalesNoteDate == new DateTime(1970, 1, 1) ? "-" : d.LocalSalesNoteDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    result.Rows.Add(index, d.UnitCode, d.LocalSalesNoteNo, LSDate, d.LocalSalesContractNo, d.PaymentTerm, d.BuyerName,  
                                    d.PackingOutNo, d.RONo, d.BuyerBrandName, d.Article, d.ComodityName, d.Quantity, d.UOMUnit, d.CurrencyCode, d.Amount);
                }

                double TotQty = Query.Sum(x => x.Quantity);
                double TotIDR = Query.Sum(x => x.Amount);

                result.Rows.Add("", "", "", "", "", "T  O  T  A  L  :", "", "", "", "", "", "", TotQty, "", "", TotIDR);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET PENERIMAAN SUBCON GARMENT";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("KONFEKSI : {0}", string.IsNullOrWhiteSpace(unit) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().UnitCode));
                    sheet.Cells[$"A3:L3"].Merge = true;
                    sheet.Cells[$"A3:L3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:L3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:L3"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A5"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }
    }
}
