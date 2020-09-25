using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteDataUtil : BaseDataUtil<GarmentShippingLocalSalesNoteRepository, GarmentShippingLocalSalesNoteModel>
    {
        public GarmentShippingLocalSalesNoteDataUtil(GarmentShippingLocalSalesNoteRepository repository, GarmentShippingLocalSalesContractDataUtil dataUtilSC) : base(repository)
        {
        }

        public override GarmentShippingLocalSalesNoteModel GetModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesNoteItemModel> { new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "") };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", 1, "", true, "",false, items);

            return model;
        }

        public override GarmentShippingLocalSalesNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesNoteItemModel> { new GarmentShippingLocalSalesNoteItemModel(0,0, null, null, 0, 0, null, 0, 0, 0, null) };
            var model = new GarmentShippingLocalSalesNoteModel(null, 0, null, null, DateTimeOffset.MinValue, 0, null, null, 0, null, null, null, 0, null, true, null,false, items);

            return model;
        }
    }
}
