using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalPriceCorrectionNoteViewModel viewModel = new GarmentShippingLocalPriceCorrectionNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalPriceCorrectionNoteViewModel viewModel = new GarmentShippingLocalPriceCorrectionNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalPriceCorrectionNoteItemViewModel>
            {
                new GarmentShippingLocalPriceCorrectionNoteItemViewModel()
                {
                    isChecked = true
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
