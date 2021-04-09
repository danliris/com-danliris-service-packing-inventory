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
    public class GarmentShippingInvoiceCMTPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingInvoiceViewModel viewModel, Buyer buyer, BankAccount bank, GarmentPackingListViewModel pl, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            //Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 300, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentShippingInvoicePDFTemplatePageEvent(viewModel, timeoffset);

            document.Open();

            #region Header
            //PdfPTable tableHeader = new PdfPTable(3);
            //tableHeader.SetWidths(new float[] { 1f, 1f, 1f });

            //PdfPCell cellHeaderContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            //cellHeaderContentLeft.AddElement(new Phrase("Invoice No.  :  " + viewModel.InvoiceNo, normal_font));
            //tableHeader.AddCell(cellHeaderContentLeft);

            //PdfPCell cellHeaderContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //cellHeaderContentCenter.AddElement(new Phrase("\n", normal_font));
            //cellHeaderContentCenter.AddElement(new Paragraph("Date  :  " + viewModel.InvoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));
            //tableHeader.AddCell(cellHeaderContentCenter);

            //PdfPCell cellHeaderContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //cellHeaderContentRight.AddElement(new Phrase("", bold_font));
            //tableHeader.AddCell(cellHeaderContentRight);

            //PdfPCell cellHeader = new PdfPCell(tableHeader);
            //tableHeader.ExtendLastRow = false;
            //tableHeader.SpacingAfter = 15f;
            //document.Add(tableHeader);
            #endregion

            #region detailOrders
            //PdfPTable tabledetailOrders = new PdfPTable(3);
            //tabledetailOrders.SetWidths(new float[] { 0.6f, 1.4f, 2f });

            //PdfPCell cellDetailContentLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER };
            //PdfPCell cellDetailContentRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            //PdfPCell cellDetailContentCenter = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER };

            //cellDetailContentLeft.Phrase = new Phrase("SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF", normal_font);
            //cellDetailContentLeft.Colspan = 2;
            //tabledetailOrders.AddCell(cellDetailContentLeft);

            //cellDetailContentLeft.Phrase = new Phrase("CO NO.  : " + viewModel.CO, normal_font);
            //cellDetailContentLeft.Colspan = 1;
            //cellDetailContentLeft.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentLeft);

            //cellDetailContentRight.AddElement(new Phrase("MESSRS      : ", normal_font));
            //cellDetailContentRight.Border = Rectangle.NO_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentRight);

            //cellDetailContentCenter.AddElement(new Phrase(viewModel.BuyerAgent.Name, normal_font));
            //cellDetailContentCenter.AddElement(new Phrase(viewModel.ConsigneeAddress, normal_font));
            //cellDetailContentCenter.Border = Rectangle.NO_BORDER;
            ////cellDetailContentCenter.AddElement(new Phrase(buyer.Country, normal_font));
            //tabledetailOrders.AddCell(cellDetailContentCenter);


            //cellDetailContentRight.Phrase=new Phrase("", normal_font);
            //cellDetailContentRight.AddElement(new Phrase("CONFIRMATION OF ORDER NO. : " + viewModel.ConfirmationOfOrderNo, normal_font));
            //cellDetailContentRight.AddElement(new Phrase("SHIPPED PER : " + viewModel.ShippingPer, normal_font));
            //cellDetailContentRight.AddElement(new Phrase("SAILING ON OR ABOUT : " + viewModel.SailingDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));
            //cellDetailContentRight.AddElement(new Phrase("FROM : " + viewModel.From, normal_font));
            //cellDetailContentRight.AddElement(new Phrase("TO   : " + viewModel.To, normal_font));
            //// cellDetailContentRight.AddElement(new Phrase("\n", normal_font));
            //cellDetailContentRight.Border = Rectangle.LEFT_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentRight);



            //cellDetailContentRight.Phrase = new Phrase("DELIVERED TO : ", normal_font);
            //cellDetailContentRight.Colspan = 1;
            //cellDetailContentRight.Border = Rectangle.BOTTOM_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentRight);

            //cellDetailContentCenter.Phrase=new Phrase(viewModel.DeliverTo, normal_font);
            //cellDetailContentCenter.Border = Rectangle.BOTTOM_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentCenter);

            //cellDetailContentRight.Phrase = new Phrase("", normal_font);
            //cellDetailContentRight.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            //tabledetailOrders.AddCell(cellDetailContentRight);

            //PdfPCell cellDetail = new PdfPCell(tabledetailOrders);
            //tabledetailOrders.ExtendLastRow = false;
            //tabledetailOrders.SpacingAfter = 5f;
            //tabledetailOrders.HeaderRows = 3;
            //document.Add(tabledetailOrders);
            #endregion

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
            float[] bodyTableWidths = new float[] { 1.6f, 1.6f, 1.6f, 1.6f, 1.2f, 0.8f, 1.5f, 1.8f, 1.5f, 1.8f };
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
            bodyTableCellLeftBorder.Phrase = new Phrase(".", body_font);
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
            float[] calculationTableWidths = new float[] { 4f, 2f, 2f, 6f };
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
                amountToText = "MINUS " + NumberToTextEN.toWords((double)totalPaid);
            }
            else
            {
                amountToText = NumberToTextEN.toWords((double)totalPaid);
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
            cellMarkContentR.AddElement(new Phrase("SIDE MARKS :", normal_font_underlined));
            cellMarkContentR.AddElement(new Phrase(pl.SideMark, normal_font));
            tableMark.AddCell(cellMarkContentR);

            tableMark.ExtendLastRow = false;
            tableMark.SpacingAfter = 15f;
            document.Add(tableMark);

            //
            PdfPTable tableMark1 = new PdfPTable(2);
            tableMark1.SetWidths(new float[] { 2f, 4f });
            tableMark1.WidthPercentage = 100;
            byte[] shippingMarkImage;

            try
            {
                shippingMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.ShippingMarkImageFile));
            }
            catch (Exception)
            {
                shippingMarkImage = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAA0NDQ0ODQ4QEA4UFhMWFB4bGRkbHi0gIiAiIC1EKjIqKjIqRDxJOzc7STxsVUtLVWx9aWNpfZeHh5e+tb75+f8BDQ0NDQ4NDhAQDhQWExYUHhsZGRseLSAiICIgLUQqMioqMipEPEk7NztJPGxVS0tVbH1pY2l9l4eHl761vvn5///CABEIAAoACgMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAAB//aAAgBAQAAAACnD//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIQAAAAf//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMQAAAAf//EABQQAQAAAAAAAAAAAAAAAAAAACD/2gAIAQEAAT8AH//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIBAT8Af//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMBAT8Af//Z");
            }

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

            byte[] sideMarkImage;

            try
            {
                sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.SideMarkImageFile));
            }
            catch (Exception)
            {
                sideMarkImage = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAA0NDQ0ODQ4QEA4UFhMWFB4bGRkbHi0gIiAiIC1EKjIqKjIqRDxJOzc7STxsVUtLVWx9aWNpfZeHh5e+tb75+f8BDQ0NDQ4NDhAQDhQWExYUHhsZGRseLSAiICIgLUQqMioqMipEPEk7NztJPGxVS0tVbH1pY2l9l4eHl761vvn5///CABEIAAoACgMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAAB//aAAgBAQAAAACnD//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIQAAAAf//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMQAAAAf//EABQQAQAAAAAAAAAAAAAAAAAAACD/2gAIAQEAAT8AH//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIBAT8Af//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMBAT8Af//Z");
            }

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

            new PdfPCell(tableMark);
            tableMark1.ExtendLastRow = false;
            tableMark1.SpacingAfter = 5f;
            document.Add(tableMark1);

            //

            #endregion


            #region Weight
            PdfPTable tableWeight = new PdfPTable(3);
            tableWeight.SetWidths(new float[] { 2f, 0.2f, 7.8f });
            tableWeight.WidthPercentage = 100;

            PdfPCell cellWeightContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellWeightContentLeft.AddElement(new Phrase("GROSS WEIGHT ", normal_font));
            cellWeightContentLeft.AddElement(new Phrase("NETT WEIGHT ", normal_font));
            cellWeightContentLeft.AddElement(new Phrase("MEASSUREMENT ", normal_font));
            tableWeight.AddCell(cellWeightContentLeft);

            PdfPCell cellWeightContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellWeightContentCenter.AddElement(new Phrase(" : ", normal_font));
            cellWeightContentCenter.AddElement(new Phrase(" : ", normal_font));
            cellWeightContentCenter.AddElement(new Phrase(" : ", normal_font));
            tableWeight.AddCell(cellWeightContentCenter);

            PdfPCell cellWeightContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellWeightContentRight.AddElement(new Phrase($"{pl.GrossWeight} KGS", normal_font));
            cellWeightContentRight.AddElement(new Phrase($"{pl.NettWeight} KGS", normal_font));

            PdfPTable tableMeasurement = new PdfPTable(5);
            tableMeasurement.SetWidths(new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f });
            tableMeasurement.WidthPercentage = 60;

            PdfPCell cellMeasurement = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            if (pl.Measurements.Count > 0)
            {
                double totalctns = 0;
                double totalCM = 0;
                foreach (var m in pl.Measurements)
                {
                    double cbm = (m.Length * m.Width * m.Height * m.CartonsQuantity) / 1000000;

                    cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", m.Length) + " X", normal_font);
                    cellMeasurement.PaddingLeft = 1;
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", m.Width) + " X", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", m.Height) + " X", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase($"{m.CartonsQuantity} CTNS = ", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", cbm) + " CBM", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    totalctns += m.CartonsQuantity;
                    totalCM += cbm;
                }

                cellMeasurement.Phrase = new Phrase("", normal_font);
                cellMeasurement.Colspan = 2;
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("                       " + $"{totalctns} CTNS", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", totalCM) + " CBM", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellWeightContentRight.AddElement(tableMeasurement);
            }

            tableWeight.AddCell(cellWeightContentRight);


            tableWeight.SpacingAfter = 1f;
            document.Add(tableWeight);
            #endregion

            #region REMARK
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

            try
            {
                shippingRemarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.RemarkImageFile));
            }
            catch (Exception)
            {
                shippingRemarkImage = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAA0NDQ0ODQ4QEA4UFhMWFB4bGRkbHi0gIiAiIC1EKjIqKjIqRDxJOzc7STxsVUtLVWx9aWNpfZeHh5e+tb75+f8BDQ0NDQ4NDhAQDhQWExYUHhsZGRseLSAiICIgLUQqMioqMipEPEk7NztJPGxVS0tVbH1pY2l9l4eHl761vvn5///CABEIAAoACgMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAAB//aAAgBAQAAAACnD//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIQAAAAf//EABQBAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMQAAAAf//EABQQAQAAAAAAAAAAAAAAAAAAACD/2gAIAQEAAT8AH//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQIBAT8Af//EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQMBAT8Af//Z");
            }

            Image shipRemarkImage = Image.GetInstance(imgb: shippingRemarkImage);

            if (shipRemarkImage.Width > 60)
            {
                float percentage = 0.0f;
                percentage = 100 / shipRemarkImage.Width;
                shipRemarkImage.ScalePercent(percentage * 100);
            }

            PdfPCell shipRemarkImageCell = new PdfPCell(shipRemarkImage);
            shipRemarkImageCell.Border = Rectangle.NO_BORDER;
            tableRemark2.AddCell(shipRemarkImageCell);

            new PdfPCell(tableRemark2);
            tableRemark2.ExtendLastRow = false;
            tableRemark2.SpacingAfter = 5f;
            document.Add(tableRemark2);
            //         
            #endregion
            //document.Add(new Paragraph("REMARK : ", normal_font_underlined));
            //document.Add(new Paragraph(pl.Remark, normal_font));

            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));

            Paragraph sign = new Paragraph("( MRS. ADRIYANA DAMAYANTI )", normal_font_underlined);
            sign.Alignment = Element.ALIGN_RIGHT;
            Paragraph author = new Paragraph("AUTHORIZED SIGNATURE  ", normal_font);
            author.Alignment = Element.ALIGN_RIGHT;

            document.Add(sign);
            document.Add(author);

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }


}