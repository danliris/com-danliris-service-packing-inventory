using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Data;
using System.Globalization;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes
{
    public class FinishedGoodsMinutesService : IFinishedGoodsMinutesService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IGarmentReceiptSubconPackingListRepository garmentReceiptSubconPackingList;
        private readonly IGarmentReceiptSubconPackingListItemRepository garmentReceiptSubconPackingListItem;
        private readonly IGarmentShippingLocalSalesNoteTSRepository garmentShippingLocalSalesNoteTS;
        private readonly IGarmentShippingLocalSalesNoteTSItemRepository garmentShippingLocalSalesNoteTSItem;
        public FinishedGoodsMinutesService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            this._identityProvider = serviceProvider.GetService<IIdentityProvider>();
            this.serviceProvider = serviceProvider;
            garmentReceiptSubconPackingList = serviceProvider.GetService<IGarmentReceiptSubconPackingListRepository>();
            garmentReceiptSubconPackingListItem = serviceProvider.GetService<IGarmentReceiptSubconPackingListItemRepository>();
            garmentShippingLocalSalesNoteTS = serviceProvider.GetService<IGarmentShippingLocalSalesNoteTSRepository>();
            garmentShippingLocalSalesNoteTSItem = serviceProvider.GetService<IGarmentShippingLocalSalesNoteTSItemRepository>();
        }

        public async Task<List<FinishedGoodsMinutesVM>> GetReportQuery(string invoiceNo)
        {
            var queryHeader = garmentReceiptSubconPackingList.ReadAll().Where(x => x.InvoiceNo.Contains(invoiceNo) && x.IsDeleted == false);
            var query =  (from a in queryHeader
                               join b in garmentReceiptSubconPackingListItem.ReadAll() on a.Id equals b.PackingListId
                               join c in garmentShippingLocalSalesNoteTSItem.ReadAll() on a.Id equals c.PackingListId into lcsItem
                               from lcsitems in lcsItem.DefaultIfEmpty()
                               join d in garmentShippingLocalSalesNoteTS.ReadAll() on lcsitems.LocalSalesNoteId equals d.Id into lcss
                               from lcs in lcss.DefaultIfEmpty()
                               select new 
                               {
                                   InvoiceNo = a.InvoiceNo,
                                   RONo = b.RONo,
                                   SentQty = b.TotalQuantityPackingOut,
                                   SentUomUnit = b.UomUnit,
                                   ComodityName = b.ComodityName,
                                   BuyerName = a.BuyerName,
                                   LocalSalesNote = lcs != null ? lcs.NoteNo : ""
                               }).GroupBy(x => new { x.InvoiceNo, x.RONo, x.SentUomUnit, x.ComodityName, x.BuyerName, x.LocalSalesNote },(key,group) => new FinishedGoodsMinutesVM
                               {
                                   InvoiceNo = key.InvoiceNo,
                                   RONo = key.RONo,
                                   SentQty = group.Sum(s => s.SentQty),
                                   SentUomUnit = key.SentUomUnit,
                                   ComodityName = key.ComodityName,
                                   BuyerName = key.BuyerName,
                                   LocalSalesNote = key.LocalSalesNote
                               }).ToList();

            var LSN = query.Select(x => x.LocalSalesNote).Distinct().ToList();
            var BCOut = new List<GetBCOUTVM>();

            if (LSN.Count > 0)
            {
                BCOut = await GetBC(LSN);
            }

            var CompPurchasing = await GetCompletePurchasing(query.Select(x => x.RONo).Distinct().ToList());

            foreach(var a in query)
            {
                var MarchBCOut = BCOut.FirstOrDefault(x => x.LocalSalesNote == a.LocalSalesNote);
                var MarchROPurch = CompPurchasing.Where(x => x.RONo == a.RONo).ToList();

                a.BCNo = MarchBCOut != null ? MarchBCOut.BCNo : "";
                a.BCType = MarchBCOut != null ? MarchBCOut.BCType : "";
                a.BCDate = MarchBCOut != null ? MarchBCOut.BCDate : DateTimeOffset.MinValue;

                a.detailRO = MarchROPurch;
            }
            return query;
        }

        public async Task<MemoryStream> GetExcel(string invoiceNo)
        {
            var query = await GetReportQuery(invoiceNo);

            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RONO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO DOKUMEN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS DOKUMEN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL DOKUMEN", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "QTY KIRIM", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN KIRIM", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BARANG JADI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO. NOTA JUAL LOKAL", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "DETAIL BARANG DIPAKAI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KODE BARANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN PAKAI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ASAL SJ", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "SUPPLIER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QTY MASUK", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO BC MASUK", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS BC MASUK", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL BC MASUK", DataType = typeof(string) });

            if(query.Count > 0)
            {
                foreach(var h in query)
                {
                    foreach(var i in h.detailRO)
                    {
                        string BCDateOut = h.BCDate == DateTimeOffset.MinValue ? "-" : h.BCDate.ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                        string BCDateIn = i.ReceiptBCDate == DateTimeOffset.MinValue ? "-" : i.ReceiptBCDate.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                        result.Rows.Add(h.InvoiceNo, h.RONo, h.BCNo, h.BCType, BCDateOut
                            , h.SentQty, h.SentUomUnit, h.ComodityName, h.BuyerName, h.LocalSalesNote
                            , i.ProductName, i.ProductCode, i.UsedQty, i.UsedUomUnit, i.DONo
                            , i.SupplierName, i.ReceiptQty, i.ReceiptUomUnit, i.ReceiptBCNo, i.ReceiptBCType
                            , BCDateIn);
                    }
                }
            }

            ExcelPackage package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Data");
            var col = (char)('A' + result.Columns.Count);

            sheet.Cells[$"A1:{col}1"].Value = string.Format("LAPORAN RISALAH BARANG JADI");
            sheet.Cells[$"A1:{col}1"].Merge = true;
            sheet.Cells[$"A1:{col}1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A1:{col}1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A1:{col}1"].Style.Font.Bold = true;
            sheet.Cells[$"A2:{col}2"].Value = string.Format("No Invoice : " + invoiceNo);
            sheet.Cells[$"A2:{col}2"].Merge = true;
            sheet.Cells[$"A2:{col}2"].Style.Font.Bold = true;
            sheet.Cells[$"A2:{col}2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A2:{col}2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            var index = 0;
            int value;

            Dictionary<string, int> bonspan = new Dictionary<string, int>();
            foreach (var a in query)
            {
                foreach(var b in a.detailRO)
                {
                    if (bonspan.TryGetValue(a.InvoiceNo + a.RONo + a.BCNo + a.LocalSalesNote, out value))
                    {
                        bonspan[a.InvoiceNo + a.RONo + a.BCNo + a.LocalSalesNote]++;
                    }
                    else
                    {
                        bonspan[a.InvoiceNo + a.RONo + a.BCNo + a.LocalSalesNote] = 1;
                    }
                }
               
            }

            index = 5;

            string[] colsToMerge = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            foreach (KeyValuePair<string, int> c in bonspan)
            {
                foreach (var colToMerge in colsToMerge)
                {
                    sheet.Cells[colToMerge + index + ":" + colToMerge + (index + c.Value - 1)].Merge = true;
                    sheet.Cells[colToMerge + index + ":" + colToMerge + (index + c.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                }
                index += c.Value;
            }

            sheet.Cells["A4"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);
            sheet.Cells.AutoFitColumns();

            sheet.Cells[$"A4:U4"].Style.Font.Bold = true;

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }


        public async Task<List<GetBCOUTVM>> GetBC(List<string> noteNo)
        {
            string Uri = "garment-subcon-custom-outs/by-local-sales-note";
            IHttpClientService httpClient = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.PurchasingEndpoint}{Uri}", new StringContent(JsonConvert.SerializeObject(noteNo), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<GetBCOUTVM> viewModel = JsonConvert.DeserializeObject<List<GetBCOUTVM>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FinishedGoodsMinutesPurchasingVM>> GetCompletePurchasing(List<string> roNo)
        {
            string Uri = "for-finished-good-minutes";
            IHttpClientService httpClient = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.PurchasingEndpoint}{Uri}", new StringContent(JsonConvert.SerializeObject(roNo), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<FinishedGoodsMinutesPurchasingVM> viewModel = JsonConvert.DeserializeObject<List<FinishedGoodsMinutesPurchasingVM>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }


}
