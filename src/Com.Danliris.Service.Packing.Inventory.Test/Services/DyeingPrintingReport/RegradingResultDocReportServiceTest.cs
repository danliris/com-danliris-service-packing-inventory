using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.RegradingResultDocReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingReport
{
    public class RegradingResultDocReportServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaOutputProductionOrderRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
               .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected RegradingResultDocReportService GetService(IServiceProvider serviceProvider)
        {
            return new RegradingResultDocReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success_REG()
        {
            var item = new DyeingPrintingAreaOutputProductionOrderModel("INSPECTION MATERIAL", "PACKING", false, 1, "no", "", 1, "", "REG", "", "", "", "", "", "", "", "",
                "", 1, 1, 1, "", 1, "", 1, "", "", "", "", "", 1, "", 1, "", 1, 1, "", false, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, "", "", DateTime.Now)
            { DyeingPrintingAreaOutputId = 1 };
            var items = new List<DyeingPrintingAreaOutputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaOutputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", false, "", "", 1, "", false, "", "", "", "",
                "", "", "", "", "", false, items)
            { Id = 1 };

            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(item.ProductionOrderNo, DateTime.Now.AddDays(-30), DateTime.Now, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_Success_GRADE()
        {
            var item = new DyeingPrintingAreaOutputProductionOrderModel("INSPECTION MATERIAL", "PACKING", false, 1, "no", "", 1, "", "GRADE", "", "", "", "", "", "", "", "",
                "", 1, 1, 1, "", 1, "", 1, "", "", "", "", "", 1, "", 1, "", 1, 1, "", false, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, "", "", DateTime.Now)
            { DyeingPrintingAreaOutputId = 1 };
            var items = new List<DyeingPrintingAreaOutputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaOutputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", false, "", "", 1, "", false, "", "", "", "",
                "", "", "", "", "", false, "", items)
            { Id = 1, CreatedUtc = DateTime.Now };

            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(item.ProductionOrderNo, DateTime.Now, DateTime.Now, 7);

            Assert.NotEmpty(result.ToList());
        }


        [Fact]
        public void GenerateExcel_Success()
        {
            var item = new DyeingPrintingAreaOutputProductionOrderModel("INSPECTION MATERIAL", "PACKING", false, 1, "no", "", 1, "", "GRADE", "", "", "", "", "", "", "", "",
                "", 1, 1, 1, "", 1, "", 1, "", "", "", "", "", 1, "", 1, "", 1, 1, "", false, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, "", "", DateTime.Now)
            { DyeingPrintingAreaOutputId = 1 };
            var items = new List<DyeingPrintingAreaOutputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaOutputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", false, "", "", 1, "", false, "", "", "", "",
                "", "", "", "", "", false, "", items)
            { Id = 1, CreatedUtc = DateTime.Now };

            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);


            var result = service.GenerateExcel(null, DateTime.Now, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>().AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel("b", DateTime.Now, DateTime.Now, 7);

            Assert.NotNull(result);
        }
    }
}
