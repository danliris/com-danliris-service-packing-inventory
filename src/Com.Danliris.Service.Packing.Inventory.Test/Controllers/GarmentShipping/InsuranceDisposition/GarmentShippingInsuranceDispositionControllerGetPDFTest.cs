using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.InsuranceDisposition;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionControllerGetPDFTest
    {
        protected GarmentShippingInsuranceDispositionController GetController(IGarmentShippingInsuranceDispositionService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentShippingInsuranceDispositionController(service, identityProvider, validateService)
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
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(ViewModelPiutang, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private Insurance insuranceVm
        {
            get
            {
                return new Insurance()
                {
                    Id = 1,
                    Name = "aa",
                    Code = "aa",
                    SwiftCode = "aa",
                    AccountNumber = "aa",
                    
                };
            }
        }

        protected virtual GarmentShippingInsuranceDispositionViewModel ViewModelPiutang
        {
            get
            {
                return new GarmentShippingInsuranceDispositionViewModel()
                {
                    paymentDate=DateTimeOffset.Now,
                    policyType="Piutang",
                    dispositionNo="dispo",
                    rate=1,
                    insurance=new Insurance
                    {
                        Id=1,
                        Name="name",
                        Code="code"
                    },
                    bankName="sda",
                    items = new List<GarmentShippingInsuranceDispositionItemViewModel>() {
                        new GarmentShippingInsuranceDispositionItemViewModel()
                        {
                            amount=12.34m,
                            BuyerAgent=new BuyerAgent
                            {
                                Name="aa",
                                Code="aa",
                                Id=1
                            },
                            invoiceId=1,
                            invoiceNo="aa",
                            policyDate=DateTimeOffset.Now,
                            policyNo="aa",
                        }
                    }
                };
            }
        }
        protected virtual GarmentShippingInsuranceDispositionViewModel ViewModelKargo
        {
            get
            {
                return new GarmentShippingInsuranceDispositionViewModel()
                {
                    paymentDate = DateTimeOffset.Now,
                    policyType = "Kargo",
                    dispositionNo = "dispo",
                    rate = 1,
                    insurance = new Insurance
                    {
                        Id = 1,
                        Name = "name",
                        Code = "code"
                    },
                    bankName = "sda",
                    items = new List<GarmentShippingInsuranceDispositionItemViewModel>() {
                        new GarmentShippingInsuranceDispositionItemViewModel()
                        {
                            amount=12.34m,
                            BuyerAgent=new BuyerAgent
                            {
                                Name="aa",
                                Code="aa",
                                Id=1
                            },
                            invoiceId=1,
                            invoiceNo="aa",
                            policyDate=DateTimeOffset.Now,
                            policyNo="aa",
                            currencyRate=1.2m,
                            amount1A=1,
                            amount1B=2,
                            amount2A=3,
                            amount2B=4,
                            amount2C=5,
                            
                        }
                    }
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInsuranceDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModelKargo);
            serviceMock.Setup(s => s.GetInsurance(It.IsAny<int>())).Returns(insuranceVm);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInsuranceDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_Piutang()
        {
            var serviceMock = new Mock<IGarmentShippingInsuranceDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModelPiutang);
            serviceMock.Setup(s => s.GetInsurance(It.IsAny<int>())).Returns(insuranceVm);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInsuranceDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInsuranceDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInsuranceDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInsuranceDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingInsuranceDispositionViewModel));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInsuranceDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInsuranceDispositionService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInsuranceDispositionViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}
