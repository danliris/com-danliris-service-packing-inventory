using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public class RecapOmzetPerMonthMonitoringViewModel
    {
        public int packingListId { get; set; }
        public DateTimeOffset truckingDate { get; set; }
        public string buyerAgentName { get; set; }
        public string buyerAgentCode { get; set; }

        public string comodity { get; set; }

        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
        public DateTimeOffset invoiceDate { get; set; }

        public string pebNo { get; set; }
        public DateTimeOffset pebDate { get; set; }

        public double quantity { get; set; }
        public string uom { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public double rate { get; set; }
        public decimal idrAmount { get; set; }
    }

    class JoinedData
    {
        public int packingListId { get; set; }
        public DateTimeOffset truckingDate { get; set; }
        public string buyerAgentName { get; set; }
        public string buyerAgentCode { get; set; }
        public string destination { get; set; }
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
        public DateTimeOffset invoiceDate { get; set; }

        public string pebNo { get; set; }
        public DateTimeOffset pebDate { get; set; }

        public IEnumerable<JoinedDataItem> items { get; set; }
    }

    class JoinedDataItem
    {
        public string comodity { get; set; }
        public double quantity { get; set; }
        public string uom { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    class CurrencyFilter
    {
        public DateTime date { get; set; }
        public string code { get; set; }
    }
}
