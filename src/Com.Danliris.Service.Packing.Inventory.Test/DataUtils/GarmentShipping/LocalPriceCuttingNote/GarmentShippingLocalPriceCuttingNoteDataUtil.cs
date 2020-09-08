using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteDataUtil : BaseDataUtil<GarmentShippingLocalPriceCuttingNoteRepository, GarmentShippingLocalPriceCuttingNoteModel>
    {
        public GarmentShippingLocalPriceCuttingNoteDataUtil(GarmentShippingLocalPriceCuttingNoteRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalPriceCuttingNoteModel GetModel()
        {
            var items = new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1) };
            var model = new GarmentShippingLocalPriceCuttingNoteModel("", DateTimeOffset.Now, 1, "", "", true, "", items);

            return model;
        }

        public override GarmentShippingLocalPriceCuttingNoteModel GetEmptyModel()
        {
            var items = new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { new GarmentShippingLocalPriceCuttingNoteItemModel(0, null, 0, 0) };
            var model = new GarmentShippingLocalPriceCuttingNoteModel(null, DateTimeOffset.MinValue, 0, null, null, true, null, items);

            return model;
        }
    }
}
