using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.TransitInput
{
    public class TransitInputViewModel : BaseViewModel
    {
        public int InspectionAreaId { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string ProductionOrderNo { get; set; }
        public string CartNo { get; set; }
        public string Shift { get; set; }
        public string Unit { get; set; }
        public string Material { get; set; }
        public string MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UOMUnit { get; set; }
        public decimal Balance { get; set; }
        public string Remark { get; set; }
    }
}
