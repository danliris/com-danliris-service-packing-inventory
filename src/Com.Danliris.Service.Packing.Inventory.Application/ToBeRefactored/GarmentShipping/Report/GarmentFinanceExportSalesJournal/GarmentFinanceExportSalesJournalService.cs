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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceExportSalesJournalService : IGarmentFinanceExportSalesJournalService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;

        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentFinanceExportSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }
        private GarmentCurrency GetCurrencyPEBDate(string stringDate)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var currencyUri = ApplicationSetting.CoreEndpoint + $"master/garment-detail-currencies/sales-debtor-currencies-peb?stringDate={stringDate}";
            var currencyResponse = httpClient.GetAsync(currencyUri).Result.Content.ReadAsStringAsync(); 

            var currencyResult = new BaseResponse<GarmentCurrency>()
            {
                data = new GarmentCurrency()
            };
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(currencyResponse.Result);
            var json = result.Single(p => p.Key.Equals("data")).Value;
            var data = JsonConvert.DeserializeObject<GarmentCurrency>(json.ToString());

            return data;
        }
        public List<GarmentFinanceExportSalesJournalViewModel> GetReportQuery(int month, int year, int offset)
        {

            DateTime dateFrom = new DateTime(year, month, 1);
            int nextYear = month == 12 ? year + 1 : year;
            int nextMonth = month == 12 ? 1 : month + 1;
            DateTime dateTo = new DateTime(nextYear, nextMonth, 1);
            List<GarmentFinanceExportSalesJournalViewModel> data = new List<GarmentFinanceExportSalesJournalViewModel>();
            
            var queryInv = repository.ReadAll();
            
            var queryPL = plrepository.ReadAll()
                .Where(w => w.TruckingDate.AddHours(offset).Date >= dateFrom && w.TruckingDate.AddHours(offset).Date < dateTo.Date
                    && (w.InvoiceType == "DL" || w.InvoiceType == "DS" || w.InvoiceType == "DLR" || w.InvoiceType == "SMR"));

            var joinQuery = from a in queryInv
                            join b in queryPL on a.PackingListId equals b.Id
                            where a.IsDeleted == false && b.IsDeleted == false
                            select new dataQuery
                            {
                                InvoiceType= b.InvoiceType,
                                TotalAmount=a.TotalAmount,
                                PEBDate=a.PEBDate
                            };

            List<dataQuery> dataQuery = new List<dataQuery>();

            foreach (var invoice in joinQuery.ToList())
            {
                GarmentCurrency currency = GetCurrencyPEBDate(invoice.PEBDate.Date.ToShortDateString());
                var rate = currency != null ? Convert.ToDecimal(currency.rate) : 0;
                invoice.Rate = rate;
                dataQuery.Add(invoice);
            }

            var join = from a in dataQuery
                       select new GarmentFinanceExportSalesJournalViewModel
                       {
                           remark= a.InvoiceType== "DL" || a.InvoiceType == "DS" ? "       PNJ. BR. JADI EXPORT LANGSUNG" : "       PNJ. LAIN-LAIN EXPORT LANGSUNG",
                           credit= a.TotalAmount * a.Rate,
                           debit= 0,
                           account= a.InvoiceType == "DL" || a.InvoiceType == "DS" ? "5024.00.4.00" : "5026.00.4.00"
                       };

            var debit = new GarmentFinanceExportSalesJournalViewModel
            {
                remark = "PIUTANG USAHA EXPORT",
                credit = 0,
                debit = join.Sum(a => a.credit),
                account = "1103.00.5.00"
            };
            data.Add(debit);

            var sumquery = join.ToList()
                   .GroupBy(x => new { x.remark, x.account }, (key, group) => new
                   {
                       Remark = key.remark,
                       Account = key.account,
                       Credit = group.Sum(s => s.credit)
                   }).OrderBy(a=>a.Remark);
            foreach(var item in sumquery)
            {
                var obj = new GarmentFinanceExportSalesJournalViewModel
                {
                    remark = item.Remark,
                    credit = item.Credit,
                    debit = 0,
                    account = item.Account
                };

                data.Add(obj);
            }

            var total= new GarmentFinanceExportSalesJournalViewModel
            {
                remark = "",
                credit = join.Sum(a => a.credit),
                debit = join.Sum(a => a.credit),
                account = ""
            };
            data.Add(total);
            return data;
        }

        public List<GarmentFinanceExportSalesJournalViewModel> GetReportData(int month, int year, int offset)
        {
            var Query =  GetReportQuery(month, year, offset);
            return Query.ToList();
        }


        public MemoryStream GenerateExcel(int month, int year, int offset)
        {
            var Query = GetReportQuery(month, year, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "AKUN DAN KETERANGAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "");
            else
            {
                foreach (var d in Query)
                {

                    string Credit = d.credit > 0 ? string.Format("{0:N2}", d.credit) : "";
                    string Debit = d.debit > 0 ? string.Format("{0:N2}", d.debit) : "";

                    result.Rows.Add(d.remark, d.account, Debit, Credit);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }

    public class BaseResponse<T>
    {
        public string apiVersion { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public static implicit operator BaseResponse<T>(BaseResponse<string> v)
        {
            throw new NotImplementedException();
        }
    }

    public class dataQuery
    {
        public string InvoiceType { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Rate { get; set; }

    }
}
