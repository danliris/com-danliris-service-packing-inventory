using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public class MaterialDeliveryNotePdfTemplate
    {
        //private int timeoffsset;
        public string TITLE = "PT. DAN LIRIS";
        public string ADDRESS = "Sukoharjo";
        public string DOCUMENT_TITLE = "BON PENGIRIMAN BARANG";
        public string ISO = "FM.S-00-GJ-15-011";

        public Font HEADER_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
        public Font HEADER_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
        public Font SUBHEADER_FONT_BOLD_UNDERLINED = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12, Font.UNDERLINE);
        public Font TEXT_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        public Font TEXT_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

        public BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
        public BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

        private readonly PdfPTable Title;
        private readonly PdfPTable AddressTitle;
        private readonly PdfPTable DocumentTitle;
        private readonly PdfPTable DocumentISO;
        private readonly PdfPTable DocumentInfo;
        private readonly PdfPTable GroupDetailInfo;
        private readonly PdfPTable DetailInfo;
        private readonly PdfPTable NettoSection;
        private readonly PdfPTable SignatureSection;
        //private readonly PdfContentByte Title_New;

        List<string> bodyTableColumns = new List<string> { "No", "Nama Barang", "Bale", "KG" };
        List<string> groupTableColumns = new List<string> { "No", "KONSTRUKSI", "METER" };

        public MaterialDeliveryNotePdfTemplate(MaterialDeliveryNoteViewModel model, int timeoffset)
        {
            Title = GetTitle();
            //Title_New = Get_Title_New();
            AddressTitle = GetAddressTitle();
            DocumentTitle = GetDocumentTitle();
            DocumentISO = GetISO();
            DocumentInfo = GetBuyerInfo(model, timeoffset);
            GroupDetailInfo = GetGroupDetailInfo(model);
            DetailInfo = GetDetailInfo(model, timeoffset);
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

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);


            document.Open();

            #region Header
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            cb.SetFontAndSize(bf_bold, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PT DAN LIRIS", 15, 390, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Sukoharjo", 15, 375, 0);

            cb.SetFontAndSize(bf_bold, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM.S-00-GJ-15-011", 460, 390, 0);

            cb.EndText();

            //document.Add((IElement)Title_New);
            //document.Add(Title);
            //document.Add(AddressTitle);
            document.Add(DocumentTitle);
            //document.Add(DocumentISO);
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

        //private PdfContentByte Get_Title_New()
        //{



        //    return cb;

        //}

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
                PaddingBottom = 15f
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
                PaddingBottom = 15f
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

        private PdfPTable GetBuyerInfo(MaterialDeliveryNoteViewModel model, int timeoffset)
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


            cell.Phrase = new Phrase($"Kepada Yth : {model.buyer.Name}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"No. : {model.Code}", TEXT_FONT_BOLD);
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

            cell.Phrase = new Phrase($"U/dikirim Kpd : {model.unit.Name}", TEXT_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase($"Tanggal : { model.Date.AddHours(timeoffset).ToString("dd MMMM yyyy")}", TEXT_FONT);
            cell.Phrase = new Phrase($"Sesuai DO No. : {model.DONumber.DOSalesNo}", TEXT_FONT_BOLD);
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

        private PdfPTable GetGroupDetailInfo(MaterialDeliveryNoteViewModel model)
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

        private PdfPTable GetDetailInfo(MaterialDeliveryNoteViewModel model, int timeoffset)
        {
            PdfPTable container = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            float[] containerWidths = new float[] { 1f };
            container.SetWidths(containerWidths);

            PdfPCell cellContainer = new PdfPCell()
            {
                //Border = Rectangle.NO_BORDER
            };

            PdfPTable table = new PdfPTable(bodyTableColumns.Count)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 0.5f, 3.5f, 1f, 1f };
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

            double balTotal = 0;
            //decimal pieceTotal = 0;
            //decimal meterTotal = 0;
            double kgTotal = 0;
            int index = 1;
            foreach (var detail in model.Items)
            {
                cellHeader.Phrase = new Phrase(index++.ToString(), TEXT_FONT);
                table.AddCell(cellHeader);

                cellLeft.Phrase = new Phrase(detail.MaterialName, TEXT_FONT);
                table.AddCell(cellLeft);

                cellRight.Phrase = new Phrase(detail.WeightBale?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                balTotal += detail.WeightBale.GetValueOrDefault();

                cellRight.Phrase = new Phrase(detail.GetTotal?.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                table.AddCell(cellRight);
                kgTotal += detail.GetTotal.GetValueOrDefault();

            }

            for (int i = 1; i <= 3; i++)
            {
                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);
            }

            foreach (var detail in model.Items)
            {
                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellLeft.Phrase = new Phrase("LOT." + detail.InputLot + " BRUTO = " + detail.WeightBruto + " KG", TEXT_FONT);
                table.AddCell(cellLeft);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);



                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                var dos = detail.WeightDOS.Split(",");
                var cone = detail.WeightCone.Split(",");

                cellLeft.Phrase = new Phrase(dos[0] + "DOS@" + cone[0] + "CONE+" + dos[1] + "DOS@" + cone[1] + "CONE", TEXT_FONT);
                table.AddCell(cellLeft);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);


                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellLeft.Phrase = new Phrase($"PROD`{model.DateFrom.AddHours(timeoffset):dd}-{model.DateTo.AddHours(timeoffset):dd} {model.DateFrom.AddHours(timeoffset):y}", TEXT_FONT);
                table.AddCell(cellLeft);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);

                cellRight.Phrase = new Phrase(" ", TEXT_FONT);
                table.AddCell(cellRight);
            }

            //PdfPCell cellColspan = new PdfPCell()
            //{
            //    Colspan = 5,
            //    HorizontalAlignment = Element.ALIGN_CENTER,
            //    VerticalAlignment = Element.ALIGN_MIDDLE,
            //};

            //cellColspan.Phrase = new Phrase("Total", TEXT_FONT);
            //table.AddCell(cellColspan);

            cellRight.Phrase = new Phrase(" ", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(" ", TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(balTotal == 0 ? "" : balTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            cellRight.Phrase = new Phrase(kgTotal == 0 ? "" : kgTotal.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
            table.AddCell(cellRight);

            container.AddCell(table);

            //cellContainer.Phrase = new Phrase("", TEXT_FONT);
            //container.AddCell(cellContainer);
            //cellContainer.Phrase = new Phrase("", TEXT_FONT);
            //container.AddCell(cellContainer);
            //cellContainer.Phrase = new Phrase("", TEXT_FONT);
            //container.AddCell(cellContainer);
            //cellContainer.Phrase = new Phrase("", TEXT_FONT);
            //container.AddCell(cellContainer);

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

        private PdfPTable GetSignatureSection(MaterialDeliveryNoteViewModel model, int timeoffset)
        {
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 1f, 1f, 1f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            PdfPCell cellLeft = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            PdfPCell cellColspan = new PdfPCell()
            {
                Colspan = 4,
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            cellColspan.Phrase = new Phrase("No.FO : " + model.FONumber + "       Ex.Spn : " + model.storage.Name, TEXT_FONT);
            table.AddCell(cellColspan);

            cellColspan.Phrase = new Phrase("No.SC : " + model.salesContract.SalesContractNo, TEXT_FONT);
            table.AddCell(cellColspan);

            //cell.Phrase = new Phrase(" ", TEXT_FONT);
            //table.AddCell(cell);
            //cell.Phrase = new Phrase(" ", TEXT_FONT);
            //table.AddCell(cell);
            //cell.Phrase = new Phrase(" ", TEXT_FONT);
            //table.AddCell(cell);
            //cell.Phrase = new Phrase(" ", TEXT_FONT);
            //table.AddCell(cell);

            //cell.Phrase = new Phrase("Mengetahui", TEXT_FONT);
            //table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase($"Sukoharjo, {model.DateSJ.AddHours(timeoffset):dd MMMM yyyy}", TEXT_FONT);
            table.AddCell(cell);

            //cell.Phrase = new Phrase("Kasubsie Gudang Dyeing Printing", TEXT_FONT);
            //table.AddCell(cell);
            cell.Phrase = new Phrase("Audit:", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Ekspedisi:", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Kabag:", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Petugas Gudang:", TEXT_FONT);
            table.AddCell(cell);

            for (var i = 0; i < 11; i++)
            {

                cell.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cell);
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
            cell.Phrase = new Phrase("(                        )", TEXT_FONT);
            table.AddCell(cell);
            cell.Phrase = new Phrase($"({model.CreatedBy})", TEXT_FONT);
            table.AddCell(cell);



            return table;
        }
    }
}