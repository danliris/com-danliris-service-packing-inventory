using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            PaymentDispositionRecapViewModel viewModel = new PaymentDispositionRecapViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            PaymentDispositionRecapViewModel viewModel = new PaymentDispositionRecapViewModel
            {
                date = DateTimeOffset.Now,
                emkl = new EMKL { Id = 0 },
                items = new List<PaymentDispositionRecapItemViewModel>()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
