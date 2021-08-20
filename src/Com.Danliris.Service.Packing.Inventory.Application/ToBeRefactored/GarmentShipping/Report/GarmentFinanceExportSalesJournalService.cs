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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceExportSalesJournalService : IGarmentFinanceExportSalesJournalService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentFinanceExportSalesJournalService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public List<GarmentFinanceExportSalesJournalViewModel> GetReportQuery(int month, int year, int offset)
        {

            DateTime dateFrom = new DateTime(year, month, 1);
            int nextYear = month == 12 ? year + 1 : year;
            int nextMonth = month == 12 ? 1 : month + 1;
            DateTime dateTo = new DateTime(nextYear, nextMonth + 1, 1);
            List<GarmentFinanceExportSalesJournalViewModel> data = new List<GarmentFinanceExportSalesJournalViewModel>();
            
            var queryInv = repository.ReadAll();
            var queryPL = plrepository.ReadAll()
                .Where(w => w.TruckingDate.AddHours(offset).Date >= dateFrom && w.TruckingDate.AddHours(offset).Date < dateTo.Date
                    && (w.InvoiceType == "DL" || w.InvoiceType == "DS" || w.InvoiceType == "DLR" || w.InvoiceType == "SMR"));
            
            var join = from a in queryInv
                       join b in queryPL on a.PackingListId equals b.Id
                       where a.IsDeleted == false && b.IsDeleted == false
                       select new GarmentFinanceExportSalesJournalViewModel
                       {
                           remark= b.InvoiceType== "DL" || b.InvoiceType == "DS" ? "       PNJ. BR. JADI EXPORT LANGSUNG" : "       PNJ. LAIN-LAIN EXPORT LANGSUNG",
                           credit= a.TotalAmount,
                           debit= 0,
                           account= b.InvoiceType == "DL" || b.InvoiceType == "DS" ? "5024.00.4.00" : "5026.00.4.00"
                       };

            var debit = new GarmentFinanceExportSalesJournalViewModel
            {
                remark = "PIUTANG USAHA EXPORT",
                credit = 0,
                debit = join.Sum(a => a.credit),
                account = "1103.00.5.00"
            };
            data.Add(debit);

            var sumquery = join.ToList()
                   .GroupBy(x => new { x.remark, x.account }, (key, group) => new
                   {
                       Remark = key.remark,
                       Account = key.account,
                       Credit = group.Sum(s => s.credit)
                   }).OrderBy(a=>a.Remark);
            foreach(var item in sumquery)
            {
                var obj = new GarmentFinanceExportSalesJournalViewModel
                {
                    remark = item.Remark,
                    credit = item.Credit,
                    debit = 0,
                    account = item.Account
                };

                data.Add(obj);
            }

            var total= new GarmentFinanceExportSalesJournalViewModel
            {
                remark = "",
                credit = join.Sum(a => a.credit),
                debit = join.Sum(a => a.credit),
                account = ""
            };
            data.Add(total);
            return data;
        }

        public List<GarmentFinanceExportSalesJournalViewModel> GetReportData(int month, int year, int offset)
        {
            var Query = GetReportQuery(month, year, offset);
            return Query.ToList();
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
    }
}
