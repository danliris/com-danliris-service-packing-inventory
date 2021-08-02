using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Master;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.Master
{
    public class FabricProductControllerTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IFabricPackingSKUService fabricPackingSKUService, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IFabricPackingSKUService)))
                .Returns(fabricPackingSKUService);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
              .Returns(identityProvider);

            spMock.Setup(s => s.GetService(typeof(IValidateService)))
              .Returns(validateService);


            return spMock;
        }

        private FabricProductController GetController(IServiceProvider serviceProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new FabricProductController(serviceProvider)
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

        public FabricSKUFormDto formDto
        {
            get
            {
                return new FabricSKUFormDto()
                {
                };
            }
        }

        [Fact]
        public void Should_Success_Post()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.CreateSKU(It.IsAny<FabricSKUFormDto>()))
                .Returns(1);
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response =  controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }


        [Fact]
        public void Post_Throws_ServiceValidationException()
        {
            //Setup
           
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.CreateSKU(It.IsAny<FabricSKUFormDto>()))
                .Throws(GetServiceValidationException());
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public void Post_Return_InternalServerError()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.CreateSKU(It.IsAny<FabricSKUFormDto>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Success_Return_OK()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(new FabricSKUDto());
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Return_NotFound()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(()=>null);
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public void GetById_Return_InternalServerError()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Get_Success_Return_OK()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.GetIndex(It.IsAny<IndexQueryParam>()))
                .ReturnsAsync(new FabricSKUIndex(new List<FabricSKUIndexInfo>(),1,1,1));
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = await controller.Get(new IndexQueryParam());

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Get_Return_InternalServerError()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.GetIndex(It.IsAny<IndexQueryParam>()))
                .ThrowsAsync(new Exception());

            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = await controller.Get(new IndexQueryParam());

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public void Delete_Return_InternalServerError()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.DeleteSKU(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Delete_Success_Return_NoContent()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.DeleteSKU(It.IsAny<int>()))
                .Returns(1);
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public void Delete_Return_NotFound()
        {
            //Setup

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
                .Setup(s => s.DeleteSKU(It.IsAny<int>()))
                .Returns(0);
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var response = controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }


        [Fact]
        public void Should_Success_Put()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();

            serviceMock
              .Setup(s => s.GetById(It.IsAny<int>()))
              .Returns(new FabricSKUDto());

            serviceMock
                .Setup(s => s.UpdateSKU(It.IsAny<int>(),It.IsAny<FabricSKUFormDto>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Put(1,dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public void Put_Return_NotFound()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
              .Setup(s => s.GetById(It.IsAny<int>()))
              .Returns(()=>null);

            var service = serviceMock.Object;


            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public void Put_Throws_ServiceValidationException()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
              .Setup(s => s.GetById(It.IsAny<int>()))
              .Returns(new FabricSKUDto());

            serviceMock
                .Setup(s => s.UpdateSKU(It.IsAny<int>(), It.IsAny<FabricSKUFormDto>()))
                .Throws(GetServiceValidationException());
            var service = serviceMock.Object;


            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public void Put_Return_InternalServerError()
        {
            //Setup
            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<FabricSKUFormDto>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var serviceMock = new Mock<IFabricPackingSKUService>();
            serviceMock
              .Setup(s => s.GetById(It.IsAny<int>()))
              .Returns(new FabricSKUDto());

            serviceMock
                .Setup(s => s.UpdateSKU(It.IsAny<int>(), It.IsAny<FabricSKUFormDto>()))
                .Throws(new Exception());
            var service = serviceMock.Object;


            //Act
            var controller = GetController(GetServiceProvider(service, identityProvider, validateService).Object);
            var dataUtil = formDto;
            var response = controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
