using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using System;
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

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentCoverLetterViewModel viewModel = new GarmentCoverLetterViewModel
            {
                date = DateTimeOffset.MinValue,
                bookingDate = DateTimeOffset.MinValue,
                order = new Buyer { Id = 0 },
                forwarder = new Forwarder { id = 0 },
                exportEstimationDate = DateTimeOffset.MinValue,
                shippingStaff = new ShippingStaff { id = 0 }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
