using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearUnit
{
    public class OmzetYearUnitService : IOmzetYearUnitService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IGarmentPackingListRepository packingListRepository;
        private readonly IIdentityProvider _identityProvider;

        public static readonly string[] MONTH_NAMES = { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI", "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

        public OmzetYearUnitService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearUnitViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var invoiceQuery = shippingInvoiceRepository.ReadAll();

            var packingListQuery = packingListRepository.ReadAll()
                .Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);

            var joinedData = invoiceQuery.Join(packingListQuery, i => i.PackingListId, p => p.Id, (invoice, packingList) => new JoinedData
            {
                month = packingList.TruckingDate,
                items = invoice.Items.Select(i => new JoinedDataItem
                {
                    unit = i.UnitId,
                    amount = i.Amount
                }).ToList()
            }).ToList();

            var filterUnit = new Dictionary<string, string>()
            {
                { "(" + string.Join(" || ", joinedData.SelectMany(s => s.items.Select(i => "Id==\"" + i.unit + "\"")).Distinct().OrderBy(o => o).ToHashSet()) + ")", "true" },
            };

            var masterUnits = GetUnits(filterUnit).Result;

            var selectedData = joinedData.SelectMany(s => s.items.Select(i => new SelectedData
            {
                month = MONTH_NAMES[s.month.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Month - 1],
                unit = masterUnits.Where(w => w.Id == i.unit).Select(a => a.Name).FirstOrDefault() ?? "-",
                amount = i.amount
            })).ToList();

            var units = selectedData.Select(s => s.unit).Distinct().OrderBy(o => o).ToHashSet();

            List<OmzetYearUnitTableViewModel> tables = new List<OmzetYearUnitTableViewModel>();
            foreach (var month in MONTH_NAMES)
            {
                OmzetYearUnitTableViewModel header = new OmzetYearUnitTableViewModel
                {
                    month = month,
                    items = new Dictionary<string, decimal>()
                };

                foreach (var unit in units)
                {
                    header.items[unit] = selectedData.Where(w => w.month == month && w.unit == unit).Sum(s => s.amount);
                }

                tables.Add(header);
            }

            Dictionary<string, decimal> totals = new Dictionary<string, decimal>();
            Dictionary<string, decimal> averages = new Dictionary<string, decimal>();
            foreach (var unit in units)
            {
                var total = selectedData.Where(w => w.unit == unit).Sum(s => s.amount);
                totals[unit] = total;
                averages[unit] = Math.Round(total / 12, 2, MidpointRounding.AwayFromZero);
            }

            var result = new OmzetYearUnitViewModel
            {
                units = units,
                tables = tables,
                totals = totals,
                averages = averages
            };

            return result;
        }

        private async Task<List<Unit>> GetUnits(object filter)
        {
            string uri = "master/garment-units";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.GetAsync($"{APIEndpoint.Core}{uri}?filter={JsonConvert.SerializeObject(filter)}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<Unit> viewModel = JsonConvert.DeserializeObject<List<Unit>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new List<Unit>();
            }
        }

        public OmzetYearUnitViewModel GetReportData(int year)
        {
            var data = GetData(year);

            return data;
        }

        public ExcelResult GenerateExcel(int year)
        {
            var data = GetData(year);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "BULAN", DataType = typeof(string) });
            foreach (var unit in data.units)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = unit, DataType = typeof(double) });
            }

            if (data.tables.Count() == 0)
            {
                var values = new List<object> { null };
                foreach (var unit in data.units)
                {
                    values.Add(null);
                }

                dt.Rows.Add(values.ToArray());
            }
            else
            {
                foreach (var d in data.tables)
                {
                    var values = new List<object> { d.month };
                    foreach (var unit in data.units)
                    {
                        values.Add(d.items[unit]);
                    }

                    dt.Rows.Add(values.ToArray());
                }
            }

            var totalValues = new List<object> { "J U M L A H" };
            foreach (var unit in data.units)
            {
                totalValues.Add(data.totals[unit]);
            }
            dt.Rows.Add(totalValues.ToArray());

            var averageValues = new List<object> { "AVERAGE" };
            foreach (var unit in data.units)
            {
                averageValues.Add(data.averages[unit]);
            }
            dt.Rows.Add(averageValues.ToArray());

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerUnit") }, false);
            var filename = $"Report Omzet Per Unit {year}.xlsx";

            return new ExcelResult(excel, filename);
        }
    }
}
