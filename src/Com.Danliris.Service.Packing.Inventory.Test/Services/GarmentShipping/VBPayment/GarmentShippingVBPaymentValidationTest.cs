using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentValidationTest
    {
        [Fact]
        public void Validate_DefaultValue()
        {
            GarmentShippingVBPaymentViewModel viewModel = new GarmentShippingVBPaymentViewModel();

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_EmptyValue()
        {
            GarmentShippingVBPaymentViewModel viewModel = new GarmentShippingVBPaymentViewModel
            {
                paymentType="EMKL",
                emkl=new EMKL {Id=0 },
                buyer = new Buyer { Id = 0 }

            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());

            GarmentShippingVBPaymentViewModel viewModel2 = new GarmentShippingVBPaymentViewModel
            {
                paymentType = "forwarder",
                forwarder = new Forwarder { id = 0 },
                buyer = new Buyer { Id = 0 }

            };

            var result2 = viewModel2.Validate(null);
            Assert.NotEmpty(result2.ToList());
        }

        [Fact]
        public void Validate_ItemsDefaultValue()
        {
            GarmentShippingVBPaymentViewModel viewModel = new GarmentShippingVBPaymentViewModel();
            viewModel.units = new List<GarmentShippingVBPaymentUnitViewModel>
            {
                new GarmentShippingVBPaymentUnitViewModel()
            };
            viewModel.invoices = new List<GarmentShippingVBPaymentInvoiceViewModel>
            {
                new GarmentShippingVBPaymentInvoiceViewModel()
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Validate_ItemsEmptyValue()
        {
            GarmentShippingVBPaymentViewModel viewModel = new GarmentShippingVBPaymentViewModel();
            viewModel.invoices = new List<GarmentShippingVBPaymentInvoiceViewModel>
            {
                new GarmentShippingVBPaymentInvoiceViewModel
                {
                    invoiceId=0,
                }
            };
            viewModel.units = new List<GarmentShippingVBPaymentUnitViewModel>
            {
                new GarmentShippingVBPaymentUnitViewModel
                {
                    unit=new Unit
                    {
                        Id=0,
                    },
                    billValue=0
                }
            };

            var result = viewModel.Validate(null);
            Assert.NotEmpty(result.ToList());
        }
    }
}
