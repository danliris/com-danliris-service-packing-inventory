using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport
{
    public class GarmentDetailOmzetByUnitReportService : IGarmentDetailOmzetByUnitReportService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityProvider _identityProvider;

        public GarmentDetailOmzetByUnitReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentDetailOmzetByUnitReportViewModel> GetData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var quaryInvItem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var expendGood = GetExpenditureGood(DateFrom, DateTo, unit, offset);

            var ROs = expendGood.Select(x => x.RONo).ToArray();
            var invo = expendGood.Select(x => x.Invoice).ToArray();

            queryInv = queryInv.Where(x => x.PEBDate != DateTimeOffset.MinValue);

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryPL = queryPL.Where(w => w.Omzet == true);

            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);
            List<GarmentDetailOmzetByUnitReportViewModel> omzetgmt = new List<GarmentDetailOmzetByUnitReportViewModel>();

            var Queryshipping = (from a in queryInv
                                 join b in quaryInvItem on a.Id equals b.GarmentShippingInvoiceId
                                 join c in queryPL on a.PackingListId equals c.Id
                                 where ROs.Contains(b.RONo) && invo.Contains(a.InvoiceNo)
                                 && a.PEBDate != DateTimeOffset.MinValue && b.UnitCode != "SMP1"
                                 select new GarmentDetailOmzetByUnitReportViewModel
                                 {
                                     Urutan = "A",
                                     PEBDate = a.PEBDate,
                                     TruckingDate = c.TruckingDate,
                                     RONumber = b.RONo,
                                     InvoiceNo = a.InvoiceNo,
                                     Quantity = b.UomUnit.Substring(0, 3) == "SET" || b.UomUnit.Substring(0, 3) == "PAC" ? b.Quantity * 2 : b.Quantity,
                                     UOMUnit = b.UomUnit,
                                     CurrencyCode = b.CurrencyCode,
                                     Amount = b.Amount,

                                 }).Distinct().ToList();

            var Query1 = (from a in expendGood
                          join b in Queryshipping on new { invoice = a.Invoice.Trim(), rono = a.RONo.Trim() } equals new { invoice = b.InvoiceNo.Trim(), rono = b.RONumber.Trim() } into omzets
                          from bb in omzets.DefaultIfEmpty()
                          select new GarmentDetailOmzetByUnitReportViewModel
                          {
                              Urutan = "A",
                              InvoiceNo = a.Invoice.TrimEnd(),
                              PEBDate = bb == null ? DateTimeOffset.MinValue : bb.PEBDate,
                              TruckingDate = bb == null ? DateTimeOffset.MinValue : bb.TruckingDate,
                              BuyerAgentName = a.Buyer.Code.TrimEnd() + " - " + a.Buyer.Name.TrimEnd(),
                              ComodityName = a.Comodity.Name.TrimEnd(),
                              UnitCode = a.Unit.Code.TrimEnd(),
                              RONumber = a.RONo.TrimEnd(),
                              Quantity = 0,
                              UOMUnit = "PCS",
                              CurrencyCode = "USD", 
                              Amount = bb == null ? 0 : bb.Amount,
                              ArticleStyle = a.Article.TrimEnd(),
                              QuantityInPCS = a.TotalQuantity,
                          }).Distinct().ToList();

            //
            var sampleExpendGood = GetSampleExpenditureGood(DateFrom, DateTo, unit, offset);
            var RO1s = sampleExpendGood.Select(x => x.RONo).ToArray();
            var invo1 = sampleExpendGood.Select(x => x.Invoice).ToArray();
            var QueryshippingSmpl = (from a in queryInv
                                     join b in quaryInvItem on a.Id equals b.GarmentShippingInvoiceId
                                     join c in queryPL on a.PackingListId equals c.Id
                                     where RO1s.Contains(b.RONo) && invo1.Contains(a.InvoiceNo)
                                     && a.PEBDate != DateTimeOffset.MinValue && b.UnitCode == "SMP1"
                                     select new GarmentDetailOmzetByUnitReportViewModel
                                     {
                                         Urutan = "A",
                                         PEBDate = a.PEBDate,
                                         TruckingDate = c.TruckingDate,
                                         RONumber = b.RONo.TrimEnd(),
                                         InvoiceNo = a.InvoiceNo.TrimEnd(),
                                         Quantity = b.UomUnit.Substring(0, 3) == "SET" || b.UomUnit.Substring(0, 3) == "PAC" ? b.Quantity * 2 : b.Quantity,
                                         UOMUnit = b.UomUnit,
                                         CurrencyCode = b.CurrencyCode,
                                         Amount = b.Amount,
                                     }).Distinct().ToList();

            var Query2 = (from c in sampleExpendGood
                          join d in QueryshippingSmpl on new { invoice = c.Invoice.TrimEnd(), rono = c.RONo.TrimEnd() } equals new { invoice = d.InvoiceNo.TrimEnd(), rono = d.RONumber.TrimEnd() } into omzets1
                          from dd in omzets1.DefaultIfEmpty()
                          select new GarmentDetailOmzetByUnitReportViewModel
                          {
                              Urutan = "A",
                              InvoiceNo = c.Invoice.TrimEnd(),
                              PEBDate = dd == null ? DateTimeOffset.MinValue : dd.PEBDate,
                              TruckingDate = dd == null ? DateTimeOffset.MinValue : dd.TruckingDate,
                              BuyerAgentName = c.Buyer.Code.TrimEnd() + " - " + c.Buyer.Name.TrimEnd(),
                              ComodityName = c.Comodity.Name.TrimEnd(),
                              UnitCode = c.Unit.Code.TrimEnd(),
                              RONumber = c.RONo.TrimEnd(),
                              Quantity = 0,
                              UOMUnit = "PCS",
                              CurrencyCode = "USD",
                              Amount = dd == null ? 0 : dd.Amount,
                              ArticleStyle = c.Article.TrimEnd(),
                              QuantityInPCS = c.TotalQuantity,
                          }).Distinct().ToList();
            //
            var CombineData = Query1.Union(Query2).ToList();
            // Final Query Omzet
            var Query = (from a in CombineData
                         join b in queryInv on a.InvoiceNo equals b.InvoiceNo
                         join c in queryPL on b.PackingListId equals c.Id
                         select new GarmentDetailOmzetByUnitReportViewModel
                         {
                             Urutan = "A",
                             InvoiceNo = a.InvoiceNo,
                             PEBDate = b.PEBDate,
                             TruckingDate = c.TruckingDate,
                             BuyerAgentName = a.BuyerAgentName,
                             ComodityName = a.ComodityName,
                             UnitCode = a.UnitCode,
                             RONumber = a.RONumber,
                             Quantity = 0,
                             UOMUnit = "PCS",
                             CurrencyCode = "USD",
                             Amount = a.Amount,
                             ArticleStyle = a.ArticleStyle,
                             QuantityInPCS = a.QuantityInPCS,
                         }).Distinct().ToList();

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
   
            return Query.Distinct().OrderBy(w => w.UnitCode).ThenBy(w => w.PEBDate).ThenBy(w => w.InvoiceNo).ToList();

        }

        public ListResult<GarmentDetailOmzetByUnitReportViewModel> GetReportData(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(unit, dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentDetailOmzetByUnitReportViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(string unit, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(unit, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KONFEKSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ITEM", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "R/O", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "STYLE   ORD/ART NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RATE", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IN IDR", DataType = typeof(decimal) });


            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {

                result.Rows.Add("", "", "", "", "", "", "", "", "", 0, "", 0, "", 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET EXPORT GARMENT";
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

                    string TruckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PEBDate = d.PEBDate == new DateTime(1970, 1, 1) ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string Qty = string.Format("{0:N0}", d.QuantityInPCS);
                    //string Qty1 = string.Format("{0:N0}", d.QuantityInPCS);
                    string AmtUSD = string.Format("{0:N2}", d.Amount);
                    string Rate = string.Format("{0:N2}", d.Rate);
                    string AmtIDR = string.Format("{0:N2}", d.AmountIDR);

                    result.Rows.Add(index, d.UnitCode, d.InvoiceNo, TruckDate, PEBDate, d.BuyerAgentName, d.ComodityName, d.RONumber, d.ArticleStyle, d.QuantityInPCS, d.UOMUnit, d.Amount, d.CurrencyCode, d.Rate, d.AmountIDR);
                }

                //string TotQty = string.Format("{0:N2}", Query.Sum(x => x.QuantityInPCS));
                //string TotUSD = string.Format("{0:N2}", Query.Sum(x => x.Amount));
                //string TotIDR = string.Format("{0:N2}", Query.Sum(x => x.AmountIDR));

                double TotQty = Query.Sum(x => x.QuantityInPCS);
                decimal TotUSD = Query.Sum(x => x.Amount);
                decimal TotIDR = Query.Sum(x => x.AmountIDR);

                result.Rows.Add("", "", "", "", " T  O  T  A  L  : ", "", "", "", "", TotQty, "", TotUSD, "", 0, TotIDR);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "LAPORAN OMZET EXPORT GARMENT";
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

        async Task<List<GarmentCurrency>> GetCurrencies(List<CurrencyFilter> filters)
        {
            string uri = "master/garment-detail-currencies/single-by-code-date-peb";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{ApplicationSetting.CoreEndpoint}{uri}", new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<GarmentCurrency> viewModel = JsonConvert.DeserializeObject<List<GarmentCurrency>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new List<GarmentCurrency>();
            }
        }

        public List<GarmentExpenditureGood> GetExpenditureGood(DateTime dateFrom, DateTime dateTo, string unitcode, int offset)
        {
            string expenditureUri = "expenditure-goods/forOmzet";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.ProductionEndpoint}{expenditureUri}?dateFrom={dateFrom}&dateTo={dateTo}&unitcode={unitcode}&offset={offset}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                List<GarmentExpenditureGood> viewModel;
                //{
                viewModel = JsonConvert.DeserializeObject<List<GarmentExpenditureGood>>(result.GetValueOrDefault("data").ToString());
                //}
                return viewModel;
            }
            else
            {
                return new List<GarmentExpenditureGood>();
            }
        }
        //
        public List<GarmentExpenditureGood> GetSampleExpenditureGood(DateTime dateFrom, DateTime dateTo, string unitcode, int offset)
        {
            string expenditureUri = "garment-sample-expenditure-goods/forOmzet";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.ProductionEndpoint}{expenditureUri}?dateFrom={dateFrom}&dateTo={dateTo}&unitcode={unitcode}&offset={offset}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                List<GarmentExpenditureGood> viewModel;
                //{
                viewModel = JsonConvert.DeserializeObject<List<GarmentExpenditureGood>>(result.GetValueOrDefault("data").ToString());
                //}
                return viewModel;
            }
            else
            {
                return new List<GarmentExpenditureGood>();
            }
        }
    }
}
