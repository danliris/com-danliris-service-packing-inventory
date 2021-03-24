using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructurePdfTemplate
    {
        private IIdentityProvider _identityProvider;

        public GarmentShippingCostStructurePdfTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GeneratePdfTemplate(GarmentShippingCostStructureViewModel viewModel)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, 20, 20, 170, 30);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentShippingCostStructurePDFTemplatePageEvent(_identityProvider, viewModel);

            document.Open();

            #region Description
            PdfPTable tableDescription = new PdfPTable(4);
            tableDescription.SetWidths(new float[] { 4f, 0.2f, 5.8f, 2f });
            PdfPCell cellDescription = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescription.Phrase = new Phrase("INVOICE NO.", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.InvoiceNo, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.FabricType, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("NAMA BARANG / NO. POS HS", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.Comodity.Name.Replace(" ", String.Empty) + " / " + viewModel.HsCode, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("DIEKSPOR KE", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase(viewModel.Destination, normal_font);
            tableDescription.AddCell(cellDescription);
            cellDescription.Phrase = new Phrase("", normal_font);
            tableDescription.AddCell(cellDescription);

            Paragraph space = new Paragraph("\n", normal_font);
            document.Add(space);
            new PdfPCell(tableDescription);
            tableDescription.ExtendLastRow = false;
            tableDescription.SpacingAfter = 5f;
            tableDescription.SpacingBefore = 10f;
            document.Add(tableDescription);

            #endregion

            double jumlahBiayaProduksi = 0;
            double totalPercentage = 0;
            double totalBCDEF = 0;

            #region Item A
            Paragraph itemADesc = new Paragraph("A. BAHAN / KOMPONEN YANG DIIMPOR ATAU YANG TIDAK DIKETAHUI ASALNYA", normal_font);
            document.Add(itemADesc);

            AddLine(document);

            PdfPTable tableDescItemA = new PdfPTable(6);
            tableDescItemA.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDescItemA = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemA.Phrase = new Phrase("NO.", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            cellDescItemA.Phrase = new Phrase("URAIAN BARANG / POS TARIF HS", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            cellDescItemA.Phrase = new Phrase("NEGARA ASAL", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            cellDescItemA.Phrase = new Phrase("NILAI", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            cellDescItemA.Phrase = new Phrase("%", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            cellDescItemA.Phrase = new Phrase("", normal_font);
            tableDescItemA.AddCell(cellDescItemA);
            new PdfPCell(tableDescItemA);
            tableDescItemA.ExtendLastRow = false;
            document.Add(tableDescItemA);

            AddLine(document);

            PdfPTable tableDataItemA = new PdfPTable(6);
            tableDataItemA.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDataItemA = new PdfPCell() { Border = Rectangle.NO_BORDER };
            string[] dataItemA = { "RAW COTTON (5201)", "TYSSALIS ( 1108 )", "PVA ( 3905 )" };
            string[] countryItemA = { "USA/AUSTRALIA", "FRANCE", "JAPAN" };
            double[] percentageItemA = { 13.00, 1.50, 0.50 };

            var no = 0;
            var countPercentItemA = 0.00;
            foreach (var itemA in dataItemA)
            {
                cellDataItemA.Phrase = new Phrase(Convert.ToString(no + 1) + ".", normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(itemA, normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(countryItemA[no], normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(String.Format("{0:N4}", (percentageItemA[no] * viewModel.Amount) / 100), normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(String.Format("{0:n}", percentageItemA[no]), normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase("", normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                countPercentItemA += Convert.ToDouble(percentageItemA[no]);
                no++;
            }
            totalPercentage += countPercentItemA;
            new PdfPCell(tableDataItemA);
            tableDataItemA.ExtendLastRow = false;
            document.Add(tableDataItemA);

            AddLine(document);

            PdfPTable tableSummaryItemA = new PdfPTable(6);
            tableSummaryItemA.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellSummaryItemA = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSummaryItemA.Phrase = new Phrase("", normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            cellSummaryItemA.Phrase = new Phrase("", normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            cellSummaryItemA.Phrase = new Phrase("JUMLAH A", normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            var nilaiA = ( countPercentItemA * viewModel.Amount ) / 100;
            cellSummaryItemA.Phrase = new Phrase("", normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            cellSummaryItemA.Phrase = new Phrase(String.Format("{0:N}", countPercentItemA), normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            cellSummaryItemA.Phrase = new Phrase(String.Format("{0:N4}", nilaiA), normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            new PdfPCell(tableSummaryItemA);
            tableSummaryItemA.ExtendLastRow = false;
            document.Add(tableSummaryItemA);
            jumlahBiayaProduksi += nilaiA;

            #endregion

            #region Item B
            Paragraph itemBDesc = new Paragraph("B. BAHAN / KOMPONEN YANG BERASAL DARI ASEAN", normal_font);
            document.Add(itemBDesc);

            AddLine(document);

            PdfPTable tableDescItemB = new PdfPTable(6);
            tableDescItemB.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDescItemB = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemB.Phrase = new Phrase("NO.", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            cellDescItemB.Phrase = new Phrase("URAIAN BARANG / POS TARIF HS", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            cellDescItemB.Phrase = new Phrase("NEGARA ASAL", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            cellDescItemB.Phrase = new Phrase("NILAI", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            cellDescItemB.Phrase = new Phrase("%", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            cellDescItemB.Phrase = new Phrase("", normal_font);
            tableDescItemB.AddCell(cellDescItemB);
            new PdfPCell(tableDescItemB);
            tableDescItemB.ExtendLastRow = false;
            document.Add(tableDescItemB);

            AddLine(document);

            PdfPTable tableDataItemB = new PdfPTable(6);
            tableDataItemB.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDataItemB = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellDataItemB.Phrase = new Phrase("", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            cellDataItemB.Phrase = new Phrase("-", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            cellDataItemB.Phrase = new Phrase("-", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            cellDataItemB.Phrase = new Phrase("-", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            cellDataItemB.Phrase = new Phrase("-", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            cellDataItemB.Phrase = new Phrase("", normal_font);
            tableDataItemB.AddCell(cellDataItemB);
            new PdfPCell(tableDataItemB);
            tableDataItemB.ExtendLastRow = false;
            document.Add(tableDataItemB);

            AddLine(document);

            PdfPTable tableSummaryItemB = new PdfPTable(6);
            tableSummaryItemB.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellSummaryItemB = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSummaryItemB.Phrase = new Phrase("", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            cellSummaryItemB.Phrase = new Phrase("", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            cellSummaryItemB.Phrase = new Phrase("JUMLAH B", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            cellSummaryItemB.Phrase = new Phrase("", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            cellSummaryItemB.Phrase = new Phrase("", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            cellSummaryItemB.Phrase = new Phrase("", normal_font);
            tableSummaryItemB.AddCell(cellSummaryItemB);
            new PdfPCell(tableSummaryItemB);
            tableSummaryItemB.ExtendLastRow = false;
            document.Add(tableSummaryItemB);

            #endregion

            #region Item C
            Paragraph itemCDesc = new Paragraph("C. BAHAN / KOMPONEN YANG BERASAL DARI INDONESIA", normal_font);
            document.Add(itemCDesc);

            AddLine(document);

            PdfPTable tableDescItemC = new PdfPTable(6);
            tableDescItemC.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDescItemC = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemC.Phrase = new Phrase("NO.", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            cellDescItemC.Phrase = new Phrase("URAIAN BARANG / POS TARIF HS", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            cellDescItemC.Phrase = new Phrase("NAMA PEMASOK", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            cellDescItemC.Phrase = new Phrase("NILAI", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            cellDescItemC.Phrase = new Phrase("%", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            cellDescItemC.Phrase = new Phrase("", normal_font);
            tableDescItemC.AddCell(cellDescItemC);
            new PdfPCell(tableDescItemC);
            tableDescItemC.ExtendLastRow = false;
            document.Add(tableDescItemC);

            AddLine(document);

            PdfPTable tableDataItemC = new PdfPTable(6);
            tableDataItemC.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellDataItemC = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDataItemC.Phrase = new Phrase("", normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            cellDataItemC.Phrase = new Phrase("ACCESSORIES", normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            cellDataItemC.Phrase = new Phrase("INDONESIA", normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            cellDataItemC.Phrase = new Phrase(String.Format("{0:N4}", (10 * viewModel.Amount) / 100), normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            cellDataItemC.Phrase = new Phrase(String.Format("{0:n}", 10), normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            cellDataItemC.Phrase = new Phrase("", normal_font);
            tableDataItemC.AddCell(cellDataItemC);
            new PdfPCell(tableDataItemC);
            tableDataItemC.ExtendLastRow = false;
            document.Add(tableDataItemC);

            AddLine(document);

            PdfPTable tableSummaryItemC = new PdfPTable(6);
            tableSummaryItemC.SetWidths(new float[] { 1f, 3f, 2f, 2f, 2f, 2f });
            PdfPCell cellSummaryItemC = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSummaryItemC.Phrase = new Phrase("", normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            cellSummaryItemC.Phrase = new Phrase("", normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            cellSummaryItemC.Phrase = new Phrase("JUMLAH C", normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            var nilaiC = ( 10 * viewModel.Amount ) / 100;
            totalBCDEF += nilaiC;
            cellSummaryItemC.Phrase = new Phrase("", normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            cellSummaryItemC.Phrase = new Phrase(String.Format("{0:N}", 10), normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            cellSummaryItemC.Phrase = new Phrase(String.Format("{0:N4}", nilaiC), normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            new PdfPCell(tableSummaryItemC);
            tableSummaryItemC.ExtendLastRow = false;
            document.Add(tableSummaryItemC);

            jumlahBiayaProduksi += nilaiC;
            totalPercentage += 10;

            #endregion

            #region Item D
            Paragraph itemDDesc = new Paragraph("D.BIAYA PRODUKSI LANGSUNG", normal_font);
            document.Add(itemDDesc);

            //AddLine(document);

            PdfPTable tableDescItemD = new PdfPTable(6);
            tableDescItemD.SetWidths(new float[] { 8f, 0f, 0f, 0f, 2f, 2f });
            PdfPCell cellDescItemD = new PdfPCell() { Border = Rectangle.NO_BORDER };
            string[] dataItemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
            double[] percentageItemD = { 10, 54 };

            var countPercentItemD = 0.00;
            for (var i = 0; i < 2; i++)
            {
                cellDescItemD.Phrase = new Phrase("- " + dataItemD[i], normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase(String.Format("{0:n}", percentageItemD[i]), normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase(String.Format("{0:N4}", (percentageItemD[i] * viewModel.Amount) / 100), normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                countPercentItemD += percentageItemD[i];
            }
            totalPercentage += countPercentItemD;
            new PdfPCell(tableDescItemD);
            tableDescItemD.ExtendLastRow = false;
            document.Add(tableDescItemD);

            AddLine(document);

            PdfPTable tableSummaryItemD = new PdfPTable(6);
            tableSummaryItemD.SetWidths(new float[] { 2f, 2f, 2f, 2f, 2f, 2f });
            PdfPCell cellSummaryItemD = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSummaryItemD.Phrase = new Phrase("", normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            cellSummaryItemD.Phrase = new Phrase("", normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            cellSummaryItemD.Phrase = new Phrase("BIAYA PRODUKSI", normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            var nilaiD = ( countPercentItemD * viewModel.Amount ) / 100;
            totalBCDEF += nilaiD;
            jumlahBiayaProduksi += nilaiD;
            cellSummaryItemD.Phrase = new Phrase("", normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            cellSummaryItemD.Phrase = new Phrase(String.Format("{0:N}", countPercentItemD), normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            cellSummaryItemD.Phrase = new Phrase(String.Format("{0:N4}", jumlahBiayaProduksi), normal_font);
            tableSummaryItemD.AddCell(cellSummaryItemD);
            new PdfPCell(tableSummaryItemD);
            tableSummaryItemD.ExtendLastRow = false;
            tableSummaryItemD.SpacingAfter = 10f;
            document.Add(tableSummaryItemD);

            #endregion

            #region Item E

            PdfPTable tableDescItemE = new PdfPTable(6);
            tableDescItemE.SetWidths(new float[] { 8f, 0f, 0f, 0f, 2f, 2f });
            PdfPCell cellDescItemE = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemE.Phrase = new Phrase("E. KEUNTUNGAN", normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            cellDescItemE.Phrase = new Phrase("", normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            cellDescItemE.Phrase = new Phrase("", normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            cellDescItemE.Phrase = new Phrase("", normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            cellDescItemE.Phrase = new Phrase(String.Format("{0:n}", 10), normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            cellDescItemE.Phrase = new Phrase(String.Format("{0:N4}", (10 * viewModel.Amount) / 100), normal_font);
            tableDescItemE.AddCell(cellDescItemE);
            new PdfPCell(tableDescItemE);
            tableDescItemE.ExtendLastRow = false;
            document.Add(tableDescItemE);

            AddLine(document);

            PdfPTable tableSummaryItemE = new PdfPTable(6);
            tableSummaryItemE.SetWidths(new float[] { 2f, 2f, 2f, 2f, 2f, 2f });
            PdfPCell cellSummaryItemE = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSummaryItemE.Phrase = new Phrase("", normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);
            cellSummaryItemE.Phrase = new Phrase("", normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);
            cellSummaryItemE.Phrase = new Phrase("EKS HARGA PABRIK", normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);
            var nilaiE = (10 * viewModel.Amount) / 100;
            totalBCDEF += nilaiE;
            jumlahBiayaProduksi += nilaiE;
            cellSummaryItemE.Phrase = new Phrase("", normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);

            totalPercentage += 10;
            cellSummaryItemE.Phrase = new Phrase(String.Format("{0:N}", totalPercentage), normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);
            cellSummaryItemE.Phrase = new Phrase(String.Format("{0:N4}", jumlahBiayaProduksi), normal_font);
            tableSummaryItemE.AddCell(cellSummaryItemE);
            new PdfPCell(tableSummaryItemE);
            tableSummaryItemE.ExtendLastRow = false;
            tableSummaryItemE.SpacingAfter = 10f;
            document.Add(tableSummaryItemE);

            #endregion

           // document.NewPage();

            #region Item F
            //Paragraph spaceF = new Paragraph("\n", normal_font);
            //document.Add(spaceF);
            //Paragraph itemFDesc = new Paragraph("F. BIAYA PENGANGKUTAN BARANG SAMPAI KE KAPAL", normal_font);
            //document.Add(itemFDesc);

            //AddLine(document);

            PdfPTable tableDescItemF = new PdfPTable(6);
            tableDescItemF.SetWidths(new float[] { 8f, 0f, 0f, 0f, 2f, 2f });
            PdfPCell cellDescItemF = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemF.Phrase = new Phrase("F. BIAYA PENGANGKUTAN BARANG SAMPAI KE KAPAL", normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            cellDescItemF.Phrase = new Phrase("", normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            cellDescItemF.Phrase = new Phrase("", normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            cellDescItemF.Phrase = new Phrase("", normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            cellDescItemF.Phrase = new Phrase(String.Format("{0:n}", 1), normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            cellDescItemF.Phrase = new Phrase(String.Format("{0:N4}", (1 * viewModel.Amount) / 100), normal_font);
            tableDescItemF.AddCell(cellDescItemF);
            var nilaiF = (1 * viewModel.Amount) / 100;
            totalBCDEF += nilaiF;
            jumlahBiayaProduksi += nilaiF;
            new PdfPCell(tableDescItemF);
            tableDescItemF.ExtendLastRow = false;
            document.Add(tableDescItemF);

            AddLine(document);

            #endregion

            #region Item G

            PdfPTable tableDescItemG = new PdfPTable(6);
            tableDescItemG.SetWidths(new float[] { 10f, 0f, 0f, 0f, 0f, 2f });
            PdfPCell cellDescItemG = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescItemG.Phrase = new Phrase("G. HARGA SAMPAI KE KAPAL ( F O B )", normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            cellDescItemG.Phrase = new Phrase("", normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            cellDescItemG.Phrase = new Phrase("", normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            cellDescItemG.Phrase = new Phrase("", normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            cellDescItemG.Phrase = new Phrase("", normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            cellDescItemG.Phrase = new Phrase(String.Format("{0:N4}", jumlahBiayaProduksi), normal_font);
            tableDescItemG.AddCell(cellDescItemG);
            new PdfPCell(tableDescItemG);
            tableDescItemG.ExtendLastRow = false;
            tableDescItemG.SpacingAfter = 20f;
            document.Add(tableDescItemG);
            #endregion

            #region Prosentase Komponen Lokal

            PdfPTable tableDescKomponenLokal = new PdfPTable(3);
            tableDescKomponenLokal.SetWidths(new float[] { 2f, 0.8f, 2f});
            PdfPCell cellDescKomponenLokal = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDescKomponenLokal.Phrase = new Phrase("PROSENTASE KOMPONEN LOKAL = ", normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            PdfPCell cellRumus1 = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            cellRumus1.Phrase = new Phrase("B + C + D + E + F", normal_font);
            cellRumus1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellRumus1);
            cellDescKomponenLokal.Phrase = new Phrase("X 100 %", normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellDescKomponenLokal.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellDescKomponenLokal.Rowspan = 1;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            cellDescKomponenLokal.Phrase = new Phrase("G", normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            cellDescKomponenLokal.Phrase = new Phrase(" = ", normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            PdfPCell cellRumus2 = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            cellRumus2.Phrase = new Phrase(String.Format("{0:N4}", totalBCDEF), normal_font);
            cellRumus2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellRumus2);
            cellDescKomponenLokal.Phrase = new Phrase("X 100 %", normal_font);
            cellDescKomponenLokal.Rowspan = 1;
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellDescKomponenLokal.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            cellDescKomponenLokal.Phrase = new Phrase(String.Format("{0:N4}", jumlahBiayaProduksi), normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            cellDescKomponenLokal.Phrase = new Phrase(" = ", normal_font);
            cellDescKomponenLokal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);
            PdfPCell cellHasil = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            var hasil = (totalBCDEF / jumlahBiayaProduksi) * 100;
            cellHasil.Phrase = new Phrase(String.Format("{0:N4}", hasil) + " %", normal_font);
            cellHasil.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellHasil);
            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            new PdfPCell(tableDescKomponenLokal);
            tableDescKomponenLokal.ExtendLastRow = false;
            document.Add(tableDescKomponenLokal);
            #endregion
            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        Chunk GetScalledChunk(string text, Font font, float scalling = 0.5f)
        {
            Chunk chunk = new Chunk(text, font);
            chunk.SetHorizontalScaling(scalling);
            return chunk;
        }

        public void AddLine(Document document)
        {
            Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.5F, 100.0F, BaseColor.Black, Element.ALIGN_LEFT, 1)));
            p.SpacingBefore = -10f;
            document.Add(p);
        }
    }

    class GarmentShippingCostStructurePDFTemplatePageEvent : PdfPageEventHelper
    {
        private IIdentityProvider _identityProvider;
        private GarmentShippingCostStructureViewModel _viewModel;

        public GarmentShippingCostStructurePDFTemplatePageEvent(IIdentityProvider identityProvider, GarmentShippingCostStructureViewModel viewModel)
        {
            _identityProvider = identityProvider;
            _viewModel = viewModel;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin, marginTop = document.TopMargin, marginRight = document.RightMargin, marginBottom = document.BottomMargin;

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region JUDUL

            var judulY = height - marginTop + 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "FORMAT STRUKTUR BIAYA PER UNIT", width / 2, judulY, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft + 150, height - marginTop + 5);
            cb.LineTo(width - marginRight - 150, height - marginTop + 5);
            cb.Stroke();

            #endregion


            #region SUB JUDUL
            var subJudulY = height - marginTop - 5;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "( DALAM US $ )", width / 2, subJudulY, 0);
            #endregion

            #region PRINTED

            //var printY = marginBottom - 10;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Waktu Cetak : " + DateTimeOffset.Now.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy H:mm:ss zzz"), marginLeft, printY, 0);

            #endregion

            cb.EndText();
        }
    }
}
