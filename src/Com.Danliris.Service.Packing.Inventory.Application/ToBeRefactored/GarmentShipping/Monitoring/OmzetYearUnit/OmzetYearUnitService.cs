using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearUnit
{
    public class OmzetYearUnitService : IOmzetYearUnitService
    {
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IIdentityProvider _identityProvider;

        public static readonly string[] MONTH_NAMES = { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI", "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

        public OmzetYearUnitService(IServiceProvider serviceProvider)
        {
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearUnitViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var query = shippingInvoiceRepository.ReadAll()
                .Where(w => w.InvoiceDate >= dateFrom && w.InvoiceDate < dateTo);

            var queriedData = query.Select(s => new QueriedData
            {
                month = s.InvoiceDate,
                items = s.Items.Select(i => new QueriedDataItem
                {
                    unit = i.UnitCode,
                    amount = i.Amount
                })
            }).ToList();

            var selectedData = queriedData.SelectMany(s => s.items.Select(i => new SelectedData
            {
                month = MONTH_NAMES[s.month.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Month - 1],
                unit = i.unit,
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
