using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentShippingNoteCreditAdviceMonitoringServiceTest
    {        
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingNoteCreditAdviceRepository carepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteCreditAdviceRepository)))
                .Returns(carepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShippingNoteCreditAdviceMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingNoteCreditAdviceMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_DN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA DEBIT", "DN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(model.BuyerName, null, null, null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_CN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA KREDIT", "CN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(model.BuyerName, null, null, null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_DN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA DEBIT", "DN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(model.BuyerName, null, null, null, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_CN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA KREDIT", "CN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(model.BuyerName, null, null, null, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
