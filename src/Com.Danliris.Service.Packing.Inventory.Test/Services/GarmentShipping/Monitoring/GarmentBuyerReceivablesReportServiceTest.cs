using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
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
    public class GarmentBuyerReceivablesReportServiceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository repository, IGarmentShippingInvoiceItemRepository itemrepository, IGarmentPackingListRepository plrepository, IGarmentShippingCreditAdviceRepository carepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(itemrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
               .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCreditAdviceRepository)))
                .Returns(carepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentBuyerReceivablesReportService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentBuyerReceivablesReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var items = new List<GarmentShippingInvoiceItemModel>
                {
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 12, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 21, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 2, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                            {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 31, 2, "", "", "", "",  "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 0, 1)
                              {
                           GarmentShippingInvoiceId = 1
                         },
                };

            var model = new GarmentShippingInvoiceModel(1, "DL/229999", DateTimeOffset.Now, "", "", 1, "AGENT1", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", items, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("DL/229999", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingCreditAdviceModel(1, 1, "DL/229999", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GetReportData(model.BuyerAgentCode, model.InvoiceNo, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {

            var items = new List<GarmentShippingInvoiceItemModel>
                {
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 12, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                         {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 21, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 2, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                            {
                           GarmentShippingInvoiceId = 1
                         },
                     new GarmentShippingInvoiceItemModel("2120001", "", 1, "BRAND1", 31, 2, "", "", "", "",  "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 0, 1)
                              {
                           GarmentShippingInvoiceId = 1
                         },
                };

            var model = new GarmentShippingInvoiceModel(1, "DL/229999", DateTimeOffset.Now, "", "", 1, "AGENT1", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", items, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("DL/229999", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingCreditAdviceModel(1, 1, "DL/229999", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(model.BuyerAgentCode, model.InvoiceNo, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
