using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
  public  class ReceivingDispatchControllerTest
    {
        private ReceivingDispatchController GetController(IReceivingDispatchService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new ReceivingDispatchController(service, identityProvider)
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

        private CreateReceivingDispatchDocumentViewModel ViewModel
        {
            get
            {
                return new CreateReceivingDispatchDocumentViewModel()
                {
                    Items = new List<CreateReceivingDispatchDocumentItemViewModel>()
                    {
                        new CreateReceivingDispatchDocumentItemViewModel()
                        {
                            Code = "code",
                            PackingId = 1,
                            PackingType = "PackingType",
                            Quantity = 1,
                            SKUId = 1,
                            UOMUnit = "UOMUnit",

                        }
                    },
                    Storage = new Storage()
                    {
                        Id =1,
                        Code = "code",
                        Name ="name",
                        Unit =new UnitStorage()
                        {
                            Division = new DivisionStorage()
                            {
                                Name="name",
                                
                            },
                            Name="name"
                            
                        },
                        
                    }
                };
            }
        }

        [Fact]
        public async Task Receive_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IReceivingDispatchService>();
            serviceMock.Setup(s => s.Receive(It.IsAny<CreateReceivingDispatchDocumentViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.Receive(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Receive_Return_BadRequest()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IReceivingDispatchService>();
            serviceMock.Setup(s => s.Receive(It.IsAny<CreateReceivingDispatchDocumentViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.Receive(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Dispatch_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IReceivingDispatchService>();
            serviceMock.Setup(s => s.Dispatch(It.IsAny<CreateReceivingDispatchDocumentViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.Dispatch(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Dispatch_Return_BadRequest()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IReceivingDispatchService>();
            serviceMock.Setup(s => s.Dispatch(It.IsAny<CreateReceivingDispatchDocumentViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.Dispatch(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

    }
}
