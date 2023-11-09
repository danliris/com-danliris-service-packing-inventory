using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailSizeViewModel : BaseViewModel
    {
        public SizeViewModel Size { get; set; }
        public double Quantity { get; set; }
    }
}
