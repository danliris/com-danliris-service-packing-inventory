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
                dhlCharges = 0,
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
                paymentTerm = "TT/OA",
                otherCharge = -1,
                dhlCharges = 0,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_Value()
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel()
            {
                invoiceNo = "invoiceNo",
                packingListId = 1,
                paymentTerm = "LC",
                bankComission = -1,
                discrepancyFee = -1,
                btbAmount = -1,
                btbRatio = -1,
                btbRate = -1,
                btbTransfer = -1,
                btbMaterial = -1,
                billDays = -1,
                billAmount = -1,
                creditInterest = -1,
                dhlCharges = -1,
                bankCharges = -1
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}