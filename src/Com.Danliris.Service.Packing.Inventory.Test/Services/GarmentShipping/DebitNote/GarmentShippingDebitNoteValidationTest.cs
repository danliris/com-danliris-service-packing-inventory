using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingDebitNote
{
    public class GarmentShippingDebitNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingDebitNoteViewModel viewModel = new GarmentShippingDebitNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingDebitNoteViewModel viewModel = new GarmentShippingDebitNoteViewModel();
            viewModel.items = new List<GarmentShippingNoteItemViewModel>
            {
                new GarmentShippingNoteItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
