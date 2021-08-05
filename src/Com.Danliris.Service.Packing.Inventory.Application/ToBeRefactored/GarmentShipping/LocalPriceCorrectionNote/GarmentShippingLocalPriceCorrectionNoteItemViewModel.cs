using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteItemViewModel : BaseViewModel
    {
        public bool isChecked { get; set; }
        public GarmentShippingLocalSalesNoteItemViewModel salesNoteItem { get; set; }
        public double priceCorrection { get; set; }
    }
}
