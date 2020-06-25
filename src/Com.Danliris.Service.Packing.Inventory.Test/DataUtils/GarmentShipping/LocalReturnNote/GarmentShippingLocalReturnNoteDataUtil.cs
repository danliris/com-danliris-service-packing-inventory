using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteDataUtil : BaseDataUtil<GarmentShippingLocalReturnNoteRepository, GarmentShippingLocalReturnNoteModel>
    {
        public GarmentShippingLocalReturnNoteDataUtil(GarmentShippingLocalReturnNoteRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalReturnNoteModel GetModel()
        {
            var items = new HashSet<GarmentShippingLocalReturnNoteItemModel> { new GarmentShippingLocalReturnNoteItemModel(1, 1) };
            var model = new GarmentShippingLocalReturnNoteModel("", 1, DateTimeOffset.Now, "", items);

            return model;
        }

        public override GarmentShippingLocalReturnNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalReturnNoteItemModel> { new GarmentShippingLocalReturnNoteItemModel(0, 0) };
            var model = new GarmentShippingLocalReturnNoteModel(null, 0, DateTimeOffset.MinValue,null,  items);

            return model;
        }
    }
}