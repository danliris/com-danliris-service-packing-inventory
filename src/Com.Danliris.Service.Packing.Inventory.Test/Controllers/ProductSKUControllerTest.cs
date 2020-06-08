using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    public class ProductSKUControllerTest
    {
        private ProductSKUController GetController(IProductSKUService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new ProductSKUController(service, identityProvider)
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

        

       private CreateProductSKUViewModel ViewModel
        {
            get
            {
                return new CreateProductSKUViewModel()
                {
                    Composition = "composition",
                    Construction = "construction",
                    Design = "design",
                    Grade = "grade",
                    LotNo = "lotNo",
                    ProductType = "productType",
                    UOMUnit = "UOMUnit",
                    Width = "width",
                    WovenType = "wovenType",
                    YarnType1 = "yarnType1",
                    YarnType2 = "yarnType2"
                };
            }
        }

        private UpdateProductSKUViewModel updateViewModel
        {
            get
            {
                return new UpdateProductSKUViewModel()
                {
                    Composition = "composition",
                    Construction = "construction",
                    Design = "design",
                    Grade = "grade",
                    LotNo = "lotNo",
                    ProductType = "productType",
                    UOMUnit = "UOMUnit",
                    Width = "width",
                    WovenType = "wovenType",
                    YarnType1 = "yarnType1",
                    YarnType2 = "yarnType2"
                };
            }
        }

        [Fact]
        public async Task Post_Return_Success()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.Create(It.IsAny<CreateProductSKUViewModel>())) ;
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_Return_BadRequest()
        {
            var dataUtil = ViewModel;
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.Create(It.IsAny<CreateProductSKUViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "error_test");
            controller.ModelState.AddModelError("key", "error_test");

            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_Success()
        {
            var dataUtil = updateViewModel;
            //setup
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(),It.IsAny<UpdateProductSKUViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.Put(1,dataUtil);
            
            //assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_Return_BadRequest()
        {
            var dataUtil = updateViewModel;
            //setup
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateProductSKUViewModel>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider);
            controller.ModelState.AddModelError("key", "error_test");
            var response = await controller.Put(1, dataUtil);

            //assert
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_Return_success()
        {
            //setup
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.Delete(1);

            //assert
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Return_Success()
        {
            //setup
            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(new ProductSKUModel());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider);
            var response = await controller.GetById(1);

            //assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetByKeyword_Return_Success()
        {
            //setup
            ListResult<IndexViewModel> result = new ListResult<IndexViewModel>(
                new List<IndexViewModel>()
                {
                    new IndexViewModel()
                    {
                        Id =1,
                        Code ="Code",
                        Name ="Name",
                        ProductType = "ProductType",
                      
                    }
                },1,1,1);;

            var serviceMock = new Mock<IProductSKUService>();
            serviceMock.Setup(s => s.ReadByKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            //act
            var controller = GetController(service, identityProvider);
            var response =  controller.GetByKeyword("keyword","{}",1,25);

            //assert
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

    }
}
