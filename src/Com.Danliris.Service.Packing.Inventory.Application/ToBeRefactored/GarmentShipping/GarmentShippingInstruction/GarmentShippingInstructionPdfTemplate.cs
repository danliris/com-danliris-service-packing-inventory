using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingInstructionViewModel viewModel, GarmentCoverLetterViewModel cl, GarmentPackingListViewModel pl, GarmentShippingInvoiceViewModel invoice, int timeoffset)
        {
            const int MARGIN = 25;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            //Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, 100, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentShippingInstructionPDFTemplatePageEvent();

            document.Open();

            #region header
            PdfPTable tableHeader = new PdfPTable(4);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 2f, 4f, 2f, 4f });

            PdfPCell cellHeaderContent1 = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER };
            cellHeaderContent1.AddElement(new Phrase("TO", normal_font));
            cellHeaderContent1.AddElement(new Phrase("ATTN", normal_font));
            cellHeaderContent1.AddElement(new Phrase("CC", normal_font));
            cellHeaderContent1.AddElement(new Phrase("FROM", normal_font));
            tableHeader.AddCell(cellHeaderContent1);

            PdfPCell cellHeaderContent2 = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER };
            cellHeaderContent2.AddElement(new Phrase(": " + viewModel.forwarder.name, normal_font));
            cellHeaderContent2.AddElement(new Phrase(": " + viewModel.ATTN, normal_font));
            cellHeaderContent2.AddElement(new Phrase(": " + viewModel.CC, normal_font));
            cellHeaderContent2.AddElement(new Phrase(": " + viewModel.ShippingStaffName + "/" + viewModel.Phone, normal_font));
            tableHeader.AddCell(cellHeaderContent2);

            PdfPCell cellHeaderContent3 = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER };
            cellHeaderContent3.AddElement(new Phrase("FAX", normal_font));
            cellHeaderContent3.AddElement(new Phrase("REF", normal_font));
            cellHeaderContent3.AddElement(new Phrase("DATE", normal_font));
            tableHeader.AddCell(cellHeaderContent3);


            PdfPCell cellHeaderContent4 = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER };
            cellHeaderContent4.AddElement(new Phrase(": " + viewModel.forwarder.fax, normal_font));
            cellHeaderContent4.AddElement(new Phrase(": " + viewModel.InvoiceNo, normal_font));
            cellHeaderContent4.AddElement(new Phrase(": " + viewModel.Date.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));
            tableHeader.AddCell(cellHeaderContent4);

            document.Add(tableHeader);
            #endregion

            Paragraph sub = new Paragraph("SUBJECT : SHIPPING INSTRUCTION", header_font);
            sub.Alignment = Element.ALIGN_CENTER;
            document.Add(sub);

            document.Add(new Paragraph("DEAR SIRS,", normal_font));
            document.Add(new Paragraph("WE REQUEST YOU TO ARRANGE THE FOLLOWING SHIPMENT", normal_font));

            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph($"{invoice.Description}", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            #region detail
            PdfPTable detailTable = new PdfPTable(3);
            detailTable.HorizontalAlignment = Element.ALIGN_LEFT;
            float[] detailTableWidths = new float[] { 2.5f, 0.5f, 6f };
            detailTable.SetWidths(detailTableWidths);
            detailTable.WidthPercentage = 100;

            PdfPCell cellLeft = new PdfPCell() { MinimumHeight = 15, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            double qty = 0;
            foreach (var invItem in invoice.Items)
            {
                qty += invItem.Quantity;
            }

            cellLeft.Phrase = new Phrase("QUANTITY", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(string.Format("{0:n0}", qty), normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("GROSS WEIGHT", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{string.Format("{0:n2}", pl.GrossWeight)} KGS", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("NETT WEIGHT", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{string.Format("{0:n2}", pl.NettWeight)} KGS", normal_font);
            detailTable.AddCell(cellLeft);


            cellLeft.Phrase = new Phrase("MEASUREMENT", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            PdfPTable tableMeasurement = new PdfPTable(5);
            tableMeasurement.SetWidths(new float[] { 1f, 1f, 1f, 2f, 2f });
            tableMeasurement.WidthPercentage = 100;

            double totcbm = 0;
            double volweight = 0;

            PdfPCell cellMeasurement = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            if (pl.Measurements.Count > 0)
            {
                foreach (var m in pl.Measurements)
                {
                    double cbm = (m.Length * m.Width * m.Height * m.CartonsQuantity) / 1000000;
                    double vlm = (m.Length * m.Width * m.Height * m.CartonsQuantity) / 6000;

                    cellMeasurement.Phrase = new Phrase($"{string.Format("{0:n2}", m.Length)} X", normal_font);
                    cellMeasurement.PaddingLeft = 1;
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase($"{string.Format("{0:n2}", m.Width)} X", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase($"{string.Format("{0:n2}", m.Height)} X", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase($"{string.Format("{0:n2}", m.CartonsQuantity)} CTNS = ", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", cbm) + " CBM", normal_font);
                    tableMeasurement.AddCell(cellMeasurement);

                    totcbm += cbm;
                    volweight += vlm;
                }

                cellMeasurement.Phrase = new Phrase("", normal_font);
                cellMeasurement.PaddingLeft = 1;
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("---------------------", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                cellMeasurement.PaddingLeft = 1;
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("TOTAL CBM = ", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase(string.Format("{0:n2}", totcbm) + " CBM", normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellLeft.AddElement(tableMeasurement);
            }

            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("VOLUME WEIGHT", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{string.Format("{0:n0}", volweight)} KGS", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("CARTON", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{string.Format("{0:n0}", viewModel.CartonNo)} CTNS", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("MARKS", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(viewModel.Marks, normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("PORT OF LOADING", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{invoice.From}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("PORT OF DISCHARGE", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(viewModel.PortOfDischarge, normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("PLACE OF DELIVERY", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.PlaceOfDelivery}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("FEEDER VESSEL BY/DATE", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.FeederVessel}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("OCEAN VESSEL BY/DATE", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.OceanVessel}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("SHIPPER", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase("PT. DAN LIRIS, JL. MERAPI NO. 23, KELURAHAN BANARAN, KECAMATAN GROGOL,\n" +
                                         "SUKOHARJO 57552, INDONESIA.\n" +
                                         "PHONE : 0271-714400  FAX : 0271-735222", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("CONSIGNEE", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.BuyerAgent.Name} \n" +
                                         $"{viewModel.BuyerAgentAddress}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("NOTIFY ADDRESS", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.Notify}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("BILL OF LADING", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.LadingBill}", normal_font);
            detailTable.AddCell(cellLeft);

            string ladingDate = viewModel.LadingDate == DateTimeOffset.MinValue ? "" :
                                viewModel.LadingDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"));

            cellLeft.Phrase = new Phrase("DATE OF BILL OF LADING", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{ladingDate}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("FREIGHT", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.Freight}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("CONTAINER NO.", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(cl == null ? "-" : cl.containerNo, normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase("SHIPPING SEAL.", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(cl == null ? "-" : cl.shippingSeal, normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase("LETTER OF CREDIT NO.", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{pl.LCNo}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("ISSUING BANK", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{pl.IssuedBy}", normal_font);
            detailTable.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("SPECIAL INSTRUCTION", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(":", normal_font);
            detailTable.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase($"{viewModel.SpecialInstruction}", normal_font);
            detailTable.AddCell(cellLeft);

            document.Add(detailTable);
            #endregion

            #region SIGN
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("THANK YOU,", normal_font));
            document.Add(new Paragraph("WITH BEST REGARDS,", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph("\n", normal_font));
            document.Add(new Paragraph($"{invoice.ShippingStaff}", normal_font));
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }

    class GarmentShippingInstructionPDFTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
    {
        public override void OnStartPage(PdfWriter writer, Document document)
        {

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

            cb.SetFontAndSize(bf, 8);

            #region LEFT

            var branchOfficeY = height - marginTop + 50;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " HEAD OFFICE :", marginLeft, branchOfficeY, 0);
            string[] branchOffices = {
                " Cable   : DANLIRIS",
                " Phone   : (62271)740888, 714400",
                " Website : www.danliris.com",
                " Fax. : (62271)740777, 735222",
            };
            for (int i = 0; i < branchOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, branchOffices[i], marginLeft, branchOfficeY - 10 - (i * 10), 0);
            }

            #endregion

            #region CENTER

            var headOfficeX = width / 2 + 30;
            var headOfficeY = height - marginTop + 60;


            string[] headOffices = {
                "                                                                                                                                                                      Ref. No. : FM-00-SP-24-004",
                "P.T. DAN LIRIS",
                "SPINNING - WEAVING - FINISHING - PRINTING - GARMENT",
                "JL. MERAPI No. 23, KEL. BANARAN, KEC. GROGOL, SUKOHARJO - INDONESIA",
                "PO. BOX 166 SOLO 57100",
                "                                                                                                                                                                                        Page " + (writer.PageNumber),
               // " "
            };
            for (int i = 0; i < headOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, headOffices[i], headOfficeX, headOfficeY - (i * 10), 0);
            }

            #endregion


            #region RIGHT

            BaseColor grey = new BaseColor(128, 128, 128);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Page " + (writer.PageNumber), width - (1 / 2) - marginRight, height - marginTop + 20, 0);

            #endregion


            cb.EndText();
        }
    }
}
