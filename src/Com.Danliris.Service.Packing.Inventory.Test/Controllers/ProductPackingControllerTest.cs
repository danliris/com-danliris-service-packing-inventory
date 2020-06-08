using Com.Danliris.Service.Packing.Inventory.Application.ProductPacking;
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
   public class ProductPackingControllerTest
    {
       

        private ProductPackingController GetController(IProductPackingService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new ProductPackingController(service, identityProvider)
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
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }


        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private ProductPackingFormViewModel ViewModel
        {
            get
            {
                return new ProductPackingFormViewModel()
                {
                    PackingType = "PackingType",
                    Quantity = 1,
                    SKUId =1,
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new ProductPackingFormViewModel();
            var validator = new ProductPackingFormValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Post_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<ProductPackingFormViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
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
            var serviceMock = new Mock<IProductPackingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<ProductPackingFormViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "error_test");
            var response = await controller.Post(dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<ProductPackingFormViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            var response = await controller.Put(1,dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_BadRequest()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<ProductPackingFormViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "error_test");
            var response = await controller.Put(1, dataUtil);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            var response = await controller.Delete(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task  GetById_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();

            var result = new ProductPackingModel();

            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(result);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            var response = await controller.GetById(1);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetByKeyword_Return_Success()
        {
            //setup
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductPackingService>();


            ListResult<IndexViewModel> result = new ListResult<IndexViewModel>(
                new List<IndexViewModel>()
                {
                    new IndexViewModel()
                    {
                        Id =1,
                        Code ="Code",
                        Name ="Name",
                        LastModifiedUtc = DateTime.UtcNow,
                        PackingType ="PackingType",
                        Quantity =1
                    }
                }, 1, 1, 1);

            serviceMock.Setup(s => s.ReadByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //Act
            var controller = GetController(service, identityProvider);
            var response =  controller.GetByKeyword("","{}",1,25);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
    }
}
