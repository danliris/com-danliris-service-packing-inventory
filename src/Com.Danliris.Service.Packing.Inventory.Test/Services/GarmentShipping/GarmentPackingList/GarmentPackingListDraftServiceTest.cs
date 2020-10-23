using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentPackingListDraftService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentPackingListDraftService(serviceProvider);
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

            Assert.NotEmpty(result);
        }

        private Mock<IServiceProvider> GetServiceProviderWithIdentity(IGarmentPackingListRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repository);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            return spMock;
        }

        private Mock<IGarmentPackingListRepository> GetRepositoryMock(List<GarmentPackingListModel> models)
        {
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock
                .Setup(s => s.Query)
                .Returns(models.AsQueryable());
            repoMock
                .Setup(s => s.SaveChanges())
                .ReturnsAsync(1);

            return repoMock;
        }

        [Fact]
        public async Task PostBooking_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 }
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var id = models.Select(s => s.Id).First();

            await service.PostBooking(id);
        }

        [Fact]
        public async Task UnpostBooking_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 }
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var id = models.Select(s => s.Id).First();

            await service.UnpostBooking(id);
        }
    }
}
