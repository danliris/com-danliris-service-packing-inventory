using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter
{
    public class GarmentLocalCoverLetterPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentLocalCoverLetterViewModel viewModel, GarmentShippingLocalSalesNoteViewModel salesNote, Buyer buyer, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font small_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 5);
            //Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region title
            Paragraph title = new Paragraph("SURAT PENGANTAR",header_font_bold);
            title.Alignment = Element.ALIGN_CENTER;

            Paragraph no = new Paragraph(viewModel.localCoverLetterNo, bold_font);
            no.Alignment = Element.ALIGN_CENTER;

            Paragraph date = new Paragraph(viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
            date.Alignment = Element.ALIGN_RIGHT;

            document.Add(title);
            document.Add(no);
            document.Add(date);
            document.Add(new Paragraph("\n", normal_font));
            #endregion

            #region header
            PdfPTable tableHeader = new PdfPTable(6);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 4f,1f,6f,4f,1f,3f });
            PdfPCell cellHeaderLeft = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellHeaderRight = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellHeaderLeft.Phrase = new Phrase("Kepada Yth.", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(":", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.buyer.Name, normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderRight.Phrase = new Phrase("No Nota Penjualan", normal_font);
            tableHeader.AddCell(cellHeaderRight);
            cellHeaderLeft.Phrase = new Phrase(":", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.noteNo, normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Alamat", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(":", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(buyer.Address, normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderRight.Phrase = new Phrase("Tgl Nota Penjualan", normal_font);
            tableHeader.AddCell(cellHeaderRight);
            cellHeaderLeft.Phrase = new Phrase(":", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(salesNote.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            tableHeader.SpacingAfter = 10;
            document.Add(tableHeader);
            #endregion

            document.Add(new Paragraph("Dengan hormat,", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("      Bersama ini kami kirimkan kepada Bapak sejumlah barang dengan", normal_font));

            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region detail
            PdfPTable tableDetail = new PdfPTable(3);
            tableDetail.WidthPercentage = 80;
            tableDetail.SetWidths(new float[] { 1f, 1f, 1f });
            PdfPCell cellDetail = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            double cbmtotal = 0;


            cellDetail.Phrase = new Phrase("Truck", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Nomor Polisi", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Pengemudi", normal_font);
            tableDetail.AddCell(cellDetail);

            cellDetail.Phrase = new Phrase(viewModel.truck, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.plateNumber, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.driver, normal_font);
            tableDetail.AddCell(cellDetail);

            tableDetail.SpacingAfter = 15;
            document.Add(tableDetail);
            #endregion

            #region marks
            PdfPTable tableMark = new PdfPTable(2);
            tableMark.WidthPercentage = 100;
            tableMark.SetWidths(new float[] { 1f, 6f });
            PdfPCell cellMark = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellMark.Phrase = new Phrase("Shippig Mark :", normal_font);
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase(viewModel.remark+"\n\n", normal_font);
            tableMark.AddCell(cellMark);

            tableMark.SpacingAfter = 15;
            document.Add(tableMark);
            #endregion

            document.Add(new Paragraph("Demikian harap diterima dengan baik dan terima kasih", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region sign
            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });
            PdfPCell cellSign = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("SHIPPING STAF : "+ viewModel.shippingStaff.name, normal_font);
            cellSign.Colspan = 3;
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("Pengemudi Truck, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Mengetahui, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 2;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Hormat Kami, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(                        )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(                        )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(                        )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(                        )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("\n", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Pembukuan DL", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Sat Pam", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("UnitKonveksi", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("CATATAN : \n" +
                                         "1. Mohon bisa dikirim kembali Pengantar ini apabila barang sudah diterima \n" +
                                         "2. ....................................................", normal_font);
            cellSign.Colspan = 2;
            cellSign.Rowspan = 2;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Diterima, \n\n\n\n", normal_font);
            cellSign.Rowspan = 1;
            cellSign.HorizontalAlignment = Element.ALIGN_CENTER;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(                        )", normal_font);
            cellSign.Rowspan = 1;
            tableSign.AddCell(cellSign);

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
