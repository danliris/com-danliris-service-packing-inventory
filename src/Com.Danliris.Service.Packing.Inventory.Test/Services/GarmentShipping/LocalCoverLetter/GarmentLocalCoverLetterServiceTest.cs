using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentLocalCoverLetter
{
    public class GarmentLocalCoverLetterServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentLocalCoverLetterRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentLocalCoverLetterRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentLocalCoverLetterService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLocalCoverLetterService(serviceProvider);
        }

        protected GarmentLocalCoverLetterViewModel ViewModel
        {
            get
            {
                return new GarmentLocalCoverLetterViewModel
                {
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalCoverLetterModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalCoverLetterModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "", DateTimeOffset.Now, "", "", "", 1, "");

            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalCoverLetterModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "", DateTimeOffset.Now, "", "", "", 1, "");

            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadByLocalSalesNoteId_Success()
        {
            var model = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "BC123", DateTimeOffset.Now, "", "", "", 1, "");

            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByLocalSalesNoteIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadByLocalSalesNoteId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalCoverLetterModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
