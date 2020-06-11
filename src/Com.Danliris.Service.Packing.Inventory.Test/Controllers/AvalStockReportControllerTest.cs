using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
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
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class AvalStockReportControllerTest
    {
        private AvalStockReportController GetController(IAvalStockReportService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new AvalStockReportController(service, identityProvider)
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

        private AvalStockReportViewModel ViewModel
        {
            get
            {
                return new AvalStockReportViewModel()
                {
                    AvalType = "tupe",
                    EndAvalQuantity = 1,
                    EndAvalWeightQuantity = 1,
                    InAvalQuantity = 2,
                    InAvalWeightQuantity = 2,
                    OutAvalQuantity = 1,
                    OutAvalWeightQuantity = 1,
                    StartAvalQuantity = 0,
                    StartAvalWeightQuantity = 0
                };
            }
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IAvalStockReportService>();
            serviceMock.Setup(s => s.GetReportData(It.IsAny<DateTimeOffset>()))
                .Returns(new ListResult<AvalStockReportViewModel>(new List<AvalStockReportViewModel>() { ViewModel }, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;


            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get(DateTimeOffset.UtcNow);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            //v
            var serviceMock = new Mock<IAvalStockReportService>();
            serviceMock.Setup(s => s.GetReportData(It.IsAny<DateTimeOffset>()))
                .Returns(new ListResult<AvalStockReportViewModel>(new List<AvalStockReportViewModel>() { ViewModel }, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;


            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get(null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetExcel()
        {
            //v
            var serviceMock = new Mock<IAvalStockReportService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;


            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcel(DateTimeOffset.UtcNow);
            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetExcel()
        {
            //v
            var serviceMock = new Mock<IAvalStockReportService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;


            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcel(null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
