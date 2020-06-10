using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public  class ItemsMaterialDeliveryNoteWeavingViewModel: BaseViewModel
    {
        public string ItemNoSPP { get; set; }
        public string ItemMaterialName { get; set; }
        public string ItemDesign { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public decimal InputPacking { get; set; }
        public decimal Length { get; set; }
        public decimal InputConversion { get; set; }
    }
}