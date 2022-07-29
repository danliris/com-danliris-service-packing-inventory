using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport
{
    public class GarmentCMTSalesService : IGarmentCMTSalesService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IServiceProvider _serviceProvider;

        private readonly IIdentityProvider _identityProvider;

        public GarmentCMTSalesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentCMTSalesViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var quaryInvItem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();
            var queryCA = carepository.ReadAll();



            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryInv = queryInv.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;


            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);


            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);


            var Query = (from a in queryInv
                        join b in queryPL on a.PackingListId equals b.Id
                        join c in queryCA on a.Id equals c.InvoiceId into dd
                        from CA in dd.DefaultIfEmpty()
                        join d in quaryInvItem on a.Id equals d.GarmentShippingInvoiceId
                        where a.IsDeleted == false && b.IsDeleted == false && CA.IsDeleted == false
                        && a.PEBDate != DateTimeOffset.MinValue && d.CMTPrice > 0

                        select new GarmentCMTSalesViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            Ronos = d.RONo,
                            InvoiceDate = a.InvoiceDate,
                            BuyerAgentName = a.BuyerAgentCode + " - " + a.BuyerAgentName,
                            PEBDate = a.PEBDate,
                            FOB = a.TotalAmount,
                            FAB = Convert.ToDecimal(d.Quantity) * (d.Price - d.CMTPrice),
                            ToBePaid = a.AmountToBePaid,
                            CurrencyCode = d.CurrencyCode
                        }).ToList();
                        
            var newQ = Query.GroupBy(s => new { s.InvoiceNo }).Select(d => new GarmentCMTSalesViewModel()
            {
                InvoiceNo = d.Key.InvoiceNo,
                Ronos = string.Join(",",d.Select(x=>x.Ronos)),
                InvoiceDate = d.FirstOrDefault().InvoiceDate,
                BuyerAgentName = d.FirstOrDefault().BuyerAgentName,

                PEBDate = d.FirstOrDefault().PEBDate,


                FOB = d.FirstOrDefault().FOB,

                FAB = d.Sum(x => x.FAB),
                ToBePaid = d.FirstOrDefault().ToBePaid,
                CurrencyCode = d.FirstOrDefault().CurrencyCode,
                Quantity = d.Sum(x => x.Quantity)
            }).ToList();


            var currencyFilters = newQ
                                    .GroupBy(o => new { o.PEBDate, o.CurrencyCode })
                                    .Select(o => new CurrencyFilter { date = o.Key.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime, code = o.Key.CurrencyCode })
                                    .ToList();

            var currencies = GetCurrencies(currencyFilters).Result;

            decimal rate;

            foreach (var data in newQ)
            {
                rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date == data.PEBDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime).Select(s => s.rate).LastOrDefault());
                //rate = 0;
                data.Rate = rate;
                data.FOBIdr = rate * data.FOB;
                //data.FAB = data.Quantity * data.CMTPrice;
                data.FABIdr = rate * data.FAB;
                data.ToBePaidIdr = rate * data.ToBePaid;
            }



            return newQ;
        }

        public ListResult<GarmentCMTSalesViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetData(buyerAgent, dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentCMTSalesViewModel>(data, 1, total, total);
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kurs", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "FOB USD", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "FOB IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LESS FAB USD", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LESS FAB IDR", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TO BE PAID USD", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TO BE PAID IDR", DataType = typeof(string) });

            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "REPORT REALISASI CMT PENJUALAN";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("Buyer : {0}", string.IsNullOrWhiteSpace(buyerAgent) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().BuyerAgentName));
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

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PEBDate = d.PEBDate == DateTimeOffset.MinValue ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string rate = string.Format("{0:N2}", d.Rate);
                    string fobUSD = string.Format("{0:N2}", d.FOB);
                    string fobIdr = string.Format("{0:N2}", d.FOBIdr);
                    string fabUSD = string.Format("{0:N2}", d.FAB);
                    string fabIdr = string.Format("{0:N2}", d.FABIdr);
                    string TBPaid = string.Format("{0:N2}", d.ToBePaid);
                    string TBPaidIdr = string.Format("{0:N2}", d.ToBePaidIdr);


                    result.Rows.Add(index, d.InvoiceNo, InvDate, d.BuyerAgentName, PEBDate, rate, fobUSD, fobIdr, fabUSD, fabIdr, TBPaid, TBPaidIdr);
                }

                string ifobUSD = string.Format("{0:N2}", Query.Sum(x => x.FOB));
                string ifobIdr = string.Format("{0:N2}", Query.Sum(x => x.FOBIdr));
                string ifabUSD = string.Format("{0:N2}", Query.Sum(x => x.FAB));
                string ifabIdr = string.Format("{0:N2}", Query.Sum(x => x.FABIdr));
                string iTBPaid = string.Format("{0:N2}", Query.Sum(x => x.ToBePaid));


                string iTBPaidIdr = string.Format("{0:N2}", Query.Sum(x => x.ToBePaidIdr));
                result.Rows.Add("", "", "", "", "", "TOTAL", ifobUSD, ifobIdr, ifabUSD, ifabIdr, iTBPaid, iTBPaidIdr);

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    #region KopTable
                    sheet.Cells[$"A1:L1"].Value = "REPORT REALISASI CMT PENJUALAN";
                    sheet.Cells[$"A1:L1"].Merge = true;
                    sheet.Cells[$"A1:L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:L1"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:L2"].Value = string.Format("Periode Tanggal : {0} s/d {1}", DateFrom.ToString("dd MMM yyyy", new CultureInfo("id-ID")), DateTo.ToString("dd MMM yyyy", new CultureInfo("id-ID")));
                    sheet.Cells[$"A2:L2"].Merge = true;
                    sheet.Cells[$"A2:L2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:L2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:L2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:L3"].Value = string.Format("Buyer : {0}", string.IsNullOrWhiteSpace(buyerAgent) ? "ALL" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().BuyerAgentName));
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
    }
}
