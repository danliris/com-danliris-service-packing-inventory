using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalItemViewModel : BaseViewModel
    {
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string AvalUomUnit { get; set; }
        public double AvalQuantity { get; set; }
        public double AvalQuantityKg { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
    }
}
