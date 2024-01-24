using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSDataUtil : BaseDataUtil<GarmentShippingLocalSalesNoteTSRepository, GarmentShippingLocalSalesNoteTSModel>
    {
        public GarmentShippingLocalSalesNoteTSDataUtil(GarmentShippingLocalSalesNoteTSRepository repository/*, GarmentReceiptSubconPackingListDataUtil dataUtilSC*/) : base(repository)
        {
        }

        public override GarmentShippingLocalSalesNoteTSModel GetModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesNoteTSItemModel> { new GarmentShippingLocalSalesNoteTSItemModel(1, 1,1, "",1, 1, 1, "", "","") };
            var model = new GarmentShippingLocalSalesNoteTSModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", "", "", 1, true, 1, 1, "", true, true, true, true, false,"","", DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "",false, items);

            return model;
        }

        public override GarmentShippingLocalSalesNoteTSModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesNoteTSItemModel> { new GarmentShippingLocalSalesNoteTSItemModel(1, 1, 1, "", 1, 1, 1, "", "", "") };
            var model = new GarmentShippingLocalSalesNoteTSModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", "", "", 1, true, 1, 1, "", true, true, true, true, false, "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", false, items);

            return model;
        }
    }
}
