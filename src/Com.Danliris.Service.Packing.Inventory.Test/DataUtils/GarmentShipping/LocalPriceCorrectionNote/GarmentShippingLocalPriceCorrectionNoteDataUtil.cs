using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteDataUtil : BaseDataUtil<GarmentShippingLocalPriceCorrectionNoteRepository, GarmentShippingLocalPriceCorrectionNoteModel>
    {
        public GarmentShippingLocalPriceCorrectionNoteDataUtil(GarmentShippingLocalPriceCorrectionNoteRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalPriceCorrectionNoteModel GetModel()
        {
            var items = new List<GarmentShippingLocalPriceCorrectionNoteItemModel>() { new GarmentShippingLocalPriceCorrectionNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, "", "", 1, 1, "", 1), 1) };
            var model = new GarmentShippingLocalPriceCorrectionNoteModel("", DateTimeOffset.Now, 1, new GarmentShippingLocalSalesNoteModel("", DateTimeOffset.Now, 1, "", "", 1, "", "", "", 1, "", true, "", true, null), items);

            return model;
        }

        public override GarmentShippingLocalPriceCorrectionNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalPriceCorrectionNoteItemModel> { new GarmentShippingLocalPriceCorrectionNoteItemModel(0, null, 0) };
            var model = new GarmentShippingLocalPriceCorrectionNoteModel(null, DateTimeOffset.MinValue, 0, null, items);

            return model;
        }
    }
}
