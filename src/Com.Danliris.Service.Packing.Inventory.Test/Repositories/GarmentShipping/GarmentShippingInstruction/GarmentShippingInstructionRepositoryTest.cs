using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInstruction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInstructionRepository, GarmentShippingInstructionModel, GarmentShippingInstructionDataUtil>
    {
        private const string ENTITY = "GarmentShippingInstruction";

        public GarmentShippingInstructionRepositoryTest() : base(ENTITY)
        {
        }
    }
}
