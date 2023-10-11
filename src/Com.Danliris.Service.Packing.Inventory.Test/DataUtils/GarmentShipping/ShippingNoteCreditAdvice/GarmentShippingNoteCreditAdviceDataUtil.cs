using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNoteCreditAdvice;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceDataUtil : BaseDataUtil<GarmentShippingNoteCreditAdviceRepository, GarmentShippingNoteCreditAdviceModel>
    {
        public GarmentShippingNoteCreditAdviceDataUtil(GarmentShippingNoteCreditAdviceRepository repository) : base(repository)
        {
        }

        public override GarmentShippingNoteCreditAdviceModel GetModel()
        {   
            var model = new GarmentShippingNoteCreditAdviceModel(1, "", "", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 0, 0, 0, 0, DateTimeOffset.Now, "");

            return model;
        }

        public override GarmentShippingNoteCreditAdviceModel GetEmptyModel()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(0, "", "", DateTimeOffset.MinValue, "", "", 0, 0, 0, DateTimeOffset.MinValue, 0, 0, "", "", "", 0, "", "", "", 0, 0, 0, 0, DateTimeOffset.MinValue, "");

            return model;
        }
    }
}
