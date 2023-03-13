using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public class GarmentBuyerReceivablesReportService : IGarmentBuyerReceivablesReportService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;      
        private readonly IIdentityProvider _identityProvider;

        public GarmentBuyerReceivablesReportService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();            
        }

        public IQueryable<GarmentBuyerReceivablesReportViewModel> GetData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var queryItm = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();
            var queryCA = carepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryInv = queryInv.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(invoiceNo))
            {
                queryInv = queryInv.Where(w => w.InvoiceNo == invoiceNo);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryInv = queryInv.Where(w => w.SailingDate.AddHours(offset).Date >= DateFrom.Date && w.SailingDate.AddHours(offset).Date <= DateTo.Date);
            
            queryPL = queryPL.Where(w => w.Omzet == true);
            
            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);

            var newQ = (from a in queryInv
                        join c in queryPL on a.PackingListId equals c.Id
                        join b in queryItm on a.Id equals b.GarmentShippingInvoiceId
                        where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false 
                
                        select new GarmentBuyerReceivablesReportTempViewModel
                        {
                            InvoiceId = a.Id,
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            BuyerAgentName = a.BuyerAgentName,
                            BuyerBrandName = b.BuyerBrandName,
                            ToBePaid = a.AmountToBePaid,
                            SailingDate = a.SailingDate,
                            PaymentDue = a.PaymentDue,
                            DueDate = a.SailingDate.AddDays(a.PaymentDue),
                        }).Distinct();
            
            var newQuery = (from a in newQ
                            join d in queryCA on a.InvoiceId equals d.InvoiceId into dd
                            from CA in dd.DefaultIfEmpty()
                            where CA.IsDeleted == false 

                        select new GarmentBuyerReceivablesReportViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            BuyerAgentName = a.BuyerAgentName,
                            BuyerBrandName = a.BuyerBrandName,
                            ToBePaid = a.ToBePaid,
                            SailingDate = a.SailingDate,
                            PaymentDue = a.PaymentDue,
                            DueDate = a.SailingDate.AddDays(a.PaymentDue),
                            PaymentDate = CA == null ? new DateTime(1970, 1, 1) : CA.PaymentDate,
                            PaymentAmount = CA == null ? 0 : CA.AmountPaid,
                            BankCharges = CA == null ? 0 : CA.BankCharges,
                            OtherCharges = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? CA.OtherCharge : (CA.BankComission + CA.DiscrepancyFee + CA.CreditInterest),
                            ReceiptAmount = CA == null ? 0 : CA.AmountPaid - (CA.BankCharges + CA.OtherCharge),
                            OutStandingAmount = CA == null ? Convert.ToDouble(a.ToBePaid) : CA.BalanceAmount,
                            BankDetail = CA == null ? "-" : CA.BankAccountName,
                            ReceiptNo = CA == null ? "-" : CA.ReceiptNo,
                            OverDue = CA == null ? 0 : (CA.PaymentDate.ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date - a.SailingDate.AddDays(a.PaymentDue).ToOffset(TimeSpan.FromHours(_identityProvider.TimezoneOffset)).Date).Days,
                        });
            return newQuery;
        }

        public List<GarmentBuyerReceivablesReportViewModel> GetReportData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo).ThenBy(b => b.PaymentDate);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "NO INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TGL INVOICE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER AGENT", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER BRAND", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT TO BE PAID (US$)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ETD DATE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "TEMPO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "DUE DATE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PAYMENT DATE", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PAYMENT AMOUNT (US$)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK CHARGES", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "OTHER CHARGES", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "AMOUNT RECEIPT (US$)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "OUTSTANDING AMOUNT (US$)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BANK DETAILS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "RECEIPT NO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "OVERDUE", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string JTDate = d.DueDate == new DateTime(1970, 1, 1) ? "-" : d.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PayDate = d.PaymentDate == new DateTime(1970, 1, 1) ? "-" : d.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string TBPaid = string.Format("{0:N2}", d.ToBePaid);
                    string AmtPaid = string.Format("{0:N2}", d.PaymentAmount);
                    string BCharges = string.Format("{0:N2}", d.BankCharges);
                    string OCharges = string.Format("{0:N2}", d.OtherCharges);
                    string RcptAmt = string.Format("{0:N2}", d.ReceiptAmount);
                    string OutStdg = string.Format("{0:N2}", d.OutStandingAmount);

                    result.Rows.Add(index, d.InvoiceNo, InvDate, d.BuyerAgentName, d.BuyerBrandName, TBPaid, SailDate, d.PaymentDue, JTDate, PayDate, AmtPaid, BCharges, OCharges, RcptAmt, OutStdg, d.BankDetail, d.ReceiptNo, d.OverDue);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
