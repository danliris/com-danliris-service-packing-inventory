using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteItemDataUtil : BaseDataUtil<GarmentShippingLocalSalesNoteItemRepository, GarmentShippingLocalSalesNoteItemModel>
    {
        public GarmentShippingLocalSalesNoteItemDataUtil(GarmentShippingLocalSalesNoteItemRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalSalesNoteItemModel GetModel()
        {
             var model = new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "", "");

            return model;
        }

        public override GarmentShippingLocalSalesNoteItemModel GetEmptyModel()
        {
            var model = new GarmentShippingLocalSalesNoteItemModel(0,0, null, null, 0, 0, null, 0, 0, 0, null, null);
     
            return model;
        }
    }
}
