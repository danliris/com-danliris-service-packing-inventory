using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter
{
    public class GarmentCoverLetterPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentCoverLetterViewModel viewModel, GarmentPackingListViewModel pl, Buyer buyer, int timeoffset)
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
            writer.PageEvent = new GarmentCoverLetterPdfTemplatePageEvent();
            document.Open();

            PdfPTable tableTitle = new PdfPTable(1);
            tableTitle.WidthPercentage = 100;
            tableTitle.SetWidths(new float[] { 6f });
            PdfPCell cellTitle = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            //cellTitle.Phrase = new Phrase("Ref No. : FM-00-SP-24-008", header_font);
            //tableTitle.AddCell(cellTitle);
            cellTitle.Phrase = new Phrase("SURAT PENGANTAR", header_font_bold);
            tableTitle.AddCell(cellTitle);
            cellTitle.Phrase = new Phrase(viewModel.invoiceNo, header_font_bold);
            tableTitle.AddCell(cellTitle);

            tableTitle.SpacingAfter = 10;
            document.Add(tableTitle);

            #region header
            PdfPTable tableHeader = new PdfPTable(3);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 3f, 1.5f, 2f });
            PdfPCell cellHeaderLeft = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellHeaderLeft.Phrase = new Phrase("Kepada Yth.\n\n" +
                                               //$"{viewModel.emkl.Name} \n\n" +
                                               $"{viewModel.destination} \n" +
                                               $"{viewModel.address} \n" +
                                               $"PIC : {viewModel.pic} \n\n" +
                                               $"Forwarder : {viewModel.forwarder.name} \n\n" +
                                               $"ATTN  : {viewModel.attn} \n" +
                                               $"PHONE : {viewModel.phone}", normal_font);
            cellHeaderLeft.Rowspan = 7;
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Tanggal", normal_font);
            cellHeaderLeft.Rowspan = 1;
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Order ", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.order.Name, normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Jumlah Pcs ", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.pcsQuantity == 0 ? "-" : $"{ string.Format("{0:n0}", viewModel.pcsQuantity)} PCS", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Jumlah Sets", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.setsQuantity == 0 ? "-" : $"{ string.Format("{0:n0}", viewModel.setsQuantity)} SETS", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Jumlah Packs", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.packQuantity == 0 ? "-" : $"{ string.Format("{0:n0}", viewModel.packQuantity)} PACKS", normal_font);
            tableHeader.AddCell(cellHeaderLeft);


            cellHeaderLeft.Phrase = new Phrase("Jumlah Collie", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{ string.Format("{0:n0}", viewModel.cartoonQuantity)} COLLIE", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Invoice No.", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase(viewModel.invoiceNo, normal_font);
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
            PdfPTable tableDetail = new PdfPTable(7);
            tableDetail.WidthPercentage = 100;
            tableDetail.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f });
            PdfPCell cellDetail = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            double cbmtotal = 0;
            if (pl.Measurements.Count > 0)
            {
                foreach (var m in pl.Measurements)
                {
                    double cbm = (m.Length * m.Width * m.Height * m.CartonsQuantity) / 1000000;
                    cbmtotal += cbm;
                }
            }

            cellDetail.Phrase = new Phrase("Truck", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Nomor Polisi", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Pengemudi", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("No Kontainer", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Seal Pelayaran", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Seal Dan Liris", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase("Jumlah Muatan", normal_font);
            tableDetail.AddCell(cellDetail);

            Paragraph Truck = new Paragraph($"EMKL : {viewModel.emkl.Name} \n\n" +
                                            $"Truk : {viewModel.truck}", normal_font);
            cellDetail.Phrase = Truck;
            cellDetail.VerticalAlignment = Element.ALIGN_TOP;
            tableDetail.AddCell(cellDetail);

            cellDetail.Phrase = new Phrase(viewModel.plateNumber, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.driver, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.containerNo, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.shippingSeal, normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase(viewModel.dlSeal, normal_font);
            tableDetail.AddCell(cellDetail);
            Paragraph weight = new Paragraph($"GW  : {string.Format("{0:n2}", pl.GrossWeight)} KGS \n\n" +
                                           $"NW  : {string.Format("{0:n2}", pl.NettWeight)} KGS \n\n" +
                                           $"Volume : {string.Format("{0:n2}", Math.Round(cbmtotal, 2))} m", normal_font);
            Chunk chunk = new Chunk("3", small_font);
            chunk.SetTextRise(2);
            Paragraph m2 = new Paragraph(chunk);
            m2.Alignment = Element.ALIGN_TOP;
            weight.Add(m2);
            cellDetail.Phrase = weight;
            cellDetail.VerticalAlignment = Element.ALIGN_TOP;
            tableDetail.AddCell(cellDetail);

            tableDetail.SpacingAfter = 10;
            document.Add(tableDetail);
            #endregion

            #region marks
            PdfPTable tableMark = new PdfPTable(4);
            tableMark.WidthPercentage = 100;
            tableMark.SetWidths(new float[] { 1f, 3f, 1f, 3f });
            PdfPCell cellMark = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellMark.Phrase = new Phrase("Shippig Mark :", normal_font);
            cellMark.Rowspan = 2;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase(pl.ShippingMark, normal_font);
            tableMark.AddCell(cellMark);

            string sealType = "";
            string seal = "";
            if (!string.IsNullOrEmpty(viewModel.shippingSeal))
            {
                sealType += "Seal Pelayaran \n";
                seal += ": " + viewModel.shippingSeal + "\n";
            }
            if (!string.IsNullOrEmpty(viewModel.dlSeal))
            {
                sealType += "Seal DL \n";
                seal += ": " + viewModel.dlSeal + "\n";
            }
            if (!string.IsNullOrEmpty(viewModel.emklSeal))
            {
                sealType += "Seal EMKL \n";
                seal += ": " + viewModel.emklSeal + "\n";
            }


            cellMark.Phrase = new Phrase(sealType, normal_font);
            cellMark.Rowspan = 1;
            cellMark.Border = Rectangle.NO_BORDER;
            cellMark.HorizontalAlignment = Element.ALIGN_LEFT;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase(seal, normal_font);
            cellMark.Rowspan = 1;
            cellMark.Border = Rectangle.NO_BORDER;
            cellMark.HorizontalAlignment = Element.ALIGN_LEFT;
            tableMark.AddCell(cellMark);

            //cellMark.Phrase = new Phrase("SEND TO          :", normal_font);
            //cellMark.Colspan = 1;
            //cellMark.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cellMark.Border = Rectangle.BOTTOM_BORDER;
            //tableMark.AddCell(cellMark);

            //cellMark.Phrase = new Phrase($"{buyer.Name} \n" +
            //                             $"{buyer.Address} \n", normal_font);
            //cellMark.HorizontalAlignment = Element.ALIGN_LEFT;
            //tableMark.AddCell(cellMark);

            //tableMark.SpacingAfter = 15;
            //document.Add(tableMark);
            #endregion

            document.Add(new Paragraph("Demikian harap diterima dengan baik dan terima kasih.", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region sign
            PdfPTable tableSign = new PdfPTable(5);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
            PdfPCell cellSign = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellSign.Phrase = new Phrase("Pengemudi Truck, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Mengetahui, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 2;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("                          Hormat Kami, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("             \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("__________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("__________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("__________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("__________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(    " + viewModel.shippingStaff.name + "    )", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Pembukuan DL", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Sat Pam", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Konfeksi " + $"{viewModel.unit}", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Shipping Staff", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("JAM MASUK : ______", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("JAM KELUAR : ______", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n", normal_font);
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase("\n\n", normal_font);
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("CATATAN : \n" +
                                         "1. Mohon bisa dikirim kembali Pengantar ini apabila barang sudah diterima \n" +
                                         "2. ....................................................", normal_font);
            cellSign.Colspan = 3;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Diterima, \n\n\n\n\n\n", normal_font);
            cellSign.Rowspan = 1;
            cellSign.HorizontalAlignment = Element.ALIGN_CENTER;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            cellSign.Colspan = 3;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("                              (__________________)", normal_font);
            cellSign.Rowspan = 1;
            tableSign.AddCell(cellSign);
            cellSign.Phrase = new Phrase(" ", normal_font);
            cellSign.Colspan = 3;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("                              JAM DITERIMA : ______", normal_font);
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

        class GarmentCoverLetterPdfTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
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
                "Banaran, Grogol, Sukoharjo 57193, Central Java, Indonesia",
                "TELP.: (+62 271) 740888, 714400",
                "FAX. : (+62 271) 735222, 740777",
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
                if (imageIso.Width > 100)
                {
                    float percentage = 0.0f;
                    percentage = 100 / imageIso.Width;
                    imageIso.ScalePercent(percentage * 100);
                }
                imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight - 30, branchOfficeY);
                cb.AddImage(imageIso, inlineImage: true);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight-30, headOfficeY, 0);

                #endregion

                #region LINE

                cb.MoveTo(marginLeft, height - marginTop + 35);
                cb.LineTo(width - marginRight, height - marginTop + 35);
                cb.Stroke();

                cb.MoveTo(marginLeft, height - marginTop + 32);
                cb.LineTo(width - marginRight, height - marginTop + 32);
                cb.Stroke();

                #endregion

                cb.EndText();
            }
        }
    }
}
