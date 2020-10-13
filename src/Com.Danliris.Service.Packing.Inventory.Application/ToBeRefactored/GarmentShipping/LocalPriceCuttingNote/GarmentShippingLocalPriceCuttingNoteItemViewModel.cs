using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteItemViewModel : BaseViewModel
    {
        public int salesNoteId { get; set; }
        public string salesNoteNo { get; set; }
        public double salesAmount { get; set; }
        public double cuttingAmount { get; set; }
        public bool includeVat { get; set; }
    }
}
