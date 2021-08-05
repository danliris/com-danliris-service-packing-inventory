using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote
{
    public class GarmentDebitNoteMonitoringViewModel
    {
        public string DNNo { get; set; }
        public DateTimeOffset DNDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
    }
}
