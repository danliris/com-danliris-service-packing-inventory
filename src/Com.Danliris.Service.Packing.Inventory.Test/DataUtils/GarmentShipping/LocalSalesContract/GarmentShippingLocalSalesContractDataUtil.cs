using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractDataUtil : BaseDataUtil<GarmentShippingLocalSalesContractRepository, GarmentShippingLocalSalesContractModel>
    {
        public GarmentShippingLocalSalesContractDataUtil(GarmentShippingLocalSalesContractRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalSalesContractModel GetModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesContractItemModel> { new GarmentShippingLocalSalesContractItemModel(1, "", "", 1, 1,1, "", 1) };
            var model = new GarmentShippingLocalSalesContractModel("", DateTimeOffset.Now, 1, "", "", "", "", "", "", 1, "","","","", true, 1, 1, 1, false, items);

            return model;
        }

        public override GarmentShippingLocalSalesContractModel GetEmptyModel()
        {
            var items = new HashSet<GarmentShippingLocalSalesContractItemModel> { new GarmentShippingLocalSalesContractItemModel(0, null, null, 0, 0,0, null, 0) };
            var model = new GarmentShippingLocalSalesContractModel(null, DateTimeOffset.MinValue, 0, null, null, null, null, null, null,0, null, null, null, null, true, 1, 1, 0, false, items);

            return model;
        }
    }
}
