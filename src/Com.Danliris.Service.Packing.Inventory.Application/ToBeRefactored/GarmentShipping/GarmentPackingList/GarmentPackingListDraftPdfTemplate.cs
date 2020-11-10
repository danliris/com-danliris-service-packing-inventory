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

            Font header_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(maxSizesCount > 11 ? PageSize.A4.Rotate() : PageSize.A4, 20, 20, 70, 30);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentPackingListDraftPDFTemplatePageEvent(_identityProvider, viewModel);

            document.Open();

            PdfPCell cellBorderBottomRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBorderBottom = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            var cartons = new List<GarmentPackingListDetailViewModel>();
            double grandTotal = 0;
            foreach (var item in viewModel.Items)
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
                cellItemContent.Phrase = new Phrase("DESCRIPTION OF GOODS", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(":", normal_font);
                tableItem.AddCell(cellItemContent);
                cellItemContent.Phrase = new Phrase(item.Description, normal_font);
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

                PdfPTable tableDetail = new PdfPTable(SIZES_COUNT + 8);
                var width = new List<float> { 3f, 3f, 2f, 4f };
                for (int i = 0; i < SIZES_COUNT; i++) width.Add(1f);
                width.AddRange(new List<float> { 2f, 1f, 3f, 3f });
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
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("@\nPCS", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("QUANTITY\nPCS", normal_font, 0.75f));
                tableDetail.AddCell(cellBorderBottomRight);
                cellBorderBottom.Phrase = new Phrase(GetScalledChunk("TOTAL", normal_font, 0.75f));
                cellBorderBottom.Rowspan = 2;
                tableDetail.AddCell(cellBorderBottom);
                cellBorderBottom.Rowspan = 1;

                for (int i = 0; i < SIZES_COUNT; i++)
                {
                    var size = sizes.ElementAtOrDefault(i);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(size.Key == 0 ? "" : size.Value, normal_font, 0.5f));
                    cellBorderBottomRight.Rowspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);
                }

                double subCtns = 0;
                double subTotal = 0;
                foreach (var detail in item.Details)
                {
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Carton1}- {detail.Carton2}", normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.Colour, normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.Style, normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(item.OrderNo, normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    for (int i = 0; i < SIZES_COUNT; i++)
                    {
                        var size = sizes.ElementAtOrDefault(i);
                        double quantity = 0;
                        if (size.Key != 0)
                        {
                            quantity = detail.Sizes.Where(w => w.Size.Id == size.Key).Sum(s => s.Quantity);
                        }

                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(quantity == 0 ? "" : quantity.ToString(), normal_font, 0.5f));

                        tableDetail.AddCell(cellBorderBottomRight);
                    }
                    subCtns += detail.CartonQuantity;
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.CartonQuantity.ToString(), normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.QuantityPCS.ToString(), normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    var totalQuantity = (detail.CartonQuantity * detail.QuantityPCS);
                    subTotal += totalQuantity;
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(totalQuantity.ToString(), normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottom.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottom);

                    if (cartons.FindIndex(c => c.Carton1 == detail.Carton1 && c.Carton2 == detail.Carton2) < 0)
                    {
                        cartons.Add(new GarmentPackingListDetailViewModel { Carton1 = detail.Carton1, Carton2 = detail.Carton2, CartonQuantity = detail.CartonQuantity });
                    }
                }
                grandTotal += subTotal;

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 7,
                    Padding = 5,
                    Phrase = new Phrase("SUB TOTAL ................................................................................. ", normal_font)
                });
                cellBorderBottom.Phrase = new Phrase(subTotal.ToString(), normal_font);
                tableDetail.AddCell(cellBorderBottom);

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = SIZES_COUNT + 8,
                    Phrase = new Phrase($"      - Sub Ctns = {subCtns}       - Sub G.W. = {item.AVG_GW}      - Sub N.W. = {item.AVG_NW}", normal_font)
                });

                new PdfPCell(tableDetail);
                tableDetail.ExtendLastRow = false;
                tableDetail.KeepTogether = true;
                tableDetail.WidthPercentage = 95f;
                document.Add(tableDetail);
            }

            #region GrandTotal

            PdfPTable tableGrandTotal = new PdfPTable(2);
            tableGrandTotal.SetWidths(new float[] { 18f + SIZES_COUNT * 1f, 3f });
            PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 2, Padding = 0.5f, Phrase = new Phrase("") };

            tableGrandTotal.AddCell(cellHeaderLine);
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 5,
                Phrase = new Phrase("GRAND TOTAL ...............................................................................", normal_font)
            });
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Phrase = new Phrase(grandTotal.ToString(), normal_font)
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

            new PdfPCell(tableMark);
            tableMark.ExtendLastRow = false;
            tableMark.SpacingAfter = 5f;
            document.Add(tableMark);

            #endregion

            #region Measurement

            PdfPTable tableMeasurement = new PdfPTable(3);
            tableMeasurement.SetWidths(new float[] { 2f, 0.2f, 7.8f });
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
                cellMeasurementDetail.Phrase = new Phrase(measurement.Length + " X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Width + " X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Height + " X ", normal_font);
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
            tableMeasurement.AddCell(new PdfPCell(tableMeasurementDetail) { Border = Rectangle.NO_BORDER, PaddingRight = 100 });

            tableMeasurement.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                Colspan = 3,
                Phrase = new Phrase(viewModel.Remark, normal_font)
            });

            new PdfPCell(tableMeasurement);
            tableMeasurement.ExtendLastRow = false;
            tableMeasurement.SpacingAfter = 5f;
            document.Add(tableMeasurement);

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

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region INFO

            var infoY = height - marginTop + 5;

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Invoice No. : " + viewModel.InvoiceNo, marginLeft, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Date : " + viewModel.Date.GetValueOrDefault().ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("MMM dd, yyyy."), width / 2, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page : " + writer.PageNumber, width - marginRight, infoY, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop);
            cb.LineTo(width - marginRight, height - marginTop);
            cb.Stroke();

            #endregion

            #region SIGNATURE

            var printY = marginBottom - 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Waktu Cetak : " + new DateTimeOffset(DateTime.Now, new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy H:mm:ss zzz"), marginLeft, printY, 0);

            #endregion

            cb.EndText();
        }
    }
}
