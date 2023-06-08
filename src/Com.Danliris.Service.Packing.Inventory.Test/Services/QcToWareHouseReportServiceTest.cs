using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class QcToWarehouseReportServiceTest
    {
        private QcToWarehouseReportViewModel ViewModel1
        {
            get
            {
                return new QcToWarehouseReportViewModel
                {
                    orderType = "a",
                    inputQuantitySolid = 0,
                    inputQuantityDyeing = 0,
                    inputQuantityPrinting = 0,
                    createdUtc = DateTime.UtcNow,
                };
            }
        }

        


        public QcToWarehouseReportService GetService(IServiceProvider serviceProvider)
        {
            return new QcToWarehouseReportService(serviceProvider);
        }
        public Mock<IServiceProvider> GetServiceProvider(IQcToWarehouseReportService repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IQcToWarehouseReportService)))
                .Returns(repository);

            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaOutputRepository outputRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaOutputRepository outputRepo, IDyeingPrintingAreaOutputProductionOrderRepository outputSpp)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSpp);
            return spMock;
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSpp = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            outRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now, "a", 1, "a", "a")

                }.AsQueryable());
            outputSpp.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);

            var result = service.GetReportData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.InventoryType);
            //var result1 = service.GetReportData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, "BARU");

            Assert.NotEmpty(result);
           // Assert.NotEmpty(result1);
        }

        //stock Gudang Baru
        [Fact]
        public void Should_Success_Read_New()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSpp = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            outRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now, "a", 1, "a", "a")

                }.AsQueryable());
            outputSpp.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            var ModelAwalOut1 =ModelAwalOut;
            ModelAwalOut1.InventoryType = null;

            var ModelAwalIn1 = ModelAwalIn;
            ModelAwalIn1.InventoryType = null;

            var ModelIn1 = ModelIn;
            ModelIn1.InventoryType = null;

            var ModelOut1 = ModelOut;
            ModelOut1.InventoryType = null;

            var ModelAdjIn1 = ModelAdjIn;
            ModelAdjIn1.InventoryType = null;

            var ModelAdjOut1 = ModelAdjOut;
            ModelAdjOut1.InventoryType = null;

            var ModelAwalAdjIn1 = ModelAwalAdjIn;
            ModelAwalAdjIn1.InventoryType = null;

            var ModelAwalAdjOut1 = ModelAwalAdjOut;
            ModelAwalAdjOut1.InventoryType = null;

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut1, ModelAwalIn1, ModelIn1, ModelOut1, ModelAdjIn1, ModelAdjOut1, ModelAwalAdjIn1, ModelAwalAdjOut1 };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);

            //var result = service.GetReportData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.InventoryType);
            var result = service.GetReportData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, "BARU");

            Assert.NotEmpty(result);
        }
        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputSpp = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var outRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var serviceMock = new Mock<StockWarehouseService>();

            var x = Model;
            x.Id = 1;
            foreach (var y in x.DyeingPrintingAreaInputProductionOrders)
            {
                y.Id = 1;
                y.DyeingPrintingAreaInputId = 1;
            }

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { x }.AsQueryable());
            var a = OutputModel;
            a.Id = 1;
            foreach (var t in a.DyeingPrintingAreaOutputProductionOrders)
            {
                t.Id = 1;
                t.DyeingPrintingAreaOutputId = 1;
            }
            //var test = OutputModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault();
            //test.DyeingPrintingAreaOutputId = 1;
            //OutputModel.DyeingPrintingAreaOutputProductionOrders = test;
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { a }.AsQueryable());

            outRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { a }.AsQueryable());

            var testmodel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", int.Parse("1"), 1, 1, "a", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now, "a", 1, "a", "a");
            
            testmodel.Id = 1;
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(
                    //new List<DyeingPrintingAreaInputProductionOrderModel> {
                    x.DyeingPrintingAreaInputProductionOrders.ToList().AsQueryable()
            //}.AsQueryable()
            );

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());


            outputSpp.Setup(s => s.ReadAll())
                .Returns(a.DyeingPrintingAreaOutputProductionOrders.AsQueryable());
            //serviceMock.Setup(s => s.GetReportData(new DateTimeOffset(DateTime.Now), "PACKING"))
            //    .Returns(new List<ReportStockWarehouseViewModel>(new List<ReportStockWarehouseViewModel>() { ViewModel1 }));
            //var service = GetService(GetServiceProvider(inputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);
            var service = GetService(GetServiceProvider(inputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);
            //var service = new StockWarehouseService(serviceMock.Object);

            var result = service.GenerateExcel(ModelIn.Date.AddDays(3), "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.InventoryType);


            Assert.NotNull(result);
        }


        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputSpp = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var outRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var serviceMock = new Mock<StockWarehouseService>();

            var x = Model;
            x.Id = 1;
            foreach (var y in x.DyeingPrintingAreaInputProductionOrders)
            {
                y.Id = 1;
                y.DyeingPrintingAreaInputId = 1;
            }

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { x }.AsQueryable());
            var a = OutputModel;
            a.Id = 1;
            foreach (var t in a.DyeingPrintingAreaOutputProductionOrders)
            {
                t.Id = 1;
                t.DyeingPrintingAreaOutputId = 1;
            }
            //var test = OutputModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault();
            //test.DyeingPrintingAreaOutputId = 1;
            //OutputModel.DyeingPrintingAreaOutputProductionOrders = test;
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { a }.AsQueryable());

            outRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { a }.AsQueryable());

            var testmodel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", int.Parse("1"), 1, 1, "a", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now, "a", 1, "a", "a");
            
            testmodel.Id = 1;
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(
                    //new List<DyeingPrintingAreaInputProductionOrderModel> {
                    x.DyeingPrintingAreaInputProductionOrders.ToList().AsQueryable()
            //}.AsQueryable()
            );

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());


            outputSpp.Setup(s => s.ReadAll())
                .Returns(a.DyeingPrintingAreaOutputProductionOrders.AsQueryable());
            //serviceMock.Setup(s => s.GetReportData(new DateTimeOffset(DateTime.Now), "PACKING"))
            //    .Returns(new List<ReportStockWarehouseViewModel>(new List<ReportStockWarehouseViewModel>() { ViewModel1 }));
            //var service = GetService(GetServiceProvider(inputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);
            var service = GetService(GetServiceProvider(inputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);
            //var service = new StockWarehouseService(serviceMock.Object);

            var result = service.GenerateExcel(ModelIn.Date, "TRANSIT", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.InventoryType);


            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetPackingData()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSpp = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            outRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now, "a", 1, "a", "a")

                }.AsQueryable());
            outputSpp.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);

            var result = service.GetPackingData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.Grade);

            //will be uncomment latter
           // Assert.NotEmpty(result);
        }

        [Fact]
        public void ValidateVM()
        {
            var simpleVM = new SimpleReportViewModel();
            Assert.Equal(0, simpleVM.ProductionOrderId);

            var reportVM = new ReportStockWarehouseViewModel();
            Assert.Null(reportVM.Construction);
            Assert.Null(reportVM.Unit);
            Assert.Null(reportVM.Motif);
            Assert.Null(reportVM.Color);
            Assert.Null(reportVM.Jenis);
            Assert.Null(reportVM.Ket);
            Assert.Null(reportVM.Satuan);
        }
    }
}
