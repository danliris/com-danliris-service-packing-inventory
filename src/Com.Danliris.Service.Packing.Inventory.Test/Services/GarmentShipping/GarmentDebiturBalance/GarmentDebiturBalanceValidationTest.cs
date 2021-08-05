using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance;
using System;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentDebiturBalanceViewModel viewModel = new GarmentDebiturBalanceViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentDebiturBalanceViewModel viewModel = new GarmentDebiturBalanceViewModel
            {
                balanceDate = DateTimeOffset.MinValue,
                buyerAgent = new BuyerAgent { Id = 0 },
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
