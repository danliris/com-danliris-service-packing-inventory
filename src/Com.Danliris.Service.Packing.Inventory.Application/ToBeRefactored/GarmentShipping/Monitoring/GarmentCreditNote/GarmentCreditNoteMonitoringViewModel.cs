using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditNote
{
    public class GarmentCreditNoteMonitoringViewModel
    {
        public string CNNo { get; set; }
        public DateTimeOffset CNDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
    }
}
