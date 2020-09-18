using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingLocalSalesContractViewModel viewModel = new GarmentShippingLocalSalesContractViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingLocalSalesContractViewModel viewModel = new GarmentShippingLocalSalesContractViewModel();
            viewModel.items = new List<GarmentShippingLocalSalesContractItemViewModel>
            {
                new GarmentShippingLocalSalesContractItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
