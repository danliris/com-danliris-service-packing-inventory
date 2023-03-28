using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingStockOpname;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.DyeingPrintingStockOpname
{
    public class StockOpnameWarehouseMutationControllerTest
    {
        private StockOpnameMutationController GetController(IStockOpnameMutationService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new StockOpnameMutationController(service, identityProvider, validateService)
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

        private ServiceValidationException GetServiceValidationExeption(dynamic ViewModel)
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private IndexViewModel viewModelIndex
        {
            get 
            {
                return new IndexViewModel()
                {
                    Id = 1,
                    Area = "Gudang Jadi",
                    BonNo = "BonNo",
                    Date = DateTime.MinValue,
                    Type = "type"

                };
            }
        }

        private StockOpnameMutationViewModel viewModel
        {
            get 
            {
                return new StockOpnameMutationViewModel()
                {
                    Id = 1,
                    CreatedUtc = DateTime.Now,
                    Area = "Stock Opnmae",
                    BonNo = "BonNo",
                    Date = DateTime.Now,
                    DestinationArea = "Gudang Jadi",
                    Type = "keluar",
                    DyeingPrintingStockOpnameMutationItems = new List<StockOpnameMutationItemViewModel>()
                    { 
                        new StockOpnameMutationItemViewModel()
                        { 
                            Id = 1,
                            Balance = 1,
                            Color = "Color",
                            Construction = "Construction",
                            Grade = "A",
                            Motif = "Motif",
                            PackagingQty = 1,
                            PackagingLength = 1,
                            PackagingType = "Roll",
                            PackagingUnit = "Roll",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            Remark = "a",
                            ProcessTypeId = 1,
                            ProcessTypeName = "process",
                            Unit = "Dyeing",
                            UomUnit = "mtr",
                            Track = new Application.ToBeRefactored.CommonViewModelObjectProperties.Track()
                            { 
                                Id = 1,
                                Type = "a",
                                Box = "a",
                                Name = "a"
                            },
                            ProductSKUId = 1,
                            FabricSKUId = 1,
                            ProductSKUCode = "a",
                            ProductPackingId =1,
                            FabricPackingId = 1,
                            ProductPackingCode = "code",
                            TypeOut = "out",
                            SendQuantity = 1,


                        }
                    }
                };
            }
        }

        private ReportSOViewModel viewModelReportSO
        {
            get
            {
                return new ReportSOViewModel
                {
                    ProductionOrderId = 1,
                    ProductionOrderNo = "OrderNo",
                    ProductPackingCode = "Code",
                    ProcessTypeName = "ProcessType",
                    PackagingUnit = "Roll",
                    PackagingType = "type",
                    Grade = "a",
                    Color = "a",
                    TrackId = 1,
                    TrackName = "a",
                    SaldoBegin = 1,
                    InQty = 1,
                    OutQty = 1,
                    Total = 1,
                    PackagingQty = 1,
                    PackingLength = 1,
                    BuyerName = "buyer",
                    BonNo = "boNo",
                    DateIn = DateTime.Now,
                    Construction = "a",
                    Motif = "a"
                };
            }
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            //Arrange
            var dataUtil = viewModel;

            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameMutationViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameMutationViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Post()
        {
            //Arrange
            var dataUtil = viewModel;

            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameMutationViewModel>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameMutationViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Post()
        {
            //Arrange
            var dataUtil = new StockOpnameMutationViewModel();

            var serviceMock = new Mock<IStockOpnameMutationService>();

            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameMutationViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameMutationViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");

            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_ServiceValidationException_Post()
        {
            //Arrange
            var dataUtil = viewModel;

            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameMutationViewModel>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameMutationViewModel>()))
                .Throws(GetServiceValidationExeption(dataUtil));

            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(viewModel);

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.Get();

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.Get();

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get_Monitoring_SO_Mutation()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.GetMonitoringSO(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ReportSOViewModel>() { viewModelReportSO });

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get_Monitoring_SO_Mutation()
        {
            //Arrange
            //var dataUtil = viewModelReportSO;
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock
                .Setup(s => s.GetMonitoringSO(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetExcel__Monitoring_SO_Mutation()
        {
            //v
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetMonitoringXls(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Success_GetExcel__Monitoring_SO_DateNotNull()
        {
            //v
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            DateTime dt2 = DateTime.Now;
            var response = controller.GetMonitoringXls(dt2, dt2, It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetExcel_Monitoring_SO()
        {
            //v
            var serviceMock = new Mock<IStockOpnameMutationService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetMonitoringXls(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

    }
}
