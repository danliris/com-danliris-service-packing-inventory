using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearCountry
{
    public class OmzetYearCountryService : IOmzetYearCountryService
    {
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IGarmentPackingListRepository shippingpackinglistRepository;
        private readonly IIdentityProvider _identityProvider;

        public OmzetYearCountryService(IServiceProvider serviceProvider)
        {
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            shippingpackinglistRepository = serviceProvider.GetService<IGarmentPackingListRepository>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearCountryViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var invoicequery = shippingInvoiceRepository.ReadAll();

            var packinglistquery = shippingpackinglistRepository.ReadAll()
                                   .Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);

            var joinedData = invoicequery.Join(packinglistquery, i => i.PackingListId, p => p.Id, (invoice, packinglist) => new JoinedData
            {
                destination = packinglist.Destination,
                items = invoice.Items.Select(i => new JoinedDataItem
                {
                    quantity = i.Quantity,
                    uom = i.UomUnit,
                    amount = i.CMTPrice > 0 ? Convert.ToDecimal(i.Quantity) * i.CMTPrice : i.Amount
                }).ToList()
            }).ToList();

            var DetailData = joinedData.SelectMany(s => s.items.Select(i => new
            {
                countryname = s.destination,
                uomunit = i.uom,
                qty = i.quantity,
                amnt = i.amount,
            }));

            var GroupData = DetailData.GroupBy(g => g.countryname).Select(g => new OmzetYearCountryItemViewModel
            {
                country = g.Key,
                pcsQuantity = g.Where(i => i.uomunit == "PCS").Sum(i => i.qty),
                setsQuantity = g.Where(i => i.uomunit == "SETS").Sum(i => i.qty),
                amount = g.Sum(i => i.amnt)
            });

            var data = GroupData.ToList();

            var result = new OmzetYearCountryViewModel
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

        public OmzetYearCountryViewModel GetReportData(int year)
        {
            var data = GetData(year);

            return data;
        }

        public MemoryStreamResult GenerateExcel(int year)
        {
            var data = GetData(year);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "N E G A R A", DataType = typeof(string) });
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
                    dt.Rows.Add(++i, d.country, d.pcsQuantity, d.setsQuantity, d.amount, d.percentage);
                }
            }

            dt.Rows.Add(null, "J U M L A H", null, null, data.totalAmount, 100);

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerBuyer") }, false);
            var filename = $"Report Omzet Per Negara {year}.xlsx";

            return new MemoryStreamResult(excel, filename);
        }
    }
}
