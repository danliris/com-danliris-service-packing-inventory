using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse;
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
    public class StockWarehouseServiceTest
    {
        private ReportStockWarehouseViewModel ViewModel1
        {
            get
            {
                return new ReportStockWarehouseViewModel
                {
                    Akhir = 1,
                    Awal = 3,
                    Color = "a",
                    Construction = "a",
                    Grade = "a",
                    Jenis = "a",
                    Keluar = 2,
                    Ket = "a",
                    Masuk = 4,
                    Motif = "a",
                    NoSpp = "a",
                    Satuan = "a",
                    Unit = "a",
                };
            }
        }

        private InputPackagingViewModel ViewModel
        {
            get
            {
                return new InputPackagingViewModel()
                {
                    Id = 1,
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    Group = "A",
                    PackagingProductionOrders = new List<InputPackagingProductionOrdersViewModel>
                    {
                        new InputPackagingProductionOrdersViewModel()
                        {
                            Id = 1,
                            Area="PACKING",
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            IsChecked = false,
                            InputQtyPacking = 1,
                            InputQuantity = 1,
                            Motif = "sd",
                            PackingInstruction = "d",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "d"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            Unit = "s",
                            UomUnit = "d",
                            QtyOrder = 123,
                            ProductionOrderNo ="sd"
                        }
                    },
                };
            }
        }

        private DyeingPrintingAreaInputModel Model
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark,s.ProductionMachine, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id,
                    s.MaterialConstruction.Name, s.MaterialWidth, 0, "", s.PackingType, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                    s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking)).ToList());
            }
        }
        private DyeingPrintingAreaOutputModel OutputModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, false, "PACKING", ViewModel.Group, "OUT", ViewModel.PackagingProductionOrders.Select(s =>
                      new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, "PACKING", false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                      s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark,s.ProductionMachine, s.Grade, s.Status, s.Balance, 1, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, "", 0, "", s.PackingType, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false, s.PackingLength)).ToList());
            }
        }

        private InputPackagingViewModel ViewModelIM
        {
            get
            {
                return new InputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Id = 1,
                    Group = "A",
                    PackagingProductionOrders = new List<InputPackagingProductionOrdersViewModel>()
                    {
                        new InputPackagingProductionOrdersViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Area = "INSPECTION MATERIAL",
                            Grade = "s",
                            HasOutputDocument = false,
                            IsChecked = false,
                            Motif = "sd",
                            PackingInstruction = "d",
                            Remark = "RE",
                            Status = "s",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name ="a"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            Unit = "s",
                            UomUnit = "d",
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaMovementModel ModelOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "PACKING", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Keluar, 1, "type", "gr", "rem", "type",1,"unit",1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "PACKING", "IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAdjIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "PACKING", "ADJ IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAdjOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "PACKING", "ADJ OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddMonths(-1), "PACKING", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Keluar, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddMonths(-1), "PACKING", "IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalAdjIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddMonths(-1), "PACKING", "ADJ IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalAdjOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddMonths(-1), "PACKING", "ADJ OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", ViewModel1.Masuk, 1, "type", "gr", "rem", "type", 1, "unit", 1);
            }
        }


        public StockWarehouseService GetService(IServiceProvider serviceProvider)
        {
            return new StockWarehouseService(serviceProvider);
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo)
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
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1)
                }.AsQueryable());
            outputSpp.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);

            var result = service.GetReportData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId);

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

            var testmodel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", int.Parse("1"), 1, 1, "a", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1);
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

            var result = service.GenerateExcel(ModelIn.Date.AddDays(3), "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId);


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

            var testmodel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", int.Parse("1"), 1, 1, "a", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1);
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

            var result = service.GenerateExcel(ModelIn.Date, "TRANSIT", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId);


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
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1)
                }.AsQueryable());
            outputSpp.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalIn, ModelIn, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outRepoMock.Object, outputSpp.Object).Object);

            var result = service.GetPackingData(ModelIn.Date, "PACKING", 7, ModelIn.Unit, ModelIn.PackingType, ModelIn.Construction, ModelIn.Buyer, ModelIn.ProductionOrderId, ModelIn.Grade);

            Assert.NotEmpty(result);
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
