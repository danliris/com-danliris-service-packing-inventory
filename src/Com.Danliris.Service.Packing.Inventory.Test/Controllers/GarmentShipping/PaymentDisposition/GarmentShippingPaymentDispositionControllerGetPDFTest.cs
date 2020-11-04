using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.PaymentDisposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionControllerGetPDFTest
    {
        protected GarmentShippingPaymentDispositionController GetController(IGarmentShippingPaymentDispositionService service, IIdentityProvider identityProvider, IValidateService validateService, IGarmentShippingInvoiceService invoiceService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentShippingPaymentDispositionController(service, identityProvider, validateService, invoiceService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = claimPrincipal.Object

                    }
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }

        protected ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        protected virtual GarmentShippingPaymentDispositionViewModel viewModel
        {
            get
            {
                return new GarmentShippingPaymentDispositionViewModel()
                {
                    paymentType="FORWARDER",
                    forwarder=new Forwarder
                    {
                        id=1,
                        name="as",
                        code="ad",
                    },
                    accNo="sad",
                    address="test",
                    bank= "test",
                    billValue=10,
                    buyerAgent=new BuyerAgent
                    {
                        Name= "test",
                        Code= "test",
                        Id=1
                    },
                    dispositionNo= "test",
                    flightVessel= "test",
                    freightBy= "test",
                    freightDate=DateTimeOffset.Now,
                    freightNo= "test",
                    incomeTax=new IncomeTax
                    {
                        name= "test",
                        rate=1
                    },
                    invoiceDate=DateTimeOffset.Now,
                    invoiceNumber= "test",
                    invoiceTaxNumber= "test",
                    IncomeTaxValue=10,
                    isFreightCharged=false,
                    npwp= "test",
                    paidAt= "test",
                    sendBy= "test",
                    paymentDate=DateTimeOffset.Now,
                    paymentMethod= "test",
                    paymentTerm= "test",
                    remark= "test",
                    totalBill=10,
                    vatValue=10,
                    billDetails= new List<GarmentShippingPaymentDispositionBillDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionBillDetailViewModel
                        {
                            billDescription="test",
                            amount=100
                        }
                    },
                    unitCharges=new List<GarmentShippingPaymentDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingPaymentDispositionUnitChargeViewModel
                        {
                            amountPercentage=100,
                            billAmount=100,
                            unit=new Unit
                            {
                                Code="test",
                                Id=1
                            }
                        }
                    },
                    invoiceDetails=new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                        {
                            amount=100,
                            chargeableWeight=1,
                            grossWeight=1,
                            invoiceId=1,
                            invoiceNo="test",
                            quantity=1,
                            totalCarton=1,
                            volume=1,
                            
                        }
                    }
                };
            }
        }

        protected virtual GarmentShippingPaymentDispositionViewModel viewModelFFC
        {
            get
            {
                return new GarmentShippingPaymentDispositionViewModel()
                {
                    paymentType = "FORWARDER",
                    forwarder = new Forwarder
                    {
                        id = 1,
                        name = "as",
                        code = "ad",
                    },
                    accNo = "sad",
                    address = "test",
                    bank = "test",
                    billValue = 10,
                    buyerAgent = new BuyerAgent
                    {
                        Name = "test",
                        Code = "test",
                        Id = 1
                    },
                    dispositionNo = "test",
                    flightVessel = "test",
                    freightBy = "test",
                    freightDate = DateTimeOffset.Now,
                    freightNo = "test",
                    incomeTax = new IncomeTax
                    {
                        name = "test",
                        rate = 1
                    },
                    invoiceDate = DateTimeOffset.Now,
                    invoiceNumber = "test",
                    invoiceTaxNumber = "test",
                    IncomeTaxValue = 10,
                    isFreightCharged = true,
                    npwp = "test",
                    paidAt = "test",
                    sendBy = "test",
                    paymentDate = DateTimeOffset.Now,
                    paymentMethod = "test",
                    paymentTerm = "test",
                    remark = "test",
                    totalBill = 10,
                    vatValue = 10,
                    billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionBillDetailViewModel
                        {
                            billDescription="test",
                            amount=100
                        }
                    },
                    unitCharges = new List<GarmentShippingPaymentDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingPaymentDispositionUnitChargeViewModel
                        {
                            amountPercentage=100,
                            billAmount=100,
                            unit=new Unit
                            {
                                Code="test",
                                Id=1
                            }
                        }
                    },
                    invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                        {
                            amount=100,
                            chargeableWeight=1,
                            grossWeight=1,
                            invoiceId=1,
                            invoiceNo="test",
                            quantity=1,
                            totalCarton=1,
                            volume=1,

                        }
                    }
                };
            }
        }
        protected virtual GarmentShippingPaymentDispositionViewModel viewModelCourier
        {
            get
            {
                return new GarmentShippingPaymentDispositionViewModel()
                {
                    paymentType = "COURIER",
                    courier = new Courier
                    {
                        Id = 1,
                        Name = "as",
                        Code = "ad",
                    },
                    accNo = "sad",
                    address = "test",
                    bank = "test",
                    billValue = 10,
                    buyerAgent = new BuyerAgent
                    {
                        Name = "test",
                        Code = "test",
                        Id = 1
                    },
                    dispositionNo = "test",
                    flightVessel = "test",
                    freightBy = "test",
                    freightDate = DateTimeOffset.Now,
                    freightNo = "test",
                    incomeTax = new IncomeTax
                    {
                        name = "test",
                        rate = 1
                    },
                    invoiceDate = DateTimeOffset.Now,
                    invoiceNumber = "test",
                    invoiceTaxNumber = "test",
                    IncomeTaxValue = 10,
                    isFreightCharged = false,
                    npwp = "test",
                    paidAt = "test",
                    sendBy = "test",
                    paymentDate = DateTimeOffset.Now,
                    paymentMethod = "test",
                    paymentTerm = "test",
                    remark = "test",
                    totalBill = 10,
                    vatValue = 10,
                    billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionBillDetailViewModel
                        {
                            billDescription="test",
                            amount=100
                        }
                    },
                    unitCharges = new List<GarmentShippingPaymentDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingPaymentDispositionUnitChargeViewModel
                        {
                            amountPercentage=100,
                            billAmount=100,
                            unit=new Unit
                            {
                                Code="test",
                                Id=1
                            }
                        }
                    },
                    invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                        {
                            amount=100,
                            chargeableWeight=1,
                            grossWeight=1,
                            invoiceId=1,
                            invoiceNo="test",
                            quantity=1,
                            totalCarton=1,
                            volume=1,

                        }
                    }
                };
            }
        }
        protected virtual GarmentShippingPaymentDispositionViewModel viewModelEMKL
        {
            get
            {
                return new GarmentShippingPaymentDispositionViewModel()
                {
                    paymentType = "EMKL",
                    emkl = new EMKL
                    {
                        Id = 1,
                        Name = "as",
                        Code = "ad",
                    },
                    accNo = "sad",
                    address = "test",
                    bank = "test",
                    billValue = 10,
                    buyerAgent = new BuyerAgent
                    {
                        Name = "test",
                        Code = "test",
                        Id = 1
                    },
                    dispositionNo = "test",
                    flightVessel = "test",
                    freightBy = "test",
                    freightDate = DateTimeOffset.Now,
                    freightNo = "test",
                    incomeTax = new IncomeTax
                    {
                        name = "test",
                        rate = 1
                    },
                    invoiceDate = DateTimeOffset.Now,
                    invoiceNumber = "test",
                    invoiceTaxNumber = "test",
                    IncomeTaxValue = 10,
                    isFreightCharged = false,
                    npwp = "test",
                    paidAt = "test",
                    sendBy = "test",
                    paymentDate = DateTimeOffset.Now,
                    paymentMethod = "test",
                    paymentTerm = "test",
                    remark = "test",
                    totalBill = 10,
                    vatValue = 10,
                    billDetails = new List<GarmentShippingPaymentDispositionBillDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionBillDetailViewModel
                        {
                            billDescription="test",
                            amount=100
                        }
                    },
                    unitCharges = new List<GarmentShippingPaymentDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingPaymentDispositionUnitChargeViewModel
                        {
                            amountPercentage=100,
                            billAmount=100,
                            unit=new Unit
                            {
                                Code="test",
                                Id=1
                            }
                        }
                    },
                    invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                        {
                            amount=100,
                            chargeableWeight=1,
                            grossWeight=1,
                            invoiceId=1,
                            invoiceNo="test",
                            quantity=1,
                            totalCarton=1,
                            volume=1,

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel invoiceVM
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "no",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",
                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            },
                            Comodity=new Comodity
                            {
                                Name="asaa",
                                Code="sdba",
                                Id=1
                            }

                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            },
                            Comodity=new Comodity
                            {
                                Name="asaa",
                                Code="sdba",
                                Id=1
                            }

                        }
                    },
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_FFC()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModelFFC);
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_COURIER()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModelCourier);
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_EMKL()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModelEMKL);
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingPaymentDispositionViewModel));
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingPaymentDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            invoiceServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(invoiceVM);
            var invoiceService = invoiceServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
