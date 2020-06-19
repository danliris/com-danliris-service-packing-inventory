using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDODataUtil : BaseDataUtil<GarmentShippingLocalSalesDORepository, GarmentShippingLocalSalesDOModel>
    {
        public GarmentShippingLocalSalesDODataUtil(GarmentShippingLocalSalesDORepository repository, GarmentShippingLocalSalesNoteDataUtil SalesNoteDataUtil) : base(repository)
        {
        }

        public override GarmentShippingLocalSalesDOModel GetModel()
        {
            var item = new HashSet<GarmentShippingLocalSalesDOItemModel> { new GarmentShippingLocalSalesDOItemModel(1,1,1, "", "", "", 1, 1, "", 1, 1, 1, 1) };
            var model = new GarmentShippingLocalSalesDOModel("", "", 1, DateTimeOffset.Now, 1, "", "", "", "", item);

            return model;
        }

        public override GarmentShippingLocalSalesDOModel GetEmptyModel()
        {
            var item = new HashSet<GarmentShippingLocalSalesDOItemModel> { new GarmentShippingLocalSalesDOItemModel(0,0,0, null, null, null, 0, 0, null, 0, 0, 0, 0) };
            var model = new GarmentShippingLocalSalesDOModel(null, null, 0, DateTimeOffset.MinValue, 0, null, null, null, null,  item);

            return model;
        }
    }
}
