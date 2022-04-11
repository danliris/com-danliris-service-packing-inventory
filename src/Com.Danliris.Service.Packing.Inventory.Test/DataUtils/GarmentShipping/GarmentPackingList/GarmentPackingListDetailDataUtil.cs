using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDetailDataUtil : BaseDataUtil<GarmentPackingListDetailRepository, GarmentPackingListDetailModel>
    {
        public GarmentPackingListDetailDataUtil(GarmentPackingListDetailRepository repository) : base(repository)
        {
        }

        public override GarmentPackingListDetailModel GetModel()
        {
            var model = new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, null, 1);

            return model;
        }

        public override GarmentPackingListDetailModel GetEmptyModel()
        {
            var model = new GarmentPackingListDetailModel(0, 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 1);

            return model;
        }
    }
}
