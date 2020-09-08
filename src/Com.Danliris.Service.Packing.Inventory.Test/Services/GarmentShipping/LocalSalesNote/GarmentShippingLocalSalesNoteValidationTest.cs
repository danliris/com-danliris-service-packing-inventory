using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalSalesNoteViewModel viewModel = new GarmentShippingLocalSalesNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentShippingLocalSalesNoteViewModel viewModel = new GarmentShippingLocalSalesNoteViewModel
            {
                date = DateTimeOffset.MinValue,
                transactionType = new TransactionType { id = 0 },
                buyer = new Buyer { Id = 0 }

            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalSalesNoteViewModel viewModel = new GarmentShippingLocalSalesNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalSalesNoteItemViewModel>
            {
                new GarmentShippingLocalSalesNoteItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingLocalSalesNoteViewModel viewModel = new GarmentShippingLocalSalesNoteViewModel();
            viewModel.items = new List<GarmentShippingLocalSalesNoteItemViewModel>
            {
                new GarmentShippingLocalSalesNoteItemViewModel
                {
                    product = new ProductViewModel {id = 0 },
                    uom = new UnitOfMeasurement { Id = 0 }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
