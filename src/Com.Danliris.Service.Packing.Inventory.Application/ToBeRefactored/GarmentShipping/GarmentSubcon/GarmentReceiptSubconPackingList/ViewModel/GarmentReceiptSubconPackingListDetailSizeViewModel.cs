using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListDetailSizeViewModel : BaseViewModel
    {
        public SizeViewModel Size { get; set; }
        public double Quantity { get; set; }
        public string Color { get; set; }
        public Guid PackingOutItemId { get; set; }
    }
}
