using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByBrand
{
    public class GarmentOmzetMonthlyByBrandViewModel
    {
        public string BrandName { get; set; }
        public List<GarmentOmzetMonthlyByBuyerBrandViewModel> Units { get; set; }
    }

    public class GarmentOmzetMonthlyByBuyerBrandViewModel
    {
        public string UnitCode { get; set; }
        public double Quantities { get; set; }
        public decimal Amounts { get; set; }

        public List<GarmentOmzetMonthlyByBrandDetailViewModel> Details { get; set; }
    }

    public class GarmentOmzetMonthlyByBrandDetailViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string RO_Number { get; set; }
        public string ComodityName { get; set; }
        public string ComodityDesc { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
        public string UOMUnit { get; set; }
    }

    public class GarmentOmzetMonthlyByBrandListViewModel
    {
        public string UnitCode { get; set; }
        public string BrandName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string RO_Number { get; set; }
        public string ComodityName { get; set; }
        public string ComodityDesc { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
        public string UOMUnit { get; set; }
    }
}
