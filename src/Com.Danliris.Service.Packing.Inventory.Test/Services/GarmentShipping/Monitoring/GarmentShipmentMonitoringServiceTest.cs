using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
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
    public class GarmentShipmentMonitoringServiceTest
    {        
        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository plrepository, IGarmentShippingInvoiceRepository repository, IGarmentShippingInvoiceItemRepository itemrepository, IGarmentShippingInvoiceAdjustmentRepository adjrepository, IGarmentCoverLetterRepository clrepository, IGarmentShippingCreditAdviceRepository carepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(itemrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceAdjustmentRepository)))
                .Returns(adjrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentCoverLetterRepository)))
                .Returns(clrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCreditAdviceRepository)))
                .Returns(carepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShipmentMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShipmentMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED)
            {
                Id = 1
            };

            var model2 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1,"","",null)
            {
                Id = 1
            };

            var iteminv = new List<GarmentShippingInvoiceItemModel>
                        {
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                             },                 
                         };

            var itemadj = new List<GarmentShippingInvoiceAdjustmentModel>
                        {
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 100)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 25)
                             {
                               GarmentShippingInvoiceId = 1
                             },
                         };

            var model3 = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            var model4 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(iteminv.AsQueryable());

            var repoMock4 = new Mock<IGarmentShippingInvoiceAdjustmentRepository>();
            repoMock4.Setup(s => s.ReadAll())
                .Returns(itemadj.AsQueryable());

            var repoMock5 = new Mock<IGarmentCoverLetterRepository>();
            repoMock5.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model3 }.AsQueryable());

            var repoMock6 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock6.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model4 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object, repoMock4.Object, repoMock5.Object, repoMock6.Object).Object);

            var result = service.GetReportData(model2.BuyerAgentCode, null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED)
            {
                Id = 1
            };

            var model2 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var iteminv = new List<GarmentShippingInvoiceItemModel>
                        {
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                             },
                         };

            var itemadj = new List<GarmentShippingInvoiceAdjustmentModel>
                        {
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 100)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 25)
                             {
                               GarmentShippingInvoiceId = 1
                             },
                         };

            var model3 = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            var model4 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(iteminv.AsQueryable());

            var repoMock4 = new Mock<IGarmentShippingInvoiceAdjustmentRepository>();
            repoMock4.Setup(s => s.ReadAll())
                .Returns(itemadj.AsQueryable());

            var repoMock5 = new Mock<IGarmentCoverLetterRepository>();
            repoMock5.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model3 }.AsQueryable());

            var repoMock6 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock6.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model4 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object, repoMock4.Object, repoMock5.Object, repoMock6.Object).Object);


            var result = service.GenerateExcel(model2.BuyerAgentCode, null, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED)
            {
                Id = 1
            };

            var model2 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var iteminv = new List<GarmentShippingInvoiceItemModel>
                        {
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "", "", 1, "", 1, 1, 1, "", 1, "C10", 1)
                             {
                               GarmentShippingInvoiceId = 1
                             },
                         };

            var itemadj = new List<GarmentShippingInvoiceAdjustmentModel>
                        {
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 100)
                             {
                               GarmentShippingInvoiceId = 1
                              },
                         new GarmentShippingInvoiceAdjustmentModel(1, "", 25)
                             {
                               GarmentShippingInvoiceId = 1
                             },
                         };

            var model3 = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            var model4 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>().AsQueryable());

            var repoMock4 = new Mock<IGarmentShippingInvoiceAdjustmentRepository>();
            repoMock4.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceAdjustmentModel>().AsQueryable());

            var repoMock5 = new Mock<IGarmentCoverLetterRepository>();
            repoMock5.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>().AsQueryable());

            var repoMock6 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock6.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object, repoMock3.Object, repoMock4.Object, repoMock5.Object, repoMock6.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
