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
    public class GarmentPackingListDraftExcelTemplate
    {
        private readonly IIdentityProvider _identityProvider;
        public GarmentPackingListDraftExcelTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GenerateExcelTemplate(GarmentPackingListViewModel viewModel)
        {
            //int maxSizesCount = viewModel.Items == null || viewModel.Items.Count < 1 ? 0 : viewModel.Items.Max(i => i.Details == null || i.Details.Count < 1 ? 0 : i.Details.Max(d => d.Sizes == null || d.Sizes.Count < 1 ? 0 : d.Sizes.GroupBy(g => g.Size.Id).Count()));
            int maxSizesCount = 0;
            var sizesMax = new Dictionary<int, string>();
            foreach (var item in viewModel.Items)
            {
                foreach (var detail in item.Details)
                {
                    foreach (var size in detail.Sizes)
                    {
                        sizesMax[size.Size.Id] = size.Size.Size;
                    }
                }
            }
            maxSizesCount = sizesMax.Count;
            int SIZES_COUNT = maxSizesCount > 11 ? 20 : 11;

            var col = GetColNameFromIndex(SIZES_COUNT + 4);
            var colCtns = GetColNameFromIndex(SIZES_COUNT + 5);
            var colPcs = GetColNameFromIndex(SIZES_COUNT + 6);
            var colQty = GetColNameFromIndex(SIZES_COUNT + 7);
            var colGw = GetColNameFromIndex(SIZES_COUNT + 8);
            var colNw = GetColNameFromIndex(SIZES_COUNT + 9);
            var colNnw = GetColNameFromIndex(SIZES_COUNT + 10);

            DataTable result = new DataTable();

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");
            sheet.Cells.Style.Font.SetFromFont(new Font("Tahoma", 7, FontStyle.Regular));

            sheet.Cells["A1"].Value = "DRAFT PACKING LIST";
            sheet.Cells[$"A1:{colNnw}1"].Merge = true;
            sheet.Cells[$"A1:{colNnw}1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["A1"].Style.Font.SetFromFont(new Font("Helvetica", 25));
            sheet.Cells["A1"].Style.Font.Bold = true;
            sheet.Row(1).Height = 30;

            sheet.Cells["A2"].Value = "Invoice No.";
            sheet.Cells["A2:B2"].Merge = true;
            sheet.Cells["C2"].Value = ":";
            sheet.Cells["D2"].Value = viewModel.InvoiceNo;
            sheet.Cells["D2:E2"].Merge = true;

            sheet.Cells[$"{colCtns}2"].Value = "Date";
            sheet.Cells[$"{colPcs}2"].Value = ":";
            sheet.Cells[$"{colQty}2"].Value = viewModel.CreatedUtc.ToString("MMM dd, yyyy.");
            sheet.Cells[$"{colQty}2:{colNnw}2"].Merge = true;

            sheet.Cells[$"A2:{colNnw}2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;

            sheet.Cells["A4"].Value = "SHIPPING METHOD";
            sheet.Cells["A4:B4"].Merge = true;
            sheet.Cells["C4"].Value = ":";
            sheet.Cells["D4"].Value = viewModel.ShipmentMode;
            sheet.Cells["D4:E4"].Merge = true;

            var cartons = new List<GarmentPackingListDetailViewModel>();
            double grandTotal = 0;
            List<string> cartonNumbers = new List<string>();

            var newItems = new List<GarmentPackingListItemViewModel>();
            var newDetails = new List<GarmentPackingListDetailViewModel>();

            foreach (var item in viewModel.Items.OrderBy(a => a.RONo))
            {
                foreach (var detail in item.Details)
                {
                    newDetails.Add(detail);
                }
            }
            newDetails = newDetails.OrderBy(a => a.Carton1).ToList();

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
                            AVG_NW = a.AVG_NW
                        })
                            .Single(a => a.Id == d.PackingListItemId);
                        y.Details = new List<GarmentPackingListDetailViewModel>();
                        y.Details.Add(d);
                        newItems.Add(y);
                    }
                }
            }
            var index = 8;
            var afterSubTotalIndex = 0;
            foreach (var item in newItems)
            {
                var sizeIndex = index + 1;
                var valueIndex = sizeIndex + 1;

                #region Item
                sheet.Cells["A5"].Value = "RO No";
                sheet.Cells["A5:B5"].Merge = true;
                sheet.Cells["C5"].Value = ":";
                sheet.Cells["D5"].Value = item.RONo;
                sheet.Cells["D5:H5"].Merge = true;

                sheet.Cells["I5"].Value = "ARTICLE";
                sheet.Cells["I5:O5"].Merge = true;
                sheet.Cells["P5"].Value = ":";
                sheet.Cells["Q5"].Value = item.Article;
                sheet.Cells[$"Q5:{colNnw}5"].Merge = true;

                sheet.Cells["A6"].Value = "BUYER";
                sheet.Cells["A6:B6"].Merge = true;
                sheet.Cells["C6"].Value = ":";
                sheet.Cells["D6"].Value = viewModel.BuyerAgent.Name;
                sheet.Cells["D6:H6"].Merge = true;

                sheet.Cells["I6"].Value = "DESCRIPTION OF GOODS";
                sheet.Cells["I6:O6"].Merge = true;
                sheet.Cells["P6"].Value = ":";
                sheet.Cells["Q6"].Value = item.ComodityDescription;
                sheet.Cells[$"Q6:{colNnw}6"].Merge = true;
                #endregion

                var sizes = new Dictionary<int, string>();
                foreach (var detail in item.Details)
                {
                    foreach (var size in detail.Sizes)
                    {
                        sizes[size.Size.Id] = size.Size.Size;
                    }
                }

                sheet.Cells[$"A{index}"].Value = "CARTON NO.";
                sheet.Cells[$"A{index}:A{index + 1}"].Merge = true;
                sheet.Cells[$"A{index}:A{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"A{index}:A{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{index}:{colNnw}{index}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
                sheet.Cells[$"A{index}:{colNnw}{index}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[$"A{index}:A{index + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[$"A{index}:{colNnw}{index + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Column(GetColNumberFromName("A")).Width = 8;

                sheet.Cells[$"B{index}"].Value = "COLOUR";
                sheet.Cells[$"B{index}:B{index + 1}"].Merge = true;
                sheet.Cells[$"B{index}:B{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B{index}:B{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Column(GetColNumberFromName("B")).Width = 5;

                sheet.Cells[$"C{index}"].Value = "STYLE";
                sheet.Cells[$"C{index}:C{index + 1}"].Merge = true;
                sheet.Cells[$"C{index}:C{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"C{index}:C{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Column(GetColNumberFromName("C")).Width = 5;

                sheet.Cells[$"D{index}"].Value = "ORDER NO.";
                sheet.Cells[$"D{index}:D{index + 1}"].Merge = true;
                sheet.Cells[$"D{index}:D{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"D{index}:D{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Column(GetColNumberFromName("D")).Width = 7;

                sheet.Cells[$"E{index}"].Value = "SIZE";
                sheet.Cells[$"E{index}:{col}{index}"].Merge = true;
                sheet.Cells[$"E{index}:{col}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colCtns}{index}"].Value = "CTNS";
                sheet.Column(GetColNumberFromName(colCtns)).Width = 5;
                sheet.Cells[$"{colCtns}{index}:{colCtns}{index + 1}"].Merge = true;
                sheet.Cells[$"{colCtns}{index}:{colCtns}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colCtns}{index}:{colCtns}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colPcs}{index}"].Value = "@ PCS";
                sheet.Column(GetColNumberFromName(colPcs)).Width = 5;
                sheet.Cells[$"{colPcs}{index}:{colPcs}{index + 1}"].Merge = true;
                sheet.Cells[$"{colPcs}{index}:{colPcs}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colPcs}{index}:{colPcs}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colQty}{index}"].Value = "QTY PCS";
                sheet.Column(GetColNumberFromName(colQty)).Width = 4;
                sheet.Cells[$"{colQty}{index}:{colQty}{index + 1}"].Merge = true;
                sheet.Cells[$"{colQty}{index}:{colQty}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colQty}{index}:{colQty}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colGw}{index}"].Value = "GW";
                sheet.Column(GetColNumberFromName(colGw)).Width = 4;
                sheet.Cells[$"{colGw}{index}:{colGw}{index + 1}"].Merge = true;
                sheet.Cells[$"{colGw}{index}:{colGw}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colGw}{index}:{colGw}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colNw}{index}"].Value = "NW";
                sheet.Column(GetColNumberFromName(colNw)).Width = 4;
                sheet.Cells[$"{colNw}{index}:{colNw}{index + 1}"].Merge = true;
                sheet.Cells[$"{colNw}{index}:{colNw}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colNw}{index}:{colNw}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colNnw}{index}"].Value = "NNW";
                sheet.Column(GetColNumberFromName(colNnw)).Width = 4;
                sheet.Cells[$"{colNnw}{index}:{colNnw}{index + 1}"].Merge = true;
                sheet.Cells[$"{colNnw}{index}:{colNnw}{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"{colNnw}{index}:{colNnw}{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var colSize = GetColNameFromIndex(5 + i);
                    var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                    sheet.Cells[$"{colSize}{sizeIndex}"].Value = size.Key == 0 ? "" : size.Value;
                    sheet.Cells[$"{colSize}{sizeIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                var subCartons = new List<GarmentPackingListDetailViewModel>();
                double subTotal = 0;
                var sizeSumQty = new Dictionary<int, double>();
                foreach (var detail in item.Details)
                {
                    var ctnsQty = detail.CartonQuantity;
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

                    sheet.Cells[$"{colCtns}{valueIndex}"].Value = ctnsQty.ToString();
                    sheet.Cells[$"{colCtns}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colPcs}{valueIndex}"].Value = detail.QuantityPCS.ToString();
                    sheet.Cells[$"{colPcs}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var totalQuantity = (detail.CartonQuantity * detail.QuantityPCS);
                    subTotal += totalQuantity;

                    sheet.Cells[$"{colQty}{valueIndex}"].Value = totalQuantity.ToString();
                    sheet.Cells[$"{colQty}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colGw}{valueIndex}"].Value = string.Format("{0:n2}", detail.GrossWeight);
                    sheet.Cells[$"{colGw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colNw}{valueIndex}"].Value = string.Format("{0:n2}", detail.NetWeight);
                    sheet.Cells[$"{colNw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[$"{colNnw}{valueIndex}"].Value = string.Format("{0:n2}", detail.NetNetWeight);
                    sheet.Cells[$"{colNnw}{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    if (cartons.FindIndex(c => c.Carton1 == detail.Carton1 && c.Carton2 == detail.Carton2) < 0)
                    {
                        cartons.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = ctnsQty });
                    }
                    if (subCartons.FindIndex(c => c.Carton1 == detail.Carton1 && c.Carton2 == detail.Carton2) < 0)
                    {
                        subCartons.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = ctnsQty });
                    }
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

                grandTotal += subTotal;

                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Merge = true;
                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Value = "SUB TOTAL";
                sheet.Cells[$"A{sumValueIndex}:{colPcs}{sumValueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[$"{colQty}{sumValueIndex}"].Value = subTotal.ToString();
                sheet.Cells[$"{colQty}{sumValueIndex}:{colNnw}{sumValueIndex}"].Merge = true;
                sheet.Cells[$"{colQty}{sumValueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{sumValueIndex}:{colNnw}{sumValueIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[$"A{sumValueIndex}:{colNnw}{sumValueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                var subCtns = subCartons.Sum(c => c.CartonQuantity);
                afterSubTotalIndex = sumValueIndex + 1;

                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Merge = true;
                sheet.Cells[$"A{afterSubTotalIndex}"].Value = $"      - Sub Ctns = {subCtns}       - Sub G.W. = {item.Details.Sum(a => a.GrossWeight * a.CartonQuantity)} Kgs      - Sub N.W. = {item.Details.Sum(a => a.NetWeight * a.CartonQuantity)} Kgs     - Sub N.N.W. = {item.Details.Sum(a => a.NetNetWeight*a.CartonQuantity)} Kgs";
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[$"A{afterSubTotalIndex}:{colNnw}{afterSubTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                index = afterSubTotalIndex + 2;
            }

            #region GrandTotal
            var grandTotalIndex = afterSubTotalIndex + 2;
            sheet.Cells[$"A{grandTotalIndex}:{colPcs}{grandTotalIndex}"].Merge = true;
            sheet.Cells[$"A{grandTotalIndex}:{colPcs}{grandTotalIndex}"].Value = "GRAND TOTAL";
            sheet.Cells[$"A{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            sheet.Cells[$"A{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            sheet.Cells[$"{colQty}{grandTotalIndex}"].Value = grandTotal.ToString();
            sheet.Cells[$"{colQty}{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Merge = true;
            sheet.Cells[$"{colQty}{grandTotalIndex}:{colNnw}{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            var totalCtns = cartons.Sum(c => c.CartonQuantity);
            var comodities = viewModel.Items.Select(s => s.Comodity.Name.ToUpper()).Distinct();
            var spellingWordIndex = grandTotalIndex + 2;
            sheet.Cells[$"A{spellingWordIndex}:{colNnw}{spellingWordIndex}"].Merge = true;
            sheet.Cells[$"A{spellingWordIndex}"].Value = $"{totalCtns} {viewModel.SayUnit} [ {NumberToTextEN.toWords(totalCtns).Trim().ToUpper()} {viewModel.SayUnit} OF {string.Join(" AND ", comodities)}]";

            for (int i = 8; i < grandTotalIndex; i++)
            {
                sheet.Row(i).Height = 16;
            }

            #endregion

            #region Mark
            var shippingMarkIndex = spellingWordIndex + 2;
            var sideMarkIndex = spellingWordIndex + 2;

            sheet.Cells[$"A{shippingMarkIndex}"].Value = "SHIPPING MARKS";
            sheet.Cells[$"A{shippingMarkIndex}:B{shippingMarkIndex}"].Merge = true;
            sheet.Cells[$"A{++shippingMarkIndex}"].Value = viewModel.ShippingMark;

            sheet.Cells[$"H{sideMarkIndex}"].Value = "SIDE MARKS";
            sheet.Cells[$"H{sideMarkIndex}:J{sideMarkIndex}"].Merge = true;
            sheet.Cells[$"H{++sideMarkIndex}"].Value = viewModel.SideMark;

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
            if (!String.IsNullOrEmpty(viewModel.SideMarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile)))
                {
                    sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile));
                    Image _sideMarkImage = byteArrayToImage(sideMarkImage);
                    var sideMarkImageIndex = sideMarkIndex + 1;

                    ExcelPicture excelPictureSideMarkImage = sheet.Drawings.AddPicture("SideMarkImage", _sideMarkImage);
                    excelPictureSideMarkImage.From.Column = 7;
                    excelPictureSideMarkImage.From.Row = sideMarkImageIndex;
                    excelPictureSideMarkImage.SetSize(200, 200);
                }
            }

            #endregion

            #region Measurement
            var grossWeightIndex = shippingMarkIndex + 13;
            var netWeightIndex = grossWeightIndex + 1;
            var netNetWeightIndex = netWeightIndex + 1;
            var measurementIndex = netNetWeightIndex + 1;

            sheet.Cells[$"A{grossWeightIndex}"].Value = "GROSS WEIGHT";
            sheet.Cells[$"A{grossWeightIndex}:B{grossWeightIndex}"].Merge = true;
            sheet.Cells[$"C{grossWeightIndex}"].Value = ":";
            sheet.Cells[$"D{grossWeightIndex}"].Value = viewModel.GrossWeight + " KGS";

            sheet.Cells[$"A{netWeightIndex}"].Value = "NET WEIGHT";
            sheet.Cells[$"A{netWeightIndex}:B{netWeightIndex}"].Merge = true;
            sheet.Cells[$"C{netWeightIndex}"].Value = ":";
            sheet.Cells[$"D{netWeightIndex}"].Value = viewModel.NettWeight + " KGS";

            sheet.Cells[$"A{netNetWeightIndex}"].Value = "NET NET WEIGHT";
            sheet.Cells[$"A{netNetWeightIndex}:B{netNetWeightIndex}"].Merge = true;
            sheet.Cells[$"C{netNetWeightIndex}"].Value = ":";
            sheet.Cells[$"D{netNetWeightIndex}"].Value = viewModel.NetNetWeight + " KGS";

            sheet.Cells[$"A{measurementIndex}"].Value = "MEASUREMENT";
            sheet.Cells[$"A{measurementIndex}:B{measurementIndex}"].Merge = true;
            sheet.Cells[$"C{measurementIndex}"].Value = ":";

            decimal totalCbm = 0;
            foreach (var measurement in viewModel.Measurements)
            {
                sheet.Cells[$"D{measurementIndex}"].Value = measurement.Length + " X ";
                sheet.Cells[$"D{measurementIndex}:E{measurementIndex}"].Merge = true;

                sheet.Cells[$"F{measurementIndex}"].Value = measurement.Width + " X ";
                sheet.Cells[$"F{measurementIndex}:G{measurementIndex}"].Merge = true;

                sheet.Cells[$"H{measurementIndex}"].Value = measurement.Height + " X ";
                sheet.Cells[$"H{measurementIndex}:I{measurementIndex}"].Merge = true;

                sheet.Cells[$"J{measurementIndex}"].Value = measurement.CartonsQuantity + " CTNS";
                sheet.Cells[$"J{measurementIndex}:K{measurementIndex}"].Merge = true;

                sheet.Cells[$"L{measurementIndex}"].Value = "=";

                var cbm = (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000;
                totalCbm += cbm;
                sheet.Cells[$"M{measurementIndex}"].Value = string.Format("{0:N2} CBM", cbm);
                sheet.Cells[$"M{measurementIndex}:O{measurementIndex}"].Merge = true;

                measurementIndex++;
            }
            var totalMeasurementIndex = measurementIndex;
            sheet.Cells[$"D{totalMeasurementIndex}:O{totalMeasurementIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[$"D{totalMeasurementIndex}"].Value = "TOTAL";
            sheet.Cells[$"D{totalMeasurementIndex}:I{totalMeasurementIndex}"].Merge = true;

            sheet.Cells[$"J{totalMeasurementIndex}"].Value = viewModel.Measurements.Sum(m => m.CartonsQuantity) + " CTNS .";
            sheet.Cells[$"J{totalMeasurementIndex}:K{totalMeasurementIndex}"].Merge = true;
            sheet.Cells[$"M{totalMeasurementIndex}"].Value = string.Format("{0:N2} CBM", totalCbm);
            sheet.Cells[$"L{totalMeasurementIndex}"].Value = "=";
            sheet.Cells[$"M{totalMeasurementIndex}:O{totalMeasurementIndex}"].Merge = true;

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

            sheet.Cells.Style.WrapText = true;
            sheet.PrinterSettings.LeftMargin = 0.39M;
            sheet.PrinterSettings.TopMargin = 0;
            sheet.PrinterSettings.RightMargin = 0;
            sheet.PrinterSettings.Orientation = maxSizesCount > 11 ? eOrientation.Landscape : eOrientation.Portrait;

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

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}