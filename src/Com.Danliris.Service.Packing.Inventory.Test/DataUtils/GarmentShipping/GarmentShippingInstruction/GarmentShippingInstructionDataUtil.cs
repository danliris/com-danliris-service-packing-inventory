using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionDataUtil : BaseDataUtil<GarmentShippingInstructionRepository, GarmentShippingInstructionModel>
    {
        public GarmentShippingInstructionDataUtil(GarmentShippingInstructionRepository repository) : base(repository)
        {
        }

        public override GarmentShippingInstructionModel GetModel()
        {
            var model = new GarmentShippingInstructionModel("no", 1, DateTimeOffset.Now, 1, "", "", "", "", "", 1, "", "", "", DateTimeOffset.Now, "", "", "", "", "", "", "", "", 1, "", 1, "", "", "", "", "");
            return model;
        }

        public override GarmentShippingInstructionModel GetEmptyModel()
        {
            var model = new GarmentShippingInstructionModel(null, 0, DateTimeOffset.MinValue, 0, null, null, null, null, null, 0, null, null, null, DateTimeOffset.MinValue, null, null, null, null, null, null, null, null, 0, null, 0, null, null, null, null, null);
            return model;
        }
    }
}
