using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingDetailLocalSalesNoteViewModel viewModel = new GarmentShippingDetailLocalSalesNoteViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentShippingDetailLocalSalesNoteViewModel viewModel = new GarmentShippingDetailLocalSalesNoteViewModel
            {
                date = DateTimeOffset.MinValue,
                transactionType = new TransactionType { id = 0 },
                buyer = new Buyer { Id = 0 },
                amount=0,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingDetailLocalSalesNoteViewModel viewModel = new GarmentShippingDetailLocalSalesNoteViewModel();
            viewModel.items = new List<GarmentShippingDetailLocalSalesNoteItemViewModel>
            {
                new GarmentShippingDetailLocalSalesNoteItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingDetailLocalSalesNoteViewModel viewModel = new GarmentShippingDetailLocalSalesNoteViewModel();
            viewModel.items = new List<GarmentShippingDetailLocalSalesNoteItemViewModel>
            {
                new GarmentShippingDetailLocalSalesNoteItemViewModel
                {
                    Unit = new Unit {Id = 0 },
                    uom = new UnitOfMeasurement { Id = 0 },
                    quantity=10,
                    amount=100
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
