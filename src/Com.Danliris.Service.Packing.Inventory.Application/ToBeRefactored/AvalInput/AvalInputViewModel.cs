using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput
{
    public class AvalInputViewModel : BaseViewModel
    {
        public string BonNo { get; set; }
        public string CartNo { get; set; }
        public string Unit { get; set; }
        public string Area { get; set; }
        public string ProductionOrderType { get; set; }
        public string Shift { get; set; }
        public string UOMUnit { get; set; }
        public double ProductionOrderQuantity { get; set; }
        public double QtyKg { get; set; }
    }
}