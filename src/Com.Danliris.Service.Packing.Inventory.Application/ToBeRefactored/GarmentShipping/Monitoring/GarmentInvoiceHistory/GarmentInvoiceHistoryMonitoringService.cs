using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoiceHistory
{
    public class GarmentInvoiceHistoryMonitoringService : IGarmentInvoiceHistoryMonitoringService
    {
        private readonly IGarmentPackingListRepository plrepository;
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentShippingInstructionRepository sirepository;
        private readonly IGarmentCoverLetterRepository clrepository;
        private readonly IGarmentShippingCreditAdviceRepository carepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentInvoiceHistoryMonitoringService(IServiceProvider serviceProvider)
        {
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            sirepository = serviceProvider.GetService<IGarmentShippingInstructionRepository>();
            clrepository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
            carepository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentInvoiceHistoryMonitoringViewModel> GetData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryPL = plrepository.ReadAll();
            var queryInv = repository.ReadAll();
            var querySI = sirepository.ReadAll();
            var queryCL = clrepository.ReadAll();
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
            
            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);

            var NewQuery = (from a in queryPL
                            join b in queryInv on a.Id equals b.PackingListId
                            join c in querySI on b.Id equals c.InvoiceId into aa
                            from SI in aa.DefaultIfEmpty()
                            join d in queryCL on SI.InvoiceId equals d.InvoiceId into bb
                            from CL in bb.DefaultIfEmpty()
                            join e in queryCA on CL.InvoiceId equals e.InvoiceId into cc
                            from CA in cc.DefaultIfEmpty()

                            select new GarmentInvoiceHistoryMonitoringViewModel
                            {
                            InvoiceNo = a.InvoiceNo,
                            PLDate = a.Date,
                            InvoiceDate = b.InvoiceDate,
                            TruckingDate = a.TruckingDate,
                            BuyerAgentName = b.BuyerAgentCode + " - " + b.BuyerAgentName,
                            ConsigneeName = b.Consignee,
                            SectionCode = a.SectionCode,
                            Destination = a.Destination,
                            PaymentTerm = a.PaymentTerm,
                            LCNo = a.PaymentTerm=="LC" ? a.LCNo : "-",
                            ShippingStaff = a.ShippingStaffName,
                            Status = a.Status.ToString(),
                            PEBNo = b.PEBNo,
                            PEBDate = b.PEBDate,
                            SIDate =  SI == null ? "-" : SI.Date.Day.ToString()+"-"+ SI.Date.Month.ToString()+"-"+SI.Date.Year.ToString(),
                            CLDate = CL == null ? "-" : CL.Date.Day.ToString() + "-" + CL.Date.Month.ToString() + "-" + CL.Date.Year.ToString(),
                            CADate = CA == null ? "-" : CA.Date.Day.ToString() + "-" + CA.Date.Month.ToString() + "-" + CA.Date.Year.ToString(),
                            PaymentDate = CA == null ? "-" : CA.PaymentDate.Day.ToString() + "-" + CA.PaymentDate.Month.ToString() + "-" + CA.PaymentDate.Year.ToString(),
                            });         
            return NewQuery;
        }

        public List<GarmentInvoiceHistoryMonitoringViewModel> GetReportData(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, invoiceNo, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Packing List", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Consignee", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Payment Term", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No L/C", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Shipping Instruction", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Surat Pengantar", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Credit Advice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Payment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staf Shipping", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {        
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string PLDate = d.PLDate == new DateTime(1970, 1, 1) ? "-" : d.PLDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string TruckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string PEBDate = d.PEBDate == new DateTime(1970, 1, 1) ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                        
                    result.Rows.Add(index, d.InvoiceNo, PLDate, InvDate, TruckDate, d.BuyerAgentName, d.ConsigneeName, d.Destination, d.SectionCode, d.PaymentTerm, d.LCNo, d.PEBNo, PEBDate, d.SIDate, d.CLDate, d.CADate, d.PaymentDate, d.ShippingStaff, d.Status);
                }

            }          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
