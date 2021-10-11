using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentDraftPackingListItemRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentDraftPackingListItemRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentDraftPackingListItemService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentDraftPackingListItemService(serviceProvider);
        }

        protected GarmentDraftPackingListItemViewModel ViewModel
        {
            get
            {
                return new GarmentDraftPackingListItemViewModel
                {
                    Details = new List<GarmentDraftPackingListDetailViewModel>()
                    {
                        new GarmentDraftPackingListDetailViewModel
                        {
                            NetWeight = 10,
                            NetNetWeight = 0,
                            Id = 1,
                            Sizes = new List<GarmentDraftPackingListDetailSizeViewModel>()
                            {
                                new GarmentDraftPackingListDetailSizeViewModel()
                            }
                        },
                        new GarmentDraftPackingListDetailViewModel
                        {
                            NetWeight = 10,
                            NetNetWeight = 10,
                            Id = 2,
                            Sizes = new List<GarmentDraftPackingListDetailSizeViewModel>()
                            {
                                new GarmentDraftPackingListDetailSizeViewModel()
                            }
                        }
                    }
                };
            }
        }

        protected GarmentDraftPackingListItemViewModels ViewModels
        {
            get
            {
                return new GarmentDraftPackingListItemViewModels
                {
                    Items = new List<GarmentDraftPackingListItemViewModel>
                    {
                        new GarmentDraftPackingListItemViewModel
                        {
                            Details = new List<GarmentDraftPackingListDetailViewModel>()
                            {
                                new GarmentDraftPackingListDetailViewModel
                                {
                                    NetWeight = 10,
                                    NetNetWeight = 0,
                                    Id = 1,
                                    Sizes = new List<GarmentDraftPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentDraftPackingListDetailSizeViewModel()
                                    }
                                },
                                new GarmentDraftPackingListDetailViewModel
                                {
                                    NetWeight = 10,
                                    NetNetWeight = 10,
                                    Id = 2,
                                    Sizes = new List<GarmentDraftPackingListDetailSizeViewModel>()
                                    {
                                        new GarmentDraftPackingListDetailSizeViewModel()
                                    }
                                }
                            }
                        }
                    },
                };
            }
        }

        private Mock<IGarmentDraftPackingListItemRepository> GetRepositoryMock(List<GarmentDraftPackingListItemModel> models)
        {
            var repoMock = new Mock<IGarmentDraftPackingListItemRepository>();
            repoMock
                .Setup(s => s.Query)
                .Returns(models.AsQueryable());
            repoMock
                .Setup(s => s.SaveChanges())
                .ReturnsAsync(1);

            return repoMock;
        }

        private Mock<IServiceProvider> GetServiceProviderWithIdentity(IGarmentDraftPackingListItemRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentDraftPackingListItemRepository)))
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

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentDraftPackingListItemRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentDraftPackingListItemModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentDraftPackingListItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModels.Items.ToList());

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentDraftPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "",new List<GarmentDraftPackingListDetailModel>());

            var repoMock = new Mock<IGarmentDraftPackingListItemRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentDraftPackingListItemModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", "");

            Assert.NotEmpty(result.Data);
        }
    }
}
