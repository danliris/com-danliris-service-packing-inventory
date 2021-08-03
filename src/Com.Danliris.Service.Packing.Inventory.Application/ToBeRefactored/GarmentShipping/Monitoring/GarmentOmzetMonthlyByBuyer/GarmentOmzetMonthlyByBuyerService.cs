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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByBuyer
{
    public class GarmentOmzetMonthlyByBuyerService : IGarmentOmzetMonthlyByBuyerService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentOmzetMonthlyByBuyerService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentOmzetMonthlyByBuyerViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryitem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryPL = queryPL.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            queryPL = queryPL.Where(w => w.Omzet == true);
            queryPL = queryPL.Where(w => w.IsUsed == true);

            var newQ = (from a in queryPL
                        join b in query on a.Id equals b.PackingListId
                        join c in queryitem on b.Id equals c.GarmentShippingInvoiceId
      
                        select new GarmentOmzetMonthlyByBuyerListViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            TruckingDate = a.TruckingDate,
                            BuyerName = a.BuyerAgentCode + " - " + a.BuyerAgentName,
                            RO_Number = c.RONo,
                            ComodityName = c.ComodityName,
                            ComodityDesc = c.ComodityDesc,
                            UnitCode = c.UnitCode,
                            UOMUnit = c.UomUnit,
                            Amount = c.Amount, 
                            Quantity = c.Quantity,
                        }).OrderBy(o => o.BuyerName).ThenBy(o => o.UnitCode).ThenBy(o => o.TruckingDate).ThenBy(o => o.InvoiceNo);              

            var garmentomzets = newQ.ToList()
                .GroupBy(gmtomzt => {
                    var buyeragent = gmtomzt.BuyerName;
                    return buyeragent;
                })
                .Select(groupomzt => new GarmentOmzetMonthlyByBuyerViewModel
                {
                    BuyerName = groupomzt.Key,
                    Units = groupomzt
                        .GroupBy(ba => ba.UnitCode)
                        .Select(groupUnit => new GarmentOmzetMonthlyByBuyerBuyerViewModel
                        {
                            UnitCode = groupUnit.Key,
                            Quantities = groupUnit.Sum(s => s.Quantity),
                            Amounts = groupUnit.Sum(s => s.Amount),
                            Details = groupUnit
                                .Select(detail => GetGarmentOmzet(detail))
                                .OrderBy(o => o.TruckingDate)
                                .ToList()
                        })
                        .OrderBy(o => o.UnitCode)
                        .ToList()
                })
                .ToList();

            return garmentomzets.AsQueryable();
        }

        GarmentOmzetMonthlyByBuyerDetailViewModel GetGarmentOmzet(GarmentOmzetMonthlyByBuyerListViewModel go)
        {
            return new GarmentOmzetMonthlyByBuyerDetailViewModel
            {
                InvoiceNo = go.InvoiceNo,
                TruckingDate = go.TruckingDate,
                RO_Number = go.RO_Number,
                ComodityName = go.ComodityName,
                ComodityDesc = go.ComodityDesc,
                UOMUnit = go.UOMUnit,
                Quantity = go.Quantity,
                Amount = go.Amount,
            };
        }

        public List<GarmentOmzetMonthlyByBuyerViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            var data = Query.ToList();
            DataTable result = new DataTable();
           
            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
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

                    foreach (var unit in d.Units)
                    {
                        var buyerFirstMergedRowPosition = rowPosition;
                        var buyerLastMergedRowPosition = rowPosition;
                        var index = 0;
                        foreach (var detail in unit.Details)
                        {
                            index++;
                            result.Rows.Add(index, d.BuyerName, unit.UnitCode, detail.InvoiceNo, detail.TruckingDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), detail.RO_Number, detail.ComodityName, detail.ComodityDesc, detail.Quantity, detail.UOMUnit, detail.Amount);

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

                        result.Rows.Add(null, "SUB TOTAL", null, null, null, null, null, null, unit.Quantities, null, unit.Amounts);

                        mergeCells.Add(($"B{rowPosition}:G{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));

                        ucLastMergedRowPosition = rowPosition++;
                    }

                    if (ucFirstMergedRowPosition != ucLastMergedRowPosition)
                    {
                        mergeCells.Add(($"A{ucFirstMergedRowPosition}:A{ucLastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top));
                    }
                }

                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null);
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null);

                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                        result.Rows.Add(null, "GRAND TOTAL", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, grandTotalByUom[i].amount, null, null, "GRAND TOTAL", data.Sum(d => d.Units.Sum(b => b.Details.Sum(dtl => dtl.Amount))), null);
                    }
                    else
                    {
                        result.Rows.Add(null, null, grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, grandTotalByUom[i].amount, null, null, null, null, null);
                    }
                    mergeCells.Add(($"D{++rowPosition}:D{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null);
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
