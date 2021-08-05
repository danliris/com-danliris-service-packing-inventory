using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
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
    public class DyeingPrintingProductControllerTest
    {
        private DyeingPrintingProductController GetController(IDyeingPrintingProductService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new DyeingPrintingProductController(service, identityProvider)
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

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private DyeingPrintingProductPackingViewModel ViewModel
        {
            get
            {
                return new DyeingPrintingProductPackingViewModel()
                {
                    Color = "c",
                    FabricPackingId = 1,
                    FabricSKUId = 1,
                    HasPrintingProductPacking = true,
                    HasPrintingProductSKU = true,
                    Id = 1,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialWidth = "1",
                    Motif = "s",
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = 1,
                        No = "s"
                    },
                    ProductPackingCodes = new List<string>() { "s" },
                    ProductPackingId = 1,
                    ProductSKUCode = "s",
                    ProductSKUId = 1,
                    UomUnit = "s",
                    YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = 1,
                        Name = "s"
                    }
                };
            }
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IDyeingPrintingProductService>();
            serviceMock.Setup(s => s.GetDataProductPacking(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<DyeingPrintingProductPackingViewModel>(new List<DyeingPrintingProductPackingViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IDyeingPrintingProductService>();
            serviceMock.Setup(s => s.GetDataProductPacking(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;


            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put_HasPrintingProductPacking()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IDyeingPrintingProductService>();
            serviceMock.Setup(s => s.UpdatePrintingStatusProductPacking(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.UpdatePrintingStatusProductPacking(1, true);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Put_HasPrintingProductPacking()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IDyeingPrintingProductService>();
            serviceMock.Setup(s => s.UpdatePrintingStatusProductPacking(It.IsAny<int>(), It.IsAny<bool>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.UpdatePrintingStatusProductPacking(1, true);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
