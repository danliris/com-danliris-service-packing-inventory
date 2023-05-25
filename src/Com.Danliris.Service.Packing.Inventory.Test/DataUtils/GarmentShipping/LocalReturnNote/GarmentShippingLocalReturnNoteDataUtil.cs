using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
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
            var items = new HashSet<GarmentShippingLocalReturnNoteItemModel> { new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "", ""), 1) };
            var model = new GarmentShippingLocalReturnNoteModel("", 1, DateTimeOffset.Now, "", new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1,"", true, false, false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", null), items);

            return model;
        }

        public override GarmentShippingLocalReturnNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalReturnNoteItemModel> { new GarmentShippingLocalReturnNoteItemModel(0, null,0) };
            var model = new GarmentShippingLocalReturnNoteModel(null, 0, DateTimeOffset.MinValue,null,null,  items);

            return model;
        }
    }
}