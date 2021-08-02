using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.PaymentDispositionRecap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapControllerTest
    {
        protected GarmentShippingPaymentDispositionRecapController GetController(IPaymentDispositionRecapService service, IIdentityProvider identityProvider, IValidateService validateService, IGarmentShippingInvoiceService invoiceService, IGarmentPackingListService packingListService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new GarmentShippingPaymentDispositionRecapController(service, identityProvider, validateService)
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

        protected virtual PaymentDispositionRecapViewModel ViewModel
        {
            get
            {
                return new PaymentDispositionRecapViewModel();
            }
        }

        protected ServiceValidationException GetServiceValidationExeption()
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

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        //PUT

        [Fact]
        public async Task Put_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<PaymentDispositionRecapViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);

            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<PaymentDispositionRecapViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.Put(dataUtil.Id, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        //POST

        [Fact]
        public async Task Post_Created()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<PaymentDispositionRecapViewModel>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);

            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ValidationException_BadRequest()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Create(It.IsAny<PaymentDispositionRecapViewModel>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        //GET

        [Fact]
        public void Get_Ok()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<PaymentDispositionRecapViewModel>(new List<PaymentDispositionRecapViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        //GETBYID

        [Fact]
        public async Task GetById_Ok()
        {
            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new PaymentDispositionRecapViewModel());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        //DELETE

        [Fact]
        public async Task Delete_Ok()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);

            var response = await controller.Delete(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IPaymentDispositionRecapService>();
            serviceMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var service = serviceMock.Object;
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var invoiceService = invoiceServiceMock.Object;

            var plServiceMock = new Mock<IGarmentPackingListService>();
            var plService = plServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<PaymentDispositionRecapViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService, invoiceService, plService);
            var response = await controller.Delete(dataUtil.Id);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
