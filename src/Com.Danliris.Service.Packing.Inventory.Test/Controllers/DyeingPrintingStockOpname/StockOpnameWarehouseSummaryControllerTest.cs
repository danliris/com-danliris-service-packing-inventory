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
    public class StockOpnameWarehouseSummaryControllerTest
    {
        private StockOpnameSummaryController GetController(IStockOpnameSummaryService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new StockOpnameSummaryController(service, identityProvider, validateService)
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

        private UpdateTrackViewModel viewModelTrack
        {
            get
            {
                return new UpdateTrackViewModel()
                {
                    Id = 1,
                    ProductionOrderId = 1,
                    ProductionOrderNo = "a",
                    ProductPackingCode = "a",
                    ProcessTypeName = "a",
                    PackagingUnit = "a",
                    Grade = "a",
                    Color = "a",
                    Construction = "a",
                    Motif = "a",
                    TrackId = 1,
                    TrackName = "a",
                    TrackBox = "a",
                    Track = "a",
                    PackagingQty = 1,
                    PackagingLength = 1,
                    Balance = 1

                };
            }
        }

        private StockOpnameWarehouseSummaryViewModel viewModel
        {
            get 
            {
                return new StockOpnameWarehouseSummaryViewModel()
                {
                    Id = 1,
                    ProductionOrderNo = "a",
                    ProductPackingCode = "a",
                    ProcessTypeName = "a",
                    PackagingUnit = "a",
                    Grade = "a",
                    Color = "a",
                    Construction = "a",
                    Motif = "a",
                    
                    TrackName = "a",
                    
                    
                    PackagingQty = 1,
                    PackagingLength = 1,
                    Balance = 1

                };
            }
        }

        private StockOpnameTrackViewModel viewModelTrack2
        {
            get 
            {
                return new StockOpnameTrackViewModel
                {
                    ProductPackingCode = "a",
                    Grade = "a",
                    PackagingLength = 1,
                    Items = new List<TrackingViewModel>()
                    { 
                        new TrackingViewModel()
                        { 
                            PackagingQtyRemains = 1,
                            PackagingQtySplit =1,
                            PackingQtyDiff =1,
                            Balance = 1,
                            Track = new Application.ToBeRefactored.CommonViewModelObjectProperties.Track()
                            { 
                                Id =1,
                                Name ="a",
                                Type = "a",
                                Box = "a"

                            }
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Success_GetUpdateTrack()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock
                .Setup(s => s.GetDataUpdateTrack(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<UpdateTrackViewModel>() { viewModelTrack});

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetUpdateTrack(1, "a", 1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Success_GetUpdateTrack()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock
                .Setup(s => s.GetDataUpdateTrack(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = controller.GetUpdateTrack(1, "a", 1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameSummaryService>();
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
            var response = await controller.GetByIdUpdateTrack(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            //Arrange
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());

            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);

            //Act
            var response = await controller.GetByIdUpdateTrack(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = viewModelTrack2;
            //v
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameTrackViewModel>()))
                .ReturnsAsync(1);

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameTrackViewModel>()))
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

        // Command 30-01-2024
        //[Fact]
        //public async Task Should_NotValid_Put()
        //{
        //    //Arrange
        //    var dataUtil = viewModelTrack2;

        //    var serviceMock = new Mock<IStockOpnameSummaryService>();
        //    serviceMock
        //        .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameTrackViewModel>()))
        //        .ReturnsAsync(1);

        //    var service = serviceMock.Object;

        //    var validateServiceMock = new Mock<IValidateService>();
        //    validateServiceMock
        //        .Setup(s => s.Validate(It.IsAny<StockOpnameTrackViewModel>()))
        //        .Verifiable();

        //    var validateService = validateServiceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider, validateService);
        //    controller.ModelState.AddModelError("test", "test");

        //    //Act
        //    var response = await controller.Put(1, dataUtil);

        //    //Assert
        //    Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        //}

        // Command 30-01-2024
        //[Fact]
        //public async Task Should_ValidateExcpetion_Put()
        //{
        //    //Arrange
        //    var dataUtil = viewModelTrack2;
        //    var serviceMock = new Mock<IStockOpnameSummaryService>();
        //    serviceMock
        //        .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameTrackViewModel>()))
        //        .ReturnsAsync(1);

        //    var service = serviceMock.Object;

        //    var validateServiceMock = new Mock<IValidateService>();
        //    validateServiceMock
        //        .Setup(s => s.Validate(It.IsAny<StockOpnameTrackViewModel>()))
        //        .Throws(GetServiceValidationExeption(viewModel));

        //    var validateService = validateServiceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider, validateService);

        //    //Act
        //    var response = await controller.Put(1, dataUtil);

        //    //Assert
        //    Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        //}

        [Fact]
        public async Task Should_Exception_Put()
        {
            //Arrange
            var dataUtil = viewModelTrack2;

            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<StockOpnameTrackViewModel>()))
                .Throws(new Exception("mess"));

            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<StockOpnameTrackViewModel>()))
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
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<int>(), It.IsAny<string>(),It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls(1, "a", 1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetStockOpnameExcel()
        {
            //v
            var serviceMock = new Mock<IStockOpnameSummaryService>();
            serviceMock.Setup(s => s.GenerateExcelMonitoring(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetXls(1, "a", 1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
