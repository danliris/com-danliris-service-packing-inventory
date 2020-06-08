using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class ItemsViewModel : BaseViewModel
    {

        public string NoSPP { get; set; }
        public string MaterialName { get; set; }
        public string InputLot { get; set; }
        public double? WeightBruto { get; set; }
        public double? WeightDOS { get; set; }
        public double? WeightCone { get; set; }
        public double? WeightBale { get; set; }
        public double? GetTotal { get; set; }
    }
}