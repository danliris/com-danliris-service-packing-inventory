using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentLetterOfCreditRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentLetterOfCreditRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentLetterOfCreditService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLetterOfCreditService(serviceProvider);
        }

        protected GarmentLetterOfCreditViewModel ViewModel
        {
            get
            {
                return new GarmentLetterOfCreditViewModel
                {
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLetterOfCreditModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLetterOfCreditModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLetterOfCreditModel("no", DateTimeOffset.Now, "", 1, "", "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", 1, 1, "", 2);

            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLetterOfCreditModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingLetterOfCreditModel("no", DateTimeOffset.Now, "", 1, "", "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", 1, 1, "", 2);

            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLetterOfCreditModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
