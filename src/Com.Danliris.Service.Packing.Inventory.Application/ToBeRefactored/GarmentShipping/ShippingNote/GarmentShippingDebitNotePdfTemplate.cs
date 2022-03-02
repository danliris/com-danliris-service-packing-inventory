using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public class GarmentShippingDebitNotePdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingDebitNoteViewModel viewModel, int timeoffset)
        {
            Font normal_font = FontFactory.GetFont(BaseFont.COURIER, 10, Font.NORMAL);
            Font underlined_font = FontFactory.GetFont(BaseFont.COURIER, 10, Font.UNDERLINE);
            Font big_font = FontFactory.GetFont(BaseFont.COURIER, 16, Font.BOLD);

            Document document = new Document(PageSize.A4, 20, 20, 20, 20);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            var chunkHeader = new Chunk("PT. DAN LIRIS", big_font);
            chunkHeader.SetHorizontalScaling(1.5f);
            document.Add(new Paragraph(chunkHeader));

            PdfPTable tableHeadOffice = new PdfPTable(2);
            tableHeadOffice.SetWidths(new float[] { 1.5f, 6.5f });

            PdfPCell cellHeadOffice = new PdfPCell { Border = Rectangle.NO_BORDER };
            cellHeadOffice.Phrase = new Phrase("Head Office : ", normal_font);
            tableHeadOffice.AddCell(cellHeadOffice);
            cellHeadOffice.Phrase = new Phrase("Jl. Merapi No. 23, Kel. Banaran Kec. Grogol Kab. Sukoharjo\nTelp.(0271)714400, Fax.(0271)735222\ne-Mail:", normal_font);
            cellHeadOffice.Phrase.Add(new Chunk("nara.exportgmt@danliris.com", underlined_font));
            tableHeadOffice.AddCell(cellHeadOffice);
            Chunk chunkAddress = new Chunk("MESSRS :\n" + viewModel.buyer.Name + "\n" + viewModel.buyer.Address, normal_font);
            chunkAddress.SetHorizontalScaling(0.8f);
            Phrase phraseBuyerHeader = new Phrase(chunkAddress);

            //phraseBuyerHeader.Add(new Chunk(new VerticalPositionMark()));
            //phraseBuyerHeader.Add(new Chunk("DEBIT NOTE", normal_font));

            phraseBuyerHeader.Add(new Chunk(new VerticalPositionMark()));
            phraseBuyerHeader.Add(new Chunk("DATE : " + viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")), normal_font));

            tableHeadOffice.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                Colspan = 2,
                PaddingLeft = 10f,
                Phrase = phraseBuyerHeader
            });

            new PdfPCell(tableHeadOffice);
            tableHeadOffice.ExtendLastRow = false;
            tableHeadOffice.SpacingAfter = 5f;
            document.Add(tableHeadOffice);

            document.Add(new Paragraph("DEBIT NOTE", big_font) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 5f });

            document.Add(new Paragraph(viewModel.noteNo, big_font) { Alignment = Element.ALIGN_RIGHT, SpacingAfter = 5f });

            PdfPTable tableItems = new PdfPTable(2);
            tableItems.SetWidths(new float[] { 3f, 1f });

            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Phrase = new Phrase("D e s c r i p t i o n", normal_font)
            });
            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Phrase = new Phrase("Amount", normal_font)
            });

            PdfPTable tableItemsContent = new PdfPTable(2);
            tableItemsContent.SetWidths(new float[] { 3f, 1f });

            tableItemsContent.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                Phrase = new Phrase(viewModel.description, normal_font)
            });

            tableItemsContent.AddCell(new PdfPCell
            {
                Border = Rectangle.LEFT_BORDER,
                Phrase = new Phrase(" ", normal_font)
            });

            foreach (var item in viewModel.items)
            {
                tableItemsContent.AddCell(new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    Phrase = new Phrase(item.description, normal_font)
                });
                Phrase phraseAmount = new Phrase();
                phraseAmount.Add(new Chunk(viewModel.bank.Currency.Code, normal_font));
                phraseAmount.Add(new Chunk(new VerticalPositionMark()));
                phraseAmount.Add(new Chunk(item.amount.ToString("n"), normal_font));
                tableItemsContent.AddCell(new PdfPCell
                {
                    Border = Rectangle.LEFT_BORDER,
                    Phrase = phraseAmount
                });
            }

            PdfPCell pdfPCellItemsContent = new PdfPCell
            {
                Colspan = 2,
                Padding = 0,
                Border = Rectangle.NO_BORDER,
                MinimumHeight = 400
            };
            new PdfPCell(tableItemsContent);
            pdfPCellItemsContent.AddElement(tableItemsContent);
            tableItems.AddCell(pdfPCellItemsContent);

            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.TOP_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingRight = 10,
                Phrase = new Phrase("TOTAL", normal_font)
            });
            Phrase phraseTotalAmount = new Phrase();
            phraseTotalAmount.Add(new Chunk(viewModel.bank.Currency.Code, normal_font));
            phraseTotalAmount.Add(new Chunk(new VerticalPositionMark()));
            phraseTotalAmount.Add(new Chunk(viewModel.totalAmount.ToString("n"), normal_font));
            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER,
                Phrase = phraseTotalAmount
            });
            //tableItems.AddCell(new PdfPCell
            //{
            //    Colspan = 2,
            //    PaddingBottom = 10f,
            //    Border = Rectangle.NO_BORDER,
            //    Phrase = new Phrase("KETERANGAN   : " + viewModel.description, normal_font)
            //});
            string amountToText = "";
            amountToText = CurrencyToText.ToWords(Convert.ToDecimal(viewModel.totalAmount));
            if (viewModel.bank.Currency.Code == "USD")
            {
                tableItems.AddCell(new PdfPCell
                {
                    Colspan = 2,
                    PaddingBottom = 10f,
                    Border = Rectangle.NO_BORDER,
                    //Phrase = new Phrase("SAY : US DOLLARS " + NumberToTextEN.toWords(viewModel.totalAmount).Trim().ToUpper() + " ONLY ///", normal_font)
                    Phrase = new Phrase("SAY : US DOLLARS " + amountToText.ToUpper() + " ONLY ///", normal_font)
                });
            }
            else
            {
                tableItems.AddCell(new PdfPCell
                {
                    Colspan = 2,
                    PaddingBottom = 10f,
                    Border = Rectangle.NO_BORDER,
                    Phrase = new Phrase("TERBILANG : " + NumberToTextIDN.terbilang(viewModel.totalAmount).Trim().ToUpper() + " RUPIAH ///", normal_font)
                });
            }

            //tableItems.AddCell(new PdfPCell
            //{
            //    Border = Rectangle.NO_BORDER,
            //    PaddingRight = 10f,
            //    Phrase = new Phrase("Please TT the above payment to our correspondence bank as follow :", normal_font)
            //});
            Phrase phraseSign = new Phrase();

            phraseSign.Add(new Chunk("S.E. & O\n" + viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN")) + "\n\n\n\n", normal_font));
            Chunk chunkSignName = new Chunk("WAHYU PAMUNGKAS", normal_font);
            chunkSignName.SetUnderline(1, -1);
            phraseSign.Add(chunkSignName);
            phraseSign.Add(new Chunk("\nAUTHORIZED SIGNATURE", normal_font));

            string branch = "";

            if (viewModel.bank.bankName == "PT. CIMB NIAGA Tbk.")
            {
                branch = "SOLO BRANCH OFFICE";
            }
            else if (viewModel.bank.bankName == "PANIN BANK")
            {
                branch = "SOLO BRANCH OFFICE";
            }
            else
            {
                branch = "SRIWEDARI BRANCH OFFICE";
            }

            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                PaddingLeft = 20f,
                PaddingRight = 10f,
                Phrase = new Phrase($"{viewModel.bank.bankName}\n{branch}\n{viewModel.bank.bankAddress}\nACC. No. {viewModel.bank.AccountNumber} ({viewModel.bank.Currency.Code})\nA/N. PT. DAN LIRIS\nSWIFT CODE : {viewModel.bank.swiftCode}\nPURPOSE CODE : 1011", normal_font)
            });

            tableItems.AddCell(new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Rowspan = 2,
                Phrase = phraseSign
            });

            new PdfPCell(tableItems);
            tableItems.ExtendLastRow = false;
            tableItems.SpacingAfter = 5f;
            document.Add(tableItems);

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;

        }
    }
}
