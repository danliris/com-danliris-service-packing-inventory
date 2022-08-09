using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceCMTWithHeaderPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingInvoiceViewModel viewModel, Buyer buyer, BankAccount bank, GarmentPackingListViewModel pl, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 90, 80);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentShippingInvoiceCMTWithHeaderPDFTemplatePageEvent(viewModel, timeoffset);

            document.Open();

            #region LC
            PdfPTable tableLC = new PdfPTable(3);
            tableLC.SetWidths(new float[] { 2f, 0.1f, 6f });

            if (pl.PaymentTerm == "LC")
            {
                PdfPCell cellLCContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentLeft.AddElement(new Phrase("LETTER OF CREDIT NUMBER ", normal_font));
                cellLCContentLeft.AddElement(new Phrase("LC DATE ", normal_font));
                cellLCContentLeft.AddElement(new Phrase("ISSUED BY ", normal_font));
                tableLC.AddCell(cellLCContentLeft);

                PdfPCell cellLCContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentCenter.AddElement(new Phrase(": ", normal_font));
                cellLCContentCenter.AddElement(new Phrase(": ", normal_font));
                cellLCContentCenter.AddElement(new Phrase(": ", normal_font));
                tableLC.AddCell(cellLCContentCenter);

                PdfPCell cellLCContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentRight.AddElement(new Phrase(viewModel.LCNo, normal_font));
                cellLCContentRight.AddElement(new Phrase(pl.LCDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));
                cellLCContentRight.AddElement(new Phrase(viewModel.IssuedBy, normal_font));
                tableLC.AddCell(cellLCContentRight);
            }
            else
            {
                PdfPCell cellLCContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentLeft.AddElement(new Phrase("PAYMENT TERM ", normal_font));
                tableLC.AddCell(cellLCContentLeft);

                PdfPCell cellLCContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentCenter.AddElement(new Phrase(": ", normal_font));
                tableLC.AddCell(cellLCContentCenter);

                PdfPCell cellLCContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellLCContentRight.AddElement(new Phrase("TT PAYMENT", normal_font));
                tableLC.AddCell(cellLCContentRight);
            }

            PdfPCell cellLC = new PdfPCell(tableLC);
            tableLC.ExtendLastRow = false;
            tableLC.SpacingAfter = 4f;
            document.Add(tableLC);
            #endregion

            #region Body Table

            PdfPTable bodyTable = new PdfPTable(10);
            float[] bodyTableWidths = new float[] { 1.7f, 1.7f, 1.7f, 1.7f, 0.7f, 1.1f, 1.5f, 1.7f, 1.5f, 1.7f };
            bodyTable.SetWidths(bodyTableWidths);
            bodyTable.WidthPercentage = 100;

            #region Set Body Table Header
            PdfPCell bodyTableHeader = new PdfPCell();// { FixedHeight = 20 };
            //PdfPCell table1RightCellHeader = new PdfPCell() { FixedHeight = 20, Colspan = 4 };

            bodyTableHeader.Phrase = new Phrase("DESCRIPTION", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.SetLeading(0, 1.3f);
            //bodyTableHeader.Rowspan = 2;
            bodyTableHeader.Colspan = 4;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("QUANTITY", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.Colspan = 2;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("UNIT PRICE\n" + viewModel.CPrice + " IN USD", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.Rowspan = 1;
            bodyTableHeader.Colspan = 1;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("TOTAL PRICE\n" + viewModel.CPrice + " IN USD", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("UNIT PRICE\n" + "CMT IN USD", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("TOTAL PRICE\n" + "CMT IN USD", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            //bodyTableHeader.Phrase = new Phrase("", normal_font);
            //bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            ////bodyTableHeader.Rowspan = 2;
            //bodyTableHeader.Colspan = 4;
            //bodyTable.AddCell(bodyTableHeader);

            //bodyTableHeader.Phrase = new Phrase("", normal_font);
            //bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.Colspan = 2;
            //bodyTable.AddCell(bodyTableHeader);

            //bodyTableHeader.Phrase = new Phrase(viewModel.CPrice + " IN USD", normal_font);
            //bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.Colspan = 2;
            //bodyTable.AddCell(bodyTableHeader);

            //bodyTableHeader.Phrase = new Phrase("CMT IN USD", normal_font);
            //bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableHeader.Colspan = 2;
            //bodyTable.AddCell(bodyTableHeader);
            #endregion

            #region Set Body Table Value
            PdfPCell bodyTableCellRightBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.RIGHT_BORDER };
            PdfPCell bodyTableCellLeftBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.LEFT_BORDER };
            PdfPCell bodyTableCellCenterBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER };
            PdfPCell bodyTableCellNoBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER };

            bodyTableCellLeftBorder.Phrase = new Phrase(viewModel.Description, body_font);
            bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.SetLeading(0, 1.3f);
            bodyTableCellLeftBorder.Colspan = 4;
            bodyTable.AddCell(bodyTableCellLeftBorder);

            bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
            bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.Colspan = 2;
            bodyTable.AddCell(bodyTableCellLeftBorder);

            bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellCenterBorder.SetLeading(0, 1.3f);
            bodyTable.AddCell(bodyTableCellCenterBorder);

            bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellRightBorder.SetLeading(0, 1.3f);
            bodyTable.AddCell(bodyTableCellRightBorder);

            bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellCenterBorder);

            bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellRightBorder);

            //SPACE
            //bodyTableCellLeftBorder.Phrase = new Phrase(".", body_font);
            //bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
            //bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellLeftBorder.Colspan = 4;
            //bodyTable.AddCell(bodyTableCellLeftBorder);

            //bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
            //bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellLeftBorder.Colspan = 2;
            //bodyTable.AddCell(bodyTableCellLeftBorder);

            //bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            //bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTable.AddCell(bodyTableCellCenterBorder);

            //bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            //bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTable.AddCell(bodyTableCellRightBorder);

            //bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            //bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTable.AddCell(bodyTableCellCenterBorder);

            //bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            //bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            //bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            //bodyTable.AddCell(bodyTableCellRightBorder);

            //
            bodyTableCellLeftBorder.Phrase = new Phrase(viewModel.Remark, body_font);
            bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.Colspan = 4;
            bodyTable.AddCell(bodyTableCellLeftBorder);

            bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
            bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellLeftBorder.Colspan = 2;
            bodyTable.AddCell(bodyTableCellLeftBorder);

            bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellCenterBorder);

            bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellRightBorder);

            bodyTableCellCenterBorder.Phrase = new Phrase("", body_font);
            bodyTableCellCenterBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellCenterBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellCenterBorder);

            bodyTableCellRightBorder.Phrase = new Phrase("", body_font);
            bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellRightBorder);

            decimal totalAmount = 0;
            double totalQuantity = 0;
            double totalCMTPrice = 0;
            decimal totalPrice = 0;
            Dictionary<string, double> total = new Dictionary<string, double>();
            foreach (var item in viewModel.Items)
            {
                totalAmount += item.Amount;
                totalQuantity += item.Quantity;
                totalCMTPrice += item.Quantity * (double)item.CMTPrice;
                if (item.CMTPrice > 0)
                {
                    totalPrice += item.Amount;
                }

                bodyTableCellLeftBorder.Phrase = new Phrase(item.ComodityDesc, body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellNoBorder.Phrase = new Phrase(item.Desc2, body_font);
                bodyTableCellNoBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellNoBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellNoBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellNoBorder);

                bodyTableCellNoBorder.Phrase = new Phrase(item.Desc3, body_font);
                bodyTableCellNoBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellNoBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellNoBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellNoBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Desc4, body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellRightBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellLeftBorder.Phrase = new Phrase(string.Format("{0:n0}", item.Quantity), body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.BorderColorRight = BaseColor.White;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Uom.Unit, body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellRightBorder.BorderColorLeft = BaseColor.White;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Price == 0 ? "" : string.Format("{0:n4}", item.Price), body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Amount == 0 ? "" : string.Format("{0:n2}", item.Amount), body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.CMTPrice != 0 ? string.Format("{0:n4}", item.CMTPrice) : "", body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.CMTPrice != 0 ? string.Format("{0:n2}", item.Quantity * (double)item.CMTPrice) : "", body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCellRightBorder);

                if (total.ContainsKey(item.Uom.Unit))
                {
                    total[item.Uom.Unit] += item.Quantity;
                }
                else
                {
                    total.Add(item.Uom.Unit, item.Quantity);
                }
            }
            PdfPCell bodyTableCellFooter = new PdfPCell() { FixedHeight = 20, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER };

            bodyTableCellFooter.Phrase = new Phrase("TOTAL  ", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Colspan = 4;
            bodyTable.AddCell(bodyTableCellFooter);

            var val1 = total.Select(x => String.Format("{0:n0}", x.Value));
            var result1 = String.Join("\n", val1);

            var key1 = total.Select(x => String.Format("{0}", x.Key));
            var result2 = String.Join("\n", key1);

            bodyTableCellFooter.Phrase = new Phrase($"{result1}", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Colspan = 1;
            bodyTableCellFooter.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);


            bodyTableCellFooter.Phrase = new Phrase(result2, body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase("", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase(totalAmount == 0 ? "" : string.Format("{0:n2}", totalAmount), body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase("", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase(totalCMTPrice != 0 ? string.Format("{0:n2}", totalCMTPrice) : "", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);

            #endregion
            bodyTable.HeaderRows = 1;
            document.Add(bodyTable);
            #endregion

            #region calculationTable
            PdfPTable calculationTable = new PdfPTable(4);
            calculationTable.HorizontalAlignment = Element.ALIGN_LEFT;
            float[] calculationTableWidths = new float[] { 4f, 0.8f, 1.3f, 6f };
            calculationTable.SetWidths(calculationTableWidths);
            calculationTable.WidthPercentage = 100;

            PdfPCell calculationCellRight = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell calculationCellLeft = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            calculationCellLeft.Phrase = new Phrase("TOTAL AMOUNT FOB ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellLeft.Phrase = new Phrase(": USD ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellRight.Phrase = new Phrase(string.Format("{0:n2}", totalAmount), normal_font);
            calculationTable.AddCell(calculationCellRight);
            calculationCellRight.Phrase = new Phrase("", normal_font);
            calculationTable.AddCell(calculationCellRight);


            calculationCellLeft.Phrase = new Phrase("LESS FABRIC COST ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellLeft.Phrase = new Phrase(": USD ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellRight.Phrase = new Phrase(string.Format("{0:n2}", (totalPrice - (decimal)totalCMTPrice) * -1), normal_font);
            calculationTable.AddCell(calculationCellRight);
            calculationCellRight.Phrase = new Phrase("", normal_font);
            calculationTable.AddCell(calculationCellRight);

            decimal totalAmountCMT = totalAmount - (totalPrice - (decimal)totalCMTPrice);
            calculationCellLeft.Phrase = new Phrase("TOTAL AMOUNT CMT ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellLeft.Phrase = new Phrase(": USD ", normal_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellRight.Phrase = new Phrase(string.Format("{0:n2}", totalAmountCMT), normal_font);
            calculationTable.AddCell(calculationCellRight);
            calculationCellRight.Phrase = new Phrase("", normal_font);
            calculationTable.AddCell(calculationCellRight);

            decimal totalPaid = totalAmountCMT;
            if (viewModel.GarmentShippingInvoiceAdjustments.Count > 0)
            {
                foreach (var adj in viewModel.GarmentShippingInvoiceAdjustments)
                {
                    totalPaid += adj.AdjustmentValue;
                    calculationCellLeft.Phrase = new Phrase($"{adj.AdjustmentDescription} ", normal_font);
                    calculationTable.AddCell(calculationCellLeft);
                    calculationCellLeft.Phrase = new Phrase(": USD ", normal_font);
                    calculationTable.AddCell(calculationCellLeft);
                    calculationCellRight.Phrase = new Phrase(string.Format("{0:n2}", adj.AdjustmentValue), normal_font);
                    calculationTable.AddCell(calculationCellRight);
                    calculationCellRight.Phrase = new Phrase("", normal_font);
                    calculationTable.AddCell(calculationCellRight);
                }
            }

            calculationCellLeft.Phrase = new Phrase($"TOTAL AMOUNT TO BE PAID ", bold_font);
            calculationCellLeft.Border = Rectangle.TOP_BORDER;
            calculationTable.AddCell(calculationCellLeft);
            calculationCellLeft.Phrase = new Phrase(": USD ", bold_font);
            calculationTable.AddCell(calculationCellLeft);
            calculationCellRight.Phrase = new Phrase(string.Format("{0:n2}", totalPaid), bold_font);
            calculationCellRight.Border = Rectangle.TOP_BORDER;
            calculationTable.AddCell(calculationCellRight);
            calculationCellRight.Phrase = new Phrase("", bold_font);
            calculationCellRight.Border = Rectangle.NO_BORDER;
            calculationTable.AddCell(calculationCellRight);

            string amountToText = "";
            if (totalPaid < 0)
            {
                totalPaid = totalPaid * -1;
                //amountToText = "MINUS " + NumberToTextEN.toWords((double)totalPaid);
                amountToText = "MINUS " + CurrencyToText.ToWords(totalPaid);
            }
            else
            {
                //amountToText = NumberToTextEN.toWords((double)totalPaid);
                amountToText = CurrencyToText.ToWords(totalPaid);
            }
            calculationCellLeft.Phrase = new Phrase($"SAY : US DOLLARS {amountToText.ToUpper()} ONLY ///", normal_font);
            calculationCellLeft.Colspan = 4;
            calculationCellLeft.Border = Rectangle.NO_BORDER;
            calculationTable.AddCell(calculationCellLeft);

            document.Add(calculationTable);
            #endregion

            document.Add(new Paragraph("\n", normal_font));

            if (bank != null)
            {
                document.Add(new Paragraph("PLEASE TT THE ABOVE PAYMENT TO OUR CORRESPONDENCE BANK AS FOLLOW   : ", normal_font));

                document.Add(new Paragraph(bank.bankName, normal_font));
                document.Add(new Paragraph(bank.bankAddress, normal_font));
                document.Add(new Paragraph("ACC NO. " + bank.AccountNumber + $"({bank.Currency.Code})", normal_font));
                document.Add(new Paragraph("A/N. PT. DAN LIRIS", normal_font));
                document.Add(new Paragraph("SWIFT CODE : " + bank.swiftCode, normal_font));
                document.Add(new Paragraph("PURPOSE CODE : 1011", normal_font));
                document.Add(new Paragraph("\n", normal_font));
            }

            #region MARK
            PdfPTable tableMark = new PdfPTable(2);
            tableMark.SetWidths(new float[] { 2f, 4f });
            tableMark.WidthPercentage = 100;

            PdfPCell cellMarkContent = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellMarkContent.AddElement(new Phrase("SHIPPING MARKS :", normal_font_underlined));
            cellMarkContent.AddElement(new Phrase(pl.ShippingMark, normal_font));
            tableMark.AddCell(cellMarkContent);

            PdfPCell cellMarkContentR = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellMarkContentR.AddElement(new Phrase(pl.SideMark != null ? "SIDE MARKS :" : "", normal_font_underlined));
            cellMarkContentR.AddElement(new Phrase(pl.SideMark != null ? pl.SideMark : "", normal_font));
            tableMark.AddCell(cellMarkContentR);

            tableMark.ExtendLastRow = false;
            tableMark.SpacingAfter = 15f;
            document.Add(tableMark);

            //
            PdfPTable tableMark1 = new PdfPTable(2);
            tableMark1.SetWidths(new float[] { 2f, 4f });
            tableMark1.WidthPercentage = 100;
            var noImage = "data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAA0NDQ0ODQ4QEA4UFhMWFB4bGRkbHi0gIiAiIC1EKjIqKjIqRDxJOzc7STxsVUtLVWx9aWNpfZeHh5e+tb75+f8BDQ0NDQ4NDhAQDhQWExYUHhsZGRseLSAiICIgLUQqMioqMipEPEk7NztJPGxVS0tVbH1pY2l9l4eHl761vvn5///CABEIAAoACgMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAAB//aAAgBAQAAAACnD//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIQAAAAf//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMQAAAAf//EABQQAQAAAAAAAAAAAAAAAAAAACD/2gAIAQEAAT8AH//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIBAT8Af//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMBAT8Af//Z";
            byte[] shippingMarkImage;

            if (String.IsNullOrEmpty(pl.ShippingMarkImageFile))
            {
                pl.ShippingMarkImageFile = noImage;
            }

            if (IsBase64String(Base64.GetBase64File(pl.ShippingMarkImageFile)))
            {
                shippingMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.ShippingMarkImageFile));
                Image shipMarkImage = Image.GetInstance(imgb: shippingMarkImage);

                if (shipMarkImage.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 100 / shipMarkImage.Width;
                    shipMarkImage.ScalePercent(percentage * 100);
                }

                PdfPCell shipMarkImageCell = new PdfPCell(shipMarkImage);
                shipMarkImageCell.Border = Rectangle.NO_BORDER;
                tableMark1.AddCell(shipMarkImageCell);
            }

            byte[] sideMarkImage;

            if (String.IsNullOrEmpty(pl.SideMarkImageFile))
            {
                pl.SideMarkImageFile = noImage;
            }

            if (IsBase64String(Base64.GetBase64File(pl.SideMarkImageFile)))
            {
                sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.SideMarkImageFile));
                Image _sideMarkImage = Image.GetInstance(imgb: sideMarkImage);

                if (_sideMarkImage.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 100 / _sideMarkImage.Width;
                    _sideMarkImage.ScalePercent(percentage * 100);
                }

                PdfPCell _sideMarkImageCell = new PdfPCell(_sideMarkImage);
                _sideMarkImageCell.Border = Rectangle.NO_BORDER;
                tableMark1.AddCell(_sideMarkImageCell);
            }

            new PdfPCell(tableMark1);
            tableMark1.ExtendLastRow = false;
            tableMark1.SpacingAfter = 5f;
            document.Add(tableMark1);

            //

            #endregion


            #region Weight
            PdfPTable tableMeasurement = new PdfPTable(3);
            tableMeasurement.SetWidths(new float[] { 2f, 0.2f, 12f });
            PdfPCell cellMeasurement = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellMeasurement.Phrase = new Phrase("GROSS WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(String.Format("{0:0.00}", pl.GrossWeight) + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(String.Format("{0:0.00}", pl.NettWeight) + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("MEASUREMENT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            PdfPTable tableMeasurementDetail = new PdfPTable(5);
            tableMeasurementDetail.SetWidths(new float[] { 2f, 2f, 2f, 2f, 2f });
            PdfPCell cellMeasurementDetail = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            decimal totalCbm = 0;
            foreach (var measurement in pl.Measurements)
            {
                cellMeasurementDetail.Phrase = new Phrase(String.Format("{0:0.00}", measurement.Length) + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(String.Format("{0:0.00}", measurement.Width) + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(String.Format("{0:0.00}", measurement.Height) + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.CartonsQuantity + " CTNS = ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                var cbm = (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000;
                totalCbm += cbm;
                cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N2} CBM", cbm), normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
            }

            cellMeasurementDetail.Border = Rectangle.TOP_BORDER;
            cellMeasurementDetail.Phrase = new Phrase("", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase("TOTAL", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase(pl.Measurements.Sum(m => m.CartonsQuantity) + " CTNS .", normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);
            cellMeasurementDetail.Phrase = new Phrase(string.Format("{0:N2} CBM", totalCbm), normal_font);
            tableMeasurementDetail.AddCell(cellMeasurementDetail);

            new PdfPCell(tableMeasurementDetail);
            tableMeasurementDetail.ExtendLastRow = false;
            var paddingRight = 200;
            tableMeasurement.AddCell(new PdfPCell(tableMeasurementDetail) { Border = Rectangle.NO_BORDER, PaddingRight = paddingRight });

            new PdfPCell(tableMeasurement);
            tableMeasurement.ExtendLastRow = false;
            tableMeasurement.SpacingAfter = 5f;
            document.Add(tableMeasurement);
            #endregion

            #region REMARK
            if (pl.Remark != null)
            {
                PdfPTable tableRemark = new PdfPTable(1);
                tableRemark.SetWidths(new float[] { 6f });
                tableRemark.WidthPercentage = 100;

                PdfPCell cellRemarkContent = new PdfPCell() { Border = Rectangle.NO_BORDER };
                cellRemarkContent.AddElement(new Phrase("REMARK :", normal_font_underlined));
                cellRemarkContent.AddElement(new Phrase(pl.Remark, normal_font));
                tableRemark.AddCell(cellRemarkContent);

                tableRemark.ExtendLastRow = false;
                tableRemark.SpacingAfter = 10f;
                document.Add(tableRemark);

                //
                PdfPTable tableRemark2 = new PdfPTable(1);
                tableRemark2.SetWidths(new float[] { 6f });
                tableRemark2.WidthPercentage = 100;
                byte[] shippingRemarkImage;

                if (String.IsNullOrEmpty(pl.RemarkImageFile))
                {
                    pl.RemarkImageFile = noImage;
                }

                if (IsBase64String(Base64.GetBase64File(pl.RemarkImageFile)))
                {
                    shippingRemarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.RemarkImageFile));
                    Image shipRemarkImage = Image.GetInstance(imgb: shippingRemarkImage);

                    if (shipRemarkImage.Width > 60)
                    {
                        float percentage = 0.0f;
                        percentage = 100 / shipRemarkImage.Width;
                        shipRemarkImage.ScalePercent(percentage * 100);
                    }

                    PdfPCell shipRemarkImageCell = new PdfPCell(shipRemarkImage);
                    shipRemarkImageCell.Border = Rectangle.NO_BORDER;
                    shipRemarkImageCell.Colspan = 3;
                    tableRemark2.AddCell(shipRemarkImageCell);
                }

                new PdfPCell(tableRemark2);
                tableRemark2.ExtendLastRow = false;
                tableRemark2.SpacingAfter = 5f;
                document.Add(tableRemark2);
            }
            //         
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }

    class GarmentShippingInvoiceCMTWithHeaderPDFTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
    {
        private GarmentShippingInvoiceViewModel viewModel;
        private int timeoffset;

        public GarmentShippingInvoiceCMTWithHeaderPDFTemplatePageEvent(GarmentShippingInvoiceViewModel viewModel, int timeoffset)
        {
            this.viewModel = viewModel;
            this.timeoffset = timeoffset;
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
            Font big_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14, Font.BOLD);
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7, Font.UNDERLINE);

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

            cb.SetFontAndSize(bf, 7);


            #region LOGODL

            byte[] imageByteDL = Convert.FromBase64String(Base64ImageStrings.LOGO_DANLIRIS_58_58);
            Image imageDL = Image.GetInstance(imageByteDL);
            if (imageDL.Width > 60)
            {
                float percentage = 0.0f;
                percentage = 60 / imageDL.Width;
                imageDL.ScalePercent(percentage * 100);
            }
            imageDL.SetAbsolutePosition(marginLeft, height - imageDL.ScaledHeight - marginTop + 60);
            cb.AddImage(imageDL, inlineImage: true);

            #endregion

            #region ADDRESS

            var headOfficeX = width / 2 + 30;
            var headOfficeY = height - marginTop + 45;

            var branchOfficeY = height - marginTop + 50;

            byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
            Image image1 = Image.GetInstance(imageByte);
            if (image1.Width > 100)
            {
                float percentage = 0.0f;
                percentage = 100 / image1.Width;
                image1.ScalePercent(percentage * 100);
            }
            image1.SetAbsolutePosition(marginLeft + 80, height - image1.ScaledHeight - marginTop + 75);
            cb.AddImage(image1, inlineImage: true);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Head Office : Jl. Merapi No. 23", marginLeft + 80, branchOfficeY, 0);
            string[] branchOffices = {
                "Banaran, Grogol, Sukoharjo 57552",
                "Central Java, Indonesia",
                "Tel. : (+62-271) 740888, 714400",
                "Fax. : (+62-271) 740777, 735222",
                "PO BOX 166 Solo, 57100",
                "Website : www.danliris.com",
            };
            for (int i = 0; i < branchOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, branchOffices[i], marginLeft + 80, branchOfficeY - 10 - (i * 10), 0);
            }

            #endregion

            #region LOGOISO

            byte[] imageByteIso = Convert.FromBase64String(Base64ImageStrings.ISO);
            Image imageIso = Image.GetInstance(imageByteIso);
            if (imageIso.Width > 100)
            {
                float percentage = 0.0f;
                percentage = 100 / imageIso.Width;
                imageIso.ScalePercent(percentage * 100);
            }
            imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 60);
            cb.AddImage(imageIso, inlineImage: true);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight, height - imageIso.ScaledHeight - marginTop + 60 - 5, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop - 15);
            cb.LineTo(width - marginRight, height - marginTop - 15);
            cb.Stroke();

            cb.MoveTo(marginLeft, height - marginTop - 18);
            cb.LineTo(width - marginRight, height - marginTop - 18);
            cb.Stroke();

            #endregion

            #region Table
            PdfPTable table = new PdfPTable(1);

            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]

            PdfPTable tabledetailOrders = new PdfPTable(3);
            tabledetailOrders.SetWidths(new float[] { 0.6f, 1.4f, 2f });

            PdfPCell cellDetailContentLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER };
            PdfPCell cellDetailContentRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            PdfPCell cellDetailContentRight2 = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            PdfPCell cellDetailContentCenter = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER };
            PdfPCell cellDetailContentCenter2 = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER };

            PdfPCell cellHeaderContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellHeaderContentLeft.AddElement(new Phrase("                                                  COMMERCIAL INVOICE", big_font));
            cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentLeft.AddElement(new Phrase("Invoice No.  :  " + viewModel.InvoiceNo + "                                                                           Date  :  " + viewModel.InvoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("MMM dd, yyyy.", new System.Globalization.CultureInfo("en-EN")) + "                                                             Page  : " + (writer.PageNumber), normal_font));
            cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentLeft.Colspan = 3;
            tabledetailOrders.AddCell(cellHeaderContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF", normal_font);
            cellDetailContentLeft.Colspan = 2;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            tabledetailOrders.AddCell(cellDetailContentLeft);


            PdfPTable tabledetailOrders2 = new PdfPTable(3);
            tabledetailOrders2.SetWidths(new float[] { 1.5f, 0.2f, 1.5f });

            cellDetailContentLeft.Phrase = new Phrase("CO NO.", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.TOP_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.CO, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.TOP_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("CONFIRMATION OF ORDER NO.", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.ConfirmationOfOrderNo, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("SHIPPED PER", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.ShippingPer, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);


            cellDetailContentLeft.Phrase = new Phrase("SAILING ON OR ABOUT", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.SailingDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);


            cellDetailContentLeft.Phrase = new Phrase("FROM", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.From, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("TO", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(":", normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase(viewModel.To, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.NO_BORDER;
            tabledetailOrders2.AddCell(cellDetailContentLeft);

            PdfPCell c2 = new PdfPCell(tabledetailOrders2);//this line made the difference
            c2.Rowspan = 3;
            tabledetailOrders.AddCell(c2);

            cellDetailContentRight.AddElement(new Phrase("MESSRS      : ", normal_font));
            cellDetailContentRight.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight);

            if (viewModel.InvoiceNo.Substring(0, 2) == "SM" || viewModel.InvoiceNo.Substring(0, 2) == "DS" || viewModel.InvoiceNo.Substring(0, 3) == "DLR")
            {
                cellDetailContentCenter.AddElement(new Phrase(viewModel.Consignee, normal_font));
            }
            else
            {
                cellDetailContentCenter.AddElement(new Phrase(viewModel.BuyerAgent.Name, normal_font));
            }
            cellDetailContentCenter.AddElement(new Phrase(viewModel.ConsigneeAddress, normal_font));
            cellDetailContentCenter.Border = Rectangle.NO_BORDER;
            tabledetailOrders.AddCell(cellDetailContentCenter);

            cellDetailContentRight2.AddElement(new Phrase("DELIVERED TO : ", normal_font));
            cellDetailContentRight2.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight2);

            cellDetailContentCenter2.AddElement(new Phrase(viewModel.DeliverTo, normal_font));
            cellDetailContentCenter2.Border = Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentCenter2);

            cellDetailContentRight2.Phrase = new Phrase("\n", normal_font);
            cellDetailContentRight2.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight2);

            PdfPCell cellDetail = new PdfPCell(tabledetailOrders);
            cellDetail.Border = Rectangle.NO_BORDER;
            table.AddCell(cellDetail);

            table.WriteSelectedRows(0, -1, document.LeftMargin, height - marginTop + tabledetailOrders.TotalHeight - 205, writer.DirectContent);
            #endregion

            #region START
            PdfPTable tableStart = new PdfPTable(1);
            tableStart.SetWidths(new float[] { 8f });

            PdfPCell cellHeaderContentStart = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentStart.AddElement(new Phrase("\n", normal_font));

            tableStart.AddCell(cellHeaderContentStart);
            tableStart.SpacingAfter = 50f;
            document.Add(tableStart);
            #endregion

            #region SIGNATURE
            var printY = document.BottomMargin - 20;
            var signX = document.RightMargin + 500;
            var signY = printY + 20;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "( MRS. ADRIYANA DAMAYANTI )", document.RightMargin + 500, signY, 0);
            cb.MoveTo(signX - 60, signY - 2);
            cb.LineTo(signX + 45, signY - 2);
            cb.Stroke();
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "AUTHORIZED SIGNATURE", document.RightMargin + 500, signY - 15, 0);

            #endregion

            #region FOOTER

            var printY1 = document.BottomMargin - 30;
            var signX1 = document.LeftMargin + 20;
            var signY1 = printY1 + 20;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "The shippers accept no responsibility for the arrival of goods at destination or for loss or damage in transit after goods were shipped in good order and condition.", document.LeftMargin, 40, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "The  goods  supplied  under  this  invoice  remain  our  property  until  the  invoice  has been credited into  one or our Bank account  mentioned above in full and without restriction.", document.LeftMargin, 30, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "without restriction.", document.LeftMargin, 30, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "______________________________________________________________________________________________________________________________________________", document.LeftMargin, 20, 0);

            #endregion

            cb.EndText();
        }
    }
}