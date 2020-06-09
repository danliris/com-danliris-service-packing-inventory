using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentCoverLetterRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentCoverLetterRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentCoverLetterService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentCoverLetterService(serviceProvider);
        }

        protected GarmentCoverLetterViewModel ViewModel
        {
            get
            {
                return new GarmentCoverLetterViewModel
                {
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentCoverLetterModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentCoverLetterModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentCoverLetterModel(1, "", DateTimeOffset.Now, "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", "");

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentCoverLetterModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentCoverLetterModel(1, "", DateTimeOffset.Now, "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", "");

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentCoverLetterModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
