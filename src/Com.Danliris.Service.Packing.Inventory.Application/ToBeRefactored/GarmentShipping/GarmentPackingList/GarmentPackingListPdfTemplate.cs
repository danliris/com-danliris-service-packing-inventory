using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
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
    public class GarmentPackingListPdfTemplate
    {
        private const int SIZES_COUNT = 11;
        private IIdentityProvider _identityProvider;

        public GarmentPackingListPdfTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GeneratePdfTemplate(GarmentPackingListViewModel viewModel, string fob)
        {
            Font header_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, 20, 20, 170, 170);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentPackingListPDFTemplatePageEvent(_identityProvider, viewModel);

            document.Open();

            #region Description

            PdfPTable tableDescription = new PdfPTable(3);
            tableDescription.SetWidths(new float[] { 2f, 0.2f, 7.8f });
            PdfPCell cellDescription = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescription.Phrase = new Phrase("FOB", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(fob, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("LC No.", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.LCNo, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("ISSUED BY", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.IssuedBy, normal_font);
            tableDescription.AddCell(cellDescription);

            new PdfPCell(tableDescription);
            tableDescription.ExtendLastRow = false;
            tableDescription.SpacingAfter = 5f;
            document.Add(tableDescription);

            #endregion

            PdfPCell cellBorderBottomRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBorderBottom = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            double totalCtns = 0;
            double grandTotal = 0;
            foreach (var item in viewModel.Items)
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
                cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("ART. NO.", normal_font, 0.75f));
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
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(item.Article, normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(item.OrderNo, normal_font, 0.5f));
                    tableDetail.AddCell(cellBorderBottomRight);
                    for (int i = 0; i < SIZES_COUNT; i++)
                    {
                        var size = sizes.ElementAtOrDefault(i);
                        if (size.Key != 0)
                        {
                            cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk(detail.Sizes.Where(w => w.Size.Id == size.Key).Select(s => s.Quantity.ToString()).FirstOrDefault() ?? "", normal_font, 0.5f));
                        }
                        else
                        {
                            cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("", normal_font, 0.5f));
                        }
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
                }
                totalCtns += subCtns;
                grandTotal += subTotal;

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = 18,
                    Padding = 5,
                    Phrase = new Phrase("SUB TOTAL ................................................................................. ", normal_font)
                });
                cellBorderBottom.Phrase = new Phrase(subTotal.ToString(), normal_font);
                tableDetail.AddCell(cellBorderBottom);

                tableDetail.AddCell(new PdfPCell()
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    Colspan = 19,
                    Phrase = new Phrase($"      - Sub Ctns = {subCtns}       - Sub G.W. = {item.AVG_GW}      - Sub G.N. = {item.AVG_NW}", normal_font)
                });

                new PdfPCell(tableDetail);
                tableDetail.ExtendLastRow = false;
                tableDetail.KeepTogether = true;
                tableDetail.WidthPercentage = 95f;
                document.Add(tableDetail);
            }

            #region GrandTotal

            PdfPTable tableGrandTotal = new PdfPTable(1);
            tableGrandTotal.SetWidths(new float[] { 1f });
            PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 3, Padding = 0.5f, Phrase = new Phrase("") };

            tableGrandTotal.AddCell(cellHeaderLine);
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                Padding = 5,
                Phrase = new Phrase($"GRAND TOTAL ...............................................................................        {grandTotal.ToString()}", normal_font)
            });
            tableGrandTotal.AddCell(cellHeaderLine);
            var comodities = viewModel.Items.Select(s => s.Comodity.Name.ToUpper());
            tableGrandTotal.AddCell(new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                Padding = 5,
                Phrase = new Phrase($"{totalCtns} CTNS [ {NumberToTextEN.toWords(totalCtns).Trim().ToUpper()} CARTOONS OF {string.Join(" AND ", comodities)}]", normal_font)
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
            PdfPCell cellMeasurementDetail = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Rectangle.ALIGN_RIGHT };
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
                cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N2} CBM", (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000), normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
            }
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

    class GarmentPackingListPDFTemplatePageEvent : PdfPageEventHelper
    {
        private IIdentityProvider identityProvider;
        private GarmentPackingListViewModel viewModel;

        public GarmentPackingListPDFTemplatePageEvent(IIdentityProvider identityProvider, GarmentPackingListViewModel viewModel)
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

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region REF

            var refY = height - marginTop + 25;
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Ref. No. : FM-00-SP-24-005", width - marginRight, refY, 0);

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

            #region REF

            var signX = width - 140;
            var signY = marginBottom - 80;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "( MRS. ADRIYANA DAMAYANTI )", signX, signY, 0);
            cb.MoveTo(signX - 55, signY - 2);
            cb.LineTo(signX + 55, signY - 2);
            cb.Stroke();
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "AUTHORIZED SIGNATURE", signX, signY - 10, 0);

            #endregion

            cb.EndText();
        }
    }
}
