using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(PaymentDispositionRecapViewModel viewModel, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font small_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 6);
            Font small_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);

            Document document = new Document(PageSize.A4.Rotate(), MARGIN, MARGIN, 40, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentPaymentDispositionRecapPDFTemplatePageEvent(viewModel.recapNo);
            document.Open();

            #region header

            Paragraph title = new Paragraph("LAMPIRAN DISPOSISI PEMBAYARAN REKAP EMKL\n\n", header_font_bold);

            Paragraph title1 = new Paragraph("Kepada  : Yth. Bp. Wakid. -Keuangan\n" +
                "Mohon dibayarkan kepada " + viewModel.emkl.Name + ", " + viewModel.emkl.address + " NPWP " + viewModel.emkl.npwp +
                "\n\nBiaya kirim sbb:\n\n", normal_font);
            Paragraph no = new Paragraph(viewModel.recapNo, normal_font_bold);
            document.Add(title);
            document.Add(title1);
            document.Add(no);
            #endregion

            #region table
            PdfPTable tableBody = new PdfPTable(19);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 0.4f, 1.1f, 0.8f, 1.1f, 1.1f,
                                                1f, 1.8f, 0.6f, 0.5f, 1f,
                                                0.8f, 0.8f, 0.3f, 1f, 0.8f, 0.8f,
                                                0.8f, 0.8f, 0.8f });

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellRightNoLeftBorder = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellLeftNoRightBorder = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellCenter.Phrase = new Phrase("No.", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("No. Disposisi", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Tgl Inv/Tagihan", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("No Inv/Tagihan", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Nomor Faktur Pajak", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Invoice", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Buyer", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Volume", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Unit", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Amount", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Jasa", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("PPH", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Terbayar", small_font);
            cellCenter.Colspan = 2;
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("1A", small_font);
            cellCenter.Colspan = 1;
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("1B", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("2A", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("2B", small_font);
            tableBody.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("2C", small_font);
            tableBody.AddCell(cellCenter);

            var idx = 0;
            double total1A = 0;
            double total1B = 0;
            double total2A = 0;
            double total2B = 0;
            double total2C = 0;

            foreach (var item in viewModel.items)
            {
                total1A += item.paymentDisposition.amountPerUnit.ContainsKey("C1A") ? item.paymentDisposition.amountPerUnit["C1A"] : 0;
                total1B += item.paymentDisposition.amountPerUnit.ContainsKey("C1B") ? item.paymentDisposition.amountPerUnit["C1B"] : 0;
                total2A += item.paymentDisposition.amountPerUnit.ContainsKey("C2A") ? item.paymentDisposition.amountPerUnit["C2A"] : 0;
                total2B += item.paymentDisposition.amountPerUnit.ContainsKey("C2B") ? item.paymentDisposition.amountPerUnit["C2B"] : 0;
                total2C += item.paymentDisposition.amountPerUnit.ContainsKey("C2C") ? item.paymentDisposition.amountPerUnit["C2C"] : 0;
                idx++;
                string invNo = "";
                foreach (var detail in item.paymentDisposition.invoiceDetails)
                {
                    if (invNo != item.paymentDisposition.dispositionNo)
                    {

                        cellCenter.Phrase = new Phrase(idx.ToString(), small_font);
                        tableBody.AddCell(cellCenter);
                        cellLeft.Phrase = new Phrase(item.paymentDisposition.dispositionNo, small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(item.paymentDisposition.invoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMM yy", new System.Globalization.CultureInfo("id-ID")), small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(item.paymentDisposition.invoiceNumber, small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(item.paymentDisposition.invoiceTaxNumber, small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(detail.invoiceNo, small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(detail.invoice.BuyerAgent.Name, small_font);
                        tableBody.AddCell(cellLeft);
                        cellRight.Phrase = new Phrase(string.Format("{0:n2}", detail.packingList.totalCBM), small_font);
                        tableBody.AddCell(cellRight);
                        cellLeft.Phrase = new Phrase(string.Join(", ", detail.invoice.items.Select(s => s.unit).Distinct()), small_font);
                        tableBody.AddCell(cellLeft);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amount.ToString("N0"), small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.service.ToString("N0"), small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.incomeTaxValue.ToString("N0"), small_font);
                        tableBody.AddCell(cellRight);
                        cellLeftNoRightBorder.Phrase = new Phrase("Rp ", small_font);
                        tableBody.AddCell(cellLeftNoRightBorder);
                        cellRightNoLeftBorder.Phrase = new Phrase(item.paymentDisposition.paid.ToString("N0"), small_font);
                        tableBody.AddCell(cellRightNoLeftBorder);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amountPerUnit.ContainsKey("C1A") ? item.paymentDisposition.amountPerUnit["C1A"].ToString("N0") : "", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amountPerUnit.ContainsKey("C1B") ? item.paymentDisposition.amountPerUnit["C1B"].ToString("N0") : "", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amountPerUnit.ContainsKey("C2A") ? item.paymentDisposition.amountPerUnit["C2A"].ToString("N0") : "", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amountPerUnit.ContainsKey("C2B") ? item.paymentDisposition.amountPerUnit["C2B"].ToString("N0") : "", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase(item.paymentDisposition.amountPerUnit.ContainsKey("C2C") ? item.paymentDisposition.amountPerUnit["C2C"].ToString("N0") : "", small_font);
                        tableBody.AddCell(cellRight);
                    }
                    else
                    {
                        cellCenter.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellCenter);
                        cellLeft.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(detail.invoiceNo, small_font);
                        tableBody.AddCell(cellLeft);
                        cellLeft.Phrase = new Phrase(detail.invoice.BuyerAgent.Name, small_font);
                        tableBody.AddCell(cellLeft);
                        cellRight.Phrase = new Phrase(string.Format("{0:n2}", detail.packingList.totalCBM), small_font);
                        tableBody.AddCell(cellRight);
                        cellLeft.Phrase = new Phrase(string.Join(", ", detail.invoice.items.Select(s => s.unit).Distinct()), small_font);
                        tableBody.AddCell(cellLeft);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellLeftNoRightBorder.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellLeftNoRightBorder);
                        cellRightNoLeftBorder.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRightNoLeftBorder);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);
                        cellRight.Phrase = new Phrase("", small_font);
                        tableBody.AddCell(cellRight);

                    }
                    invNo = item.paymentDisposition.dispositionNo;

                }
            }

            double totalPaid = (double)viewModel.items.Sum(s => s.paymentDisposition.paid);
            var terbilang = NumberToTextIDN.terbilang(totalPaid) + " rupiah";

            decimal totalAmount = viewModel.items.Sum(s => s.paymentDisposition.amount);
            double totalService = viewModel.items.Sum(s => s.service);
            decimal totalIncomeTax = viewModel.items.Sum(s => s.paymentDisposition.incomeTaxValue);

            cellRight.Phrase = new Phrase(totalAmount.ToString("N0"), small_font);
            cellRight.Colspan = 10;
            tableBody.AddCell(cellRight);
            cellRight.Phrase = new Phrase(totalService.ToString("N0"), small_font);
            cellRight.Colspan = 1;
            tableBody.AddCell(cellRight);
            cellRight.Phrase = new Phrase(totalIncomeTax.ToString("N0"), small_font);
            cellRight.Colspan = 1;
            tableBody.AddCell(cellRight);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableBody.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(totalPaid.ToString("N0"), small_font);
            tableBody.AddCell(cellRightNoLeftBorder);
            cellRight.Phrase = new Phrase("", small_font);
            cellRight.Colspan = 5;
            tableBody.AddCell(cellRight);

            cellLeft.Phrase = new Phrase("Terbilang : " + terbilang, small_font_bold);
            cellLeft.Colspan = 19;
            tableBody.AddCell(cellLeft);

            tableBody.HeaderRows = 1;
            tableBody.SpacingBefore = 10;
            document.Add(tableBody);
            #endregion

            #region unit

            PdfPTable tableUnit = new PdfPTable(3);
            tableUnit.WidthPercentage = 30;
            tableUnit.SetWidths(new float[] { 1f, 0.2f, 1f });

            cellCenter.Phrase = new Phrase("BEBAN UNIT", normal_font_bold);
            cellCenter.Colspan = 3;
            tableUnit.AddCell(cellCenter);

            cellLeft.Phrase = new Phrase("1A", small_font);
            cellLeft.Colspan = 1;
            tableUnit.AddCell(cellLeft);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableUnit.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(total1A.ToString("N0"), small_font);
            tableUnit.AddCell(cellRightNoLeftBorder);

            cellLeft.Phrase = new Phrase("1B", small_font);
            tableUnit.AddCell(cellLeft);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableUnit.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(total1B.ToString("N0"), small_font);
            tableUnit.AddCell(cellRightNoLeftBorder);

            cellLeft.Phrase = new Phrase("2A", small_font);
            tableUnit.AddCell(cellLeft);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableUnit.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(total2A.ToString("N0"), small_font);
            tableUnit.AddCell(cellRightNoLeftBorder);

            cellLeft.Phrase = new Phrase("2B", small_font);
            tableUnit.AddCell(cellLeft);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableUnit.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(total2B.ToString("N0"), small_font);
            tableUnit.AddCell(cellRightNoLeftBorder);

            cellLeft.Phrase = new Phrase("2C", small_font);
            tableUnit.AddCell(cellLeft);
            cellLeftNoRightBorder.Phrase = new Phrase("Rp", small_font);
            tableUnit.AddCell(cellLeftNoRightBorder);
            cellRightNoLeftBorder.Phrase = new Phrase(total2C.ToString("N0"), small_font);
            tableUnit.AddCell(cellRightNoLeftBorder);

            tableUnit.SpacingAfter = 10;
            tableUnit.SpacingBefore = 5;
            tableUnit.HorizontalAlignment = Element.ALIGN_RIGHT;
            document.Add(tableUnit);

            #endregion

            #region sign
            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySignNoBorder.Phrase = new Phrase($"Solo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("Hormat,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Mengetahui,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Dicek,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Kasir,\n\n\n\n", normal_font);
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

    class GarmentPaymentDispositionRecapPDFTemplatePageEvent : PdfPageEventHelper
    {
        string no;
        public GarmentPaymentDispositionRecapPDFTemplatePageEvent(string no)
        {
            this.no = no;
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin, marginTop = document.TopMargin, marginRight = document.RightMargin, marginBottom = document.BottomMargin;

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region INFO

            var infoY = height - marginTop + 10;
            if(writer.PageNumber!=1)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, no, marginLeft, infoY, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page " + writer.PageNumber, width - marginRight, infoY, 0);

            #endregion


            cb.EndText();
        }
    }
}
