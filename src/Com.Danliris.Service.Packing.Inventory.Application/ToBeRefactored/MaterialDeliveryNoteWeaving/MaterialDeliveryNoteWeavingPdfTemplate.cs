using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingPdfTemplate
    {
        public string TITLE = "PT DAN LIRIS";
        public string ADDRESS = "BANARAN, GROGOL, SUKOHARJO";
        public string DOCUMENT_TITLE = "BON PENGIRIMAN BARANG";
        public string ISO = "FM.W-00-GG-15-004";

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

        List<string> bodyTableColumns = new List<string> { "No", "Nama Barang", "Grd", "Jns", "Ball", "Piece", "Meter", "Kg"};
        List<string> groupTableColumns = new List<string> { "No", "KONSTRUKSI", "METER" };

        public MaterialDeliveryNoteWeavingPdfTemplate(MaterialDeliveryNoteWeavingViewModel model, int timeoffset)
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
            //document.Add(Title);
            //document.Add(AddressTitle);
            document.Add(DocumentTitle);
            document.Add(DocumentISO);
            document.Add(DocumentInfo);
            //document.Add(GroupDetailInfo);
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

        private PdfPTable GetBuyerInfo(MaterialDeliveryNoteWeavingViewModel model, int timeoffset)
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


            cell.Phrase = new Phrase($"Tanggal S.J : {model.DateSJ?.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"Bagian : {model.Unit.Name}", TEXT_FONT);
            table.AddCell(cell);
            //cell.Phrase = new Phrase($"NO. : {model.BonNo}", TEXT_FONT);
            //table.AddCell(cell);

            //string buyerName = model.ShippingProductionOrders.FirstOrDefault()?.Buyer;
            //cell.Phrase = new Phrase($"U/ dikirim kepada: {buyerName}", TEXT_FONT);
            //table.AddCell(cell);

            //cell.Phrase = new Phrase("", TEXT_FONT);
            //table.AddCell(cell);

            //cell.Phrase = new Phrase($"Sesuai DO. NO. : {model.DeliveryOrder.No}", TEXT_FONT);
            //table.AddCell(cell);

            cell.Phrase = new Phrase($"Dikirim Kepada : {model.SendTo}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase($"Tanggal : { model.Date.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
            cell.Phrase = new Phrase($"Nomor : {model.Code}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
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

        private PdfPTable GetGroupDetailInfo(MaterialDeliveryNoteWeavingViewModel model)
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

            cellHeader.Phrase = new Phrase("Total", TEXT_FONT);
            table.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellHeader);

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

        private PdfPTable GetDetailInfo(MaterialDeliveryNoteWeavingViewModel model)
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
            float[] widths = new float[] { 1f, 3f, 2f, 2f, 2f, 2f, 2f, 2f};
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

            decimal balTotal = 0;
            decimal pieceTotal = 0;
            decimal meterTotal = 0;
            decimal kgTotal = 0;
            int index = 1;
            foreach (var detail in model.ItemsMaterialDeliveryNoteWeaving)
            {
                cellRight.Phrase = new Phrase(index++.ToString(), TEXT_FONT);
                table.AddCell(cellRight);

                cellHeader.Phrase = new Phrase(detail.itemMaterialName, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.itemGrade, TEXT_FONT);
                table.AddCell(cellHeader);

                cellHeader.Phrase = new Phrase(detail.itemType, TEXT_FONT);
                table.AddCell(cellHeader);

                cellRight.Phrase = new Phrase(detail.inputBale?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                balTotal += detail.inputBale.GetValueOrDefault();

                cellRight.Phrase = new Phrase(detail.inputPiece?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                pieceTotal += detail.inputPiece.GetValueOrDefault();

                cellRight.Phrase = new Phrase(detail.inputMeter?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                meterTotal += detail.inputMeter.GetValueOrDefault();

                cellRight.Phrase = new Phrase(detail.inputKg?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                kgTotal += detail.inputKg.GetValueOrDefault();

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

            cellRight.Phrase = new Phrase(balTotal == 0 ? "" : balTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(pieceTotal == 0 ? "" : pieceTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(meterTotal == 0 ? "" : meterTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(kgTotal == 0 ? "" : kgTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
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

        private PdfPTable GetSignatureSection(MaterialDeliveryNoteWeavingViewModel model, int timeoffset)
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
            cell.Phrase = new Phrase($"Sukoharjo, {model.DateSJ?.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
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
                
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
            }

            cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase($"({model.CreatedBy})", TEXT_FONT);
            table.AddCell(cell);



            return table;
        }
    }
}