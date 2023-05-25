using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailShippingLocalSalesNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemDataUtil : BaseDataUtil<GarmentShippingDetailLocalSalesNoteItemRepository, GarmentShippingDetailLocalSalesNoteItemModel>
    {
        public GarmentShippingDetailLocalSalesNoteItemDataUtil(GarmentShippingDetailLocalSalesNoteItemRepository repository) : base(repository)
        {
        }

        public override GarmentShippingDetailLocalSalesNoteItemModel GetModel()
        {
             var model = new GarmentShippingDetailLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 100);

            return model;
        }

        public override GarmentShippingDetailLocalSalesNoteItemModel GetEmptyModel()
        {
            var model = new GarmentShippingDetailLocalSalesNoteItemModel(0,0, null, null, 0, 0, null, 0);
     
            return model;
        }
    }
}
