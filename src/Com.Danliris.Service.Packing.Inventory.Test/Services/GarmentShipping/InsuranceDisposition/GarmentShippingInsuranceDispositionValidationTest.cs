using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingInsuranceDispositionViewModel viewModel = new GarmentShippingInsuranceDispositionViewModel();
            viewModel.policyType = "KARGO";
            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingInsuranceDispositionViewModel viewModel = new GarmentShippingInsuranceDispositionViewModel();
            viewModel.policyType = "PIUTANG";
            viewModel.items = new List<GarmentShippingInsuranceDispositionItemViewModel>
            {
                new GarmentShippingInsuranceDispositionItemViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_PIUTANG()
        {
            GarmentShippingInsuranceDispositionViewModel viewModel = new GarmentShippingInsuranceDispositionViewModel();
            viewModel.policyType = "PIUTANG";
            viewModel.rate = 0;
            viewModel.items = new List<GarmentShippingInsuranceDispositionItemViewModel>
            {
                new GarmentShippingInsuranceDispositionItemViewModel()
                {
                    amount=0
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
        [Fact]
        public void Validate_KARGO()
        {
            GarmentShippingInsuranceDispositionViewModel viewModel = new GarmentShippingInsuranceDispositionViewModel();
            viewModel.policyType = "KARGO";
            viewModel.rate = 0;
            viewModel.items = new List<GarmentShippingInsuranceDispositionItemViewModel>
            {
                new GarmentShippingInsuranceDispositionItemViewModel()
                {
                    amount=0,
                    currencyRate=0
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
