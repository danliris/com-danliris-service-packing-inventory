using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListItemsServiceTest
    {
        protected GarmentPackingListItemsService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentPackingListItemsService(serviceProvider);
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
                                    },
                                    Length = 10,
                                    Width = 20,
                                    Height =30,
                                    CartonQuantity = 1,
                                    Id = 1,
                                },
                                new GarmentPackingListDetailViewModel
                                {
                                    Sizes = new List<GarmentPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentPackingListDetailSizeViewModel()
                                    },
                                }
                            }
                        }
                    },
                    Measurements = new List<GarmentPackingListMeasurementViewModel>
                    {
                        new GarmentPackingListMeasurementViewModel()
                        {
                           Length = 10,
                           Width = 20,
                           Height =30,
                           CartonsQuantity = 1,
                        },
                        new GarmentPackingListMeasurementViewModel(),
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
        public void Read_Success()
        {
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", null) { CreatedBy = "UserTest" } };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.DRAFT, "", false, "");

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProviderWithIdentity(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes,1) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "") { Id = 1 };
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel("", "", GarmentPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentPackingListModel> models = new List<GarmentPackingListModel> { model };

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock
                .Setup(s => s.Query)
                .Returns(mockModels.Object);

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImageFile");

            var serviceProviderMock = GetServiceProviderWithIdentity(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            var result = await service.ReadById(model.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> {
                new GarmentPackingListDetailSizeModel(1, "", 1){ Id = 1 },
                new GarmentPackingListDetailSizeModel(1, "", 1){ Id = 2 },
            };
            var details = new HashSet<GarmentPackingListDetailModel> {
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 1, 1, 1, sizes, 1){ Id = 1 },
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 2, 3, 1, sizes, 1){ Id = 2 },
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 2, 3, 1, sizes, 1){ Id = 3},
            };
            var items = new HashSet<GarmentPackingListItemModel> {
                new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details){ Id = 1, CreatedBy = "UserTest" },
                new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", new HashSet<GarmentPackingListDetailModel>()){ Id = 2, CreatedBy = "UserTest" }
            };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> {
                new GarmentPackingListMeasurementModel(10, 20, 30, 1){ Id = 1 },
                new GarmentPackingListMeasurementModel(10, 20, 30, 1){ Id = 2 },
                new GarmentPackingListMeasurementModel(10, 20, 30, 1){ Id = 0 },
            };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "") { Id = 1 };
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel("", "", GarmentPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentPackingListModel> models = new List<GarmentPackingListModel> { model };

            var ViewModel = this.ViewModel;

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock
                .Setup(s => s.Query)
                .Returns(mockModels.Object);
            repoMock
                .Setup(s => s.SaveChanges())
                .ReturnsAsync(1);

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.GetFileNameFromPath(It.Is<string>(str => str == ViewModel.ShippingMarkImagePath)))
                .Returns(ViewModel.ShippingMarkImagePath);
            imageServiceMock.Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImagePath");

            var serviceProviderMock = GetServiceProviderWithIdentity(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            foreach (var i in model.Items)
            {
                GarmentPackingListItemViewModel item = new GarmentPackingListItemViewModel { Id = i.Id, Details = new List<GarmentPackingListDetailViewModel>() };
                foreach (var d in i.Details)
                {
                    GarmentPackingListDetailViewModel detail = new GarmentPackingListDetailViewModel { Id = d.Id, Length = d.Length, Width = d.Width, Height = d.Height, GrossWeight = d.GrossWeight, NetWeight = d.NetWeight, NetNetWeight = d.NetNetWeight, CartonQuantity = d.CartonQuantity, Sizes = new List<GarmentPackingListDetailSizeViewModel>() };
                    foreach (var s in d.Sizes)
                    {
                        detail.Sizes.Add(new GarmentPackingListDetailSizeViewModel { Id = s.Id });
                        detail.Sizes.Add(new GarmentPackingListDetailSizeViewModel());
                        break;
                    }
                    item.Details.Add(detail);
                    item.Details.Add(new GarmentPackingListDetailViewModel { Sizes = new List<GarmentPackingListDetailSizeViewModel> { new GarmentPackingListDetailSizeViewModel() } });
                    break;
                }
                ViewModel.Items.Add(item);
                break;
            }
            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Update_ZeroNetNetWeight_Success()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> {
                new GarmentPackingListDetailSizeModel(1, "", 1){ Id = 1 },
                new GarmentPackingListDetailSizeModel(1, "", 1){ Id = 2 },
            };
            var details = new HashSet<GarmentPackingListDetailModel> {
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 0, sizes, 1){Id = 0},
                new GarmentPackingListDetailModel(2, 2, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 1, sizes, 1){Id = 0},
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 1, sizes, 1){Id = 1},
                new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 0, sizes, 1){Id = 2},
            };
            var items = new HashSet<GarmentPackingListItemModel> {
                new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details){ Id = 1, CreatedBy = "UserTest" },
                new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", new HashSet<GarmentPackingListDetailModel>()){ Id = 2, CreatedBy = "UserTest" }
            };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> {
                new GarmentPackingListMeasurementModel(1, 1, 1, 1){ Id = 1 },
                new GarmentPackingListMeasurementModel(1, 2, 3, 1){ Id = 2 },
                new GarmentPackingListMeasurementModel(4, 5, 6, 1){ Id = 2 },
            };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "") { Id = 1 };
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel("", "", GarmentPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentPackingListModel> models = new List<GarmentPackingListModel> { model };

            var ViewModel = this.ViewModel;

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock
                .Setup(s => s.Query)
                .Returns(mockModels.Object);
            repoMock
                .Setup(s => s.SaveChanges())
                .ReturnsAsync(1);

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.GetFileNameFromPath(It.Is<string>(str => str == ViewModel.ShippingMarkImagePath)))
                .Returns(ViewModel.ShippingMarkImagePath);
            imageServiceMock.Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImagePath");

            var serviceProviderMock = GetServiceProviderWithIdentity(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            foreach (var i in model.Items)
            {
                GarmentPackingListItemViewModel item = new GarmentPackingListItemViewModel { Id = i.Id, Details = new List<GarmentPackingListDetailViewModel>() };
                foreach (var d in i.Details)
                {
                    GarmentPackingListDetailViewModel detail = new GarmentPackingListDetailViewModel { Id = d.Id, Carton1 = d.Carton1, Carton2 = d.Carton2, Length = d.Length, Width = d.Width, Height = d.Height, GrossWeight = d.GrossWeight, NetWeight = d.NetWeight, NetNetWeight = d.NetNetWeight, CartonQuantity = d.CartonQuantity, Sizes = new List<GarmentPackingListDetailSizeViewModel>() };
                    foreach (var s in d.Sizes)
                    {
                        detail.Sizes.Add(new GarmentPackingListDetailSizeViewModel { Id = s.Id });
                        detail.Sizes.Add(new GarmentPackingListDetailSizeViewModel());
                        break;
                    }
                    item.Details.Add(detail);
                    item.Details.Add(new GarmentPackingListDetailViewModel { Sizes = new List<GarmentPackingListDetailSizeViewModel> { new GarmentPackingListDetailSizeViewModel() } });
                }
                ViewModel.Items.Add(item);
            }
            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }
    }
}
