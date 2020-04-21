using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput
{
    public class ShipmentIndexViewModel
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string ProductionOrderNo { get; set; }
        public string BuyerName { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string Grade { get; set; }
        public double PackingQTY { get; set; }
        public string PackingUom { get; set; }
        public decimal PackingBalance { get; set; }
        public string UomUnit { get; set; }
        public double MeterLength { get; set; }
        public double YardsLength { get; set; }
    }
}
