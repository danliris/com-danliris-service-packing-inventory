using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentDebiturBalanceRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentDebiturBalanceRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentDebiturBalanceService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentDebiturBalanceService(serviceProvider);
        }

        protected GarmentDebiturBalanceViewModel ViewModel
        {
            get
            {
                return new GarmentDebiturBalanceViewModel
                {
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentDebiturBalanceRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentDebiturBalanceModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentDebiturBalanceModel>().AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            var service = GetService(spMock.Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentDebiturBalanceModel(DateTimeOffset.Now, 1, "", "", 0);

            var repoMock = new Mock<IGarmentDebiturBalanceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentDebiturBalanceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentDebiturBalanceModel(DateTimeOffset.Now, 1, "", "", 0);

            var repoMock = new Mock<IGarmentDebiturBalanceRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentDebiturBalanceRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentDebiturBalanceModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var model = new GarmentDebiturBalanceModel(DateTimeOffset.Now, 1, "", "", 0);

            var repoMock = new Mock<IGarmentDebiturBalanceRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var spMock = GetServiceProvider(repoMock.Object);
 
            var service = GetService(spMock.Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }    
    }
}
