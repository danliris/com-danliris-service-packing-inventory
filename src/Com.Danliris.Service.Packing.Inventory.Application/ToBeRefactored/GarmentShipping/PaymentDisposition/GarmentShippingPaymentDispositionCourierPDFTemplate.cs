using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionCourierPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingPaymentDispositionViewModel viewModel, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header

            Paragraph title = new Paragraph("LAMPIRAN DISPOSISI PEMBAYARAN KURIR", header_font_bold_underlined);
            title.Alignment = Element.ALIGN_CENTER;

            Paragraph title1 = new Paragraph("DISPOSISI BIAYA SHIPMENT", normal_font_underlined);
            Paragraph no = new Paragraph(viewModel.dispositionNo, normal_font);
            Paragraph title2 = new Paragraph("Kepada : \nBp./Ibu \nKasir Exp Garment PT Danliris", normal_font);
            
            Paragraph words = new Paragraph($"Harap dibayarkan kepada {viewModel.courier.Name} {viewModel.address} " +
                $"NPWP {viewModel.npwp} untuk tagihan Invoice No. {viewModel.invoiceNumber} Tgl. " +
                $"{viewModel.invoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}" +
                $" Faktur Pajak No.{viewModel.invoiceTaxNumber} dengan rincian sbb : ", normal_font);

            document.Add(title);
            document.Add(title1);
            document.Add(title2);
            document.Add(no);
            document.Add(words);
            #endregion



            #region bodyTable

            PdfPTable tableBody = new PdfPTable(5);
            tableBody.WidthPercentage = 70;
            tableBody.SetWidths(new float[] { 3f, 0.5f, 1f, 2.8f, 1f });

            PdfPCell cellCenterNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeftNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRightNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellLeftNoBorder1 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellLeftTopBorder = new PdfPCell() { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRightTopBorder = new PdfPCell() { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            var last = viewModel.billDetails.Last();
            foreach (var bill in viewModel.billDetails)
            {
                cellLeftNoBorder.Phrase = new Phrase("- " + bill.billDescription, normal_font);
                tableBody.AddCell(cellLeftNoBorder);
                cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
                tableBody.AddCell(cellCenterNoBorder);
                cellLeftNoBorder.Phrase = new Phrase("RP", normal_font);
                tableBody.AddCell(cellLeftNoBorder);
                cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", bill.amount), normal_font);
                tableBody.AddCell(cellRightNoBorder);
                if (bill == last)
                {
                    cellLeftNoBorder.Phrase = new Phrase("(+)", normal_font);
                    tableBody.AddCell(cellLeftNoBorder);
                }
                else
                {
                    cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                    tableBody.AddCell(cellLeftNoBorder);
                }
            }

            cellLeftNoBorder.Phrase = new Phrase("- Total Amount", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("RP", normal_font);
            tableBody.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.billValue), normal_font);
            tableBody.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase($"- PPH {viewModel.incomeTax.name} ({viewModel.incomeTax.rate}%)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font_bold);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("RP", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.IncomeTaxValue), normal_font_bold);
            tableBody.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(-)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("RP", normal_font);
            tableBody.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.billValue - viewModel.IncomeTaxValue), normal_font);
            tableBody.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("- PPN", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("RP", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.vatValue), normal_font);
            tableBody.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(+)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("- Total Bayar", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font_bold);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("RP", normal_font_bold);
            tableBody.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.totalBill), normal_font_bold);
            tableBody.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            tableBody.SpacingAfter = 10;
            tableBody.SpacingBefore = 5;
            tableBody.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableBody);

            var terbilang = NumberToTextIDN.terbilang((double)viewModel.totalBill) + " rupiah";

            Paragraph trbilang = new Paragraph($"[ Terbilang : {terbilang} ]\n", normal_font);
            document.Add(trbilang);
            #endregion

            #region bank
            Paragraph units = new Paragraph("Beban Per Unit", normal_font_bold);
            document.Add(units);

            PdfPTable tableUnit = new PdfPTable(2);
            tableUnit.WidthPercentage = 50;
            tableUnit.SetWidths(new float[] { 1f, 1f });

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };


            cellCenter.Phrase = new Phrase("Unit", normal_font_bold);
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Amount", normal_font_bold);
            tableUnit.AddCell(cellCenter);

            foreach (var unitItem in viewModel.unitCharges)
            {
                cellLeft.Phrase = new Phrase(unitItem.unit.Code, normal_font);
                tableUnit.AddCell(cellLeft);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", unitItem.billAmount), normal_font);
                tableUnit.AddCell(cellRight);
            }

            tableUnit.SpacingAfter = 10;
            tableUnit.SpacingBefore = 5;
            tableUnit.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableUnit);


            Paragraph closing = new Paragraph("Demikian permohonan kami, terima kasih.\n\n", normal_font);
            document.Add(closing);
            #endregion

            #region sign
            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellBodySignNoBorder.Phrase = new Phrase($"Terima kasih, \nSolo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("Hormat kami,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Mengetahui,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Menyetujui,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Dicek oleh,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

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
