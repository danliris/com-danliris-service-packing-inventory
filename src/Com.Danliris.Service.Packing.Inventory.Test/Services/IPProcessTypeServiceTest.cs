using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
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
    public class IPProcessTypeServiceTest
    {
        private IIPProcessTypeRepository _repoMock;
        private IPProcessTypeService _serviceMock;
        private IServiceProvider _serviceProvider;

        public IPProcessTypeService GetService(IServiceProvider serviceProvider)
        {
            return new IPProcessTypeService(serviceProvider);
        }
        public IPProcessTypeService GetService()
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IIPProcessTypeRepository)))
                .Returns(_repoMock);
            _serviceProvider = spMock.Object;
            _serviceMock = new IPProcessTypeService(_serviceProvider);
            return _serviceMock;
        }

        public IPProcessTypeServiceTest(bool isException = false)
        {
            if (isException)
                _repoMock = GetGlobalDefaulExceptionMock().Object;

            else
                _repoMock = GetGlobalDefaultMock().Object;

            var servicemock = GetService();

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
        private IPProcessTypeModel ModelDb
        {
            get
            {
                return new IPProcessTypeModel
                (
                    "1",
                    "Testing"
                );
            }
        }
        private IPProcessTypeModel NotValidModelDb
        {
            get
            {
                return new IPProcessTypeModel
                (
                     "1",
                    null
                );
            }
        }

        private Mock<IIPProcessTypeRepository> GetGlobalDefaultMock()
        {
            var repoMock = new Mock<IIPProcessTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPProcessTypeModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<IPProcessTypeModel>{ ModelDb }.AsQueryable());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelDb);
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPProcessTypeModel>()))
                .ReturnsAsync(1);
            return repoMock;
        }
        private Mock<IIPProcessTypeRepository> GetGlobalDefaulExceptionMock()
        {
            var repoMock = new Mock<IIPProcessTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPProcessTypeModel>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPProcessTypeModel>()))
                .Throws(new Exception());
            return repoMock;
        }

        [Fact]
        public async void Should_Success_Create()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0,result);
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public  void Should_Success_ReadAll()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Should_Success_ReadByID()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public  void Should_Success_ReadByPage()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadByPage("1",null,1,1);

            Assert.NotNull( result);
        }
        [Fact]
        public async void Should_Success_Update()
        {
            //v
            var unittest = new IPProcessTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Update(1, ViewModel);
            Assert.NotEqual(0,result);
        }
        [Fact]
        public void Should_Success_Validate()
        {
            IPProcessTypeViewModelValidator validation = new IPProcessTypeViewModelValidator();
            FluentValidation.Results.ValidationResult valid = validation.Validate(ViewModel);
            Assert.True(valid.IsValid);
        }
    }
}
