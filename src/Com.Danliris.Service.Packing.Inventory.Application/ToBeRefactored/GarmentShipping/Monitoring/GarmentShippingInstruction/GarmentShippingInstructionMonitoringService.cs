using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingInstruction
{
    public class GarmentShippingInstructionMonitoringService : IGarmentShippingInstructionMonitoringService
    {
        private readonly IGarmentShippingInstructionRepository repository;
        private readonly IGarmentShippingInvoiceRepository invrepository;
        private readonly IGarmentCoverLetterRepository clrepository;
        private readonly IGarmentPackingListRepository plrepository;

        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingInstructionMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingInstructionRepository>();
            invrepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            clrepository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
            plrepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentShippingInstructionMonitoringViewModel> GetData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var querySI = repository.ReadAll();
            var queryIV = invrepository.ReadAll();
            var queryCL = clrepository.ReadAll();
            var queryPL = plrepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(buyerAgent))
            {
                querySI = querySI.Where(w => w.BuyerAgentCode == buyerAgent);
            }

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            querySI = querySI.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            var newQ = (from a in querySI
                        join b in queryIV on a.InvoiceId equals b.Id
                        join c in queryPL on b.PackingListId equals c.Id
                        join d in queryCL on b.Id equals d.InvoiceId

                        select new GarmentShippingInstructionMonitoringViewModel
                        {
                            InvoiceNo = a.InvoiceNo,
                            SIDate = a.Date,
                            ForwarderCode = a.ForwarderCode,
                            ForwarderName = a.ForwarderName,
                            ShippingStaffName = a.ShippingStaffName,
                            BuyerAgentCode = a.BuyerAgentCode,
                            BuyerAgentName = a.BuyerAgentName,
                            CartonNo =  c.TotalCartons,
                            TruckingDate = a.TruckingDate,
                            PortOfDischarge = a.PortOfDischarge,
                            PlaceOfDelivery = a.PlaceOfDelivery,
                            ContainerNo = d.ContainerNo,
                            PCSQuantity = d.PCSQuantity,
                            SETSQuantity = d.SETSQuantity,
                            PACKQuantity = d.PACKQuantity,
                            GrossWeight = c.GrossWeight, 
                            NettWeight = c.NettWeight,                            
                          });
            return newQ;
        }

        public List<GarmentShippingInstructionMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.BuyerAgentCode).ThenBy(b => b.InvoiceNo);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset)
       {

            var Query = GetData(buyerAgent, dateFrom, dateTo, offset);
            DataTable result = new DataTable();
            
            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal SI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Forwarder", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staff Shipping", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Container No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Port Of Discharge", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Place Of Delivery", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Quantity PCS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity SETS", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity PACK", DataType = typeof(string) });

            result.Columns.Add(new DataColumn() { ColumnName = "Total Carton", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Gross Weight", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nett Weight", DataType = typeof(string) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string SIDate = d.SIDate == new DateTime(1970, 1, 1) ? "-" : d.SIDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));
                    string TruckDate = d.TruckingDate == new DateTime(1970, 1, 1) ? "-" : d.TruckingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("MM/dd/yyyy", new CultureInfo("us-US"));

                    string Fwdr = d.ForwarderCode + " -" + d.ForwarderName;
                    string Agent = d.BuyerAgentCode + " -" + d.BuyerAgentName;

                    string PcsQ = string.Format("{0:N2}", d.PCSQuantity);
                    string SetQ = string.Format("{0:N2}", d.SETSQuantity);
                    string PckQ = string.Format("{0:N2}", d.PACKQuantity);
                    string Ctns = string.Format("{0:N2}", d.CartonNo);
                    string GW = string.Format("{0:N2}", d.GrossWeight);
                    string NW = string.Format("{0:N2}", d.NettWeight);
                   
                    result.Rows.Add(index, d.InvoiceNo, SIDate, TruckDate, Fwdr, Agent, d.ShippingStaffName, d.ContainerNo, d.PortOfDischarge, d.PlaceOfDelivery, PcsQ, SetQ, PckQ, Ctns, GW, NW);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
