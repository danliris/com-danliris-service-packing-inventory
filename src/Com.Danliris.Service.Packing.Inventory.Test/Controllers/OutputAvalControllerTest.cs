using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaOutput;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class OutputAvalControllerTest
    {
        private OutputAvalController GetController(IOutputAvalService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new OutputAvalController(service, identityProvider, validateService)
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

        private ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(OutputAvalViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private OutputAvalViewModel OutputAvalViewModel
        {
            get
            {
                return new OutputAvalViewModel()
                {
                    Id = 1,
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "GUDANG AVAL",
                    Shift = "PAGI",
                    Group = "A",
                    HasNextAreaDocument = true,
                    AvalItems = new List<OutputAvalItemViewModel>()
                    {
                        new OutputAvalItemViewModel()
                        {
                            AvalItemId = 12,
                            AvalType = "SAMBUNGAN",
                            AvalCartNo = "5-11",
                            AvalUomUnit = "KRG",
                            AvalQuantity = 5,
                            AvalQuantityKg = 10,
                            AvalOutQuantity = 5,
                            AvalOutSatuan = 1
                        }
                    },
                    DyeingPrintingMovementIds = new List<OutputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new OutputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 51,
                            AvalItemId = 22
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = OutputAvalViewModel;

            var validator = new OutputAvalValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Post()
        {
            var dataUtil = new OutputAvalViewModel();
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Post()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputAvalViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_ServiceValidationException_Post()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputAvalViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(OutputAvalViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAvailableAval()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadAvailableAval(It.IsAny<DateTimeOffset>(), 
                                                       It.IsAny<string>(), 
                                                       It.IsAny<string>(),
                                                       It.IsAny<int>(), 
                                                       It.IsAny<int>(), 
                                                       It.IsAny<string>(), 
                                                       It.IsAny<string>(), 
                                                       It.IsAny<string>()))
                       .Returns(new ListResult<AvailableAvalIndexViewModel>(new List<AvailableAvalIndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableAval(OutputAvalViewModel.Date, OutputAvalViewModel.Shift, OutputAvalViewModel.Group);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetAvailableAval()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadAvailableAval(dataUtil.Date,
                                                       dataUtil.Shift,
                                                       dataUtil.Group,
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableAval(DateTimeOffset.UtcNow, "SIANG", "B");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetAvalAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetAvalAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAvailableAllAval()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadAllAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Returns(new ListResult<AvailableAvalIndexViewModel>(new List<AvailableAvalIndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableAllAval(OutputAvalViewModel.Date, OutputAvalViewModel.Shift, OutputAvalViewModel.Group);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetAvailableAllAval()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadAllAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableAllAval(DateTimeOffset.UtcNow, "SIANG", "B");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAvailableByBonAval()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByBonAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Returns(new ListResult<AvailableAvalIndexViewModel>(new List<AvailableAvalIndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableByBonAval(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetAvailableByBonAval()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByBonAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableByBonAval(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetSummaryAvalByType()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByTypeAvailableAval(
                                                       It.IsAny<string>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Returns(new ListResult<AvailableAvalIndexViewModel>(new List<AvailableAvalIndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSummaryAvalByType("string");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetSummaryAvalByType()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByTypeAvailableAval(
                                                       It.IsAny<string>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSummaryAvalByType("string");


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Null_GetSummaryAvalByType()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByTypeAvailableAval(
                                                       It.IsAny<string>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Returns<ListResult<AvailableAvalIndexViewModel>>(null);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetSummaryAvalByType("string");


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Null_GetAvailableByBonAval()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadByBonAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                      .Returns<ListResult<AvailableAvalIndexViewModel>>(null);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableByBonAval(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Null_GetAvailableAllAval()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.ReadAllAvailableAval(
                                                       It.IsAny<int>(),
                                                       It.IsAny<int>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>(),
                                                       It.IsAny<string>()))
                       .Returns<ListResult<AvailableAvalIndexViewModel>>(null);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAvailableAllAval(DateTimeOffset.UtcNow, "SIANG", "B");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAdjProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.GetDistinctAllProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<AdjAvalItemViewModel>(new List<AdjAvalItemViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAdjProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetAdjProductionOrders()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.GetDistinctAllProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAdjProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAvalAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();

            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcel("7");

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_Exception_GetAvalAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputAvalService>();

            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcel("7");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Put()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_ValidateExcpetion_Put()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputAvalViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Put()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputAvalViewModel>())).Throws(new Exception("mess"));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputAvalViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Success_Delete()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var dataUtil = OutputAvalViewModel;
            //v
            var serviceMock = new Mock<IOutputAvalService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).Throws(new Exception("error"));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();

            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
