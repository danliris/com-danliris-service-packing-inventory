using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.VBPayment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.VBNote
{
    public class GarmentShippingVBPaymentRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingVBPaymentRepository, GarmentShippingVBPaymentModel, GarmentShippingVBPaymentDataUtil>
    {
        private const string ENTITY = "GarmentShippingVBPayment";

        public GarmentShippingVBPaymentRepositoryTest() : base(ENTITY)
        {
        }

       
    }
}
