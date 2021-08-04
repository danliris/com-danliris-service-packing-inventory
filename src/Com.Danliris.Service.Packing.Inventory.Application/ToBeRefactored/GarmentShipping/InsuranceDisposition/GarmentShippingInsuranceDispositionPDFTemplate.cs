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
    public class GarmentShippingInsuranceDispositionPDFTemplate
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

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 120, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header

            Paragraph title = new Paragraph("L A M P I R A N", header_font_bold_underlined);
            title.Alignment = Element.ALIGN_CENTER;

            decimal totalPremi = viewModel.items.Sum(a => a.amount * viewModel.rate / 100);
            Phrase intro = new Phrase();
            intro.Add(new Chunk("Mohon dibayarkan uang sebesar ", normal_font));
            intro.Add(new Chunk("USD " + string.Format("{0:n2}", totalPremi), normal_font_bold));

            var terbilang = NumberToTextIDN.terbilangDollar((double)totalPremi).Contains("UD Dollar") ? NumberToTextIDN.terbilangDollar((double)totalPremi) : NumberToTextIDN.terbilangDollar((double)totalPremi) + " US Dollar";

            intro.Add(new Chunk($" (terbilang : {terbilang}) untuk pembayaran polis asuransi proteksi piutang " +
                $"dagang ke {viewModel.insurance.Name}.\n", normal_font));
            intro.Add(new Chunk("Disposisi no : " + viewModel.dispositionNo, normal_font));

            document.Add(title);
            document.Add(new Paragraph("\n", normal_font));
            document.Add(intro);
            #endregion



            #region bodyTable

            PdfPTable tableBody = new PdfPTable(8);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 3.5f, 2.5f, 3.5f, 2f, 0.2f, 2f, 0.2f, 2f });

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellCurrency = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellAmount = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellLeft.Phrase = new Phrase("Nomor Polis & Certificate", normal_font);
            tableBody.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Tgl Polis", normal_font);
            tableBody.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Buyer", normal_font);
            tableBody.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Invoice", normal_font);
            tableBody.AddCell(cellLeft);

            cellCenter.Phrase = new Phrase("Amount", normal_font);
            cellCenter.Colspan = 2;
            tableBody.AddCell(cellCenter);

            cellCenter.Phrase = new Phrase("Premi", normal_font);
            tableBody.AddCell(cellCenter);

            foreach (var item in viewModel.items)
            {
                cellLeft.Phrase = new Phrase(item.policyNo, normal_font);
                cellLeft.Colspan = 1;
                tableBody.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase(item.policyDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font);
                tableBody.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase(item.BuyerAgent.Name, normal_font);
                tableBody.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase(item.invoiceNo, normal_font);
                tableBody.AddCell(cellLeft);

                cellCurrency.Phrase = new Phrase("$", normal_font);
                tableBody.AddCell(cellCurrency);

                cellAmount.Phrase = new Phrase(string.Format("{0:n2}", item.amount), normal_font);
                tableBody.AddCell(cellAmount);

                cellCurrency.Phrase = new Phrase("$", normal_font);
                tableBody.AddCell(cellCurrency);

                cellAmount.Phrase = new Phrase(string.Format("{0:n2}", (item.amount * viewModel.rate) / 100), normal_font);
                tableBody.AddCell(cellAmount);
            }

            cellRight.Phrase = new Phrase("Total Premi", normal_font);
            cellRight.Colspan = 4;
            tableBody.AddCell(cellRight);

            cellCurrency.Phrase = new Phrase("$", normal_font_bold);
            tableBody.AddCell(cellCurrency);

            decimal totalAmount = viewModel.items.Sum(a => a.amount);
            cellAmount.Phrase = new Phrase(string.Format("{0:n2}", totalAmount), normal_font_bold);
            cellAmount.Colspan = 1;
            tableBody.AddCell(cellAmount);

            cellCurrency.Phrase = new Phrase("$", normal_font_bold);
            tableBody.AddCell(cellCurrency);

            cellAmount.Phrase = new Phrase(string.Format("{0:n2}", totalPremi), normal_font_bold);
            tableBody.AddCell(cellAmount);

            tableBody.SpacingAfter = 10;
            tableBody.SpacingBefore = 5;
            document.Add(tableBody);
            #endregion

            #region bank
            Paragraph bank = new Paragraph("Mohon ditransfer ke rekening bank sbb :\n\n", normal_font);
            document.Add(bank);

            PdfPTable tableBank = new PdfPTable(3);
            tableBank.WidthPercentage = 80;
            tableBank.SetWidths(new float[] { 3f, 0.5f, 14f });


            cellLeft.Phrase = new Phrase("AccountName", normal_font);
            cellLeft.Border = Rectangle.NO_BORDER;
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

            //#region sign
            //Paragraph date = new Paragraph($"Sukoharjo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            //document.Add(date);

            //PdfPTable tableSign = new PdfPTable(4);
            //tableSign.WidthPercentage = 100;
            //tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });

            //PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            //cellBodySignNoBorder.Phrase = new Phrase("Hormat kami,\n\n\n\n", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("Mengetahui,\n\n\n\n", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("Dicek,\n\n\n\n", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("Diterima,\n\n\n\n", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);


            //cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("(                           )", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);


            //cellBodySignNoBorder.Phrase = new Phrase("STAFF SHIPPING", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("KABAG/KASIE SHIPPING", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("BAGIAN VERIFIKASI", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);
            //cellBodySignNoBorder.Phrase = new Phrase("BAGIAN KASIR", normal_font);
            //tableSign.AddCell(cellBodySignNoBorder);

            //document.Add(tableSign);
            //#endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
