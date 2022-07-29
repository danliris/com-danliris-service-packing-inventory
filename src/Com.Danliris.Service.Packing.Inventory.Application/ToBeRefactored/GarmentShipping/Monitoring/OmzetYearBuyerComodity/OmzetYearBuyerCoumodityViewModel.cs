﻿using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyerComodity
{
    public class OmzetYearBuyerComodityViewModel
    {
        public string buyerName { get; set; }
        public string comodityName { get; set; }
        public double pcsQuantity { get; set; }
        public double setsQuantity { get; set; }
        public decimal amount { get; set; }
    }

    public class OmzetYearBuyerComodityTempViewModel
    {
        public string buyerName { get; set; }
        public string comodityName { get; set; }
        public double quantity { get; set; }
        public string uomunit { get; set; }
        public decimal amount { get; set; }
    }
}
