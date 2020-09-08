using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.LocalSalesDO;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOControllerGetPdfTest 
    {
        protected GarmentShippingLocalSalesDOController GetController(IGarmentShippingLocalSalesDOService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentShippingLocalSalesDOController(service, identityProvider, validateService)
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
        private GarmentShippingLocalSalesDOViewModel viewModel
        {
            get
            {
                return new GarmentShippingLocalSalesDOViewModel()
                {
                    buyer = new Buyer
                    {
                        Id = 1,
                        Name = "aa"
                    },
                    date = DateTimeOffset.Now,
                    localSalesDONo = "aa",
                    storageDivision="askd",
                    to = "asdla",
                    items = new List<GarmentShippingLocalSalesDOItemViewModel>()
                    {
                        new GarmentShippingLocalSalesDOItemViewModel
                        {
                            quantity=1,
                            cartonQuantity=1,
                            product=new ProductViewModel
                            {
                                name="akdh"
                            },
                            grossWeight=1,
                            nettWeight=1,
                            uom=new UnitOfMeasurement
                            {
                                Unit="kjshd",
                            },
                            volume=1,
                            description="asjhd",
                        }
                    }
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(viewModel);

            var service = serviceMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesDOViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesDOViewModel>()))
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
            var serviceMock = new Mock<IGarmentShippingLocalSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingLocalSalesDOViewModel));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesDOViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingLocalSalesDOService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingLocalSalesDOViewModel>()))
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
