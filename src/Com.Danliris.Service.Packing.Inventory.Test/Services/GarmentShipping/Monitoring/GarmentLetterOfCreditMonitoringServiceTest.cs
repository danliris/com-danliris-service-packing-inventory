using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentLetterOfCreditMonitoringServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentLetterOfCreditRepository lcrepository, IGarmentShippingInvoiceRepository invrepository, IGarmentPackingListRepository plrepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentLetterOfCreditRepository)))
                    .Returns(lcrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(invrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentLetterOfCreditMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLetterOfCreditMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingLetterOfCreditModel("001", DateTimeOffset.Now, "", 1, "", "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", 1, 1, "", 2)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "LC", "001", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLetterOfCreditModel>() { model }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock2.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.ApplicantCode, null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingLetterOfCreditModel("001", DateTimeOffset.Now, "", 1, "", "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", 1, 1, "", 2)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "LC", "001", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", null, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLetterOfCreditModel>() { model }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model2 }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock2.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(model.ApplicantCode, null, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLetterOfCreditModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock2.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
