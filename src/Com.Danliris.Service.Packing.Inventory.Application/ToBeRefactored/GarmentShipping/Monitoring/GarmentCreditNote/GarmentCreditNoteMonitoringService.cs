using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditNote
{
    public class GarmentCreditNoteMonitoringService : IGarmentCreditNoteMonitoringService
    {
        private readonly IGarmentShippingNoteRepository repository;
        private readonly IGarmentShippingNoteItemRepository itemrepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityProvider _identityProvider;

        public GarmentCreditNoteMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }

       public IQueryable<GarmentCreditNoteMonitoringViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();
            var queryitem = itemrepository.ReadAll();
       
            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                query = query.Where(w => w.BuyerCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.BuyerCode).ThenBy(b => b.NoteNo);
            
            var newQ = (from a in query
                        join b in queryitem on a.Id equals b.ShippingNoteId
                        where a.NoteType == GarmentShippingNoteTypeEnum.CN

                        select new GarmentCreditNoteMonitoringViewModel
                        {
                            CNNo = a.NoteNo,
                            CNDate = a.Date,
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            Description = b.Description,
                            CurrencyCode = b.CurrencyCode,
                            Amount = b.Amount,
                          });
            return newQ;
        }

        public List<GarmentCreditNoteMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.BuyerCode).ThenBy(b => b.CNNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
       {

            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Nota Kredit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Nota Kredit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "K e t e r a n g a n", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mata Uang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "J u m l a h", DataType = typeof(string) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string NoteDate = d.CNDate == new DateTime(1970, 1, 1) ? "-" : d.CNDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                 
                    string Amnt = string.Format("{0:N2}", d.Amount);
                       
                    result.Rows.Add(index, d.CNNo, NoteDate, d.BuyerCode, d.BuyerName, d.Description, d.CurrencyCode, Amnt);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public ListResult<GarmentCreditNoteMIIMonitoringViewModel> GetReportDataMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetDataMII(dateFrom, dateTo, offset);
            var total = data.Count;

            return new ListResult<GarmentCreditNoteMIIMonitoringViewModel>(data, 1, total, total);
        }

        public List<GarmentCreditNoteMIIMonitoringViewModel> GetDataMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var query = repository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            query = query.Where(w => w.ReceiptDate.AddHours(offset).Date >= DateFrom.Date && w.ReceiptDate.AddHours(offset).Date <= DateTo.Date && w.ReceiptNo != null);

            //queryCA = queryCA.OrderBy(w => w.InvoiceNo);
            //List<GarmentCreditNoteMIIMonitoringViewModel> datadn = new List<GarmentCreditNoteMIIMonitoringViewModel>();

            var Query = (from a in query
                         join b in itemrepository.ReadAll() on a.Id equals b.ShippingNoteId
                         where a.NoteType == GarmentShippingNoteTypeEnum.CN

                         select new GarmentCreditNoteMIIMonitoringViewModel
                         {
                             CNId = a.Id,
                             DNDate = a.Date,
                             DNDate1 = a.Date.AddHours(7).Date,
                             InvoiceNo = a.NoteNo,
                             BuyerCode = a.BuyerCode,
                             BuyerName = a.BuyerName,
                             BankName = "",
                             AccountBankNo = "",
                             ReceiptNo = a.ReceiptNo == null ? "-" : a.ReceiptNo,
                             Amount = Convert.ToDecimal(a.TotalAmount),
                             CurrencyCode = b.CurrencyCode,
                             Rate = b.CurrencyCode == "IDR" ? 1 : 0,
                             AmountIDR = b.CurrencyCode == "IDR" ? Convert.ToDecimal(a.TotalAmount) : 0,
                             Header = 1
                             //ItemCreditNote = b.ItemTypeDebitCreditNote == null ? "-" : b.ItemTypeDebitCreditNote,
                         }).Distinct();

            var QuerySub = (from a in query
                            join b in itemrepository.ReadAll() on a.Id equals b.ShippingNoteId
                            where a.NoteType == GarmentShippingNoteTypeEnum.CN

                            select new GarmentCreditNoteMIIMonitoringViewModel
                            {
                                CNId = a.Id,
                                DNDate = a.Date,
                                DNDate1 = a.Date.AddHours(7).Date,
                                InvoiceNo = b.ItemTypeDebitCreditNote,
                                BuyerCode = a.BuyerCode,
                                BuyerName = a.BuyerName,
                                BankName = "",
                                AccountBankNo = "",
                                ReceiptNo = a.ReceiptNo == null ? "-" : a.ReceiptNo,
                                Amount = Convert.ToDecimal(b.Amount),
                                CurrencyCode = b.CurrencyCode,
                                Rate = b.CurrencyCode == "IDR" ? 1 : 0,
                                AmountIDR = b.CurrencyCode == "IDR" ? Convert.ToDecimal(b.Amount) : 0,
                                Header = 0
                                //ItemCreditNote = b.ItemTypeDebitCreditNote == null ? "-" : b.ItemTypeDebitCreditNote,
                            });

            var result = Query.Union(QuerySub).OrderBy(x => x.CNId).ThenByDescending( x => x.Header).ToList();

            //var result2 = result.OrderBy(x => x.CNId);
                
                //.OrderBy( x => x.InvoiceNo).ThenByDescending( x => x.Header).ToList();
            ///

            //var currencyFilters = Query
            //             .GroupBy(o => new { o.DNDate1, o.CurrencyCode })
            //             .Select(o => new CurrencyFilter { date = o.Key.DNDate1.Date, code = o.Key.CurrencyCode })
            //             .ToList();

            //var currencies = GetCurrencies(currencyFilters).Result;

            //decimal rate;

            //foreach (var data in Query)
            //{
            //    rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date.Date == data.DNDate1.Date).Select(s => s.rate).FirstOrDefault());
            //    //rate = Convert.ToDecimal(currencies.Where(q => q.code == data.CurrencyCode && q.date == data.CADate.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).Date).Select(s => s.rate).LastOrDefault());

            //    if (data.CurrencyCode == "USD")
            //    {
            //        data.Rate = rate;
            //        data.AmountIDR = rate * data.Amount;
            //        datadn.Add(data);
            //    }
            //}
            //

            return result;
        }

        public MemoryStream GenerateExcelMII(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetDataMII(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KD BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA BUYER", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO KWITANSI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK DEVISA", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO REK BANK", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO CREDIT NOTE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "CURRENCY", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RATE", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT IDR", DataType = typeof(decimal) });

            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", 0, 0, 0);
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string DNTgl = d.DNDate == new DateTime(1970, 1, 1) ? "-" : d.DNDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    result.Rows.Add(DNTgl, d.BuyerCode, d.BuyerName, d.ReceiptNo, d.BankName, d.AccountBankNo, d.InvoiceNo, d.CurrencyCode, d.Rate, d.Amount, d.AmountIDR);
                    
                }
            }

            ExcelPackage package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Sheet 1");

            //var row = 1;
            //var merge = 4;
            var headers = new string[] { "TANGGAL", "KD BUYER", "NAMA BUYER", "NO KWITANSI", "BANK DEVISA", "NO REK BANK", "NO CREDIT NOTE", "CURRENCY", "RATE", "AMOUNT", "AMOUNT IDR" };
            var col = (char)('A' + 1);
            foreach (var i in Enumerable.Range(0, 11))
            {
                col = (char)('A' + i);
                sheet.Cells[$"{col}1"].Value = headers[i];
                //sheet.Cells[$"{col}8:{col}9"].Merge = true;
                sheet.Cells[$"{col}1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"{col}1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[$"{col}1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //sheet.Cells[row, 1].Value = "TANGGAL";
            //sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            //sheet.Cells[row, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            //sheet.Cells[row, 1, merge, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            //sheet.Cells[row, 1, merge, 1].Merge = true;

            //sheet.Cells[row, 2].Value = "KD BUYER";
            //sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            //sheet.Cells[row, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            //sheet.Cells[row, 2, merge, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            //sheet.Cells[row, 2, merge, 2].Merge = true;

            //sheet.Cells[row, 2].Value = "KD BUYER";
            //sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            //sheet.Cells[row, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            //sheet.Cells[row, 2, merge, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            //sheet.Cells[row, 2, merge, 2].Merge = true;

            //var count = Query.Count();
            var indexBold = 0;

            foreach (var d in Query)
            {
                indexBold++;

                if (d.Header == 1)
                {
                    sheet.Cells[$"A{1 + indexBold}:K{1 + indexBold}"].Style.Font.Bold = true;
                }
                else {
                    sheet.Cells[$"A{1 + indexBold}:K{1 + indexBold}"].Style.Font.Bold = false;
                }
                

            }

            int tableRowStart = 2;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(result, false, OfficeOpenXml.Table.TableStyles.Light8);

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
