using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.ProductionSubmissionReport;
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
    public class ProductionSubmissionReportServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaInputProductionOrderRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
               .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected ProductionSubmissionReportService GetService(IServiceProvider serviceProvider)
        {
            return new ProductionSubmissionReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success_LJS()
        {
            var item = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL",1,"","","","","","","","","","",0,true,1) { DyeingPrintingAreaInputId=1 };
            var items = new List<DyeingPrintingAreaInputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaInputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "","", "", "", true, 1, 1, items) { Id = 1 };


            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.BonNo,null,DateTime.Now,DateTime.Now, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_Success_LBJ()
        {
            var item = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "", "", "", "", "", "", "", "", "", "", 0, true, 1) { DyeingPrintingAreaInputId = 1 };
            var items = new List<DyeingPrintingAreaInputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaInputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", "", "", true, 1, 1, items) { Id = 1 };


            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.BonNo, null, DateTime.Now, DateTime.Now, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_Success_LBM()
        {
            var item = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "", "", "", "", "", "", "", "", "", "", 0, true, 1) { DyeingPrintingAreaInputId = 1 };
            var items = new List<DyeingPrintingAreaInputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaInputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", "", "", true, 1, 1, items) { Id = 1 };


            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.BonNo, null, DateTime.Now, DateTime.Now, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var item = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "", "", "", "", "", "", "", "", "", "", 0, true, 1) { DyeingPrintingAreaInputId = 1 };
            var items = new List<DyeingPrintingAreaInputProductionOrderModel>() { item };
            var model = new DyeingPrintingAreaInputModel(DateTimeOffset.Now, "INSPECTION MATERIAL", "", "", "", "", true, 1, 1, items) { Id = 1 };


            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);


            var result = service.GenerateExcel(model.BonNo, null, DateTime.Now, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>().AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel("a", "b", DateTime.Now, DateTime.Now,7);

            Assert.NotNull(result);
        }
    }
}
