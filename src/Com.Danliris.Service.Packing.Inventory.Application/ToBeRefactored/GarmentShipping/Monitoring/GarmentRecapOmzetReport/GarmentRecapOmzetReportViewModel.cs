using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport
{
    public class GarmentRecapOmzetReportViewModel
    {
        public string Omzet { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public string Destination { get; set; }
        public string ComodityName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string PEBNo { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountIDR { get; set; }
    }

    class CurrencyFilter
    {
        public DateTime date { get; set; }
        public string code { get; set; }
    }
}
