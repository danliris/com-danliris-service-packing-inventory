using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentPackingListService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentPackingListService(serviceProvider);
        }

        protected GarmentPackingListViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListViewModel
                {
                    Items = new List<GarmentPackingListItemViewModel>
                    {
                        new GarmentPackingListItemViewModel
                        {
                            Details = new List<GarmentPackingListDetailViewModel>()
                            {
                                new GarmentPackingListDetailViewModel
                                {
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentPackingListDetailSizeViewModel()
                                    }
                                }
                            }
                        }
                    },
                    Measurements = new List<GarmentPackingListMeasurementViewModel>
                    {
                        new GarmentPackingListMeasurementViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentPackingListModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", 1, "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, null, 1, 1, 1, null, "", "", "", false);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }
		[Fact]
		public void ReadNotUsed_Success()
		{
			var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", 1, "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, null, 1, 1, 1, null, "", "", "", false);

			var repoMock = new Mock<IGarmentPackingListRepository>();
			repoMock.Setup(s => s.ReadAll())
				.Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

			var service = GetService(GetServiceProvider(repoMock.Object).Object);

			var result = service.ReadNotUsed(1, 25, "{}", "{}", null);

			Assert.NotEmpty(result.Data);
		}
		[Fact]
        public async Task ReadById_Success()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", 1, 1, 1, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, "", 1, "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", 1, "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, items, 1, 1, 1, measurements, "", "", "", false);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPackingListModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
