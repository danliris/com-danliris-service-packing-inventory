using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentPackingListMonitoringServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repository);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentPackingListMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentPackingListMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(model.Id, model.InvoiceType, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);

            Assert.NotEmpty(result.Data);
            Assert.Empty(result.Data.Where(d => d.id == 0));
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(model.BuyerAgentId, model.BuyerAgentCode, model.InvoiceType, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);

            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(0, null, null, null, null);

            Assert.NotNull(result.Data);
        }
    }
}
