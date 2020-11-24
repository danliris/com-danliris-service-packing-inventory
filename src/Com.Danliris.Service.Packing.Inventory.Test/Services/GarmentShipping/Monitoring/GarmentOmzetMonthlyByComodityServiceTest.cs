using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByComodity;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentOmetMonthlyByComodityServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository repository, IGarmentShippingInvoiceItemRepository itemRepository, IGarmentPackingListRepository plRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(itemRepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
           .Returns(plRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentOmzetMonthlyByComodityService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentOmzetMonthlyByComodityService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                 "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };
            var model1 = new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "MS", "TES COMODITY 1", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
            {
                GarmentShippingInvoiceId = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "Australia", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "")
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.Now, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var items = new List<GarmentShippingInvoiceItemModel>
                {
                     new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "MS", "TES COMODITY 1", "", "comodesc", "comodesc", "comodesc", 1, "uom1", 1, 1, 1, "", 1, "C10", 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "LB", "TES COMODITY 2", "", "", "", "", 2, "Uom2", 1, 1, 1, "", 1, "C10", 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "GB", "TES COMODITY 3", "", "", "", "", 2, "Uom2", 1, 1, 1, "", 1, "B10", 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                };
            var invoiceUnitModels = new HashSet<GarmentShippingInvoiceUnitModel> {
                new GarmentShippingInvoiceUnitModel(1,"unitcode", 3,1)
                {
                    Id = 1
                }
            };
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                "", DateTimeOffset.Now, "", items, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", invoiceUnitModels)
            {
                Id = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "Australia", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "")
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(null, null, 0);

            Assert.NotNull(result);
        }
    }
}
