using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Inventory;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.Inventory
{
    public class InventorySKUControllerTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IInventorySKUService inventorySKUService, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IInventorySKUService)))
                .Returns(inventorySKUService);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
              .Returns(identityProvider);

            spMock.Setup(s => s.GetService(typeof(IValidateService)))
              .Returns(validateService);

            return spMock;
        }

        private InventorySKUController GetController(IServiceProvider serviceProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new InventorySKUController(serviceProvider)
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

        private ServiceValidationException GetServiceValidationException()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(formDto, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private FormDto formDto
        {
            get
            {
                return new FormDto();
                
            }
        }

        private DocumentDto documentDto
        {
            get
            {
                var documents = new ProductSKUInventoryDocumentModel("documentNo",DateTimeOffset.Now, "referenceNo", "referenceType",0, "storageName", "storageCode","type","remark");
                var items = new List<ProductSKUInventoryMovementModel>()
                {
                    new ProductSKUInventoryMovementModel(0,0,0,0,"storageCode","storageName",0,"type","remark")
                };
                var products = new List<ProductSKUModel>()
                {
                    new ProductSKUModel("code","name",0,0,"description")
                };
                var uoms = new List<UnitOfMeasurementModel>()
                {
                    new UnitOfMeasurementModel("unit")
                };
                var categories = new List<CategoryModel>()
                {
                    new CategoryModel("name","code")
                };
                return new DocumentDto(documents, items, products, uoms, categories);
            }
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.AddDocument(It.IsAny<FormDto>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_BadRequest()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.AddDocument(It.IsAny<FormDto>())).Throws(GetServiceValidationException());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_InternalServerError()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.AddDocument(It.IsAny<FormDto>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Return_OK()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.GetDocumentById(It.IsAny<int>())).Returns(documentDto);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response =  controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Return_NotFound()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.GetDocumentById(It.IsAny<int>())).Returns(() => null);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response =  controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Return_InternalServerError()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.GetDocumentById(It.IsAny<int>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response =  controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Get_Return_OK()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.GetDocumentIndex(It.IsAny<IndexQueryParam>())).Returns(new DocumentIndexDto(new List<ProductSKUInventoryDocumentModel>(),1,25));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.Get(new IndexQueryParam());

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Return_InternalServerError()
        {
            //Setup
            var dataUtil = formDto;
            var serviceMock = new Mock<IInventorySKUService>();
            serviceMock.Setup(s => s.GetDocumentIndex(It.IsAny<IndexQueryParam>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<FormDto>())).Verifiable();
            var validateService = validateServiceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response =  controller.Get(new IndexQueryParam());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


    }
}
