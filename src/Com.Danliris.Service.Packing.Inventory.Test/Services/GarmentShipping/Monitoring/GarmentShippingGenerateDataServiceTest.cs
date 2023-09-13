using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingGenerateData;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentShippingGenerateDataServiceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository plrepository, IGarmentShippingInvoiceRepository repository, IGarmentShippingInvoiceItemRepository itemrepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(itemrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShippingGenerateDataService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingGenerateDataService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var items = new List<GarmentShippingInvoiceItemModel>
                {
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C1A", 1, 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C2A", 1, 2)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C2B", 1, 3)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                };


            var units = new List<GarmentShippingInvoiceUnitModel>
            {
                 new GarmentShippingInvoiceUnitModel(1, "", 1,1)
                 {
                      GarmentShippingInvoiceId = 1
                 },
                 new GarmentShippingInvoiceUnitModel(2, "", 1, 1)
                   {
                      GarmentShippingInvoiceId = 1
                 },
            };

            var model2 = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", items, 1000, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "", "", units, 1, 1)
            {
                Id = 1
            };

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var items = new List<GarmentShippingInvoiceItemModel>
                {
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C1A", 1, 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C2A", 1, 2)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                    new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C2B", 1, 3)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                };

            var units = new List<GarmentShippingInvoiceUnitModel>
            {
                 new GarmentShippingInvoiceUnitModel(1, "", 1,1)
                 {
                      GarmentShippingInvoiceId = 1
                 },
                 new GarmentShippingInvoiceUnitModel(2, "", 1, 1)
                   {
                      GarmentShippingInvoiceId = 1
                 },
            };

            var model2 = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", items, 1000, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "", "", units, 1, 1)
            {
                Id = 1
            };

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }
    }
}
