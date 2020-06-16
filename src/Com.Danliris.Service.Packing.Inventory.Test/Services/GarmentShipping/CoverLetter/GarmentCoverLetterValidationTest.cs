using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentCoverLetterViewModel viewModel = new GarmentCoverLetterViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
