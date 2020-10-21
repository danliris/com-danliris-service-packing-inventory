using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftValidationTest
    {
        private GarmentPackingListDraftViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListDraftViewModel
                {
                    Items = new List<GarmentPackingListItemViewModel>()
                    {
                        new GarmentPackingListItemViewModel()
                    },
                    Measurements = new List<GarmentPackingListMeasurementViewModel>()
                    {
                        new GarmentPackingListMeasurementViewModel()
                    },
                };
            }
        }

        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentPackingListViewModel viewModel = ViewModel;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
