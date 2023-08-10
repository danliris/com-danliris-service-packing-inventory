using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using System.Linq;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal
{
    public class GarmentFinanceLocalSalesJournalService : IGarmentFinanceLocalSalesJournalService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository repositoryItem;

        private readonly IIdentityProvider _identityProvider;

        public GarmentFinanceLocalSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            repositoryItem= serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        //public List<GarmentFinanceLocalSalesJournalViewModel> GetReportQuery(int month, int year, int offset)
        public List<GarmentFinanceLocalSalesJournalViewModel> GetReportQuery(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            //DateTime dateFrom = new DateTime(year, month, 1);
            //int nextYear = month == 12 ? year + 1 : year;
            //int nextMonth = month == 12 ? 1 : month + 1;
            //DateTime dateTo = new DateTime(nextYear, nextMonth, 1);

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            List<GarmentFinanceLocalSalesJournalViewModel> data = new List<GarmentFinanceLocalSalesJournalViewModel>();

            var queryHeader = repository.ReadAll()
                .Where(w => w.IsDeleted == false && w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date
                    && (w.TransactionTypeCode == "LBL" || w.TransactionTypeCode == "LBM" || w.TransactionTypeCode == "SBJ" 
                    || w.TransactionTypeCode == "SMR" || w.TransactionTypeCode=="LJS" || w.TransactionTypeCode == "LBJ"))
                    .Select(a=>new { a.Id, a.TransactionTypeCode, a.UseVat});

            var query = from a in queryHeader
                        join b in repositoryItem.ReadAll() on a.Id equals b.LocalSalesNoteId
                        where b.IsDeleted == false
                        select new GarmentFinanceLocalSalesJournalViewModel
                        {
                            remark = a.TransactionTypeCode == "LJS" ? "     PENJUALAN JASA LOKAL" : a.TransactionTypeCode == "LBJ" ? "     PENJUALAN BARANG JADI LOKAL" : "     PENJUALAN LAIN-LAIN LOKAL",
                            credit = b.Quantity * b.Price,
                            debit = a.UseVat ? (b.Quantity * b.Price * 110 / 100) : (b.Quantity * b.Price),
                            account = a.TransactionTypeCode == "LJS" ? "501320" : a.TransactionTypeCode == "LBJ" ? "501120" : "501420",
                            type = a.TransactionTypeCode == "LJS" ? "C" : a.TransactionTypeCode == "LBJ" ? "D" : "B",                            
                        };

           var debit = new GarmentFinanceLocalSalesJournalViewModel
            {
                remark = "PIUTANG USAHA LOKAL GARMENT",
                credit = 0,
                debit = query.Sum(a => a.debit),
                account = "110300",                
                type = "A"
            };
            data.Add(debit);

            var sumquery = query.ToList()
                   .GroupBy(x => new { x.remark, x.account, x.type }, (key, group) => new
                   {
                       Remark = key.remark,
                       Account = key.account,
                       Credit = group.Sum(s => s.credit),
                       Type = key.type
                   }).OrderBy(a => a.Type);

            foreach (var item in sumquery)
            {
                var obj = new GarmentFinanceLocalSalesJournalViewModel
                {
                    remark = item.Remark,
                    credit = item.Credit,
                    debit = 0,
                    account = item.Account,
                    type=item.Type
                };

                data.Add(obj);
            }
           
            var ppn = new GarmentFinanceLocalSalesJournalViewModel
            {
                remark = "     PPN KELUARAN",
                credit = query.Sum(a => a.debit) - query.Sum(a => a.credit),
                debit = 0,
                account = "341300",                  
                type = "E"
            };

            data.Add(ppn);

            var total = new GarmentFinanceLocalSalesJournalViewModel
            {
                remark = "J U M L A H :",
                credit = debit.debit,
                debit = debit.debit,
                account = "",
                type = "F"
            };
            data.Add(total);

            return data.OrderBy(a=>a.type).ToList();
        }

        //public MemoryStream GenerateExcel(int month, int year, int offset)
        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            //var Query = GetReportQuery(month, year, offset);
            var Query = GetReportQuery(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "AKUN DAN KETERANGAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });

            ExcelPackage package = new ExcelPackage();

            //if (Query.ToArray().Count() == 0)
            //    result.Rows.Add("", "", "", "");
            //else

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", 0, 0);
                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);

                    //string Bln = MONTH_NAMES[month - 1];

                    sheet.Column(1).Width = 50;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 20;
                    sheet.Column(4).Width = 20;

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

                    sheet.Cells[$"A4:D4"].Value = "IKHTISAR JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PEMBELIAN IMPORT";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("ddMMyyyy") + " S/D " + DateTo.ToString("ddMMyyyy");
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

                    result.Rows.Add(d.remark, d.account, d.debit, d.credit);
                }

                bool styling = true;

                foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
                {
                    var sheet = package.Workbook.Worksheets.Add(item.Value);
                    //string Bln = MONTH_NAMES[month - 1];

                    sheet.Column(1).Width = 50;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 20;
                    sheet.Column(4).Width = 20;

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

                    sheet.Cells[$"A4:D4"].Value = "IKHTISAR JURNAL";
                    sheet.Cells[$"A4:D4"].Merge = true;
                    sheet.Cells[$"A4:D4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[$"A4:D4"].Style.Font.Bold = true;

                    sheet.Cells[$"C5"].Value = "BUKU HARIAN";
                    sheet.Cells[$"C5"].Style.Font.Bold = true;
                    sheet.Cells[$"D5"].Value = ": PEMBELIAN IMPORT";
                    sheet.Cells[$"D5"].Style.Font.Bold = true;

                    sheet.Cells[$"C6"].Value = "PERIODE";
                    sheet.Cells[$"C6"].Style.Font.Bold = true;
                    sheet.Cells[$"D6"].Value = ": " + DateFrom.ToString("ddMMyyyy") + " S/D " + DateTo.ToString("ddMMyyyy");
                    sheet.Cells[$"D6"].Style.Font.Bold = true;

                    #endregion
                    sheet.Cells["A8"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                    //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
            //if (Query.ToArray().Count() == 0)
            //    result.Rows.Add("", "", "", "");
            //else
            //{
            //    foreach (var d in Query)
            //    {

            //        string Credit = d.credit > 0 ? string.Format("{0:N2}", d.credit) : "";
            //        string Debit = d.debit > 0 ? string.Format("{0:N2}", d.debit) : "";

            //        result.Rows.Add(d.remark, d.account, Debit, Credit);
            //    }
            //}

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }


        //public List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(int month, int year, int offset)
        public List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
        var Query = GetReportQuery(dateFrom, dateTo, offset);
        //var Query = GetReportQuery(month, year, offset);
            return Query.ToList();
        }
    }
}
