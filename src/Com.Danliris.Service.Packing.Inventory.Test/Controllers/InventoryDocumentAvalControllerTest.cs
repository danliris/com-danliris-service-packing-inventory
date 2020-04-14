using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval;
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
    public class InventoryDocumentAvalControllerTest
    {
        private InventoryDocumentAvalController GetController(IInventoryDocumentAvalService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new InventoryDocumentAvalController(service, identityProvider)
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

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private InventoryDocumentAvalViewModel ViewModel
        {
            get
            {
                return new InventoryDocumentAvalViewModel()
                {
                    Date = DateTimeOffset.UtcNow,
                    BonNo = "IM.20.0002",
                    Shift = "PAGI",
                    UOMUnit = "MTR",
                    ProductionOrderQuantity = 2500,
                    QtyKg = 2
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new InventoryDocumentAvalViewModel();
            var validator = new InventoryDocumentAvalValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Post()
        {
            var dataUtil = new InventoryDocumentAvalViewModel();
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        //[Fact]
        //public async Task Should_Success_Put()
        //{
        //    var dataUtil = ViewModel;
        //    //v
        //    var serviceMock = new Mock<IInventoryDocumentAvalService>();
        //    serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ReturnsAsync(1);
        //    var service = serviceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider);
        //    //controller.ModelState.IsValid == false;
        //    var response = await controller.Put(dataUtil.Id, dataUtil);

        //    Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        //}

        //[Fact]
        //public async Task Should_NotValid_Put()
        //{
        //    var dataUtil = new InventoryDocumentAvalViewModel();
        //    //v
        //    var serviceMock = new Mock<IInventoryDocumentAvalService>();
        //    serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ReturnsAsync(1);
        //    var service = serviceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider);
        //    controller.ModelState.AddModelError("test", "test");
        //    //controller.ModelState.IsValid == false;
        //    var response = await controller.Put(dataUtil.Id, dataUtil);

        //    Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        //}

        //[Fact]
        //public async Task Should_Exception_Put()
        //{
        //    var dataUtil = ViewModel;
        //    //v
        //    var serviceMock = new Mock<IInventoryDocumentAvalService>();
        //    serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<InventoryDocumentAvalViewModel>())).ThrowsAsync(new Exception());
        //    var service = serviceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider);
        //    //controller.ModelState.IsValid == false;
        //    var response = await controller.Put(dataUtil.Id, dataUtil);

        //    Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        //}

        [Fact]
        public async Task Should_Success_GetById()
        {
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
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
            var serviceMock = new Mock<IInventoryDocumentAvalService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
