using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteDataUtil : BaseDataUtil<GarmentShippingDetailLocalSalesNoteRepository, GarmentShippingDetailLocalSalesNoteModel>
    {
        public GarmentShippingDetailLocalSalesNoteDataUtil(GarmentShippingDetailLocalSalesNoteRepository repository, GarmentShippingLocalSalesNoteDataUtil dataUtilSC) : base(repository)
        {
        }

        public override GarmentShippingDetailLocalSalesNoteModel GetModel()
        {
            var items = new HashSet<GarmentShippingDetailLocalSalesNoteItemModel> { new GarmentShippingDetailLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 100) };
            var model = new GarmentShippingDetailLocalSalesNoteModel("", 1, 1, "", DateTimeOffset.Now, 1, "", "", 1, "", "", 0, items);

            return model;
        }

        public override GarmentShippingDetailLocalSalesNoteModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingDetailLocalSalesNoteItemModel> { new GarmentShippingDetailLocalSalesNoteItemModel(0, 0, null, null, 0, 0, null, 0) };
            var model = new GarmentShippingDetailLocalSalesNoteModel(null, 0, 0, null, DateTimeOffset.MinValue, 0, null, null, 0, null, null, 0, items);
       
            return model;
        }
    }
}
