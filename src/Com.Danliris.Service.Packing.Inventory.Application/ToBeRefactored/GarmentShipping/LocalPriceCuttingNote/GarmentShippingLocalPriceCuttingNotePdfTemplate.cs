using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNotePdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingLocalPriceCuttingNoteViewModel viewModel, Buyer buyer, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header
            PdfPTable tableHeader = new PdfPTable(2);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 3f, 2f });

            PdfPCell cellHeaderContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };


            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PT. DAN LIRIS", header_font_bold));
            cellHeaderContent1.AddElement(new Phrase("Kel. Banaran Kec.Grogol Kab. Sukoharjo", normal_font));
            cellHeaderContent1.AddElement(new Phrase("Telp : 0271-714400, Fax. 0271-717178", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PO. Box. 166 Solo-57100 Indonesia", normal_font));
            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase("NOTA POTONGAN", normal_font));
            tableHeader.AddCell(cellHeaderContent1);

            cellHeaderContent2.AddElement(new Phrase("Sukoharjo, " + viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase(viewModel.buyer.Name, normal_font));
            cellHeaderContent2.AddElement(new Phrase(buyer.Address, normal_font));
            tableHeader.AddCell(cellHeaderContent2);

            document.Add(tableHeader);
            #endregion

            #region title

            PdfPTable tableTitle = new PdfPTable(3);
            tableTitle.WidthPercentage = 100;
            tableTitle.SetWidths(new float[] { 2f, 2f, 1f });

            PdfPCell cellTitle1 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellTitle1.Phrase = new Phrase(viewModel.cuttingPriceNoteNo, normal_font_bold);
            cellTitle1.Colspan = 2;
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("SUKOHARJO - JATENG", normal_font_underlined);
            cellTitle1.Colspan = 1;
            cellTitle1.HorizontalAlignment = Element.ALIGN_LEFT;
            tableTitle.AddCell(cellTitle1);

            tableTitle.SpacingAfter = 10;
            document.Add(tableTitle);
            #endregion

            #region bodyTable
            PdfPTable tableBody = new PdfPTable(2);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 5f, 2f });
            PdfPCell cellBodyLeft = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyLeftNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyRightNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyCenter = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodyCenter.Phrase = new Phrase("Description", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jumlah", normal_font);
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            foreach (var item in viewModel.items)
            {
                cellBodyLeft.Phrase = new Phrase(item.salesNoteNo, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.cuttingAmount), normal_font);
                tableBody.AddCell(cellBodyRight);
            }

            double totalPrice = viewModel.items.Sum(a => a.cuttingAmount);
            double ppn = 0;
            if (viewModel.useVat)
            {
                ppn = totalPrice * 0.1;
            }
            double finalPrice = totalPrice + ppn;

            cellBodyRight.Phrase = new Phrase("Dasar Pengenaan Pajak...............Rp.", normal_font);
            cellBodyRight.Border = Rectangle.NO_BORDER;
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", totalPrice), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.NO_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase("PPN = 10% X Dasar Pengenaan Pajak...Rp.", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", ppn), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.BOTTOM_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase("Jumlah..............................Rp.", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", finalPrice), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.NO_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);


            tableBody.SpacingAfter = 10;
            document.Add(tableBody);
            #endregion

            #region footer
            PdfPTable tableFooter = new PdfPTable(2);
            tableFooter.WidthPercentage = 100;
            tableFooter.SetWidths(new float[] { 1.5f, 9f });

            PdfPCell cellFooterContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            string terbilang = NumberToTextIDN.terbilang(Math.Round(finalPrice, 2));

            cellFooterContent1.Phrase = (new Phrase("Terbilang :", normal_font));
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = (new Phrase(terbilang, normal_font));
            tableFooter.AddCell(cellFooterContent2);

            cellFooterContent1.Phrase = (new Phrase("Catatan   :", normal_font));
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = (new Phrase(viewModel.remark, normal_font));
            tableFooter.AddCell(cellFooterContent2);

            tableFooter.SpacingAfter = 10;
            document.Add(tableFooter);
            #endregion

            #region sign
            PdfPTable tableSign = new PdfPTable(5);
            tableSign.WidthPercentage = 80;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
            PdfPCell cellBodySign = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySign.Phrase = new Phrase("Diterima Oleh", normal_font);
            tableSign.AddCell(cellBodySign);

            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySign.Phrase = new Phrase("Dibuat Oleh", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("Diperiksa Oleh", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("Disetujui Oleh", normal_font);
            tableSign.AddCell(cellBodySign);


            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);

            cellBodySignNoBorder.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);

            document.Add(tableSign);
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
        
    }
}
