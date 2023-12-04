using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport
{
    public class GarmentDetailOmzetByUnitReportViewModel
    {
        public string Urutan { get; set; }
        public string InvoiceNo { get; set; }
        public string BuyerAgentName { get; set; }
        public string ComodityName { get; set; }
        public string ArticleStyle { get; set; }
        public string UnitCode { get; set; }
        public string ExpenditureGoodNo { get; set; }
        public string RONumber { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double QuantityInPCS { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public double AmountIDR { get; set; }
    }

    public class GarmentDetailOmzetByUnitReportTempViewModel
    {
        public int PLId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
    }

    class CurrencyFilter
    {
        public DateTime date { get; set; }
        public string code { get; set; }
    }

    class RONumberFilter
    {
        public string RONo { get; set; }
    }

}
