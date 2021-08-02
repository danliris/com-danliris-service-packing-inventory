using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public class GarmentInvoiceMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public DateTimeOffset CADate { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string BuyerAgentName { get; set; }
        public string ConsigneeName { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public string OrderNo { get; set; }
        public string ShippingStaffName { get; set; }
        public decimal Amount { get; set; }
        public decimal ToBePaid { get; set; }
        public double AmountPaid { get; set; }
    }
}
