using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Buyer { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string ProductionOrderNo { get; set; }
        public long ProductionOrderId { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderQuantity { get; set; }
        public string CartNo { get; set; }
        public string MaterialName { get; set; }
        public string MaterialConstructionName { get; set; }
        public string MaterialWidth { get; set; }
        public string UnitName { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string Mutation { get; set; }
        public double MeterLength { get; set; }
        public double YardsLength { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public string UomUnit { get; set; }
    }
}
