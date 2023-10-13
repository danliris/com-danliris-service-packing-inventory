using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdviceMII;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice4MII
{
    public class GarmentShippingNoteCreditAdviceMIIMonitoringService : IGarmentShippingNoteCreditAdviceMIIMonitoringService
    {
        private readonly IGarmentShippingNoteCreditAdviceRepository carepository;
        private readonly IGarmentShippingNoteRepository repository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentShippingNoteCreditAdviceMIIMonitoringService(IServiceProvider serviceProvider)
        {
            carepository = serviceProvider.GetService<IGarmentShippingNoteCreditAdviceRepository>();
            repository = serviceProvider.GetService<IGarmentShippingNoteRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }

       public List<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryCA = carepository.ReadAll();
            var querySN = repository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryCA = queryCA.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            //queryCA = queryCA.OrderBy(w => w.InvoiceNo);
            List<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel> data = new List<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel>();
            List<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel> dataca = new List<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel>();

            var Query = (from a in queryCA 
                         join b in querySN on a.ShippingNoteId equals b.Id
                         select new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                         {
                            CADate = a.PaymentDate,
                            NoteNo = a.NoteNo,                          
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            BankName = a.BankAccountName,
                            AccountBankNo = a.BankAccountNo.Replace(".", ""),
                            ReceiptNo = a.ReceiptNo == null ? "-" : a.ReceiptNo,  
                            Amount = Convert.ToDecimal(a.NettNego),
                            BankComission = Convert.ToDecimal(a.BankComission),
                            CreditInterest = Convert.ToDecimal(a.CreditInterest),
                            BankCharges = Convert.ToDecimal(a.BankCharges),
                            InsuranceCharges = Convert.ToDecimal(a.InsuranceCharge),                
                            CurrencyCode = "USD",
                            Rate = 0,                            
                            AmountIDR = 0,                            
                        }).OrderBy(x=> x.CADate).ThenBy(x=> x.NoteNo);

            var currencyFilters = Query
                         .Select(o => new CurrencyFilter { date = o.CADate.Date, code = o.CurrencyCode }).Distinct()
                         .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var dataz in Query)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == dataz.CurrencyCode && q.date.Date == dataz.CADate.Date).Select(s => s.rate).LastOrDefault());

                dataz.Rate = rate;
                dataz.AmountIDR = rate * dataz.Amount;
               
                data.Add(dataz);
            }

            //

            foreach (GarmentShippingNoteCreditAdviceMIIMonitoringViewModel x in data.OrderBy(x => x.NoteNo))
            {
                var NetNego = new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName =x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    NoteNo = x.NoteNo,
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.Amount,
                    AmountIDR = x.Amount * x.Rate,
                };

                dataca.Add(NetNego);

                var BankCom = new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    NoteNo = "BANK COMISSION",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.BankComission,
                    AmountIDR = x.BankComission * x.Rate,
                };

                if (BankCom.Amount > 0)
                {
                    dataca.Add(BankCom);
                }

                var Credit = new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    NoteNo = "CREDIT INTEREST",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.CreditInterest,
                    AmountIDR = x.CreditInterest * x.Rate,
                };

                if (Credit.Amount > 0)
                {
                    dataca.Add(Credit);
                }

                var BCharges = new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    NoteNo = "BANK CHARGES",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.BankCharges,
                    AmountIDR = x.BankCharges * x.Rate,
                };

                if (BCharges.Amount > 0)
                {
                    dataca.Add(BCharges);
                }

                var OCharges = new GarmentShippingNoteCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    NoteNo = "INSURANCE CHARGES",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.InsuranceCharges,
                    AmountIDR = x.InsuranceCharges * x.Rate,
                };

                if (OCharges.Amount > 0)
                {
                    dataca.Add(OCharges);
                }
            }

            return dataca;
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

        public ListResult<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentShippingNoteCreditAdviceMIIMonitoringViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KD BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO KWITANSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK DEVISA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO REK BANK", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KETERANGAN", DataType = typeof(string) });
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

                    string CATgl = d.CADate == new DateTime(1970, 1, 1) ? "-" : d.CADate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
   
                    result.Rows.Add(CATgl, d.BuyerCode, d.BuyerName, d.ReceiptNo, d.BankName, d.AccountBankNo, d.NoteNo, d.CurrencyCode, d.Rate, d.Amount, d.AmountIDR);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
