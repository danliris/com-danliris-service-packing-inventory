using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice
{
    public class IndexViewModel
    {
        public int id { get; set; }
        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public double amount { get; set; }
        public double paidAmount { get; set; }
        public string buyerName { get; set; }
        public string bankAccountName { get; set; }
    }
}
