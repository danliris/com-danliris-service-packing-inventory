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
            tableDescription.SetWidths(new float[] { 4f, 0.2f, 4.8f, 3f });
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

            decimal jumlahBiayaProduksi = 0m;
            decimal totalPercentage = 0m;
            decimal totalBCDEF = 0m;

            string[] dataItemA = null;
            string[] countryItemA = null;
            decimal[] percentageItemA = null;

            string[] dataItemC = null;
            string[] countryItemC = null;
            decimal[] percentageItemC = null;

            string[] dataItemD = null;
            decimal[] percentageItemD = null;

            if (viewModel.FabricType == "COTTON")
            {
                string[] itemA = { "RAW COTTON (5201)", "TYSSALIS ( 1108 )", "PVA ( 3905 )" };
                string[] countryA = { "USA/AUSTRALIA", "FRANCE", "JAPAN" };
                decimal[] percentageA = { 13.00m, 1.50m, 0.50m };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "ACCESSORIES" };
                string[] countryC = { "INDONESIA" };
                decimal[] percentageC = { 10.00m };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 54.00m };

                dataItemD = itemD;
                percentageItemD = percentageD;

            } else if (viewModel.FabricType == "CVC ( COTTON / POLYESTER )")
            {
                string[] itemA = { "RAW COTTON (5201)", "TYSSALIS ( 1108 )", "PVA ( 3905 )" };
                string[] countryA = { "USA/AUSTRALIA", "FRANCE", "JAPAN" };
                decimal[] percentageA = { 4.60m, 0.80m, 0.30m };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "POLYESTER (5503)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA" };
                decimal[] percentageC = { 8.15m, 10.00m };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 55.15m };

                dataItemD = itemD;
                percentageItemD = percentageD;
            } else if(viewModel.FabricType == "POLYESTER")
            {
                string[] itemA = { "TYSSALIS (1108)", "P V A (3905)" };
                string[] countryA = { "FRANCE", "JAPAN" };
                decimal[] percentageA = { 1.90m, 0.65m };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "POLYESTER (5503)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA" };
                decimal[] percentageC = { 51.45m, 10.00m };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 15.00m };

                dataItemD = itemD;
                percentageItemD = percentageD;
            } else if (viewModel.FabricType == "TC ( POLYESTER / COTTON )")
            {
                string[] itemA = { "RAW COTTON (5201)", "TYSSALIS (1108)", "P V A (3905)" };
                string[] countryA = { "USA/AUSTRALIA", "FRANCE", "JAPAN" };
                decimal[] percentageA = { 5.30M, 1.40M, 0.35M };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "POLYESTER (5503)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA" };
                decimal[] percentageC = { 3.55M, 10.00M };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 58.40M };

                dataItemD = itemD;
                percentageItemD = percentageD;
            } else if (viewModel.FabricType == "VISCOSE")
            {
                string[] itemA = { "TYSSALIS (1108)", "P V A (3905)" };
                string[] countryA = { "FRANCE", "JAPAN" };
                decimal[] percentageA = { 1.90M, 0.65M };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "RAYON (5516)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA" };
                decimal[] percentageC = { 51.45M, 10.00M };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 15.00M };

                dataItemD = itemD;
                percentageItemD = percentageD;
            } else if (viewModel.FabricType == "VISCOSE / POLYESTER")
            {
                string[] itemA = { "TYSSALIS (1108)", "P V A (3905)" };
                string[] countryA = { "FRANCE", "JAPAN" };
                decimal[] percentageA = { 1.85M, 0.95M };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "RAYON (5516)", "POLYESTER (5503)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA", "" };
                decimal[] percentageC = { 44.70M, 6.50M, 10.00M };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 15.00M };

                dataItemD = itemD;
                percentageItemD = percentageD;
            } else if (viewModel.FabricType == "VISCOSE FUJIETTE")
            {
                string[] itemA = { "TYSSALIS (1108)", "P V A (3905)" };
                string[] countryA = { "FRANCE", "JAPAN" };
                decimal[] percentageA = { 1.90M, 0.65M };

                dataItemA = itemA;
                countryItemA = countryA;
                percentageItemA = percentageA;

                string[] itemC = { "RAYON FUJIETTE (5406)", "ACCESSORIES" };
                string[] countryC = { "INDONESIA", "INDONESIA" };
                decimal[] percentageC = { 51.45M, 10.00M };

                dataItemC = itemC;
                countryItemC = countryC;
                percentageItemC = percentageC;

                string[] itemD = { "BURUH", "BIAYA LANGSUNG LAINNYA" };
                decimal[] percentageD = { 10.00m, 15.00M };

                dataItemD = itemD;
                percentageItemD = percentageD;
            }

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


            var no = 0;
            decimal countPercentItemA = 0m;
            decimal nilaiA = 0m;
            foreach (var itemA in dataItemA)
            {
                cellDataItemA.Phrase = new Phrase(Convert.ToString(no + 1) + ".", normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(itemA, normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(countryItemA[no], normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                nilaiA += Convert.ToDecimal(String.Format("{0:N4}", (percentageItemA[no] * Convert.ToDecimal(viewModel.Amount)) / 100));
                cellDataItemA.Phrase = new Phrase(String.Format("{0:N4}", (percentageItemA[no] * Convert.ToDecimal(viewModel.Amount)) / 100), normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase(String.Format("{0:n}", percentageItemA[no]), normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                cellDataItemA.Phrase = new Phrase("", normal_font);
                tableDataItemA.AddCell(cellDataItemA);
                countPercentItemA += percentageItemA[no];
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
            cellSummaryItemA.Phrase = new Phrase("", normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            cellSummaryItemA.Phrase = new Phrase(String.Format("{0:N}", countPercentItemA), normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);

            jumlahBiayaProduksi += Convert.ToDecimal(String.Format("{0:N4}",nilaiA));
            cellSummaryItemA.Phrase = new Phrase(String.Format("{0:N4}", nilaiA), normal_font);
            tableSummaryItemA.AddCell(cellSummaryItemA);
            new PdfPCell(tableSummaryItemA);
            tableSummaryItemA.ExtendLastRow = false;
            document.Add(tableSummaryItemA);

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

            var noC = 0;
            decimal countPercentItemC = 0m;
            decimal nilaiC = 0m;
            foreach (var itemC in dataItemC)
            {
                cellDataItemC.Phrase = new Phrase(Convert.ToString(noC + 1) + ".", normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                cellDataItemC.Phrase = new Phrase(itemC, normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                cellDataItemC.Phrase = new Phrase(countryItemC[noC], normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                nilaiC += Convert.ToDecimal(String.Format("{0:N4}", (percentageItemC[noC] * Convert.ToDecimal(viewModel.Amount)) / 100));
                cellDataItemC.Phrase = new Phrase(String.Format("{0:N4}", (percentageItemC[noC] * Convert.ToDecimal(viewModel.Amount)) / 100), normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                cellDataItemC.Phrase = new Phrase(String.Format("{0:n}", percentageItemC[noC]), normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                cellDataItemC.Phrase = new Phrase("", normal_font);
                tableDataItemC.AddCell(cellDataItemC);
                countPercentItemC += percentageItemC[noC];
                noC++;
            }
            totalPercentage += countPercentItemC;
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
            totalBCDEF += Convert.ToDecimal(String.Format("{0:N4}", nilaiC));
            cellSummaryItemC.Phrase = new Phrase("", normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            cellSummaryItemC.Phrase = new Phrase(String.Format("{0:N}", countPercentItemC), normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            jumlahBiayaProduksi += Convert.ToDecimal(String.Format("{0:N4}", nilaiC));
            cellSummaryItemC.Phrase = new Phrase(String.Format("{0:N4}", nilaiC), normal_font);
            tableSummaryItemC.AddCell(cellSummaryItemC);
            new PdfPCell(tableSummaryItemC);
            tableSummaryItemC.ExtendLastRow = false;
            document.Add(tableSummaryItemC);

            #endregion

            #region Item D
            Paragraph itemDDesc = new Paragraph("D.BIAYA PRODUKSI LANGSUNG", normal_font);
            document.Add(itemDDesc);

            //AddLine(document);

            PdfPTable tableDescItemD = new PdfPTable(6);
            tableDescItemD.SetWidths(new float[] { 8f, 0f, 0f, 0f, 2f, 2f });
            PdfPCell cellDescItemD = new PdfPCell() { Border = Rectangle.NO_BORDER };

            var noD = 0;
            decimal countPercentItemD = 0;
            decimal nilaiD = 0;
            foreach (var itemD in dataItemD)
            {
                cellDescItemD.Phrase = new Phrase("- " + itemD, normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase("", normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                cellDescItemD.Phrase = new Phrase(String.Format("{0:n}", percentageItemD[noD]), normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                nilaiD += Convert.ToDecimal(String.Format("{0:N4}", (percentageItemD[noD] * Convert.ToDecimal(viewModel.Amount)) / 100));
                cellDescItemD.Phrase = new Phrase(String.Format("{0:N4}", (percentageItemD[noD] * Convert.ToDecimal(viewModel.Amount)) / 100), normal_font);
                tableDescItemD.AddCell(cellDescItemD);
                countPercentItemD += percentageItemD[noD];
                noD++;
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
            totalBCDEF += Convert.ToDecimal(String.Format("{0:N4}", nilaiD));
            jumlahBiayaProduksi += Convert.ToDecimal(String.Format("{0:N4}", nilaiD));
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
            decimal nilaiE = Convert.ToDecimal(String.Format("{0:N4}", (10.00M * Convert.ToDecimal(viewModel.Amount)) / 100));
            totalBCDEF += Convert.ToDecimal(String.Format("{0:N4}", nilaiE));
            jumlahBiayaProduksi += Convert.ToDecimal(String.Format("{0:N4}", nilaiE));
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
            decimal nilaiF = Convert.ToDecimal(String.Format("{0:N4}",(1.00M * Convert.ToDecimal(viewModel.Amount)) / 100));
            totalBCDEF += Convert.ToDecimal(String.Format("{0:N4}", nilaiF));
            jumlahBiayaProduksi += Convert.ToDecimal(String.Format("{0:N4}", nilaiF));
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
            decimal hasil = (totalBCDEF / jumlahBiayaProduksi) * 100;
            cellHasil.Phrase = new Phrase(String.Format("{0:N4}", hasil) + " %", normal_font);
            cellHasil.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableDescKomponenLokal.AddCell(cellHasil);
            cellDescKomponenLokal.Phrase = new Phrase("", normal_font);
            tableDescKomponenLokal.AddCell(cellDescKomponenLokal);

            new PdfPCell(tableDescKomponenLokal);
            tableDescKomponenLokal.ExtendLastRow = false;
            document.Add(tableDescKomponenLokal);
            #endregion

            #region signature
            PdfPTable tableSignature = new PdfPTable(1);
            //actual width of table in points

            tableSignature.TotalWidth = 120f;

            //fix the absolute width of the table

            tableSignature.LockedWidth = true;
            PdfPCell cellSignature = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellSignature.Phrase = new Phrase("PT DANLIRIS", normal_font);
            cellSignature.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tableSignature.AddCell(cellSignature);
            cellSignature.Phrase = new Phrase("SURAKARTA, " + viewModel.Date.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy"), normal_font);
            cellSignature.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tableSignature.AddCell(cellSignature);

            cellSignature.Phrase = new Phrase("\n", normal_font);
            tableSignature.AddCell(cellSignature);
            cellSignature.Phrase = new Phrase("\n", normal_font);
            tableSignature.AddCell(cellSignature);
            PdfPCell cellTtd = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER };
            cellTtd.Phrase = new Phrase("", normal_font);
            tableSignature.AddCell(cellTtd);

            new PdfPCell(tableSignature);
            tableSignature.ExtendLastRow = false;
            // Creates a paragraph indentated by the left by 20 units.
            var paragraphSignature = new Paragraph();

            // You insert the table into the indentated paragraph.
            paragraphSignature.Add(tableSignature);

            paragraphSignature.IndentationLeft = 400f;
            document.Add(paragraphSignature);
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

            cb.MoveTo(marginLeft + 200, height - marginTop + 5);
            cb.LineTo(width - marginRight - 200, height - marginTop + 5);
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
