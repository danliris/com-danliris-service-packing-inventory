using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentCoverLetterMonitoringServiceTest
    {
       public Mock<IServiceProvider> GetServiceProvider(IGarmentCoverLetterRepository clrepository)

        {
            var spMock = new Mock<IServiceProvider>();
           
            spMock.Setup(s => s.GetService(typeof(IGarmentCoverLetterRepository)))
               .Returns(clrepository);
 
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentCoverLetterMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentCoverLetterMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {            
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "EMKL", " EMKL Test", "", "", "", "", "", DateTimeOffset.Now, 1, "X01", "Order Test", 1, 1, 1, 1, 1, "FWDR", "FWDR Test", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "")
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);
         
            var result = service.GetReportData(model.OrderCode, model.EMKLCode, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "EMKL", " EMKL Test", "", "", "", "", "", DateTimeOffset.Now, 1, "X01", "Order Test", 1, 1, 1, 1, 1, "FWDR", "FWDR Test", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "")
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(model.OrderCode, model.EMKLCode, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
