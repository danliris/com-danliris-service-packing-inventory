using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentPackingListViewModel viewModel = new GarmentPackingListViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DateMoreThanToday()
        {
            GarmentPackingListViewModel viewModel = new GarmentPackingListViewModel
            {
                Date = DateTimeOffset.Now.AddDays(1),
                Items = new List<GarmentPackingListItemViewModel>(),
                Measurements = new List<GarmentPackingListMeasurementViewModel>()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = new GarmentPackingListViewModel
            {
                Items = new List<GarmentPackingListItemViewModel>
                {
                    new GarmentPackingListItemViewModel()
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DetailsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = new GarmentPackingListViewModel
            {
                Items = new List<GarmentPackingListItemViewModel>
                {
                    new GarmentPackingListItemViewModel
                    {
                        Details = new List<GarmentPackingListDetailViewModel>
                        {
                            new GarmentPackingListDetailViewModel()
                        }
                    }
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_MeasurementsDefaultValue()
        {
            GarmentPackingListViewModel viewModel = new GarmentPackingListViewModel
            {
                Measurements = new List<GarmentPackingListMeasurementViewModel>
                {
                    new GarmentPackingListMeasurementViewModel()
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
