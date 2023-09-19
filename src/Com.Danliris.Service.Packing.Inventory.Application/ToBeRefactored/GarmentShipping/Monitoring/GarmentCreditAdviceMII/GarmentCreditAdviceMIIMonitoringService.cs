using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdviceMII;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice4MII
{
    public class GarmentCreditAdviceMIIMonitoringService : IGarmentCreditAdviceMIIMonitoringService
    {
        private readonly IGarmentShippingCreditAdviceRepository carepository;
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentCreditAdviceMIIMonitoringService(IServiceProvider serviceProvider)
        {
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }

       public List<GarmentCreditAdviceMIIMonitoringViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryCA = carepository.ReadAll();
            var queryInv = repository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryCA = queryCA.Where(w => w.PaymentDate.AddHours(offset).Date >= DateFrom.Date && w.PaymentDate.AddHours(offset).Date <= DateTo.Date);

            //queryCA = queryCA.OrderBy(w => w.InvoiceNo);
            List<GarmentCreditAdviceMIIMonitoringViewModel> data = new List<GarmentCreditAdviceMIIMonitoringViewModel>();
            List<GarmentCreditAdviceMIIMonitoringViewModel> dataca = new List<GarmentCreditAdviceMIIMonitoringViewModel>();

            var Query = (from a in queryCA 
                         join b in queryInv on a.InvoiceId equals b.Id
                         where b.PEBNo != null && b.PEBNo != "-" && b.PEBNo != " " && b.PEBDate != DateTimeOffset.MinValue
                         select new GarmentCreditAdviceMIIMonitoringViewModel
                         {
                            CADate = a.PaymentDate,
                            PEBDate = b.PEBDate.AddHours(offset).Date,
                            InvoiceNo = a.InvoiceNo,                          
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            BankName = a.BankAccountName,
                            AccountBankNo = a.BankAccountNo.Replace(".", ""),
                            ReceiptNo = a.ReceiptNo == null ? "-" : a.ReceiptNo,  
                            Amount = Convert.ToDecimal(a.NettNego),
                            DHLCharges = Convert.ToDecimal(a.DHLCharges),
                            BankComission = Convert.ToDecimal(a.BankComission),
                            CreditInterest = Convert.ToDecimal(a.CreditInterest),
                            BankCharges = Convert.ToDecimal(a.BankCharges),
                            OtherCharges = Convert.ToDecimal(a.OtherCharge),
                            DiscrepancyFee = Convert.ToDecimal(a.DiscrepancyFee),
                            CurrencyCode = "USD",
                            Rate = 0,                            
                            AmountIDR = 0,                            
                        }).OrderBy(x=> x.CADate).ThenBy(x=> x.InvoiceNo);

            var currencyFilters = Query
                         //.GroupBy(o => new { o.PEBDate, o.CurrencyCode })
                         .Select(o => new CurrencyFilter { date = o.PEBDate.Date, code = o.CurrencyCode }).Distinct()
                         .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var dataz in Query)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == dataz.CurrencyCode && q.date.Date == dataz.PEBDate.AddHours(offset).Date).Select(s => s.rate).LastOrDefault());

                dataz.Rate = rate;
                dataz.AmountIDR = rate * dataz.Amount;
               
                data.Add(dataz);
            }

            //

            foreach (GarmentCreditAdviceMIIMonitoringViewModel x in data.OrderBy(x => x.InvoiceNo))
            {
           
                            //CreditInterest = Convert.ToDecimal(a.CreditInterest),
                            //BankCharges = Convert.ToDecimal(a.BankCharges),
                            //OtherCharges = Convert.ToDecimal(a.OtherCharge),
                            //DiscrepancyFee = Convert.ToDecimal(a.DiscrepancyFee),
                
                var NetNego = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName =x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = x.InvoiceNo,
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.Amount,
                    AmountIDR = x.Amount * x.Rate,
                };

                dataca.Add(NetNego);

                var DHL = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "DHL CHARGES",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.DHLCharges,
                    AmountIDR = x.DHLCharges * x.Rate,
                };

                if (DHL.Amount > 0)
                {
                    dataca.Add(DHL);
                }

                var BankCom = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "BANK COMISSION",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.BankComission,
                    AmountIDR = x.BankComission * x.Rate,
                };

                if (BankCom.Amount > 0)
                {
                    dataca.Add(BankCom);
                }

                var Credit = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "CREDIT INTEREST",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.CreditInterest,
                    AmountIDR = x.CreditInterest * x.Rate,
                };

                if (Credit.Amount > 0)
                {
                    dataca.Add(Credit);
                }

                var BCharges = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "BANK CHARGES",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.BankCharges,
                    AmountIDR = x.BankCharges * x.Rate,
                };

                if (BCharges.Amount > 0)
                {
                    dataca.Add(BCharges);
                }

                var OCharges = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "OTHER CHARGES",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.OtherCharges,
                    AmountIDR = x.OtherCharges * x.Rate,
                };

                if (OCharges.Amount > 0)
                {
                    dataca.Add(OCharges);
                }

                var DiscFee = new GarmentCreditAdviceMIIMonitoringViewModel
                {
                    CADate = x.CADate,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    ReceiptNo = x.ReceiptNo,
                    BankName = x.BankName,
                    AccountBankNo = x.AccountBankNo,
                    InvoiceNo = "DISCREPANCY FEE",
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.DiscrepancyFee,
                    AmountIDR = x.DiscrepancyFee * x.Rate,
                };

                if (DiscFee.Amount > 0)
                {
                    dataca.Add(DiscFee);
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

        public ListResult<GarmentCreditAdviceMIIMonitoringViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentCreditAdviceMIIMonitoringViewModel>(data, 1, total, total);
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
   
                    result.Rows.Add(CATgl, d.BuyerCode, d.BuyerName, d.ReceiptNo, d.BankName, d.AccountBankNo, d.InvoiceNo, d.CurrencyCode, d.Rate, d.Amount, d.AmountIDR);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
