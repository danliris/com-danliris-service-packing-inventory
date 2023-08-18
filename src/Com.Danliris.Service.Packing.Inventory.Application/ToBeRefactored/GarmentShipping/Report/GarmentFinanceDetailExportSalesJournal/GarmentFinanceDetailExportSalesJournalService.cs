using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using OfficeOpenXml;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport;
using System.Net.Http;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceDetailExportSalesJournalService : IGarmentFinanceDetailExportSalesJournalService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;

        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentFinanceDetailExportSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }
        //private GarmentCurrency GetCurrencyPEBDate(string stringDate)
        //{
        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        MissingMemberHandling = MissingMemberHandling.Ignore
        //    };

        //    var httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

        //    var currencyUri = ApplicationSetting.CoreEndpoint + $"master/garment-detail-currencies/sales-debtor-currencies-peb?stringDate={stringDate}";
        //    var currencyResponse = httpClient.GetAsync(currencyUri).Result.Content.ReadAsStringAsync();

        //    var currencyResult = new BaseResponse<GarmentCurrency>()
        //    {
        //        data = new GarmentCurrency()
        //    };
        //    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(currencyResponse.Result);
        //    var json = result.Single(p => p.Key.Equals("data")).Value;
        //    var data = JsonConvert.DeserializeObject<GarmentCurrency>(json.ToString());

        //    return data;
        //}
        public List<GarmentFinanceDetailExportSalesJournalViewModel> GetReportQuery(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            //DateTime dateFrom = new DateTime(year, month, 1);
            //int nextYear = month == 12 ? year + 1 : year;
            //int nextMonth = month == 12 ? 1 : month + 1;
            //DateTime dateTo = new DateTime(nextYear, nextMonth, 1);

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            List<GarmentFinanceDetailExportSalesJournalViewModel> data = new List<GarmentFinanceDetailExportSalesJournalViewModel>();
            List<GarmentFinanceDetailExportSalesJournalViewModel> data1 = new List<GarmentFinanceDetailExportSalesJournalViewModel>();

            var queryInv = repository.ReadAll();
         
            var queryPL = plrepository.ReadAll()
                  .Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date
                     && (w.InvoiceType == "DL" || w.InvoiceType == "DS" || w.InvoiceType == "DLR" || w.InvoiceType == "SMR"));

            var joinQuery = from a in queryInv
                            join b in queryPL on a.PackingListId equals b.Id
                            where a.IsDeleted == false && b.IsDeleted == false
                            select new GarmentFinanceDetailExportSalesJournalViewModel
                            {
                                invoicetype = b.InvoiceType,
                                invoiceno = a.InvoiceNo,
                                truckingdate = b.TruckingDate,
                                pebdate = a.PEBDate,
                                buyer = a.BuyerAgentCode + "-" + a.BuyerAgentName,
                                pebno = a.PEBNo,                   
                                currencycode = "USD",
                                rate = 1,
                                amount = a.TotalAmount,
                                coaname = "-",
                                account = "-",
                                debit = 0,
                                credit = 0,
                            };
            //
            var currencyFilters = joinQuery
                        .GroupBy(o => new { o.pebdate, o.currencycode })
                        //.Select(o => new CurrencyFilter { date = o.Key.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime, code = o.Key.CurrencyCode })
                        .Select(o => new CurrencyFilter { date = o.Key.pebdate.AddHours(offset).Date, code = o.Key.currencycode })
                        .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var dataz in joinQuery)
            {
                // rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date == data.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime).Select(s => s.rate).LastOrDefault());
                rate = Convert.ToDecimal(currencies.Where(q => q.code == dataz.currencycode && q.date.Date == dataz.pebdate.AddHours(offset).Date).Select(s => s.rate).LastOrDefault());

                dataz.rate = rate;
            
                data1.Add(new GarmentFinanceDetailExportSalesJournalViewModel
                {
                    invoicetype = dataz.invoicetype,
                    invoiceno = dataz.invoiceno,
                    truckingdate = dataz.truckingdate,
                    buyer = dataz.buyer,
                    pebno = dataz.pebno,
                    pebdate = dataz.pebdate,
                    currencycode = dataz.currencycode,
                    rate = rate,
                    amount = dataz.amount * dataz.rate,
                    coaname = "-",
                    account = "-",
                    debit = 0,
                    credit = 0,
                });
            }
            ///
            foreach (GarmentFinanceDetailExportSalesJournalViewModel x in data1.OrderBy(x => x.invoiceno))
            {
                var debit = new GarmentFinanceDetailExportSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = x.currencycode,
                    rate = x.rate,
                    amount = 0,
                    coaname = "PIUTANG USAHA EKSPOR GARMENT",
                    account = "111300",
                    debit = x.amount,
                    credit = 0,
                };

                data.Add(debit);

                var credit1 = new GarmentFinanceDetailExportSalesJournalViewModel
                {
                    invoicetype = x.invoicetype,
                    invoiceno = x.invoiceno,
                    truckingdate = x.truckingdate,
                    buyer = x.buyer,
                    pebno = x.pebno,
                    currencycode = x.currencycode,
                    rate = x.rate,
                    amount = 0,
                    account = x.invoicetype == "DL" || x.invoicetype == "DS" ? "503120" : "503320",
                    coaname = x.invoicetype == "DL" || x.invoicetype == "DS" ? "       PENJ.BR.JADI EXPORT LANGSUNG" : "       PENJ.LAIN - LAIN EXPORT LANGS.",
                    debit = 0,
                    credit = x.amount,
                };

                data.Add(credit1);
            }

            var total = new GarmentFinanceDetailExportSalesJournalViewModel
            {
                invoicetype = "",
                invoiceno = "",
                truckingdate = DateTimeOffset.MinValue,
                buyer = "",
                pebno = "",
                currencycode = "",
                rate = 0,
                amount = 0,
                coaname = "",
                account = "J U M L A H",
                debit = data1.Sum(a => a.amount),
                credit = data1.Sum(a => a.amount),
            };
          
            data.Add(total);

            return data;
        }

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

        //public List<GarmentFinanceDetailLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        //{
        //    var Query = GetReportQuery(dateFrom, dateTo, offset);
        //    return Query.ToList();
        //}

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = GetReportQuery(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO AKUN ", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO PEB", DataType = typeof(string) });
            
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KURS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });
            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;
                    sheet.Column(6).Width = 15;

                    sheet.Column(7).Width = 15;
                    sheet.Column(8).Width = 15;
                    sheet.Column(9).Width = 20;
                    sheet.Column(10).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT AMBASSADOR GARMINDO";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN EXPORT";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;
             
                    result.Rows.Add( d.invoiceno, d.truckingdate, d.account, d.coaname, d.buyer, d.pebno, d.currencycode, d.rate, d.debit, d.credit);
                }

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);


                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;
                    sheet.Column(6).Width = 15;

                    sheet.Column(9).Width = 15;
                    sheet.Column(10).Width = 15;
                    sheet.Column(11).Width = 20;
                    sheet.Column(12).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT AMBASSADOR GARMINDO";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN EXPORT";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }
        
    }

    //public class BaseResponse<T>
    //{
    //    public string apiVersion { get; set; }
    //    public int statusCode { get; set; }
    //    public string message { get; set; }
    //    public T data { get; set; }

    //    public static implicit operator BaseResponse<T>(BaseResponse<string> v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
   
}
