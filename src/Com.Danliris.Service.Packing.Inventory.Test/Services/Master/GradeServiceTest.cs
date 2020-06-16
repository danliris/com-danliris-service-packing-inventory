using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.Grade;
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
    public class GradeServiceTest
    {
        public GradeService GetService(IServiceProvider serviceProvider)
        {
            return new GradeService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IGradeRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGradeRepository)))
                .Returns(repository);

            return spMock;
        }

        private GradeViewModel ViewModel
        {
            get
            {
                return new GradeViewModel()
                {
                    Id = 1,
                    Type = "test",
                    Code = "1",
                    IsAvalGrade = true,
                };
            }
        }

        private GradeModel Model
        {
            get
            {
                return new GradeModel(ViewModel.Type, ViewModel.Code, ViewModel.IsAvalGrade);
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<GradeModel>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GradeModel>() { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", Model.Type);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(GradeModel));


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GradeModel>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Upload()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.MultipleInsertAsync(It.IsAny<IEnumerable<GradeModel>>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Upload(new List<GradeViewModel>() { ViewModel });

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_UploadValidate()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GradeModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.UploadValidate(new List<GradeViewModel>() { ViewModel });

            Assert.True(result.Item1);
        }

        [Fact]
        public void Should_Error_UploadValidate_EmptyData()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GradeModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var vm = new GradeViewModel();
            var result = service.UploadValidate(new List<GradeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "test";
            result = service.UploadValidate(new List<GradeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "0";
            result = service.UploadValidate(new List<GradeViewModel>() { vm });

            Assert.False(result.Item1);

            vm.Code = "110";
            result = service.UploadValidate(new List<GradeViewModel>() { vm });

            Assert.False(result.Item1);
        }

        [Fact]
        public void Should_Error_UploadValidate_ExistData()
        {
            var repoMock = new Mock<IGradeRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GradeModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.UploadValidate(new List<GradeViewModel>() { ViewModel });

            Assert.False(result.Item1);

        }


        [Fact]
        public void Should_Success_ValidateHeader()
        {
            var repoMock = new Mock<IGradeRepository>();

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ValidateHeader(new List<string>() { "Grade", "Kode" });

            Assert.True(result);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IGradeRepository>();
            var model = Model;
            model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GradeModel>() { model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new GradeViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "str";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "0";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Code = "1202";
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
            var repoMock = new Mock<IGradeRepository>();

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.DownloadTemplate();

            Assert.NotNull(result);
        }
    }
}
