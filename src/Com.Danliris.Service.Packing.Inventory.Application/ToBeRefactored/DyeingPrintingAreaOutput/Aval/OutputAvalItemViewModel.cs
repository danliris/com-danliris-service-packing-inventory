using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalItemViewModel : BaseViewModel
    {
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string UomUnit { get; set; }
        public double Quantity { get; set; }
        public double QuantityKg { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
    }
}
