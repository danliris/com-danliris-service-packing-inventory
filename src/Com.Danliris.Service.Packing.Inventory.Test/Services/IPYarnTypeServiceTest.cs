using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
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
    public class IPYarnTypeServiceTest
    {
        private IIPYarnTypeRepository _repoMock;
        private IPYarnTypeService _serviceMock;
        private IServiceProvider _serviceProvider;

        public IPYarnTypeService GetService(IServiceProvider serviceProvider)
        {
            return new IPYarnTypeService(serviceProvider);
        }
        public IPYarnTypeService GetService()
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IIPYarnTypeRepository)))
                .Returns(_repoMock);
            _serviceProvider = spMock.Object;
            _serviceMock = new IPYarnTypeService(_serviceProvider);
            return _serviceMock;
        }

        public IPYarnTypeServiceTest(bool isException = false)
        {
            if (isException)
                _repoMock = GetGlobalDefaulExceptionMock().Object;

            else
                _repoMock = GetGlobalDefaultMock().Object;

            var servicemock = GetService();

        }

        private IPYarnTypeViewModel ViewModel
        {
            get
            {
                return new IPYarnTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    YarnType = "Testing"
                };
            }
        }
        private IPYarnTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPYarnTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    YarnType = null
                };
            }
        }
        private IPYarnTypeModel ModelDb
        {
            get
            {
                return new IPYarnTypeModel
                (
                    "1",
                    "Testing"
                );
            }
        }
        private IPYarnTypeModel NotValidModelDb
        {
            get
            {
                return new IPYarnTypeModel
                (
                     "1",
                    null
                );
            }
        }

        private Mock<IIPYarnTypeRepository> GetGlobalDefaultMock()
        {
            var repoMock = new Mock<IIPYarnTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPYarnTypeModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<IPYarnTypeModel>{ ModelDb }.AsQueryable());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelDb);
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPYarnTypeModel>()))
                .ReturnsAsync(1);
            return repoMock;
        }
        private Mock<IIPYarnTypeRepository> GetGlobalDefaulExceptionMock()
        {
            var repoMock = new Mock<IIPYarnTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPYarnTypeModel>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPYarnTypeModel>()))
                .Throws(new Exception());
            return repoMock;
        }

        [Fact]
        public async void Should_Success_Create()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0,result);
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public  void Should_Success_ReadAll()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Should_Success_ReadByID()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public  void Should_Success_ReadByPage()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadByPage("1",null,1,1);

            Assert.NotNull( result);
        }
        [Fact]
        public async void Should_Success_Update()
        {
            //v
            var unittest = new IPYarnTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Update(1, ViewModel);
            Assert.NotEqual(0,result);
        }
        [Fact]
        public void Should_Success_Validate()
        {
            IPYarnTypeViewModelValidator validation = new IPYarnTypeViewModelValidator();
            FluentValidation.Results.ValidationResult valid = validation.Validate(ViewModel);
            Assert.True(valid.IsValid);
        }
        [Fact]
        public void Should_Success_GetIdViewModel()
        {
            var id = ViewModel.Id;
            Assert.NotEqual(0, id);
        }
    }
}
