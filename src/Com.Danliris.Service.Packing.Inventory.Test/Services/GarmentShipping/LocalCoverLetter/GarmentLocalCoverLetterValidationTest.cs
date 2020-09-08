using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using System;
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

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentLocalCoverLetterViewModel viewModel = new GarmentLocalCoverLetterViewModel
            {
                noteNo = "noteNo",
                date = DateTimeOffset.MinValue,
                buyer = new Buyer { Id = 0 },
                shippingStaff = new ShippingStaff { id = 0 }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
