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
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
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
                    DONumber="123",
                    FONumber="123",
                    Receiver="abc",
                    Remark= "abc",
                    SCNumber= "abc",
                    Sender= "abc",
                    StorageNumber= "abc",

                    Items = new List<ItemsViewModel>()
                    {
                        new ItemsViewModel()
                        {
                            NoSPP = "123",
                            MaterialName = "s",
                            InputLot = "123",
                            WeightBruto = 2,
                            WeightDOS = 2,
                            WeightCone = 2,
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

            //dataUtil.InspectionMaterialProductionOrders = new List<InputInspectionMaterialProductionOrderViewModel>()
            //{
            //    new InputInspectionMaterialProductionOrderViewModel()
            //    {
            //        ProductionOrder = new ProductionOrder()
            //    }
            //};
            //result = validator.Validate(dataUtil);
            //Assert.NotEqual(0, result.Errors.Count);
        }

        //[Fact]
        //public async Task Should_Success_Post()
        //{
        //    var dataUtil = ViewModel;
        //    //v
        //    var serviceMock = new Mock<IMaterialDeliveryNoteService>();
        //    serviceMock.Setup(s => s.Create(It.IsAny<MaterialDeliveryNoteViewModel>())).ReturnsAsync();
        //    var service = serviceMock.Object;

        //    var validateServiceMock = new Mock<IValidateService>();
        //    validateServiceMock.Setup(s => s.Validate(It.IsAny<MaterialDeliveryNoteViewModel>()))
        //        .Verifiable();
        //    var validateService = validateServiceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider, validateService);
        //    //controller.ModelState.IsValid == false;
        //    var response = await controller.Post(dataUtil);

        //    Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        //}

    }
}
