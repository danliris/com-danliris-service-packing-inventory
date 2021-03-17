using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureDetailModel : StandardEntity
    {
        public int CostStructureItemId { get; private set; }
        public string Description { get; private set; }
        public string CountryFrom { get; private set; }
        public double Percentage { get; private set; }
        public double Value { get; private set; }

        public GarmentShippingCostStructureDetailModel() { } 
        public GarmentShippingCostStructureDetailModel(string description, string countryFrom, double percentage, double value)
        {
            Description = description;
            CountryFrom = countryFrom;
            Percentage = percentage;
            Value = value;
        }
    }
}
