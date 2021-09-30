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

        public List<GarmentFinanceLocalSalesJournalViewModel> GetReportQuery(int month, int year, int offset)
        {

            DateTime dateFrom = new DateTime(year, month, 1);
            int nextYear = month == 12 ? year + 1 : year;
            int nextMonth = month == 12 ? 1 : month + 1;
            DateTime dateTo = new DateTime(nextYear, nextMonth, 1);
            List<GarmentFinanceLocalSalesJournalViewModel> data = new List<GarmentFinanceLocalSalesJournalViewModel>();

            var queryHeader = repository.ReadAll()
                .Where(w => w.Date.AddHours(offset).Date >= dateFrom && w.Date.AddHours(offset).Date < dateTo.Date
                    && (w.TransactionTypeCode == "LBL" || w.TransactionTypeCode == "LBM" || w.TransactionTypeCode == "SBJ" 
                    || w.TransactionTypeCode == "SMR" || w.TransactionTypeCode=="LJS" || w.TransactionTypeCode == "LBJ"))
                    .Select(a=>new { a.Id, a.TransactionTypeCode, a.UseVat});

            var query = from a in queryHeader
                        join b in repositoryItem.ReadAll() on a.Id equals b.LocalSalesNoteId
                        select new GarmentFinanceLocalSalesJournalViewModel
                        {
                            remark = a.TransactionTypeCode == "LJS" ? "     PENJUALAN JASA LOKAL" : a.TransactionTypeCode == "LBJ" ? "     PENJUALAN BARANG JADI LOKAL" : "     PENJUALAN LAIN-LAIN LOKAL",
                            credit = b.Quantity * b.Price,
                            debit = a.UseVat ? (b.Quantity * b.Price * 110 / 100) : (b.Quantity * b.Price),
                            account = a.TransactionTypeCode == "LJS" ? "5013.00.4.00" : a.TransactionTypeCode == "LBJ" ? "5011.00.4.00" : "5014.00.4.00",
                            type = a.TransactionTypeCode == "LJS" ? "C" : a.TransactionTypeCode == "LBJ" ? "D" : "B",
                            
                        };

            var debit = new GarmentFinanceLocalSalesJournalViewModel
            {
                remark = "PIUTANG USAHA LOKAL GARMENT",
                credit = 0,
                debit = query.Sum(a => a.debit),
                account = "1101.00.4.00",
                type="A"
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
                credit = query.Sum(a => a.debit)- query.Sum(a => a.credit),
                debit = 0,
                account = "3320.00.4.00",
                type = "E"
            };

            data.Add(ppn);

            var total = new GarmentFinanceLocalSalesJournalViewModel
            {
                remark = "",
                credit = debit.debit,
                debit = debit.debit,
                account = "",
                type = "F"
            };
            data.Add(total);

            return data.OrderBy(a=>a.type).ToList();
        }
        public MemoryStream GenerateExcel(int month, int year, int offset)
        {
            var Query = GetReportQuery(month, year, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "AKUN DAN KETERANGAN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AKUN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DEBET", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "KREDIT", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "");
            else
            {
                foreach (var d in Query)
                {

                    string Credit = d.credit > 0 ? string.Format("{0:N2}", d.credit) : "";
                    string Debit = d.debit > 0 ? string.Format("{0:N2}", d.debit) : "";

                    result.Rows.Add(d.remark, d.account, Debit, Credit);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(int month, int year, int offset)
        {
            var Query = GetReportQuery(month, year, offset);
            return Query.ToList();
        }
    }
}
