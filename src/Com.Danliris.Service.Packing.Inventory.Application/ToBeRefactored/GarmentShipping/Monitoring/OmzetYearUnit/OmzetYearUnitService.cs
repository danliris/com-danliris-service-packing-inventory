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

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "B U Y E R", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY - PCS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY - SETS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "%", DataType = typeof(double) });

            //if (data.Items.Count() == 0)
            //{
            //    dt.Rows.Add(null, null, null, null, null, null);
            //}
            //else
            //{
            //    int i = 0;
            //    foreach (var d in data.Items)
            //    {
            //        dt.Rows.Add(++i, d.unit, d.pcsQuantity, d.setsQuantity, d.amount, d.percentage);
            //    }
            //}

            //dt.Rows.Add(null, "J U M L A H", null, null, data.totalAmount, 100);

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerUnit") }, false);
            var filename = $"Report Omzet Per Unit {year}.xlsx";

            return new ExcelResult(excel, filename);
        }
    }
}
