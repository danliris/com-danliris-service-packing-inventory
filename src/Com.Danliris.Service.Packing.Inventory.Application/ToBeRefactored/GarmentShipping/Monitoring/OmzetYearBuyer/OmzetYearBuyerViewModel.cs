using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyer
{
    public class OmzetYearBuyerViewModel
    {
        public decimal totalAmount { get; set; }
        public List<OmzetYearBuyerItemViewModel> Items { get; set; }
    }

    public class OmzetYearBuyerItemViewModel
    {
        public string buyer { get; set; }
        public double pcsQuantity { get; set; }
        public double setsQuantity { get; set; }
        public decimal amount { get; set; }
        public decimal percentage { get; set; }
    }
}
