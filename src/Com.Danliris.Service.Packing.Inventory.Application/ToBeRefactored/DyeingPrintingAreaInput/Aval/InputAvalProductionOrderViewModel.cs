using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalProductionOrderViewModel : BaseViewModel
    {
        public string AvalType { get; set; }
        public string CartNo { get; set; }
        public string UomUnit { get; set; }
        public double Quantity { get; set; }
        public double QuantityKg { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
    }
}
