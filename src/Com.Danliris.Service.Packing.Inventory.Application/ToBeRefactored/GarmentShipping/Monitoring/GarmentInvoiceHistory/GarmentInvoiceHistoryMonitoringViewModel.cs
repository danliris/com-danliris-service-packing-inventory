using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoiceHistory
{
    public class GarmentInvoiceHistoryMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset PLDate { get; set; }      
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string BuyerAgentName { get; set; }
        public string ConsigneeName { get; set; }
        public string Destination { get; set; }
        public string SectionCode { get; set; }
        public string PaymentTerm { get; set; }
        public string LCNo { get; set; }
        public string PEBNo { get; set; }
        public string ShippingStaff { get; set; }
        public string Status { get; set; }
        public DateTimeOffset PEBDate { get; set; }                
        public string SIDate { get; set; }
        public string CLDate { get; set; }
        public string CADate { get; set; }
        public string PaymentDate { get; set; }
    }
}
