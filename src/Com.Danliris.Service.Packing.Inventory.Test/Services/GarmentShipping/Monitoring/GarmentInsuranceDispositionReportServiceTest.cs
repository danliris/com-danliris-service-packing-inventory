using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentInsuranceDispositionReportServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInsuranceDispositionRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInsuranceDispositionRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentInsuranceDispositionReportService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentInsuranceDispositionReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {        
            var items = new List<GarmentShippingInsuranceDispositionItemModel>
                {
                     new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "001", "DL/210001", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1)
                         {
                           InsuranceDispositionId = 1
                         },
                     new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "002", "DL/210002", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2)
                         {
                           InsuranceDispositionId = 1
                         },                    
                };

            var model = new GarmentShippingInsuranceDispositionModel("001", "Kargo", DateTimeOffset.Now, "", 1, "", "", 1, "", items)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())               
                .Returns(new List<GarmentShippingInsuranceDispositionModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadItemAll())
              .Returns(items.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(null, DateTime.MinValue, DateTime.Now, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var items = new List<GarmentShippingInsuranceDispositionItemModel>
                {
                     new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "001", "DL/210001", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1)
                         {
                           InsuranceDispositionId = 1
                         },
                     new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "002", "DL/210002", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2)
                         {
                           InsuranceDispositionId = 1
                         },
                };

            var model = new GarmentShippingInsuranceDispositionModel("001", "Kargo", DateTimeOffset.Now, "", 1, "", "", 1, "", items)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadItemAll())
              .Returns(items.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
           
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>().AsQueryable());

            repoMock.Setup(s => s.ReadItemAll())
             .Returns(new List<GarmentShippingInsuranceDispositionItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }
    }
}
