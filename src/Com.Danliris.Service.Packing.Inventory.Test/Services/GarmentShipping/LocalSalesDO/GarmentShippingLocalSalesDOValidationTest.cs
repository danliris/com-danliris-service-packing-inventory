using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalSalesDOViewModel viewModel = new GarmentShippingLocalSalesDOViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalSalesDOViewModel viewModel = new GarmentShippingLocalSalesDOViewModel();
            viewModel.items = new List<GarmentShippingLocalSalesDOItemViewModel>
            {
                new GarmentShippingLocalSalesDOItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
