using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteItemViewModel : BaseViewModel
    {
        public bool isChecked { get; set; }
        public GarmentShippingLocalSalesNoteItemViewModel salesNoteItem { get; set; }
        public double returnQuantity { get; set; }
    }
}
