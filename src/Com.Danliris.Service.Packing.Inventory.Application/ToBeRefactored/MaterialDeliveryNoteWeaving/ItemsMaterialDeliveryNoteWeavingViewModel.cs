using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public  class ItemsMaterialDeliveryNoteWeavingViewModel: BaseViewModel
    {
        public string itemNoSOP { get; set; }
        public string itemMaterialName { get; set; }
        public string itemGrade { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public decimal? inputBale { get; set; }
        public decimal? inputPiece { get; set; }
        public decimal? inputMeter { get; set; }
        public decimal? inputKg { get; set; }
    }
}