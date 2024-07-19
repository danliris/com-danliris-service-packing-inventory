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
using System.IO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearMarketing;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyerComodity
{
    public class OmzetYearMarketingService : IOmzetYearMarketingService
    {
        private readonly IGarmentShippingInvoiceRepository shippingInvoiceRepository;
        private readonly IGarmentShippingInvoiceItemRepository shippingInvoiceItemRepository;
        private readonly IGarmentPackingListRepository shippingpackinglistRepository;
        private readonly IIdentityProvider _identityProvider;

        public OmzetYearMarketingService(IServiceProvider serviceProvider)
        {
            shippingInvoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            shippingInvoiceItemRepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            shippingpackinglistRepository = serviceProvider.GetService<IGarmentPackingListRepository>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<OmzetYearMarketingViewModel> GetData(int year)
        {
            DateTimeOffset dateFrom = new DateTimeOffset(year - 1, 12, 31, 17, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));
            DateTimeOffset dateTo = new DateTimeOffset(year + 1, 1, 1, 0, 0, 0, new TimeSpan(_identityProvider.TimezoneOffset, 0, 0));

            var invoicequery = shippingInvoiceRepository.ReadAll();
            var invoiceitemquery = shippingInvoiceItemRepository.ReadAll();
            var packinglistquery = shippingpackinglistRepository.ReadAll();

            packinglistquery = packinglistquery.Where(w => w.TruckingDate >= dateFrom && w.TruckingDate < dateTo);
            
            packinglistquery = packinglistquery.Where(w => w.Omzet == true);
         
            var newQ = from a in packinglistquery
                       join b in invoicequery on a.Id equals b.PackingListId
                       join c in invoiceitemquery on b.Id equals c.GarmentShippingInvoiceId
                       where b.PEBNo != null && b.PEBNo != "-" && b.PEBNo != " "
                             && b.PEBDate != DateTimeOffset.MinValue

                       select new OmzetYearMarketingTempViewModel
                       {
                           marketingName = c.MarketingName,                         
                           quantity = c.Quantity,
                           uomunit = c.UomUnit,
                           amount = c.Amount,
                       };
            //        
            var Query = from data in newQ
                        group data by new { data.marketingName } into groupData
                        select new OmzetYearMarketingViewModel
                        {
                            marketingName = groupData.Key.marketingName,
                            pcsQuantity = groupData.Where(i =>  i.uomunit == "PCS").Sum(i => i.quantity),
                            setsQuantity = groupData.Where(i => i.uomunit == "SETS").Sum(i => i.quantity),
                            packsQuantity = groupData.Where(i => i.uomunit == "PACKS").Sum(i => i.quantity),
                            amount = Math.Round(groupData.Sum(s => s.amount), 2),
                        };
            return Query.AsQueryable();
        }
        
        public List<OmzetYearMarketingViewModel> GetReportData(int year)
        {
            var Query = GetData(year);
            Query = Query.OrderBy(b => b.marketingName);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(int year)
        {
            var Query = GetData(year);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "MARKETING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QTY - PCS", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "QTY - SETS", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "QTY - PACKs", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(double) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", 0, 0, 0, 0);
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;
                    result.Rows.Add(index, d.marketingName, d.pcsQuantity, d.setsQuantity, d.packsQuantity, d.amount);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
