using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingCreditNote
{
    public class GarmentShippingCreditNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingCreditNoteViewModel viewModel = new GarmentShippingCreditNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentShippingCreditNoteViewModel viewModel = new GarmentShippingCreditNoteViewModel
            {
                date = DateTimeOffset.MinValue,
                buyer = new Buyer { Id = 0 }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingCreditNoteViewModel viewModel = new GarmentShippingCreditNoteViewModel();
            viewModel.items = new List<GarmentShippingNoteItemViewModel>
            {
                new GarmentShippingNoteItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingCreditNoteViewModel viewModel = new GarmentShippingCreditNoteViewModel();
            viewModel.items = new List<GarmentShippingNoteItemViewModel>
            {
                new GarmentShippingNoteItemViewModel
                {
                    currency = new Currency { Id = 0 }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
