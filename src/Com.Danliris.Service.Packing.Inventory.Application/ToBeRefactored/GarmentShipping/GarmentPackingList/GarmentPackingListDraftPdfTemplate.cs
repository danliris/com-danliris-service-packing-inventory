using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftPdfTemplate
    {
        private IIdentityProvider _identityProvider;

        public GarmentPackingListDraftPdfTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GeneratePdfTemplate(GarmentPackingListViewModel viewModel)
        {
            int maxSizesCount = viewModel.Items == null || viewModel.Items.Count < 1 ? 0 : viewModel.Items.Max(i => i.Details == null || i.Details.Count < 1 ? 0 : i.Details.Max(d => d.Sizes == null || d.Sizes.Count < 1 ? 0 : d.Sizes.GroupBy(g => g.Size.Id).Count()));
            int SIZES_COUNT = maxSizesCount > 11 ? 20 : 11;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(maxSizesCount > 11 ? PageSize.A4.Rotate() : PageSize.A4, 20, 20, 70, 30);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.PageEvent = new GarmentPackingListDraftPDFTemplatePageEvent(_identityProvider, viewModel);

            document.Open();
            PdfContentByte cb = writer.DirectContent;

            PdfPCell cellBorderBottomRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBorderBottom = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            var cartons = new List<GarmentPackingListDetailViewModel>();
            double grandTotal = 0;

            var arraySubTotal = new Dictionary<String, double>();
            List<string> cartonNumbers = new List<string>();

            var newItems = new List<GarmentPackingListItemViewModel>();
            var newDetails = new List<GarmentPackingListDetailViewModel>();
            foreach (var item in viewModel.Items)
            {
                foreach (var detail in item.Details)
                {
                    newDetails.Add(detail);
                }
            }
            newDetails = newDetails.OrderBy(a => a.Carton1).ToList();

            foreach (var x in viewModel.Items.OrderBy(o => o.RONo))
            {
                if (newItems.Count == 0)
                {
                    newItems.Add(x);
                }
                else
                {
                    if (newItems.Last().RONo == x.RONo && newItems.Last().OrderNo == x.OrderNo)
                    {
                        foreach (var d in x.Details.OrderBy(a => a.Carton1))
                        {
                            newItems.Last().Details.Add(d);
                        }
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
                            Uom = a.Uom
                        }).Single(a => a.RONo == x.RONo && a.OrderNo == x.OrderNo);
                        y.Details = new List<GarmentPackingListDetailViewModel>();
                        foreach (var d in x.Details.OrderBy(a => a.Carton1))
                        {
                            y.Details.Add(d);
                        }
                        newItems.Add(y);
                    }
                }
            }

            //foreach (var d in newDetails)
            //{
            //    if (newItems.Count == 0)
            //    {
            //        var i = viewModel.Items.Single(a => a.Id == d.PackingListItemId);
            //        i.Details = new List<GarmentPackingListDetailViewModel>();
            //        i.Details.Add(d);
            //        newItems.Add(i);
            //    }
            //    else
            //    {
            //        if (newItems.Last().Id == d.PackingListItemId)
            //        {
            //            newItems.Last().Details.Add(d);
            //        }
            //        else
            //        {
            //            var y = viewModel.Items.Select(a=> new GarmentPackingListItemViewModel {
            //                Id =a.Id, RONo=a.RONo, Article=a.Article, BuyerAgent=a.BuyerAgent, ComodityDescription=a.ComodityDescription,
            //                OrderNo=a.OrderNo, AVG_GW=a.AVG_GW, AVG_NW=a.AVG_NW})
            //                .Single(a => a.Id == d.PackingListItemId);
            //            y.Details = new List<GarmentPackingListDetailViewModel>();
            //            y.Details.Add(d);
            //            newItems.Add(y);
            //        }
            //    }
            //}

            document.Add(new Paragraph("SHIPPING METHOD : " + viewModel.ShipmentMode + "\n", normal_font));

            foreach (var item in newItems.OrderBy(a=>a.RONo))
            {
                #region Item

                PdfPTable tableItem = new PdfPTable(6);
                tableItem.SetWidths(new float[] { 2f, 0.2f, 2.8f, 2f, 0.2f, 2.8f });
                PdfPCell cellItemContent = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellItemContent.Phrase = new Phrase("RO No", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(item.RONo, normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase("ARTICLE", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(item.Article, normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase("BUYER", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(viewModel.BuyerAgent.Name, normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase("", normal_font);
                cellItemContent.Phrase = new Phrase("DESCRIPTION OF GOODS", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(item.ComodityDescription, normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase("", normal_font);
                tableItem.AddCell(cellItemContent);
                tableItem.AddCell(cellItemContent);
                tableItem.AddCell(cellItemContent);

                new PdfPCell(tableItem);
                tableItem.ExtendLastRow = false;
                document.Add(tableItem);

                #endregion

                var sizes = new Dictionary<int, string>();
                foreach (var detail in item.Details)
                {
                    foreach (var size in detail.Sizes)
                    {
                        sizes[size.Size.Id] = size.Size.Size;
                    }
                }

                PdfPTable tableDetail = new PdfPTable(SIZES_COUNT + 11);
                var width = new List<float> { 2f, 3.5f, 4f, 4f };
                for (int i = 0; i < SIZES_COUNT; i++) width.Add(1f);
                width.AddRange(new List<float> { 1.5f, 1f, 1.5f, 2f, 1.5f, 1.5f, 1.5f });
                tableDetail.SetWidths(width.ToArray());

                PdfPCell cellDetailLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 20, Padding = 0.5f, Phrase = new Phrase("") };
                tableDetail.AddCell(cellDetailLine);
                tableDetail.AddCell(cellDetailLine);

                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("CARTON NO.", normal_font, 0.75f));
                cellBorderBottomRight.Rowspan = 2;
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("COLOUR", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("STYLE", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("ORDER NO.", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("S I Z E", normal_font, 0.75f));
                cellBorderBottomRight.Colspan = SIZES_COUNT;
                cellBorderBottomRight.Rowspan = 1;
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("CTNS", normal_font, 0.75f));
                cellBorderBottomRight.Colspan = 1;
                cellBorderBottomRight.Rowspan = 2;
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("@", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("QTY", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("SATUAN", normal_font, 0.75f));
                cellBorderBottomRight.Colspan = 1;
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("GW/\nCTN", normal_font, 0.75f));
                cellBorderBottomRight.Rowspan = 2;
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("NW/\nCTN", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("NNW/\nCTN", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Rowspan = 1;

                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(size.Key == 0 ? "" : size.Value, normal_font, 0.5f));
                    cellBorderBottomRight.Rowspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);
                }

                var subCartons = new List<GarmentPackingListDetailViewModel>();
                var subGrossWeight = new List<GarmentPackingListDetailViewModel>();
                var subNetWeight = new List<GarmentPackingListDetailViewModel>();
                var subNetNetWeight = new List<GarmentPackingListDetailViewModel>();

                double subTotal = 0;
                var sizeSumQty = new Dictionary<int, double>();
                foreach (var detail in item.Details)
                {
                    var ctnsQty = detail.CartonQuantity;
                    var grossWeight = detail.GrossWeight;
                    var netWeight = detail.NetWeight;
                    var netNetWeight = detail.NetNetWeight;
                    if (cartonNumbers.Contains($"{detail.Index}-{detail.Carton1}- {detail.Carton2}"))
                    {
                        ctnsQty = 0;
                        grossWeight = 0;
                        netWeight = 0;
                        netNetWeight = 0;
                    }
                    else
                    {
                        cartonNumbers.Add($"{detail.Index}-{detail.Carton1}- {detail.Carton2}");
                    }
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Carton1}- {detail.Carton2}", normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.Colour, normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.Style, normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(item.OrderNo, normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    for (int i = 0; i < SIZES_COUNT; i++)
                    {
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

                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(quantity == 0 ? "" : quantity.ToString(), normal_font, 0.6f));

                        tableDetail.AddCell(cellBorderBottomRight);
                    }
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(ctnsQty.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.QuantityPCS.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    var totalQuantity = (detail.CartonQuantity * detail.QuantityPCS);
                    subTotal += totalQuantity;
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(totalQuantity.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(item.Uom.Unit, normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.GrossWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.NetWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.NetNetWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);

                    if (cartons.FindIndex(c => c.Carton1 == detail.Carton1 && c.Carton2 == detail.Carton2 && c.Index == detail.Index) < 0)
                    {
                        cartons.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = ctnsQty });
                    }
                    if (subCartons.FindIndex(c => c.Carton1 == detail.Carton1 && c.Carton2 == detail.Carton2 && c.Index == detail.Index) < 0)
                    {
                        subCartons.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = ctnsQty });
                        subGrossWeight.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = detail.CartonQuantity, GrossWeight = grossWeight });
                        subNetWeight.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = detail.CartonQuantity, NetWeight = netWeight });
                        subNetNetWeight.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = detail.CartonQuantity, NetNetWeight = netNetWeight });

                    }

                }

                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.5f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.5f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.5f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("SUMMARY", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var size = sizes.OrderBy(a => a.Value).ElementAtOrDefault(i);
                    double quantity = 0;
                    if (size.Key != 0)
                    {
                        quantity = sizeSumQty.Where(w => w.Key == size.Key).Sum(a => a.Value);
                    }

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(quantity == 0 ? "" : quantity.ToString(), normal_font, 0.5f));

                    tableDetail.AddCell(cellBorderBottomRight);
                }
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight); 
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);


                if (!arraySubTotal.ContainsKey(item.Uom.Unit))
                {
                    arraySubTotal.Add(item.Uom.Unit, subTotal);
                }
                else
                {
                    arraySubTotal[item.Uom.Unit] += subTotal;
                }
                grandTotal += subTotal;

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 6,
                    Padding = 5,
                    Phrase = new Phrase("SUB TOTAL ....................................................................................................................................................................... ", normal_font)
                });
                cellBorderBottom.Phrase = new Phrase(subTotal.ToString() + " " + item.Uom.Unit, normal_font);
                cellBorderBottom.Colspan = 2;
                tableDetail.AddCell(cellBorderBottom);
                cellBorderBottom.Phrase = new Phrase("", normal_font);
                cellBorderBottom.Colspan = 3;
                tableDetail.AddCell(cellBorderBottom);
                cellBorderBottom.Colspan = 1;

                var subCtns = subCartons.Sum(c => c.CartonQuantity);
                var subGw = subGrossWeight.Sum(c => c.GrossWeight * c.CartonQuantity);
                var subNw = subNetWeight.Sum(c => c.NetWeight * c.CartonQuantity);
                var subNnw = subNetNetWeight.Sum(c => c.NetNetWeight * c.CartonQuantity);

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 11,
                    Phrase = new Phrase($"      - Sub Ctns = {subCtns}           - Sub G.W. = {String.Format("{0:0.00}", subGw)} Kgs           - Sub N.W. = {String.Format("{0:0.00}", subNw)} Kgs            - Sub N.N.W. = {String.Format("{0:0.00}", subNnw)} Kgs", normal_font)
                });

                new PdfPCell(tableDetail);
                tableDetail.ExtendLastRow = false;
                //tableDetail.KeepTogether = true;
                tableDetail.WidthPercentage = 95f;
                //tableDetail.HeaderRows = 3;
                document.Add(tableDetail);
            }

            #region GrandTotal

            PdfPTable tableGrandTotal = new PdfPTable(2);
            tableGrandTotal.SetWidths(new float[] { 18f + SIZES_COUNT * 1f, 5f });
            PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 2, Padding = 0.5f, Phrase = new Phrase("") };

            tableGrandTotal.AddCell(cellHeaderLine);
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 5,
                Phrase = new Phrase("GRAND TOTAL ...................................................................................................................................................................................", normal_font)
            });
            var grandTotalResult = string.Join(" / ",arraySubTotal.Select(x => x.Value + " " + x.Key).ToArray());
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Phrase = new Phrase(grandTotalResult, normal_font)
            });
            tableGrandTotal.AddCell(cellHeaderLine);
            var totalCtns = cartons.Sum(c => c.CartonQuantity);
            var comodities = viewModel.Items.Select(s => s.Comodity.Name.ToUpper()).Distinct();
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                Colspan = 2,
                Padding = 5,
                Phrase = new Phrase($"{totalCtns} {viewModel.SayUnit} [ {NumberToTextEN.toWords(totalCtns).Trim().ToUpper()} {viewModel.SayUnit} OF {string.Join(" AND ", comodities)}]", normal_font)
            });

            new PdfPCell(tableGrandTotal);
            tableGrandTotal.ExtendLastRow = false;
            tableGrandTotal.WidthPercentage = 95f;
            tableGrandTotal.SpacingAfter = 5f;
            document.Add(tableGrandTotal);

            #endregion

            #region Mark

            PdfPTable tableMark = new PdfPTable(2);
            tableMark.SetWidths(new float[] { 1f, 1f });

            PdfPCell cellShippingMark = new PdfPCell() { Border = Rectangle.NO_BORDER };
            Chunk chunkShippingMark = new Chunk("SHIPPING MARKS", normal_font);
            chunkShippingMark.SetUnderline(0.5f, -1);
            Phrase phraseShippingMark = new Phrase();
            phraseShippingMark.Add(chunkShippingMark);
            phraseShippingMark.Add(new Chunk("     :", normal_font));
            cellShippingMark.AddElement(phraseShippingMark);
            cellShippingMark.AddElement(new Paragraph(viewModel.ShippingMark, normal_font));
            tableMark.AddCell(cellShippingMark);

            PdfPCell cellSideMark = new PdfPCell() { Border = Rectangle.NO_BORDER };
            Chunk chunkSideMark = new Chunk("SIDE MARKS", normal_font);
            chunkSideMark.SetUnderline(0.5f, -1);
            Phrase phraseSideMark = new Phrase();
            phraseSideMark.Add(chunkSideMark);
            phraseSideMark.Add(new Chunk("     :", normal_font));
            cellSideMark.AddElement(phraseSideMark);
            cellSideMark.AddElement(new Paragraph(viewModel.SideMark, normal_font) { });
            tableMark.AddCell(cellSideMark);
            var noImage = "data:image/png;base64,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";
            byte[] shippingMarkImage;
            if (String.IsNullOrEmpty(viewModel.ShippingMarkImageFile))
            {
                viewModel.ShippingMarkImageFile = noImage;    
            }

            if (IsBase64String(Base64.GetBase64File(viewModel.ShippingMarkImageFile)))
            {
                shippingMarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.ShippingMarkImageFile));

                Image shipMarkImage = Image.GetInstance(imgb: shippingMarkImage);
                if (shipMarkImage.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 100 / shipMarkImage.Width;
                    shipMarkImage.ScalePercent(percentage * 100);
                    PdfPCell shipMarkImageCell = new PdfPCell(shipMarkImage);
                    shipMarkImageCell.Border = Rectangle.NO_BORDER;
                    tableMark.AddCell(shipMarkImageCell);
                }
            }

            byte[] sideMarkImage;
            if (String.IsNullOrEmpty(viewModel.SideMarkImageFile))
            {
                viewModel.SideMarkImageFile = noImage;
            }

            if (IsBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile)))
            {
                sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.SideMarkImageFile));
                Image _sideMarkImage = Image.GetInstance(imgb: sideMarkImage);
                if (_sideMarkImage.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 100 / _sideMarkImage.Width;
                    _sideMarkImage.ScalePercent(percentage * 100);
                }

                PdfPCell _sideMarkImageCell = new PdfPCell(_sideMarkImage);
                _sideMarkImageCell.Border = Rectangle.NO_BORDER;
                tableMark.AddCell(_sideMarkImageCell);
            }

            new PdfPCell(tableMark);
            tableMark.ExtendLastRow = false;
            tableMark.SpacingAfter = 5f;
            document.Add(tableMark);

            #endregion

            #region Measurement

            PdfPTable tableMeasurement = new PdfPTable(3);
            tableMeasurement.SetWidths(new float[] { 2f, 0.2f, 12f });
            PdfPCell cellMeasurement = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellMeasurement.Phrase = new Phrase("GROSS WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(viewModel.GrossWeight + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(viewModel.NettWeight + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(viewModel.NetNetWeight + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("MEASUREMENT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            PdfPTable tableMeasurementDetail = new PdfPTable(5);
            tableMeasurementDetail.SetWidths(new float[] { 1f, 1f, 1f, 1.5f, 2f });
            PdfPCell cellMeasurementDetail = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            decimal totalCbm = 0;
            foreach (var measurement in viewModel.Measurements)
            {
                cellMeasurementDetail.Phrase = new Phrase(measurement.Length + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Width + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Height + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.CartonsQuantity + " CTNS = ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                var cbm = (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000;
                totalCbm += cbm;
                cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N3} CBM", cbm), normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
            }

            cellMeasurementDetail.Border = Rectangle.TOP_BORDER;
            cellMeasurementDetail.Phrase = new Phrase("", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase("TOTAL", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase(viewModel.Measurements.Sum(m => m.CartonsQuantity) + " CTNS .", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N3} CBM", totalCbm), normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);

            new PdfPCell(tableMeasurementDetail);
            tableMeasurementDetail.ExtendLastRow = false;
            var paddingRight = SIZES_COUNT > 11 ? 400 : 200;
            tableMeasurement.AddCell(new PdfPCell(tableMeasurementDetail) { Border = Rectangle.NO_BORDER, PaddingRight = paddingRight });

            tableMeasurement.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                Colspan = 3,
                Phrase = new Phrase("REMARK :", normal_font_underlined)
            });
            tableMeasurement.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                Colspan = 3,
                Phrase = new Phrase(viewModel.Remark, normal_font)
            });
            byte[] remarkImage;
            if (String.IsNullOrEmpty(viewModel.RemarkImageFile))
            {
                viewModel.RemarkImageFile = noImage;
            }

            if (IsBase64String(Base64.GetBase64File(viewModel.RemarkImageFile)))
            {
                remarkImage = Convert.FromBase64String(Base64.GetBase64File(viewModel.RemarkImageFile));

                Image images = Image.GetInstance(imgb: remarkImage);

                if (images.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 100 / images.Width;
                    images.ScalePercent(percentage * 100);
                }

                PdfPCell imageCell = new PdfPCell(images);
                imageCell.Border = Rectangle.NO_BORDER;
                imageCell.Colspan = 3;
                tableMeasurement.AddCell(imageCell);
            }

            new PdfPCell(tableMeasurement);
            tableMeasurement.ExtendLastRow = false;
            tableMeasurement.SpacingAfter = 5f;
            document.Add(tableMeasurement);
            //document.Add(images);

            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        Chunk GetScalledChunk(string text, Font font, float scalling = 0.5f)
        {
            Chunk chunk = new Chunk(text, font);
            chunk.SetHorizontalScaling(scalling);
            return chunk;
        }

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }

    class GarmentPackingListDraftPDFTemplatePageEvent : PdfPageEventHelper
    {
        private IIdentityProvider identityProvider;
        private GarmentPackingListViewModel viewModel;

        public GarmentPackingListDraftPDFTemplatePageEvent(IIdentityProvider identityProvider, GarmentPackingListViewModel viewModel)
        {
            this.identityProvider = identityProvider;
            this.viewModel = viewModel;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin, marginTop = document.TopMargin, marginRight = document.RightMargin, marginBottom = document.BottomMargin;

            #region TITLE

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 16);

            var titleY = height - marginTop + 25;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "DRAFT PACKING LIST", width / 2, titleY, 0);

            #endregion

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region INFO

            var infoY = height - marginTop + 5;

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Invoice No. : " + viewModel.InvoiceNo, marginLeft, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Date : " + viewModel.CreatedUtc.ToString("MMM dd, yyyy."), width / 2, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page : " + writer.PageNumber, width - marginRight, infoY, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop);
            cb.LineTo(width - marginRight, height - marginTop);
            cb.Stroke();

            #endregion

            #region SIGNATURE

            var printY = marginBottom - 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Waktu Cetak : " + DateTimeOffset.Now.ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy H:mm:ss zzz"), marginLeft, printY, 0);

            #endregion

            cb.EndText();
        }

       
    }

}
