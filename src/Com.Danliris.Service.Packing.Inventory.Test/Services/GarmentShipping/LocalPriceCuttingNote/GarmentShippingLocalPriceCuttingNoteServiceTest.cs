using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalPriceCuttingNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalPriceCuttingNoteRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalPriceCuttingNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalPriceCuttingNoteService(serviceProvider);
        }

        protected GarmentShippingLocalPriceCuttingNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalPriceCuttingNoteViewModel
                {
                    items = new List<GarmentShippingLocalPriceCuttingNoteItemViewModel>()
                    {
                        new GarmentShippingLocalPriceCuttingNoteItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalPriceCuttingNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalPriceCuttingNoteModel("", DateTimeOffset.Now, 1, "", "", true, "", new List<GarmentShippingLocalPriceCuttingNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1) };
            var model = new GarmentShippingLocalPriceCuttingNoteModel("", DateTimeOffset.Now, 1, "", "", true, "", items);

            var repoMock = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
