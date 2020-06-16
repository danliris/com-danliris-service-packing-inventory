using Com.Danliris.Service.Packing.Inventory.Application.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class ProductControllerTest
    {
        private ProductController GetController(IProductService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new ProductController(service, identityProvider)
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
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }

        [Fact]
        public void Should_Success_CreateProductPackAndSKUValidator()
        {
            CreateProductPackAndSKUValidator validator = new CreateProductPackAndSKUValidator();
           
            Assert.NotNull(validator);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = new CreateProductPackAndSKUViewModel();
            //v
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.CreateProductPackAndSKU(It.IsAny<CreateProductPackAndSKUViewModel>())).ReturnsAsync(new ProductPackingBarcodeInfo("", 1, 1, "", "", 1, ""));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task Post_Return_BadRequest()
        {
            var dataUtil = new CreateProductPackAndSKUViewModel();
            //v
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.CreateProductPackAndSKU(It.IsAny<CreateProductPackAndSKUViewModel>())).ReturnsAsync(new ProductPackingBarcodeInfo("", 1, 1, "", "", 1, ""));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "ErrorMessage");

            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }
    }
}
