using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentCreditAdviceMonitoringServiceTest
    {        
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingCreditAdviceRepository carepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCreditAdviceRepository)))
                .Returns(carepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentCreditAdviceMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentCreditAdviceMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(model.BuyerName, null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", 1, "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(model.BuyerName, null, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
