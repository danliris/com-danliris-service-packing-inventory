using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Microsoft.EntityFrameworkCore;
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

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", new List<GarmentPackingListItemModel>(), 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", "");

            Assert.NotEmpty(result.Data);
        }
		[Fact]
		public void ReadNotUsed_Success()
		{
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

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
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel("", "", GarmentPackingListStatusEnum.CREATED, ""));

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var imageServiceMock = new Mock<IAzureImageService>();
            imageServiceMock.Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ImageFile");

            var serviceProviderMock = GetServiceProvider(repoMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IAzureImageService)))
                .Returns(imageServiceMock.Object);

            var service = GetService(serviceProviderMock.Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadByInvoiceNo_Success()
        {
            var sizes = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizes) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByInvoiceNoAsync(It.IsAny<string>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadByInvoiceNo("no");

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

        [Fact]
        public async Task ReadPdfById_Success()
        {
            var sizesA = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "A", 1) };
            var sizesB = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "B", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesA), new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var itemsInvoice = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
            var adjustmentsInvoice = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "fee", 100) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "fee", 1, 1) };
            var modelInvoice = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", itemsInvoice, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustmentsInvoice, 100000, "aa", "aa", units);

            var repoInvoiceMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoInvoiceMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { modelInvoice }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repoInvoiceMock.Object);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_LC_Success()
        {
            var sizesA = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "A", 1) };
            var sizesB = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "B", 1) };
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesA), new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var itemsInvoice = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
            var adjustmentsInvoice = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "fee", 100) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "fee", 1,1) };
            var modelInvoice = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", itemsInvoice, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustmentsInvoice, 100000,"aa","aa",units);

            var repoInvoiceMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoInvoiceMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { modelInvoice }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repoInvoiceMock.Object);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Big_Sizes_Success()
        {
            var sizesA = new HashSet<GarmentPackingListDetailSizeModel> { new GarmentPackingListDetailSizeModel(1, "A", 1) };
            var sizesB = new HashSet<GarmentPackingListDetailSizeModel> {};
            for (int i = 2; i < 22; i++)
            {
                sizesB.Add(new GarmentPackingListDetailSizeModel(i, "B", i * 100));
            }
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesA), new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, sizesB) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details, 1, 1) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", items, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var itemsInvoice = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) };
            var adjustmentsInvoice = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "fee", 100) };
            var units = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "fee", 1, 1) };
            var modelInvoice = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", itemsInvoice, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustmentsInvoice, 100000, "aa","aa",units);

            var repoInvoiceMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoInvoiceMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { modelInvoice }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repoInvoiceMock.Object);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
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
        public async Task Set_Post_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 },
                new GarmentPackingListModel { Id = 2 },
                new GarmentPackingListModel { Id = 3 },
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var ids = models.Select(s => s.Id).ToList();

            await service.SetPost(ids);
        }

        [Fact]
        public async Task Set_Unpost_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 }
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var id = models.Select(s => s.Id).First();

            await service.SetUnpost(id);
        }

        [Fact]
        public async Task Set_ApproveMd_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 }
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var id = models.Select(s => s.Id).First();

            await service.SetApproveMd(id, ViewModel);
        }

        [Fact]
        public async Task Set_ApproveShipping_Success()
        {
            List<GarmentPackingListModel> models = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel { Id = 1 }
            };

            var spMock = GetServiceProviderWithIdentity(GetRepositoryMock(models).Object);

            var service = GetService(spMock.Object);

            var id = models.Select(s => s.Id).First();

            await service.SetApproveShipping(id, ViewModel);
        }
    }
}
