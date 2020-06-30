using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using System;
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
        public void Validate_EmptyValue()
        {
            GarmentShippingLocalPriceCorrectionNoteViewModel viewModel = new GarmentShippingLocalPriceCorrectionNoteViewModel
            {
                salesNote = new GarmentShippingLocalSalesNoteViewModel { Id = 0},
                correctionDate = DateTimeOffset.MinValue,
                items = new List<GarmentShippingLocalPriceCorrectionNoteItemViewModel>()
            };

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

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingLocalPriceCorrectionNoteViewModel viewModel = new GarmentShippingLocalPriceCorrectionNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalPriceCorrectionNoteItemViewModel>
            {
                new GarmentShippingLocalPriceCorrectionNoteItemViewModel()
                {
                    isChecked = true,
                    salesNoteItem = new GarmentShippingLocalSalesNoteItemViewModel { Id = 0 },
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
