using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public class GarmentInvoiceMonitoringViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public string ConsigneeName { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public string OrderNo { get; set; }
        public string ShippingStaffName { get; set; }
        public decimal Amount { get; set; }
        public decimal ToBePaid { get; set; }

        //public string RONo { get; set; }
        //public string SCNo { get; set; }
        //public string BuyerBrandName { get; set; }
        //public string Description { get; set; }
        //public double Quantity { get; set; }
        //public string UOMUnit { get; set; }
        //public string CurrencyCode { get; set; }
        //public decimal Price { get; set; }
        //public decimal Amount { get; set; }
        //public decimal CMTPrice { get; set; }
        //public decimal AmountCMT { get; set; }
        //public decimal LessFabCost { get; set; }

    }
}
