using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD.LocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.TraceableOut
{
    public class TraceableOutService : ITraceableOutService
    {
        private readonly IGarmentMDLocalSalesNoteRepository _repository;
        private readonly IGarmentMDLocalSalesNoteItemRepository _itemRepository;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IServiceProvider serviceProvider;

        public TraceableOutService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<PackingInventoryDbContext>();
            _repository = serviceProvider.GetService<IGarmentMDLocalSalesNoteRepository>();
            _itemRepository = serviceProvider.GetService<IGarmentMDLocalSalesNoteItemRepository>();
            this.serviceProvider = serviceProvider;
        }

        public async Task<List<TraceableOutVM>> getQuery(string bcno,string bcType,string category)
        {
            var queryHeader = _repository.ReadAll()
                .Where(s => s.BCNo == bcno);

            var query = (from a in queryHeader
                        join b in _dbContext.GarmentMDLocalSalesNoteItems.AsNoTracking() on a.Id equals b.LocalSalesNoteId
                        join c in _dbContext.GarmentMDLocalSalesNoteDetails.AsNoTracking() on b.Id equals c.LocalSalesNoteItemId
                        where b.ComodityName.ToLower() == category.ToLower() && a.BCType == bcType && b.ComodityName == category
                        && c.BonFrom != "SISA"
                         select new 
                        {
                            BCDate = a.BCDate.Value.AddHours(7).Date,
                            BCNo = a.BCNo,
                            BCType = a.BCType,
                            BuyerName = a.BuyerName,
                            Date = a.Date.AddHours(7).Date,
                            NoteNo = a.NoteNo,
                            BonNo =  c.BonNo ,
                            Qty =  c.Quantity,
                            RO = c.RONo ,
                            UnitQtyName = c.UomUnit,
                            ComodityName =  c.ComodityName ,
                        }).GroupBy(x => new { x.BCType, x.BCNo, x.BCDate, x.Date, x.ComodityName, x.BonNo, x.BuyerName,  x.RO, x.UnitQtyName,x.NoteNo }, (key, group) => new TraceableOutVM
                        {
                            BCDate = key.BCDate,
                            BCNo = key.BCNo,
                            BCType = key.BCType,
                            BuyerName = key.BuyerName,
                            ComodityName = key.ComodityName,
                            Date = key.Date,
                            BonNo = key.BonNo,
                            NoteNo = key.NoteNo,
                            Qty = group.Sum(x => x.Qty),
                            RO = key.RO,
                            UnitQtyName = key.UnitQtyName
                        }).ToList();

            var queryLeftOver = (from a in queryHeader
                         join b in _dbContext.GarmentMDLocalSalesNoteItems.AsNoTracking() on a.Id equals b.LocalSalesNoteId
                         join c in _dbContext.GarmentMDLocalSalesNoteDetails.AsNoTracking() on b.Id equals c.LocalSalesNoteItemId
                         join d in _dbContext.GarmentMDSalesNoteDetailItems on c.Id equals d.LocalSalesNoteDetailId
                         where b.ComodityName.ToLower() == category.ToLower() && a.BCType == bcType && b.ComodityName == category
                         && c.BonFrom == "SISA"
                         select new 
                         {
                             BCDate = a.BCDate.Value.AddHours(7).Date,
                             BCNo = a.BCNo,
                             BCType = a.BCType,
                             BuyerName = a.BuyerName,
                             Date = a.Date.AddHours(7).Date,
                             NoteNo = a.NoteNo,
                             BonNo = c.BonNo,
                             Qty = d.Quantity,
                             RO = d.RONo,
                             UnitQtyName = d.UomUnit,
                             ComodityName = d.ComodityName,
                         }).GroupBy(x => new { x.BCType, x.BCNo, x.BCDate, x.Date, x.ComodityName, x.BonNo, x.BuyerName, x.RO, x.UnitQtyName, x.NoteNo }, (key, group) => new TraceableOutVM
                         {
                             BCDate = key.BCDate,
                             BCNo = key.BCNo,
                             BCType = key.BCType,
                             BuyerName = key.BuyerName,
                             ComodityName = key.ComodityName,
                             Date = key.Date,
                             BonNo = key.BonNo,
                             NoteNo = key.NoteNo,
                             Qty = group.Sum(x => x.Qty),
                             RO = key.RO,
                             UnitQtyName = key.UnitQtyName
                         }).ToList();

            var result = query.Concat(queryLeftOver).ToList();

            var listRo = result.Select(x => x.RO).ToHashSet();

            var listDetail = await GetDetail(listRo);

            result[0].rincian = listDetail;

            return result.OrderBy(s => s.NoteNo).ThenBy(s => s.RO).ToList();
        }

        public async Task<MemoryStream> GetExcel(string bcno, string bcType, string category)
        {
            var index2 = 0;
            var query = await getQuery(bcno, bcType, category);
            if (query.Count == 0)
            {
                throw new Exception("Data Tidak Tersedia.");
            }
            else
            {
                var satuan = "-";

                DataTable result = new DataTable();

                result.Columns.Add(new DataColumn() { ColumnName = "no", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "tanggal keluar", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "no. bon", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "nama barang", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "jumlah barang", DataType = typeof(double) });
                result.Columns.Add(new DataColumn() { ColumnName = "satuan", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "no invoice", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "buyer", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "jenis", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "nomor", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "tanggal", DataType = typeof(string) });
                result.Columns.Add(new DataColumn() { ColumnName = "RO", DataType = typeof(string) });

                DataTable result2 = new DataTable();
                result2.Columns.Add(new DataColumn() { ColumnName = "Nomor22", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Nama Barang 2", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Jumlah Pemakaian", DataType = typeof(Double) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Satuan 2", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "jumlah budget", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Jenis BC", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "No.", DataType = typeof(String) });
                result2.Columns.Add(new DataColumn() { ColumnName = "Tanggal 2", DataType = typeof(String) });


                //if (query.ToArray().Count() == 0)
                //    result.Rows.Add("", "", "", "", 0, "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
                //else
                //{

                var index = 0;
                foreach (var item in query)
                {
                    index++;
                    string date = item.Date == new DateTimeOffset(new DateTime(1970, 1, 1)) ? "-" : Convert.ToDateTime(item.Date).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string BCDate = item.BCDate == new DateTimeOffset(new DateTime(1970, 1, 1)) ? "-" : Convert.ToDateTime(item.BCDate).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, date, item.BonNo, item.ComodityName, item.Qty, item.UnitQtyName, item.NoteNo, item.BuyerName, item.BCType, item.BCNo, BCDate, item.RO);

                    if (item.rincian != null)
                    {
                        foreach (var detail in item.rincian)
                        {
                            index2++;
                            string BCDate2 = detail.BCDate == new DateTimeOffset(new DateTime(1970, 1, 1)) ? "-" : Convert.ToDateTime(detail.BCDate).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                            result2.Rows.Add(index2, detail.DestinationJob, detail.ItemCode, detail.ItemName, detail.SmallestQuantity, detail.UnitQtyName, detail.BCType, detail.BCNo, BCDate2);

                        }
                    }

                }
                ExcelPackage package = new ExcelPackage();



                var sheet = package.Workbook.Worksheets.Add("Data");


                var Tittle = new string[] { "Monitoring Pengeluaran Hasil Produksi" };
                var headers = new string[] { "No", "Tanggal Keluar", "No BON", "Nama Barang", "Jumlah Barang", "Satuan", "No. Invoice", "Buyer", "Dokumen", "Dokumen1", "Dokumen2", "RO" };
                var subHeaders = new string[] { "Jenis", "Nomor", "Tanggal" };


                sheet.Cells["A5"].LoadFromDataTable(result, false, OfficeOpenXml.Table.TableStyles.Light16);

                sheet.Cells["A2"].Value = Tittle[0];
                sheet.Cells["A2:L2"].Merge = true;

                sheet.Cells["I3"].Value = headers[8];
                sheet.Cells["I3:K3"].Merge = true;

                foreach (var i in Enumerable.Range(0, 8))
                {
                    var col = (char)('A' + i);
                    sheet.Cells[$"{col}3"].Value = headers[i];
                    sheet.Cells[$"{col}3:{col}4"].Merge = true;
                }

                foreach (var i in Enumerable.Range(0, 3))
                {
                    var col = (char)('I' + i);
                    sheet.Cells[$"{col}4"].Value = subHeaders[i];
                }

                foreach (var i in Enumerable.Range(0, 1))
                {
                    var col = (char)('L' + i);
                    sheet.Cells[$"{col}3"].Value = headers[i + 11];
                    sheet.Cells[$"{col}3:{col}4"].Merge = true;
                }
                sheet.Cells["A1:L4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:L4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells["A1:L4"].Style.Font.Bold = true;
                //-----------


                var countdata = query.Count();

                sheet.Cells[$"A{countdata + 11}"].LoadFromDataTable(result2, false, OfficeOpenXml.Table.TableStyles.Light16);

                var Tittle1 = new string[] { "PERINCIAN PEMAKAIAN BAHAN BAKU DAN BAHAN PENOLONG" };
                var headers1 = new string[] { "No", "No. RO", "Kode Barang", "Nama Barang", "Jumlah Pemakaian", "Satuan", "Dokumen", "Dokumen1", "Dokumen2" };
                var subHeaders1 = new string[] { "Jenis", "Nomor", "Tanggal" };

                sheet.Cells[$"A{countdata + 8}"].Value = Tittle1[0];
                sheet.Cells[$"A{countdata + 8}:I{countdata + 8}"].Merge = true;

                sheet.Cells[$"G{countdata + 9}"].Value = headers1[6];
                sheet.Cells[$"G{countdata + 9}:I{countdata + 9}"].Merge = true;

                foreach (var i in Enumerable.Range(0, 6))
                {
                    var col = (char)('A' + i);
                    sheet.Cells[$"{col}{countdata + 9}"].Value = headers1[i];
                    sheet.Cells[$"{col}{countdata + 9}:{col}{countdata + 10}"].Merge = true;
                }

                foreach (var i in Enumerable.Range(0, 3))
                {
                    var col = (char)('G' + i);
                    sheet.Cells[$"{col}{countdata + 10}"].Value = subHeaders1[i];
                }


                sheet.Cells[$"A{countdata + 8}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                sheet.Cells[$"A{countdata + 8}:I{countdata + 10}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{countdata + 8}:I{countdata + 10}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[$"A{countdata + 8}:I{countdata + 10}"].Style.Font.Bold = true;

                var widths = new int[] { 5, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };
                foreach (var i in Enumerable.Range(0, headers.Length))
                {
                    sheet.Column(i + 1).Width = widths[i];
                }

                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);
                return stream;
            }
        }
        private async Task<List<TraceableOutBeacukaiDetailViewModel>> GetDetail(HashSet<string> listRO)
        {
            string Uri = "traceable/out/detail";
            IHttpClientService httpClient = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.PurchasingEndpoint}{Uri}", new StringContent(JsonConvert.SerializeObject(listRO), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<TraceableOutBeacukaiDetailViewModel> viewModel = JsonConvert.DeserializeObject<List<TraceableOutBeacukaiDetailViewModel>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
