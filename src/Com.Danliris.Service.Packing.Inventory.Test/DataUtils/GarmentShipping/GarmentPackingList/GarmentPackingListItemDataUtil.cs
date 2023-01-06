using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListItemDataUtil : BaseDataUtil<GarmentPackingListItemRepository, GarmentPackingListItemModel>
    {
        public GarmentPackingListItemDataUtil(GarmentPackingListItemRepository repository) : base(repository)
        {
        }

        public override GarmentPackingListItemModel GetModel()
        {
            var model = new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", null);

            return model;
        }

        public override GarmentPackingListItemModel GetEmptyModel()
        {
            var model = new GarmentPackingListItemModel(null, null, 0, null, 0, null, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, null, null, null);

            return model;
        }
    }
}
