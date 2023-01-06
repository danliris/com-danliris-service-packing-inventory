using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearCountry
{
    public class OmzetYearCountryViewModel
    {
        public decimal totalAmount { get; set; }
        public List<OmzetYearCountryItemViewModel> Items { get; set; }
    }

    public class OmzetYearCountryItemViewModel
    {
        public string country { get; set; }
        public double pcsQuantity { get; set; }
        public double setsQuantity { get; set; }
        public double packsQuantity { get; set; }
        public decimal amount { get; set; }
        public decimal percentage { get; set; }
    }
}
