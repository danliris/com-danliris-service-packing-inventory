using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionForwarderFCPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingPaymentDispositionViewModel viewModel, List<GarmentShippingInvoiceViewModel> invoices, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 40, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header

            Paragraph title = new Paragraph("LAMPIRAN DISPOSISI PEMBAYARAN FORWARDER\n\n\n", header_font_bold);
            title.Alignment = Element.ALIGN_CENTER;

            Paragraph title1 = new Paragraph("DISPOSISI BIAYA SHIPMENT", normal_font_underlined);
            Paragraph no = new Paragraph(viewModel.dispositionNo, normal_font);
            Paragraph fwd = new Paragraph($"Invoice  {viewModel.forwarder.name}   No. : {viewModel.invoiceNumber}   Tanggal  : " +
                $"{viewModel.invoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);

            document.Add(title);
            document.Add(title1);
            document.Add(no);
            document.Add(fwd);
            document.Add(new Paragraph("\n", normal_font));
            #endregion



            #region bodyTable

            List<string> inv = new List<string>();
            List<string> como = new List<string>();
            foreach (var i in invoices)
            {
                var dupInv = como.Find(a => a == i.InvoiceNo);
                if (string.IsNullOrEmpty(dupInv))
                {
                    inv.Add(i.InvoiceNo);
                }
                foreach (var item in i.Items)
                {
                    var dupComo = como.Find(a => a == item.Comodity.Name);
                    if (string.IsNullOrEmpty(dupComo))
                    {
                        como.Add(item.Comodity.Name);
                    }
                }
            }

            decimal invTotalQty = viewModel.invoiceDetails.Sum(a => a.quantity);
            decimal totalCtns = viewModel.invoiceDetails.Sum(a => a.totalCarton);

            PdfPTable tableBody = new PdfPTable(7);
            tableBody.WidthPercentage = 80;
            tableBody.SetWidths(new float[] { 3f, 0.5f, 5f, 0.5f, 1f, 4f, 1f });

            PdfPCell cellCenterNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeftNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRightNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellLeftNoBorder1 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellLeftTopBorder = new PdfPCell() { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRightTopBorder = new PdfPCell() { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellRightNoLeftBorder = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellLeftBottomBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellLeftNoBorder.Phrase = new Phrase("Dikirim per", normal_font);
            cellLeftNoBorder.Colspan = 1;
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.sendBy, normal_font);
            cellLeftNoBorder1.Colspan = 5;
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Flight / Route", normal_font);
            cellLeftNoBorder.Colspan = 1;
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.flightVessel, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Destination To", normal_font);
            cellLeftNoBorder.Colspan = 1;
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.destination, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase(viewModel.freightBy=="AIR"?"AWB No." : "BL No.", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.freightNo, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Tgl", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.freightDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Dscription", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(string.Join(", ", como), normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Shipping Co.", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.forwarder.name, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Buyer", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.buyerAgent.Name, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("LC No.", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.paymentTerm, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            tableBody.SpacingAfter = 10;
            tableBody.SpacingBefore = 5;
            tableBody.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableBody);
            #endregion

            #region unit

            PdfPTable tableUnit = new PdfPTable(8);
            tableUnit.WidthPercentage = 100;
            tableUnit.SetWidths(new float[] { 4f, 0.5f, 4f, 4f, 4f, 4f, 4f, 4f });

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };


            cellCenter.Phrase = new Phrase("Invoice No", normal_font);
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Amount", normal_font);
            cellCenter.Colspan = 2;
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Qty", normal_font);
            cellCenter.Colspan = 1;
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Volume", normal_font);
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("G.W.", normal_font);
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Chargeable Weight", normal_font);
            tableUnit.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("Total Carton", normal_font);
            tableUnit.AddCell(cellCenter);

            foreach (var invoice in viewModel.invoiceDetails)
            {
                cellLeft.Phrase = new Phrase(invoice.invoiceNo, normal_font);
                tableUnit.AddCell(cellLeft);
                cellLeftBottomBorder.Phrase = new Phrase("$", normal_font);
                tableUnit.AddCell(cellLeftBottomBorder);
                cellRightNoLeftBorder.Phrase = new Phrase(string.Format("{0:n2}", invoice.amount), normal_font);
                tableUnit.AddCell(cellRightNoLeftBorder);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", invoice.quantity) + " PCS", normal_font);
                tableUnit.AddCell(cellRight);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", invoice.volume) + " CBM", normal_font);
                tableUnit.AddCell(cellRight);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", invoice.grossWeight) + " KGS", normal_font);
                tableUnit.AddCell(cellRight);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", invoice.chargeableWeight) + " KGS", normal_font);
                tableUnit.AddCell(cellRight);
                cellRight.Phrase = new Phrase(string.Format("{0:n2}", invoice.totalCarton) + " CTNS", normal_font);
                tableUnit.AddCell(cellRight);
            }

            tableUnit.SpacingAfter = 10;
            tableUnit.SpacingBefore = 5;
            tableUnit.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableUnit);

            #endregion

            #region bill
            document.Add(new Phrase("Realisasi biaya :", normal_font));

            PdfPTable tableBill = new PdfPTable(7);
            tableBill.WidthPercentage = 95;
            tableBill.SetWidths(new float[] { 2f, 0.5f, 7f, 0.5f, 1f, 3f, 1f });
            var last = viewModel.billDetails.Last();
            foreach (var bill in viewModel.billDetails)
            {
                cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                tableBill.AddCell(cellLeftNoBorder);
                cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                tableBill.AddCell(cellLeftNoBorder);
                cellLeftNoBorder.Phrase = new Phrase("- " +bill.billDescription, normal_font);
                tableBill.AddCell(cellLeftNoBorder);
                cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
                tableBill.AddCell(cellCenterNoBorder);
                cellLeftNoBorder.Phrase = new Phrase("IDR", normal_font);
                tableBill.AddCell(cellLeftNoBorder);
                cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", bill.amount), normal_font);
                tableBill.AddCell(cellRightNoBorder);
                if (bill == last)
                {
                    cellLeftNoBorder.Phrase = new Phrase("(+)", normal_font);
                    tableBill.AddCell(cellLeftNoBorder);
                }
                else
                {
                    cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                    tableBill.AddCell(cellLeftNoBorder);
                }
            }

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("IDR", normal_font);
            tableBill.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.billValue), normal_font);
            tableBill.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase($"- PPH {viewModel.incomeTax.name} ({viewModel.incomeTax.rate}%)", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font_bold);
            tableBill.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("IDR", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.IncomeTaxValue), normal_font_bold);
            tableBill.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(-)", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("IDR", normal_font);
            tableBill.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.billValue - viewModel.IncomeTaxValue), normal_font);
            tableBill.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("- PPN", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBill.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("IDR", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.vatValue), normal_font);
            tableBill.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(+)", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase($"Total bayar setelah PPH {viewModel.incomeTax.name} & PPN", normal_font);
            tableBill.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("IDR", normal_font_bold);
            tableBill.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.totalBill), normal_font_bold);
            tableBill.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBill.AddCell(cellLeftNoBorder);

            tableBill.SpacingAfter = 10;
            tableBill.SpacingBefore = 5;
            tableBill.HorizontalAlignment = Element.ALIGN_CENTER;
            document.Add(tableBill);
            #endregion

            var terbilang = NumberToTextIDN.terbilang((double)viewModel.totalBill) + " Rupiah";

            Paragraph trbilang = new Paragraph($"[ Terbilang : {terbilang} ]\n\n", normal_font);
            document.Add(trbilang);

            document.Add(new Paragraph("Lampiran    : " + viewModel.remark + "\n\n", normal_font));

            #region sign
            PdfPTable tableSign = new PdfPTable(3);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySignNoBorder.Phrase = new Phrase($"Solo, {DateTimeOffset.Now.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);


            cellBodySignNoBorder.Phrase = new Phrase("Hormat,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Dicheck,\n\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);
            cellBodySignNoBorder.Phrase = new Phrase("Mengetahui,\n\n\n\n", normal_font);
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
