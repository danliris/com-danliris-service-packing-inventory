using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WarpType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.Master
{
    public class WarpTypeServiceTest
    {
        public WarpTypeService GetService(IServiceProvider serviceProvider)
        {
            return new WarpTypeService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IWarpTypeRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IWarpTypeRepository)))
                .Returns(repository);

            return spMock;
        }

        private WarpTypeViewModel ViewModel
        {
            get
            {
                return new WarpTypeViewModel()
                {
                    Id = 1,
                    Type = "test",
                    Code = "01"
                };
            }
        }

        private WarpTypeModel Model
        {
            get
            {
                return new WarpTypeModel(ViewModel.Type, ViewModel.Code);
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<WarpTypeModel>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<WarpTypeModel>() { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", Model.Type);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(WarpTypeModel));


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<WarpTypeModel>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Upload()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.MultipleInsertAsync(It.IsAny<IEnumerable<WarpTypeModel>>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Upload(new List<WarpTypeViewModel>() { ViewModel });

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_UploadValidate()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<WarpTypeModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.UploadValidate(new List<WarpTypeViewModel>() { ViewModel });

            Assert.True(result.Item1);
        }

        [Fact]
        public void Should_Error_UploadValidate_EmptyData()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<WarpTypeModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var vm = new WarpTypeViewModel();
            var result = service.UploadValidate(new List<WarpTypeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "test";
            result = service.UploadValidate(new List<WarpTypeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "0";
            result = service.UploadValidate(new List<WarpTypeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "110";
            result = service.UploadValidate(new List<WarpTypeViewModel>() { vm });

            Assert.False(result.Item1);
        }

        [Fact]
        public void Should_Error_UploadValidate_ExistData()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<WarpTypeModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.UploadValidate(new List<WarpTypeViewModel>() { ViewModel });

            Assert.False(result.Item1);

        }


        [Fact]
        public void Should_Success_ValidateHeader()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ValidateHeader(new List<string>() { "Jenis Lusi", "Kode" });

            Assert.True(result);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IWarpTypeRepository>();
            var model = Model;
            model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<WarpTypeModel>() { model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new WarpTypeViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "str";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "0";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "120";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = Model.Type;
            vm.Code = Model.Code;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }

        [Fact]
        public void Should_Success_DownloadTemplate()
        {
            var repoMock = new Mock<IWarpTypeRepository>();

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.DownloadTemplate();

            Assert.NotNull(result);
        }
    }
}
