using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
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
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.MaterialDelivery
{
    public class MaterialDeliveryNoteControllerTest
    {
        private MaterialDeliveryNoteController GetController(IMaterialDeliveryNoteService service, IIdentityProvider identityProvider, IValidateService validateService)
        {

            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new MaterialDeliveryNoteController(service, identityProvider, validateService)
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

        private MaterialDeliveryNoteViewModel ViewModel
        {
            get
            {
                return new MaterialDeliveryNoteViewModel()
                {
                    DateSJ = DateTimeOffset.UtcNow,
                    BonCode = "123",
                    DateFrom = DateTimeOffset.UtcNow,
                    DateTo = DateTimeOffset.UtcNow,
                    DONumber = new DeliveryOrderMaterialDeliveryNoteWeaving()
                    {
                        Id = 1,
                        DOSalesNo = "DOSalesNo"
                    },
                    FONumber = "123",
                    Receiver = new BuyerMaterialDeliveryNoteWeaving()
                    {
                        Id = 1,
                        Code = "Code",
                        Name = "Name"
                    },
                    Remark = "abc",
                    SCNumber = new SalesContract()
                    {
                        Id = 1,
                        Number = "Number"
                    },
                    Sender = new UnitMaterialDeliveryNoteWeaving()
                    {
                        Id = 1,
                        Code = "Code",
                        Name = "Name"
                    },
                    StorageNumber = new StorageMaterialDeliveryNoteWeaving()
                    {
                        Id = 1,
                        Code = "Code",
                        Name = "Name"
                    },

                    Items = new List<ItemsViewModel>()
                    {
                        new ItemsViewModel()
                        {
                            NoSOP = "123",
                            MaterialName = "s",
                            InputLot = "123",
                            WeightBruto = 2,
                            WeightDOS = "123,123",
                            WeightCone = "123,123",
                            WeightBale = 2,
                            GetTotal = 2,
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new MaterialDeliveryNoteViewModel()
            {
                DateSJ = DateTimeOffset.UtcNow.AddHours(-5)
            };
            var validator = new MaterialDeliveryNoteValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.DateSJ = DateTimeOffset.UtcNow.AddDays(1);
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.DateSJ = DateTimeOffset.UtcNow.AddDays(-1);
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

            dataUtil.Items = new List<ItemsViewModel>()
            {
                new ItemsViewModel()
            };
            result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);

        }

        [Fact]
        public async Task Should_Success_Post()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Post(dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_InternalServerError()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteViewModel>())).Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Post(dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_BadRequest()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("key", "errorMessage");
            var response = await controller.Post(dataUtil);

            //assertion
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Throws_ServiceValidationExeption()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteViewModel>())).Throws(GetServiceValidationExeption());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Post(dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Success_Put()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteViewModel>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(1, dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }


        [Fact]
        public async Task Put_Throws_ServiceValidationException()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteViewModel>())).Throws(GetServiceValidationExeption());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(1, dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_InternalServerError()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteViewModel>())).Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Put(1, dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_BadRequest()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<MaterialDeliveryNoteViewModel>())).Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("key", "errorMessage");
            controller.ModelState.AddModelError("key", "errorMessage");
            var response = await controller.Put(1, dataUtil);

            //Assertion
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Delete(1);

            //Assertion
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }


        [Fact]
        public async Task Delete_Return_InternalServerError()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.Delete(1);

            //Assertion
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(dataUtil);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetById(1);

            //Assertion
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }


        [Fact]
        public async Task GetById_Return_InternalServerError()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).Throws(new Exception());
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = await controller.GetById(1);

            //Assertion
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetByKeyword()
        {
            //Setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(dataUtil);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider, validateService);
            var response = controller.GetByKeyword("keyword", "{}", 1, 25, "{}");

            //Assertion
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Success_GetPdfById()
        {
            //v
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
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
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
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
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(MaterialDeliveryNoteViewModel));
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
            var serviceMock = new Mock<IMaterialDeliveryNoteService>();
            var vm = ViewModel;
            vm.Items = new List<ItemsViewModel>();
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
