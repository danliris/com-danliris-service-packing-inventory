using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNotePdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingLocalReturnNoteViewModel viewModel, Buyer buyer, int timeoffset)
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
            cellHeaderContent1.AddElement(new Phrase("Jl. Merapi No. 23,  Kel. Banaran Kec.Grogol Kab. Sukoharjo", normal_font));
            cellHeaderContent1.AddElement(new Phrase("Telp : 0271-714400, Fax. 0271-717178", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PO. Box. 166 Solo-57100 Indonesia", normal_font));
            tableHeader.AddCell(cellHeaderContent1);

            cellHeaderContent2.AddElement(new Phrase("Sukoharjo, " + viewModel.returnDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase(viewModel.salesNote.buyer.Name, normal_font));
            cellHeaderContent2.AddElement(new Phrase(buyer.Address, normal_font));
            tableHeader.AddCell(cellHeaderContent2);

            document.Add(tableHeader);
            #endregion

            #region title

            Paragraph title = new Paragraph("NOTA RETUR", header_font_bold);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            Paragraph no = new Paragraph(viewModel.returnNoteNo, header_font_bold);
            no.Alignment = Element.ALIGN_CENTER;
            document.Add(no);

            Paragraph location = new Paragraph("SUKOHARJO - JATENG", normal_font_underlined);
            location.Alignment = Element.ALIGN_RIGHT;
            document.Add(location);
            #endregion

            #region bodyTable
            PdfPTable tableBody = new PdfPTable(5);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 4f, 2.5f, 1.5f, 3f, 3f });
            PdfPCell cellBodyLeft = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyLeftNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyRightNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyCenter = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            
            cellBodyCenter.Phrase = new Phrase("Nama Barang", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Quantity", normal_font);
            cellBodyCenter.Colspan = 2;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Harga Sat.", normal_font);
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jumlah", normal_font);
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            foreach (var item in viewModel.items)
            {
                
                cellBodyLeft.Phrase = new Phrase(item.salesNoteItem.product.name, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", item.returnQuantity), normal_font);
                tableBody.AddCell(cellBodyRightNoBorder);

                cellBodyLeftNoBorder.Phrase = new Phrase(item.salesNoteItem.uom.Unit, normal_font);
                tableBody.AddCell(cellBodyLeftNoBorder);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.salesNoteItem.price), normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.salesNoteItem.price * item.returnQuantity), normal_font);
                tableBody.AddCell(cellBodyRight);
            }

            double totalPrice = viewModel.items.Sum(a => a.returnQuantity * a.salesNoteItem.price);
            double ppn = 0;
            if (viewModel.salesNote.useVat)
            {
                ppn = totalPrice * 0.1;
            }
            double finalPrice = totalPrice + ppn;

            cellBodyRight.Phrase = new Phrase("Dasar Pengenaan Pajak...............Rp.", normal_font);
            cellBodyRight.Border = Rectangle.NO_BORDER;
            cellBodyRight.Colspan = 4;
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

            tableBody.SpacingBefore = 5;
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

            //cellFooterContent1.Phrase = new Phrase("Tempo     :", normal_font);
            //tableFooter.AddCell(cellFooterContent1);
            //cellFooterContent2.Phrase = new Phrase(viewModel.salesNote.tempo + " Hari", normal_font);
            //tableFooter.AddCell(cellFooterContent2);

            //cellFooterContent1.Phrase = (new Phrase("JT.       :", normal_font));
            //tableFooter.AddCell(cellFooterContent1);
            //cellFooterContent2.Phrase = (new Phrase(viewModel.returnDate.GetValueOrDefault().AddDays(viewModel.salesNote.tempo).ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            //tableFooter.AddCell(cellFooterContent2);

            cellFooterContent1.Phrase = (new Phrase("Terbilang :", normal_font));
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = (new Phrase(terbilang + " rupiah", normal_font));
            tableFooter.AddCell(cellFooterContent2);

            cellFooterContent1.Phrase = (new Phrase("Catatan   :", normal_font));
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = (new Phrase(viewModel.description, normal_font));
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
            document.Add(new Phrase("Model DL1", normal_font));

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
