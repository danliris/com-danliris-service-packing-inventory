using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListItemsServiceTest
    {
        protected GarmentShippingPackingListItemsService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingPackingListItemsService(serviceProvider);
        }

        protected GarmentShippingPackingListViewModel ViewModel
        {
            get
            {
                return new GarmentShippingPackingListViewModel
                {
                    Items = new List<GarmentShippingPackingListItemViewModel>
                    {
                        new GarmentShippingPackingListItemViewModel
                        {
                            Details = new List<GarmentShippingPackingListDetailViewModel>()
                            {
                                new GarmentShippingPackingListDetailViewModel
                                {
                                    Sizes = new List<GarmentShippingPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentShippingPackingListDetailSizeViewModel()
                                    },
                                    Length = 10,
                                    Width = 20,
                                    Height =30,
                                    CartonQuantity = 1,
                                    Id = 1,
                                },
                                new GarmentShippingPackingListDetailViewModel
                                {
                                    Sizes = new List<GarmentShippingPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentShippingPackingListDetailSizeViewModel()
                                    },
                                }
                            }
                        }
                    },
                    Measurements = new List<GarmentShippingPackingListMeasurementViewModel>
                    {
                        new GarmentShippingPackingListMeasurementViewModel()
                        {
                           Length = 10,
                           Width = 20,
                           Height =30,
                           CartonsQuantity = 1,
                        },
                        new GarmentShippingPackingListMeasurementViewModel(),
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

        private Mock<IServiceProvider> GetServiceProviderWithIdentity(IGarmentShippingPackingListRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingPackingListRepository)))
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

        private Mock<IGarmentShippingPackingListRepository> GetRepositoryMock(List<GarmentShippingPackingListModel> models)
        {
            var repoMock = new Mock<IGarmentShippingPackingListRepository>();
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
            var items = new HashSet<GarmentShippingPackingListItemModel> { new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", null) { CreatedBy = "UserTest" } };

            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.DRAFT, "", false, "", false, false, false, "");

            var repoMock = new Mock<IGarmentShippingPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProviderWithIdentity(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> { new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1) };
            var details = new HashSet<GarmentShippingPackingListDetailModel> { new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes, 1) };
            var items = new HashSet<GarmentShippingPackingListItemModel> {
                new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details)
            };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> { new GarmentShippingPackingListMeasurementModel(1, 1, 1, 1, "a") };
            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "") { Id = 1 };


            model.StatusActivities.Add(new GarmentShippingPackingListStatusActivityModel("", "", GarmentShippingPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentShippingPackingListModel> models = new List<GarmentShippingPackingListModel> { model };

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentShippingPackingListRepository>();
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
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> {
                new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1){ Id = 1 },
                new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1){ Id = 2 },
            };
            var details = new HashSet<GarmentShippingPackingListDetailModel> {
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 1, 1, 1, sizes, 1){ Id = 1 },
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 2, 3, 1, sizes, 1){ Id = 2 },
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 10, 20, 30, 2, 3, 1, sizes, 1){ Id = 3},
            };
            var items = new HashSet<GarmentShippingPackingListItemModel> {
                new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details){ Id = 1, CreatedBy = "UserTest" },
                new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", new HashSet<GarmentShippingPackingListDetailModel>()){ Id = 2, CreatedBy = "UserTest" }
            };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> {
                new GarmentShippingPackingListMeasurementModel(10, 20, 30, 1, "a"){ Id = 1 },
                new GarmentShippingPackingListMeasurementModel(10, 20, 30, 1, "a"){ Id = 2 },
                new GarmentShippingPackingListMeasurementModel(10, 20, 30, 1, "a"){ Id = 0 },
            };

            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "") { Id = 1 };

            model.StatusActivities.Add(new GarmentShippingPackingListStatusActivityModel("", "", GarmentShippingPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentShippingPackingListModel> models = new List<GarmentShippingPackingListModel> { model };

            var ViewModel = this.ViewModel;

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentShippingPackingListRepository>();
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
                GarmentShippingPackingListItemViewModel item = new GarmentShippingPackingListItemViewModel { Id = i.Id, Details = new List<GarmentShippingPackingListDetailViewModel>() };
                foreach (var d in i.Details)
                {
                    GarmentShippingPackingListDetailViewModel detail = new GarmentShippingPackingListDetailViewModel { Id = d.Id, Length = d.Length, Width = d.Width, Height = d.Height, GrossWeight = d.GrossWeight, NetWeight = d.NetWeight, NetNetWeight = d.NetNetWeight, CartonQuantity = d.CartonQuantity, Sizes = new List<GarmentShippingPackingListDetailSizeViewModel>() };
                    foreach (var s in d.Sizes)
                    {
                        detail.Sizes.Add(new GarmentShippingPackingListDetailSizeViewModel { Id = s.Id });
                        detail.Sizes.Add(new GarmentShippingPackingListDetailSizeViewModel());
                        break;
                    }
                    item.Details.Add(detail);
                    item.Details.Add(new GarmentShippingPackingListDetailViewModel { Sizes = new List<GarmentShippingPackingListDetailSizeViewModel> { new GarmentShippingPackingListDetailSizeViewModel() } });
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
            var sizes = new HashSet<GarmentShippingPackingListDetailSizeModel> {
                new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1){ Id = 1 },
                new GarmentShippingPackingListDetailSizeModel(1, "", 1, 1){ Id = 2 },
            };
            var details = new HashSet<GarmentShippingPackingListDetailModel> {
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 0, sizes, 1){Id = 0},
                new GarmentShippingPackingListDetailModel(2, 2, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 1, sizes, 1){Id = 0},
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 1, sizes, 1){Id = 1},
                new GarmentShippingPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 3, 0, sizes, 1){Id = 2},
            };
            var items = new HashSet<GarmentShippingPackingListItemModel> {
                new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", details){ Id = 1, CreatedBy = "UserTest" },
                new GarmentShippingPackingListItemModel("", "", "", 1, "", 1, "", "", "",  "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "", new HashSet<GarmentShippingPackingListDetailModel>()){ Id = 2, CreatedBy = "UserTest" }
            };
            var measurements = new HashSet<GarmentShippingPackingListMeasurementModel> {
                new GarmentShippingPackingListMeasurementModel(1, 1, 1, 1, "a"){ Id = 1 },
                new GarmentShippingPackingListMeasurementModel(1, 2, 3, 1, "a"){ Id = 2 },
                new GarmentShippingPackingListMeasurementModel(4, 5, 6, 1, "a"){ Id = 2 },
            };

            var model = new GarmentShippingPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentShippingPackingListStatusEnum.CREATED, "", false, "", false, false, false, "") { Id = 1 };

            model.StatusActivities.Add(new GarmentShippingPackingListStatusActivityModel("", "", GarmentShippingPackingListStatusEnum.CREATED.ToString(), ""));

            List<GarmentShippingPackingListModel> models = new List<GarmentShippingPackingListModel> { model };

            var ViewModel = this.ViewModel;

            var mockModels = models.AsQueryable().BuildMock();
            var repoMock = new Mock<IGarmentShippingPackingListRepository>();
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
                GarmentShippingPackingListItemViewModel item = new GarmentShippingPackingListItemViewModel { Id = i.Id, Details = new List<GarmentShippingPackingListDetailViewModel>() };
                foreach (var d in i.Details)
                {
                    GarmentShippingPackingListDetailViewModel detail = new GarmentShippingPackingListDetailViewModel { Id = d.Id, Carton1 = d.Carton1, Carton2 = d.Carton2, Length = d.Length, Width = d.Width, Height = d.Height, GrossWeight = d.GrossWeight, NetWeight = d.NetWeight, NetNetWeight = d.NetNetWeight, CartonQuantity = d.CartonQuantity, Sizes = new List<GarmentShippingPackingListDetailSizeViewModel>() };
                    foreach (var s in d.Sizes)
                    {
                        detail.Sizes.Add(new GarmentShippingPackingListDetailSizeViewModel { Id = s.Id });
                        detail.Sizes.Add(new GarmentShippingPackingListDetailSizeViewModel());
                        break;
                    }
                    item.Details.Add(detail);
                    item.Details.Add(new GarmentShippingPackingListDetailViewModel { Sizes = new List<GarmentShippingPackingListDetailSizeViewModel> { new GarmentShippingPackingListDetailSizeViewModel() } });
                }
                ViewModel.Items.Add(item);
            }
            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }
    }
}