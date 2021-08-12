using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
    public class ShippingPackingListViewModel
    {
        public int InvoiceId { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public DateTimeOffset Date { get; set; }
        public string InvoiceNo { get; set; }
    }
}