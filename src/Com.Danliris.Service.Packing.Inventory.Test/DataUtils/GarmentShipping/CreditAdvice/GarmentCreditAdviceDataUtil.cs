﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCreditAdvice
{
    public class GarmentCreditAdviceDataUtil : BaseDataUtil<GarmentShippingCreditAdviceRepository, GarmentShippingCreditAdviceModel>
    {
        public GarmentCreditAdviceDataUtil(GarmentShippingCreditAdviceRepository repository) : base(repository)
        {
        }

        public override GarmentShippingCreditAdviceModel GetModel()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 0, 0, 0, DateTimeOffset.Now, 0, 0, 0, 0, 0, 0, 0, "", 1, "", "", "", 1, "", "", "", 0, 0, 0, DateTimeOffset.Now, "", DateTimeOffset.Now, 0, "", DateTimeOffset.Now, 0, DateTimeOffset.Now, "", 0, 0);

            return model;
        }

        public override GarmentShippingCreditAdviceModel GetEmptyModel()
        {
            var model = new GarmentShippingCreditAdviceModel(0, 0, null, DateTimeOffset.MinValue, 0, 0, 0, 0, null, null, null, true, null, 0, 0, null, DateTimeOffset.MinValue, DateTimeOffset.MinValue, null, 0, 0, 0, DateTimeOffset.MinValue, 0, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, 0, null, null, null, 0, 0, 0, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, 0, null, DateTimeOffset.MinValue, 0, DateTimeOffset.MinValue, null, 0, 0);

            return model;
        }
    }
}
