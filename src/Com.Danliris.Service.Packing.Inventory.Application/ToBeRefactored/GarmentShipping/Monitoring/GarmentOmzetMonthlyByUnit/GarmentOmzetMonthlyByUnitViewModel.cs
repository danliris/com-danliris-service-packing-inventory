using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByUnit
{
    public class GarmentOmzetMonthlyByUnitViewModel
    {
        public string UnitCode { get; set; }
        public List<GarmentOmzetMonthlyByUnitBuyerViewModel> Buyers { get; set; }
    }

    public class GarmentOmzetMonthlyByUnitBuyerViewModel
    {
        public string Buyer { get; set; }
        public double Quantities { get; set; }
        public decimal Amounts { get; set; }

        public List<GarmentOmzetMonthlyByUnitDetailViewModel> Details { get; set; }
    }

    public class GarmentOmzetMonthlyByUnitDetailViewModel
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

    public class GarmentOmzetMonthlyByUnitListViewModel
    {
        public string UnitCode { get; set; }
        public string BuyerName { get; set; }
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
