using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote
{
    public class GarmentDebitNoteMonitoringService : IGarmentDebitNoteMonitoringService
    {
        private readonly IGarmentShippingNoteRepository repository;
        private readonly IGarmentShippingNoteItemRepository itemrepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityProvider _identityProvider;

        public GarmentDebitNoteMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }

       public IQueryable<GarmentDebitNoteMonitoringViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryitem = itemrepository.ReadAll();
       
            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                query = query.Where(w => w.BuyerCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.BuyerCode).ThenBy(b => b.NoteNo);
            
            var newQ = (from a in query
                        join b in queryitem on a.Id equals b.ShippingNoteId
                        where a.NoteType == GarmentShippingNoteTypeEnum.DN

                        select new GarmentDebitNoteMonitoringViewModel
                        {
                            DNNo = a.NoteNo,
                            DNDate = a.Date,
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            Description = b.Description,
                            CurrencyCode = b.CurrencyCode,
                            Amount = b.Amount,
                          });
            return newQ;
        }

        public List<GarmentDebitNoteMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.BuyerCode).ThenBy(b => b.DNNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
       {

            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Nota Debit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Nota Debit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "K e t e r a n g a n", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mata Uang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "J u m l a h", DataType = typeof(string) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string NoteDate = d.DNDate == new DateTime(1970, 1, 1) ? "-" : d.DNDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                 
                    string Amnt = string.Format("{0:N2}", d.Amount);
                       
                    result.Rows.Add(index, d.DNNo, NoteDate, d.BuyerCode, d.BuyerName, d.Description, d.CurrencyCode, Amnt);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
        // MII
        public List<GarmentDebitNoteMIIMonitoringViewModel> GetDataMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
      
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            //queryCA = queryCA.OrderBy(w => w.InvoiceNo);
            List<GarmentDebitNoteMIIMonitoringViewModel> datadn = new List<GarmentDebitNoteMIIMonitoringViewModel>();

            var Query = (from a in query                       
                         where a.NoteType == GarmentShippingNoteTypeEnum.DN

                         select new GarmentDebitNoteMIIMonitoringViewModel
                         {
                             DNDate = a.Date,
                             DNDate1 = a.Date.AddHours(7).Date,
                             InvoiceNo = "-",
                             BuyerCode = a.BuyerCode,
                             BuyerName = a.BuyerName,
                             BankName = a.BankName,
                             AccountBankNo = a.BankAccountNo.Replace(".",""),
                             ReceiptNo = a.ReceiptNo == null ? "-" : a.ReceiptNo,
                             Amount = Convert.ToDecimal(a.TotalAmount),
                             CurrencyCode = a.BankCurrencyCode,
                             Rate = a.BankCurrencyCode == "IDR" ? 1 : 0,
                             AmountIDR = a.BankCurrencyCode == "IDR" ? Convert.ToDecimal(a.TotalAmount) : 0,
                         });
            ///

            var currencyFilters = Query
                         .GroupBy(o => new { o.DNDate1, o.CurrencyCode })
                         .Select(o => new CurrencyFilter { date = o.Key.DNDate1.Date, code = o.Key.CurrencyCode })
                         .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var data in Query)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date.Date == data.DNDate1.Date).Select(s => s.rate).FirstOrDefault());
                //rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date == data.CADate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Date).Select(s => s.rate).LastOrDefault());

                if (data.CurrencyCode == "USD")
                {
                    data.Rate = rate;
                    data.AmountIDR = rate * data.Amount;
                    datadn.Add(data);
                }
            }
            //

            return datadn;
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

        public ListResult<GarmentDebitNoteMIIMonitoringViewModel> GetReportDataMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetDataMII(dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentDebitNoteMIIMonitoringViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcelMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataMII(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KD BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO KWITANSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK DEVISA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO REK BANK", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "INVOICE NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "CURRENCY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RATE", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR", DataType = typeof(decimal) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", 0, 0, 0);
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string DNTgl = d.DNDate == new DateTime(1970, 1, 1) ? "-" : d.DNDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    result.Rows.Add(DNTgl, d.BuyerCode, d.BuyerName, d.ReceiptNo, d.BankName, d.AccountBankNo, d.InvoiceNo, d.CurrencyCode, d.Rate, d.Amount, d.AmountIDR);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

    }
}
