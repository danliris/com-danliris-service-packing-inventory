using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
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
using static Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse.StockOpnameWarehouseService;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.DyeingPrintingStockOpname
{
   public class StockOpnameWarehouseControllerTest
    {
        private StockOpnameWarehouseController GetController(IStockOpnameWarehouseService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new StockOpnameWarehouseController(service, identityProvider, validateService)
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

        private ServiceValidationException GetServiceValidationExeption( dynamic ViewModel)
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


        private StockOpnameWarehouseViewModel viewModel
        {
            get
            {
                return new StockOpnameWarehouseViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    WarehousesProductionOrders = new List<StockOpnameWarehouseProductionOrderViewModel>()
                    {
                        new StockOpnameWarehouseProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            Material = new Material(){
                            Code="Code",
                            Name="Name"
                            },
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        private BarcodeInfoViewModel ViewModel
        {
            get
            {
                return new BarcodeInfoViewModel
                {
                    PackingCode = "a",
                    MaterialName = "a",
                    MaterialConstructionName =  "a",
                    YarnMaterialName = "a",
                    PackingLength = 1,
                    PackingType = "a",
                    Color = "a",
                    OrderNo = "P001",
                    UOMSKU = "mtr",
                    DocumentNo = "a",
                    Grade = "a",
                    Balance = 1,
                    CreatedBy = "dev2",
                    PackagingQty = 1
                };
            }
        }

        private StockOpnameWarehouseProductionOrderViewModel viewModelItem 
        {
            get
            {
                return new StockOpnameWarehouseProductionOrderViewModel
                {
                    Id = 1,
                    ProductionOrder = new ProductionOrder()
                    {
                        Code = "SLD",
                        Id = 62,
                        Type = "SOLID",
                        No = "F/2020/000"
                    },
                    PackingInstruction = "a",
                    Construction = "a",
                    Unit = "a",
                    Buyer = "a",
                    Color = "a",
                    Motif = "a",
                    UomUnit = "a",
                    Remark = "a",
                    Grade = "a",
                    Status = "a",
                    Balance = 50,
                    PreviousBalance = 100,
                    InputId = 2,
                    ProductionOrderNo = "asd",
                    Material = new Material()
                    {
                        Code = "Code",
                        Name = "Name"
                    },
                    MtrLength = 10,
                    YdsLength = 10,
                    Quantity = 10,
                    PackagingType = "s",
                    PackagingUnit = "a",
                    PackagingQty = 10,
                    QtyOrder = 10
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
                    InQty   = 1,
                    OutQty = 1,
                    Total = 1,
                    PackagingQty = 1,
                    PackingLength = 1,
                    BuyerName = "buyer",
                    BonNo = "boNo",
                    DateIn = DateTime.MinValue,
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
           
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameWarehouseViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);
            
            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_NotValid_Post()
        {
            //Arrange
            var dataUtil = new StockOpnameWarehouseViewModel();

            var serviceMock = new Mock<IStockOpnameWarehouseService>();

            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameWarehouseViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
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
        public async Task Should_Exception_Post()
        {
            //Arrange
            var dataUtil = viewModel;
            
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameWarehouseViewModel>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();

            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);
            
            //Act
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_ServiceValidationException_Post()
        {
            //Arrange
            var dataUtil = viewModel;
          
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<StockOpnameWarehouseViewModel>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
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
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
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
            var dataUtil = viewModel;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
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
        public void Should_Succes_GetListBon()
        {
            //Arrange
            var dataUtil = viewModel;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(),1,1,1) { });

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetListBon();

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Throw_Exception_GetListBon()
        {
            //Arrange
            var dataUtil = viewModel;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<string>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetListBon();

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Success_Delete()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
          
            //Act
            var response = await controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Exception_Delete()
        {
            //Arrange
            var dataUtil = viewModel;
            
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
          
            //Act
            var response = await controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = viewModel;
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameWarehouseViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
           
            //Act
            var response = await controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_NotValid_Put()
        {
            //Arrange
            var dataUtil = viewModel;
          
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameWarehouseViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
           
            //Act
            var response = await controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_ValidateExcpetion_Put()
        {
            //Arrange
            var dataUtil = viewModel;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameWarehouseViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Throws(GetServiceValidationExeption(viewModel));

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            
            //Act
            var response = await controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Put()
        {
            //Arrange
            var dataUtil = viewModel;
            
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameWarehouseViewModel>()))
                .Throws(new Exception("mess"));

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameWarehouseViewModel>()))
                .Verifiable();

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetStockOpnameExcel()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelDocumentAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetStockOpnameExcel()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelDocumentAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetScanView()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GetMonitoringScan( It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<BarcodeInfoViewModel>() { ViewModel });
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            var response = controller.GetScanView(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetScanView()
        {
            //Arrange
            var dataUtil = viewModel;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.GetMonitoringScan(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            //var response = controller.GetListBon();

            var response = controller.GetScanView(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public void Should_Success_GetStockOpnameExcelViewScan()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoringScan(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXlsScanView(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetStockOpnameExcelViewScan()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoringScan(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXlsScanView(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get_Report_SO()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.GetReportDataSO(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ReportSOViewModel>() { viewModelReportSO });

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.Get(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>());

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get_Report_SO()
        {
            //Arrange
            var dataUtil = viewModelReportSO;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.GetReportDataSO(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.Get(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetExcel_Report_SO()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetExcel_Report_SO()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public void Should_Success_Get_Monitoring_SO()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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
        public void Should_Exception_Get_Monitoring_SO()
        {
            //Arrange
            var dataUtil = viewModelReportSO;
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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
        public void Should_Success_GetExcel__Monitoring_SO()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            DateTime dt2 = new DateTime(2015, 12, 31);
            var response = controller.GetMonitoringXls(dt2, dt2, It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetExcel_Monitoring_SO()
        {
            //v
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
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

        // Command 30-01-2024
        //[Fact]
        //public void Should_Success_Get_Barcode()
        //{
        //    //Arrange
        //    var serviceMock = new Mock<IStockOpnameWarehouseService>();
        //    serviceMock
        //        .Setup(s => s.getDatabyCode( It.IsAny<string>(), It.IsAny<int>()))
        //        .Returns(new List<StockOpnameWarehouseProductionOrderViewModel>() { viewModelItem });

        //    var service = serviceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;
        //    var validateServiceMock = new Mock<IValidateService>();
        //    var validateService = validateServiceMock.Object;

        //    var controller = GetController(service, identityProvider, validateService);

        //    //Act
        //    var response = controller.GetBarcode(It.IsAny<string>(), It.IsAny<int>());

        //    //Assert
        //    Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        //}

        [Fact]
        public void Should_Exception_Get_Barcode()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameWarehouseService>();
            serviceMock
                .Setup(s => s.getDatabyCode(It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetBarcode(It.IsAny<string>(), It.IsAny<int>());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


    }
}
