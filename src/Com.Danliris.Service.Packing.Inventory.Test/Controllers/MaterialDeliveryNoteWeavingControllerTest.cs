using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.MaterialDelivery;
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
    public class MaterialDeliveryNoteWeavingControllerTest
    {
        private MaterialDeliveryNoteWeavingController GetController(IMaterialDeliveryNoteWeavingService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new MaterialDeliveryNoteWeavingController(service, identityProvider, validateService)
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
            ValidationContext validationContext = new ValidationContext(ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private MaterialDeliveryNoteWeavingViewModel ViewModel
        {
            get
            {

                return new MaterialDeliveryNoteWeavingViewModel()
                {
                    Code = "abc123",
                    DateSJ = DateTimeOffset.UtcNow,
                    selectedDO = new DeliveryOrderMaterialDeliveryNoteWeaving
                    {
                        Id = 1,
                        DOSalesNo = "123"
                    },
                    SendTo = "abc",
                    Unit = new UnitMaterialDeliveryNoteWeaving
                    {
                        Id = 1,
                        Code = "abc123",
                        Name = "abc"
                    },
                    Buyer = new BuyerMaterialDeliveryNoteWeaving
                    {
                        Id = 1,
                        Code = "abc123",
                        Name = "abc"
                    },
                    NumberBonOut = "abc123",
                    Storage = new StorageMaterialDeliveryNoteWeaving
                    {
                        Id = 1,
                        Code = "abc123",
                        Name = "abc",
                        unit = "abc"
                    },
                    Remark = "abc",
                    ItemsMaterialDeliveryNoteWeaving = new List<ItemsMaterialDeliveryNoteWeavingViewModel>()
                    {
                        new ItemsMaterialDeliveryNoteWeavingViewModel()
                        {
                            itemNoSOP = "abc123",
                            itemMaterialName = "abc123",
                            itemGrade = "abc123",
                            itemType = "abc123",
                            itemCode = "abc123",
                            inputBale = 2,
                            inputPiece = 2,
                            inputMeter = 2,
                            inputKg = 2
                        }
                    }
                };

            }

        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = ViewModel;
            var validator = new MaterialDeliveryNoteWeavingValidator();
            var result = validator.Validate(dataUtil);
            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var dataUtil = new MaterialDeliveryNoteWeavingViewModel();
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteWeavingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteWeavingViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
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
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.ReadByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),  It.IsAny<string>()))
                .Returns(new ListResult<MaterialDeliveryNoteWeavingViewModel>(new List<MaterialDeliveryNoteWeavingViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),  It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.ReadByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Success_Delete()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()));
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteWeavingViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteWeavingViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteWeavingViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteWeavingViewModel>())).Throws(new Exception("mess"));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteWeavingViewModel>()))
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
        public async Task Should_Success_GetPdfById()
        {
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, "7");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPdfById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, "7");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPdfById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(MaterialDeliveryNoteWeavingViewModel));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, "7");

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPdfById_Empty_Detail()
        {
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteWeavingService>();
            var vm = ViewModel;
            vm.ItemsMaterialDeliveryNoteWeaving = new List<ItemsMaterialDeliveryNoteWeavingViewModel>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(vm);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPdfById(1, "7");

            Assert.NotNull(response);
        }
    }
}
