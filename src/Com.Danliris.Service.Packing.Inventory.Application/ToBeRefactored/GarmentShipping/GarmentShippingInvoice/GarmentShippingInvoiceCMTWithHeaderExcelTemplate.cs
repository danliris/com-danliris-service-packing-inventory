﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceCMTWithHeaderExcelTemplate
    {

        public IIdentityProvider _identityProvider;

        public GarmentShippingInvoiceCMTWithHeaderExcelTemplate(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public MemoryStream GenerateExcelTemplate(GarmentShippingInvoiceViewModel viewModel, Buyer buyer, BankAccount bank, GarmentPackingListViewModel pl, int timeoffset)
        {
           
            DataTable result = new DataTable();

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");

            byte[] LogoDLImage;
            if (!String.IsNullOrEmpty(Base64ImageStrings.LOGO_DANLIRIS_58_58))
            {
                if (IsBase64String(Base64.GetBase64File(Base64ImageStrings.LOGO_DANLIRIS_58_58)))
                {
                    LogoDLImage = Convert.FromBase64String(Base64.GetBase64File(Base64ImageStrings.LOGO_DANLIRIS_58_58));
                    Image logoDLImage = byteArrayToImage(LogoDLImage);

                    ExcelPicture excelLogoDLImage = sheet.Drawings.AddPicture("LogoDLImage", logoDLImage);
                    excelLogoDLImage.From.Column = 0;
                    excelLogoDLImage.From.Row = 4;
                    excelLogoDLImage.SetSize(75, 75);
                }
            }

            byte[] LogoISOImage;
            if (!String.IsNullOrEmpty(Base64ImageStrings.ISO))
            {
                if (IsBase64String(Base64.GetBase64File(Base64ImageStrings.ISO)))
                {
                    LogoISOImage = Convert.FromBase64String(Base64.GetBase64File(Base64ImageStrings.ISO));
                    Image logoISOImage = byteArrayToImage(LogoISOImage);

                    ExcelPicture excelLogoISoImage = sheet.Drawings.AddPicture("LogoISOImage", logoISOImage);
                    excelLogoISoImage.From.Column = 9;
                    excelLogoISoImage.From.Row = 4;
                    excelLogoISoImage.SetSize(120, 75);
                }
            }

            var AddressIndex = 4;

            byte[] LogoNameImage;
            if (!String.IsNullOrEmpty(Base64ImageStrings.LOGO_NAME))
            {
                if (IsBase64String(Base64.GetBase64File(Base64ImageStrings.LOGO_NAME)))
                {
                    LogoNameImage = Convert.FromBase64String(Base64.GetBase64File(Base64ImageStrings.LOGO_NAME));
                    Image logoNameImage = byteArrayToImage(LogoNameImage);

                    ExcelPicture excelLogoNameImage = sheet.Drawings.AddPicture("LogoNameImage", logoNameImage);
                    excelLogoNameImage.From.Column = 1;
                    excelLogoNameImage.From.Row = 1;
                    excelLogoNameImage.SetSize(150, 30);


                    sheet.Row(4).Height = 12;
                    sheet.Cells[$"B{AddressIndex}"].Value = "Head Office : Jl. Merapi No. 23";
                    sheet.Cells[$"B{AddressIndex}:C{AddressIndex}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(5).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 1}"].Value = "Banaran, Grogol, Sukoharjo 57552";
                    sheet.Cells[$"B{AddressIndex + 1}:C{AddressIndex + 1}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 1}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(6).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 2}"].Value = "Central Java, Indonesia";
                    sheet.Cells[$"B{AddressIndex + 2}:C{AddressIndex + 2}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 2}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(7).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 3}"].Value = "Tel. : (+62-271) 740888, 714400";
                    sheet.Cells[$"B{AddressIndex + 3}:C{AddressIndex + 3}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 3}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(8).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 4}"].Value = "Fax. : (+62-271) 740777, 735222";
                    sheet.Cells[$"B{AddressIndex + 4}:C{AddressIndex + 4}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 4}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(9).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 5}"].Value = "PO BOX 166 Solo, 57100";
                    sheet.Cells[$"B{AddressIndex + 5}:C{AddressIndex + 5}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 5}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Row(10).Height = 12;
                    sheet.Cells[$"B{AddressIndex + 6}"].Value = "Website : www.danliris.com";
                    sheet.Cells[$"B{AddressIndex + 6}:C{AddressIndex + 6}"].Merge = true;
                    sheet.Cells[$"B{AddressIndex + 6}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }
            }

            sheet.Cells[$"A11:J11"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

            var InvIndex = 13;
            ///
            sheet.Row(13).Height = 20;
            if (viewModel.InvoiceNo.Substring(0, 2) == "SM" || viewModel.InvoiceNo.Substring(0, 3) == "SMR")
            {
                sheet.Cells[$"A{InvIndex}"].Value = "NON-COMMERCIAL INVOICE";
            }
            else
            {
                sheet.Cells[$"A{InvIndex}"].Value = "COMMERCIAL INVOICE";
            }
            sheet.Cells[$"A{InvIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{InvIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A{InvIndex}:J{InvIndex}"].Merge = true;
            ///       
            sheet.Row(15).Height = 20;
            sheet.Cells["A15"].Value = "INVOICE NO. : " + viewModel.InvoiceNo;
            sheet.Cells[$"A15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["A15:B15"].Merge = true;
            
            sheet.Column(1).Width = 20;
            sheet.Column(2).Width = 15;
            sheet.Column(3).Width = 15;
            sheet.Column(4).Width = 15;
            sheet.Column(5).Width = 10;
            sheet.Column(6).Width = 10;
            sheet.Column(7).Width = 15;
            sheet.Column(8).Width = 15;
            sheet.Column(9).Width = 15;
            sheet.Column(10).Width = 15;

            sheet.Cells[$"A15:J15"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells["E15"].Value = "DATE : " + viewModel.InvoiceDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("MMM dd, yyyy.").ToUpper();
            sheet.Cells[$"E15"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E15:G15"].Merge = true;
            //
            sheet.Row(16).Height = 18;
            sheet.Cells[$"A16"].Value = "SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF";
            sheet.Cells[$"A16"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"A16:D16"].Merge = true;

            sheet.Cells[$"E16"].Value = "CO NO.";
            sheet.Cells[$"E16"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E16:G16"].Merge = true;

            sheet.Cells[$"H16"].Value = ": " + viewModel.CO;
            sheet.Cells[$"H16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J16"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H16:J16"].Merge = true;
            //
            sheet.Row(17).Height = 18;
            sheet.Cells[$"A17"].Value = " ";
            sheet.Cells[$"A17"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A17:D17"].Merge = true;
       
            sheet.Cells[$"E17"].Value = "CONFIRMATION OF ORDER NO";
            sheet.Cells[$"E17"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E17:G17"].Merge = true;

            sheet.Cells[$"H17"].Value = ": " + viewModel.ConfirmationOfOrderNo;
            sheet.Cells[$"H17"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"H17"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J17"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H17:J17"].Merge = true;
            //

            sheet.Row(18).Height = 18;
            sheet.Cells[$"A18"].Value = "MESSRS ";
            sheet.Cells[$"A18"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            if (viewModel.InvoiceNo.Substring(0, 2) == "SM" || viewModel.InvoiceNo.Substring(0, 2) == "DS" || viewModel.InvoiceNo.Substring(0, 3) == "DLR")
            {
                sheet.Cells[$"B18"].Value = ": " + viewModel.Consignee;
                sheet.Cells[$"B18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            else
            {
                sheet.Cells[$"B18"].Value = ": " + viewModel.BuyerAgent.Name;
                sheet.Cells[$"B18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            sheet.Cells[$"B18:D18"].Merge = true;

            sheet.Cells[$"E18"].Value = "SHIPPING PER";
            sheet.Cells[$"E18"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E18:G18"].Merge = true;

            sheet.Cells[$"H18"].Value = ": " + viewModel.ShippingPer;
            sheet.Cells[$"H18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"H18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J18"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H18:J18"].Merge = true;
            //          
            sheet.Row(19).Height = 45;
            sheet.Cells[$"A19"].Value = " ";
            sheet.Cells[$"A19"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            sheet.Cells[$"B19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"B19"].Value = ": " + viewModel.ConsigneeAddress;
            sheet.Cells[$"B19:D19"].Merge = true;

            sheet.Cells[$"E19"].Value = "SAILING ON OR ABOUT";
            sheet.Cells[$"E19"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"E19:G19"].Merge = true;

            sheet.Cells[$"H19"].Value = ": " + viewModel.SailingDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"));
            sheet.Cells[$"H19"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"H19"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            sheet.Cells[$"J19"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H19:J19"].Merge = true;
            //

            sheet.Row(20).Height = 18;
            sheet.Cells[$"A20"].Value = "DELIVERED TO";
            sheet.Cells[$"A20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"A20"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"A20"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
           
            sheet.Cells[$"B20"].Value = ": " + viewModel.DeliverTo;
            sheet.Cells[$"B20"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"B20"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"B20:D20"].Merge = true;

            sheet.Row(21).Height = 18;
            sheet.Cells[$"A21"].Value = " ";
            sheet.Cells[$"A21"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"B21:D21"].Merge = true;

            sheet.Cells[$"B21:D21"].Merge = true;

            sheet.Cells[$"E21"].Value = "FROM";
            sheet.Cells[$"E21"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E21"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E21"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E21:G21"].Merge = true;

            sheet.Cells[$"H21"].Value = ": " + viewModel.From;
            sheet.Cells[$"H21"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"H217"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J21"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H21:J21"].Merge = true;
            //
  
            sheet.Cells[$"E21"].Value = "TO";
            sheet.Cells[$"E21"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E21"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"E21"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E21:G21"].Merge = true;

            sheet.Cells[$"H21"].Value = ": " + viewModel.To;
            sheet.Cells[$"H21"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[$"H21"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J21"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H21:J21"].Merge = true;

            sheet.Cells[$"A21:J21"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            var index = 0;
            //
            if (pl.PaymentTerm == "LC")
            {
                sheet.Row(22).Height = 18;
                sheet.Cells[$"A22"].Value = "LETTER OF CREDIT NUMBER";
                sheet.Cells[$"B22"].Value = ": " + viewModel.LCNo;
                sheet.Cells[$"B22:C22"].Merge = true;
                sheet.Cells[$"A22"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A22"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B22"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B22"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                sheet.Row(23).Height = 18;
                sheet.Cells[$"A23"].Value = "LC DATE";
                sheet.Cells[$"B23"].Value = ": " + pl.LCDate.GetValueOrDefault().ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-EN"));
                sheet.Cells[$"B23:C23"].Merge = true;
                sheet.Cells[$"A23"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A23"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B23"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B23"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                sheet.Row(24).Height = 18;
                sheet.Cells[$"A24"].Value = "ISSUED BY";
                sheet.Cells[$"B24"].Value = ": " + viewModel.IssuedBy;
                sheet.Cells[$"B24:C24"].Merge = true;
                sheet.Cells[$"A24:J24"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                
                index = 25;
            }
            else
            {
                sheet.Row(22).Height = 18;
                sheet.Cells[$"A22"].Value = "PAYMENT TERM";
                sheet.Cells[$"B22"].Value = ": TT PAYMENT";
                sheet.Cells[$"B22:C22"].Merge = true;
                sheet.Cells[$"A22:J22"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"A22"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A22"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[$"B22"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B22"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                index = 23;
            }

            //
            
            double totalQtys = 0;
            decimal totalAmnts = 0;
            double totalCMTPrice = 0;
            decimal totalPrice = 0;

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

            sheet.Cells[$"I{index}"].Value = "UNIT PRICE";
            sheet.Cells[$"I{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"I{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"I{index}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"I{afterIndex}"].Value = "CMT IN USD";
            sheet.Cells[$"I{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"I{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"I{afterIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"J{index}"].Value = "TOTAL PRICE";
            sheet.Cells[$"J{index}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"J{index}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{index}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"J{afterIndex}"].Value = "CMT IN USD";
            sheet.Cells[$"J{afterIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"J{afterIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{afterIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"A{afterIndex}:J{afterIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
                sheet.Cells[$"I{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;


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
                sheet.Cells[$"I{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{DescIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{DescIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
                sheet.Cells[$"I{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{DescIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{DescIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
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
                sheet.Cells[$"I{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{afterIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{afterIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
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
                sheet.Cells[$"I{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
                sheet.Cells[$"I{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
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
                sheet.Cells[$"I{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{MarkIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }

            foreach (var item in viewModel.Items.OrderBy(o => o.ComodityDesc))
            {
                //
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
                    sheet.Cells[$"E{valueIndex}"].Value = string.Format("{0:n2}", item.Quantity);
                    sheet.Cells[$"E{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"E{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                }
                else
                {
                    sheet.Cells[$"E{valueIndex}"].Value = string.Format("{0:n0}", item.Quantity);
                    sheet.Cells[$"E{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    sheet.Cells[$"E{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                }

                sheet.Cells[$"F{valueIndex}"].Value = item.Uom.Unit.TrimEnd();
                sheet.Cells[$"F{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[$"F{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"F{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"G{valueIndex}"].Value = string.Format("{0:n4}", item.Price);
                sheet.Cells[$"G{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"G{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"G{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"H{valueIndex}"].Value = string.Format("{0:n2}", item.Amount);
                sheet.Cells[$"H{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"H{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"H{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"I{valueIndex}"].Value = string.Format("{0:n4}", item.CMTPrice);
                sheet.Cells[$"I{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"I{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"I{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                sheet.Cells[$"J{valueIndex}"].Value = string.Format("{0:n2}", item.Quantity * (double)item.CMTPrice);
                sheet.Cells[$"J{valueIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                sheet.Cells[$"J{valueIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[$"J{valueIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                totalQtys += item.Quantity;
                totalAmnts += item.Amount;
                totalCMTPrice += item.Quantity * (double)item.CMTPrice;

                if (item.CMTPrice > 0)
                {
                    totalPrice += item.Amount;
                }

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
            sheet.Cells[$"I{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"I{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{afterTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{afterTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

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
            sheet.Cells[$"I{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"I{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{afterTotalIndex + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{afterTotalIndex + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
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
            sheet.Cells[$"A{grandTotalIndex}:J{grandTotalIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

            sheet.Cells[$"E{grandTotalIndex}"].Value = string.Format("{0:n0}", result1);
            sheet.Cells[$"E{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            sheet.Cells[$"E{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"E{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"E{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"F{grandTotalIndex}"].Value = result2;
            sheet.Cells[$"F{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"F{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"F{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"H{grandTotalIndex}"].Value = string.Format("{0:n2}", totalAmnts);
            sheet.Cells[$"H{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            sheet.Cells[$"H{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"H{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"H{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"J{grandTotalIndex}"].Value = string.Format("{0:n2}", totalCMTPrice);
            sheet.Cells[$"J{grandTotalIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            sheet.Cells[$"J{grandTotalIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"J{grandTotalIndex}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[$"J{grandTotalIndex}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[$"A{grandTotalIndex}:J{grandTotalIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

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
                sheet.Cells[$"A{spellingAdjustIndex}:B{spellingAdjustIndex}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex}"].Value = string.Format("{0:n2}", totalAmnts);
                sheet.Cells[$"D{spellingAdjustIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                sheet.Row(spellingAdjustIndex + 1).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex + 1}"].Value = "LESS FABRIC COST";
                sheet.Cells[$"A{spellingAdjustIndex + 1}:B{spellingAdjustIndex + 1}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex + 1}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex + 1}"].Value = string.Format("{0:n2}", (totalPrice - (decimal)totalCMTPrice) * -1);
                sheet.Cells[$"D{spellingAdjustIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                decimal totalAmountCMT = totalAmnts - (totalPrice - (decimal)totalCMTPrice);

                sheet.Row(spellingAdjustIndex + 2).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex + 2}"].Value = "TOTAL AMOUNT CMT";
                sheet.Cells[$"A{spellingAdjustIndex + 2}:B{spellingAdjustIndex + 2}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex + 2}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex + 2}"].Value = string.Format("{0:n2}", totalAmountCMT);
                sheet.Cells[$"D{spellingAdjustIndex + 2}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                decimal totalPaid = totalAmountCMT;
                var AdjustIndex = spellingAdjustIndex + 3;

                foreach (var adj in viewModel.GarmentShippingInvoiceAdjustments)
                {
                    sheet.Row(AdjustIndex).Height = 18;
                    totalPaid += adj.AdjustmentValue;
                    sheet.Cells[$"A{AdjustIndex}"].Value = adj.AdjustmentDescription;
                    sheet.Cells[$"A{AdjustIndex}:B{AdjustIndex}"].Merge = true;
                    sheet.Cells[$"C{AdjustIndex}"].Value = "  : USD ";
                    sheet.Cells[$"D{AdjustIndex}"].Value = string.Format("{0:n2}", adj.AdjustmentValue);
                    sheet.Cells[$"D{AdjustIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    AdjustIndex++;
                    TtltIndex = AdjustIndex;
                }

                sheet.Cells[$"A{TtltIndex}:D{TtltIndex}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

                sheet.Row(TtltIndex).Height = 18;
                sheet.Cells[$"A{TtltIndex}"].Value = "TOTAL AMOUNT TO BE PAID";
                sheet.Cells[$"A{TtltIndex}:B{TtltIndex}"].Merge = true;
                sheet.Cells[$"C{TtltIndex}"].Value = "  : USD";
                sheet.Cells[$"D{TtltIndex}"].Value = string.Format("{0:n2}", totalPaid);
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

                sheet.Row(TtltIndex + 2).Height = 18;
                sheet.Cells[$"A{TtltIndex + 2}:J{TtltIndex + 2}"].Merge = true;
                sheet.Cells[$"A{TtltIndex + 2}"].Value = "SAY : US DOLLARS " + amountToText.ToUpper() + " ONLY ///";

                PaymentIndex = TtltIndex + 4;
                //
                sheet.Row(PaymentIndex).Height = 20;
                sheet.Cells[$"A{PaymentIndex}:J{PaymentIndex}"].Merge = true;
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
                sheet.Row(spellingAdjustIndex).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex}"].Value = "TOTAL AMOUNT FOB";
                sheet.Cells[$"A{spellingAdjustIndex}:B{spellingAdjustIndex}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex}"].Value = string.Format("{0:n2}", totalAmnts);
                sheet.Cells[$"D{spellingAdjustIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                sheet.Row(spellingAdjustIndex + 1).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex + 1}"].Value = "LESS FABRIC COST";
                sheet.Cells[$"A{spellingAdjustIndex + 1}:B{spellingAdjustIndex + 1}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex + 1}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex + 1}"].Value = string.Format("{0:n2}", (totalPrice - (decimal)totalCMTPrice) * -1);
                sheet.Cells[$"D{spellingAdjustIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                decimal totalAmountCMT = totalAmnts - (totalPrice - (decimal)totalCMTPrice);

                sheet.Row(spellingAdjustIndex + 2).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex + 2}"].Value = "TOTAL AMOUNT CMT";
                sheet.Cells[$"A{spellingAdjustIndex + 2}:B{spellingAdjustIndex + 2}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex + 2}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex + 2}"].Value = string.Format("{0:n2}", totalAmountCMT);
                sheet.Cells[$"D{spellingAdjustIndex + 2}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                sheet.Cells[$"A{spellingAdjustIndex + 2}:D{spellingAdjustIndex + 2}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

                decimal totalPaid = totalAmountCMT;

                sheet.Row(spellingAdjustIndex + 3).Height = 18;
                sheet.Cells[$"A{spellingAdjustIndex + 3}"].Value = "TOTAL AMOUNT TO BE PAID";
                sheet.Cells[$"A{spellingAdjustIndex + 3}:B{spellingAdjustIndex + 3}"].Merge = true;
                sheet.Cells[$"C{spellingAdjustIndex + 3}"].Value = "  : USD ";
                sheet.Cells[$"D{spellingAdjustIndex + 3}"].Value = string.Format("{0:n2}", totalPaid);
                sheet.Cells[$"D{spellingAdjustIndex + 3}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                if (totalPaid < 0)
                {
                    totalPaid = totalPaid * -1;
                    amountToText = "MINUS " + CurrencyToText.ToWords(totalAmnts);
                }
                else
                {
                    amountToText = CurrencyToText.ToWords(totalPaid);
                }

                spellingWordIndex = spellingAdjustIndex + 5;

                sheet.Row(spellingWordIndex).Height = 18;
                sheet.Cells[$"A{spellingWordIndex}"].Value = "SAY : US DOLLARS " + amountToText.ToUpper() + " ONLY ///";
                sheet.Cells[$"A{spellingWordIndex}:J{spellingWordIndex}"].Merge = true;
                
                PaymentIndex = spellingWordIndex + 2;
                //
                sheet.Row(PaymentIndex).Height = 20;
                sheet.Cells[$"A{PaymentIndex}"].Value = "PLEASE TT THE ABOVE PAYMENT TO OUR CORRESPONDENCE BANK AS FOLLOW :";
                sheet.Cells[$"A{PaymentIndex}:J{PaymentIndex}"].Merge = true;
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
            sheet.Cells[$"I{signatureIndex}:J{signatureIndex}"].Merge = true;
            sheet.Cells[$"I{signatureIndex}"].Value = "( MRS. ADRIYANA DAMAYANTI )";
            sheet.Row(signatureIndex).Height = 18;

            sheet.Cells[$"I{signatureIndex}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"I{signatureIndex}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[$"I{signatureIndex}:J{signatureIndex}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
           
            sheet.Cells[$"I{signatureIndex + 1}"].Value = "AUTHORIZED SIGNATURE";
            sheet.Row(signatureIndex + 1).Height = 18;
            sheet.Cells[$"I{signatureIndex + 1}:J{signatureIndex + 1}"].Merge = true;
            sheet.Cells[$"I{signatureIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"I{signatureIndex + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[$"I{signatureIndex + 1}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
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
