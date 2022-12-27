using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList; 
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByMarketing
{
    public class GarmentOmzetMonthlyByMarketingService : IGarmentOmzetMonthlyByMarketingService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentOmzetMonthlyByMarketingService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentOmzetMonthlyByMarketingViewModel> GetData(string marketingName, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryitem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);
            
            if (!string.IsNullOrWhiteSpace(marketingName))
            {
                queryitem = queryitem.Where(w => w.MarketingName == marketingName);
            }
            queryPL = queryPL.Where(w => w.Omzet == true);           
            //queryPL = queryPL.Where(w => w.IsUsed == true);

            var newQ = (from a in queryPL
                        join b in query on a.Id equals b.PackingListId
                        join c in queryitem on b.Id equals c.GarmentShippingInvoiceId
                        where b.PEBNo != null && b.PEBNo != "-" && b.PEBNo != " "
                              && b.PEBDate != DateTimeOffset.MinValue
                              && c.MarketingName != null

                        select new GarmentOmzetMonthlyByMarketingListViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            MarketingName = c.MarketingName,
                            TruckingDate = a.TruckingDate,
                            BuyerName = a.BuyerAgentName,
                            RO_Number = c.RONo,
                            ComodityName = c.ComodityName,
                            ComodityDesc = c.ComodityDesc,
                            UnitCode = c.UnitCode,
                            UOMUnit = c.UomUnit,
                            Amount = c.Amount,
                            Quantity = c.Quantity,
                        }).OrderBy(o => o.MarketingName).ThenBy(o => o.BuyerName).ThenBy(o => o.TruckingDate).ThenBy(o => o.InvoiceNo);              

            var garmentomzets = newQ.ToList()
                .GroupBy(gmtomzt => {
                    var marketing = gmtomzt.MarketingName;
                    return marketing;
                })
                .Select(groupomzt => new GarmentOmzetMonthlyByMarketingViewModel
                {
                    MarketingName = groupomzt.Key,
                    Buyers = groupomzt
                        .GroupBy(sie => sie.BuyerName)
                        .Select(groupBuyer => new GarmentOmzetMonthlyByMarketingBuyerViewModel
                        {
                            Buyer = groupBuyer.Key,
                            Quantities = groupBuyer.Sum(s => s.Quantity),
                            Amounts = groupBuyer.Sum(s => s.Amount),
                            Details = groupBuyer
                                .Select(detail => GetGarmentOmzet(detail))
                                .OrderBy(o => o.TruckingDate)
                                .ToList()
                        })
                        .OrderBy(o => o.Buyer)
                        .ToList()
                })
                .ToList();

            return garmentomzets.AsQueryable();
        }

        GarmentOmzetMonthlyByMarketingDetailViewModel GetGarmentOmzet(GarmentOmzetMonthlyByMarketingListViewModel go)
        {
            return new GarmentOmzetMonthlyByMarketingDetailViewModel
            {
                InvoiceNo = go.InvoiceNo,
                UnitCode = go.UnitCode,
                TruckingDate = go.TruckingDate,
                RO_Number = go.RO_Number,
                ComodityName = go.ComodityName,
                ComodityDesc = go.ComodityDesc,
                UOMUnit = go.UOMUnit,
                Quantity = go.Quantity,
                Amount = go.Amount,
            };
        }

        public List<GarmentOmzetMonthlyByMarketingViewModel> GetReportData(string marketingName, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(marketingName, dateFrom, dateTo, offset);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string marketingName, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(marketingName, dateFrom, dateTo, offset);
            var data = Query.ToList();
            DataTable result = new DataTable();
           
            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "MARKETING", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "U N I T", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "T R U C K I N G", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "R/O", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "I  T  E  M", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "STYLE ORD / ART NO.", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "QUANTITY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(string) });

            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            int rowPosition = 2;
            if (data != null && data.Count > 0)
            {
                var grandTotalByUom = new List<TotalByUom>();

                foreach (var d in data)
                {
                    var ucFirstMergedRowPosition = rowPosition;
                    var ucLastMergedRowPosition = rowPosition;

                    foreach (var buyer in d.Buyers)
                    {
                        var buyerFirstMergedRowPosition = rowPosition;
                        var buyerLastMergedRowPosition = rowPosition;
                        var index = 0;
                        foreach (var detail in buyer.Details)
                        {
                            index++;
                            result.Rows.Add(index, d.MarketingName, buyer.Buyer, detail.UnitCode, detail.InvoiceNo, detail.TruckingDate.ToString("MM/dd/yyyy", new CultureInfo("us-US")), detail.RO_Number, detail.ComodityName, detail.ComodityDesc, detail.Quantity, detail.UOMUnit, detail.Amount);

                            buyerLastMergedRowPosition = rowPosition++;

                            var currentUom = grandTotalByUom.FirstOrDefault(c => c.uom == detail.UOMUnit);
                            if (currentUom == null)
                            {
                                grandTotalByUom.Add(new TotalByUom
                                {
                                    uom = detail.UOMUnit,
                                    quantity = detail.Quantity,
                                    amount = detail.Amount
                                });
                            }
                            else
                            {
                                currentUom.quantity += detail.Quantity;
                                currentUom.amount += detail.Amount;
                            }
                        }

                        result.Rows.Add(null, "SUB TOTAL", null, null, null, null, null, null, null, buyer.Quantities, null, buyer.Amounts);

                        mergeCells.Add(($"B{rowPosition}:G{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));

                        ucLastMergedRowPosition = rowPosition++;
                    }

                    if (ucFirstMergedRowPosition != ucLastMergedRowPosition)
                    {
                        mergeCells.Add(($"A{ucFirstMergedRowPosition}:A{ucLastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top));
                    }
                }

                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null);
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null);

                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                        result.Rows.Add(null, null, "GRAND TOTAL", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, grandTotalByUom[i].amount, null, null, "GRAND TOTAL", data.Sum(d => d.Buyers.Sum(b => b.Details.Sum(dtl => dtl.Amount))), null);
                    }
                    else
                    {
                        result.Rows.Add(null, null, null, grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, grandTotalByUom[i].amount, null, null, null, null, null);
                    }
                    mergeCells.Add(($"D{++rowPosition}:D{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null);
            }

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "GarmentOmzet") }, true);
      
            return excel;
        }

        private class TotalByUom
        {
            public string uom { get; set; }
            public double quantity { get; set; }
            public decimal amount { get; set; }
        }

    }
}
