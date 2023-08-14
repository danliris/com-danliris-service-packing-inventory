using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using OfficeOpenXml;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceDetailLocalSalesJournalService : IGarmentFinanceDetailLocalSalesJournalService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository repositoryItem;

        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;

        public GarmentFinanceDetailLocalSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            repositoryItem = serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
        }
      
        public List<GarmentFinanceDetailLocalSalesJournalViewModel> GetReportQuery(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            //DateTime dateFrom = new DateTime(year, month, 1);
            //int nextYear = month == 12 ? year + 1 : year;
            //int nextMonth = month == 12 ? 1 : month + 1;
            //DateTime dateTo = new DateTime(nextYear, nextMonth, 1);

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            List<GarmentFinanceDetailLocalSalesJournalViewModel> data = new List<GarmentFinanceDetailLocalSalesJournalViewModel>();
        
            var queryHeader = repository.ReadAll()
                         .Where(w => w.IsDeleted == false && w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date
                         && (w.TransactionTypeCode == "LBL" || w.TransactionTypeCode == "LBM" || w.TransactionTypeCode == "SBJ"
                         || w.TransactionTypeCode == "SMR" || w.TransactionTypeCode == "LJS" || w.TransactionTypeCode == "LBJ")) 
                         .Select(a => new { a.Id, a.NoteNo, a.Date, a.BuyerCode, a.BuyerName, a.TransactionTypeCode, a.UseVat });

            var query = from a in queryHeader
                        join b in repositoryItem.ReadAll() on a.Id equals b.LocalSalesNoteId
                        where b.IsDeleted == false

                        group new { Amt = b.Quantity * b.Price } by new { a.NoteNo, a.Date, a.BuyerCode, a.BuyerName, a.TransactionTypeCode, a.UseVat } into G


                        select new GarmentFinanceDetailLocalSalesJournalViewModel
                        {
                            NoteNo = G.Key.NoteNo,
                            NoteDate = G.Key.Date,
                            BuyerName = G.Key.BuyerCode +" - " + G.Key.BuyerName,
                            NoteType = G.Key.TransactionTypeCode,
                            CurrencyCode = "IDR",
                            Rate = 1,
                            Amount = G.Sum(m => m.Amt),
                            UseVat = G.Key.UseVat == true ? "YA" : "TIDAK",
                            VatAmount = G.Key.UseVat == true ? (G.Sum(m => m.Amt) * 110 / 100) : G.Sum(m => m.Amt),
                            COACode = "-",
                            COAName = "-",
                            Debit = 0,
                            Credit = 0,
                        };

            // DETAIL JURNAL

            foreach (GarmentFinanceDetailLocalSalesJournalViewModel x in query.OrderBy(x => x.NoteNo))
            {
                var debit = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    NoteNo = x.NoteNo,
                    NoteDate = x.NoteDate,
                    BuyerName = x.BuyerName,
                    NoteType = x.NoteType,
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.Amount,
                    VatAmount = x.VatAmount,
                    COACode = "110300",
                    COAName = "PIUTANG USAHA LOKAL GARMENT",
                    Debit = x.VatAmount,
                    Credit = 0,
                };

                data.Add(debit);

                var credit1 = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    NoteNo = x.NoteNo,
                    NoteDate = x.NoteDate,
                    BuyerName = x.BuyerName,
                    NoteType = x.NoteType,
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.Amount,
                    VatAmount = x.VatAmount,
                    COACode = x.NoteType == "LJS" ? "501320" : x.NoteType == "LBJ" ? "501120" : "501420",
                    COAName = x.NoteType == "LJS" ? "       PENJUALAN JASA LOKAL" : x.NoteType == "LBJ" ? "       PENJUALAN BARANG JADI LOKAL" : "       PENJUALAN LAIN-LAIN LOKAL",
                    Debit =  0,
                    Credit = x.Amount,
                };

                data.Add(credit1);

                var credit2 = new GarmentFinanceDetailLocalSalesJournalViewModel
                {
                    NoteNo = x.NoteNo,
                    NoteDate = x.NoteDate,
                    BuyerName = x.BuyerName,
                    NoteType = x.NoteType,
                    CurrencyCode = x.CurrencyCode,
                    Rate = x.Rate,
                    Amount = x.Amount,
                    VatAmount = x.VatAmount,
                    COACode = "341300",
                    COAName = "       PPN KELUARAN",
                    Debit = 0,
                    Credit = x.VatAmount - x.Amount,
                };

                if (credit2.Credit > 0)
                    {
                    data.Add(credit2);
                    }
            }

            var total = new GarmentFinanceDetailLocalSalesJournalViewModel
            {
                NoteNo = "",
                NoteDate = DateTimeOffset.MinValue,
                BuyerName = "",
                NoteType = "",
                CurrencyCode = "",
                Rate = 0,
                Amount = 0,
                VatAmount =0,
                COACode = "",
                COAName = "J U M L A H",
                Debit =  query.Sum(a => a.VatAmount),
                Credit = query.Sum(a => a.VatAmount),
            };
          
            data.Add(total);

            return data;
        }

        //public List<GarmentFinanceDetailLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        //{
        //    var Query = GetReportQuery(dateFrom, dateTo, offset);
        //    return Query.ToList();
        //}

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = GetReportQuery(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TANGGAL", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO AKUN ", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NAMA AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });     
            
            result.Columns.Add(new DataColumn() { ColumnName = "MATA UANG", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KURS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });
            ExcelPackage package = new ExcelPackage();

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", "", "", "", "", 0, 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;

                    sheet.Column(6).Width = 15;
                    sheet.Column(7).Width = 15;
                    sheet.Column(8).Width = 20;
                    sheet.Column(9).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT. DAN LIRIS";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN LOKAL";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;
                    string InvDate = d.NoteDate == new DateTime(1970, 1, 1) ? "-" : d.NoteDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    result.Rows.Add( d.NoteNo, InvDate, d.COACode, d.COAName, d.BuyerName, d.CurrencyCode, d.Rate, d.Debit, d.Credit);
                }

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    sheet.Column(1).Width = 15;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 15;
                    sheet.Column(4).Width = 50;
                    sheet.Column(5).Width = 50;

                    sheet.Column(6).Width = 15;
                    sheet.Column(7).Width = 15;
                    sheet.Column(8).Width = 20;
                    sheet.Column(9).Width = 20;

                    #region KopTable
                    sheet.Cells[$"A1:D1"].Value = "PT. DAN LIRIS";
                    sheet.Cells[$"A1:D1"].Merge = true;
                    sheet.Cells[$"A1:D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A1:D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A1:D1"].Style.Font.Bold = true;

                    sheet.Cells[$"A2:D2"].Value = "ACCOUNTING DEPT.";
                    sheet.Cells[$"A2:D2"].Merge = true;
                    sheet.Cells[$"A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    sheet.Cells[$"A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A2:D2"].Style.Font.Bold = true;

                    sheet.Cells[$"A4:D4"].Value = "RINCIAN JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PENJUALAN LOKAL";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("dd-MM-yyyy") + " S/D " + DateTo.ToString("dd-MM-yyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }
        
    }

    //public class BaseResponse<T>
    //{
    //    public string apiVersion { get; set; }
    //    public int statusCode { get; set; }
    //    public string message { get; set; }
    //    public T data { get; set; }

    //    public static implicit operator BaseResponse<T>(BaseResponse<string> v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
   
}
