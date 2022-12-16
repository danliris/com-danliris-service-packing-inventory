using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport
{
    public class GarmentRecapOmzetReportService : IGarmentRecapOmzetReportService
    {
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IServiceProvider _serviceProvider;

        private readonly IIdentityProvider _identityProvider;

        public GarmentRecapOmzetReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentRecapOmzetReportViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryPL = plrepository.ReadAll();
            var queryInv = repository.ReadAll();
            var quaryInvItem = itemrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);

            var Query = (
                        from a in queryPL
                        join b in queryInv on a.Id equals b.PackingListId
                        join c in quaryInvItem on b.Id equals c.GarmentShippingInvoiceId
                        where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
                              && a.Accounting == true && a.Omzet == true && b.PEBNo != null && b.PEBNo != "-" && b.PEBNo != " "
                              && b.PEBDate != DateTimeOffset.MinValue
                        select new GarmentRecapOmzetReportViewModel
                        {
                            TruckingDate = a.TruckingDate,
                            BuyerAgentCode = a.BuyerAgentCode,
                            BuyerAgentName = a.BuyerAgentName,
                            Destination = a.Destination,
                            ComodityName = c.ComodityName,
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = b.InvoiceDate,
                            PEBNo = b.PEBNo,
                            PEBDate = b.PEBDate,
                            Quantity = c.Quantity,
                            UOMUnit = c.UomUnit,
                            CurrencyCode = c.CurrencyCode,
                            Amount = c.Amount,
                            Omzet = "Y",
                            Rate = 0,
                            AmountIDR = 0,
                        }).ToList();

            var newQ = Query.GroupBy(s => new { s.InvoiceNo, s.ComodityName, s.UOMUnit }).Select(d => new GarmentRecapOmzetReportViewModel()
            {

                TruckingDate = d.FirstOrDefault().TruckingDate,
                BuyerAgentCode = d.FirstOrDefault().BuyerAgentCode,
                BuyerAgentName = d.FirstOrDefault().BuyerAgentName,
                Destination = d.FirstOrDefault().Destination,
                ComodityName = d.Key.ComodityName,
                InvoiceNo = d.Key.InvoiceNo,
                InvoiceDate = d.FirstOrDefault().InvoiceDate,
                PEBNo = d.FirstOrDefault().PEBNo,
                PEBDate = d.FirstOrDefault().PEBDate,
                Quantity = d.Sum(x => x.Quantity),
                UOMUnit = d.Key.UOMUnit,
                CurrencyCode = d.FirstOrDefault().CurrencyCode,
                Omzet = d.FirstOrDefault().Omzet,
                Amount = d.Sum(x => x.Amount),
                Rate = 0,
                AmountIDR = 0,

            }).ToList();

            var currencyFilters = Query
                           .GroupBy(o => new { o.PEBDate, o.CurrencyCode })
                           .Select(o => new CurrencyFilter { date = o.Key.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime, code = o.Key.CurrencyCode })
                           .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var data in Query)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date == data.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime).Select(s => s.rate).LastOrDefault());

                data.Rate = rate;
                data.AmountIDR = rate * data.Amount;
            }

            //return newQ;

            var newQ1 = Query.GroupBy(s => new { s.InvoiceNo, s.ComodityName, s.UOMUnit }).Select(d => new GarmentRecapOmzetReportViewModel()
            {

                TruckingDate = d.FirstOrDefault().TruckingDate,
                BuyerAgentCode = d.FirstOrDefault().BuyerAgentCode,
                BuyerAgentName = d.FirstOrDefault().BuyerAgentName,
                Destination = d.FirstOrDefault().Destination,
                ComodityName = d.Key.ComodityName,
                InvoiceNo = d.Key.InvoiceNo,
                InvoiceDate = d.FirstOrDefault().InvoiceDate,
                PEBNo = d.FirstOrDefault().PEBNo,
                PEBDate = d.FirstOrDefault().PEBDate,
                Quantity = d.Sum(x => x.Quantity),
                UOMUnit = d.Key.UOMUnit,
                CurrencyCode = d.FirstOrDefault().CurrencyCode,
                Rate = d.FirstOrDefault().Rate,
                Omzet = d.FirstOrDefault().Omzet,
                Amount = d.Sum(x => x.Amount),
                AmountIDR = d.Sum(x => x.AmountIDR),
            }).ToList();

            return newQ1;
        }

        public ListResult<GarmentRecapOmzetReportViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentRecapOmzetReportViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "N E G A R A", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KODE BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS BARANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "INVOICE NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "INVOICE TGL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PEB NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PEB TGL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "MT UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "RATE", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "JML IDR", DataType = typeof(decimal) });

            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", 0, "", "", 0, 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "PT. DAN LIRIS";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:L2"].Value = "BUKU PENJUALAN EXPORT GARMENT";
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;

                    sheet.Cells[$"A3:L3"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
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
                int rowPosition = 1;
                var grandTotalByUom = new List<TotalByUom>();

                foreach (var d in Query)
                {
                    index++;

                    var currentUom = grandTotalByUom.FirstOrDefault(c => c.uom == d.UOMUnit);
                    if (currentUom == null)
                    {
                        grandTotalByUom.Add(new TotalByUom
                        {
                            uom = d.UOMUnit,
                            quantity = d.Quantity,
                            amount = d.Amount,
                            amount1 = d.AmountIDR
                        });
                    }
                    else
                    {
                        currentUom.quantity += d.Quantity;
                        currentUom.amount += d.Amount;
                        currentUom.amount1 += d.AmountIDR;
                    }

                    string TrckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PEBDate = d.PEBDate == DateTimeOffset.MinValue ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    result.Rows.Add(index, TrckDate, d.BuyerAgentName, d.Destination, d.BuyerAgentCode, d.ComodityName, d.InvoiceNo, InvDate, d.PEBNo,
                                    PEBDate, d.Quantity, d.UOMUnit, d.CurrencyCode, d.Amount, d.Rate, d.AmountIDR);
                }
                //
                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, "", grandTotalByUom[i].amount, 0, grandTotalByUom[i].amount1);
                    }
                    else
                    {
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, "", grandTotalByUom[i].amount, 0, grandTotalByUom[i].amount1);
                    }
                }
                //
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "PT. DAN LIRIS";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:L2"].Value = "BUKU PENJUALAN EXPORT GARMENT";
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;

                    sheet.Cells[$"A3:L3"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
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


        //async Task<GarmentDetailCurrency> GetCurrencies(string code, DateTime date)
        //{
        //    string uri = "master/garment-detail-currencies/single-by-code-date";
        //    IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

        //    var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.CoreEndpoint}{uri}", new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));
        //    var response = httpClient.GetAsync($"{ApplicationSetting.CoreEndpoint}{uri}?code={code}&stringDate={date.ToString()}").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = response.Content.ReadAsStringAsync().Result;
        //        Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
        //        GarmentDetailCurrency viewModel = JsonConvert.DeserializeObject<GarmentDetailCurrency>(result.GetValueOrDefault("data").ToString());
        //        return viewModel;
        //    }
        //    else
        //    {
        //        return new GarmentDetailCurrency();
        //    }
        //}

        async Task<List<GarmentDetailCurrency>> GetCurrencies(List<CurrencyFilter> filters)
        {
            string uri = "master/garment-detail-currencies/single-by-code-date-peb";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.CoreEndpoint}{uri}", new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<GarmentDetailCurrency> viewModel = JsonConvert.DeserializeObject<List<GarmentDetailCurrency>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new List<GarmentDetailCurrency>();
            }
        }

        private class TotalByUom
        {
            public string uom { get; set; }
            public double quantity { get; set; }
            public decimal amount { get; set; }
            public decimal amount1 { get; set; }
        }
    }
}
