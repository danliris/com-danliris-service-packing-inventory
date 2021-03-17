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
    public class GarmentShippingInvoicePdfTemplate
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

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 350, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentShippingInvoicePDFTemplatePageEvent(viewModel, timeoffset);

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

            PdfPTable bodyTable = new PdfPTable(8);
            float[] bodyTableWidths = new float[] { 1.8f, 1.8f, 1.8f, 1.8f, 0.8f, 0.5f, 1f, 1.3f };
            bodyTable.SetWidths(bodyTableWidths);
            bodyTable.WidthPercentage = 100;

            #region Set Body Table Header
            PdfPCell bodyTableHeader = new PdfPCell();// { FixedHeight = 30 };
            //PdfPCell table1RightCellHeader = new PdfPCell() { FixedHeight = 20, Colspan = 4 };

            bodyTableHeader.Phrase = new Phrase("DESCRIPTION", normal_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.Rowspan = 2;
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
            #endregion

            #region Set Body Table Value
            PdfPCell bodyTableCellRightBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.RIGHT_BORDER };
            PdfPCell bodyTableCellLeftBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.LEFT_BORDER };
            PdfPCell bodyTableCellCenterBorder = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER };

            bodyTableCellLeftBorder.Phrase = new Phrase($"{viewModel.Description}", body_font);
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

            ////SPACE
            //bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
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

            ////

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

            //SPACE
            //bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
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

            //
            bodyTableCellLeftBorder.Phrase = new Phrase($"{viewModel.Remark}", body_font);
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

            //SPACE
            bodyTableCellLeftBorder.Phrase = new Phrase("", body_font);
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


            decimal totalAmount = 0;
            double totalQuantity = 0;

            Dictionary<string, double> total = new Dictionary<string, double>();

            foreach (var item in viewModel.Items)
            {
                totalAmount += item.Amount;
                totalQuantity += item.Quantity;

                bodyTableCellLeftBorder.Phrase = new Phrase($"{item.ComodityDesc}", body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.Colspan = 1;
                bodyTableCellLeftBorder.Border = Rectangle.LEFT_BORDER;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellLeftBorder.Phrase = new Phrase($"{item.Desc2}", body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.Border = Rectangle.NO_BORDER;
                bodyTableCellLeftBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellLeftBorder.Phrase = new Phrase($"{item.Desc3}", body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.Border = Rectangle.NO_BORDER;
                bodyTableCellLeftBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellLeftBorder.Phrase = new Phrase($"{item.Desc4}", body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.Border = Rectangle.RIGHT_BORDER;
                bodyTableCellLeftBorder.Colspan = 1;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellLeftBorder.Phrase = new Phrase(string.Format("{0:n0}", item.Quantity), body_font);
                bodyTableCellLeftBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellLeftBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellLeftBorder.BorderColorRight = BaseColor.White;
                bodyTableCellLeftBorder.Border = Rectangle.LEFT_BORDER;
                bodyTable.AddCell(bodyTableCellLeftBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Uom.Unit, body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTableCellRightBorder.BorderColorLeft = BaseColor.White;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Price != 0 ? string.Format("{0:n4}", item.Price) : "", body_font);
                bodyTableCellRightBorder.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTableCellRightBorder.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCellRightBorder);

                bodyTableCellRightBorder.Phrase = new Phrase(item.Amount != 0 ? string.Format("{0:n2}", item.Amount) : "", body_font);
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


            bodyTableCellFooter.Phrase = new Phrase($"{result2}", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase("", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableCellFooter);

            bodyTableCellFooter.Phrase = new Phrase(totalAmount != 0 ? string.Format("{0:n2}", totalAmount) : "", body_font);
            bodyTableCellFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
            bodyTableCellFooter.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTableCellFooter.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            bodyTable.AddCell(bodyTableCellFooter);

            #endregion
            bodyTable.HeaderRows = 1;
            document.Add(bodyTable);
            #endregion


            string amountToText = NumberToTextEN.toWords((double)totalAmount);
            document.Add(new Paragraph("SAY   : US DOLLARS " + amountToText.ToUpper() + " ONLY ///", normal_font));
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
            tableMark.SpacingAfter = 10f;
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
            cellMeasurement.Phrase = new Phrase(pl.GrossWeight + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("NET WEIGHT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(pl.NettWeight + " KGS", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            cellMeasurement.Phrase = new Phrase("MEASUREMENT", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            PdfPTable tableMeasurementDetail = new PdfPTable(5);
            tableMeasurementDetail.SetWidths(new float[] { 1f, 1f, 1f, 1.5f, 2f });
            PdfPCell cellMeasurementDetail = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            decimal totalCbm = 0;
            foreach (var measurement in pl.Measurements)
            {
                cellMeasurementDetail.Phrase = new Phrase(measurement.Length + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Width + " CM X ", normal_font);
                tableMeasurementDetail.AddCell(cellMeasurementDetail);
                cellMeasurementDetail.Phrase = new Phrase(measurement.Height + " CM X ", normal_font);
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

            //document.Add(new Paragraph("REMARK : ", normal_font_underlined));
            //document.Add(new Paragraph(pl.Remark, normal_font));

            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //document.Add(new Paragraph("\n", normal_font));
            //
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

            //
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

    class GarmentShippingInvoicePDFTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
    {
        private GarmentShippingInvoiceViewModel viewModel;
        private int timeoffset;

        public GarmentShippingInvoicePDFTemplatePageEvent(GarmentShippingInvoiceViewModel viewModel, int timeoffset)
        {
            this.viewModel = viewModel;
            this.timeoffset = timeoffset;
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

            cb.SetFontAndSize(bf, 8);


            #region CENTER

            var headOfficeX = width / 2 + 30;
            var headOfficeY = height - marginTop + 170;


            string[] headOffices = {
                "                                                                                                                   Ref. No. : FM-00-SP-24-006",
            };
            for (int i = 0; i < headOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, headOffices[i], headOfficeX, headOfficeY, 0);
            }

            #endregion

            //#region RIGHT
            //BaseColor grey = new BaseColor(128, 128, 128);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Page " + (writer.PageNumber), width - (6 / 2) - marginRight, height - marginTop + 160, 0);
            //#endregion

            #region table
            PdfPTable table = new PdfPTable(1);

            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]

            PdfPTable tabledetailOrders = new PdfPTable(3);
            tabledetailOrders.SetWidths(new float[] { 0.6f, 1.4f, 2f });

            PdfPCell cellDetailContentLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER };
            PdfPCell cellDetailContentRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            PdfPCell cellDetailContentCenter = new PdfPCell() { Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER };

            PdfPCell cellHeaderContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentLeft.AddElement(new Phrase("Invoice No.  :  " + viewModel.InvoiceNo + "                                                                           Date  :  " + viewModel.InvoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")) + "                                                             Page  : " + (writer.PageNumber), normal_font));
            cellHeaderContentLeft.AddElement(new Phrase("\n", normal_font));
            cellHeaderContentLeft.Colspan = 3;
            tabledetailOrders.AddCell(cellHeaderContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF", normal_font);
            cellDetailContentLeft.Colspan = 2;
            tabledetailOrders.AddCell(cellDetailContentLeft);

            cellDetailContentLeft.Phrase = new Phrase("CO NO.  : " + viewModel.CO, normal_font);
            cellDetailContentLeft.Colspan = 1;
            cellDetailContentLeft.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            tabledetailOrders.AddCell(cellDetailContentLeft);

            cellDetailContentRight.AddElement(new Phrase("MESSRS      : ", normal_font));
            cellDetailContentRight.Border = Rectangle.NO_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight);

            cellDetailContentCenter.AddElement(new Phrase(viewModel.BuyerAgent.Name, normal_font));
            cellDetailContentCenter.AddElement(new Phrase(viewModel.ConsigneeAddress, normal_font));
            cellDetailContentCenter.Border = Rectangle.NO_BORDER;
            //cellDetailContentCenter.AddElement(new Phrase(buyer.Country, normal_font));
            tabledetailOrders.AddCell(cellDetailContentCenter);


            cellDetailContentRight.Phrase = new Phrase("", normal_font);
            cellDetailContentRight.AddElement(new Phrase("CONFIRMATION OF ORDER NO. : " + viewModel.ConfirmationOfOrderNo, normal_font));
            cellDetailContentRight.AddElement(new Phrase("SHIPPED PER : " + viewModel.ShippingPer, normal_font));
            cellDetailContentRight.AddElement(new Phrase("SAILING ON OR ABOUT : " + viewModel.SailingDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));
            cellDetailContentRight.AddElement(new Phrase("FROM : " + viewModel.From, normal_font));
            cellDetailContentRight.AddElement(new Phrase("TO   : " + viewModel.To, normal_font));
            // cellDetailContentRight.AddElement(new Phrase("\n", normal_font));
            cellDetailContentRight.Border = Rectangle.LEFT_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight);


            cellDetailContentRight.Phrase = new Phrase("DELIVERED TO : ", normal_font);
            cellDetailContentRight.Colspan = 1;
            cellDetailContentRight.Border = Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight);

            cellDetailContentCenter.Phrase = new Phrase(viewModel.DeliverTo, normal_font);
            cellDetailContentCenter.Border = Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentCenter);

            cellDetailContentRight.Phrase = new Phrase("", normal_font);
            cellDetailContentRight.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            tabledetailOrders.AddCell(cellDetailContentRight);

            PdfPCell cellDetail = new PdfPCell(tabledetailOrders);
            cellDetail.Border = Rectangle.NO_BORDER;
            table.AddCell(cellDetail);

            table.WriteSelectedRows(0, -1, document.LeftMargin, height - marginTop + tabledetailOrders.TotalHeight+10, writer.DirectContent);
            #endregion

            cb.EndText();
        }
    }
}
