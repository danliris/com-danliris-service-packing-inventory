using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalItemViewModel : BaseViewModel
    {
        public int AvalItemId { get; set; }
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string AvalUomUnit { get; set; }
        public double AvalQuantity { get; set; }
        public double AvalQuantityKg { get; set; }
        public double AvalOutSatuan { get; set; }
        public double AvalOutQuantity { get; set; }
        public string DeliveryNote { get; set; }
        public string AdjDocumentNo { get; set; }
    }
}
