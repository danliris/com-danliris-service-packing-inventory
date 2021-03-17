using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingCostStructureRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCostStructureRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);

            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider_Error(IGarmentShippingCostStructureRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCostStructureRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);

            return spMock;
        }

        protected GarmentShippingCostStructureService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingCostStructureService(serviceProvider);
        }

        protected GarmentShippingCostStructureViewModel ViewModel
        {
            get
            {
                return new GarmentShippingCostStructureViewModel
                {
                   
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingCostStructureRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingCostStructureModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCostStructureModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingCostStructureModel("invoiceno", DateTimeOffset.Now, 1, "comodityCode", "comodityName", "hsCode", "destination", 1, "fabricType", 10000);

            var repoMock = new Mock<IGarmentShippingCostStructureRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCostStructureModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingCostStructureModel("invoiceno", DateTimeOffset.Now, 1, "comodityCode", "comodityName", "hsCode", "destination", 1, "fabricType", 10000);

            var repoMock = new Mock<IGarmentShippingCostStructureRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingCostStructureRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingCostStructureModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingCostStructureRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
