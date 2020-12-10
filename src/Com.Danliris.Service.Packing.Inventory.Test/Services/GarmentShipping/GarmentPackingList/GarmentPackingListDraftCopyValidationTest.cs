using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftCopyValidationTest
    {
        private GarmentPackingListDraftCopyViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListDraftCopyViewModel
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
            GarmentPackingListDraftCopyViewModel viewModel = ViewModel;

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
