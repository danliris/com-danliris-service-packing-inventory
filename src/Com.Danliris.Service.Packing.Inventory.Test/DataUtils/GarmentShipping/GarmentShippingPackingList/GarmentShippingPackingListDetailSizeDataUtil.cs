using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailSizeDataUtil : BaseDataUtil<GarmentShippingPackingListDetailSizeRepository, GarmentShippingPackingListDetailSizeModel>
    {
        public GarmentShippingPackingListDetailSizeDataUtil(GarmentShippingPackingListDetailSizeRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPackingListDetailSizeModel GetModel()
        {
            var model = new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1);

            return model;
        }

        public override GarmentShippingPackingListDetailSizeModel GetEmptyModel()
        {
            var model = new GarmentShippingPackingListDetailSizeModel(0,null, 0, 0);

            return model;
        }
    }
}
