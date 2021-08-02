using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesReportByBuyer
{
    public class GarmentLocalSalesReportByBuyerService : IGarmentLocalSalesReportByBuyerService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository itemrepository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentLocalSalesReportByBuyerService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
           _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentLocalSalesReportByBuyerViewModel> GetData(string buyer, string lstype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryitem = itemrepository.ReadAll();
   
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            var newQ = (from a in query
                        join b in queryitem on a.Id equals b.LocalSalesNoteId
                        where a.BuyerCode == (string.IsNullOrWhiteSpace(buyer) ? a.BuyerCode : buyer)
                              && a.TransactionTypeCode == (string.IsNullOrWhiteSpace(lstype) ? a.TransactionTypeCode : lstype)


                        select new GarmentLocalSalesReportByBuyerListViewModel
                        {
                            LSNo = a.NoteNo,
                            LSDate = a.Date,
                            LSType = a.TransactionTypeName,
                            BuyerName = a.BuyerCode + " - " + a.BuyerName,
                            Tempo = a.Tempo,
                            DispoNo = a.DispositionNo,
                            Remark = a.Remark,
                            UseVat = a.UseVat ? "YA" : "TIDAK",
                            ProductCode = b.ProductCode,
                            ProductName = b.ProductName,
                            Quantity = b.Quantity,
                            UOMUnit = b.UomUnit,
                            Price = b.Price,
                            Amount = Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price),
                        }).OrderBy(o => o.BuyerName).ThenBy(o => o.LSDate).ThenBy(o => o.LSNo);              

            var glocalsales = newQ.ToList()
                .GroupBy(gmtls => {
                    var byr = gmtls.BuyerName;
                    return byr;
                })
                .Select(grouplocalsales => new GarmentLocalSalesReportByBuyerViewModel
                {
                    BuyerName = grouplocalsales.Key,
                    LocalSalesNo = grouplocalsales
                        .GroupBy(ba => ba.LSNo)
                        .Select(grouplsno => new GarmentLocalSalesReportByBuyerBuyerViewModel
                        {
                            NoteNo = grouplsno.Key,
                            Quantities = grouplsno.Sum(s => s.Quantity),
                            Amounts = grouplsno.Sum(s => s.Amount),
                            Details = grouplsno
                                .Select(detail => GetGarmentLocalSales(detail))
                                .OrderBy(o => o.LSDate)
                                .ToList()
                        })
                        .OrderBy(o => o.NoteNo)
                        .ToList()
                })
                .ToList();

            return glocalsales.AsQueryable();
        }

        GarmentLocalSalesReportByBuyerDetailViewModel GetGarmentLocalSales(GarmentLocalSalesReportByBuyerListViewModel go)
        {
            return new GarmentLocalSalesReportByBuyerDetailViewModel
            {
                LSDate = go.LSDate,
                LSType = go.LSType,
                Tempo = go.Tempo,
                DispoNo = go.DispoNo,
                Remark = go.Remark,
                UseVat = go.UseVat,
                ProductCode = go.ProductCode,
                ProductName = go.ProductName,
                Quantity = go.Quantity,
                UOMUnit = go.UOMUnit,
                Price = go.Price,
                Amount = go.Amount,
            };
        }

        public List<GarmentLocalSalesReportByBuyerViewModel> GetReportData(string buyer, string lstype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyer, lstype, dateFrom, dateTo, offset);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyer, string lstype, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyer, lstype, dateFrom, dateTo, offset);
            var data = Query.ToList();
            DataTable result = new DataTable();
           
            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JENIS NOTA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO DISPOSISI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "K E T E R A N G A N", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TEMPO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KENA PPN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KODE BARANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BARANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JUMLAH", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "H A R G A", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "JUMLAH HARGA", DataType = typeof(string) });


            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            int rowPosition = 2;
            if (data != null && data.Count > 0)
            {
                var grandTotalByUom = new List<TotalByUom>();

                foreach (var d in data)
                {
                    var ucFirstMergedRowPosition = rowPosition;
                    var ucLastMergedRowPosition = rowPosition;

                    foreach (var lsnumber in d.LocalSalesNo)
                    {
                        var buyerFirstMergedRowPosition = rowPosition;
                        var buyerLastMergedRowPosition = rowPosition;
                        var index = 0;
                        foreach (var detail in lsnumber.Details)
                        {
                            index++;
                            result.Rows.Add(index, d.BuyerName, lsnumber.NoteNo, detail.LSDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), detail.LSType, detail.DispoNo, detail.Remark, detail.Tempo, detail.UseVat, detail.ProductCode, detail.ProductName, detail.Quantity, detail.UOMUnit, detail.Price, detail.Amount);

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

                        result.Rows.Add(null, null, null, null, null, "SUB TOTAL", null, null, null, null, null, lsnumber.Quantities, null, null, lsnumber.Amounts);

                        mergeCells.Add(($"B{rowPosition}:G{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));

                        ucLastMergedRowPosition = rowPosition++;
                    }

                    if (ucFirstMergedRowPosition != ucLastMergedRowPosition)
                    {
                        mergeCells.Add(($"A{ucFirstMergedRowPosition}:A{ucLastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top));
                    }
                }

                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                        result.Rows.Add(null, null, "GRAND TOTAL", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, null, grandTotalByUom[i].amount, null, null, null, "GRAND TOTAL", data.Sum(d => d.LocalSalesNo.Sum(b => b.Details.Sum(dtl => dtl.Amount))), null, null);
                    }
                    else
                    {
                        result.Rows.Add(null, null, null, grandTotalByUom[i].quantity, grandTotalByUom[i].uom, null, null, grandTotalByUom[i].amount, null, null, null, null, null, null, null);
                    }
                    mergeCells.Add(($"D{++rowPosition}:D{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "GarmentLocalSales") }, true);
      
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
