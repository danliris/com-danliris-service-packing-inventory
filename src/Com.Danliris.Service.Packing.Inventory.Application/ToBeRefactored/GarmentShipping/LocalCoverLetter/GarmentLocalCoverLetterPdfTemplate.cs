using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
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

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 140, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.PageEvent = new GarmentLocalCoverLetterPdfTemplatePageEvent();
            document.Open();

            #region title
            Paragraph title = new Paragraph("SURAT PENGANTAR",header_font_bold);
            title.Alignment = Element.ALIGN_CENTER;

            Paragraph no = new Paragraph(viewModel.localCoverLetterNo, bold_font);
            no.Alignment = Element.ALIGN_CENTER;

            Paragraph date = new Paragraph("Sukoharjo, "+viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
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
            PdfPCell cellDetail = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            //double cbmtotal = 0;


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

            cellMark.Phrase = new Phrase("No Bea Cukai :", normal_font);
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase(viewModel.bcNo, normal_font);
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase("Tgl Bea Cukai :", normal_font);
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase(viewModel.bcdate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
            tableMark.AddCell(cellMark);

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
            PdfPTable tableSign = new PdfPTable(5);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
            PdfPCell cellSign = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            //cellSign.Phrase = new Phrase("", normal_font);
            //tableSign.AddCell(cellSign);

            //cellSign.Phrase = new Phrase("SHIPPING STAF : "+ viewModel.shippingStaff.name, normal_font);
            //cellSign.Colspan = 3;
            //tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("Pengemudi Truck, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Mengetahui, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 2;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("\n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
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

            cellSign.Phrase = new Phrase($"( {viewModel.shippingStaff.name} )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("\n", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Audit", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Sat Pam", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Bagian Gudang", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Shipping Staff", normal_font);
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("CATATAN : \n" +
                                         "1. Mohon bisa dikirim kembali Pengantar ini apabila barang sudah diterima \n" +
                                         "2. ....................................................", normal_font);
            cellSign.Colspan = 3;
            cellSign.Rowspan = 2;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Diterima, \n\n\n\n", normal_font);
            cellSign.Rowspan = 1;
            cellSign.Colspan = 2;
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

        class GarmentLocalCoverLetterPdfTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

                float height = writer.PageSize.Height, width = writer.PageSize.Width;
                float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

                cb.SetFontAndSize(bf, 6);

                #region LEFT


                var branchOfficeY = height - marginTop + 70;

                byte[] imageByteDL = Convert.FromBase64String(Base64ImageStrings.LOGO_DANLIRIS_58_58);
                Image imageDL = Image.GetInstance(imageByteDL);
                imageDL.SetAbsolutePosition(marginLeft, branchOfficeY);
                cb.AddImage(imageDL, inlineImage: true);
                //for (int i = 0; i < branchOffices.Length; i++)
                //{
                //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, branchOffices[i], marginLeft, branchOfficeY - 10 - (i * 8), 0);
                //}

                #endregion

                #region CENTER

                var headOfficeX = width / 2 - 200;
                var headOfficeY = height - marginTop + 110;

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
                "Head Office : JL. MERAPI NO. 23 ",
                "Banaran, Grogol, Sukoharjo 57552",
                "Central Java, Indonesia",
                "TELP.: (+62 271) 740888, 714400" ,
                "FAX. : (+62 271) 735222, 740777" ,
                "PO BOX 166 Solo, 57100",
                "Website : www.danliris.com",
            };
                for (int i = 0; i < headOffices.Length; i++)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, headOffices[i], headOfficeX, headOfficeY - image.ScaledHeight - (i * 10), 0);
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
                imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight - 30, branchOfficeY);
                cb.AddImage(imageIso, inlineImage: true);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight-30, headOfficeY, 0);

                #endregion

                cb.EndText();
            }
        }
    }
}
