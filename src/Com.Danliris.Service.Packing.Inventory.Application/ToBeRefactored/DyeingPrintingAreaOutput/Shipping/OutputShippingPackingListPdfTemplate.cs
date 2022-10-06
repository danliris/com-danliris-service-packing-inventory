using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingPackingListPdfTemplate
    {

        private IIdentityProvider _identityProvider;

        public OutputShippingPackingListPdfTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GeneratePdfTemplate(OutputShippingViewModel viewModel)
        {
            var newItems = viewModel.ShippingProductionOrders.
                GroupBy(x => new { x.PackingListBaleNo }).
                Select(x => new {
                    PackingListBaleNo = x.Key.PackingListBaleNo,
                    Balance = x.Sum(s => s.Qty),
                    PackingQty = x.Sum(s => s.QtyPacking),
                    PackingListNet = x.Sum(s => s.PackingListNet),
                    PackingListGross = x.Sum(s => s.PackingListGross)

                });

            //foreach (var itemA in viewModel.ShippingProductionOrders.GroupBy( x => new { x.PackingListBaleNo })) 
            //{
            //    newItems.Add(itemA);
            //}

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, 20, 20, 170, 60);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new OutputShippingPackingListPDFTemplatePageEvent(_identityProvider, viewModel);

            document.Open();

            PdfPCell cellBorderBottomRight = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBorderBottom = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            
           
            PdfPTable tableDescription = new PdfPTable(3);
            tableDescription.SetWidths(new float[] { 2f, 0.2f, 12f });
            PdfPCell cellDesscription = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellDesscription.Phrase = new Phrase("DESCRIPTION ", normal_font);
            tableDescription.AddCell(cellDesscription);
            cellDesscription.Phrase = new Phrase(":", normal_font);
            tableDescription.AddCell(cellDesscription);
            cellDesscription.Phrase = new Phrase("" + viewModel.ShippingProductionOrders.First().DestinationBuyerName, normal_font);
            tableDescription.AddCell(cellDesscription);

            cellDesscription.Phrase = new Phrase("", normal_font);
            tableDescription.AddCell(cellDesscription);
            cellDesscription.Phrase = new Phrase("", normal_font);
            tableDescription.AddCell(cellDesscription);
            cellDesscription.Phrase = new Phrase("" + viewModel.PackingListDescription, normal_font);
            tableDescription.AddCell(cellDesscription);

            new PdfPCell(tableDescription);
            tableDescription.ExtendLastRow = false;
            document.Add(tableDescription);

            #region Cartoon/Bale
            if (viewModel.PackingType == "CARTON/BALE")
            {
                foreach (var itemA in newItems)
                {
                    PdfPTable tableDetail = new PdfPTable(6);
                    var width = new List<float> { 1f, 1f, 1f, 1f };
                    for (int i = 0; i < 2; i++) width.Add(1f);
                    // width.AddRange(new List<float> { 1f});

                    PdfPCell cellDetailLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 6, Padding = 0.5f, Phrase = new Phrase("") };
                    tableDetail.AddCell(cellDetailLine);
                    tableDetail.AddCell(cellDetailLine);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("CARTON NO.", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("COLOUR", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("ROLL", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("Quantity In Meters", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("WEIGHT IN KGS", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 2;
                    cellBorderBottomRight.Rowspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);


                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("NET", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 1;
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("GROSS", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 1;
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);

                    //cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("CTNS", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 1;
                    //cellBorderBottomRight.Rowspan = 2;
                    //tableDetail.AddCell(cellBorderBottomRight);
                    foreach (var detail in viewModel.ShippingProductionOrders.Where(x => x.PackingListBaleNo == itemA.PackingListBaleNo))
                    {
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.PackingListBaleNo}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Color}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.QtyPacking}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Qty}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.PackingListNet}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.PackingListGross}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                    }
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("SUBTOTAL", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemA.PackingQty}", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemA.Balance}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemA.PackingListNet}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemA.PackingListGross}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    new PdfPCell(tableDetail);

                    tableDetail.ExtendLastRow = false;
                    tableDetail.WidthPercentage = 95f;
                    tableDetail.SpacingAfter = 10f;

                    document.Add(tableDetail);
                };

                #region GrandTotal
                var grandTotalPackingResult = (newItems.Sum(x => x.PackingQty)).ToString();
                var grandTotalBalanceResult = (newItems.Sum(x => x.Balance)).ToString();
                var grandTotalNetResult = (newItems.Sum(x => x.PackingListNet)).ToString();
                var grandTotalGrossResult = (newItems.Sum(x => x.PackingListGross)).ToString();
                PdfPTable tableGrandTotal = new PdfPTable(6);
                tableGrandTotal.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 6, Padding = 0.5f, Phrase = new Phrase("") };

                tableGrandTotal.AddCell(cellHeaderLine);
                tableGrandTotal.AddCell(cellHeaderLine);
                tableGrandTotal.AddCell(new PdfPCell()
                {
                    Colspan = 2,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    Phrase = new Phrase("GRAND TOTAL ...................", normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    Colspan = 1,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalPackingResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalBalanceResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalNetResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalGrossResult, normal_font)
                });
                tableGrandTotal.AddCell(cellHeaderLine);
                new PdfPCell(tableGrandTotal);
                tableGrandTotal.ExtendLastRow = false;
                tableGrandTotal.WidthPercentage = 95f;
                tableGrandTotal.SpacingAfter = 5f;
                document.Add(tableGrandTotal);
                #endregion
            }
            #endregion
            else
            {
                var newitemsB = viewModel.ShippingProductionOrders.GroupBy(x => new { x.Color }).Select(x => new
                {
                    PackingListBaleNo = x.FirstOrDefault().PackingListBaleNo,
                    Balance = x.Sum( s => s.Qty),
                    PackingQty = x.Sum( s => s.QtyPacking),
                    PackingListNet = x.Sum(s => s.PackingListNet),
                    PackingListGross = x.Sum( s => s.PackingListGross),
                    Color = x.Key.Color

                }).OrderBy( x => x.Color);
                foreach (var itemB in newitemsB)
                {
                    #region Item

                    PdfPTable tableItem = new PdfPTable(3);
                    tableItem.SetWidths(new float[] { 2f, 0.2f, 12f });
                    PdfPCell cellItemContent = new PdfPCell() { Border = Rectangle.NO_BORDER };

                    cellItemContent.Phrase = new Phrase("COLOUR", normal_font);
                    tableItem.AddCell(cellItemContent);
                    cellItemContent.Phrase = new Phrase(":", normal_font);
                    tableItem.AddCell(cellItemContent);
                    cellItemContent.Phrase = new Phrase("  " + itemB.Color, normal_font);
                    tableItem.AddCell(cellItemContent);

                    new PdfPCell(tableItem);
                    tableItem.ExtendLastRow = false;
                    document.Add(tableItem);

                    #endregion
                    PdfPTable tableDetail = new PdfPTable(6);
                    var width = new List<float> { 1f, 1f, 1f, 1f };
                    for (int i = 0; i < 2; i++) width.Add(1f);
                    // width.AddRange(new List<float> { 1f});

                    PdfPCell cellDetailLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 6, Padding = 0.5f, Phrase = new Phrase("") };
                    tableDetail.AddCell(cellDetailLine);
                    tableDetail.AddCell(cellDetailLine);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("NO ROLL", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("COLOUR", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("ROLL", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("Quantity In Meters", normal_font, 0.75f));
                    //cellBorderBottomRight.Rowspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("WEIGHT IN KGS", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 2;
                    cellBorderBottomRight.Rowspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);


                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("NET", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 1;
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);

                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("GROSS", normal_font, 0.75f));
                    cellBorderBottomRight.Rowspan = 1;
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);

                    //cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("CTNS", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 1;
                    //cellBorderBottomRight.Rowspan = 2;
                    //tableDetail.AddCell(cellBorderBottomRight);
                   
                    var itemC = viewModel.ShippingProductionOrders.Select(x => new OutputShippingProductionOrderViewModel
                    {
                        No = 0,
                        Color = x.Color,
                        QtyPacking = x.QtyPacking,
                        Qty = x.Qty,
                        PackingListNet = x.PackingListNet,
                        PackingListGross = x.PackingListGross

                    }).OrderBy( x => x.Color);
                    var newDetails = new List<OutputShippingProductionOrderViewModel>();
                    int no = 1;
                    foreach (var i in itemC)
                    {
                        i.No = no++;
                        newDetails.Add(i);
                    }


                    foreach (var detail in newDetails.Where( x => x.Color == itemB.Color).OrderBy( s=> s.Color))
                    {
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.No}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Color}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.QtyPacking}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.Qty}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.PackingListNet}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                        cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{detail.PackingListGross}", normal_font, 0.75f));
                        tableDetail.AddCell(cellBorderBottomRight);
                    }
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk("SUBTOTAL", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemB.PackingQty}", normal_font, 0.75f));
                    cellBorderBottomRight.Colspan = 1;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemB.Balance}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemB.PackingListNet}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);
                    cellBorderBottomRight.Phrase = new Phrase(GetScalledChunk($"{itemB.PackingListGross}", normal_font, 0.75f));
                    //cellBorderBottomRight.Colspan = 2;
                    tableDetail.AddCell(cellBorderBottomRight);

                    new PdfPCell(tableDetail);

                    tableDetail.ExtendLastRow = false;
                    tableDetail.WidthPercentage = 95f;
                    tableDetail.SpacingAfter = 10f;

                    document.Add(tableDetail);
                }

                #region GrandTotal
                var grandTotalPackingResult = (newItems.Sum(x => x.PackingQty)).ToString();
                var grandTotalBalanceResult = (newItems.Sum(x => x.Balance)).ToString();
                var grandTotalNetResult = (newItems.Sum(x => x.PackingListNet)).ToString();
                var grandTotalGrossResult = (newItems.Sum(x => x.PackingListGross)).ToString();
                PdfPTable tableGrandTotal = new PdfPTable(6);
                tableGrandTotal.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                PdfPCell cellHeaderLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 6, Padding = 0.5f, Phrase = new Phrase("") };

                tableGrandTotal.AddCell(cellHeaderLine);
                tableGrandTotal.AddCell(cellHeaderLine);
                tableGrandTotal.AddCell(new PdfPCell()
                {
                    Colspan = 2,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    Phrase = new Phrase("GRAND TOTAL ...................", normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    Colspan = 1,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalPackingResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalBalanceResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalNetResult, normal_font)
                });

                tableGrandTotal.AddCell(new PdfPCell()
                {
                    //Colspan = ,
                    Border = Rectangle.BOTTOM_BORDER,
                    Padding = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Phrase = new Phrase(grandTotalGrossResult, normal_font)
                });
                tableGrandTotal.AddCell(cellHeaderLine);
                new PdfPCell(tableGrandTotal);
                tableGrandTotal.ExtendLastRow = false;
                tableGrandTotal.WidthPercentage = 95f;
                tableGrandTotal.SpacingAfter = 5f;
                document.Add(tableGrandTotal);
                #endregion

            }
            #region Remark


            var Buyer = viewModel.ShippingProductionOrders.First().DestinationBuyerName;
            PdfPTable tableMeasurement = new PdfPTable(3);
            tableMeasurement.SetWidths(new float[] { 2f, 0.2f, 12f });
            PdfPCell cellMeasurement = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPCell cellRemarkLine = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, Colspan = 6, Padding = 0.5f, Phrase = new Phrase("") };


            if (viewModel.ShippingProductionOrders.First().DeliveryOrderSalesType != "Lokal")
            {
                cellMeasurement.Phrase = new Phrase("LC NUMBER", normal_font);
                tableMeasurement.AddCell(cellMeasurement);
                cellMeasurement.Phrase = new Phrase(":", normal_font);
                tableMeasurement.AddCell(cellMeasurement);
                cellMeasurement.Phrase = new Phrase(" " + viewModel.PackingListLCNumber, normal_font);
                tableMeasurement.AddCell(cellMeasurement);

                cellMeasurement.Phrase = new Phrase("Issued By", normal_font);
                tableMeasurement.AddCell(cellMeasurement);
                cellMeasurement.Phrase = new Phrase(":", normal_font);
                tableMeasurement.AddCell(cellMeasurement);
                cellMeasurement.Phrase = new Phrase(" " + viewModel.PackingListIssuedBy, normal_font);
                tableMeasurement.AddCell(cellMeasurement);
            }

            

            cellMeasurement.Phrase = new Phrase("Shippingmark", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(":", normal_font);
            tableMeasurement.AddCell(cellMeasurement);
            cellMeasurement.Phrase = new Phrase(viewModel.PackingListRemark, normal_font);
            tableMeasurement.AddCell(cellMeasurement);

            //cellMeasurement.Phrase = new Phrase("", normal_font);
            //tableMeasurement.AddCell(cellMeasurement);
            //cellMeasurement.Phrase = new Phrase("", normal_font);
            //tableMeasurement.AddCell(cellMeasurement);
            //cellMeasurement.Phrase = new Phrase(viewModel.PackingListRemark, normal_font);
            //tableMeasurement.AddCell(cellMeasurement);

            tableMeasurement.AddCell(cellRemarkLine);
            tableMeasurement.AddCell(cellRemarkLine);


            new PdfPCell(tableMeasurement);
            tableMeasurement.ExtendLastRow = false;
            tableMeasurement.SpacingAfter = 5f;
            document.Add(tableMeasurement);

            #endregion

            #region sign
            PdfPTable tableSign = new PdfPTable(3);
            tableSign.WidthPercentage = 100;
            tableSign.SetWidths(new float[] { 1f, 1f, 1f });

            PdfPCell cellBodySignNoBorder = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            if (viewModel.ShippingProductionOrders.First().DeliveryOrderSalesType == "Lokal")
            {
                cellBodySignNoBorder.Phrase = new Phrase("Penerima", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("Marketing", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("Kepala Gudang", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);

                cellBodySignNoBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("\n\n\n\n", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);

                cellBodySignNoBorder.Phrase = new Phrase("( ........................... )", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("( ........................... )", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase($"( Adi Chriscahyo )", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);

                //cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                //tableSign.AddCell(cellBodySignNoBorder);
                //cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                //tableSign.AddCell(cellBodySignNoBorder);
                //cellBodySignNoBorder.Phrase = new Phrase("AUTHORIZED SIGNATURE", normal_font);
                //tableSign.AddCell(cellBodySignNoBorder);

            }
            else {
                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("\n\n\n\n\n\n\n", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);


                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase($"{viewModel.PackingListAuthorized}", normal_font_underlined);
                tableSign.AddCell(cellBodySignNoBorder);

                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);
                cellBodySignNoBorder.Phrase = new Phrase("AUTHORIZED SIGNATURE", normal_font);
                tableSign.AddCell(cellBodySignNoBorder);

            }

            

            document.Add(tableSign);
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
    }
    class OutputShippingPackingListPDFTemplatePageEvent : PdfPageEventHelper
    {
        private IIdentityProvider identityProvider;
        private OutputShippingViewModel viewModel;

        public OutputShippingPackingListPDFTemplatePageEvent(IIdentityProvider identityProvider, OutputShippingViewModel viewModel)
        {
            this.identityProvider = identityProvider;
            this.viewModel = viewModel;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin, marginTop = document.TopMargin, marginRight = document.RightMargin, marginBottom = document.BottomMargin;

            //int maxSizesCount = viewModel.Items.Max(i => i.Details.Max(d => d.Sizes.GroupBy(g => g.Size.Id).Count()));

            //if (maxSizesCount > 11)
            //{
            //    cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 6);

            //    #region LEFT

            //    var logoY = height - marginTop + 65;

            //    byte[] imageByteDL = Convert.FromBase64String(Base64ImageStrings.LOGO_DANLIRIS_211_200_BW);
            //    Image imageDL = Image.GetInstance(imageByteDL);
            //    imageDL.ScaleAbsolute(60f, 60f);
            //    var newColor = System.Drawing.Color.Red;
            //    imageDL.SetAbsolutePosition(marginLeft, logoY);
            //    cb.AddImage(imageDL, inlineImage: true);

            //    #endregion

            //    #region CENTER

            //    var headOfficeX = marginLeft + 75;
            //    var headOfficeY = height - marginTop + 105;

            //    byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
            //    Image image = Image.GetInstance(imageByte);
            //    if (image.Width > 160)
            //    {
            //        float percentage = 0.0f;
            //        percentage = 160 / image.Width;
            //        image.ScalePercent(percentage * 100);
            //    }
            //    image.SetAbsolutePosition(headOfficeX, headOfficeY);
            //    cb.AddImage(image, inlineImage: true);

            //    string[] headOffices = {
            //        "Head Office : Kelurahan Banaran, Kecamatan Grogol,",
            //        "Sukoharjo - Indonesia",
            //        "PO BOX 166 Solo 57100",
            //        "Telp. (62 271) 740888, 714400 (HUNTING)",
            //        "Fax. (62 271) 735222, 740777",
            //        "Website : www.danliris.com",
            //    };
            //    for (int i = 0; i < headOffices.Length; i++)
            //    {
            //        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, headOffices[i], headOfficeX, headOfficeY + 10 - image.ScaledHeight - (i * 6), 0);
            //    }

            //    #endregion

            //    #region RIGHT

            //    byte[] imageByteIso = Convert.FromBase64String(Base64ImageStrings.ISO);
            //    Image imageIso = Image.GetInstance(imageByteIso);
            //    if (imageIso.Width > 80)
            //    {
            //        float percentage = 0.0f;
            //        percentage = 80 / imageIso.Width;
            //        imageIso.ScalePercent(percentage * 100);
            //    }
            //    imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 120);
            //    cb.AddImage(imageIso, inlineImage: true);
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight, height - imageIso.ScaledHeight - marginTop + 120 - 5, 0);

            //    #endregion

            //    #region LINE

            //    cb.MoveTo(marginLeft, height - marginTop + 50);
            //    cb.LineTo(width - marginRight, height - marginTop + 50);
            //    cb.Stroke();

            //    #endregion

            //    cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 16);

            //    #region TITLE

            //    var titleY = height - marginTop + 40;
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "PACKING LIST", width / 2, titleY, 0);

            //    #endregion
            //}

            var newItems = new OutputShippingProductionOrderViewModel();

            foreach ( var item in viewModel.ShippingProductionOrders) 
            {
                
                newItems.DeliveryNote = item.DeliveryNote;
            }

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 16);
            var titleY = height - marginTop + 40;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "PACKING LIST", width / 2, titleY, 0);

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, viewModel.PackingListNo, width / 2, titleY - 12, 0);


            #region REF

            var refY = height - marginTop + 25;
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "FM-PJ-00-03-011", width - marginRight, refY, 0);

            #endregion

            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED), 8);

            #region INFO

            var infoY = height - marginTop + 10;


            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Invoice No. : " + newItems.DeliveryNote, marginLeft, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Date : " + viewModel.Date.ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("MMM dd, yyyy."), width / 2, infoY, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page : " + writer.PageNumber, width - marginRight, infoY, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop + 5);
            cb.LineTo(width - marginRight, height - marginTop + 5);
            cb.Stroke();

            #endregion

            #region PRINTED

            var printY = marginBottom - 40;
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Printed on : " + DateTimeOffset.Now.ToOffset(new TimeSpan(identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy H:mm:ss zzz"), width - marginRight, printY, 0);

            #endregion

            #region SIGNATURE

            //var signX = width - 140;
            //var signY = marginBottom - 80;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "( MRS. ADRIYANA DAMAYANTI )", signX, signY, 0);
            //cb.MoveTo(signX - 55, signY - 2);
            //cb.LineTo(signX + 55, signY - 2);
            //cb.Stroke();
            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "AUTHORIZED SIGNATURE", signX, signY - 10, 0);

            #endregion

            cb.EndText();
        }

        
    }
}
