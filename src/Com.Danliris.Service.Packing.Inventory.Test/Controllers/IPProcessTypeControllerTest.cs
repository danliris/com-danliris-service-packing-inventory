using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class IPProcessTypeControllerTest
    {
        private IIPProcessTypeService _serviceMock;
        private IIdentityProvider _identityProvider;
        private IPProcessTypeController _controllerMock;
        private IValidateService _validateMock;

        public IPProcessTypeControllerTest(bool isException = false,bool isValidate = false)
        {
            if (isException)
            {
                _serviceMock = GetGlobalDefaulExceptionMock().Object;

            }
            else
            {
                _serviceMock = GetGlobalDefaultMock().Object;
                
            }
            if(isValidate)
                _validateMock = GetGlobalExceptionValidate().Object;

            else
                _validateMock = GetGlobalValidate().Object;

            _identityProvider = GetGlobalIndetityProvider().Object;
            _controllerMock = GetController();
        }

        public IPProcessTypeController GetController()
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new IPProcessTypeController(_serviceMock, _identityProvider, _validateMock)
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

        private IPProcessTypeViewModel ViewModel
        {
            get
            {
                return new IPProcessTypeViewModel
                {
                    Code = "1",
                    ProcessType = "Testing"
                };
            }
        }
        private IPProcessTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPProcessTypeViewModel
                {
                    Code = "1",
                    ProcessType = null
                };
            }
        }

        private Mock<IIPProcessTypeService> GetGlobalDefaultMock()
        {
            var serviceMock = new Mock<IIPProcessTypeService>();
            serviceMock.Setup(s => s.ReadAll())
                .Returns(new ListResult<IPProcessTypeViewModel>(new List<IPProcessTypeViewModel> { ViewModel }, 1, 1, 1));
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.ReadByPage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ListResult<IPProcessTypeViewModel>(new List<IPProcessTypeViewModel> { ViewModel }, 1, 1, 1));
            serviceMock.Setup(s => s.Create(It.IsAny<IPProcessTypeViewModel>()))
                .ReturnsAsync(1);
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<IPProcessTypeViewModel>()))
                .ReturnsAsync(1);
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            return serviceMock;
        }
        private Mock<IIPProcessTypeService> GetGlobalDefaulExceptionMock()
        {
            var serviceMock = new Mock<IIPProcessTypeService>();
            serviceMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.ReadByPage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Create(It.IsAny<IPProcessTypeViewModel>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<IPProcessTypeViewModel>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .Throws(new Exception());
            return serviceMock;
        }
        private Mock<IIdentityProvider> GetGlobalIndetityProvider()
        {
            var identityProviderMock = new Mock<IIdentityProvider>();
            return identityProviderMock;
        }
        private Mock<IValidateService> GetGlobalValidate()
        {
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<IPProcessTypeViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;
            return validateServiceMock;
        }
        private Mock<IValidateService> GetGlobalExceptionValidate()
        {
            var newValid = new ValidationContext(this, null, null);
            var newListValidResult = new List<ValidationResult>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<IPProcessTypeViewModel>()))
                //.Throws(new ServiceValidationException("ErrorValidtasi",It.IsAny<ValidationContext>(),It.IsAny<List<ValidationResult>>()));
                .Throws(new ServiceValidationException("ErrorValidtasi", newValid, newListValidResult));

            var validateService = validateServiceMock.Object;
            return validateServiceMock;

        }
        //private Mock<IValidateService> GetGlobalExceptionValidate()
        //{
        //    var validateServiceMock = new Mock<IValidateService>();
        //    validateServiceMock.Setup(s => s.Validate(It.IsAny<IPWidthTypeViewModel>()))
        //        .Verifiable();
        //    var validateService = validateServiceMock.Object;
        //    return validateServiceMock;
        //}

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_GetById()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetById()
        {
            //v
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetByKeyword()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;

            var response = controller.GetByKeyword();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetByKeyword()
        {
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;

            var response = controller.GetByKeyword();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_Post()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }
        [Fact]
        public void Should_NotValid_Post()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            controller.ModelState.AddModelError("test", "test");
            var response = controller.Post(NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_ServiceException_Post()
        {
            //v
            var unittest = new IPProcessTypeControllerTest(false,true);
            var controller = unittest._controllerMock;
            //controller.ModelState.AddModelError("test", "test");

            var response = controller.Post(NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_Post()
        {
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Post(ViewModel);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_Edit()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Edit(1, ViewModel);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }
        [Fact]
        public void Should_NotValid_Edit()
        {
            //v
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            controller.ModelState.AddModelError("test", "test");
            var response = controller.Edit(1, NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_ServiceException_Edit()
        {
            //v
            var unittest = new IPProcessTypeControllerTest(false, true);
            var controller = unittest._controllerMock;
            //controller.ModelState.AddModelError("test", "test");

            var response = controller.Edit(1, NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_Edit()
        {
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Edit(1, ViewModel);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPProcessTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Exception_Delete()
        {
            var unittest = new IPProcessTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = await controller.Delete(1);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
