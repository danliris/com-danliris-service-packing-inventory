using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentShippingExportSalesDOViewModel viewModel, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font_bold_big = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font_bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font header_font_bold_underlined = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 11);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font small_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
            Font small_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7, Font.UNDERLINE);
            //Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();
            #region fm
            PdfPTable tableFM = new PdfPTable(3);
            tableFM.WidthPercentage = 100;
            tableFM.SetWidths(new float[] { 2f, 1f,1f });

            PdfPCell cellFM1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFMBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER };

            cellFM1.Phrase = new Phrase("",normal_font);
            tableFM.AddCell(cellFM1);

            cellFMBorder.Phrase = new Phrase("FM-00-SP-14-004/R1", header_font_bold);
            tableFM.AddCell(cellFMBorder);

            cellFM1.Phrase = new Phrase("", normal_font);
            tableFM.AddCell(cellFM1);

            document.Add(tableFM);
            #endregion

            #region header
            PdfPTable tableHeader = new PdfPTable(2);
            tableHeader.WidthPercentage = 100;
            tableHeader.SetWidths(new float[] { 2f, 2f });

            PdfPCell cellHeaderContent1 = new PdfPCell() { Border = Rectangle.NO_BORDER};
            PdfPCell cellHeaderContent2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            

            //cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase("PT. DAN LIRIS", header_font_bold_big));
            cellHeaderContent1.AddElement(new Phrase("Head Office : Jl. Merapi No 23", small_font));
            cellHeaderContent1.AddElement(new Phrase("Banaran, Grogol, Sukoharjo, 57193", small_font));
            cellHeaderContent1.AddElement(new Phrase("Central Java, Indonesia", small_font));
            cellHeaderContent1.AddElement(new Phrase("Phone : (+62 271) 740888, 714400", small_font));
            cellHeaderContent1.AddElement(new Phrase("Fax : (+62 271) 740777, 735222", small_font));
            cellHeaderContent1.AddElement(new Phrase("PO BOX 166 Solo, 57100", small_font));
            cellHeaderContent1.AddElement(new Phrase("www.danliris.com", small_font));
            cellHeaderContent1.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent1.AddElement(new Phrase(viewModel.exportSalesDONo, bold_font));
            cellHeaderContent1.AddElement(new Phrase(viewModel.buyerAgent.Name, normal_font));
            tableHeader.AddCell(cellHeaderContent1);

            //cellHeaderContent2.AddElement(new Phrase("FM-00-SP-14-004/R1", header_font_bold));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase("Surakarta, "+ viewModel.date.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), normal_font));
            cellHeaderContent2.AddElement(new Phrase("Kepada", normal_font));
            cellHeaderContent2.AddElement(new Phrase("Yth. Sdr. "+ viewModel.to, small_font));
            cellHeaderContent2.AddElement(new Phrase("Bag. Gudang " + viewModel.unit.Name, small_font));
            cellHeaderContent2.AddElement(new Phrase("Export/Banaran ", small_font));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase("D.O. Penjualan Export", header_font_bold_underlined));
            cellHeaderContent2.AddElement(new Phrase("\n", normal_font));
            cellHeaderContent2.AddElement(new Phrase("Untuk melengkapi nota No. ", small_font));
            cellHeaderContent2.AddElement(new Phrase("Harap dikeluarkan barang-barang tersebut dibawah ini.", small_font));
            tableHeader.AddCell(cellHeaderContent2);

            tableHeader.SpacingAfter = 15;
            document.Add(tableHeader);
            #endregion

            #region tableBody
            PdfPTable tableBody = new PdfPTable(9);
            tableBody.WidthPercentage = 100;
            tableBody.SetWidths(new float[] { 1f, 5f, 4.2f, 2.3f, 2.3f, 2.3f, 2.3f, 2.3f, 2.3f });

            PdfPCell cellBodyLeft = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment=Element.ALIGN_LEFT };
            PdfPCell cellBodyRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellBodyCenter = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellBodyCenter.Phrase = new Phrase("No", normal_font);
            cellBodyCenter.Rowspan = 3;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("NAMA", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jenis/Kode", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("BANYAKNYA", normal_font);
            cellBodyCenter.Rowspan = 1;
            cellBodyCenter.Colspan = 6;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Jumlah", normal_font);
            cellBodyCenter.Rowspan = 2;
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Satuan", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Carton", normal_font);
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Weight", normal_font);
            cellBodyCenter.Rowspan = 1;
            cellBodyCenter.Colspan = 2;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Volume", normal_font);
            cellBodyCenter.Rowspan = 2;
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Gross", normal_font);
            cellBodyCenter.Rowspan = 1;
            cellBodyCenter.Colspan = 1;
            tableBody.AddCell(cellBodyCenter);

            cellBodyCenter.Phrase = new Phrase("Nett", normal_font);
            tableBody.AddCell(cellBodyCenter);

            int index = 0;
            Dictionary<string, double> total = new Dictionary<string, double>();
            foreach (var item in viewModel.items)
            {
                index++;
                cellBodyLeft.Phrase = new Phrase($"{index}", normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyLeft.Phrase = new Phrase(item.comodity.Name, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyLeft.Phrase = new Phrase(item.description, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRight.Phrase = new Phrase($"{item.quantity}", normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyLeft.Phrase = new Phrase(item.uom.Unit, normal_font);
                tableBody.AddCell(cellBodyLeft);

                cellBodyRight.Phrase = new Phrase($"{item.cartonQuantity}", normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(item.grossWeight == 0 ? "" : $"{item.grossWeight}", normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(item.nettWeight == 0 ? "" : $"{item.nettWeight}", normal_font);
                tableBody.AddCell(cellBodyRight);

                cellBodyRight.Phrase = new Phrase(item.volume == 0 ? "" : $"{item.volume}", normal_font);
                tableBody.AddCell(cellBodyRight);

                if (total.ContainsKey(item.uom.Unit))
                {
                    total[item.uom.Unit] += item.quantity;
                }
                else
                {
                    total.Add(item.uom.Unit, item.quantity);
                }
            }

            double totalQty = viewModel.items.Sum(a => a.quantity);
            double totalCtn = viewModel.items.Sum(a => a.cartonQuantity);
            double totalGW = viewModel.items.Sum(a => a.grossWeight);
            double totalNW = viewModel.items.Sum(a => a.nettWeight);
            double totalVol = viewModel.items.Sum(a => a.volume);

            cellBodyRight.Phrase = new Phrase("Jumlah", normal_font);
            cellBodyRight.Colspan = 3;
            cellBodyRight.VerticalAlignment = Element.ALIGN_CENTER;
            tableBody.AddCell(cellBodyRight);

            var val1 = total.Select(x => String.Format("{0}", x.Value.ToString()));
            var result1 = String.Join("\n", val1);

            var key1 = total.Select(x => String.Format("{0}", x.Key));
            var result2 = String.Join("\n", key1);

            cellBodyRight.Phrase = new Phrase($"{result1}", normal_font);
            cellBodyRight.Colspan = 1;
            cellBodyRight.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableBody.AddCell(cellBodyRight);

            cellBodyLeft.Phrase = new Phrase($"{result2}", normal_font);
            tableBody.AddCell(cellBodyLeft);

            cellBodyRight.Phrase = new Phrase($"{totalCtn}", normal_font);
            cellBodyRight.VerticalAlignment = Element.ALIGN_CENTER;
            tableBody.AddCell(cellBodyRight);

            cellBodyRight.Phrase = new Phrase(totalGW == 0 ? "" : $"{totalGW}", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRight.Phrase = new Phrase(totalNW == 0 ? "" : $"{totalNW}", normal_font);
            tableBody.AddCell(cellBodyRight);

            cellBodyRight.Phrase = new Phrase(totalVol == 0 ? "" : $"{totalVol}", normal_font);
            tableBody.AddCell(cellBodyRight);

            tableBody.SpacingAfter = 15;
            document.Add(tableBody);
            #endregion

            document.Add(new Paragraph($"Untuk bagian / dikirim kepada {viewModel.deliverTo}",normal_font));

            document.Add(new Paragraph($"Keterangan {viewModel.remark}", normal_font));
            document.Add(new Paragraph("\n",normal_font));
            #region sign
            PdfPTable tableSign = new PdfPTable(4);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f,1f,1f,3f });

            PdfPCell cellSignBorder = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellSignNoBorder = new PdfPCell() { Border = Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellSignBorder.Phrase = new Phrase("Penjualan", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("Bag.Akuntansi", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("Gudang", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignNoBorder.Phrase = new Phrase("Terima Kasih", normal_font);
            tableSign.AddCell(cellSignNoBorder);

            cellSignBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignNoBorder.Phrase = new Phrase("Bagian Shipping\n\n\n", normal_font);
            tableSign.AddCell(cellSignNoBorder);

            cellSignBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignBorder.Phrase = new Phrase("", normal_font);
            tableSign.AddCell(cellSignBorder);

            cellSignNoBorder.Phrase = new Phrase("(___________________________)", normal_font);
            tableSign.AddCell(cellSignNoBorder);

            document.Add(tableSign);

            document.Add(new Phrase("Model DL 3b", normal_font));
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
