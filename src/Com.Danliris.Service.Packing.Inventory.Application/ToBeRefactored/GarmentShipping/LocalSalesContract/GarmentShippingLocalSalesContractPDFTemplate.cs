using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingLocalSalesContractViewModel viewModel, int timeoffset)
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

            Paragraph title = new Paragraph("SALES CONTRACT", header_font_bold_underlined);
            title.Alignment = Element.ALIGN_CENTER;
            Paragraph no = new Paragraph($"(REF : {viewModel.salesContractNo})", header_font_bold);
            no.Alignment = Element.ALIGN_CENTER;

            document.Add(title);
            document.Add(no);
            document.Add(new Paragraph("\n",normal_font));
            #endregion

            #region title

            document.Add(new Paragraph("Kami, yang bertanda tangan di bawah ini : ", normal_font));
            document.Add(new Paragraph("\n", normal_font));

            PdfPTable tableTitle = new PdfPTable(3);
            tableTitle.WidthPercentage = 100;
            tableTitle.SetWidths(new float[] { 3f, 1f, 15f });

            PdfPCell cellTitle1 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellTitle1.Phrase = new Phrase("PIHAK PERTAMA ( I )", normal_font);
            cellTitle1.Colspan = 3;
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Nama", normal_font);
            cellTitle1.Colspan = 1;
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.sellerName, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Jabatan", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.sellerPosition, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Alamat", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.sellerAddress, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("NPWP", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.sellerNPWP, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Dalam hal ini Pihak Pertama (I) disebut sebagai pihak PENJUAL", normal_font);
            cellTitle1.Colspan = 3;
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("\n", normal_font);
            cellTitle1.Colspan = 3;
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("PIHAK KEDUA ( II )", normal_font);
            cellTitle1.Colspan = 3;
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Nama", normal_font);
            cellTitle1.Colspan = 1;
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.buyer.Name, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Alamat", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.buyer.Address, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("NPWP", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(":", normal_font);
            tableTitle.AddCell(cellTitle1);
            cellTitle1.Phrase = new Phrase(viewModel.buyer.npwp, normal_font);
            tableTitle.AddCell(cellTitle1);

            cellTitle1.Phrase = new Phrase("Dalam hal ini Pihak Kedua (II) disebut sebagai PEMBELI", normal_font);
            cellTitle1.Colspan = 3;
            tableTitle.AddCell(cellTitle1);
            tableTitle.SpacingAfter = 10;
            document.Add(tableTitle);
            #endregion

            #region bodyTable
            document.Add(new Paragraph("Adapun kami, selaku Pihak Pertama (I) dan Pihak Kedua (II) akan melakukan kerjasama dengan ketentuan-ketentuan sebagai berikut : \n",normal_font));
            PdfPTable tableBody = new PdfPTable(6);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 1f, 6f, 2f, 1.5f, 2f, 2.5f });
            PdfPCell cellBodyLeft = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyLeftNoBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodyRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyRightNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyCenter = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodyCenter.Phrase = new Phrase("No", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jenis Barang", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jumlah", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Satuan", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Harga Satuan \nRp", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Harga Total \nRp", normal_font);
            tableBody.AddCell(cellBodyCenter);

            var index = 0;
            foreach (var item in viewModel.items)
            {
                index++;
                cellBodyCenter.Phrase = new Phrase(index.ToString(), normal_font);
                tableBody.AddCell(cellBodyCenter);

                cellBodyLeft.Phrase = new Phrase(item.product.name, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.quantity), normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyLeft.Phrase = new Phrase(item.uom.Unit, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.price), normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", item.price * item.quantity), normal_font);
                tableBody.AddCell(cellBodyRight);
            }

            double totalPrice = viewModel.items.Sum(a => a.quantity * a.price);
            double ppn = 0;
            if (viewModel.isUseVat)
            {
                ppn = totalPrice * 0.1;
            }
            double finalPrice = totalPrice + ppn;

            cellBodyRightNoBorder.Phrase = new Phrase("Sub Total", normal_font_bold);
            cellBodyRightNoBorder.Colspan = 5;
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", totalPrice), normal_font);
            cellBodyRight.Colspan = 5;
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase("PPN 10%", normal_font);
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", ppn), normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRightNoBorder.Phrase = new Phrase("TOTAL", normal_font);
            tableBody.AddCell(cellBodyRightNoBorder);

            cellBodyRight.Phrase = new Phrase(string.Format("{0:n2}", finalPrice), normal_font);
            tableBody.AddCell(cellBodyRight);


            tableBody.SpacingAfter = 10;
            tableBody.SpacingBefore = 5;
            document.Add(tableBody);
            #endregion



            #region sign
            document.Add(new Paragraph("Demikian surat perjanjian kontrak jual beli barang tersebut dibuat sebagaimana mestinya, \n",normal_font));
            document.Add(new Paragraph("harap menjadi perhatian adanya. \n\n", normal_font));
            document.Add(new Paragraph("Terima kasih. \n\n", normal_font));
            document.Add(new Paragraph($"Sukoharjo, {viewModel.salesContractDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font));

            PdfPTable tableSign = new PdfPTable(2);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 3f, 1f });
            
            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellBodySignNoBorder2 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            cellBodySignNoBorder.AddElement(new Phrase("Pihak Pertama (I)", normal_font));
            cellBodySignNoBorder.AddElement(new Phrase("PT DANLIRIS", normal_font));
            cellBodySignNoBorder.AddElement(new Phrase("\n\n\n\n", normal_font));
            cellBodySignNoBorder.AddElement(new Phrase(viewModel.sellerName, normal_font_underlined));
            cellBodySignNoBorder.AddElement(new Phrase(viewModel.sellerPosition, normal_font));
            tableSign.AddCell(cellBodySignNoBorder);

            cellBodySignNoBorder2.AddElement(new Phrase("Pihak Kedua (II)", normal_font));
            cellBodySignNoBorder2.AddElement(new Phrase("\n\n\n\n\n", normal_font));
            cellBodySignNoBorder2.AddElement(new Phrase(viewModel.buyer.Name, normal_font_underlined));
            cellBodySignNoBorder2.AddElement(new Phrase("Pembeli", normal_font));
            tableSign.AddCell(cellBodySignNoBorder2);

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
