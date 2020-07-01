using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
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

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 150, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            PdfPTable tableTitle = new PdfPTable(3);
            tableTitle.WidthPercentage = 100;
            tableTitle.SetWidths(new float[] { 2f, 2f, 2f });
            PdfPCell cellTitle = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellTitle.Phrase = new Phrase("Ref No. : FM-00-SP-24-008", header_font);
            tableTitle.AddCell(cellTitle);
            cellTitle.Phrase = new Phrase("SURAT PENGANTAR", header_font_bold);
            tableTitle.AddCell(cellTitle);
            cellTitle.Phrase = new Phrase($"{viewModel.invoiceNo}", header_font);
            tableTitle.AddCell(cellTitle);

            tableTitle.SpacingAfter = 10;
            document.Add(tableTitle);

            #region header
            PdfPTable tableHeader = new PdfPTable(3);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 3f, 1.5f, 2f });
            PdfPCell cellHeaderLeft = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER , HorizontalAlignment = Element.ALIGN_LEFT };

            cellHeaderLeft.Phrase = new Phrase("Kepada Yth.\n\n" +
                                               $"{viewModel.name} \n" +
                                               $"{viewModel.address} \n" +
                                               $"{viewModel.forwarder.name} \n\n" +
                                               $"ATTN  : {viewModel.attn} \n" +
                                               $"PHONE : {viewModel.phone}", normal_font);
            cellHeaderLeft.Rowspan = 5;
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("", normal_font);
            cellHeaderLeft.Rowspan = 1;
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{viewModel.date.GetValueOrDefault().ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"))}", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Order ", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{viewModel.order.Name}", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Jumlah ", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{viewModel.pcsQuantity + viewModel.setsQuantity +viewModel.packQuantity} Pcs.", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Jumlah Collie", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{viewModel.cartoonQuantity}", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            cellHeaderLeft.Phrase = new Phrase("Invoice No.", normal_font);
            tableHeader.AddCell(cellHeaderLeft);
            cellHeaderLeft.Phrase = new Phrase($"{viewModel.invoiceNo}", normal_font);
            tableHeader.AddCell(cellHeaderLeft);

            tableHeader.SpacingAfter = 10;
            document.Add(tableHeader);
            #endregion

            document.Add(new Paragraph("Dengan hormat,",normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("      Bersama ini kami kirimkan kepada Bapak sejumlah barang dengan", normal_font));

            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region detail
            PdfPTable tableDetail = new PdfPTable(4);
            tableDetail.WidthPercentage = 100;
            tableDetail.SetWidths(new float[] { 1f, 1f, 1f, 2f });
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
            cellDetail.Phrase = new Phrase("Jumlah Muatan", normal_font);
            tableDetail.AddCell(cellDetail);

            cellDetail.Phrase = new Phrase($"{viewModel.truck}", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase($"{viewModel.plateNumber}", normal_font);
            tableDetail.AddCell(cellDetail);
            cellDetail.Phrase = new Phrase($"{viewModel.driver}", normal_font);
            tableDetail.AddCell(cellDetail);
            Paragraph weight = new Paragraph($"GW  : {pl.GrossWeight} KGS \n\n" +
                                           $"NW  : {pl.NettWeight} KGS \n\n" +
                                           $"Volume : {cbmtotal} m", normal_font);
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
            tableMark.SetWidths(new float[] { 1f, 3f,1f, 3f });
            PdfPCell cellMark = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellMark.Phrase = new Phrase("Shippig Mark :", normal_font);
            cellMark.Rowspan = 2;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase($"{pl.ShippingMark}", normal_font);
            tableMark.AddCell(cellMark);

            string sealType = "";
            string seal = "";
            if (!string.IsNullOrEmpty(viewModel.shippingSeal))
            {
                sealType += "Seal Pelayaran  :  \n";
                seal +=  viewModel.shippingSeal +"\n";
            }
            if (!string.IsNullOrEmpty(viewModel.dlSeal))
            {
                sealType += "Seal DL  :  \n";
                seal += viewModel.dlSeal + "\n";
            }
            if (!string.IsNullOrEmpty(viewModel.emklSeal))
            {
                sealType += "Seal EMKL  :  \n";
                seal += viewModel.emklSeal + "\n";
            }


            cellMark.Phrase = new Phrase($"{sealType}", normal_font);
            cellMark.Rowspan = 1;
            cellMark.Border = Rectangle.NO_BORDER;
            cellMark.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase($"{seal}", normal_font);
            cellMark.Rowspan = 1;
            cellMark.Border = Rectangle.NO_BORDER;
            cellMark.HorizontalAlignment = Element.ALIGN_LEFT;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase("SEND TO          :", normal_font);
            cellMark.Colspan = 1;
            cellMark.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellMark.Border = Rectangle.BOTTOM_BORDER;
            tableMark.AddCell(cellMark);

            cellMark.Phrase = new Phrase($"{buyer.Name} \n" +
                                         $"{buyer.Address} \n", normal_font);
            cellMark.HorizontalAlignment = Element.ALIGN_LEFT;
            tableMark.AddCell(cellMark);

            tableMark.SpacingAfter = 15;
            document.Add(tableMark);
            #endregion

            document.Add(new Paragraph("Demikian harap diterima dengan baik dan terima kasih                               SHIPPING STAFF : "+ viewModel.shippingStaff.name, normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region sign
            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });
            PdfPCell cellSign = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellSign.Phrase = new Phrase("Pengemudi Truck, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Mengetahui, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 2;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Hormat Kami, \n\n\n\n\n\n", normal_font);
            cellSign.Colspan = 1;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("_________________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("_________________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("_________________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("_________________________", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Pembukuan DL", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Sat Pam", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Konveksi", normal_font);
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase($"{viewModel.unit} \n\n", normal_font);
            cellSign.Colspan = 2;
            tableSign.AddCell(cellSign);


            cellSign.Phrase = new Phrase("CATATAN : \n" +
                                         "1. Mohon bisa dikirim kembali Pengantar ini apabila barang sudah diterima \n" +
                                         "2. ....................................................", normal_font);
            cellSign.Rowspan = 2;
            cellSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("Diterima, \n\n\n\n\n\n", normal_font);
            cellSign.Rowspan = 1;
            cellSign.HorizontalAlignment = Element.ALIGN_CENTER;
            tableSign.AddCell(cellSign);

            cellSign.Phrase = new Phrase("(_________________________)", normal_font);
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
