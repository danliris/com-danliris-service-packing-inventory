using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice
{
    public class IndexViewModel
    {
        public int id { get; set; }

        public string invoiceNo { get; set; }

        public DateTimeOffset? date { get; set; }
        public double amount { get; set; }
        public double lessFabricCost { get; set; }

        public double dhlCharges { get; set; }

        public double amountToBePaid { get; set; }

        public string buyerName { get; set; }
        public string bankAccountName { get; set; }
    }
}