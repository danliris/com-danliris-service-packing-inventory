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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearSection
{
    public class OmzetYearSectionService : IOmzetYearSectionService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IGarmentPackingListRepository packingListRepository;
        private readonly IIdentityProvider _identityProvider;

        public static readonly string[] MONTH_NAMES = { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI", "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

        public OmzetYearSectionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearSectionViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var invoiceQuery = shippingInvoiceRepository.ReadAll();

            var packingListQuery = packingListRepository.ReadAll()
                .Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);

            var joinedData = invoiceQuery.Join(packingListQuery, i => i.PackingListId, p => p.Id, (invoice, packingList) => new JoinedData
            {
                month = packingList.TruckingDate,
                sectionId = invoice.SectionId,
                section = invoice.SectionCode,
                items = invoice.Items.Select(i => new JoinedDataItem
                {
                    amount = i.Amount
                }).ToList()
            }).ToList();

            var filterSection = new Dictionary<string, string>()
            {
                { "(" + string.Join(" || ", joinedData.Select(s => "Id==\"" + s.sectionId + "\"").Distinct().OrderBy(o => o).ToHashSet()) + ")", "true" },
            };

            var masterSections = GetSections(filterSection).Result;

            var selectedData = joinedData.SelectMany(s => s.items.Select(i => new SelectedData
            {
                month = MONTH_NAMES[s.month.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Month - 1],
                section = s.section + ". " + masterSections.Where(w => w.Id == s.sectionId).Select(a => a.Name).FirstOrDefault(),
                amount = i.amount
            })).ToList();

            var sections = selectedData.Select(s => s.section).Distinct().OrderBy(o => o).ToHashSet();

            List<OmzetYearSectionTableViewModel> tables = new List<OmzetYearSectionTableViewModel>();
            foreach (var month in MONTH_NAMES)
            {
                OmzetYearSectionTableViewModel table = new OmzetYearSectionTableViewModel
                {
                    month = month,
                    items = new Dictionary<string, decimal>()
                };

                foreach (var Section in sections)
                {
                    table.items[Section] = selectedData.Where(w => w.month == month && w.section == Section).Sum(s => s.amount);
                }

                tables.Add(table);
            }

            Dictionary<string, decimal> totals = new Dictionary<string, decimal>();
            Dictionary<string, decimal> averages = new Dictionary<string, decimal>();
            foreach (var Section in sections)
            {
                var total = selectedData.Where(w => w.section == Section).Sum(s => s.amount);
                totals[Section] = total;
                averages[Section] = Math.Round(total / 12, 2, MidpointRounding.AwayFromZero);
            }

            var result = new OmzetYearSectionViewModel
            {
                sections = sections,
                tables = tables,
                totals = totals,
                averages = averages
            };

            return result;
        }

        private async Task<List<Section>> GetSections(object filter)
        {
            string uri = "master/garment-sections";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = await httpClient.GetAsync($"{APIEndpoint.Core}{uri}?filter={JsonConvert.SerializeObject(filter)}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                List<Section> viewModel = JsonConvert.DeserializeObject<List<Section>>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return new List<Section>();
            }
        }

        public OmzetYearSectionViewModel GetReportData(int year)
        {
            var data = GetData(year);

            return data;
        }

        public ExcelResult GenerateExcel(int year)
        {
            var data = GetData(year);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "BULAN", DataType = typeof(string) });
            foreach (var section in data.sections)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = section, DataType = typeof(double) });
            }

            if (data.tables.Count() == 0)
            {
                var values = new List<object> { null };
                foreach (var section in data.sections)
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
                    foreach (var section in data.sections)
                    {
                        values.Add(d.items[section]);
                    }

                    dt.Rows.Add(values.ToArray());
                }
            }

            var totalValues = new List<object> { "J U M L A H" };
            foreach (var section in data.sections)
            {
                totalValues.Add(data.totals[section]);
            }
            dt.Rows.Add(totalValues.ToArray());

            var averageValues = new List<object> { "AVERAGE" };
            foreach (var section in data.sections)
            {
                averageValues.Add(data.averages[section]);
            }
            dt.Rows.Add(averageValues.ToArray());

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerSection") }, false);
            var filename = $"Report Omzet Per Section {year}.xlsx";

            return new ExcelResult(excel, filename);
        }
    }
}
