using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalReturnNoteViewModel viewModel = new GarmentShippingLocalReturnNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalReturnNoteViewModel viewModel = new GarmentShippingLocalReturnNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalReturnNoteItemViewModel>
            {
                new GarmentShippingLocalReturnNoteItemViewModel()
                {
                    isChecked = true
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
