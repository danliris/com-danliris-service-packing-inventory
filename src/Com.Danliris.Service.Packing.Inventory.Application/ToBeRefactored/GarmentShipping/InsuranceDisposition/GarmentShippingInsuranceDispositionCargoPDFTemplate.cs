using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionCargoPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingInsuranceDispositionViewModel viewModel, Insurance insurance, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font small_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font small_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 120, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header

            Paragraph title = new Paragraph("DISPOSISI PEMBAYARAN", header_font_bold_underlined);
            title.Alignment = Element.ALIGN_CENTER;

            decimal totalAmountIDR = viewModel.items.Sum(a => a.amount * a.currencyRate);
            Phrase intro = new Phrase();
            intro.Add(new Chunk("Mohon dibayarkan uang sejumlah ", normal_font));
            intro.Add(new Chunk("Rp " + string.Format("{0:n2}", totalAmountIDR), normal_font_bold));

            var terbilang = NumberToTextIDN.terbilang((double)totalAmountIDR)+ " RUPIAH";

            intro.Add(new Chunk($" (terbilang : {terbilang}) sesuai disposisi nomor \n", normal_font));
            intro.Add(new Chunk("Disposisi no : " + viewModel.dispositionNo + " tgl. " + viewModel.paymentDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            intro.Add(new Chunk("\nuntuk pembayaran Polis Asuransi ke " + viewModel.insurance.Name, normal_font));

            document.Add(title);
            document.Add(new Paragraph("\n", normal_font));
            document.Add(intro);
            #endregion

            #region bank
            Paragraph bank = new Paragraph("\nMohon ditransfer ke alamat :\n\n", normal_font);
            document.Add(bank);

            PdfPTable tableBank = new PdfPTable(3);
            tableBank.WidthPercentage = 80;
            tableBank.SetWidths(new float[] { 3f, 0.5f, 14f });

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            cellLeft.Phrase = new Phrase("AccountName", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(viewModel.insurance.Name, normal_font);
            tableBank.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Bank name", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(viewModel.bankName, normal_font);
            tableBank.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Account no.", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(insurance.AccountNumber, normal_font);
            tableBank.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Swift code", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            tableBank.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(insurance.SwiftCode, normal_font);
            tableBank.AddCell(cellLeft);

            tableBank.SpacingAfter = 10;
            tableBank.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableBank);


            Paragraph closing = new Paragraph("Demikian permohonan kami, terima kasih.\n\n", normal_font);
            document.Add(closing);
            #endregion

            #region sign
            Paragraph date = new Paragraph($"Sukoharjo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            document.Add(date);

            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySignNoBorder.Phrase = new Phrase("Hormat kami,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Mengetahui,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Dicek,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Diterima,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("STAFF SHIPPING", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("KABAG/KASIE SHIPPING", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("BAGIAN VERIFIKASI", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("BAGIAN KASIR", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            document.Add(tableSign);
            #endregion

            document.NewPage();

            #region intro2
            Paragraph to = new Paragraph("Kepada : Yth. Bp. Ricky H. - Keuangan \n " +
                "Mohon dibayarkan kepada " +viewModel.insurance.Name +" biaya asuransi sbb: ",small_font);

            Paragraph disposisi = new Paragraph("Disposisi No : " + viewModel.dispositionNo, small_font);
            disposisi.Alignment = Element.ALIGN_RIGHT;

            document.Add(to);
            document.Add(disposisi);
            #endregion

            #region table
            PdfPTable tableDetail = new PdfPTable(10);
            tableDetail.WidthPercentage = 100;
            tableDetail.SetWidths(new float[] { 1f, 3.5f, 3f, 3f, 4f, 1.2f, 2.8f, 2f, 1f, 3f });

            PdfPCell cellCenterBorder = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellRightBorder = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellLeftBorder = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellCenterBorder.Phrase = new Phrase("No", small_font);
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Tgl Polis", small_font);
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("No Polis", small_font);
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Invoice", small_font);
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Buyer", small_font);
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Amount", small_font);
            cellCenterBorder.Colspan = 2;
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Kurs", small_font);
            cellCenterBorder.Colspan = 1;
            tableDetail.AddCell(cellCenterBorder);
            cellCenterBorder.Phrase = new Phrase("Amount IDR", small_font);
            cellCenterBorder.Colspan = 2;
            tableDetail.AddCell(cellCenterBorder);

            int index = 0;
            foreach(var item in viewModel.items)
            {
                index++;

                cellCenterBorder.Phrase = new Phrase(index.ToString(), small_font);
                cellCenterBorder.Colspan = 1;
                tableDetail.AddCell(cellCenterBorder);

                cellCenterBorder.Phrase = new Phrase(item.policyDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), small_font);
                tableDetail.AddCell(cellCenterBorder);
                
                cellCenterBorder.Phrase = new Phrase(item.policyNo, small_font);
                tableDetail.AddCell(cellCenterBorder);

                cellCenterBorder.Phrase = new Phrase(item.invoiceNo, small_font);
                tableDetail.AddCell(cellCenterBorder);

                cellCenterBorder.Phrase = new Phrase(item.BuyerAgent.Name, small_font);
                tableDetail.AddCell(cellCenterBorder);

                cellLeftBorder.Phrase = new Phrase("USD", small_font);
                tableDetail.AddCell(cellLeftBorder);

                cellRightBorder.Phrase = new Phrase(string.Format("{0:n2}", item.amount), small_font);
                tableDetail.AddCell(cellRightBorder);

                cellCenterBorder.Phrase = new Phrase(string.Format("{0:n}",item.currencyRate), small_font);
                tableDetail.AddCell(cellCenterBorder);

                cellLeftBorder.Phrase = new Phrase("IDR", small_font);
                tableDetail.AddCell(cellLeftBorder);

                cellRightBorder.Phrase = new Phrase(string.Format("{0:n2}", item.amount*item.currencyRate), small_font);
                tableDetail.AddCell(cellRightBorder);
            }

            cellCenterBorder.Phrase = new Phrase("TOTAL", small_font_bold);
            cellCenterBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellCenterBorder.Colspan = 5;
            tableDetail.AddCell(cellCenterBorder);

            cellLeftBorder.Phrase = new Phrase("USD", small_font_bold);
            tableDetail.AddCell(cellLeftBorder);

            cellRightBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.items.Sum(a=>a.amount)), small_font_bold);
            tableDetail.AddCell(cellRightBorder);

            cellCenterBorder.Phrase = new Phrase("", small_font);
            cellCenterBorder.Colspan = 1;
            tableDetail.AddCell(cellCenterBorder);

            cellLeftBorder.Phrase = new Phrase("IDR", small_font_bold);
            tableDetail.AddCell(cellLeftBorder);

            cellRightBorder.Phrase = new Phrase(string.Format("{0:n2}", totalAmountIDR), small_font_bold);
            tableDetail.AddCell(cellRightBorder);

            tableDetail.SpacingAfter = 10;
            tableDetail.SpacingBefore = 5;
            document.Add(tableDetail);
            #endregion

            #region unitCharge
            Phrase titleUnit = new Phrase("Beban per Unit", small_font);
            document.Add(titleUnit);

            PdfPTable tableUnit = new PdfPTable(2);
            tableUnit.WidthPercentage = 40;
            tableUnit.SetWidths(new float[] { 3f, 6f });

            PdfPCell cellUnitLeft = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellUnitRight = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellUnitCenter = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellUnitCenter.Phrase = new Phrase("Unit", small_font);
            tableUnit.AddCell(cellUnitCenter);
            cellUnitCenter.Phrase = new Phrase("Amount", small_font);
            tableUnit.AddCell(cellUnitCenter);

            decimal amount1A = viewModel.items.Sum(a => a.amount1A);
            cellUnitLeft.Phrase= new Phrase("UNIT 1A", small_font);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(amount1A > 0 ? string.Format("{0:n2}", amount1A) : "", small_font);
            tableUnit.AddCell(cellUnitRight);

            decimal amount1B = viewModel.items.Sum(a => a.amount1B);
            cellUnitLeft.Phrase = new Phrase("UNIT 1B", small_font);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(amount1B > 0 ? string.Format("{0:n2}", amount1B) : "", small_font);
            tableUnit.AddCell(cellUnitRight);

            decimal amount2A = viewModel.items.Sum(a => a.amount2A);
            cellUnitLeft.Phrase = new Phrase("UNIT 2A", small_font);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(amount2A > 0 ? string.Format("{0:n2}", amount2A) : "", small_font);
            tableUnit.AddCell(cellUnitRight);

            decimal amount2B = viewModel.items.Sum(a => a.amount2B);
            cellUnitLeft.Phrase = new Phrase("UNIT 2B", small_font);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(amount2B > 0 ? string.Format("{0:n2}", amount2B) : "", small_font);
            tableUnit.AddCell(cellUnitRight);

            decimal amount2C = viewModel.items.Sum(a => a.amount2C);
            cellUnitLeft.Phrase = new Phrase("UNIT 2C", small_font);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(amount2C > 0 ? string.Format("{0:n2}", amount2C) : "", small_font);
            tableUnit.AddCell(cellUnitRight);

            decimal totalUnitCharge = amount1A + amount1B + amount2A + amount2B + amount2C;
            cellUnitLeft.Phrase = new Phrase("Total", small_font_bold);
            tableUnit.AddCell(cellUnitLeft);
            cellUnitRight.Phrase = new Phrase(string.Format("{0:n2}", totalUnitCharge), small_font_bold);
            tableUnit.AddCell(cellUnitRight);

            tableUnit.SpacingAfter = 5;
            tableUnit.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableUnit);

            document.Add(new Phrase("[Terbilang : " + terbilang +"]", small_font));
            #endregion

            #region sign2
            Paragraph date2 = new Paragraph($"Sukoharjo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", small_font);
            document.Add(date2);

            PdfPTable tableSign2 = new PdfPTable(4);
            tableSign2.WidthPercentage = 100;
            tableSign2.SetWidths(new float[] { 1f, 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder2 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySignNoBorder2.Phrase = new Phrase("Hormat kami,\n\n\n\n", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("Mengetahui,\n\n\n\n", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("Dicek,\n\n\n\n", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("Diterima,\n\n\n\n", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);


            cellBodySignNoBorder2.Phrase = new Phrase("(                           )", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("(                           )", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("(                           )", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("(                           )", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);


            cellBodySignNoBorder2.Phrase = new Phrase("STAFF SHIPPING", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("KABAG/KASIE SHIPPING", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("BAGIAN VERIFIKASI", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);
            cellBodySignNoBorder2.Phrase = new Phrase("BAGIAN KASIR", small_font);
            tableSign2.AddCell(cellBodySignNoBorder2);

            document.Add(tableSign2);
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
