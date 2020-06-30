using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingInstructionModel model = new GarmentShippingInstructionModel();
            model.SetInvoiceId(2, "username", "useragent");
            Assert.NotNull(model);
        }
    }
}
