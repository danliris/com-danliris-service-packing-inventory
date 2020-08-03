using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyer
{
    public class OmzetYearBuyerService : IOmzetYearBuyerService
    {
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IIdentityProvider _identityProvider;

        public OmzetYearBuyerService(IServiceProvider serviceProvider)
        {
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearBuyerViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var query = shippingInvoiceRepository.ReadAll()
                .Where(w => w.InvoiceDate >= dateFrom && w.InvoiceDate < dateTo);

            var groupedQuery = from q in query
                               group q by q.BuyerAgentCode into g
                               select new OmzetYearBuyerItemViewModel
                               {
                                   buyer = g.Key,
                                   pcsQuantity = g.SelectMany(s => s.Items.Where(i => i.UomUnit == "PCS")).Sum(i => i.Quantity),
                                   setsQuantity = g.SelectMany(s => s.Items.Where(i => i.UomUnit == "SETS")).Sum(i => i.Quantity),
                                   amount = g.SelectMany(s => s.Items).Sum(i => i.Amount)
                               };

            var data = groupedQuery.ToList();

            var result = new OmzetYearBuyerViewModel
            {
                totalAmount = data.Sum(s => s.amount),
                Items = data
            };

            foreach (var item in result.Items)
            {
                item.percentage = Math.Round(item.amount / result.totalAmount * 100, 2, MidpointRounding.AwayFromZero);
            }

            return result;
        }

        public OmzetYearBuyerViewModel GetReportData(int year)
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

            if (data.Items.Count() == 0)
            {
                dt.Rows.Add(null, null, null, null, null, null);
            }
            else
            {
                int i = 0;
                foreach (var d in data.Items)
                {
                    dt.Rows.Add(++i, d.buyer, d.pcsQuantity, d.setsQuantity, d.amount, d.percentage);
                }
            }

            dt.Rows.Add(null, "J U M L A H", null, null, data.totalAmount, 100);

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerBuyer") }, false);
            var filename = $"Report Omzet Per Buyer {year}.xlsx";

            return new ExcelResult(excel, filename);
        }
    }
}
