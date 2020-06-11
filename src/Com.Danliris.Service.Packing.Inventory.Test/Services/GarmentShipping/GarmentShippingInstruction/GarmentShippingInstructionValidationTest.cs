using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingInstructionViewModel viewModel = new GarmentShippingInstructionViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
