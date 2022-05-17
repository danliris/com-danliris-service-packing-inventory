using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment
{
    public class GarmentMonitoringDeliveredPackingListSampleService : IGarmentMonitoringDeliveredPackingListSample
    {
        private const string UserAgent = "Repository";
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentPackingListItemRepository itemrepository;
        private readonly IGarmentPackingListDetailRepository detailrepository;
        private readonly IGarmentPackingListDetailSizeRepository detailsizerepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;
        //private readonly DbSet<GarmentPackingListModel> dbSet;


        public GarmentMonitoringDeliveredPackingListSampleService(IServiceProvider serviceProvide)
        {
            _serviceProvider = serviceProvide;
            _identityProvider = serviceProvide.GetService<IIdentityProvider>();
            plrepository = serviceProvide.GetService<IGarmentPackingListRepository>();
            detailrepository = serviceProvide.GetService<IGarmentPackingListDetailRepository>();
            detailsizerepository = serviceProvide.GetService<IGarmentPackingListDetailSizeRepository>();
            itemrepository = serviceProvide.GetService<IGarmentPackingListItemRepository>();
            
        }

        public List<GarmentMonitoringDeliveredPackingListSampleViewModel> GetData(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offset)
        {

            dateFrom = dateFrom  ?? DateTimeOffset.MinValue;
            dateTo = dateTo ?? DateTimeOffset.MaxValue;

            var quePL = plrepository.ReadAll();
            var queItem = itemrepository.ReadAll();
            var queDetail = detailrepository.ReadAll();
            var queDetailSize = detailsizerepository.ReadAll();

            var Query = (from a in quePL
                         join b in queItem on a.Id equals b.PackingListId
                         join c in queDetail on b.Id equals c.PackingListItemId
                         join d in queDetailSize on c.Id equals d.PackingListDetailId
                         where
                         b.RoType == "RO SAMPLE"
                         && a.Date.AddHours(offset).Date >= dateFrom
                         && a.Date.AddHours(offset).Date <= dateTo

                         && a.PaymentTerm == (string.IsNullOrWhiteSpace(paymentTerm) ? a.PaymentTerm : paymentTerm)
                         //&& invoiceNo.Contains(a.InvoiceNo)
                         && a.InvoiceNo == (string.IsNullOrWhiteSpace(invoiceNo) ? a.InvoiceNo : invoiceNo)
                         && a.IsSampleDelivered ==  true
                         && a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false && d.IsDeleted == false
                         select new GarmentMonitoringDeliveredPackingListSampleViewModel
                         {
                             InvoiceNo = a.InvoiceNo,
                             PackingListType = a.PackingListType,
                             InvoiceType = a.InvoiceType,
                             Section = a.SectionCode,
                             Date = a.Date,
                             PaymentTerm = a.PaymentTerm,
                             BuyerAgent = a.BuyerAgentName,
                             TruckingDate = a.TruckingDate,
                             Destination = a.Destination,
                             CreatedBy = a.CreatedBy,
                             RONo = b.RONo,
                             Article = b.Article,
                             Comodity = b.ComodityName,
                             Quantity = b.Quantity,
                             Index = c.Index,
                             Carton1 = c.Carton1,
                             Carton2 = c.Carton2,
                             Style = c.Style,
                             Colour = c.Colour,
                             CartonQuantity = c.CartonQuantity,
                             QuantityPCS = c.QuantityPCS,
                             Size = d.Size,
                             SizeQuantity = d.Quantity,
                         })
                         .OrderBy(x => x.InvoiceNo)
                         .ToList();

            return Query;
        }

        public ListResult<GarmentMonitoringDeliveredPackingListSampleViewModel> GetReportData(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offset)
        {
            var Query = GetData(invoiceNo,paymentTerm, dateFrom, dateTo, offset);
      
            var total = Query.Count;
      

            return new ListResult<GarmentMonitoringDeliveredPackingListSampleViewModel>(Query, 1, total, total);
        }

        public MemoryStream GenerateExcel(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offset)
        {
            var Query = GetData(invoiceNo, paymentTerm, dateFrom, dateTo,offset);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing List", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Invoice", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Invoice", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Payment Term", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Trucking", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Per RO", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Index", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Carton 1", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Carton 2", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Style", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Colour", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Per Carton", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Carton", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Size", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Per Size", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "User", DataType = typeof(string) });

            ExcelPackage package = new ExcelPackage();
            var dateFromString = dateFrom == null ? "" : $" from {DateTimeToString(dateFrom.GetValueOrDefault())}";
            var dateToString = dateTo == null ? "" : $" to {DateTimeToString(dateTo.GetValueOrDefault())}";

            if (Query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "",0,0,0,0,"","",0,0,"",0,"");

                bool styling = true;
                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    sheet.Row(6).Style.Font.Bold = true;

                    #region KopTable
                    sheet.Cells[$"A1:W1"].Value = "REPORT MONITORING DELIVERED PACKING LIST SAMPLE";
                    sheet.Cells[$"A1:W1"].Merge = true;
                    sheet.Cells[$"A1:W1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:W1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:W1"].Style.Font.Bold = true;
                    sheet.Cells[$"A4:W4"].Value = string.Format("Periode Tanggal : {0} s/d {1}", dateFromString, dateToString);
                    sheet.Cells[$"A4:W4"].Merge = true;
                    sheet.Cells[$"A4:W4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A4:W4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:W4"].Style.Font.Bold = true;
                    sheet.Cells[$"A2:W2"].Value = string.Format("Invoice No : {0}", string.IsNullOrWhiteSpace(invoiceNo) ? "-" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().InvoiceNo));
                    sheet.Cells[$"A2:W2"].Merge = true;
                    sheet.Cells[$"A2:W2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:W2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:W2"].Style.Font.Bold = true;
                    sheet.Cells[$"A3:W3"].Value = string.Format("Payment Term : {0}", string.IsNullOrWhiteSpace(paymentTerm) ? "-" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().PaymentTerm));
                    sheet.Cells[$"A3:W3"].Merge = true;
                    sheet.Cells[$"A3:W3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A3:W3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A3:W3"].Style.Font.Bold = true;
                    #endregion
                    sheet.Cells[$"A6:W6"].Style.Font.Bold = true;
                    sheet.Cells["A6"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int counter = 5;
                int idx = 1;
                var rCount = 0;
                Dictionary<string, string> Rowcount = new Dictionary<string, string>();

                foreach (var d in Query)
                {
                    string InvDate = d.Date == new DateTime(1970, 1, 1) ? "-" : d.Date.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string TruckDate = d.TruckingDate == DateTimeOffset.MinValue ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    idx++;
                    if (!Rowcount.ContainsKey(d.InvoiceNo))
                    {
                        rCount = 0;
                        var index = idx;
                        Rowcount.Add(d.InvoiceNo, index.ToString());
                    }
                    else
                    {
                        rCount += 1;
                        Rowcount[d.InvoiceNo] = Rowcount[d.InvoiceNo] + "-" + rCount.ToString();
                        var val = Rowcount[d.InvoiceNo].Split("-");
                        if ((val).Length > 0)
                        {
                            Rowcount[d.InvoiceNo] = val[0] + "-" + rCount.ToString();
                        }
                    }

                    dt.Rows.Add(d.InvoiceNo, d.PackingListType, d.InvoiceType, d.Section, InvDate, d.PaymentTerm, d.BuyerAgent, TruckDate, d.Destination, d.RONo, d.Article, d.Comodity, d.Quantity, d.Index, d.Carton1, d.Carton2, d.Style, d.Colour, d.QuantityPCS, d.CartonQuantity, d.Size, d.SizeQuantity, d.CreatedBy);
                    counter++;

                }
                 bool styling = true;
                 
                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Territory") })
                    {
                        var sheet = package.Workbook.Worksheets.Add(item.Value);

                        #region KopTable
                        sheet.Cells[$"A1:W1"].Value = "REPORT MONITORING DELIVERED PACKING LIST SAMPLE";
                        sheet.Cells[$"A1:W1"].Merge = true;
                        sheet.Cells[$"A1:W1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"A1:W1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        sheet.Cells[$"A1:W1"].Style.Font.Bold = true;
                        sheet.Cells[$"A4:W4"].Value = string.Format("Periode Tanggal : {0} s/d {1}", dateFromString , dateToString);
                        sheet.Cells[$"A4:W4"].Merge = true;
                        sheet.Cells[$"A4:W4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"A4:W4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        sheet.Cells[$"A4:W4"].Style.Font.Bold = true;
                        sheet.Cells[$"A2:W2"].Value = string.Format("Invoice No : {0}", string.IsNullOrWhiteSpace(invoiceNo)? "-" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().InvoiceNo));
                        sheet.Cells[$"A2:W2"].Merge = true;
                        sheet.Cells[$"A2:W2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"A2:W2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        sheet.Cells[$"A2:W2"].Style.Font.Bold = true;
                        sheet.Cells[$"A3:W3"].Value = string.Format("Payment Term : {0}", string.IsNullOrWhiteSpace(paymentTerm) ? "-" : (Query.Count() == 0 ? "-" : Query.FirstOrDefault().PaymentTerm));
                        sheet.Cells[$"A3:W3"].Merge = true;
                        sheet.Cells[$"A3:W3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"A3:W3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        sheet.Cells[$"A3:W3"].Style.Font.Bold = true;
                        #endregion
                        sheet.Cells[$"A6:W6"].Style.Font.Bold = true;
                        sheet.Cells["A6"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.Light16);

                        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                    foreach (var rowMerge in Rowcount)
                    {
                        var UnitrowNum = rowMerge.Value.Split("-");
                        int rowNum2 = 1;
                        int rowNum1 = Convert.ToInt32(UnitrowNum[0]);
                        if (UnitrowNum.Length > 1)
                        {
                            rowNum2 = Convert.ToInt32(rowNum1) + Convert.ToInt32(UnitrowNum[1]);
                        }
                        else
                        {
                            rowNum2 = Convert.ToInt32(rowNum1);
                        }

                        sheet.Cells[$"A{(rowNum1 + 5)}:A{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"A{(rowNum1 + 5)}:A{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"A{(rowNum1 + 5)}:A{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"B{(rowNum1 + 5)}:B{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"B{(rowNum1 + 5)}:B{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"B{(rowNum1 + 5)}:B{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"C{(rowNum1 + 5)}:C{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"C{(rowNum1 + 5)}:C{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"C{(rowNum1 + 5)}:C{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"D{(rowNum1 + 5)}:D{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"D{(rowNum1 + 5)}:D{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"D{(rowNum1 + 5)}:D{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"E{(rowNum1 + 5)}:E{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"E{(rowNum1 + 5)}:E{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"E{(rowNum1 + 5)}:E{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"F{(rowNum1 + 5)}:F{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"F{(rowNum1 + 5)}:F{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"F{(rowNum1 + 5)}:F{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"G{(rowNum1 + 5)}:G{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"G{(rowNum1 + 5)}:G{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"G{(rowNum1 + 5)}:G{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"H{(rowNum1 + 5)}:H{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"H{(rowNum1 + 5)}:H{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"H{(rowNum1 + 5)}:H{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        sheet.Cells[$"I{(rowNum1 + 5)}:I{(rowNum2 + 5)}"].Merge = true;
                        sheet.Cells[$"I{(rowNum1 + 5)}:I{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[$"I{(rowNum1 + 5)}:I{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"J{(rowNum1 + 5)}:J{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"J{(rowNum1 + 5)}:J{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"J{(rowNum1 + 5)}:J{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"K{(rowNum1 + 5)}:K{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"K{(rowNum1 + 5)}:K{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"K{(rowNum1 + 5)}:K{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"L{(rowNum1 + 5)}:L{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"L{(rowNum1 + 5)}:L{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"L{(rowNum1 + 5)}:L{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"M{(rowNum1 + 5)}:M{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"M{(rowNum1 + 5)}:M{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"M{(rowNum1 + 5)}:M{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"N{(rowNum1 + 5)}:N{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"N{(rowNum1 + 5)}:N{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"N{(rowNum1 + 5)}:N{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"O{(rowNum1 + 5)}:O{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"O{(rowNum1 + 5)}:O{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"O{(rowNum1 + 5)}:O{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"P{(rowNum1 + 5)}:P{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"P{(rowNum1 + 5)}:P{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"P{(rowNum1 + 5)}:P{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"Q{(rowNum1 + 5)}:Q{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"Q{(rowNum1 + 5)}:Q{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"Q{(rowNum1 + 5)}:Q{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"R{(rowNum1 + 5)}:R{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"R{(rowNum1 + 5)}:R{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"R{(rowNum1 + 5)}:R{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"S{(rowNum1 + 5)}:S{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"S{(rowNum1 + 5)}:S{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"S{(rowNum1 + 5)}:S{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //sheet.Cells[$"T{(rowNum1 + 5)}:T{(rowNum2 + 5)}"].Merge = true;
                        //sheet.Cells[$"T{(rowNum1 + 5)}:T{(rowNum2 + 5)}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //sheet.Cells[$"T{(rowNum1 + 5)}:T{(rowNum2 + 5)}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    }
                }
            }   
            
            var stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;

        }

        private string DateTimeToString(DateTimeOffset dateTime)
        {
   
            return dateTime.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
        }

    }
}
