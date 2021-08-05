using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.PaymentDispositionRecap;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapControllerGetPDFTest
    {
        protected GarmentShippingPaymentDispositionRecapController GetController(IPaymentDispositionRecapService service, IIdentityProvider identityProvider, IValidateService validateService, IGarmentShippingInvoiceService invoiceService, IGarmentPackingListService packingListService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentShippingPaymentDispositionRecapController(service, identityProvider, validateService)
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

        protected virtual PaymentDispositionRecapViewModel viewModel
        {
            get
            {
                return new PaymentDispositionRecapViewModel()
                {
                    date = DateTimeOffset.Now,
                    emkl = new EMKL
                    {
                        Id = 1,
                        Name = "as",
                        Code = "ad",
                    },
                    recapNo = "gsdh",
                    items = new List<PaymentDispositionRecapItemViewModel>()
                    {
                        new PaymentDispositionRecapItemViewModel
                        {
                            service=1000,
                            paymentDisposition= new GarmentShippingPaymentDispositionViewModel
                            {
                                amount=1000,
                                billValue = 10,
                                dispositionNo = "test",
                                invoiceDate = DateTimeOffset.Now,
                                invoiceNumber = "test",
                                vatValue = 10,
                                amountPerUnit=new Dictionary<string, double>()
                                {
                                    ["C1A"]=1000,
                                    ["C2A"]=1000,
                                    ["C1B"]=1000,
                                    ["C2B"]=1000,
                                },
                                invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                                {
                                    new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                                    {
                                        chargeableWeight=1,
                                        grossWeight=1,
                                        invoiceId=1,
                                        invoiceNo="test",
                                        quantity=1,
                                        totalCarton=1,
                                        volume=1,
                                        packingList=new PackingList
                                        {
                                            totalCBM=100
                                        },
                                        invoice= new Invoice()
                                        {
                                            BuyerAgent= new BuyerAgent
                                            {
                                                Id = '1',
                                                Code = "aa",
                                                Name = "aa"
                                            },
                                            unit= "2A",
                                            items=new List<InvoiceItem>()
                                            {
                                                new InvoiceItem
                                                {
                                                    quantity=10,
                                                    unit="2A"
                                                }
                                            }
                                        }
                                    }
                                },
                                incomeTaxValue=122,
                                paid=10000,
                                
                            },
                            
                        },
                        new PaymentDispositionRecapItemViewModel
                        {
                            service=1000,
                            paymentDisposition= new GarmentShippingPaymentDispositionViewModel
                            {
                                amount=1000,
                                billValue = 10,
                                dispositionNo = "test",
                                invoiceDate = DateTimeOffset.Now,
                                invoiceNumber = "test",
                                vatValue = 10,
                                amountPerUnit=new Dictionary<string, double>()
                                {
                                    ["C1A"]=1000,
                                    ["C2A"]=1000,
                                    ["C1B"]=1000,
                                    ["C2B"]=1000,
                                },
                                invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                                {
                                    new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                                    {
                                        chargeableWeight=1,
                                        grossWeight=1,
                                        invoiceId=1,
                                        invoiceNo="test",
                                        quantity=1,
                                        totalCarton=1,
                                        volume=1,
                                        packingList=new PackingList
                                        {
                                            totalCBM=100
                                        },
                                        invoice= new Invoice()
                                        {
                                            BuyerAgent= new BuyerAgent
                                            {
                                                Id = '1',
                                                Code = "aa",
                                                Name = "aa"
                                            },
                                            unit= "2A",
                                            items=new List<InvoiceItem>()
                                            {
                                                new InvoiceItem
                                                {
                                                    quantity=10,
                                                    unit="2A"
                                                }
                                            }
                                        }
                                    }
                                },
                                incomeTaxValue=122,
                                paid=10000,

                            },

                        },
                        new PaymentDispositionRecapItemViewModel
                        {
                            service=1000,
                            paymentDisposition= new GarmentShippingPaymentDispositionViewModel
                            {
                                amount=1000,
                                billValue = 10,
                                dispositionNo = "test",
                                invoiceDate = DateTimeOffset.Now,
                                invoiceNumber = "test1",
                                vatValue = 10,
                                amountPerUnit=new Dictionary<string, double>()
                                {
                                    ["C2C"]=1000,
                                },
                                invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                                {
                                    new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                                    {
                                        chargeableWeight=1,
                                        grossWeight=1,
                                        invoiceId=1,
                                        invoiceNo="test",
                                        quantity=1,
                                        totalCarton=1,
                                        volume=1,
                                        packingList=new PackingList
                                        {
                                            totalCBM=100
                                        },
                                        invoice= new Invoice()
                                        {
                                            BuyerAgent= new BuyerAgent
                                            {
                                                Id = '1',
                                                Code = "aa",
                                                Name = "aa"
                                            },
                                            unit= "2A",
                                            items=new List<InvoiceItem>()
                                            {
                                                new InvoiceItem
                                                {
                                                    quantity=10,
                                                    unit="1A"
                                                }
                                            }

                                        }
                                    },
                                    new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                                    {
                                        chargeableWeight=1,
                                        grossWeight=1,
                                        invoiceId=1,
                                        invoiceNo="test",
                                        quantity=1,
                                        totalCarton=1,
                                        volume=1,
                                        packingList=new PackingList
                                        {
                                            totalCBM=100
                                        },
                                        invoice= new Invoice()
                                        {
                                            BuyerAgent= new BuyerAgent
                                            {
                                                Id = '1',
                                                Code = "aa",
                                                Name = "aa"
                                            },
                                            unit= "2A",
                                            items=new List<InvoiceItem>()
                                            {
                                                new InvoiceItem
                                                {
                                                    quantity=10,
                                                    unit="1A"
                                                }
                                            }

                                        }
                                    }
                                },
                                incomeTaxValue=122,
                                paid=10000,

                            },

                        },
                    }
                };
            }
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

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService); 
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(PaymentDispositionRecapViewModel));
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingPaymentDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
