using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
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
    public class IPWidthTypeServiceTest
    {
        private IIPWidthTypeRepository _repoMock;
        private IPWidthService _serviceMock;
        private IServiceProvider _serviceProvider;

        public IPWidthService GetService(IServiceProvider serviceProvider)
        {
            return new IPWidthService(serviceProvider);
        }
        public IPWidthService GetService()
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IIPWidthTypeRepository)))
                .Returns(_repoMock);
            _serviceProvider = spMock.Object;
            _serviceMock = new IPWidthService(_serviceProvider);
            return _serviceMock;
        }

        public IPWidthTypeServiceTest(bool isException = false)
        {
            if (isException)
                _repoMock = GetGlobalDefaulExceptionMock().Object;

            else
                _repoMock = GetGlobalDefaultMock().Object;

            var servicemock = GetService();

        }

        private IPWidthTypeViewModel ViewModel
        {
            get
            {
                return new IPWidthTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    WidthType = "Testing"
                };
            }
        }
        private IndexViewModel IndexViewModel
        {
            get
            {
                return new IndexViewModel
                {
                    Id = 1,
                    Code = "1",
                    WidthType = "Testing"
                };
            }
        }
        private IPWidthTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPWidthTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    WidthType = null
                };
            }
        }
        private IPWidthTypeModel ModelDb
        {
            get
            {
                return new IPWidthTypeModel
                (
                    "1",
                    "Testing"
                );
            }
        }
        private IPWidthTypeModel NotValidModelDb
        {
            get
            {
                return new IPWidthTypeModel
                (
                     "1",
                    null
                );
            }
        }

        private Mock<IIPWidthTypeRepository> GetGlobalDefaultMock()
        {
            var repoMock = new Mock<IIPWidthTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWidthTypeModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<IPWidthTypeModel>{ ModelDb }.AsQueryable());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelDb);
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWidthTypeModel>()))
                .ReturnsAsync(1);
            return repoMock;
        }
        private Mock<IIPWidthTypeRepository> GetGlobalDefaulExceptionMock()
        {
            var repoMock = new Mock<IIPWidthTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWidthTypeModel>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWidthTypeModel>()))
                .Throws(new Exception());
            return repoMock;
        }

        [Fact]
        public async void Should_Success_Create()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0,result);
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public  void Should_Success_ReadAll()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Should_Success_ReadByID()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public  void Should_Success_ReadByPage()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadByPage("1",null,1,1);

            Assert.NotNull( result);
        }
        [Fact]
        public async void Should_Success_Update()
        {
            //v
            var unittest = new IPWidthTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Update(1, ViewModel);
            Assert.NotEqual(0,result);
        }
        [Fact]
        public void Should_Success_Validate()
        {
            IPWidthTypeViewModelValidator validation = new IPWidthTypeViewModelValidator();
            FluentValidation.Results.ValidationResult valid = validation.Validate(ViewModel);
            Assert.True(valid.IsValid);
        }
        [Fact]
        public void Should_Success_GetIdViewModel()
        {
            var id = ViewModel.Id;
            Assert.NotEqual(0, id);
        }
        [Fact]
        public void Should_Success_GetIndexView()
        {
            var id = IndexViewModel.Id;
            var code = IndexViewModel.Code;
            var type = IndexViewModel.WidthType;
            Assert.NotEqual(0, id);
            Assert.NotNull(code);
            Assert.NotNull(type);

        }
    }
}
