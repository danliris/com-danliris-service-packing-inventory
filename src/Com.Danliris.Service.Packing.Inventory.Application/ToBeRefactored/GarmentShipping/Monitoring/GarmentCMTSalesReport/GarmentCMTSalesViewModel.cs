using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport
{
    public class GarmentCMTSalesViewModel
    {
        public string InvoiceNo { get; set; }
        public string Ronos { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public string BuyerAgentName { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public decimal FOB { get; set; }
        public decimal FOBIdr { get; set; }
        public decimal FAB { get; set; }
        public decimal FABIdr { get; set; }
        public decimal ToBePaid { get; set; }
        public decimal ToBePaidIdr { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public double Quantity { get; set; }
    }
}
