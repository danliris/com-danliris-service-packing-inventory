using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    },
                    ShippingMarkImagePath = "IMG_1_000101010000000000_ShippingMarkImageFile",
                    ShippingMarkImageFile = "ShippingMarkImageFile",
                    SideMarkImagePath = null,
                    SideMarkImageFile = "SideMarkImageFile",
                    RemarkImagePath = "IMG_1_000101010000000000_RemarkImagePath",
                    RemarkImageFile = null,
                };
            }
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
        public async Task Create_Success()
        {
            var repoMock = GetRepositoryMock(new List<GarmentPackingListModel>());
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentPackingListModel>()))
                .ReturnsAsync(1);

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.GetFileNameFromPath(It.Is<string>(str => str == ViewModel.ShippingMarkImagePath)))
                .Returns(ViewModel.ShippingMarkImagePath);
            imageServiceMock.Setup(s => s.RemoveImage(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            imageServiceMock.Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImagePath");

            var serviceProviderMock = GetServiceProviderWithIdentity(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            var result = await service.Create(ViewModel);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = GetRepositoryMock(new List<GarmentPackingListModel>());
            repoMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel> { new GarmentPackingListModel { Id = ViewModel.Id } }.AsQueryable());

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.GetFileNameFromPath(It.Is<string>(str => str == ViewModel.ShippingMarkImagePath)))
                .Returns(ViewModel.ShippingMarkImagePath);
            imageServiceMock.Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImagePath");

            var serviceProviderMock = GetServiceProviderWithIdentity(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            var result = await service.Update(ViewModel.Id, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task ReadPdfById_Success()
        {
            var sizesA = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "A", 1) };
            var sizesB = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "B", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesA), new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB), new GarmentPackingListDetailModel(2, 5, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB) };
            var details2 = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesA), new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1), new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1), new GarmentPackingListItemModel("a", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details2, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            //var spMock = GetServiceProvider(repoMock.Object);
            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImageFile");

            var serviceProviderMock = GetServiceProvider(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            serviceProviderMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(serviceProviderMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }
    }
}
