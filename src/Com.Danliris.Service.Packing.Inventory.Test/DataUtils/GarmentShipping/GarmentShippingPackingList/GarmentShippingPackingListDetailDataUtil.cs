using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailDataUtil : BaseDataUtil<GarmentShippingPackingListDetailRepository, GarmentShippingPackingListDetailModel>
    {
        public GarmentShippingPackingListDetailDataUtil(GarmentShippingPackingListDetailRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPackingListDetailModel GetModel()
        {
            var model = new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, null, 1);

            return model;
        }

        public override GarmentShippingPackingListDetailModel GetEmptyModel()
        {
            var model = new GarmentShippingPackingListDetailModel(0, 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 1);

            return model;
        }
    }
}
