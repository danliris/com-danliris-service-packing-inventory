using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingPaymentDispositionViewModel viewModel = new GarmentShippingPaymentDispositionViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentShippingPaymentDispositionViewModel viewModel = new GarmentShippingPaymentDispositionViewModel
            {
                paymentType = "EMKL",
                emkl = new EMKL { Id = 0 },
                buyerAgent = new BuyerAgent { Id = 0 },
                incomeTax = new IncomeTax { id = 0 }

            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());

            GarmentShippingPaymentDispositionViewModel viewModel2 = new GarmentShippingPaymentDispositionViewModel
            {
                paymentType = "FORWARDER",
                forwarder = new Forwarder { id = 0 },
                buyerAgent = new BuyerAgent { Id = 0 },
                incomeTax = new IncomeTax { id = 0 }

            };

            var result2 = viewModel2.Validate(null);
            Assert.NotEmpty(result2.ToList());

            GarmentShippingPaymentDispositionViewModel viewModel3 = new GarmentShippingPaymentDispositionViewModel
            {
                paymentType = "COURIER",
                forwarder = new Forwarder { id = 0 },
                buyerAgent = new BuyerAgent { Id = 0 },
                incomeTax = new IncomeTax { id = 0 }

            };

            var result3 = viewModel3.Validate(null);
            Assert.NotEmpty(result3.ToList());

            GarmentShippingPaymentDispositionViewModel viewModel4 = new GarmentShippingPaymentDispositionViewModel
            {
                paymentType = "PERGUDANGAN",
                warehouse = new WareHouse { Id = 0 },
                buyerAgent = new BuyerAgent { Id = 0 },
                incomeTax = new IncomeTax { id = 0 }

            };

            var result4 = viewModel4.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingPaymentDispositionViewModel viewModel = new GarmentShippingPaymentDispositionViewModel();

            viewModel.paymentType = "FORWARDER";
            viewModel.isFreightCharged = true;
            viewModel.billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>
            {
                new GarmentShippingPaymentDispositionBillDetailViewModel()
            };
            viewModel.invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>
            {
                new GarmentShippingPaymentDispositionInvoiceDetailViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());


            viewModel.paymentType = "EMKL";
            viewModel.billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>
            {
                new GarmentShippingPaymentDispositionBillDetailViewModel()
            };
            viewModel.invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>
            {
                new GarmentShippingPaymentDispositionInvoiceDetailViewModel()
            };

            var result2 = viewModel.Validate(null);
            Assert.NotEmpty(result2.ToList());


            viewModel.paymentType = "COURIER";
            viewModel.totalBill = 1000;
            viewModel.billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>
            {
                new GarmentShippingPaymentDispositionBillDetailViewModel()
            };
            viewModel.unitCharges = new List<GarmentShippingPaymentDispositionUnitChargeViewModel>
            {
                new GarmentShippingPaymentDispositionUnitChargeViewModel()
            };

            var result3 = viewModel.Validate(null);
            Assert.NotEmpty(result3.ToList());

            viewModel.paymentType = "PERGUDANGAN";
            viewModel.billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>
            {
                new GarmentShippingPaymentDispositionBillDetailViewModel()
            };
            viewModel.invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>
            {
                new GarmentShippingPaymentDispositionInvoiceDetailViewModel()
            };

            var result4 = viewModel.Validate(null);
            Assert.NotEmpty(result4.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingPaymentDispositionViewModel viewModel = new GarmentShippingPaymentDispositionViewModel();

            viewModel.paymentType = "FORWARDER";
            viewModel.invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>
            {
                new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                {
                    invoiceId=0,
                    invoiceNo=""
                }
            };
            viewModel.billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>
            {
                new GarmentShippingPaymentDispositionBillDetailViewModel
                {
                    billDescription="",
                    amount=0
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
