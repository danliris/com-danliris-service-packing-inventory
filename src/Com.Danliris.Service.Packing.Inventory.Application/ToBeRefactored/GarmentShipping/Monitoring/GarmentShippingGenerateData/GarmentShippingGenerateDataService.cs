using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
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
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingGenerateData
{
    public class GarmentShippingGenerateDataService : IGarmentShippingGenerateDataService
    {
        private readonly IGarmentShippingInvoiceRepository repository;
        private readonly IGarmentPackingListRepository plrepository;
        
        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingGenerateDataService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentShippingGenerateDataViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var queryPL = plrepository.ReadAll();
   
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);

            var Query = from a in queryPL
                        join b in queryInv on a.Id equals b.PackingListId 
     
                        select new GarmentShippingGenerateDataViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = b.InvoiceDate,
                            TruckingDate = a.TruckingDate,
                            DueDate = b.SailingDate.AddDays(b.PaymentDue),
                            BuyerCode = a.BuyerAgentCode,
                            BuyerName = a.BuyerAgentName,
                            SCNo = "-",
                            Destination = a.Destination,
                            SailingDate = b.SailingDate,
                            CurrencyCode = "USD",
                            ComodityName = b.Description,
                            PEBNo = b.PEBNo,
                            PEBDate = b.PEBDate,
                            Def = a.Omzet == false ? "TIDAK" : "YA",
                            Acc = a.Accounting == false ? "TIDAK" : "YA",
                            Amount = b.TotalAmount,
                            ToBePaid = b.AmountToBePaid,
                        };
            return Query;
        }

        public List<GarmentShippingGenerateDataViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl J/T", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No S/C", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Export", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mata Uang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal PEB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Omzet", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Accounting", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "To Be Paid", DataType = typeof(decimal) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0);
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string PEBDate = d.PEBDate == DateTimeOffset.MinValue ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string TruckDate = d.TruckingDate == DateTimeOffset.MinValue ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string DueDate = d.DueDate == DateTimeOffset.MinValue ? "-" : d.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string MtUang = "USD";
                    //string Amnt = string.Format("{0:N2}", d.Amount);
                    //string TBPaid = string.Format("{0:N2}", d.ToBePaid);

                    result.Rows.Add(index, d.InvoiceNo, InvDate, TruckDate, DueDate, d.BuyerCode, d.BuyerName, d.SCNo, d.Destination, SailDate,
                                    MtUang, d.ComodityName, d.PEBNo, PEBDate, d.Def, d.Acc, d.Amount, d.ToBePaid);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
