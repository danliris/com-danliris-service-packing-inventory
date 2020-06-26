using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteItemModel : StandardEntity
    {
        public int ReturnNoteId { get; private set; }
        public int SalesNoteItemId { get; private set; }
        public GarmentShippingLocalSalesNoteItemModel SalesNoteItem { get; private set; }
        public double ReturnQuantity { get; private set; }

        public GarmentShippingLocalReturnNoteItemModel(int salesNoteItemId, GarmentShippingLocalSalesNoteItemModel salesNoteItem, double returnQuantity)
        {
            SalesNoteItemId = salesNoteItemId;
            SalesNoteItem = salesNoteItem;
            ReturnQuantity = returnQuantity;
        }

        public GarmentShippingLocalReturnNoteItemModel()
        {
        }
    }
}
