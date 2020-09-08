using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesDORepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesDORepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalSalesDOService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalSalesDOService(serviceProvider);
        }

        protected GarmentShippingLocalSalesDOViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesDOViewModel
                {
                    items = new List<GarmentShippingLocalSalesDOItemViewModel>()
                    {
                        new GarmentShippingLocalSalesDOItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesDORepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalSalesDOModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesDOModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalSalesDOModel("", "", 1, DateTimeOffset.Now, 1, "", "", "", "", new List<GarmentShippingLocalSalesDOItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalSalesDORepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesDOModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalSalesDOItemModel>() { new GarmentShippingLocalSalesDOItemModel(1,1,1, "", "", "", 1, 1, "", 1, 1, 1, 1) };
            var model = new GarmentShippingLocalSalesDOModel("", "", 1, DateTimeOffset.Now, 1, "", "", "", "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesDORepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesDORepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesDOModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesDORepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
