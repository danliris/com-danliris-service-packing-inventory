using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditMonitoringService : IGarmentLetterOfCreditMonitoringService
    {
        private readonly IGarmentLetterOfCreditRepository lcrepository;
        private readonly IGarmentShippingInvoiceRepository invrepository;
        private readonly IGarmentPackingListRepository plrepository;
       
        private readonly IIdentityProvider _identityProvider;

        public GarmentLetterOfCreditMonitoringService(IServiceProvider serviceProvider)
        {
            lcrepository = serviceProvider.GetService<IGarmentLetterOfCreditRepository>();
            invrepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentLetterOfCreditMonitoringViewModel> GetData(string buyerAgent, string lcNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryLC = lcrepository.ReadAll();
            var queryInv = invrepository.ReadAll();
            var queryPL = plrepository.ReadAll();
            
            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryLC = queryLC.Where(w => w.ApplicantCode == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(lcNo))
            {
                queryLC = queryLC.Where(w => w.DocumentCreditNo == lcNo);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            queryLC = queryLC.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            queryLC = queryLC.OrderBy(w => w.ApplicantCode);

            var Query = (from a in queryLC
                        join b in queryInv on a.ApplicantId equals b.BuyerAgentId 
                        join c in queryPL on b.PackingListId equals c.Id
                        where a.IsDeleted == false && c.IsDeleted == false && c.IsDeleted == false
                              && c.PaymentTerm == "LC" && a.DocumentCreditNo == c.LCNo 

                        select new GarmentLetterOfCreditMonitoringViewModel
                        {
                            LCNo = a.DocumentCreditNo,
                            LCDate = a.Date,
                            IssuedBank = a.IssuedBank,
                            ApplicantName = a.ApplicantCode + " - " + a.ApplicantName,
                            InvoiceNo = c.InvoiceNo,
                            TruckingDate = c.TruckingDate,
                            ExpiredDate = a.ExpireDate,
                            ExpiredPlace = a.ExpirePlace,
                            LatestShipment = a.LatestShipment,
                            LCCondition = a.LCCondition,
                            AmountToBePaid = b.AmountToBePaid,
                            Quantity = a.Quantity,
                            UomUnit = a.UomUnit,
                            AmountLC = a.TotalAmount,
                        });
            return Query;
        }

        public List<GarmentLetterOfCreditMonitoringViewModel> GetReportData(string buyerAgent, string lcNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, lcNo, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.LCNo).ThenBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string lcNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {

            var Query = GetData(buyerAgent, lcNo, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No L/C", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl L/C", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Issued Bank", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Applicant", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Expired Date", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Expired Place", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Latest Shipment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "LC Condition", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount To Be Paid", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount L/C", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string LCDate = d.LCDate == new DateTime(1970, 1, 1) ? "-" : d.LCDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string EDate = d.ExpiredDate == new DateTime(1970, 1, 1) ? "-" : d.ExpiredDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string ShipDate = d.LatestShipment == DateTimeOffset.MinValue ? "-" : d.LatestShipment.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string TruckDate = d.TruckingDate == DateTimeOffset.MinValue ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    
                    string Qty = string.Format("{0:N2}", d.Quantity);
                    string TBPaid = string.Format("{0:N2}", d.AmountToBePaid);
                    string AmtLC = string.Format("{0:N2}", d.AmountLC);

                    result.Rows.Add(index, d.LCNo, LCDate, d.IssuedBank, d.ApplicantName, EDate, d.ExpiredPlace, ShipDate, d.LCCondition, d.InvoiceNo, TBPaid, TruckDate, Qty, d.UomUnit, AmtLC);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
