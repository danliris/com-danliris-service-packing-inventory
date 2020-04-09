using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
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
    public class FabricQualityControlControllerTest
    {
        private FabricQualityControlController GetController(IFabricQualityControlService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new FabricQualityControlController(service, identityProvider)
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

        private FabricQualityControlViewModel ViewModel
        {
            get
            {
                return new FabricQualityControlViewModel()
                {
                    Buyer = "buyer",
                    CartNo = "cartno",
                    Code = "code",
                    Color = "color",
                    Construction = "construction",
                    DateIm = DateTimeOffset.UtcNow,
                    DyeingPrintingAreaMovementBonNo = "no",
                    DyeingPrintingAreaMovementId = 1,
                    Group = "group",
                    IsUsed = false,
                    MachineNoIm = "no",
                    OperatorIm = "np",
                    OrderQuantity = 1,
                    PackingInstruction = "s",
                    PointLimit = 1,
                    PointSystem = 10,
                    ProductionOrderNo = "np",
                    ProductionOrderType = "type",
                    ShiftIm = "shif",
                    Uom = "mtr",
                    FabricGradeTests = new List<FabricGradeTestViewModel>()
                    {
                        new FabricGradeTestViewModel()
                        {
                            AvalLength = 1,
                            PointSystem =10,
                            FabricGradeTest = 1,
                            FinalArea = 1,
                            FinalLength = 1,
                            FinalGradeTest = 1,
                            FinalScore = 1,
                            Grade = "a",
                            InitLength = 2,
                            PcsNo = "np",
                            PointLimit = 1,
                            SampleLength = 1,
                            Score =1,
                            Type = "t",
                            Width = 1,
                            Criteria = new List<CriteriaViewModel>()
                            {
                                new CriteriaViewModel()
                                {
                                    Code = "c",
                                    Group = "a",
                                    Score = new Score()
                                    {
                                        A = 1,
                                        B = 1,
                                        C = 1,
                                        D = 1
                                    },
                                    Index = 1,
                                    Name = "na"
                                }
                            }
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new FabricQualityControlViewModel();
            var validator = new FabricQualityControlValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Create(It.IsAny<FabricQualityControlViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Post()
        {
            var dataUtil = new FabricQualityControlViewModel();
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Create(It.IsAny<FabricQualityControlViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Create(It.IsAny<FabricQualityControlViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<FabricQualityControlViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Put()
        {
            var dataUtil = new FabricQualityControlViewModel();
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<FabricQualityControlViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<FabricQualityControlViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
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
            var serviceMock = new Mock<IFabricQualityControlService>();
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
            var serviceMock = new Mock<IFabricQualityControlService>();
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
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(FabricQualityControlViewModel));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(FabricQualityControlViewModel));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetReport()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new ListResult<FabricQualityControlViewModel>(new List<FabricQualityControlViewModel>() { ViewModel }, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetReport();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exceptipn_GetReport()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetReport();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetXls()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls();

            Assert.NotNull(response);

            response = controller.GetXls(dateFrom:DateTime.UtcNow);

            Assert.NotNull(response);

            response = controller.GetXls(dateTo: DateTime.UtcNow);

            Assert.NotNull(response);

            response = controller.GetXls(dateFrom: DateTime.UtcNow, dateTo:DateTime.UtcNow);

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetXls()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetForSPP()
        {
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GetForSPP(It.IsAny<string>()))
                .Returns(new List<FabricQCGradeTestsViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetForSPP(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetForSPP()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IFabricQualityControlService>();
            serviceMock.Setup(s => s.GetForSPP(It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetForSPP(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
