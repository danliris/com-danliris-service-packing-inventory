using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class ItemsViewModel : BaseViewModel
    {

        public int? IdSOP { get; set; }
        public string NoSOP { get; set; }
        public string MaterialName { get; set; }
        public string InputLot { get; set; }
        public double? WeightBruto { get; set; }
        public string WeightDOS { get; set; }
        public string WeightCone { get; set; }
        public double? WeightBale { get; set; }
        public double? GetTotal { get; set; }
    }
}