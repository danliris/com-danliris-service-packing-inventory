using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice;
using System;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC_DN()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA DEBIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC PAYMENT",
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                buyer = new Buyer(),
                bank = new BankAccount()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_TT_DN()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA DEBIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "TT PAYMENT",
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                buyer = new Buyer(),
                bank = new BankAccount()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC_CN()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA KREDIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC PAYMENT",
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                buyer = new Buyer(),
                bank = null
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_TT_CN()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "CREDIT DEBIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "TT PAYMENT",
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                buyer = new Buyer(),
                bank = null
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC_DN_DateTimeOffsetMinValue()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA DEBIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC PAYMENT",
                buyer = new Buyer(),
                bank = new BankAccount(),
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                paymentDate = DateTimeOffset.MinValue,
                documentSendDate = DateTimeOffset.MinValue,
             };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_TT_DN_DateTimeOffsetMinValue()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA DEBIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "TT PAYMENT",
                buyer = new Buyer(),
                bank = new BankAccount(),
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                paymentDate = DateTimeOffset.MinValue,
                documentSendDate = DateTimeOffset.MinValue,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_LC_CN_DateTimeOffsetMinValue()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA KREDIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "LC PAYMENT",
                buyer = new Buyer(),
                bank = null,
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                paymentDate = DateTimeOffset.MinValue,
                documentSendDate = DateTimeOffset.MinValue,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_TT_CN_DateTimeOffsetMinValue()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA KREDIT",
                date = DateTimeOffset.MinValue,
                paymentTerm = "TT PAYMENT",
                buyer = new Buyer(),
                bank = null,
                paidAmount = 0,
                bankComission = 0,
                bankCharges = 0,
                creditInterest = 0,
                insuranceCharge = 0,
                paymentDate = DateTimeOffset.MinValue,
                documentSendDate = DateTimeOffset.MinValue,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_DN_Value()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA DEBIT",
                shippingnoteId = 1,
                paymentTerm = "LC PAYMENT",
                bankComission = -1,
                creditInterest = -1,
                bankCharges = -1,
                insuranceCharge = -1,
                paidAmount = -1,
                buyer = new Buyer(),
                bank = new BankAccount()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_CN_Value()
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel()
            {
                noteNo = "noteNo",
                noteType = "NOTA KREDIT",
                shippingnoteId = 1,
                paymentTerm = "LC PAYMENT",
                bankComission = -1,
                creditInterest = -1,
                bankCharges = -1,
                insuranceCharge = -1,
                paidAmount = -1,
                buyer = new Buyer(),
                bank = null,
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}