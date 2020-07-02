using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
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
        //private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        //private readonly IGarmentPackingListRepository plrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentInvoiceMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
        //  itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
        //  plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentInvoiceMonitoringViewModel> GetData(string buyerAgent, string invoiceType, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
       //   var queryItem = itemrepository.ReadAll();
       //   var queryPL = plrepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryInv = queryInv.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            queryInv = queryInv.Where(w => w.InvoiceDate.AddHours(offset).Date >= DateFrom.Date && w.InvoiceDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);
            
            var newQ = (from a in queryInv
                      where (string.IsNullOrWhiteSpace(invoiceType) ? true : (invoiceType == "DL" ? a.InvoiceNo.Substring(0, 2) == "DL" : a.InvoiceNo.Substring(0, 2) == "SM"))
                      
                        select new GarmentInvoiceMonitoringViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = a.InvoiceDate,
                            BuyerAgentCode = a.BuyerAgentCode,
                            BuyerAgentName = a.BuyerAgentName,
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
                          });
            return newQ;
        }

        public List<GarmentInvoiceMonitoringViewModel> GetReportData(string buyerAgent, string invoiceType, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, invoiceType, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo).ThenBy(b => b.BuyerAgentCode);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string invoiceType, DateTime? dateFrom, DateTime? dateTo, int offset)
       {

            var Query = GetData(buyerAgent, invoiceType, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Origin", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "C o n s i g n e e", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Sailing", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Order Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staff Shipping", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "A m o u n t", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "To Be Paid", DataType = typeof(string) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string PEBDate = d.PEBDate == DateTimeOffset.MinValue ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    string Amnt = string.Format("{0:N2}", d.Amount);
                    string TBPaid = string.Format("{0:N2}", d.ToBePaid);
                        
                    result.Rows.Add(index, d.InvoiceNo, InvDate, d.Origin, d.Destination, d.BuyerAgentCode, d.BuyerAgentName, d.ConsigneeName, SailDate, d.PEBNo, PEBDate, d.OrderNo, d.ShippingStaffName, Amnt, TBPaid);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
