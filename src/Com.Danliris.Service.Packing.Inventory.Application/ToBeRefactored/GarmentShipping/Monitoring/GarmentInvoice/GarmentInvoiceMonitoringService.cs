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
    public class GarmentInvoiceMonitoringService : IGarmentInvoiceMonitoringService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentInvoiceMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentInvoiceMonitoringViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var queryPL = plrepository.ReadAll();
            var queryCA = carepository.ReadAll();


            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryInv = queryInv.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);
            queryInv = queryInv.OrderBy(w => w.BuyerAgentCode).ThenBy(b => b.InvoiceNo);


            var newQ = (from a in queryInv
                        join b in queryPL on a.PackingListId equals b.Id
                        join c in queryCA on a.Id equals c.InvoiceId into dd
                        from CA in dd.DefaultIfEmpty()
                        where a.IsDeleted == false && b.IsDeleted == false && CA.IsDeleted == false

                        //(string.IsNullOrWhiteSpace(invoiceType) ? true : (invoiceType == "DL" ? a.InvoiceNo.Substring(0, 2) == "DL" : a.InvoiceNo.Substring(0, 2) == "SM"))

                        select new GarmentInvoiceMonitoringViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            TruckingDate = b.TruckingDate,
                            BuyerAgentName = a.BuyerAgentCode + " - " + a.BuyerAgentName,
                            ConsigneeName = a.Consignee,
                            SailingDate = a.SailingDate,
                            PEBNo = a.PEBNo,
                            PEBDate = a.PEBDate,
                            OrderNo = a.ConfirmationOfOrderNo,
                            Origin = a.From,
                            Destination = a.To,
                            ShippingStaffName = a.ShippingStaff,
                            Amount = a.TotalAmount,
                            ToBePaid = a.AmountToBePaid,
                            CADate = CA == null ? new DateTime(1970, 1, 1) : CA.Date,
                            PaymentDate = CA == null ? new DateTime(1970, 1, 1) : CA.PaymentDate,
                            //AmountPaid = CA == null ? 0 : CA.PaymentTerm == "TT/OA" ? CA.NettNego + CA.BankCharges + CA.OtherCharge : CA.BankComission + CA.DiscrepancyFee + CA.NettNego + CA.CreditInterest + CA.BankCharges,
                            AmountPaid = CA == null ? 0 : CA.NettNego,
                        });
            return newQ;
        }

        public List<GarmentInvoiceMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Origin", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "C o n s i g n e e", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Sailing", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Order Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staff Shipping", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "A m o u n t", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "To Be Paid", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Credit Advice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Pembayaran", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Paid Amount", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string PEBDate = d.PEBDate == DateTimeOffset.MinValue ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string TruckDate = d.TruckingDate == DateTimeOffset.MinValue ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string CADate = d.CADate == new DateTime(1970, 1, 1) ? "-" : d.CADate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string PayDate = d.PaymentDate == new DateTime(1970, 1, 1) ? "-" : d.PaymentDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    string Amnt = string.Format("{0:N2}", d.Amount);
                    string TBPaid = string.Format("{0:N2}", d.ToBePaid);
                    string AmtPaid = string.Format("{0:N2}", d.AmountPaid);

                    result.Rows.Add(index, d.InvoiceNo, InvDate, TruckDate, d.Origin, d.Destination, d.BuyerAgentName, d.ConsigneeName, SailDate, d.PEBNo, PEBDate, d.OrderNo, d.ShippingStaffName, Amnt, TBPaid, CADate, PayDate, AmtPaid);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
