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
        private readonly IGarmentShippingInvoiceItemRepository itemrepository;
        private readonly IGarmentPackingListRepository plrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingGenerateDataService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingInvoiceItemRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentShippingGenerateDataViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryInv = repository.ReadAll();
            var queryItem = itemrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            queryPL = queryPL.Where(w => w.TruckingDate.AddHours(offset).Date >= DateFrom.Date && w.TruckingDate.AddHours(offset).Date <= DateTo.Date);

            queryInv = queryInv.OrderBy(w => w.InvoiceNo);

            var Query = from a in queryPL
                        join b in queryInv on a.Id equals b.PackingListId
                        join c in queryItem on b.Id equals c.GarmentShippingInvoiceId

                        select new GarmentShippingGenerateDataViewModel
                        {
                            //c.Quantity, c.UomUnit, c.CurrencyCode, c.Price, c.CMTPrice, c.Amount

                            InvoiceNo = a.InvoiceNo,
                            InvoiceDate = b.InvoiceDate,
                            TruckingDate = a.TruckingDate,
                            DueDate = b.SailingDate.AddDays(b.PaymentDue),
                            PaymentTerm = a.PaymentTerm,
                            LCNo = a.LCNo,
                            BuyerCode = a.BuyerAgentCode,
                            BuyerName = a.BuyerAgentName,
                            RONo = c.RONo,
                            SCNo = c.SCNo,
                            Destination = a.Destination,
                            SailingDate = b.SailingDate,
                            CurrencyCode = "USD",
                            ComodityCode = c.ComodityCode,
                            ComodityName = c.ComodityDesc,
                            PEBNo = b.PEBNo == null ? "-" : b.PEBNo,
                            PEBDate = b.PEBNo == null ? new DateTime(1970, 1, 1) : b.PEBDate,
                            Def = a.Omzet == false ? "TIDAK" : "YA",
                            Acc = a.Accounting == false ? "TIDAK" : "YA",
                            Amount = b.TotalAmount,
                            ToBePaid = b.AmountToBePaid,
                            Quantity = c.Quantity,
                            UomUnit = c.UomUnit,
                            Price = c.Price,
                            CMTPrice = c.CMTPrice,
                            SubAmount = c.Amount,
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
            result.Columns.Add(new DataColumn() { ColumnName = "Payment Term", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LC No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No R/O", DataType = typeof(string) });
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
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga FOB", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga CMT", DataType = typeof(decimal) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sub Total", DataType = typeof(decimal) });


            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, "", 0, 0, 0);
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string InvDate = d.InvoiceDate == new DateTime(1970, 1, 1) ? "-" : d.InvoiceDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string SailDate = d.SailingDate == new DateTime(1970, 1, 1) ? "-" : d.SailingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string PEBDate = d.PEBDate == new DateTime(1970, 1, 1) ? "-" : d.PEBDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string TruckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string DueDate = d.DueDate == new DateTime(1970, 1, 1) ? "-" : d.DueDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd-MM-yyyy", new CultureInfo("id-ID"));
                    string MtUang = "USD";

                    result.Rows.Add(index, d.InvoiceNo, InvDate, TruckDate, DueDate, d.PaymentTerm, d.LCNo, d.BuyerCode, d.BuyerName, d.RONo, d.SCNo, d.Destination, SailDate,
                                    MtUang, d.ComodityName, d.PEBNo, PEBDate, d.Def, d.Acc, d.Amount, d.ToBePaid, d.Quantity, d.UomUnit, d.Price, d.CMTPrice, d.SubAmount);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
