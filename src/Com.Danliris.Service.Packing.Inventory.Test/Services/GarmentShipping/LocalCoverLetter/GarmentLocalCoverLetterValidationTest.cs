using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentLocalCoverLetter
{
    public class GarmentLocalCoverLetterValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentLocalCoverLetterViewModel viewModel = new GarmentLocalCoverLetterViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
