using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWovenType;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class IPWovenTypeServiceTest
    {
        private IIPWovenTypeRepository _repoMock;
        private IPWovenTypeService _serviceMock;
        private IServiceProvider _serviceProvider;

        public IPWovenTypeService GetService(IServiceProvider serviceProvider)
        {
            return new IPWovenTypeService(serviceProvider);
        }
        public IPWovenTypeService GetService()
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IIPWovenTypeRepository)))
                .Returns(_repoMock);
            _serviceProvider = spMock.Object;
            _serviceMock = new IPWovenTypeService(_serviceProvider);
            return _serviceMock;
        }

        public IPWovenTypeServiceTest(bool isException = false)
        {
            if (isException)
                _repoMock = GetGlobalDefaulExceptionMock().Object;

            else
                _repoMock = GetGlobalDefaultMock().Object;

            var servicemock = GetService();

        }

        private IPWovenTypeViewModel ViewModel
        {
            get
            {
                return new IPWovenTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    WovenType = "Testing"
                };
            }
        }
        private IPWovenTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPWovenTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    WovenType = null
                };
            }
        }
        private IPWovenTypeModel ModelDb
        {
            get
            {
                return new IPWovenTypeModel
                (
                    "1",
                    "Testing"
                );
            }
        }
        private IPWovenTypeModel NotValidModelDb
        {
            get
            {
                return new IPWovenTypeModel
                (
                     "1",
                    null
                );
            }
        }

        private Mock<IIPWovenTypeRepository> GetGlobalDefaultMock()
        {
            var repoMock = new Mock<IIPWovenTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWovenTypeModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<IPWovenTypeModel>{ ModelDb }.AsQueryable());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelDb);
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWovenTypeModel>()))
                .ReturnsAsync(1);
            return repoMock;
        }
        private Mock<IIPWovenTypeRepository> GetGlobalDefaulExceptionMock()
        {
            var repoMock = new Mock<IIPWovenTypeRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<IPWovenTypeModel>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<IPWovenTypeModel>()))
                .Throws(new Exception());
            return repoMock;
        }

        [Fact]
        public async void Should_Success_Create()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0,result);
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public  void Should_Success_ReadAll()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Should_Success_ReadByID()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public  void Should_Success_ReadByPage()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result =  service.ReadByPage("1",null,1,1);

            Assert.NotNull( result);
        }
        [Fact]
        public async void Should_Success_Update()
        {
            //v
            var unittest = new IPWovenTypeServiceTest();
            var service = unittest._serviceMock;
            var result = await service.Update(1, ViewModel);
            Assert.NotEqual(0,result);
        }
        [Fact]
        public void Should_Success_Validate()
        {
            IPWovenTypeViewModelValidator validation = new IPWovenTypeViewModelValidator();
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
