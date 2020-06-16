using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCreditAdvice
{
    public class GarmentCreditAdviceValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
