using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListItemDataUtil : BaseDataUtil<GarmentShippingPackingListItemRepository, GarmentShippingPackingListItemModel>
    {
        public GarmentShippingPackingListItemDataUtil(GarmentShippingPackingListItemRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPackingListItemModel GetModel()
        {
            var model = new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", null);

            return model;
        }

        public override GarmentShippingPackingListItemModel GetEmptyModel()
        {
            var model = new GarmentShippingPackingListItemModel(null, null, null, 0, null, 0, null, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, null, null, null);

            return model;
        }
    }
}
