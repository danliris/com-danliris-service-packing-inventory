using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class GoodsWarehouseDocumentsServiceTest
    {
        private GoodsWarehouseDocumentsController GetController(IGoodsWarehouseDocumentsService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GoodsWarehouseDocumentsController(service, identityProvider)
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

        private IndexViewModel ViewModel
        {
            get
            {
                return new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020, 04, 04)),
                    Group = "PAGI",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "Order001",
                    Construction = "PC 001 010",
                    Motif = "Batik",
                    Color = "Biru",
                    Grade = "A",
                    QtyPacking = "1 Rolls",
                    Qty = "1",
                    Packaging = "Rolls",
                    Satuan = "Meter",
                    Balance = "10.000",
                    Zona = "PROD"
                };
            }
        }

        [Fact]
        public void Should_Get_Success()
        {
            var serviceMock = new Mock<IGoodsWarehouseDocumentsService>();
            serviceMock.Setup(s => s.GetList(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<IndexViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.GetAll(null);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Get_Exception()
        {
            var serviceMock = new Mock<IGoodsWarehouseDocumentsService>();
            serviceMock.Setup(s => s.GetList(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.GetAll(new DateTimeOffset(new DateTime(2020,1,1)),"test","test");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_GetXls_Success()
        {
            var serviceMock = new Mock<IGoodsWarehouseDocumentsService>();
            serviceMock.Setup(s => s.GetExcel(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDataByExcel(null);

            Assert.NotNull(response);
        }
        [Fact]
        public void Should_GetXls_Exception()
        {
            var serviceMock = new Mock<IGoodsWarehouseDocumentsService>();
            serviceMock.Setup(s => s.GetExcel(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDataByExcel(null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }
    }
}
