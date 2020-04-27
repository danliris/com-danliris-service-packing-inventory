using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class FabricQualityControlPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(FabricQualityControlViewModel viewModel, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header
            PdfPTable headerTable = new PdfPTable(2);
            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2, PaddingTop = 1, PaddingBottom = 1 };
            PdfPCell cellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER, PaddingTop = 1, PaddingBottom = 1 };
            float[] widths = new float[] { 1f, 4f };
            headerTable.SetWidths(widths);
            headerTable.WidthPercentage = 100;

            cellHeaderCS2.Phrase = new Phrase("FM-FP-00-PC-19-020", body_bold_font);
            cellHeaderCS2.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(cellHeaderCS2);

            cellHeaderCS2.Phrase = new Phrase("Pemeriksaan Kain", header_font);
            cellHeaderCS2.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS2);

            //cellHeaderCS4.Phrase = new Phrase("KARTU PENGANTAR PROSES PRODUKSI", normal_font_underlined);
            //cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            //headerTable.AddCell(cellHeaderCS4);

            //cellHeaderCS4.Phrase = new Phrase($"Delivery: {DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", body_font);
            ////cellHeader.Phrase = new Phrase($"Delivery: {viewModel.ProductionOrder.DeliveryDate.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            //cellHeaderCS4.HorizontalAlignment = Element.ALIGN_RIGHT;
            //headerTable.AddCell(cellHeaderCS4);

            //cellHeaderCS4.Phrase = new Phrase("--------------------------------", bold_font);
            //cellHeaderCS4.HorizontalAlignment = Element.ALIGN_RIGHT;
            //headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS2.Phrase = new Phrase("", bold_font);
            cellHeaderCS2.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS2);

            cellHeader.Phrase = new Phrase("Nomor Pemeriksaan Kain", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Code}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Tanggal IM", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.DateIm.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Shift", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ShiftIm}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Operator IM", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.OperatorIm}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Nomor Mesin IM", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.MachineNoIm}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Nomor Kereta", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.CartNo}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Nomor Order", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrderNo}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Jenis Order", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrderType}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Konstruksi", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Construction}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Buyer", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Buyer}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Warna", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Color}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Jumlah Order", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.OrderQuantity}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("Packing Instruction", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.PackingInstruction}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            document.Add(headerTable);
            #endregion Header

            document.Add(new Paragraph("\n"));

            #region Body Table

            PdfPTable bodyTable = new PdfPTable(9);
            float[] bodyTableWidths = new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
            bodyTable.SetWidths(bodyTableWidths);
            bodyTable.WidthPercentage = 100;

            #region Set Body Table Header
            PdfPCell bodyTableHeader = new PdfPCell() { FixedHeight = 20 };
            //PdfPCell table1RightCellHeader = new PdfPCell() { FixedHeight = 20, Colspan = 4 };

            bodyTableHeader.Phrase = new Phrase("No Pcs", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Panjang Pcs", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Lebar Pcs", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Aval A", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Aval B", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Aval Sambungan", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Sampel", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Nilai", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);

            bodyTableHeader.Phrase = new Phrase("Grade", body_bold_font);
            bodyTableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTableHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable.AddCell(bodyTableHeader);
            #endregion

            #region Set Body Table Value
            PdfPCell bodyTableCell = new PdfPCell() { FixedHeight = 15 };

            foreach (var fabricGradeTest in viewModel.FabricGradeTests)
            {
                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.PcsNo}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.InitLength}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.Width}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.AvalALength}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.AvalBLength}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.AvalConnectionLength}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.SampleLength}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.Score}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);

                bodyTableCell.Phrase = new Phrase($"{fabricGradeTest.Grade}", body_font);
                bodyTableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTableCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable.AddCell(bodyTableCell);
            }

            //try
            //{
            //    document.Add(bodyTable1);
            //}
            //catch(Exception e)
            //{
            //    throw new Exception(e.Message);
            //}

            #endregion

            document.Add(bodyTable);
            #endregion

            document.Add(new Paragraph("\n"));

            #region Footer Table


            PdfPTable footerTable = new PdfPTable(3);
            float[] footerTableWidths = new float[] { 30f, 30f, 30f };
            footerTable.SetWidths(footerTableWidths);
            footerTable.WidthPercentage = 100;

            PdfPCell thinCell = new PdfPCell() { FixedHeight = 20 };
            PdfPCell thickCell = new PdfPCell() { FixedHeight = 60 };

            thinCell.Phrase = new Phrase("Dibuat Oleh", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            thinCell.Phrase = new Phrase("Mengetahui", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            thinCell.Phrase = new Phrase("Menyetujui", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            for (int i = 0; i < 3; i++)
            {
                thickCell.Phrase = new Phrase("", body_bold_font);
                thickCell.HorizontalAlignment = Element.ALIGN_CENTER;
                thickCell.VerticalAlignment = Element.ALIGN_CENTER;
                footerTable.AddCell(thickCell);
            }

            thinCell.Phrase = new Phrase("ADMIN QC", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            thinCell.Phrase = new Phrase("KABAG QC", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            thinCell.Phrase = new Phrase("KASUBSIE QC", body_bold_font);
            thinCell.HorizontalAlignment = Element.ALIGN_CENTER;
            thinCell.VerticalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(thinCell);

            document.Add(footerTable);
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
