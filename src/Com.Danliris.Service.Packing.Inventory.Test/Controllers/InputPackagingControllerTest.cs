using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaInput;
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
    public class InputPackagingControllerTest
    {
        private InputPackagingController GetController(IInputPackagingService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new InputPackagingController(service, identityProvider)
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

        private InputPackagingViewModel ViewModel
        {
            get
            {
                return new InputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow.AddDays(2),
                    Shift = "pas",
                    PackagingProductionOrders = new List<InputPackagingProductionOrdersViewModel>()
                    {
                        new InputPackagingProductionOrdersViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            OutputId = 1,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            IsChecked = false,
                            Motif = "sd",
                            PackingInstruction = "d",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new InputPackagingViewModel();
            var validator = new InputPackagingValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.CreateAsync(It.IsAny<InputPackagingViewModel>())).ReturnsAsync(1);
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
            var dataUtil = new InputPackagingViewModel();
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.CreateAsync(It.IsAny<InputPackagingViewModel>())).ReturnsAsync(1);
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
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.CreateAsync(It.IsAny<InputPackagingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(ViewModel);
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
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());
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
            var serviceMock = new Mock<IInputPackagingService>();
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
            var serviceMock = new Mock<IInputPackagingService>();
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
        public void Should_Success_GetProductionOrders()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadProductionOrders(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<InputPackagingProductionOrdersViewModel>(new List<InputPackagingProductionOrdersViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadProductionOrders(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetListBonInput()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadBonOutToPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetBonInPacking();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetListBonInput()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadBonOutToPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetBonInPacking();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetListSpp()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadInProducionOrders(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<InputPackagingProductionOrdersViewModel>(new List<InputPackagingProductionOrdersViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPacking();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetLIstSpp()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.ReadInProducionOrders(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPacking();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async Task Should_Success_Reject()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Reject(It.IsAny<InputPackagingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Reject(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Reject()
        {
            var dataUtil = new InputPackagingViewModel();
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Reject(It.IsAny<InputPackagingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Reject(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Reject()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Reject(It.IsAny<InputPackagingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Reject(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var dataUtil = ViewModel;
            dataUtil.Id = 1;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.DeleteById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Delete()
        {
            var dataUtil = ViewModel;
            dataUtil.Id = 1;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.DeleteById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async Task Should_Success_Update()
        {
            var dataUtil = ViewModel;
            dataUtil.Id = 1;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), dataUtil)).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1,dataUtil);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Update()
        {
            var dataUtil = ViewModel;
            dataUtil.Id = 1;
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<InputPackagingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetPackingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcelAll(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcelAll("7");

            Assert.NotNull(response);
        }
        [Fact]
        public void Should_Exception_GetPackingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IInputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcelAll(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcelAll("7");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }
    }
}
