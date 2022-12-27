using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearMarketing
{
    public class OmzetYearMarketingViewModel
    {
        public string marketingName { get; set; }      
        public double pcsQuantity { get; set; }
        public double setsQuantity { get; set; }
        public double packsQuantity { get; set; }
        public decimal amount { get; set; }
    }

    public class OmzetYearMarketingTempViewModel
    {
        public string marketingName { get; set; }
        public double quantity { get; set; }
        public string uomunit { get; set; }
        public decimal amount { get; set; }
    }
}
