using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalPriceCuttingNoteViewModel viewModel = new GarmentShippingLocalPriceCuttingNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptytValue()
        {
            GarmentShippingLocalPriceCuttingNoteViewModel viewModel = new GarmentShippingLocalPriceCuttingNoteViewModel
            {
                date = DateTimeOffset.MinValue,
                buyer = new Buyer { Id = 0 },
                items = new List<GarmentShippingLocalPriceCuttingNoteItemViewModel>()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalPriceCuttingNoteViewModel viewModel = new GarmentShippingLocalPriceCuttingNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalPriceCuttingNoteItemViewModel>
            {
                new GarmentShippingLocalPriceCuttingNoteItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingLocalPriceCuttingNoteViewModel viewModel = new GarmentShippingLocalPriceCuttingNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalPriceCuttingNoteItemViewModel>
            {
                new GarmentShippingLocalPriceCuttingNoteItemViewModel
                {
                    salesNoteNo = "salesNoteNo",
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
