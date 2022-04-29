using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDetailSizeDataUtil : BaseDataUtil<GarmentPackingListDetailSizeRepository, GarmentPackingListDetailSizeModel>
    {
        public GarmentPackingListDetailSizeDataUtil(GarmentPackingListDetailSizeRepository repository) : base(repository)
        {
        }

        public override GarmentPackingListDetailSizeModel GetModel()
        {
            var model = new GarmentPackingListDetailSizeModel(1, "", 1);

            return model;
        }

        public override GarmentPackingListDetailSizeModel GetEmptyModel()
        {
            var model = new GarmentPackingListDetailSizeModel(0,null, 0);

            return model;
        }
    }
}
