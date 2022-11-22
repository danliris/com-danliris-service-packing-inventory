using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingPdfTemplate
    {
        public string TITLE = "PT DAN LIRIS";
        public string ADDRESS = "BANARAN, GROGOL, SUKOHARJO";
        public string DOCUMENT_TITLE = "BON PENGIRIMAN BARANG";
        public string ISO = "FM.FP-GJ-15-005";

        public Font HEADER_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
        public Font HEADER_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
        public Font SUBHEADER_FONT_BOLD_UNDERLINED = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12, Font.UNDERLINE);
        public Font TEXT_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        public Font TEXT_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

        private readonly PdfPTable Title;
        private readonly PdfPTable AddressTitle;
        private readonly PdfPTable DocumentTitle;
        private readonly PdfPTable DocumentISO;
        private readonly PdfPTable DocumentInfo;
        private readonly PdfPTable GroupDetailInfo;
        private readonly PdfPTable DetailInfo;
        private readonly PdfPTable NettoSection;
        private readonly PdfPTable SignatureSection;

        List<string> bodyTableColumns = new List<string> { "No", "KONSTRUKSI", "NAMA BARANG", "MOTIF", "KET", "GRADE 1", "GRADE 2", "NO SP", "WARNA", "QTY PACKING", "PACKING", "YARD", "METER", "KG" };
        List<string> groupTableColumns = new List<string> { "No", "KONSTRUKSI", "METER" };

        public OutputShippingPdfTemplate(OutputShippingViewModel model, int timeoffset)
        {
            Title = GetTitle();
            AddressTitle = GetAddressTitle();
            DocumentTitle = GetDocumentTitle();
            DocumentISO = GetISO();
            DocumentInfo = GetBuyerInfo(model, timeoffset);
            GroupDetailInfo = GetGroupDetailInfo(model);
            DetailInfo = GetDetailInfo(model);
            NettoSection = GetNettoSection();
            SignatureSection = GetSignatureSection(model, timeoffset);
        }

        public MemoryStream GeneratePdfTemplate()
        {
            const int MARGIN = 20;


            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region Header
            document.Add(Title);
            document.Add(AddressTitle);
            document.Add(DocumentTitle);
            document.Add(DocumentISO);
            document.Add(DocumentInfo);
            document.Add(GroupDetailInfo);
            document.Add(DetailInfo);
            //document.Add(NettoSection);
            document.Add(SignatureSection);
            #endregion Header

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        private PdfPTable GetTitle()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };
            cell.Phrase = new Phrase(TITLE, HEADER_FONT_BOLD);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetAddressTitle()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.BOTTOM_BORDER,
                BorderWidth = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };
            cell.Phrase = new Phrase(ADDRESS, HEADER_FONT);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetDocumentTitle()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };
            cell.Phrase = new Phrase(DOCUMENT_TITLE, SUBHEADER_FONT_BOLD_UNDERLINED);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetISO()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 1f
            };
            cell.Phrase = new Phrase(ISO, TEXT_FONT_BOLD);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetBuyerInfo(OutputShippingViewModel model, int timeoffset)
        {
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 1f, 1f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 2f
            };


            cell.Phrase = new Phrase("Kepada Yth. Bagian Penjualan", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"NO. : {model.BonNo}", TEXT_FONT);
            table.AddCell(cell);

            string buyerName = model.ShippingProductionOrders.FirstOrDefault()?.DeliveryOrder.Name;
            cell.Phrase = new Phrase($"U/ dikirim kepada: {buyerName}", TEXT_FONT);
            //string buyerName = model.ShippingProductionOrders.FirstOrDefault()?.Buyer;
            //cell.Phrase = new Phrase($"U/ dikirim kepada: {buyerName}", TEXT_FONT);
            //string productionNo = model.ShippingProductionOrders.FirstOrDefault()?.ProductionOrder.No;
            //cell.Phrase = new Phrase($"U/ dikirim kepada: {productionNo}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"Sesuai DO. NO. : {model.DeliveryOrder.No}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Keterangan : Di Ball / Lose Packing / Karton", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase($"Tanggal : { model.Date.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Netto: ...... Kg Bruto: ......Kg", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase($"Jumlah Baris : { model.ShippingProductionOrders.Count }", TEXT_FONT);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetGroupDetailInfo(OutputShippingViewModel model)
        {
            PdfPTable container = new PdfPTable(2)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 1f };
            container.SetWidths(widths);

            PdfPCell cellContainer = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER
            };

            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100,

            };

            float[] tableWidths = new float[] { 1f, 5f, 2f };
            table.SetWidths(tableWidths);

            PdfPCell cellHeader = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            PdfPCell cellRight = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };


            foreach (var column in groupTableColumns)
            {
                cellHeader.Phrase = new Phrase(column, TEXT_FONT_BOLD);
                table.AddCell(cellHeader);
            }

            double lengthTotal = 0;
            int index = 1;
            var detailGroup = model.ShippingProductionOrders.GroupBy(s => s.Construction);
            foreach (var detail in detailGroup)
            {
                cellHeader.Phrase = new Phrase(index++.ToString(), TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.Key, TEXT_FONT);
                table.AddCell(cellHeader);

                cellRight.Phrase = new Phrase(detail.Sum(s => s.Qty).ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                lengthTotal += detail.Sum(s => s.Qty);

            }

            cellHeader.Phrase = new Phrase("Total", TEXT_FONT);
            table.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellHeader);

            cellRight.Phrase = new Phrase(lengthTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            //cellContainer.AddElement(table);

            container.AddCell(table);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            return container;
        }

        private PdfPTable GetDetailInfo(OutputShippingViewModel model)
        {
            PdfPTable container = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            float[] containerWidths = new float[] { 1f };
            container.SetWidths(containerWidths);

            PdfPCell cellContainer = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER
            };

            PdfPTable table = new PdfPTable(bodyTableColumns.Count)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 3f, 3f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
            table.SetWidths(widths);
            PdfPCell cellHeader = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            PdfPCell cellRight = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            PdfPCell cellLeft = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            foreach (var column in bodyTableColumns)
            {
                cellHeader.Phrase = new Phrase(column, TEXT_FONT_BOLD);
                table.AddCell(cellHeader);
            }

            decimal quantityTotal = 0;
            double lengthTotal = 0;
            double yardLengthTotal = 0;
            double weightTotal = 0;
            int index = 1;
            foreach (var detail in model.ShippingProductionOrders)
            {
                cellRight.Phrase = new Phrase(index++.ToString(), TEXT_FONT);
                table.AddCell(cellRight);

                cellHeader.Phrase = new Phrase(detail.Construction, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.ProductTextile.Name, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.Motif, TEXT_FONT);
                table.AddCell(cellHeader);

                //Ket
                cellHeader.Phrase = new Phrase(detail.ShippingRemark, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.Grade, TEXT_FONT);
                table.AddCell(cellHeader);

                //Grade 2
                cellHeader.Phrase = new Phrase(detail.ShippingGrade, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.ProductionOrder.No, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.Color, TEXT_FONT);
                table.AddCell(cellHeader);

                cellRight.Phrase = new Phrase(detail.QtyPacking.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                quantityTotal += detail.QtyPacking;

                cellHeader.Phrase = new Phrase(detail.Packing, TEXT_FONT);
                table.AddCell(cellHeader);

                var convyardLength = 1.09361 * detail.Qty;
                var yardLength = Math.Round(convyardLength, 0);
                cellRight.Phrase = new Phrase(yardLength.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                yardLengthTotal += yardLength;

                cellRight.Phrase = new Phrase(detail.Qty.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                lengthTotal += detail.Qty;

                // KG
                //cellRight.Phrase = new Phrase((packingReceiptItem.Quantity * packingReceiptItem.Weight).ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                cellRight.Phrase = new Phrase(detail.Weight.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                weightTotal += detail.Weight;

            }

            //PdfPCell cellColspan = new PdfPCell()
            //{
            //    Colspan = 5,
            //    HorizontalAlignment = Element.ALIGN_CENTER,
            //    VerticalAlignment = Element.ALIGN_MIDDLE,
            //};

            //cellColspan.Phrase = new Phrase("Total", TEXT_FONT);
            //table.AddCell(cellColspan);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(quantityTotal == 0 ? "" : quantityTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellRight);

            var convyardLengthTotal = 1.09361 * (double)lengthTotal;
            var yardLengthTotal1 = Math.Round(convyardLengthTotal, 0);

            cellRight.Phrase = new Phrase(yardLengthTotal1 == 0 ? "" : yardLengthTotal1.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(lengthTotal == 0 ? "" : lengthTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(weightTotal == 0 ? "" : weightTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            container.AddCell(table);

            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);
            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);
            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);
            cellContainer.Phrase = new Phrase("", TEXT_FONT);
            container.AddCell(cellContainer);

            return container;
        }

        private PdfPTable GetNettoSection()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };

            cell.Phrase = new Phrase("Di Ball / Lose Packing / Karton", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Netto: ...... Kg Bruto: ......Kg", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            cell.Phrase = new Phrase("", TEXT_FONT);
            cell.Phrase = new Phrase("", TEXT_FONT);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetSignatureSection(OutputShippingViewModel model, int timeoffset)
        {
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 1f, 1f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            //cell.Phrase = new Phrase("Mengetahui", TEXT_FONT);
            //table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase($"Sukoharjo, {model.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase("Kasubsie Gudang Dyeing Printing", TEXT_FONT);
            //table.AddCell(cell);
            cell.Phrase = new Phrase("Audit", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Ekspedisi", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Gudang Dyeing Printing", TEXT_FONT);
            table.AddCell(cell);

            for (var i = 0; i < 11; i++)
            {
                //cell.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cell);
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
            }

            //cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            //table.AddCell(cell);
            cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase($"( SRIWIYATI )", TEXT_FONT);
            table.AddCell(cell);



            return table;
        }
    }
}
