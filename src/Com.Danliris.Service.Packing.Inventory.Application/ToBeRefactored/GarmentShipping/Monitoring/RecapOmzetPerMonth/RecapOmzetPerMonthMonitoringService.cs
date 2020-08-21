using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public class RecapOmzetPerMonthMonitoringService : IRecapOmzetPerMonthMonitoringService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentPackingListRepository packingListRepository;
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IIdentityProvider _identityProvider;

        public RecapOmzetPerMonthMonitoringService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private List<RecapOmzetPerMonthMonitoringViewModel> GetData(int month, int year)
        {
            var packingListQuery = packingListRepository.ReadAll();

            DateTimeOffset dateFrom = new DateTimeOffset(year, month, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(month == 12 ? year + 1 : year, month == 12 ? 1 : month + 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            packingListQuery = packingListQuery.Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);

            var joinedQuery = from pl in packingListQuery
                              join inv in shippingInvoiceRepository.ReadAll() on pl.Id equals inv.PackingListId
                              select new JoinedData
                              {
                                  packingListId = pl.Id,
                                  truckingDate = pl.TruckingDate,
                                  buyerAgentName = pl.BuyerAgentName,
                                  buyerAgentCode = pl.BuyerAgentCode,
                                  invoiceId = inv.Id,
                                  invoiceNo = inv.InvoiceNo,
                                  invoiceDate = inv.InvoiceDate,
                                  pebNo = inv.PEBNo,
                                  pebDate = inv.PEBDate,
                                  items = inv.Items.Select(i => new JoinedDataItem
                                  {
                                      comodity = i.ComodityName,
                                      quantity = i.Quantity,
                                      uom = i.UomUnit,
                                      amount = i.Amount,
                                      currency = i.CurrencyCode,
                                  })
                              };

            var orderedData = joinedQuery.OrderBy(o => o.truckingDate).ToList();

            foreach (var data in orderedData)
            {
                data.items = from od in data.items
                             group od by new { od.comodity, od.currency, od.uom } into groupedItem
                             select new JoinedDataItem
                             {
                                 comodity = groupedItem.Key.comodity,
                                 quantity = groupedItem.Sum(i => i.quantity),
                                 uom = groupedItem.Key.uom,
                                 amount = groupedItem.Sum(i => i.amount),
                                 currency = groupedItem.Key.currency,
                             };
            }

            var selectedData = orderedData.SelectMany(inv => inv.items.Select(i => new RecapOmzetPerMonthMonitoringViewModel
            {
                packingListId = inv.packingListId,
                truckingDate = inv.truckingDate,
                buyerAgentName = inv.buyerAgentName,
                buyerAgentCode = inv.buyerAgentCode,
                comodity = i.comodity,
                invoiceId = inv.invoiceId,
                invoiceNo = inv.invoiceNo,
                invoiceDate = inv.invoiceDate,
                pebNo = inv.pebNo,
                pebDate = inv.pebDate,
                quantity = i.quantity,
                uom = i.uom,
                amount = i.amount,
                currency = i.currency
            })).ToList();

            var currencyFilters = selectedData
                .GroupBy(o => new { o.truckingDate, o.currency })
                .Select(o => new CurrencyFilter { date = o.Key.truckingDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime, code = o.Key.currency })
                .ToList();

            var currencies = GetCurrecncies(currencyFilters).Result;

            foreach (var data in selectedData)
            {
                data.rate = currencies.Where(q => q.code == data.currency && q.date <= data.truckingDate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).DateTime).Select(s => s.rate).LastOrDefault();
                data.idrAmount = (decimal)data.rate * data.amount;
            }

            return selectedData;
        }

        public ListResult<RecapOmzetPerMonthMonitoringViewModel> GetReportData(int month, int year)
        {
            var data = GetData(month, year);
            var total = data.Count;

            return new ListResult<RecapOmzetPerMonthMonitoringViewModel>(data, 1, total, total);
        }

        public ExcelResult GenerateExcel(int month, int year)
        {
            var data = GetData(month, year);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "TGL TRUCKING", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NAMA / ALAMAT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "INDEX DEBTR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JENIS BARANG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "INVOICE No.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "INVOICE Tgl.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "PEB No.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "PEB Tgl.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "RATE", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JML. IDR", DataType = typeof(double) });

            if (data.Count() == 0)
            {
                dt.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null);
            }
            else
            {
                foreach (var d in data)
                {
                    dt.Rows.Add(d.invoiceNo, DateTimeToString(d.truckingDate), d.buyerAgentName, d.buyerAgentCode, d.comodity, d.invoiceNo, DateTimeToString(d.invoiceDate), d.pebNo, DateTimeToString(d.pebDate), d.quantity + " " + d.uom, d.currency + " " + d.amount, d.rate, d.idrAmount);
                }
            }

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Monitoring Rekap Omzet Bulan") }, true);
            var filename = $"Monitoring Rekap Omzet Bulan {new DateTime(year, month, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}.xlsx";

            return new ExcelResult(excel, filename);
        }

        async Task<List<GarmentCurrency>> GetCurrecncies(List<CurrencyFilter> filters)
        {
            string uri = "master/garment-currencies/by-code-before-date";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.SendAsync(HttpMethod.Get, $"{APIEndpoint.Core}{uri}", new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));
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

        private string DateTimeToString(DateTimeOffset dateTime)
        {
            return dateTime.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy");
        }
    }
}
