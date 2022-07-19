using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping;
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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class OutputShippingControllerTest
    {
        private OutputShippingController GetController(IOutputShippingService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new OutputShippingController(service, identityProvider, validateService)
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
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private OutputShippingViewModel ViewModel
        {
            get
            {
                return new OutputShippingViewModel()
                {
                    Area = "SHIPPING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "SHIPPING",
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "DONo"
                    },
                    InputShippingId = 1,
                    ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
                    {
                        new OutputShippingProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            IsSave = true,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "sd"
                            },
                            Packing ="s",
                            QtyPacking = 1,
                            Qty = 1,
                            Id = 1,
                            PackingType = "sd",

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
            var dataUtil = new OutputShippingViewModel()
            {
                Date = DateTimeOffset.UtcNow.AddHours(-5)
            };
            var validator = new OutputShippingValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.Date = DateTimeOffset.UtcNow.AddDays(1);
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.Date = DateTimeOffset.UtcNow.AddDays(-1);
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
            };
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                }
            };
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputShippingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = new OutputShippingViewModel();
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputShippingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputShippingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputShippingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetByIdBon()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadByIdBon(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetByIdBon(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetByIdBon()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadByIdBon(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetByIdBon(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetShippingAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<OutputShippingViewModel>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            identityProviderMock.Setup(s => s.TimezoneOffset).Returns(7);
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetShippingAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<OutputShippingViewModel>(), It.IsAny<int>()))
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
        public void Should_Success_GetProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetInputShippingProductionOrdersByDeliveryOrder(It.IsAny<long>()))
                .Returns(new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping.InputShippingProductionOrderViewModel>() { });
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetInputShippingProductionOrdersByDeliveryOrder(It.IsAny<long>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetForSales()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadForSales(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetForSales();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetForSales()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadForSales(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetForSales();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put_HasSalesInvoice()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<OutputShippingUpdateSalesInvoiceViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.UpdateHasSalesInvoice(1, new OutputShippingUpdateSalesInvoiceViewModel());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Put_HasSalesInvoice()
        {
            var dataUtil = new OutputShippingViewModel();
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<OutputShippingUpdateSalesInvoiceViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.UpdateHasSalesInvoice(1, new OutputShippingUpdateSalesInvoiceViewModel());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Put_HasSalesInvoice()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<OutputShippingUpdateSalesInvoiceViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.UpdateHasSalesInvoice(1, new OutputShippingUpdateSalesInvoiceViewModel());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPdfById()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, false, "7");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPdfByIdBon()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, true, "7");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPdfById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, false, "7");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPdfById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(OutputShippingViewModel));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, false, "7");

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPdfById_Empty_Detail()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            var vm = ViewModel;
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(vm);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, false, "7");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
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

        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputShippingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputShippingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputShippingViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputShippingViewModel>())).Throws(new Exception("mess"));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputShippingViewModel>()))
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
        public void Should_Success_GetShippingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();

            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<string>(),It.IsAny<int>()))
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
        public void Should_Exception_GetShippingAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();

            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<string>(), It.IsAny<int>()))
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
        public void Should_Success_GetAdjProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetDistinctAllProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<AdjShippingProductionOrderViewModel>(new List<AdjShippingProductionOrderViewModel>(), 1, 1, 1));
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
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
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
        public void Should_Success_GetDistinctProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping.InputShippingProductionOrderViewModel>(new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping.InputShippingProductionOrderViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetDistinctProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetProductionOrders_SPP()
        {
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetInputShippingProductionOrdersByProductionOrder(It.IsAny<long>()))
                .Returns(new List<Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping.InputShippingProductionOrderViewModel>() { });
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrdersByProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetProductionOrders_SPP()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock.Setup(s => s.GetInputShippingProductionOrdersByProductionOrder(It.IsAny<long>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrdersByProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetPdfById_Ok()
        {
            var serviceMock = new Mock<IOutputShippingService>();
            serviceMock
                .Setup(s => s.ReadPdfPackingListById(It.IsAny<int>()))
                .ReturnsAsync(new MemoryStreamResult(new MemoryStream(), "FileName.pdf"));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/pdf";
            var response = await controller.GetPdfPackingListById(1);

            Assert.NotNull(response);
        }

    }
}
