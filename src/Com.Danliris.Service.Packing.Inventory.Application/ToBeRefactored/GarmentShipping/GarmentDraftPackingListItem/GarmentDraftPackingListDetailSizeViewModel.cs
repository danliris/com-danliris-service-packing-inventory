using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListDetailSizeViewModel : BaseViewModel
    {
        public SizeViewModel Size { get; set; }
        public double Quantity { get; set; }
    }
}
