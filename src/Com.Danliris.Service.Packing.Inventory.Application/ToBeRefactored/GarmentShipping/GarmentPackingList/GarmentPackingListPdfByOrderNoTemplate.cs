using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListPdfByOrderNoTemplate
    {
        private IIdentityProvider _identityProvider;

        public GarmentPackingListPdfByOrderNoTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GeneratePdfTemplate(GarmentPackingListViewModel viewModel, string fob, string cprice)
        {
            //int maxSizesCount = viewModel.Items.Max(i => i.Details.Max(d => d.Sizes.GroupBy(g => g.Size.Id).Count()));
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
                    if (newItems2.Last().OrderNo == item.OrderNo && newItems2.Last().Description == item.Description)
                    {
                        foreach (var d in item.Details.OrderBy(a => a.Carton1))
                        {
                            var x = newItems.Last().Details.FirstOrDefault(w => w.Id == d.Id);
                            if(x != null)
                            {
                                if (x.Index == d.Index && x.Carton1 == d.Carton1 && x.Carton2 == d.Carton2)
                                {
                                    newItems2.Add(item);
                                }
                                else
                                {
                                    newItems2.Last().Details.Add(d);
                                }
                            } else
                            {
                                newItems2.Last().Details.Add(d);
                            }
                            
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

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(sizesCount ? PageSize.A4.Rotate() : PageSize.A4, 20, 20, 170, 30);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentPackingListPdfByOrderNoPageEvent(_identityProvider, viewModel);

            document.Open();

            #region Description

            PdfPTable tableDescription = new PdfPTable(3);
            tableDescription.SetWidths(new float[] { 2f, 0.2f, 7.8f });
            PdfPCell cellDescription = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescription.Phrase = new Phrase(cprice, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(fob, normal_font);
            tableDescription.AddCell(cellDescription);
            if (viewModel.PaymentTerm == "LC")
            {
                cellDescription.Phrase = new Phrase("LC No.", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(":", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(viewModel.LCNo, normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase("Tgl. LC", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(":", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(viewModel.LCDate.GetValueOrDefault().ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy"), normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase("ISSUED BY", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(":", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(viewModel.IssuedBy, normal_font);
                tableDescription.AddCell(cellDescription);
            }
            else
            {
                cellDescription.Phrase = new Phrase("Payment Term", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(":", normal_font);
                tableDescription.AddCell(cellDescription);
                cellDescription.Phrase = new Phrase(viewModel.PaymentTerm, normal_font);
                tableDescription.AddCell(cellDescription);
            }

            new PdfPCell(tableDescription);
            tableDescription.ExtendLastRow = false;
            tableDescription.SpacingAfter = 5f;
            document.Add(tableDescription);

            #endregion

            PdfPCell cellBorderBottomRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBorderBottom = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            double totalCtns = 0;
            double grandTotal = 0;
            var uom = "";
            var arrayGrandTotal = new Dictionary<String, double>();
            List<string> cartonNumbers = new List<string>();

            

            //foreach (var x in viewModel.Items.OrderBy(o => o.RONo))
            //{
            //    if (newItems.Count == 0)
            //    {
            //        newItems.Add(x);
            //    }
            //    else
            //    {
            //        if (newItems.Last().OrderNo == x.OrderNo && newItems.Last().Description == x.Description)
            //        {
            //            foreach (var d in x.Details.OrderBy(a => a.Carton1))
            //            {
            //                newItems.Last().Details.Add(d);
            //            }
            //        }
            //        else
            //        {
            //            var y = viewModel.Items.Select(a => new GarmentPackingListItemViewModel
            //            {
            //                Id = a.Id,
            //                RONo = a.RONo,
            //                Description = a.Description,
            //                Article = a.Article,
            //                BuyerAgent = a.BuyerAgent,
            //                ComodityDescription = a.ComodityDescription,
            //                OrderNo = a.OrderNo,
            //                AVG_GW = a.AVG_GW,
            //                AVG_NW = a.AVG_NW,
            //                Uom = a.Uom
            //            })
            //                .Single(a => a.OrderNo == x.OrderNo && a.Description == x.Description);
            //            y.Details = new List<GarmentPackingListDetailViewModel>();
            //            foreach (var d in x.Details.OrderBy(a => a.Carton1))
            //            {
            //                y.Details.Add(d);
            //            }
            //            newItems.Add(y);
            //        }
            //    }
            //}

            foreach (var item in newItems2)
            {
                #region Item

                PdfPTable tableItem = new PdfPTable(3);
                tableItem.SetWidths(new float[] { 2f, 0.2f, 7.8f });
                PdfPCell cellItemContent = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellItemContent.Phrase = new Phrase("DESCRIPTION OF GOODS", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(item.Description, normal_font);
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

                PdfPCell cellDetailLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 19, Padding = 0.5f, Phrase = new Phrase("") };
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

                double subCtns = 0;
                double subTotal = 0;
                var sizeSumQty = new Dictionary<int, double>();

                var arraySubTotal = new Dictionary<String, double>();
                foreach (var detail in item.Details.OrderBy(a => a.Carton1).ThenBy(a => a.Carton2))
                {
                    var ctnsQty = detail.CartonQuantity;

                    var article = viewModel.Items.Where(a => a.Id == detail.PackingListItemId).Single().Article;
                    uom = viewModel.Items.Where(a => a.Id == detail.PackingListItemId).Single().Uom.Unit;
                    if (cartonNumbers.Contains($"{detail.Index} - {detail.Carton1}- {detail.Carton2}"))
                    {
                        ctnsQty = 0;
                    }
                    else
                    {
                        cartonNumbers.Add($"{detail.Index} - {detail.Carton1}- {detail.Carton2}");
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
                    subCtns += ctnsQty;
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(ctnsQty.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.QuantityPCS.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    var totalQuantity = (detail.CartonQuantity * detail.QuantityPCS);
                    subTotal += totalQuantity;
                    if (!arraySubTotal.ContainsKey(uom))
                    {
                        arraySubTotal.Add(uom, totalQuantity);
                    }
                    else
                    {
                        arraySubTotal[uom] += totalQuantity;
                    }
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(totalQuantity.ToString(), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(uom, normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.GrossWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.NetWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(string.Format("{0:n2}", detail.NetNetWeight), normal_font, 0.6f));
                    tableDetail.AddCell(cellBorderBottomRight);
                }

                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.6f));
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

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(quantity == 0 ? "" : quantity.ToString(), normal_font, 0.6f));

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

                totalCtns += subCtns;
                grandTotal += subTotal;
                if (!arrayGrandTotal.ContainsKey(uom))
                {
                    arrayGrandTotal.Add(uom, subTotal);
                }
                else
                {
                    arrayGrandTotal[uom] += subTotal;
                }

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 7,
                    Padding = 5,
                    Phrase = new Phrase("SUB TOTAL ....................................................................................................................................................................... ", normal_font)
                });

                var subTotalResult = string.Join(" / ", arraySubTotal.Select(x => x.Value + " " + x.Key).ToArray());
                cellBorderBottom.Phrase = new Phrase(subTotalResult, normal_font);
                cellBorderBottom.Colspan = 2;
                tableDetail.AddCell(cellBorderBottom);
                cellBorderBottom.Phrase = new Phrase("", normal_font);
                cellBorderBottom.Colspan = 3;
                tableDetail.AddCell(cellBorderBottom);
                cellBorderBottom.Colspan = 1;

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 11,
                    Phrase = new Phrase($"      - Sub Ctns = {subCtns}           - Sub G.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.GrossWeight * a.CartonQuantity))} Kgs           - Sub N.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.NetWeight * a.CartonQuantity))} Kgs            - Sub N.N.W. = {String.Format("{0:0.00}", item.Details.Sum(a => a.NetNetWeight * a.CartonQuantity))} Kgs", normal_font)
                });

                new PdfPCell(tableDetail);
                tableDetail.ExtendLastRow = false;
                tableDetail.HeaderRows = 1;
                //tableDetail.KeepTogether = true;
                tableDetail.WidthPercentage = 95f;
                tableDetail.SpacingAfter = 10f;
                document.Add(tableDetail);
            }

            #region GrandTotal

            PdfPTable tableGrandTotal = new PdfPTable(2);
            tableGrandTotal.SetWidths(new float[] { 18f + SIZES_COUNT * 1f, 3f });
            PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 2, Padding = 0.5f, Phrase = new Phrase("") };

            var grandTotalResult = string.Join(" / ", arrayGrandTotal.Select(x => x.Value + " " + x.Key).ToArray());

            tableGrandTotal.AddCell(cellHeaderLine);
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 6,
                Phrase = new Phrase("GRAND TOTAL ...................................................................................................................................................................................", normal_font)
            });
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 4,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Phrase = new Phrase(grandTotalResult, normal_font)
            });
            tableGrandTotal.AddCell(cellHeaderLine);
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

            byte[] shippingMarkImage;
            var noImage = "data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAA0NDQ0ODQ4QEA4UFhMWFB4bGRkbHi0gIiAiIC1EKjIqKjIqRDxJOzc7STxsVUtLVWx9aWNpfZeHh5e+tb75+f8BDQ0NDQ4NDhAQDhQWExYUHhsZGRseLSAiICIgLUQqMioqMipEPEk7NztJPGxVS0tVbH1pY2l9l4eHl761vvn5///CABEIAAoACgMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAAB//aAAgBAQAAAACnD//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIQAAAAf//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMQAAAAf//EABQQAQAAAAAAAAAAAAAAAAAAACD/2gAIAQEAAT8AH//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIBAT8Af//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMBAT8Af//Z";
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
                }

                PdfPCell shipMarkImageCell = new PdfPCell(shipMarkImage);
                shipMarkImageCell.Border = Rectangle.NO_BORDER;
                tableMark.AddCell(shipMarkImageCell);
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
            cellMeasurement.Phrase = new Phrase(String.Format("{0:0.00}",viewModel.GrossWeight) + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(String.Format("{0:0.00}",viewModel.NettWeight) + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(String.Format("{0:0.00}",viewModel.NetNetWeight) + " KGS", normal_font);
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
                cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N2} CBM", cbm), normal_font);
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
            cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N2} CBM", totalCbm), normal_font);
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

            #endregion

            #region sign
            PdfPTable tableSign = new PdfPTable(3);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };


            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("( MRS. ADRIYANA DAMAYANTI )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("AUTHORIZED SIGNATURE", normal_font_underlined);
            tableSign.AddCell(cellBodySignNoBorder);

            document.Add(tableSign);
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

    class GarmentPackingListPdfByOrderNoPageEvent : PdfPageEventHelper
    {
        private IIdentityProvider identityProvider;
        private GarmentPackingListViewModel viewModel;

        public GarmentPackingListPdfByOrderNoPageEvent(IIdentityProvider identityProvider, GarmentPackingListViewModel viewModel)
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

            int maxSizesCount = viewModel.Items.Max(i => i.Details.Max(d => d.Sizes.GroupBy(g => g.Size.Id).Count()));

            if (maxSizesCount > 11)
            {
                cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 6);

                #region LEFT

                var logoY = height - marginTop + 65;

                byte[] imageByteDL = Convert.FromBase64String(Base64ImageStrings.LOGO_DANLIRIS_211_200_BW);
                Image imageDL = Image.GetInstance(imageByteDL);
                imageDL.ScaleAbsolute(60f, 60f);
                var newColor = System.Drawing.Color.Red;
                imageDL.SetAbsolutePosition(marginLeft, logoY);
                cb.AddImage(imageDL, inlineImage: true);

                #endregion

                #region CENTER

                var headOfficeX = marginLeft + 75;
                var headOfficeY = height - marginTop + 105;

                byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
                Image image = Image.GetInstance(imageByte);
                if (image.Width > 160)
                {
                    float percentage = 0.0f;
                    percentage = 160 / image.Width;
                    image.ScalePercent(percentage * 100);
                }
                image.SetAbsolutePosition(headOfficeX, headOfficeY);
                cb.AddImage(image, inlineImage: true);

                string[] headOffices = {
                    "Head Office : Kelurahan Banaran, Kecamatan Grogol,",
                    "Sukoharjo - Indonesia",
                    "PO BOX 166 Solo 57100",
                    "Telp. (62 271) 740888, 714400 (HUNTING)",
                    "Fax. (62 271) 735222, 740777",
                    "Website : www.danliris.com",
                };
                for (int i = 0; i < headOffices.Length; i++)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, headOffices[i], headOfficeX, headOfficeY + 10 - image.ScaledHeight - (i * 6), 0);
                }

                #endregion

                #region RIGHT

                byte[] imageByteIso = Convert.FromBase64String(Base64ImageStrings.ISO);
                Image imageIso = Image.GetInstance(imageByteIso);
                if (imageIso.Width > 80)
                {
                    float percentage = 0.0f;
                    percentage = 80 / imageIso.Width;
                    imageIso.ScalePercent(percentage * 100);
                }
                imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 120);
                cb.AddImage(imageIso, inlineImage: true);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight, height - imageIso.ScaledHeight - marginTop + 120 - 5, 0);

                #endregion

                #region LINE

                cb.MoveTo(marginLeft, height - marginTop + 50);
                cb.LineTo(width - marginRight, height - marginTop + 50);
                cb.Stroke();

                #endregion

                cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 16);

                #region TITLE

                var titleY = height - marginTop + 30;
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "PACKING LIST", width / 2, titleY, 0);

                #endregion
            }

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region REF

            var refY = height - marginTop + 25;
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Ref. No. : FM-00-SP-24-005", width - marginRight, refY, 0);

            #endregion

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region INFO

            var infoY = height - marginTop + 10;

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Invoice No. : " + viewModel.InvoiceNo, marginLeft, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Date : " + viewModel.Date.GetValueOrDefault().ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("MMM dd, yyyy."), width / 2, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page : " + writer.PageNumber, width - marginRight, infoY, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop + 5);
            cb.LineTo(width - marginRight, height - marginTop + 5);
            cb.Stroke();

            #endregion

            #region PRINTED

            var printY = marginBottom - 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Waktu Cetak : " + DateTimeOffset.Now.ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy H:mm:ss zzz"), marginLeft, printY, 0);

            #endregion

            #region SIGNATURE

            //var signX = width - 140;
            //var signY = marginBottom - 80;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "( MRS. ADRIYANA DAMAYANTI )", signX, signY, 0);
            //cb.MoveTo(signX - 55, signY - 2);
            //cb.LineTo(signX + 55, signY - 2);
            //cb.Stroke();
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "AUTHORIZED SIGNATURE", signX, signY - 10, 0);

            #endregion

            cb.EndText();
        }
    }
}   
