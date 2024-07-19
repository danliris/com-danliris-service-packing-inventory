using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
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
        private readonly IGarmentPackingListRepository packingListRepository;
        private readonly IIdentityProvider _identityProvider;

        public OmzetYearBuyerService(IServiceProvider serviceProvider)
        {
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private OmzetYearBuyerViewModel GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var invoiceQuery = shippingInvoiceRepository.ReadAll();

            invoiceQuery = invoiceQuery.Where(w => w.PEBDate != DateTimeOffset.MinValue);

            invoiceQuery = invoiceQuery.Where(w => w.PEBNo != null && w.PEBNo != "-" && w.PEBNo != " ");

            var packingListQuery = packingListRepository.ReadAll();

            packingListQuery = packingListQuery.Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);

            packingListQuery = packingListQuery.Where(w => w.Omzet == true);

            //packingListQuery = packingListQuery.Where(w => w.Omzet == true);
            //packingListQuery = packingListQuery.Where(w => w.Accounting == true);

            var joinedData = invoiceQuery.Join(packingListQuery, i => i.PackingListId, p => p.Id, (invoice, packingList) => new JoinedData
            {
                buyer = invoice.BuyerAgentCode + " - " + invoice.BuyerAgentName,
                items = invoice.Items.Select(i => new JoinedDataItem
                {
                    uom = i.UomUnit,
                    quantity = i.Quantity,
                    amount = i.Amount
                }).ToList()
            }).ToList();

            //var joinedData = (from packingList in packingListQuery
            //                  join invoice in invoiceQuery on packingList.Id equals invoice.PackingListId
            //                  select new JoinedData
            //                  {
            //                      buyer = invoice.BuyerAgentCode,
            //                      items = invoice.Items.Select(i => new JoinedDataItem
            //                      {
            //                          uom = i.UomUnit,
            //                          quantity = i.Quantity,
            //                          amount = i.Amount
            //                      }).ToList()
            //                  }).ToList();

            var groupedData = joinedData.GroupBy(g => g.buyer).Select(g => new OmzetYearBuyerItemViewModel
            {
                buyer = g.Key,
                pcsQuantity = g.SelectMany(s => s.items.Where(i => i.uom == "PCS")).Sum(i => i.quantity),
                setsQuantity = g.SelectMany(s => s.items.Where(i => i.uom == "SETS")).Sum(i => i.quantity),
                packsQuantity = g.SelectMany(s => s.items.Where(i => i.uom == "PACKS")).Sum(i => i.quantity),
                amount = g.SelectMany(s => s.items).Sum(i => i.amount)
            });

            var data = groupedData.ToList();

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

        public MemoryStreamResult GenerateExcel(int year)
        {
            var data = GetData(year);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "B U Y E R", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY - PCS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY - SETS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY - PACKS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "%", DataType = typeof(double) });

            if (data.Items.Count() == 0)
            {
                dt.Rows.Add(null, null, null, null, null, null, null); ;
            }
            else
            {
                int i = 0;
                foreach (var d in data.Items)
                {
                    dt.Rows.Add(++i, d.buyer, d.pcsQuantity, d.setsQuantity, d.packsQuantity, d.amount, d.percentage);
                }
            }

            dt.Rows.Add(null, "J U M L A H", null, null, null, data.totalAmount, 100);

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "OmzetPerBuyer") }, false);
            var filename = $"Report Omzet Per Buyer {year}.xlsx";

            return new MemoryStreamResult(excel, filename);
        }
    }
}
