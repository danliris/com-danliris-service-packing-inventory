using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using System;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCreditAdvice
{
    public class GarmentCreditAdviceValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel()
            {
                invoiceNo = "invoiceNo",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC",
                buyer = new Buyer(),
                bank = new BankAccount()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC_DateTimeOffsetMinValue()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel()
            {
                invoiceNo = "invoiceNo",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC",
                buyer = new Buyer(),
                bank = new BankAccount(),
                negoDate = DateTimeOffset.MinValue,
                paymentDate = DateTimeOffset.MinValue,
                btbCADate = DateTimeOffset.MinValue,
                documentPresente = DateTimeOffset.MinValue
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_TT()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel()
            {
                invoiceNo = "invoiceNo",
                packingListId = 1,
                paymentTerm = "TT/OA"
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
