using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ShippingNote
{
    public class GarmentShippingNoteItemDataUtil : BaseDataUtil<GarmentShippingNoteItemRepository, GarmentShippingNoteItemModel>
    {
        public GarmentShippingNoteItemDataUtil(GarmentShippingNoteItemRepository repository) : base(repository)
        {
        }

        public override GarmentShippingNoteItemModel GetModel()
        {
             var model = new GarmentShippingNoteItemModel("", 1, "", 1);

            return model;
        }

        public override GarmentShippingNoteItemModel GetEmptyModel()
        {
            var model = new GarmentShippingNoteItemModel(null, 0, null, 01);
     
            return model;
        }
    }
}
