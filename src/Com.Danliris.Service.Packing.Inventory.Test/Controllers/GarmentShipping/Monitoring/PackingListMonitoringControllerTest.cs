using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.Monitoring
{
    public class PackingListMonitoringControllerTest
    {
        protected GarmentPackingListMonitoringController GetController(IGarmentPackingListMonitoringService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentPackingListMonitoringController(service, identityProvider)
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

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public void GetReportData_Ok()
        {
            var serviceMock = new Mock<IGarmentPackingListMonitoringService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new ListResult<GarmentPackingListMonitoringViewModel>(new List<GarmentPackingListMonitoringViewModel>() { new GarmentPackingListMonitoringViewModel() }, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.Get(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentPackingListMonitoringService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.Get(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetGenerateExcel_Success()
        {
            var serviceMock = new Mock<IGarmentPackingListMonitoringService>();
            serviceMock
                .Setup(s => s.GenerateExcel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new Application.Utilities.FileResult(new MemoryStream(), "FileName"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = controller.Get(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>());

            Assert.NotNull(response);
        }
    }
}
