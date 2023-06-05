using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdviceMII;
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
    public class GarmentCreditAdviceMIIMonitoringControllerTest
    {
        protected GarmentCreditAdviceMIIMonitoringController GetController(IGarmentCreditAdviceMIIMonitoringService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentCreditAdviceMIIMonitoringController(service, identityProvider)
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
            var serviceMock = new Mock<IGarmentCreditAdviceMIIMonitoringService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns(new ListResult<GarmentCreditAdviceMIIMonitoringViewModel>(new List<GarmentCreditAdviceMIIMonitoringViewModel>() { new GarmentCreditAdviceMIIMonitoringViewModel() }, 1, 1, 1));

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.GetReport(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>(), "{}");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IGarmentCreditAdviceMIIMonitoringService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.GetReport(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>(), "{}");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetGenerateExcel_Success()
        {
            var serviceMock = new Mock<IGarmentCreditAdviceMIIMonitoringService>();
            serviceMock
                .Setup(s => s.GenerateExcel(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = controller.GetXls(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            Assert.NotNull(response);
        }

        [Fact]
        public void GetGenerateExcel_Error()
        {
            var serviceMock = new Mock<IGarmentCreditAdviceMIIMonitoringService>();
            serviceMock
                .Setup(s => s.GenerateExcel(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = controller.GetXls(null, null);

            Assert.NotNull(response);
        }
    }
}
