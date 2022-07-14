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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetAnnualByUnitReport
{ 
    public class GarmentOmzetAnnualByUnitReportService : IGarmentOmzetAnnualByUnitReportService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IGarmentShippingInvoiceItemRepository shippingInvoiceItemRepository;
        private readonly IGarmentPackingListRepository packingListRepository;
        private readonly IIdentityProvider _identityProvider;

        public static readonly string[] MONTHS = { "01-JANUARI", "02-FEBRUARI", "03-MARET", "04-APRIL", "05-MEI", "06-JUNI", "07-JULI", "08-AGUSTUS", "09-SEPTEMBER", "10-OKTOBER", "11-NOVEMBER", "12-DESEMBER" };
        public static readonly string[] MONTH_NAMES = { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI", "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

        public GarmentOmzetAnnualByUnitReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            shippingInvoiceItemRepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<AnnualOmzetByUnitViewModel> GetData(int year, int offset)
        {
            DateTime DateFrom = new DateTime(year, 1, 1, 0, 0, 0);
            DateTime DateTo = new DateTime(year, 12, 31, 0, 0, 0);

            var queryInv = shippingInvoiceRepository.ReadAll();
            var quaryInvItem = shippingInvoiceItemRepository.ReadAll();
            var queryPL = packingListRepository.ReadAll();

            queryInv = queryInv.Where(x => x.PEBDate != DateTimeOffset.MinValue);

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryPL = queryPL.Where(w => w.Omzet == true);

            //DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            //DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var expendGood = GetExpenditureGood(DateFrom, DateTo, offset);

            var ROs = expendGood.Select(x => x.RONo).ToArray();
            var invo = expendGood.Select(x => x.Invoice).ToArray();

            //
            var Queryshipping = (from a in queryInv
                                 join b in quaryInvItem on a.Id equals b.GarmentShippingInvoiceId
                                 join c in queryPL on a.PackingListId equals c.Id
                                 where ROs.Contains(b.RONo) && invo.Contains(a.InvoiceNo)
                                 select new GarmentDetailOmzetByUnitReportViewModel
                                 {
                                     InvoiceNo = a.InvoiceNo,
                                     PEBDate = a.PEBDate,
                                     TruckingDate = c.TruckingDate,
                                     RONumber = b.RONo,
                                     Amount = b.Amount,

                                 }).Distinct().ToList();
            //                 
            var Query1 = (from a in expendGood
                          join b in Queryshipping on new { invoice = a.Invoice.Trim(), rono = a.RONo.Trim() } equals new { invoice = b.InvoiceNo.Trim(), rono = b.RONumber.Trim() } into omzets
                          from bb in omzets.DefaultIfEmpty()
                          select new GarmentDetailOmzetByUnitReportViewModel
                          {
                              InvoiceNo = a.Invoice.TrimEnd(),
                              PEBDate = bb == null ? DateTimeOffset.MinValue : bb.PEBDate,
                              TruckingDate = bb == null ? DateTimeOffset.MinValue : bb.TruckingDate,
                              RONumber = a.RONo.TrimEnd(),
                              UnitCode = a.Unit.Code,
                              UnitName = a.Unit.Name.TrimEnd(),
                              CurrencyCode = "USD",
                              Amount = bb == null ? 0 : bb.Amount,
                          }).Distinct().ToList();
            //
            var Query = (from a in Query1
                         join b in queryInv on a.InvoiceNo equals b.InvoiceNo
                         join c in queryPL on b.PackingListId equals c.Id
                         select new GarmentDetailOmzetByUnitReportViewModel
                         {
                             InvoiceNo = a.InvoiceNo,
                             PEBDate = b.PEBDate,
                             TruckingDate = c.TruckingDate,
                             Month = MONTHS[c.TruckingDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Month - 1],
                             MonthName = MONTH_NAMES[c.TruckingDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Month - 1],
                             UnitCode = a.UnitCode,
                             UnitName = a.UnitName,
                             RONumber = a.RONumber,
                             CurrencyCode = "USD",
                             Amount = a.Amount,
                         }).Distinct().ToList();
            //
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
            //
            var QueryOmzet1 = (from a in Query where a.UnitCode == "C1A"

                               select new { a.Month, a.MonthName, a.UnitName, a.Amount, a.AmountIDR })
                               .GroupBy(x => new { x.Month, x.MonthName, x.UnitName }, (key, group) => new AnnualOmzetByUnitViewModel
                               {
                                   Month = key.Month,
                                   MonthName = key.MonthName,
                                   Amount1 = group.Sum(x => x.Amount),
                                   Amount1IDR = group.Sum(x => x.AmountIDR),
                                   Amount2 = 0,
                                   Amount2IDR = 0,
                                   Amount3 = 0,
                                   Amount3IDR = 0,
                                   Amount4 = 0,
                                   Amount4IDR = 0,
                                   Amount5 = 0,
                                   Amount5IDR = 0,
                       }).OrderBy(i => i.Month);
            //
            var QueryOmzet2 = (from a in Query
                               where a.UnitCode == "C1B"

                               select new { a.Month, a.MonthName, a.UnitName, a.Amount, a.AmountIDR })
                               .GroupBy(x => new { x.Month, x.MonthName, x.UnitName }, (key, group) => new AnnualOmzetByUnitViewModel
                               {
                                   Month = key.Month,
                                   MonthName = key.MonthName,
                                   Amount1 = 0,
                                   Amount1IDR = 0,
                                   Amount2 = group.Sum(x => x.Amount),
                                   Amount2IDR = group.Sum(x => x.AmountIDR),
                                   Amount3 = 0,
                                   Amount3IDR = 0,
                                   Amount4 = 0,
                                   Amount4IDR = 0,
                                   Amount5 = 0,
                                   Amount5IDR = 0,
                               }).OrderBy(i => i.Month);
            //
            var QueryOmzet3 = (from a in Query
                               where a.UnitCode == "C2A"

                               select new { a.Month, a.MonthName, a.UnitName, a.Amount, a.AmountIDR })
                               .GroupBy(x => new { x.Month, x.MonthName, x.UnitName }, (key, group) => new AnnualOmzetByUnitViewModel
                               {
                                   Month = key.Month,
                                   MonthName = key.MonthName,
                                   Amount1 = 0,
                                   Amount1IDR = 0,
                                   Amount2 = 0,
                                   Amount2IDR = 0,
                                   Amount3 = group.Sum(x => x.Amount),
                                   Amount3IDR = group.Sum(x => x.AmountIDR),
                                   Amount4 = 0,
                                   Amount4IDR = 0,
                                   Amount5 = 0,
                                   Amount5IDR = 0,
                               }).OrderBy(i => i.Month);
            //
            var QueryOmzet4 = (from a in Query
                               where a.UnitCode == "C2B"

                               select new { a.Month, a.MonthName, a.UnitName, a.Amount, a.AmountIDR })
                               .GroupBy(x => new { x.Month, x.MonthName, x.UnitName }, (key, group) => new AnnualOmzetByUnitViewModel
                               {
                                   Month = key.Month,
                                   MonthName = key.MonthName,
                                   Amount1 = 0,
                                   Amount1IDR = 0,
                                   Amount2 = 0,
                                   Amount2IDR = 0,
                                   Amount3 = 0,
                                   Amount3IDR = 0,
                                   Amount4 = group.Sum(x => x.Amount),
                                   Amount4IDR = group.Sum(x => x.AmountIDR),
                                   Amount5 = 0,
                                   Amount5IDR = 0,
                               }).OrderBy(i => i.Month);
            //
            var QueryOmzet5 = (from a in Query
                              where a.UnitCode == "C2C"

                              select new { a.Month, a.MonthName, a.UnitName, a.Amount, a.AmountIDR })
                               .GroupBy(x => new { x.Month, x.MonthName, x.UnitName }, (key, group) => new AnnualOmzetByUnitViewModel
                               {
                                   Month = key.Month,
                                   MonthName = key.MonthName,
                                   Amount1 = 0,
                                   Amount1IDR = 0,
                                   Amount2 = 0,
                                   Amount2IDR = 0,
                                   Amount3 = 0,
                                   Amount3IDR = 0,
                                   Amount4 = 0,
                                   Amount4IDR = 0,
                                   Amount5 = group.Sum(x => x.Amount),
                                   Amount5IDR = group.Sum(x => x.AmountIDR),
                               }).OrderBy(i => i.Month);

            var QueryOmzet = QueryOmzet1.Union(QueryOmzet2).Union(QueryOmzet3).Union(QueryOmzet4).Union(QueryOmzet5);

            //

            var QueryAnnualOmzet = (from a in QueryOmzet
                               
                                    select new { a.Month, a.MonthName, a.Amount1, a.Amount1IDR, a.Amount2, a.Amount2IDR, a.Amount3, a.Amount3IDR, a.Amount4, a.Amount4IDR, a.Amount5, a.Amount5IDR })
                                    .GroupBy(x => new { x.Month, x.MonthName }, (key, group) => new AnnualOmzetByUnitViewModel
                                    {
                                       Month = key.Month,
                                       MonthName = key.MonthName,
                                       Amount1 = group.Sum(x => x.Amount1),
                                       Amount1IDR = group.Sum(x => x.Amount1IDR),
                                       Amount2 = group.Sum(x => x.Amount2),
                                       Amount2IDR = group.Sum(x => x.Amount2IDR),
                                       Amount3 = group.Sum(x => x.Amount3),
                                       Amount3IDR = group.Sum(x => x.Amount3IDR),
                                       Amount4 = group.Sum(x => x.Amount4),
                                       Amount4IDR = group.Sum(x => x.Amount4IDR),
                                       Amount5 = group.Sum(x => x.Amount5),
                                       Amount5IDR = group.Sum(x => x.Amount5IDR),
                                    }).OrderBy(i => i.Month).ToList();
            
            return QueryAnnualOmzet.OrderBy(x => x.Month).ToList();
        }
        public ListResult<AnnualOmzetByUnitViewModel> GetReportData(int year, int offset)
        {
            var data = GetData(year, offset);
            var total = data.Count;

            return new ListResult<AnnualOmzetByUnitViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(int year, int offset)
        {

            DateTime DateFrom = new DateTime(year, 1, 1, 0, 0, 0);
            DateTime DateTo = new DateTime(year, 12, 31, 0, 0, 0);

            var Query = GetData(year, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "BULAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT USD - C1A", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR - C1A", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT USD - C1B", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR - C1B", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT USD - C2A", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR - C2A", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT USD - C2B", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR - C2B", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT USD - C2C", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR - C2C", DataType = typeof(decimal) });

            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {

                result.Rows.Add("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:K1"].Value = "LAPORAN OMZET EXPORT GARMENT";
                    sheet.Cells[$"A1:K1"].Merge = true;
                    sheet.Cells[$"A1:K1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:K1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:K1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:K2"].Value = string.Format("PERIODE TAHUN : {0}", DateTo.Year.ToString(), new CultureInfo("id-ID"));
                    sheet.Cells[$"A2:K2"].Merge = true;
                    sheet.Cells[$"A2:K2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:K2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:K2"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A4"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;
                    
                    result.Rows.Add(d.MonthName, d.Amount1, d.Amount1IDR, d.Amount2, d.Amount2IDR, d.Amount3, d.Amount3IDR, d.Amount4, d.Amount4IDR, d.Amount5, d.Amount5IDR);
                }

                //string TotQty = string.Format("{0:N2}", Query.Sum(x => x.QuantityInPCS));
                //string TotUSD = string.Format("{0:N2}", Query.Sum(x => x.Amount));
                //string TotIDR = string.Format("{0:N2}", Query.Sum(x => x.AmountIDR));

                decimal TotUSD1 = Query.Sum(x => x.Amount1);
                decimal TotIDR1 = Query.Sum(x => x.Amount1IDR);
                decimal TotUSD2 = Query.Sum(x => x.Amount2);
                decimal TotIDR2 = Query.Sum(x => x.Amount2IDR);
                decimal TotUSD3 = Query.Sum(x => x.Amount3);
                decimal TotIDR3 = Query.Sum(x => x.Amount3IDR);
                decimal TotUSD4 = Query.Sum(x => x.Amount4);
                decimal TotIDR4 = Query.Sum(x => x.Amount4IDR);
                decimal TotUSD5 = Query.Sum(x => x.Amount5);
                decimal TotIDR5 = Query.Sum(x => x.Amount5IDR);
                //
                decimal AVGUSD1 = Math.Round(TotUSD1 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGIDR1 = Math.Round(TotIDR1 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGUSD2 = Math.Round(TotUSD2 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGIDR2 = Math.Round(TotIDR2 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGUSD3 = Math.Round(TotUSD3 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGIDR3 = Math.Round(TotIDR3 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGUSD4 = Math.Round(TotUSD4 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGIDR4 = Math.Round(TotIDR4 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGUSD5 = Math.Round(TotUSD5 / 12, 2, MidpointRounding.AwayFromZero);
                decimal AVGIDR5 = Math.Round(TotIDR5 / 12, 2, MidpointRounding.AwayFromZero);

                result.Rows.Add("TOTAL   : ", TotUSD1, TotIDR1, TotUSD2, TotIDR2, TotUSD3, TotIDR3, TotUSD4, TotIDR4, TotUSD5, TotIDR5);
                result.Rows.Add("AVERAGE : ", AVGUSD1, AVGIDR1, AVGUSD2, AVGIDR2, AVGUSD3, AVGIDR3, AVGUSD4, AVGIDR4, AVGUSD5, AVGIDR5);

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:K1"].Value = "LAPORAN OMZET EXPORT GARMENT";
                    sheet.Cells[$"A1:K1"].Merge = true;
                    sheet.Cells[$"A1:K1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:K1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:K1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:K2"].Value = string.Format("PERIODE TAHUN : {0}", DateTo.Year.ToString(), new CultureInfo("id-ID"));
                    sheet.Cells[$"A2:K2"].Merge = true;
                    sheet.Cells[$"A2:K2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:K2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:K2"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells["A4"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

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

        public List<GarmentExpenditureGood> GetExpenditureGood(DateTime dateFrom, DateTime dateTo, int offset)
        {
            string expenditureUri = "expenditure-goods/forOmzetAnnual";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.ProductionEndpoint}{expenditureUri}?dateFrom={dateFrom}&dateTo={dateTo}&offset={offset}").Result;
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
