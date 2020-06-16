using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class IPWarpTypeServiceTest
    {
        private IIPWarpTypeRepository _repoMock;
        private IPWarpTypeService _serviceMock;
        private IServiceProvider _serviceProvider;

        public IPWarpTypeService GetService(IServiceProvider serviceProvider)
        {
            return new IPWarpTypeService(serviceProvider);
        }
        public IPWarpTypeService GetService()
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IIPWarpTypeRepository)))
                .Returns(_repoMock);
            _serviceProvider = spMock.Object;
            _serviceMock = new IPWarpTypeService(_serviceProvider);
            return _serviceMock;
        }

        public IPWarpTypeServiceTest(bool isException = false)
        {
            if (isException)
                _repoMock = GetGlobalDefaulExceptionMock().Object;

            else
                _repoMock = GetGlobalDefaultMock().Object;

            var servicemock = GetService();

        }

        private IPWarpTypeViewModel ViewModel
        {
            get
            {
                return new IPWarpTypeViewModel
                {
                    Code = "1",
                    WarpType = "Testing"
                };
            }
        }
        private IPWarpTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPWarpTypeViewModel
                {
                    Code = "1",
                    WarpType = null
                };
            }
        }
        private IPWarpTypeModel ModelDb
        {
            get
            {
                return new IPWarpTypeModel
                (
                    "1",
                    "Testing"
                );
            }
        }
        private IPWarpTypeModel NotValidModelDb
        {
            get
            {
                return new IPWarpTypeModel
                (
                     "1",
                    null
                );
            }
        }

        private Mock<IIPWarpTypeRepository> GetGlobalDefaultMock()
        {
            var repoMock = new Mock<IIPWarpTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWarpTypeModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<IPWarpTypeModel>{ ModelDb }.AsQueryable());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelDb);
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWarpTypeModel>()))
                .ReturnsAsync(1);
            return repoMock;
        }
        private Mock<IIPWarpTypeRepository> GetGlobalDefaulExceptionMock()
        {
            var repoMock = new Mock<IIPWarpTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWarpTypeModel>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWarpTypeModel>()))
                .Throws(new Exception());
            return repoMock;
        }

        [Fact]
        public async void Should_Success_Create()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0,result);
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public  void Should_Success_ReadAll()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Should_Success_ReadByID()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public  void Should_Success_ReadByPage()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadByPage("1",null,1,1);

            Assert.NotNull( result);
        }
        [Fact]
        public async void Should_Success_Update()
        {
            //v
            var unittest = new IPWarpTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Update(1, ViewModel);
            Assert.NotEqual(0,result);
        }
        [Fact]
        public void Should_Success_Validate()
        {
            IPWarpTypeViewModelValidator validation = new IPWarpTypeViewModelValidator();
            FluentValidation.Results.ValidationResult valid = validation.Validate(ViewModel);
            Assert.True(valid.IsValid);
        }
    }
}
