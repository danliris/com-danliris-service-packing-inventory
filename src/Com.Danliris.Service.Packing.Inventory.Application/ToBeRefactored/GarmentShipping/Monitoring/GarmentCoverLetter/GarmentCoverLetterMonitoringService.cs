using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCoverLetter
{
    public class GarmentCoverLetterMonitoringService : IGarmentCoverLetterMonitoringService
    {
        private readonly IGarmentCoverLetterRepository clrepository;
  
        private readonly IIdentityProvider _identityProvider;

        public GarmentCoverLetterMonitoringService(IServiceProvider serviceProvider)
        {
            clrepository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentCoverLetterMonitoringViewModel> GetData(string buyerAgent, string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var queryCL = clrepository.ReadAll();
           
            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                queryCL = queryCL.Where(w => w.OrderCode == buyerAgent);
            }

            if (!string.IsNullOrWhiteSpace(emkl))
            {
                queryCL = queryCL.Where(w => w.EMKLCode == emkl);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            queryCL = queryCL.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            var newQ = (from a in queryCL 

                        select new GarmentCoverLetterMonitoringViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            CLDate = a.Date,
                            BookingDate = a.BookingDate,
                            ExportDate = a.ExportEstimationDate,
                            EMKLName = a.Name,
                            ForwarderName = a.ForwarderName,
                            ShippingStaffName = a.ShippingStaffName,
                            BuyerAgentCode = a.OrderCode,
                            BuyerAgentName = a.OrderName,
                            Destination = a.Destination,
                            Address = a.Address,
                            PIC = a.PIC,
                            ATTN = a.ATTN,
                            Phone = a.Phone,
                            TotalCarton = a.CartoonQuantity,
                            ContainerNo = a.ContainerNo,
                            ShippingSeal = a.ShippingSeal,
                            DLSeal = a.DLSeal,
                            PCSQuantity = a.PCSQuantity,
                            SETSQuantity = a.SETSQuantity,
                            PACKQuantity = a.PACKQuantity,
                            Truck = a.Truck,
                            PlateNumber = a.PlateNumber,
                            DriverName = a.Driver,
                            UnitName = a.Unit,
                        });
            return newQ;
        }

        public List<GarmentCoverLetterMonitoringViewModel> GetReportData(string buyerAgent, string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, emkl, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.BuyerAgentCode).ThenBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, string emkl, DateTime? dateFrom, DateTime? dateTo, int offset)
        {            
            var Query = GetData(buyerAgent, emkl, dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal SP", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Estimasi Export", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Trucking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Forwarder", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staff Shipping", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Tujuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Alamat", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "PIC", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "ATTN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Phone", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Container No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipping Seal", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Dan Liris Seal", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total Karton", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity PCS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity SETS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity PACK", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Truck", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Polisi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Driver", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Unit", DataType = typeof(string) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string CLDate = d.CLDate == new DateTime(1970, 1, 1) ? "-" : d.CLDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string EstDate = d.ExportDate == new DateTime(1970, 1, 1) ? "-" : d.ExportDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string BookDate = d.BookingDate == new DateTime(1970, 1, 1) ? "-" : d.BookingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string Agent = d.BuyerAgentCode + " -" + d.BuyerAgentName;

                    string PcsQ = string.Format("{0:N2}", d.PCSQuantity);
                    string SetQ = string.Format("{0:N2}", d.SETSQuantity);
                    string PckQ = string.Format("{0:N2}", d.PACKQuantity);
                    string CTNS = string.Format("{0:N2}", d.TotalCarton);
                    
                    result.Rows.Add(index, d.InvoiceNo, CLDate, BookDate, EstDate, d.EMKLName, d.ForwarderName, d.ShippingStaffName, Agent, d.Destination, d.Address,
                                    d.PIC, d.ATTN, d.Phone, d.ContainerNo, d.ShippingSeal, d.DLSeal, CTNS, PcsQ, SetQ, PckQ, d.Truck, d.PlateNumber, d.DriverName, d.UnitName);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
