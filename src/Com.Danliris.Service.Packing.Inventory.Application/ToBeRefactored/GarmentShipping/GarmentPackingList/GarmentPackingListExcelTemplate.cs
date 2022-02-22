using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListExcelTemplate
    {
        public IIdentityProvider _identityProvider;

        public GarmentPackingListExcelTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GenerateExcelTemplate(GarmentPackingListViewModel viewModel, string fob, string cPrice)
        {
            var newItems = new List<GarmentPackingListItemViewModel>();
            var newItems2 = new List<GarmentPackingListItemViewModel>();
            var newDetails = new List<GarmentPackingListDetailViewModel>();
            foreach (var item in viewModel.Items)
            {
                foreach (var detail in item.Details)
                {
                    newDetails.Add(detail);
                }
            }
            // if wanna group by order no delete the note by @zhikariz
            newDetails = newDetails.OrderBy(a => a.Index).ThenBy(o => o.Carton1).ThenBy(o => o.Carton2).ToList();

            foreach (var d in newDetails)
            {
                if (newItems.Count == 0)
                {
                    var i = viewModel.Items.Single(a => a.Id == d.PackingListItemId);
                    i.Details = new List<GarmentPackingListDetailViewModel>();
                    i.Details.Add(d);
                    newItems.Add(i);
                }
                else
                {
                    if (newItems.Last().Id == d.PackingListItemId)
                    {
                        newItems.Last().Details.Add(d);
                    }
                    else
                    {
                        var y = viewModel.Items.Select(a => new GarmentPackingListItemViewModel
                        {
                            Id = a.Id,
                            RONo = a.RONo,
                            Article = a.Article,
                            BuyerAgent = a.BuyerAgent,
                            ComodityDescription = a.ComodityDescription,
                            OrderNo = a.OrderNo,
                            AVG_GW = a.AVG_GW,
                            AVG_NW = a.AVG_NW,
                            Description = a.Description
                        })
                            .Single(a => a.Id == d.PackingListItemId);
                        y.Details = new List<GarmentPackingListDetailViewModel>();
                        y.Details.Add(d);
                        newItems.Add(y);
                    }
                }
            }

            foreach (var item in newItems)
            {
                if (newItems2.Count == 0)
                {
                    newItems2.Add(item);
                }
                else
                {
                    if (newItems2.Last().OrderNo == item.OrderNo)
                    {
                        foreach (var d in item.Details.OrderBy(a => a.Carton1))
                        {
                            newItems2.Last().Details.Add(d);
                        }
                    }
                    else
                    {
                        newItems2.Add(item);
                    }
                }
            }

            var sizesCount = false;
            foreach (var item in newItems2)
            {
                var sizesMax = new Dictionary<int, string>();
                foreach (var detail in item.Details.OrderBy(a => a.Carton1).ThenBy(a => a.Carton2))
                {
                    foreach (var size in detail.Sizes)
                    {
                        sizesMax[size.Size.Id] = size.Size.Size;
                    }
                }
                if (sizesMax.Count > 11)
                {
                    sizesCount = true;
                }
            }
            int SIZES_COUNT = sizesCount ? 20 : 11;

            var col = GetColNameFromIndex(4 + SIZES_COUNT);
            var colCtns = GetColNameFromIndex(SIZES_COUNT + 5);
            var colPcs = GetColNameFromIndex(SIZES_COUNT + 6);
            var colQty = GetColNameFromIndex(SIZES_COUNT + 7);
            var colSatuan = GetColNameFromIndex(SIZES_COUNT + 8);
            var colGw = GetColNameFromIndex(SIZES_COUNT + 9);
            var colNw = GetColNameFromIndex(SIZES_COUNT + 10);
            var colNnw = GetColNameFromIndex(SIZES_COUNT + 11);



            DataTable result = new DataTable();

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");


            sheet.Cells["A1"].Value = "Invoice No.";
            sheet.Cells["A1:B1"].Merge = true;
            sheet.Column(1).Width = 6;
            sheet.Column(2).Width = 5;
            sheet.Column(3).Width = 6;
            sheet.Column(3).Width = 7;
            sheet.Cells["C1"].Value = viewModel.InvoiceNo;
            sheet.Cells["C1:D1"].Merge = true;
            sheet.Cells[$"A1:{colNnw}1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells["E1"].Value = "Date : " + viewModel.Date.GetValueOrDefault().ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("MMM dd, yyyy.");
            sheet.Cells[$"E1:{col}1"].Merge = true;
            sheet.Cells[$"{colCtns}1"].Value = " ";
            sheet.Cells[$"{colCtns}1"].Style.Font.Bold = true;
            sheet.Cells[$"{colCtns}1:{colNnw}1"].Merge = true;
            sheet.Cells["A3"].Value = cPrice;
            sheet.Cells["A3:C3"].Merge = true;
            sheet.Cells["D3"].Value = ":";
            sheet.Cells["E3"].Value = fob;
            sheet.Cells[$"E3:{colNnw}3"].Merge = true;
            if (viewModel.PaymentTerm == "LC")
            {
                sheet.Cells["A4"].Value = "LC No.";
                sheet.Cells["A4:C4"].Merge = true;
                sheet.Cells["D4"].Value = ":";
                sheet.Cells["E4"].Value = viewModel.LCNo;
                sheet.Cells[$"E4:{colNnw}4"].Merge = true;
                sheet.Cells["A5"].Value = "Tgl LC.";
                sheet.Cells["A5:C5"].Merge = true;
                sheet.Cells["D5"].Value = ":";
                sheet.Cells["E5"].Value = viewModel.LCDate.GetValueOrDefault().ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy");
                sheet.Cells[$"E5:{colNnw}5"].Merge = true;
                sheet.Cells["A6"].Value = "ISSUED BY";
                sheet.Cells["A6:C6"].Merge = true;
                sheet.Cells["D6"].Value = ":";
                sheet.Cells["E6"].Value = viewModel.IssuedBy;
                sheet.Cells[$"E6:{colNnw}6"].Merge = true;
            }
            else
            {
                sheet.Cells["A4"].Value = "Payment Term";
                sheet.Cells["A4:C4"].Merge = true;
                sheet.Cells["E4"].Value = viewModel.PaymentTerm;
                sheet.Cells[$"E4:{colNnw}4"].Merge = true;
            }

            double totalCtns = 0;
            double totalGw = 0;
            double totalNw = 0;
            double totalNnw = 0;
            double grandTotal = 0;
            var uom = "";
            var arrayGrandTotal = new Dictionary<String, double>();
            List<string> cartonNumbers = new List<string>();

            var index = 8;
            var afterSubTotalIndex = 0;



            foreach (var item in newItems2)
            {

                var afterIndex = index + 1;
                var sizeIndex = afterIndex + 1;
                var valueIndex = sizeIndex + 1;

                sheet.Cells[$"A{index}"].Value = "DESCRIPTION OF GOODS";
                sheet.Cells[$"A{index}:C{index}"].Merge = true;
                sheet.Row(index).Height = 25;
                sheet.Cells[$"D{index}"].Value = ":";
                sheet.Cells[$"E{index}"].Value = item.Description;
                sheet.Cells[$"E{index}:{colNnw}{index}"].Merge = true;

                sheet.Cells[$"A{afterIndex}"].Value = "CARTON NO.";
                sheet.Cells[$"A{afterIndex}:A{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"A{afterIndex}:A{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"A{afterIndex}:A{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{afterIndex}:{colNnw}{afterIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
                sheet.Cells[$"A{afterIndex}:{colNnw}{afterIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[$"A{afterIndex}:A{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[$"A{afterIndex}:{colNnw}{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                sheet.Cells[$"B{afterIndex}"].Value = "COLOUR";
                sheet.Cells[$"B{afterIndex}:B{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"B{afterIndex}:B{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B{afterIndex}:B{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"C{afterIndex}"].Value = "STYLE";
                sheet.Cells[$"C{afterIndex}:C{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"C{afterIndex}:C{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"C{afterIndex}:C{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"D{afterIndex}"].Value = "ORDER. NO.";
                sheet.Cells[$"D{afterIndex}:D{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"D{afterIndex}:D{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"D{afterIndex}:D{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"E{afterIndex}"].Value = "SIZE";
                sheet.Cells[$"E{afterIndex}:{col}{afterIndex}"].Merge = true;
                sheet.Cells[$"E{afterIndex}:{col}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


                var sizes = new Dictionary<int, string>();
                foreach (var detail in item.Details)
                {
                    foreach (var size in detail.Sizes)
                    {
                        sizes[size.Size.Id] = size.Size.Size;
                    }
                };
                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var colSize = GetColNameFromIndex(5 + i);
                    var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                    sheet.Cells[$"{colSize}{sizeIndex}"].Value = size.Key == 0 ? "" : size.Value;
                    sheet.Cells[$"{colSize}{sizeIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                sheet.Cells[$"{colCtns}{afterIndex}"].Value = "CTNS";
                sheet.Column(GetColNumberFromName(colCtns)).Width = 4;
                sheet.Cells[$"{colCtns}{afterIndex}:{colCtns}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colCtns}{afterIndex}:{colCtns}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colCtns}{afterIndex}:{colCtns}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colPcs}{afterIndex}"].Value = "@";
                sheet.Column(GetColNumberFromName(colPcs)).Width = 4;
                sheet.Cells[$"{colPcs}{afterIndex}:{colPcs}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colPcs}{afterIndex}:{colPcs}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colPcs}{afterIndex}:{colPcs}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colQty}{afterIndex}"].Value = "QTY";
                sheet.Column(GetColNumberFromName(colQty)).Width = 4;
                sheet.Cells[$"{colQty}{afterIndex}:{colQty}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colQty}{afterIndex}:{colQty}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colQty}{afterIndex}:{colQty}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colSatuan}{afterIndex}"].Value = "SATUAN";
                sheet.Column(GetColNumberFromName(colSatuan)).Width = 6;
                sheet.Cells[$"{colSatuan}{afterIndex}:{colSatuan}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colSatuan}{afterIndex}:{colSatuan}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colSatuan}{afterIndex}:{colSatuan}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colGw}{afterIndex}"].Value = "GW";
                sheet.Column(GetColNumberFromName(colGw)).Width = 4;
                sheet.Cells[$"{colGw}{afterIndex}:{colGw}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colGw}{afterIndex}:{colGw}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colGw}{afterIndex}:{colGw}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colNw}{afterIndex}"].Value = "NW";
                sheet.Column(GetColNumberFromName(colNw)).Width = 4;
                sheet.Cells[$"{colNw}{afterIndex}:{colNw}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colNw}{afterIndex}:{colNw}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colNw}{afterIndex}:{colNw}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colNnw}{afterIndex}"].Value = "NNW";
                sheet.Column(GetColNumberFromName(colNnw)).Width = 4;
                sheet.Cells[$"{colNnw}{afterIndex}:{colNnw}{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"{colNnw}{afterIndex}:{colNnw}{afterIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colNnw}{afterIndex}:{colNnw}{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                double subCtns = 0;
                double subGw = 0;
                double subNw = 0;
                double subNnw = 0;
                double subTotal = 0;
                var sizeSumQty = new Dictionary<int, double>();
                var arraySubTotal = new Dictionary<String, double>();
                foreach (var detail in item.Details.OrderBy(o => o.Carton1).ThenBy(o => o.Carton2))
                {
                    var ctnsQty = detail.CartonQuantity;
                    uom = viewModel.Items.Where(a => a.Id == detail.PackingListItemId).Single().Uom.Unit;
                    if (cartonNumbers.Contains($"{detail.Carton1}- {detail.Carton2}"))
                    {
                        ctnsQty = 0;
                    }
                    else
                    {
                        cartonNumbers.Add($"{detail.Carton1}- {detail.Carton2}");
                    }
                    sheet.Cells[$"A{valueIndex}"].Value = $"{detail.Carton1}- {detail.Carton2}";
                    sheet.Cells[$"A{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"B{valueIndex}"].Value = detail.Colour;
                    sheet.Cells[$"B{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"C{valueIndex}"].Value = detail.Style;
                    sheet.Cells[$"C{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"D{valueIndex}"].Value = item.OrderNo;
                    sheet.Cells[$"D{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"A{valueIndex}:{colNnw}{valueIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    sheet.Cells[$"A{valueIndex}:{colNnw}{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                    for (int i = 0; i < SIZES_COUNT; i++)
                    {
                        var colSize = GetColNameFromIndex(5 + i);
                        var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                        double quantity = 0;
                        if (size.Key != 0)
                        {
                            quantity = detail.Sizes.Where(w => w.Size.Id == size.Key).Sum(s => s.Quantity);
                        }

                        if (sizeSumQty.ContainsKey(size.Key))
                        {
                            sizeSumQty[size.Key] += quantity * detail.CartonQuantity;
                        }
                        else
                        {
                            sizeSumQty.Add(size.Key, quantity * detail.CartonQuantity);
                        }
                        

                        sheet.Cells[$"{colSize}{valueIndex}"].Value = quantity == 0 ? "" : quantity.ToString();
                        sheet.Column(GetColNumberFromName(colSize)).Width = 3.5;
                        sheet.Cells[$"{colSize}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    subCtns += ctnsQty;
                    subGw += detail.GrossWeight;
                    subNw += detail.NetWeight;
                    subNnw += detail.NetNetWeight;

                    sheet.Cells[$"{colCtns}{valueIndex}"].Value = ctnsQty.ToString();
                    sheet.Cells[$"{colCtns}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colPcs}{valueIndex}"].Value = detail.QuantityPCS.ToString();
                    sheet.Cells[$"{colPcs}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var totalQuantity = detail.CartonQuantity * detail.QuantityPCS;
                    subTotal += totalQuantity;
                    if (!arraySubTotal.ContainsKey(uom))
                    {
                        arraySubTotal.Add(uom, totalQuantity);
                    }
                    else
                    {
                        arraySubTotal[uom] += totalQuantity;
                    }

                    sheet.Cells[$"{colQty}{valueIndex}"].Value = totalQuantity.ToString();
                    sheet.Cells[$"{colQty}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colSatuan}{valueIndex}"].Value = uom;
                    sheet.Cells[$"{colSatuan}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colGw}{valueIndex}"].Value = detail.GrossWeight.ToString();
                    sheet.Cells[$"{colGw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colNw}{valueIndex}"].Value = detail.NetWeight.ToString();
                    sheet.Cells[$"{colNw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colNnw}{valueIndex}"].Value = detail.NetNetWeight.ToString();
                    sheet.Cells[$"{colNnw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    valueIndex++;
                }

                var sumValueIndex = 0;
                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var colSize = GetColNameFromIndex(5 + i);
                    sumValueIndex = valueIndex + 1;
                    var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                    double quantity = 0;
                    if (size.Key != 0)
                    {
                        quantity = sizeSumQty.Where(w => w.Key == size.Key).Sum(a => a.Value);
                    }
                    
                    sheet.Cells[$"D{valueIndex}"].Value = "SUMMARY";
                    sheet.Cells[$"D{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colSize}{valueIndex}"].Value = quantity == 0 ? "" : quantity.ToString();

                }

                sheet.Cells[$"A{valueIndex}:{colNnw}{valueIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[$"A{valueIndex}:{colNnw}{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                totalCtns += subCtns;
                totalGw += subGw;
                totalNw += subNw;
                totalNnw += subNnw;
                grandTotal += subTotal;
                if (!arrayGrandTotal.ContainsKey(uom))
                {
                    arrayGrandTotal.Add(uom, subTotal);
                }
                else
                {
                    arrayGrandTotal[uom] += subTotal;
                }
                var subTotalResult = string.Join(" / ", arraySubTotal.Select(x => x.Value + " " + x.Key).ToArray());
                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Merge = true;
                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Value = "SUB TOTAL";
                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"{colQty}{sumValueIndex}"].Value = subTotalResult;
                sheet.Cells[$"{colQty}{sumValueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"{colQty}{sumValueIndex}:{colNnw}{sumValueIndex}"].Merge = true;
                sheet.Cells[$"A{sumValueIndex}:{colNnw}{sumValueIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[$"A{sumValueIndex}:{colNnw}{sumValueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                afterSubTotalIndex = sumValueIndex + 1;
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Merge = true;

                sheet.Cells[$"A{afterSubTotalIndex}"].Value = $"      - Sub Ctns = {subCtns}       - Sub G.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.GrossWeight * a.CartonQuantity))} Kgs      - Sub N.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.NetWeight * a.CartonQuantity))} Kgs     - Sub N.N.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.NetNetWeight * a.CartonQuantity))} Kgs";
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                afterIndex = sizeIndex++;
                index = afterSubTotalIndex + 2;

            }

            #region GrandTotal
            var grandTotalResult = string.Join(" / ", arrayGrandTotal.Select(x => x.Value + " " + x.Key).ToArray());
            var grandTotalIndex = afterSubTotalIndex + 2;
            sheet.Cells[$"A{grandTotalIndex}:{colPcs}{grandTotalIndex}"].Merge = true;
            sheet.Cells[$"A{grandTotalIndex}:{colPcs}{grandTotalIndex}"].Value = "GRAND TOTAL";
            sheet.Cells[$"A{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            sheet.Cells[$"A{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            sheet.Cells[$"{colQty}{grandTotalIndex}"].Value = grandTotalResult;
            sheet.Cells[$"{colQty}{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Merge = true;

            var comodities = viewModel.Items.Select(s => s.Comodity.Id>0? s.Comodity.Name.ToUpper():"FABRIC").Distinct();
            var spellingWordIndex = grandTotalIndex + 2;
            sheet.Cells[$"A{spellingWordIndex}:{colNnw}{spellingWordIndex}"].Merge = true;
            sheet.Cells[$"A{spellingWordIndex}"].Value = $"{totalCtns} {viewModel.SayUnit} [ {NumberToTextEN.toWords(totalCtns).Trim().ToUpper()} {viewModel.SayUnit} OF {string.Join(" AND ", comodities)}]";

            for (int i = 8; i < grandTotalIndex; i++)
            {
                if (sheet.Row(i).Height != 25)
                {
                    sheet.Row(i).Height = 16;
                }
            }

            #endregion

            #region Mark
            var shippingMarkIndex = spellingWordIndex + 2;
            var sideMarkIndex = spellingWordIndex + 2;

            sheet.Cells[$"A{shippingMarkIndex}"].Value = "SHIPPING MARKS";
            sheet.Cells[$"A{shippingMarkIndex}:B{shippingMarkIndex}"].Merge = true;
            sheet.Cells[$"A{++shippingMarkIndex}"].Value = viewModel.ShippingMark;

            sheet.Cells[$"F{sideMarkIndex}"].Value = "SIDE MARKS";
            sheet.Cells[$"F{sideMarkIndex}:G{sideMarkIndex}"].Merge = true;
            sheet.Cells[$"F{++sideMarkIndex}"].Value = viewModel.SideMark;

            byte[] shippingMarkImage;
            if (!String.IsNullOrEmpty(viewModel.ShippingMarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(viewModel.ShippingMarkImageFile)))
                {
                    shippingMarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.ShippingMarkImageFile));
                    Image shipMarkImage = byteArrayToImage(shippingMarkImage);
                    var imageShippingMarkIndex = shippingMarkIndex + 1;

                    ExcelPicture excelPictureShipMarkImage = sheet.Drawings.AddPicture("ShippingMarkImage", shipMarkImage);
                    excelPictureShipMarkImage.From.Column = 0;
                    excelPictureShipMarkImage.From.Row = imageShippingMarkIndex;
                    excelPictureShipMarkImage.SetSize(200, 200);
                }
            }

            byte[] sideMarkImage;
            if (!String.IsNullOrEmpty(viewModel.SideMarkImageFile) )
            {
                if (IsBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile)))
                {
                    sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile));
                    Image _sideMarkImage = byteArrayToImage(sideMarkImage);
                    var sideMarkImageIndex = sideMarkIndex + 1;

                    ExcelPicture excelPictureSideMarkImage = sheet.Drawings.AddPicture("SideMarkImage", _sideMarkImage);
                    excelPictureSideMarkImage.From.Column = 5;
                    excelPictureSideMarkImage.From.Row = sideMarkImageIndex;
                    excelPictureSideMarkImage.SetSize(200, 200);
                }
            }

            #endregion

            #region Measurement

            var grossWeightIndex = shippingMarkIndex + 18;
            var netWeightIndex = grossWeightIndex + 1;
            var netNetWeightIndex = netWeightIndex + 1;
            var measurementIndex = netNetWeightIndex + 1;

            sheet.Cells[$"A{grossWeightIndex}"].Value = "GROSS WEIGHT";
            sheet.Cells[$"A{grossWeightIndex}:B{grossWeightIndex}"].Merge = true;
            sheet.Cells[$"C{grossWeightIndex}"].Value = viewModel.GrossWeight + " KGS";

            sheet.Cells[$"A{netWeightIndex}"].Value = "NET WEIGHT";
            sheet.Cells[$"A{netWeightIndex}:B{netWeightIndex}"].Merge = true;
            sheet.Cells[$"C{netWeightIndex}"].Value = viewModel.NettWeight + " KGS";

            sheet.Cells[$"A{netNetWeightIndex}"].Value = "NET NET WEIGHT";
            sheet.Cells[$"A{netNetWeightIndex}:B{netNetWeightIndex}"].Merge = true;
            sheet.Cells[$"C{netNetWeightIndex}"].Value = viewModel.NetNetWeight + " KGS";

            sheet.Cells[$"A{measurementIndex}"].Value = "MEASUREMENT";
            sheet.Cells[$"A{measurementIndex}:B{measurementIndex}"].Merge = true;

            decimal totalCbm = 0;
            foreach (var measurement in viewModel.Measurements)
            {
                sheet.Cells[$"C{measurementIndex}"].Value = measurement.Length + " X ";
                sheet.Cells[$"D{measurementIndex}"].Value = measurement.Width + " X ";
                sheet.Cells[$"E{measurementIndex}"].Value = measurement.Height + " X ";
                sheet.Cells[$"E{measurementIndex}:G{measurementIndex}"].Merge = true;
                sheet.Cells[$"H{measurementIndex}"].Value = measurement.CartonsQuantity + " CTNS";
                sheet.Cells[$"H{measurementIndex}:I{measurementIndex}"].Merge = true;

                sheet.Cells[$"J{measurementIndex}"].Value = "=";


                var cbm = (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000;
                totalCbm += cbm;
                sheet.Cells[$"K{measurementIndex}"].Value = string.Format("{0:N2} CBM", cbm);
                sheet.Cells[$"K{measurementIndex}:M{measurementIndex}"].Merge = true;

                measurementIndex++;
            }
            var totalMeasurementIndex = measurementIndex;
            sheet.Cells[$"C{totalMeasurementIndex}:K{totalMeasurementIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[$"D{totalMeasurementIndex}"].Value = "TOTAL";
            sheet.Cells[$"D{totalMeasurementIndex}:G{totalMeasurementIndex}"].Merge = true;
            sheet.Cells[$"H{totalMeasurementIndex}"].Value = viewModel.Measurements.Sum(m => m.CartonsQuantity) + " CTNS .";
            sheet.Cells[$"H{totalMeasurementIndex}:I{totalMeasurementIndex}"].Merge = true;
            sheet.Cells[$"K{totalMeasurementIndex}"].Value = string.Format("{0:N2} CBM", totalCbm);
            sheet.Cells[$"J{totalMeasurementIndex}"].Value = "=";
            sheet.Cells[$"K{totalMeasurementIndex}:L{totalMeasurementIndex}"].Merge = true;


            #endregion

            #region remark
            var remarkIndex = totalMeasurementIndex + 1;
            sheet.Cells[$"A{remarkIndex}"].Value = "REMARK";
            sheet.Cells[$"A{++remarkIndex}"].Value = viewModel.Remark;

            byte[] remarkImage;
            var remarkImageIndex = remarkIndex + 1;
            if (!String.IsNullOrEmpty(viewModel.RemarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(viewModel.RemarkImageFile)))
                {
                    remarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.RemarkImageFile));
                    Image _remarkImage = byteArrayToImage(remarkImage);
                    ExcelPicture excelPictureRemarkImage = sheet.Drawings.AddPicture("RemarkImage", _remarkImage);
                    excelPictureRemarkImage.From.Column = 0;
                    excelPictureRemarkImage.From.Row = remarkImageIndex;
                    excelPictureRemarkImage.SetSize(200, 200);
                }
                
            }


            #endregion

            #region Signature
            var signatureIndex = remarkImageIndex + 14;
            sheet.Cells[$"{colCtns}{signatureIndex}:{colNnw}{signatureIndex}"].Merge = true;
            sheet.Cells[$"{colCtns}{signatureIndex}"].Value = "( MRS.ADRIYANA DAMAYANTI )";
            sheet.Cells[$"{colCtns}{signatureIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"{colCtns}{++signatureIndex}"].Value = "AUTHORIZED SIGNATURE";
            sheet.Cells[$"{colCtns}{signatureIndex}:{colNnw}{signatureIndex}"].Merge = true;
            sheet.Cells[$"{colCtns}{signatureIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            #endregion

            sheet.Cells.Style.Font.SetFromFont(new Font("Tahoma", 7, FontStyle.Regular));
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns(15, 40);
            sheet.Cells.Style.WrapText = true;

            sheet.PrinterSettings.LeftMargin = 0.39M;
            sheet.PrinterSettings.TopMargin = 0;
            sheet.PrinterSettings.RightMargin = 0;
            
            sheet.PrinterSettings.Orientation = sizesCount ? eOrientation.Landscape : eOrientation.Portrait;


            MemoryStream stream = new MemoryStream();
            package.DoAdjustDrawings = false;
            package.SaveAs(stream);
            return stream;
        }

        public string GetColNameFromIndex(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public int GetColNumberFromName(string columnName)
        {
            char[] characters = columnName.ToUpperInvariant().ToCharArray();
            int sum = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }
            return sum;
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            Image img = Image.FromStream(memstr);
            return img;
        }

        public  bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    
    }
}
