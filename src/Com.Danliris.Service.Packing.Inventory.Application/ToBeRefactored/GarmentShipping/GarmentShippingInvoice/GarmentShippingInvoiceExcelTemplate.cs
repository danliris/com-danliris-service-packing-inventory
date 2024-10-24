using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice.CostCalculationGarmentVM;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceExcelTemplate
    {

        public IIdentityProvider _identityProvider;

        public GarmentShippingInvoiceExcelTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GenerateExcelTemplate(GarmentShippingInvoiceViewModel viewModel, Buyer buyer, BankAccount bank, GarmentPackingListViewModel pl, int timeoffset, List<CostCalculationGarmentViewModel> ccg)
        {
           
            DataTable result = new DataTable();

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");

            sheet.Row(2).Height = 20;
            sheet.Cells["A2"].Value = "INVOICE NO. : " + viewModel.InvoiceNo;
            sheet.Cells[$"A2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["A2:B2"].Merge = true;
            
            sheet.Column(1).Width = 15;
            sheet.Column(2).Width = 15;
            sheet.Column(3).Width = 15;
            sheet.Column(4).Width = 15;
            sheet.Column(5).Width = 10;
            sheet.Column(6).Width = 10;
            sheet.Column(7).Width = 15;
            sheet.Column(8).Width = 15;

            sheet.Cells[$"A2:H2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells["D2"].Value = "DATE : " + viewModel.InvoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("MMM dd, yyyy.").ToUpper();
            sheet.Cells[$"D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D2:E2"].Merge = true;
            //
            sheet.Row(3).Height = 18;
            sheet.Cells[$"A3"].Value = "SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF";
            sheet.Cells[$"A3"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Cells[$"A3:C3"].Merge = true;
            
            sheet.Cells[$"D3"].Value = "CO NO.";
            sheet.Cells[$"D3"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D3:E3"].Merge = true;
           
            sheet.Cells[$"F3"].Value = ": " + viewModel.CO;
            sheet.Cells[$"F3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H3"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F3:H3"].Merge = true;
            //
            sheet.Row(4).Height = 18;
            sheet.Cells[$"A4"].Value = " ";
            sheet.Cells[$"A4"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A4:C4"].Merge = true;
            sheet.Cells[$"A4:C4"].Merge = true;

            sheet.Cells[$"D4"].Value = "CONFIRMATION OF ORDER NO";
            sheet.Cells[$"D4"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D4:E4"].Merge = true;

            sheet.Cells[$"F4"].Value = ": " + viewModel.ConfirmationOfOrderNo;
            sheet.Cells[$"F4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"F4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H4"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F4:H4"].Merge = true;           
            //

            sheet.Row(5).Height = 18;
            sheet.Cells[$"A5"].Value = "MESSRS ";
            sheet.Cells[$"A5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            if (viewModel.InvoiceNo.Substring(0, 2) == "SM" || viewModel.InvoiceNo.Substring(0, 2) == "DS" || viewModel.InvoiceNo.Substring(0, 3) == "DLR")
            {
                sheet.Cells[$"B5"].Value = ": " + viewModel.Consignee;
                sheet.Cells[$"B5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            else
            {
                sheet.Cells[$"B5"].Value = ": " + viewModel.BuyerAgent.Name;
                sheet.Cells[$"B5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            sheet.Cells[$"B5:C5"].Merge = true;

            sheet.Cells[$"D5"].Value = "SHIPPING PER";
            sheet.Cells[$"D5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D5:E5"].Merge = true;

            sheet.Cells[$"F5"].Value = ": " + viewModel.ShippingPer;
            sheet.Cells[$"F5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"F5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F5:H5"].Merge = true;           
            //          
            sheet.Row(6).Height = 45;
            sheet.Cells[$"A6"].Value = " ";
            sheet.Cells[$"A6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            sheet.Cells[$"B6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B6"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"B6"].Value = ": " + viewModel.ConsigneeAddress;
            sheet.Cells[$"B6:C6"].Merge = true;

            sheet.Cells[$"D6"].Value = "SAILING ON OR ABOUT";
            sheet.Cells[$"D6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D6"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"D6:E6"].Merge = true;

            sheet.Cells[$"F6"].Value = ": " + viewModel.SailingDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"));
            sheet.Cells[$"F6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"F6"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"H6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F6:H6"].Merge = true;           
            //
            sheet.Row(7).Height = 18;
            sheet.Cells[$"A7"].Value = " ";
            sheet.Cells[$"A7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A7:C7"].Merge = true;

            sheet.Cells[$"D7"].Value = "FROM";
            sheet.Cells[$"D7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D7:E7"].Merge = true;

            sheet.Cells[$"F7"].Value = ": " + viewModel.From;
            sheet.Cells[$"F7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"F7"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F7:H7"].Merge = true;
            //
            sheet.Row(8).Height = 18;
            sheet.Cells[$"A8"].Value = "DELIVERED TO";
            sheet.Cells[$"A8"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B8"].Value = ": " + viewModel.DeliverTo;
            sheet.Cells[$"B8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B8:C8"].Merge = true;

            sheet.Cells[$"D8"].Value = "TO";
            sheet.Cells[$"D8"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"D8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"D8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"D8:E8"].Merge = true;

            sheet.Cells[$"F8"].Value = ": " + viewModel.To;
            sheet.Cells[$"F8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"F8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H8"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F8:H8"].Merge = true;
           
            sheet.Cells[$"A8:H8"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            var index = 0;
            //
            if (pl.PaymentTerm == "LC")
            {
                sheet.Row(9).Height = 18;
                sheet.Cells[$"A9"].Value = "LETTER OF CREDIT NUMBER";
                sheet.Cells[$"B9"].Value = ": " + viewModel.LCNo;
                sheet.Cells[$"B9:C9"].Merge = true;
                sheet.Cells[$"A9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                sheet.Row(10).Height = 18;
                sheet.Cells[$"A10"].Value = "LC DATE";
                sheet.Cells[$"B10"].Value = ": " + pl.LCDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"));
                sheet.Cells[$"B10:C10"].Merge = true;
                sheet.Cells[$"A10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                sheet.Row(11).Height = 18;
                sheet.Cells[$"A11"].Value = "ISSUED BY";
                sheet.Cells[$"B11"].Value = ": " + viewModel.IssuedBy;
                sheet.Cells[$"B11:C11"].Merge = true;
                sheet.Cells[$"A11:H11"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A11"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A11"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B11"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B11"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                
                index = 12;
            }
            else
            {
                sheet.Row(9).Height = 20;
                sheet.Cells[$"A9"].Value = "PAYMENT TERM";
                sheet.Cells[$"B9"].Value = ": TT PAYMENT";
                sheet.Cells[$"B9:C9"].Merge = true;
                sheet.Cells[$"A9:H9"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                index = 10;
            }

            //
            
            double totalQtys = 0;
            decimal totalAmnts = 0;

            var arrayGrandTotal = new Dictionary<String, double>();
           
            var afterTotalIndex = 0;

            Dictionary<string, double> total = new Dictionary<string, double>();
            var afterIndex = index + 1;
            var DescIndex = afterIndex + 1;
            var MarkIndex = DescIndex + 2;
            var valueIndex = MarkIndex + 1;

            sheet.Cells[$"A{index}"].Value = "DESCRIPTION";
            sheet.Cells[$"A{index}:D{index}"].Merge = true;
            sheet.Cells[$"A{afterIndex}:D{afterIndex}"].Merge = true;            
            sheet.Cells[$"A{index}:D{afterIndex}"].Merge = true;
            sheet.Cells[$"A{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"E{index}"].Value = "QUANTITY";
            sheet.Cells[$"E{index}:F{index}"].Merge = true;
            sheet.Cells[$"E{afterIndex}:F{afterIndex}"].Merge = true;
            sheet.Cells[$"E{index}:F{afterIndex}"].Merge = true;
            sheet.Cells[$"E{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"E{index}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{index}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{afterIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;


            sheet.Cells[$"G{index}"].Value = "UNIT PRICE";
            sheet.Cells[$"G{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"G{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{index}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"G{afterIndex}"].Value = "FOB IN USD";
            sheet.Cells[$"G{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"G{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{afterIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"H{index}"].Value = "TOTAL PRICE";
            sheet.Cells[$"H{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"H{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{index}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"H{afterIndex}"].Value = "FOB IN USD";
            sheet.Cells[$"H{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"H{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{afterIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"A{afterIndex}:H{afterIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            
            //                      
            if (viewModel.Description != null)
            {
                sheet.Cells[$"A{afterIndex + 1}"].Value = "";
                sheet.Cells[$"A{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"D{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{afterIndex + 1}:D{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"E{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
               
                sheet.Cells[$"A{++DescIndex}"].Value = viewModel.Description;
                sheet.Cells[$"A{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"D{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{DescIndex}:D{DescIndex}"].Merge = true;
                sheet.Row(DescIndex).Height = 125;
                sheet.Cells[$"E{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"A{DescIndex + 1}"].Value = "";
                sheet.Cells[$"A{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{DescIndex + 1}:D{MarkIndex}"].Merge = true;
                sheet.Cells[$"E{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }
            else
            {
                sheet.Cells[$"A{afterIndex + 1}"].Value = "";
                sheet.Cells[$"A{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"D{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{afterIndex + 1}:D{afterIndex + 1}"].Merge = true;
                sheet.Cells[$"E{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }            
            //
            if (viewModel.Remark != null)
            {
                sheet.Row(MarkIndex).Height = 110;
                sheet.Cells[$"A{++MarkIndex}"].Value = viewModel.Remark;
                sheet.Cells[$"A{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"D{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{MarkIndex}:D{MarkIndex}"].Merge = true;
                sheet.Cells[$"E{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"A{MarkIndex + 1}"].Value = "";
                sheet.Cells[$"A{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{MarkIndex + 1}:D{MarkIndex + 1}"].Merge = true;
                sheet.Cells[$"E{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }
            else 
            {
                sheet.Cells[$"A{MarkIndex}"].Value = "";
                sheet.Cells[$"A{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A{MarkIndex}:D{MarkIndex}"].Merge = true;
                sheet.Cells[$"E{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"E{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium; 
            }

            for (var a = 0; a < pl.Items.Count(); a++)
            {
                //Get FOB Price From CCG
                var matchCC = ccg.FirstOrDefault(x => x.RO_Number == pl.Items[a].RONo);

                double fob = 0;
                var isFabricCM = false;
                double priceFOB = 0;
                if (matchCC != null)
                {
                    foreach (var ccgItem in matchCC.CostCalculationGarment_Materials)
                    {

                        if (ccgItem.isFabricCM)
                        {
                            isFabricCM = true;
                            fob += Convert.ToDouble(ccgItem.CM_Price * 1.05 / matchCC.Rate.Value);
                        }

                    }

                    if (pl.Items[a].PriceFOB == 0 && pl.Items[a].PriceCMT == 0)
                    {
                        if (isFabricCM)
                        {
                            pl.Items[a].PriceFOB = Convert.ToDouble(matchCC.ConfirmPrice + priceFOB);

                        }
                        else
                        {
                            pl.Items[a].PriceFOB = Convert.ToDouble(matchCC.ConfirmPrice);
                        }
                    }
                }
            }

            //foreach (var item in viewModel.Items.OrderBy(o => o.ComodityDesc))
            foreach (var item in viewModel.Items.OrderBy(o => o.PackingListItemId))
            {
                sheet.Cells[$"A{valueIndex}"].Value = item.ComodityDesc.TrimEnd();
                sheet.Cells[$"A{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
             
                sheet.Cells[$"B{valueIndex}"].Value = item.Desc2 == null ? "" : item.Desc2.TrimEnd();
                sheet.Cells[$"B{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"C{valueIndex}"].Value = item.Desc3 == null ? "" : item.Desc3.TrimEnd();
                sheet.Cells[$"C{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"D{valueIndex}"].Value = item.Desc4 == null ? "" : item.Desc4.TrimEnd();
                sheet.Cells[$"D{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"D{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                if (item.Uom.Unit.Substring(0, 2) == "MT" || item.Uom.Unit.Substring(0, 2) == "YA" || item.Uom.Unit.Substring(0, 2) == "YD")
                {
                    //sheet.Cells[$"E{valueIndex}"].Value = string.Format("{0:n2}", item.Quantity);
                    sheet.Cells[$"E{valueIndex}"].Value = Math.Round(item.Quantity, 2);
                    sheet.Cells[$"E{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"E{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                }
                else
                {
                    //sheet.Cells[$"E{valueIndex}"].Value = string.Format("{0:n0}", item.Quantity);
                    sheet.Cells[$"E{valueIndex}"].Value = Math.Round(item.Quantity, 0);
                    sheet.Cells[$"E{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    sheet.Cells[$"E{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                }

                sheet.Cells[$"F{valueIndex}"].Value = item.Uom.Unit.TrimEnd();
                sheet.Cells[$"F{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"F{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                var matchPL = pl.Items.FirstOrDefault(x => x.RONo == item.RONo && x.OrderNo == item.ComodityDesc);
                //sheet.Cells[$"G{valueIndex}"].Value = string.Format("{0:n4}", matchPL.PriceFOB);
                sheet.Cells[$"G{valueIndex}"].Value = Math.Round(matchPL.PriceFOB, 4);
                sheet.Cells[$"G{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"G{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                var amount = matchPL.PriceFOB * item.Quantity;
                //sheet.Cells[$"H{valueIndex}"].Value = string.Format("{0:n2}", amount);
                sheet.Cells[$"H{valueIndex}"].Value = Math.Round(item.Amount, 2);
                sheet.Cells[$"H{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"H{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                totalQtys += item.Quantity;
                totalAmnts += Convert.ToDecimal(amount);

                if (total.ContainsKey(item.Uom.Unit))
                {
                    total[item.Uom.Unit] += item.Quantity;
                }
                else
                {
                    total.Add(item.Uom.Unit, item.Quantity);
                }

                valueIndex++;
                afterTotalIndex = valueIndex;
            }
            //

            sheet.Cells[$"A{afterTotalIndex}"].Value = "";
            sheet.Cells[$"A{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{afterTotalIndex}:D{afterTotalIndex}"].Merge = true;
            sheet.Cells[$"E{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"A{afterTotalIndex + 1}"].Value = "";
            sheet.Cells[$"A{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{afterTotalIndex + 1}:D{afterTotalIndex + 1}"].Merge = true;
            sheet.Cells[$"E{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"G{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            //
            var key1 = total.Select(x => String.Format("{0}", x.Key));
            var result2 = String.Join("\n", key1);

            var val1 = total.Select(x => String.Format("{0:n2}", x.Value));
            var result1 = String.Join("\n", val1);

            var grandTotalIndex = afterTotalIndex + 2;
            //
            sheet.Row(grandTotalIndex).Height = 25;
            sheet.Cells[$"A{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A{grandTotalIndex}:D{grandTotalIndex}"].Merge = true;
            sheet.Cells[$"A{grandTotalIndex}"].Value = "T O T A L   :";
            sheet.Cells[$"A{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A{grandTotalIndex}:H{grandTotalIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            
            sheet.Cells[$"E{grandTotalIndex}"].Value = string.Format("{0:n0}", result1);
            sheet.Cells[$"E{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            sheet.Cells[$"E{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"F{grandTotalIndex}"].Value = result2;          
            sheet.Cells[$"F{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"F{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            //sheet.Cells[$"H{grandTotalIndex}"].Value = string.Format("{0:n2}", totalAmnts);
            sheet.Cells[$"H{grandTotalIndex}"].Value = Math.Round(totalAmnts, 2);
            sheet.Cells[$"H{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            sheet.Cells[$"H{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"A{grandTotalIndex}:H{grandTotalIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

            //
            var spellingWordIndex = grandTotalIndex + 2;

            var spellingAdjustIndex = grandTotalIndex + 2;

            string amountToText = "";
            var TtltIndex = 0;
            var PaymentIndex = 0;

            //
            if (viewModel.GarmentShippingInvoiceAdjustments.Count > 0)
            {
                sheet.Row(spellingAdjustIndex).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex}"].Value = "TOTAL AMOUNT FOB";
                //sheet.Cells[$"B{spellingAdjustIndex}"].Value = "  : USD "+ string.Format("{0:n2}", totalAmnts);
                sheet.Cells[$"D{spellingAdjustIndex}"].Value = "  : USD " + Math.Round(totalAmnts, 2);
                sheet.Cells[$"D{spellingAdjustIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                decimal totalPaid = totalAmnts;
                var AdjustIndex = spellingAdjustIndex + 1;

                foreach (var adj in viewModel.GarmentShippingInvoiceAdjustments)
                {
                    sheet.Row(AdjustIndex).Height = 18;
                    totalPaid += adj.AdjustmentValue;
                    sheet.Cells[$"A{AdjustIndex}"].Value = adj.AdjustmentDescription;
                    sheet.Cells[$"A{AdjustIndex}:B{AdjustIndex}"].Merge = true;
                    //sheet.Cells[$"C{AdjustIndex}"].Value = "  : USD " + string.Format("{0:n2}", adj.AdjustmentValue);
                    sheet.Cells[$"C{AdjustIndex}"].Value = "  : USD " + Math.Round(adj.AdjustmentValue, 2);
                    sheet.Cells[$"D{AdjustIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    AdjustIndex++;
                    TtltIndex = AdjustIndex;
                }

                sheet.Cells[$"A{TtltIndex}:D{TtltIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

                sheet.Row(TtltIndex).Height = 18;
                sheet.Cells[$"A{TtltIndex}"].Value = "TOTAL AMOUNT TO BE PAID";
                sheet.Cells[$"A{TtltIndex}:B{TtltIndex}"].Merge = true;
                //sheet.Cells[$"C{TtltIndex}"].Value = "  : USD " + string.Format("{0:n2}", totalPaid);
                sheet.Cells[$"C{TtltIndex}"].Value = "  : USD " + Math.Round(totalPaid, 2);
                sheet.Cells[$"D{TtltIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                if (totalPaid < 0)
                {
                    totalPaid = totalPaid * -1;
                    amountToText = "MINUS " + CurrencyToText.ToWords(totalPaid);
                }
                else
                {
                    amountToText = CurrencyToText.ToWords(totalPaid);
                }


                sheet.Row(TtltIndex + 1).Height = 18;
                sheet.Cells[$"A{TtltIndex + 1}:H{TtltIndex + 1}"].Merge = true;
                sheet.Cells[$"A{TtltIndex + 1}"].Value = "SAY : US DOLLARS " + amountToText.ToUpper() + " ONLY ///";

                PaymentIndex = TtltIndex + 3;
                //
                sheet.Row(PaymentIndex).Height = 20;
                sheet.Cells[$"A{PaymentIndex}:H{PaymentIndex}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex}"].Value = "PLEASE TT THE ABOVE PAYMENT TO OUR CORRESPONDENCE BANK AS FOLLOW :";
                sheet.Cells[$"A{PaymentIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{PaymentIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                sheet.Cells[$"A{PaymentIndex + 1}:H{PaymentIndex + 1}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 1}"].Value = bank.bankName;
                sheet.Cells[$"A{PaymentIndex + 2}:H{PaymentIndex + 2}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 2}"].Value = bank.bankAddress;
                sheet.Cells[$"A{PaymentIndex + 3}:H{PaymentIndex + 3}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 3}"].Value = "ACC NO. " + bank.AccountNumber + " " + bank.Currency.Code;
                sheet.Cells[$"A{PaymentIndex + 4}:H{PaymentIndex + 4}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 4}"].Value = "A/N. PT. DAN LIRIS";
                sheet.Cells[$"A{PaymentIndex + 5}:H{PaymentIndex + 5}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 5}"].Value = "SWIFT CODE : " + bank.swiftCode;
                sheet.Cells[$"A{PaymentIndex + 6}:H{PaymentIndex + 6}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex + 6}"].Value = "PURPOSE CODE: 1011";
                //
            }
            else
            {
                if (totalAmnts < 0)
                {
                    totalAmnts = totalAmnts * -1;
                    amountToText = "MINUS " + CurrencyToText.ToWords(totalAmnts);
                }
                else
                {
                    amountToText = CurrencyToText.ToWords(totalAmnts);
                }

                sheet.Row(spellingWordIndex).Height = 18;
                sheet.Cells[$"A{spellingWordIndex}"].Value = "SAY : US DOLLARS " + amountToText.ToUpper() + " ONLY ///";
                sheet.Cells[$"A{spellingWordIndex}:H{spellingWordIndex}"].Merge = true;
                PaymentIndex = spellingWordIndex + 2;
                //
                sheet.Row(PaymentIndex).Height = 20;
                sheet.Cells[$"A{PaymentIndex}"].Value = "PLEASE TT THE ABOVE PAYMENT TO OUR CORRESPONDENCE BANK AS FOLLOW :";
                sheet.Cells[$"A{PaymentIndex}:H{PaymentIndex}"].Merge = true;
                sheet.Cells[$"A{PaymentIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{PaymentIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                sheet.Cells[$"A{PaymentIndex + 1}"].Value = bank.bankName;
                sheet.Cells[$"A{PaymentIndex + 1}:H{PaymentIndex + 1}"].Merge = true;

                sheet.Cells[$"A{PaymentIndex + 2}"].Value = bank.bankAddress;
                sheet.Cells[$"A{PaymentIndex + 2}:H{PaymentIndex + 2}"].Merge = true;

                sheet.Cells[$"A{PaymentIndex + 3}"].Value = "ACC NO. " + bank.AccountNumber + " " + bank.Currency.Code;
                sheet.Cells[$"A{PaymentIndex + 3}:H{PaymentIndex + 3}"].Merge = true;

                sheet.Cells[$"A{PaymentIndex + 4}"].Value = "A/N. PT. DAN LIRIS";
                sheet.Cells[$"A{PaymentIndex + 4}:H{PaymentIndex + 4}"].Merge = true;

                sheet.Cells[$"A{PaymentIndex + 5}"].Value = "SWIFT CODE : " + bank.swiftCode;
                sheet.Cells[$"A{PaymentIndex + 5}:H{PaymentIndex + 5}"].Merge = true;

                sheet.Cells[$"A{PaymentIndex + 6}"].Value = "PURPOSE CODE: 1011";
                sheet.Cells[$"A{PaymentIndex + 6}:H{PaymentIndex + 6}"].Merge = true;

                //
            }
            //
            #region Mark
            var shippingMarkIndex = PaymentIndex + 8;
            var sideMarkIndex = PaymentIndex + 8;

            sheet.Cells[$"A{shippingMarkIndex}"].Value = "SHIPPING MARKS";
            sheet.Cells[$"A{shippingMarkIndex}:B{shippingMarkIndex}"].Merge = true;
            sheet.Cells[$"A{++shippingMarkIndex}"].Value = pl.ShippingMark == null ? "" : pl.ShippingMark;

            sheet.Cells[$"E{sideMarkIndex}"].Value = "SIDE MARKS";
            sheet.Cells[$"D{sideMarkIndex}:G{sideMarkIndex}"].Merge = true;
            sheet.Cells[$"D{++sideMarkIndex}"].Value = pl.SideMark == null ? "" : pl.SideMark;

            byte[] shippingMarkImage;
            if (!String.IsNullOrEmpty(pl.ShippingMarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(pl.ShippingMarkImageFile)))
                {
                    shippingMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.ShippingMarkImageFile));
                    Image shipMarkImage = byteArrayToImage(shippingMarkImage);
                    var imageShippingMarkIndex = shippingMarkIndex + 1;

                    ExcelPicture excelPictureShipMarkImage = sheet.Drawings.AddPicture("ShippingMarkImage", shipMarkImage);
                    excelPictureShipMarkImage.From.Column = 0;
                    excelPictureShipMarkImage.From.Row = imageShippingMarkIndex;
                    excelPictureShipMarkImage.SetSize(200, 200);
                }
            }

            byte[] sideMarkImage;
            if (!String.IsNullOrEmpty(pl.SideMarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(pl.SideMarkImageFile)))
                {
                    sideMarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.SideMarkImageFile));
                    Image _sideMarkImage = byteArrayToImage(sideMarkImage);
                    var sideMarkImageIndex = sideMarkIndex + 1;

                    ExcelPicture excelPictureSideMarkImage = sheet.Drawings.AddPicture("SideMarkImage", _sideMarkImage);
                    excelPictureSideMarkImage.From.Column = 5;
                    excelPictureSideMarkImage.From.Row = sideMarkImageIndex;
                    excelPictureSideMarkImage.SetSize(200, 200);
                }
            }

            #endregion

            #region Measurement

            var grossWeightIndex = shippingMarkIndex + 15;
            var netWeightIndex = grossWeightIndex + 1;
            var netNetWeightIndex = netWeightIndex + 1;
            var measurementIndex = netNetWeightIndex + 1;

            sheet.Row(grossWeightIndex).Height = 18;
            sheet.Cells[$"A{grossWeightIndex}"].Value = "GROSS WEIGHT";
            sheet.Cells[$"B{grossWeightIndex}"].Value = ": " + string.Format("{0:N2}", pl.GrossWeight)  + " KGS";
            sheet.Cells[$"A{grossWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A{grossWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B{grossWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B{grossWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Row(netWeightIndex).Height = 18;
            sheet.Cells[$"A{netWeightIndex}"].Value = "NET WEIGHT";
            sheet.Cells[$"B{netWeightIndex}"].Value = ": " + string.Format("{0:N2}", pl.NettWeight) + " KGS";
            sheet.Cells[$"A{netWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A{netWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B{netWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B{netWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Row(netNetWeightIndex).Height = 18;
            sheet.Cells[$"A{netNetWeightIndex}"].Value = "NET NET WEIGHT";
            sheet.Cells[$"B{netNetWeightIndex}"].Value = ": " + string.Format("{0:N2}", pl.NetNetWeight) + " KGS";
            sheet.Cells[$"A{netNetWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A{netNetWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B{netNetWeightIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B{netNetWeightIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Row(measurementIndex).Height = 18;
            sheet.Cells[$"A{measurementIndex}"].Value = "MEASUREMENT";
        
            decimal totalCbm = 0;
            foreach (var measurement in pl.Measurements)
            {
                sheet.Cells[$"B{measurementIndex}"].Value = string.Format("{0:N2}", measurement.Length) + " CM X ";
                sheet.Cells[$"C{measurementIndex}"].Value = string.Format("{0:N2}", measurement.Width) + " CM X ";
                sheet.Cells[$"D{measurementIndex}"].Value = string.Format("{0:N2}", measurement.Height) + " CM X  ";
                //sheet.Cells[$"E{measurementIndex}:G{measurementIndex}"].Merge = true;
                sheet.Cells[$"E{measurementIndex}"].Value = measurement.CartonsQuantity + " CTNS  =";
                //sheet.Cells[$"H{measurementIndex}:I{measurementIndex}"].Merge = true;
                //sheet.Cells[$"J{measurementIndex}"].Value = "=";

                var cbm = (decimal)measurement.Length * (decimal)measurement.Width * (decimal)measurement.Height * (decimal)measurement.CartonsQuantity / 1000000;
                totalCbm += cbm;
                sheet.Cells[$"F{measurementIndex}"].Value = string.Format("{0:N2} CBM", cbm);
                //sheet.Cells[$"K{measurementIndex}:M{measurementIndex}"].Merge = true;

                measurementIndex++;
            }
            var totalMeasurementIndex = measurementIndex;
            sheet.Row(totalMeasurementIndex).Height = 18;
            sheet.Cells[$"B{totalMeasurementIndex}:F{totalMeasurementIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[$"D{totalMeasurementIndex}"].Value = "TOTAL";
            //sheet.Cells[$"D{totalMeasurementIndex}:G{totalMeasurementIndex}"].Merge = true;
            sheet.Cells[$"E{totalMeasurementIndex}"].Value = pl.Measurements.Sum(m => m.CartonsQuantity) + " CTNS  =";
            //sheet.Cells[$"H{totalMeasurementIndex}:I{totalMeasurementIndex}"].Merge = true;
            sheet.Cells[$"F{totalMeasurementIndex}"].Value = string.Format("{0:N2} CBM", totalCbm);
            //sheet.Cells[$"J{totalMeasurementIndex}"].Value = "=";
            //sheet.Cells[$"K{totalMeasurementIndex}:L{totalMeasurementIndex}"].Merge = true;

            #endregion

            #region remark
            var remarkIndex = totalMeasurementIndex + 1;
            sheet.Cells[$"A{remarkIndex}"].Value = "REMARK";
            sheet.Cells[$"A{++remarkIndex}"].Value = pl.Remark == null ? "" : pl.Remark;

            byte[] remarkImage;
            var remarkImageIndex = remarkIndex + 1;
            if (!String.IsNullOrEmpty(pl.RemarkImageFile))
            {
                if (IsBase64String(Base64.GetBase64File(pl.RemarkImageFile)))
                {
                    remarkImage = Convert.FromBase64String(Base64.GetBase64File(pl.RemarkImageFile));
                    Image _remarkImage = byteArrayToImage(remarkImage);
                    ExcelPicture excelPictureRemarkImage = sheet.Drawings.AddPicture("RemarkImage", _remarkImage);
                    excelPictureRemarkImage.From.Column = 0;
                    excelPictureRemarkImage.From.Row = remarkImageIndex;
                    excelPictureRemarkImage.SetSize(200, 200);
                }
            }

            #endregion

            #region Signature
            var signatureIndex = remarkImageIndex + 14;
            sheet.Cells[$"G{signatureIndex}:H{signatureIndex}"].Merge = true;
            sheet.Cells[$"G{signatureIndex}"].Value = "( MRS. ADRIYANA DAMAYANTI )";
            sheet.Row(signatureIndex).Height = 18;

            sheet.Cells[$"G{signatureIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"G{signatureIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"G{signatureIndex}:H{signatureIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
           
            sheet.Cells[$"G{signatureIndex + 1}"].Value = "AUTHORIZED SIGNATURE";
            sheet.Row(signatureIndex + 1).Height = 18;
            sheet.Cells[$"G{signatureIndex + 1}:H{signatureIndex + 1}"].Merge = true;
            sheet.Cells[$"G{signatureIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"G{signatureIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"G{signatureIndex + 1}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            #endregion

            sheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 10, FontStyle.Regular));
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns(15, 40);
            sheet.Cells.Style.WrapText = true;

            sheet.PrinterSettings.LeftMargin = 0.39M;
            sheet.PrinterSettings.TopMargin = 0;
            sheet.PrinterSettings.RightMargin = 0;

            sheet.PrinterSettings.Orientation = eOrientation.Portrait;

            MemoryStream stream = new MemoryStream();
            package.DoAdjustDrawings = false;
            package.SaveAs(stream);
            return stream;
        }

        public string GetColNameFromIndex(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public int GetColNumberFromName(string columnName)
        {
            char[] characters = columnName.ToUpperInvariant().ToCharArray();
            int sum = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }
            return sum;
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            Image img = Image.FromStream(memstr);
            return img;
        }

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

    }
}
