using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BonNo { get; set; }
        public string Shift { get; set; }
        public string CartNo { get; set; }
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string Area { get; set; }
        public string ProductionOrderType { get; set; }
        public string UOMUnit { get; set; }
        public double ProductionOrderQuantity { get; set; }
        public double QtyKg { get; set; }
    }
}
