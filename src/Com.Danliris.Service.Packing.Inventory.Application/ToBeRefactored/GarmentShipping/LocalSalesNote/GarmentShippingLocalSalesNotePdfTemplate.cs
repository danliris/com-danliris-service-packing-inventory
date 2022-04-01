using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNotePdfTemplate
    {
        //private IIdentityProvider _identityProvider;

        //public GarmentShippingLocalSalesNotePdfTemplate(IIdentityProvider identityProvider)
        //{
        //    _identityProvider = identityProvider;
        //}

        public MemoryStream GeneratePdfTemplate(GarmentShippingLocalSalesNoteViewModel viewModel, GarmentLocalCoverLetterViewModel cl, Buyer buyer, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font normal_font_bold = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            //  writer.PageEvent = new GarmentShippingLocalSalesNotePdfTemplatePageEvent(_identityProvider, viewModel, cl, buyer, timeoffset);
            writer.PageEvent = new GarmentShippingLocalSalesNotePdfTemplatePageEvent(viewModel, cl, buyer, timeoffset);

            document.Open();

            //#region header
            //PdfPTable tableHeader = new PdfPTable(2);
            //tableHeader.WidthPercentage = 100;
            //tableHeader.SetWidths(new float[] { 3f, 2f });

            //PdfPCell cellHeaderContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellHeaderContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };


            //cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            //cellHeaderContent1.AddElement(new Phrase("PT. DAN LIRIS", header_font_bold));
            //cellHeaderContent1.AddElement(new Phrase("Jl. Merapi No. 23, Kel. Banaran Kec.Grogol Kab. Sukoharjo", normal_font));
            //cellHeaderContent1.AddElement(new Phrase("Telp : 0271-714400, Fax. 0271-717178", normal_font));
            //cellHeaderContent1.AddElement(new Phrase("PO. Box. 166 Solo-57100 Indonesia", normal_font));
            ////cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            //tableHeader.AddCell(cellHeaderContent1);

            //cellHeaderContent2.AddElement(new Phrase("Sukoharjo, " + viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            //cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            ////cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            //cellHeaderContent2.AddElement(new Phrase(viewModel.buyer.Code + " - " + viewModel.buyer.Name + " - " + viewModel.buyer.KaberType, normal_font));
            //cellHeaderContent2.AddElement(new Phrase(buyer.Address, normal_font));
            //tableHeader.AddCell(cellHeaderContent2);

            //document.Add(tableHeader);

            //#endregion

            //#region title

            //Paragraph title = new Paragraph("NOTA PENJUALAN GARMENT", header_font_bold);
            //title.Alignment = Element.ALIGN_CENTER;
            //document.Add(title);

            //Paragraph no = new Paragraph(viewModel.noteNo, header_font_bold);
            //no.Alignment = Element.ALIGN_CENTER;
            //document.Add(no);

            ////if (viewModel.buyer.KaberType == "KABER")
            ////{
            ////    Paragraph location = new Paragraph("PPN BERFASILITAS", normal_font_bold);
            ////    location.Alignment = Element.ALIGN_RIGHT;
            ////    document.Add(location);
            ////}
            //PdfPTable tableTitle = new PdfPTable(6);
            //tableTitle.WidthPercentage = 100;
            //tableTitle.SetWidths(new float[] { 1.5f, 2f, 1.5f, 2f, 1.5f, 1f });

            //PdfPCell cellFooterContent1x = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellFooterContent2x = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellFooterContent31 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellFooterContent41 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellFooterContent51 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            //PdfPCell cellFooterContent61 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            //cellFooterContent1x.Phrase = new Phrase("NPWP Penjual :", normal_font);
            //tableTitle.AddCell(cellFooterContent1x);
            //cellFooterContent2x.Phrase = new Phrase("01.139.907.8-532.000", normal_font);
            //tableTitle.AddCell(cellFooterContent2x);

            //cellFooterContent31.Phrase = new Phrase("NPWP Pembeli :", normal_font);
            //tableTitle.AddCell(cellFooterContent31);
            //cellFooterContent41.Phrase = new Phrase(buyer.npwp, normal_font);
            //tableTitle.AddCell(cellFooterContent41);

            //cellFooterContent51.Phrase = new Phrase("NIK Pembeli :", normal_font);
            //tableTitle.AddCell(cellFooterContent51);
            //cellFooterContent61.Phrase = new Phrase("", normal_font);
            //tableTitle.AddCell(cellFooterContent61);
            //tableTitle.SpacingAfter = 3;
            //tableTitle.HorizontalAlignment = Element.ALIGN_LEFT;
            //document.Add(tableTitle);

            //#endregion

            #region bodyTable
            PdfPTable tableBody = new PdfPTable(7);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 1.2f, 1f, 5f, 2f, 1f, 2f, 2f });
            PdfPCell cellBodyLeft = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyLeftNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyRightNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyCenter = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodyCenter.Phrase = new Phrase("Banyak", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Satuan", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Nama Barang", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Quantity", normal_font);
            cellBodyCenter.Colspan = 2;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Harga Sat.", normal_font);
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jumlah", normal_font);
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            foreach (var item in viewModel.items)
            {
                cellBodyRight.Phrase = new Phrase(item.packageQuantity == 0 ? "" : string.Format("{0:n2}", item.packageQuantity), normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyLeft.Phrase = new Phrase(item.packageUom == null ? "" : item.packageUom.Unit, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyLeft.Phrase = new Phrase(item.product.code + " - " + item.product.name, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", item.quantity), normal_font);
                tableBody.AddCell(cellBodyRightNoBorder);

                cellBodyLeftNoBorder.Phrase = new Phrase(item.uom.Unit, normal_font);
                tableBody.AddCell(cellBodyLeftNoBorder);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.price), normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.price * item.quantity), normal_font);
                tableBody.AddCell(cellBodyRight);
            }

            double totalPrice = viewModel.items.Sum(a => a.quantity * a.price);
            double ppn = 0;
            if (viewModel.useVat)
            {
                ppn = (totalPrice * viewModel.vat.rate) / 100;
            }
            double finalPrice = totalPrice + ppn;

            cellBodyRight.Phrase = new Phrase("Dasar Pengenaan Pajak...............Rp.", normal_font);
            cellBodyRight.Border = Rectangle.NO_BORDER;
            cellBodyRight.Colspan = 6;
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", totalPrice), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.NO_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase("PPN = " + viewModel.vat.rate.ToString() + "% X Dasar Pengenaan Pajak...Rp.", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", ppn), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.BOTTOM_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase("Jumlah..............................Rp.", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase(string.Format("{0:n2}", finalPrice), normal_font);
            cellBodyRightNoBorder.Border = Rectangle.NO_BORDER;
            tableBody.AddCell(cellBodyRightNoBorder);


            tableBody.SpacingAfter = 10;
            document.Add(tableBody);
            #endregion

            #region footer
            PdfPTable tableFooter = new PdfPTable(4);
            tableFooter.WidthPercentage = 100;
            tableFooter.SetWidths(new float[] { 2f, 3f, 2f, 3f });

            PdfPCell cellFooterContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent4 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellFooterContent1.Phrase = new Phrase("No Sales Contract :", normal_font);
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = new Phrase(viewModel.salesContractNo, normal_font);
            tableFooter.AddCell(cellFooterContent2);

            cellFooterContent3.Phrase = new Phrase("No Bon Keluar     :", normal_font);
            tableFooter.AddCell(cellFooterContent3);
            cellFooterContent4.Phrase = new Phrase(viewModel.expenditureNo, normal_font);
            tableFooter.AddCell(cellFooterContent4);

            cellFooterContent1.Phrase = new Phrase("No Disposisi      :", normal_font);
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = new Phrase(string.IsNullOrWhiteSpace(viewModel.dispositionNo) ? "-" : viewModel.dispositionNo, normal_font);
            tableFooter.AddCell(cellFooterContent2);

            cellFooterContent3.Phrase = new Phrase("No Bea Cukai      :", normal_font);
            tableFooter.AddCell(cellFooterContent3);
            cellFooterContent4.Phrase = new Phrase(cl == null ? "-" : cl.bcNo, normal_font);
            tableFooter.AddCell(cellFooterContent4);

            cellFooterContent1.Phrase = new Phrase("Tempo Pembayaran  :", normal_font);
            tableFooter.AddCell(cellFooterContent1);
            cellFooterContent2.Phrase = new Phrase(viewModel.tempo + " Hari", normal_font);
            tableFooter.AddCell(cellFooterContent2);

            cellFooterContent3.Phrase = (new Phrase("Tanggal J/T       :", normal_font));
            tableFooter.AddCell(cellFooterContent3);
            cellFooterContent4.Phrase = (new Phrase(viewModel.date.GetValueOrDefault().AddDays(viewModel.tempo).ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            tableFooter.AddCell(cellFooterContent4);
            document.Add(tableFooter);
            #endregion

            #region footer1
            PdfPTable tableFooter1 = new PdfPTable(2);
            tableFooter1.WidthPercentage = 100;
            tableFooter1.SetWidths(new float[] { 1.5f, 9f });

            PdfPCell cellFooterContent11 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent21 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            string terbilang = NumberToTextIDN.terbilang(Math.Round(finalPrice, 2));

            cellFooterContent11.Phrase = (new Phrase("Terbilang   :", normal_font));
            tableFooter1.AddCell(cellFooterContent11);
            cellFooterContent21.Phrase = (new Phrase(terbilang + " rupiah", normal_font));
            tableFooter1.AddCell(cellFooterContent21);

            cellFooterContent11.Phrase = (new Phrase("Catatan     :", normal_font));
            tableFooter1.AddCell(cellFooterContent11);
            cellFooterContent21.Phrase = (new Phrase(viewModel.remark, normal_font));
            tableFooter1.AddCell(cellFooterContent21);

            tableFooter1.SpacingAfter = 4;
            document.Add(tableFooter1);
            #endregion

            document.Add(new Paragraph("HARAP TRANSFER PEMBAYARAN DIATAS KEPADA BANK KORESPONDEN KAMI SEBAGAI BERIKUT :", normal_font));
            document.Add(new Paragraph("MAYBANK INDONESIA - CABANG SLAMET RIYADI", normal_font));
            document.Add(new Paragraph("ACC NO. : 2105010887  A/N : PT. DAN LIRIS", normal_font));
            document.Add(new Paragraph(" ", normal_font));

            #region sign
            PdfPTable tableSign = new PdfPTable(5);
            tableSign.KeepTogether = true;
            tableSign.WidthPercentage = 80;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
            PdfPCell cellBodySign = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodySign.Phrase = new Phrase("Diterima Oleh", normal_font);
            tableSign.AddCell(cellBodySign);

            cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySign.Phrase = new Phrase("Dibuat Oleh", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("Diperiksa Oleh", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("Disetujui Oleh", normal_font);
            tableSign.AddCell(cellBodySign);


            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);

            cellBodySignNoBorder.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);
            cellBodySign.Phrase = new Phrase("\n\n\n", normal_font);
            tableSign.AddCell(cellBodySign);

            document.Add(tableSign);
            #endregion
            //document.Add(new Phrase("Model DL1",normal_font));

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }

    //
    class GarmentShippingLocalSalesNotePdfTemplatePageEvent : PdfPageEventHelper
    {
        private IIdentityProvider identityProvider;
        private GarmentShippingLocalSalesNoteViewModel viewModel;
        private int timeoffset;
        private Buyer buyer;

        //public GarmentShippingLocalSalesNotePdfTemplatePageEvent(IIdentityProvider identityProvider, GarmentShippingLocalSalesNoteViewModel viewModel, GarmentLocalCoverLetterViewModel cl, Buyer buyer, int timeoffset)
        public GarmentShippingLocalSalesNotePdfTemplatePageEvent(GarmentShippingLocalSalesNoteViewModel viewModel, GarmentLocalCoverLetterViewModel cl, Buyer buyer, int timeoffset)
        {
            //this.identityProvider = identityProvider;
            this.viewModel = viewModel;
            this.timeoffset = timeoffset;
            this.buyer = buyer;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin, marginTop = document.TopMargin, marginRight = document.RightMargin, marginBottom = document.BottomMargin;
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font header_font_bold = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);


            #region Header
            PdfPTable tableHeader = new PdfPTable(2);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 3f, 2f });

            PdfPCell cellHeaderContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };


            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PT. DAN LIRIS", header_font_bold));
            cellHeaderContent1.AddElement(new Phrase("Jl. Merapi No. 23, Kel. Banaran Kec.Grogol Kab. Sukoharjo", normal_font));
            cellHeaderContent1.AddElement(new Phrase("Telp : 0271-714400, Fax. 0271-717178", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PO. Box. 166 Solo-57100 Indonesia", normal_font));
            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            tableHeader.AddCell(cellHeaderContent1);

            cellHeaderContent2.AddElement(new Phrase("Sukoharjo, " + viewModel.date.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase(viewModel.buyer.Code + " - " + viewModel.buyer.Name + " - " + buyer.KaberType, normal_font));
            cellHeaderContent2.AddElement(new Phrase(buyer.Address, normal_font));
            tableHeader.AddCell(cellHeaderContent2);

            document.Add(tableHeader);

            #endregion

            #region title

            Paragraph title = new Paragraph("NOTA PENJUALAN GARMENT", header_font_bold);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            Paragraph no = new Paragraph(viewModel.noteNo, header_font_bold);
            no.Alignment = Element.ALIGN_CENTER;
            document.Add(no);

            Paragraph title1 = new Paragraph(" ", normal_font);
            title1.Alignment = Element.ALIGN_CENTER;
            document.Add(title1);
            //if (viewModel.buyer.KaberType == "KABER")
            //{
            //    Paragraph location = new Paragraph("PPN BERFASILITAS", normal_font_bold);
            //    location.Alignment = Element.ALIGN_RIGHT;
            //    document.Add(location);
            //}

            PdfPTable tableTitle = new PdfPTable(6);
            tableTitle.WidthPercentage = 100;
            tableTitle.SetWidths(new float[] { 1.5f, 2f, 1.5f, 2f, 1.5f, 1f });

            PdfPCell cellFooterContent1x = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent2x = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent31 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent41 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent51 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterContent61 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellFooterContent1x.Phrase = new Phrase("NPWP Penjual :", normal_font);
            tableTitle.AddCell(cellFooterContent1x);
            cellFooterContent2x.Phrase = new Phrase("01.139.907.8-532.000", normal_font);
            tableTitle.AddCell(cellFooterContent2x);

            cellFooterContent31.Phrase = new Phrase("NPWP Pembeli :", normal_font);
            tableTitle.AddCell(cellFooterContent31);
            cellFooterContent41.Phrase = new Phrase(viewModel.buyer.npwp, normal_font);
            tableTitle.AddCell(cellFooterContent41);

            cellFooterContent51.Phrase = new Phrase("NIK Pembeli :", normal_font);
            tableTitle.AddCell(cellFooterContent51);
            cellFooterContent61.Phrase = new Phrase("", normal_font);
            tableTitle.AddCell(cellFooterContent61);
            tableTitle.SpacingAfter = 3;
            tableTitle.HorizontalAlignment = Element.ALIGN_LEFT;
            document.Add(tableTitle);

            #endregion

            cb.EndText();
        }
    }
    //

}
