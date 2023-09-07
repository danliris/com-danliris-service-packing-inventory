using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingReport;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.DyeingPrintingReport
{
    public class OrderStatusReportControllerTest
    {
        protected OrderStatusReportController GetController(IOrderStatusReportService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new OrderStatusReportController(service, identityProvider)
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
        public async void GetReportData_Ok()
        {
            var serviceMock = new Mock<IOrderStatusReportService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .ReturnsAsync(new List<OrderStatusReportViewModel>());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = await controller.GetReport(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void Get_Exception_InternalServerError()
        {
            var serviceMock = new Mock<IOrderStatusReportService>();
            serviceMock
                .Setup(s => s.GetReportData(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response =await controller.GetReport(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void GetGenerateExcel_Success()
        {
            var serviceMock = new Mock<IOrderStatusReportService>();
            serviceMock
                .Setup(s => s.GenerateExcel( It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new MemoryStream());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = controller.GetXls(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void GetGenerateExcel_Error()
        {
            var serviceMock = new Mock<IOrderStatusReportService>();
            serviceMock
                .Setup(s => s.GenerateExcel(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new MemoryStream());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";
            var response = controller.GetXls(It.IsAny<DateTime>(), It.IsAny<DateTime>(),0);

            Assert.NotNull(response);
        }
    }
}
