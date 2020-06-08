using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
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
 public   class InventoryDocumentSKUControllerTest
    {
        private InventoryDocumentSKUController GetController(IInventoryDocumentSKUService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new InventoryDocumentSKUController(service, identityProvider)
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

        private CreateInventoryDocumentSKUViewModel ViewModel
        {
            get
            {
                return new CreateInventoryDocumentSKUViewModel()
                {
                    Items = new List<CreateInventoryDocumentSKUItemViewModel>()
                    {
                        new CreateInventoryDocumentSKUItemViewModel()
                        {
                            
                            Quantity =1,
                            SKUId=1,
                            UOMUnit="UOMUnit"
                        }
                    },
                    ReferenceNo = "ReferenceNo",
                    ReferenceType = "ReferenceType",
                    Remark = "Remark",
                    Storage = new Storage()
                    {
                        Id = 1,
                        Code = "code",
                        Name = "name",
                        Unit = new UnitStorage()
                        {
                            Division = new DivisionStorage()
                            {
                                Name = "name",

                            },
                            Name = "name"

                        },

                    },
                    Type = "type",

                };
            }
        }


        [Fact]
        public async Task Post_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IInventoryDocumentSKUService>();
            serviceMock.Setup(s => s.Create(It.IsAny<CreateInventoryDocumentSKUViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_BadRequest()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IInventoryDocumentSKUService>();
            serviceMock.Setup(s => s.Create(It.IsAny<CreateInventoryDocumentSKUViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Return_Success()
        {
            //setup
            var serviceMock = new Mock<IInventoryDocumentSKUService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(new InventoryDocumentSKUModel());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetByKeyword_Return_Success()
        {
            //setup
            var serviceMock = new Mock<IInventoryDocumentSKUService>();

            ListResult<IndexViewModel> result = new ListResult<IndexViewModel>(
               new List<IndexViewModel>()
               {
                    new IndexViewModel()
                    {
                        Id =1,
                        Code ="Code",
                    }
               }, 1, 1, 1); ;

            serviceMock.Setup(s => s.ReadByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            //act
            var controller = GetController(service, identityProvider);
            var response = controller.GetByKeyword("keyword", "{}", 1, 25);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

    }
}
