using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
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
    public class GarmentShippingPaymentDispositionEMKLPDFTemplate
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

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region header

            Paragraph title = new Paragraph("LAMPIRAN DISPOSISI PEMBAYARAN EMKL", header_font_bold_underlined);
            title.Alignment = Element.ALIGN_CENTER;

            Paragraph title1 = new Paragraph("DISPOSISI BIAYA SHIPMENT", normal_font_underlined);
            Paragraph no = new Paragraph(viewModel.dispositionNo, normal_font);
            document.Add(title);
            document.Add(title1);
            document.Add(no);
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

            cellLeftNoBorder.Phrase = new Phrase("Subject", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.invoiceNumber, normal_font);
            cellLeftNoBorder1.Colspan = 5;
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Dikirim per", normal_font);
            cellLeftNoBorder.Colspan = 1;
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.sendBy, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("EMKL", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.emkl.Name, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Material", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(string.Join(", ", como), normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Pembeli", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.buyerAgent.Name, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("LC No", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(viewModel.paymentTerm, normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Invoice", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase(string.Join(", ", inv), normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Party", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase($"{string.Format("{0:n2}", invTotalQty)} pcs / {string.Format("{0:n2}", totalCtns)} ctns =  cbm", normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            cellLeftNoBorder.Phrase = new Phrase("Biaya", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder1.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder1);

            var last = viewModel.billDetails.Last();
            foreach (var bill in viewModel.billDetails)
            {
                cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                tableBody.AddCell(cellLeftNoBorder);
                cellLeftNoBorder.Phrase = new Phrase("", normal_font);
                tableBody.AddCell(cellLeftNoBorder);
                cellLeftNoBorder.Phrase = new Phrase(bill.billDescription, normal_font);
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

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftTopBorder.Phrase = new Phrase("RP", normal_font);
            tableBody.AddCell(cellLeftTopBorder);
            cellRightTopBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.billValue), normal_font);
            tableBody.AddCell(cellRightTopBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase($"PPH {viewModel.incomeTax.name} ({viewModel.incomeTax.rate}%)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font_bold);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("RP", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.IncomeTaxValue), normal_font_bold);
            tableBody.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(-)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
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

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("PPN", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase(":", normal_font);
            tableBody.AddCell(cellCenterNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("RP", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", viewModel.vatValue), normal_font);
            tableBody.AddCell(cellRightNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("(+)", normal_font_bold);
            tableBody.AddCell(cellLeftNoBorder);

            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellLeftNoBorder.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(cellLeftNoBorder);
            cellCenterNoBorder.Phrase = new Phrase("", normal_font_bold);
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

            Paragraph trbilang = new Paragraph($"[ Terbilang : {terbilang} ]\n\n", normal_font);
            document.Add(trbilang);
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
