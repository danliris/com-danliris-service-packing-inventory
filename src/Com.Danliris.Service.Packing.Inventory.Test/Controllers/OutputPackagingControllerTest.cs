using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaOutput;
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
    public class OutputPackagingControllerTest
    {
        private OutputPackagingController GetController(IOutputPackagingService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new OutputPackagingController(service, identityProvider)
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

        private OutputPackagingViewModel ViewModel
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Type= "OUT",
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "GUDANG JADI",
                    InputPackagingId = 1,
                    PackagingProductionOrders = new List<OutputPackagingProductionOrderViewModel>()
                    {
                        new OutputPackagingProductionOrderViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
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
                            UomUnit = "d",
                            PackagingQTY = 1,
                            PackagingType="WHITE",
                            PackagingUnit = "ROLLS"
                        }
                    },
                };
            }
        }
        private OutputPackagingViewModel ViewModelAdj
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Type = "ADJ",
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "GUDANG JADI",
                    InputPackagingId = 1,
                    Id = 1,
                    Active = true,
                    BonNoInput ="1" ,
                    Group="a",
                    PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
                    {
                        new InputPlainAdjPackagingProductionOrder()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
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
                            UomUnit = "d",
                            PackagingQty = 1,
                            PackagingType="WHITE",
                            PackagingUnit = "ROLLS"
                        }
                    },
                };
            }
        }
        private PlainAdjPackagingProductionOrder ViewModelAdj1
        {
            get
            {
                return new PlainAdjPackagingProductionOrder()
                {
                    ProductionOrder =new ProductionOrder() {
                        Code = "asf",
                        Id =0 ,
                        No ="asdf",
                        OrderQuantity =10,
                        Type="asdf"
                    },
                    Material =new Material() {
                        Code="asdf",
                        Id=0,
                        Name="adsf"
                    },
                    MaterialConstruction =new MaterialConstruction (){
                        Code="adsf",
                        Id=0,
                        Name="asdf"
                    },
                    MaterialWidth ="",
                    Area ="",
                    CartNo ="",
                    PackingInstruction ="",
                    Construction ="",
                    Unit ="",
                    BuyerId =1,
                    Buyer ="",
                    Color ="",
                    Motif ="",
                    UomUnit ="",
                    Balance =1,
                    HasOutputDocument =false,
                    IsChecked =true,
                    Grade ="",
                    Remark ="",
                    Status ="",

                    BalanceRemains =1,

                    PreviousBalance =1,

                    OutputId =1,

                    InputId =1,
                    PackagingType ="",
                    PackagingUnit ="",
                    PackagingQTY =1,

                    DyeingPrintingAreaInputProductionOrderId =1,

                    DyeingPrintingAreaOutputProductionOrderId =1,
                    AtQty =1,
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new OutputPackagingViewModel();
            var validator = new OutputPackagingValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputPackagingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }
        [Fact]
        public async Task Should_Success_Post_Adj()
        {
            var dataUtil = ViewModelAdj;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.CreateAdj(It.IsAny<OutputPackagingViewModel>())).ReturnsAsync(1);
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
            var dataUtil = new OutputPackagingViewModel();
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputPackagingViewModel>())).ReturnsAsync(1);
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
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.CreateV2(It.IsAny<OutputPackagingViewModel>())).ThrowsAsync(new Exception());
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
            var serviceMock = new Mock<IOutputPackagingService>();
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
            var serviceMock = new Mock<IOutputPackagingService>();
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
            var serviceMock = new Mock<IOutputPackagingService>();
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
            var serviceMock = new Mock<IOutputPackagingService>();
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
        public async Task Should_Success_GetIMAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetTransitAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPackingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcelAll())
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcelAll();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPackingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GenerateExcelAll())
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcelAll();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }

        [Fact]
        public void Should_Success_GetListBonOut()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadBonOutFromPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
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
        public void Should_Success_GetDistinctProductionOrder()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<PlainAdjPackagingProductionOrder>(new List<PlainAdjPackagingProductionOrder>() { ViewModelAdj1}, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exceptions_GetDistinctProductionOrder()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                //.Returns(new ListResult<PlainAdjPackagingProductionOrder>(new List<PlainAdjPackagingProductionOrder>() { ViewModelAdj1 }, 1, 1, 1));
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_ReadSPPGrouped()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPackGroup(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<OutputPackagingProductionOrderGroupedViewModel>(new List<OutputPackagingProductionOrderGroupedViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPackingGroup();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_ReadSPPSumBySppNO()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPackSumBySPPNo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging.InputPackagingProductionOrdersViewModel>(new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging.InputPackagingProductionOrdersViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPackingSum();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_ReadSPPGrouped()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPackGroup(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPackingGroup();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }
        [Fact]
        public void Should_Exception_GetListBonOut()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadBonOutFromPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetBonInPacking();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_GetListSPPInput()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPacking();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetListSPPSumBySppNo()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPackSumBySPPNo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPackingSum();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_GetListSPPInput()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.ReadSppInFromPack(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging.InputPackagingProductionOrdersViewModel>(new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging.InputPackagingProductionOrdersViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSppInPacking();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Exception_Delete()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.DeleteV2(It.IsAny<int>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var serviceMock = new Mock<IOutputPackagingService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetSet_GrouppedSPP()
        {
            var model = new OutputPackagingProductionOrderGroupedViewModel();
            model.ProductionOrder = "test";
            model.ProductionOrderList = new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging.InputPackagingProductionOrdersViewModel>();

            var test = model.ProductionOrder;
            var testlist = model.ProductionOrderList;
            Assert.True(true);
        }
    }
}
