using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter
{
    public class GarmentShippingNoteDataUtil : BaseDataUtil<GarmentShippingNoteRepository, GarmentShippingNoteModel>
    {
        public GarmentShippingNoteDataUtil(GarmentShippingNoteRepository repository) : base(repository)
        {
        }

        public override GarmentShippingNoteModel GetModel()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 1, "", 1) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.ND, "", DateTimeOffset.Now, 1, "", "", 1, items);

            return model;
        }

        public override GarmentShippingNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel(null, 0, null, 01) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.ND, null, DateTimeOffset.MinValue, 0, null, null, 0, items);

            return model;
        }
    }
}
