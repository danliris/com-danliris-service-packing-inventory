using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class OutputPackagingServiceTest
    {
        public OutputPackagingService GetService(IServiceProvider serviceProvider)
        {
            return new OutputPackagingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaInputRepository inputRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);

            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaInputRepository inputRepo, IDyeingPrintingAreaOutputProductionOrderRepository outputSPP)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPP);

            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaInputRepository inputRepo, IFabricPackingSKUService fabricService)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IFabricPackingSKUService)))
                .Returns(fabricService);

            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);

            return spMock;
        }

        private OutputPackagingViewModel ViewModel
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    BonNoInput = "s",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputPackagingId = 1,
                    Type = "OUT",
                    PackagingProductionOrders = new List<OutputPackagingProductionOrderViewModel>()
                    {
                        new OutputPackagingProductionOrderViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            IsSave = true,
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            ProductSKUId = 1,
                            FabricSKUId = 1,
                            ProductSKUCode = "c",
                            HasPrintingProductSKU = false,
                            ProductPackingId = 1,
                            FabricPackingId = 1,
                            ProductPackingCode = "c",
                            HasPrintingProductPacking = false,
                            NextAreaInputStatus = "DITERIMA"
                            
                        }
                    }
                };
            }
        }
        private OutputPackagingViewModel ViewModelAdj
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Type = "ADJ",
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    BonNoInput = "s",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputPackagingId = 1,
                    PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
                    {
                        new InputPlainAdjPackagingProductionOrder()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            MaterialObj = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            Material = "test",
                            Area="PACKING",
                            HasOutputDocument = false,
                            IsChecked= true,
                            BalanceRemains=1,
                            PreviousBalance=1,
                            OutputId=1,
                            InputId=1,
                            DyeingPrintingAreaInputProductionOrderId=1,
                            DyeingPrintingAreaOutputProductionOrderId=1,
                            AtQty=1,
                        }
                    }
                };
            }
        }
        private OutputPackagingViewModel ViewModelAdjMin
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Type = "ADJ",
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    BonNoInput = "s",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputPackagingId = 1,
                    PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
                    {
                        new InputPlainAdjPackagingProductionOrder()
                        {
                            Balance = -1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            MaterialObj = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialConstruction = new MaterialConstruction()
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
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel Model
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                         s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, "", s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                         s.MaterialWidth, "", s.PackagingQTY, s.PackagingType, s.PackagingUnit, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                         s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut, s.NextAreaInputStatus)).ToList());
                    
            }
        }
        private DyeingPrintingAreaOutputModel ModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, "ADJ IN", ViewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                         s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, "", s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                         s.MaterialWidth, "", s.PackagingQTY, s.PackagingType, s.PackagingUnit, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                         s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut, s.NextAreaInputStatus)).ToList());
                    
            }
        }

        private DyeingPrintingAreaOutputModel EmptyDetailModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                  ViewModel.Group, ViewModel.Type, new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }
        private InputPackagingViewModel InputViewModel
        {
            get
            {
                return new InputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    Group = "A",
                    PackagingProductionOrders = new List<InputPackagingProductionOrdersViewModel>
                    {
                        new InputPackagingProductionOrdersViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            IsChecked = false,
                            Motif = "sd",
                            PackingInstruction = "d",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            QtyOrder = 123,
                            ProductionOrderNo ="sd"
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel InputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, ViewModel.HasNextAreaDocument, s.QtyOrder, s.Grade, s.Balance, s.BuyerId, s.Id, s.Remark, s.MaterialProduct.Id, s.MaterialProduct.Name,
                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Balance, s.FinishWidth, s.DateIn)).ToList());
                    
            }
        }

        private PlainAdjPackagingProductionOrder InputModelPlain
        {
            get
            {
                return new PlainAdjPackagingProductionOrder
                {
                    Id = 1,
                    ProductionOrder = new ProductionOrder()
                    {
                        Code = "sd",
                        Id = 1,
                        Type = "sd",
                        No = "sd"
                    },
                    Material = new Material()
                    {
                        Id = 1,
                        Name = "name"
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = 1,
                        Name = "name"
                    },

                    Construction = "",

                    MaterialWidth = "",
                    Area = "",
                    CartNo = "",
                    PackingInstruction = "",
                    Unit = "",
                    BuyerId = 1,
                    Buyer = "",
                    Color = "",
                    Motif = "",
                    UomUnit = "",
                    Balance = 1,
                    HasOutputDocument = false,
                    IsChecked = true,
                    Grade = "",
                    Remark = "",
                    Status = "",
                    BalanceRemains = 1,
                    PreviousBalance = 1,
                    OutputId = 1,
                    InputId = 1,
                    PackagingType = "",
                    PackagingUnit = "",
                    PackagingQTY = 1,
                    DyeingPrintingAreaInputProductionOrderId = 1,
                    DyeingPrintingAreaOutputProductionOrderId = 1,
                    AtQty = 1
                };
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now)

                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Duplicate()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now)

                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GetListInputSPP()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var inputspp = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", 1, 1, 1, "s", 1, "s", 1, "s", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            inputspp.Id = 1;
            inputspp.DyeingPrintingAreaInputId = 1;
            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    inputspp
                }.AsQueryable());

            var inputbon = new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now), "PACKING", "PAGI", "s", "A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        inputspp
                    });
            inputbon.Id = 1;

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    inputbon
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = service.ReadSppInFromPack(0, 10, string.Empty, string.Empty, "s");

            //Assert.NotEqual(0, result);
            Assert.NotNull(result);
        }
        [Fact]
        public void Should_Success_GetDistinctProductionOrder()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var inputspp = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", 1, 1, 1, "s", 1, "s", 1, "s", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            inputspp.Id = 1;
            inputspp.DyeingPrintingAreaInputId = 1;
            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    inputspp
                }.AsQueryable());

            var inputbon = new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now), "PACKING", "PAGI", "s", "A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        inputspp
                    });
            inputbon.Id = 1;

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    inputbon
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);
            var order = new Dictionary<string, string>() { { "ProductionOrderNo", "desc" } };
            var result = service.GetDistinctProductionOrder(0, 10, string.Empty, JsonConvert.SerializeObject(order), "s");

            //Assert.NotEqual(0, result);
            Assert.NotNull(result);
        }
        [Fact]
        public void Should_Success_GetListInputSPPSummed()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var inputspp = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", 1, 1, 1, "s", 1, "s", 1, "s", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            inputspp.Id = 1;
            inputspp.DyeingPrintingAreaInputId = 1;
            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    inputspp
                }.AsQueryable());

            var inputbon = new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now), "PACKING", "PAGI", "s", "A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        inputspp
                    });
            inputbon.Id = 1;

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    inputbon
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = service.ReadSppInFromPackSumBySPPNo(0, 10, string.Empty, string.Empty, "s");

            //Assert.NotEqual(0, result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_Success_Delete()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, "zimmer", s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, "", s.PackagingQTY, s.PackagingType, s.PackagingUnit, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut)).ToList());
                    
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, s.Balance, s.BuyerId, s.Id, s.Remark, s.MaterialProduct.Id, s.MaterialProduct.Name,
                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Balance, s.FinishWidth, s.DateIn)).ToList());
                    

            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());



            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object, inputRepoMock.Object).Object);


            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteV2()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes2 = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, "zimmer", s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, "", s.PackagingQTY, s.PackagingType, s.PackagingUnit, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut)).ToList());
                    
            var tes3 = new List<DyeingPrintingAreaOutputModel>() { tes2 };
            var json = JsonConvert.SerializeObject(tes3);
            var tes = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, "PC.IM.2020.002", false, ViewModel.DestinationArea, ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                         new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                             s.Motif, s.UomUnit, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.QtyOut, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, 0, s.Id, s.BuyerId, json,
                             s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut)).ToList());
                         
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);


            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tes);

            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, s.Balance, s.BuyerId, s.Id, s.Remark, s.MaterialProduct.Id, s.MaterialProduct.Name,
                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Balance, s.FinishWidth, s.DateIn)).ToList());
                    

            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());



            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object, inputRepoMock.Object).Object);


            var result = await service.DeleteV2(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_DeleteV22()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes2 = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, "zimmer", s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, "", s.PackagingQTY, s.PackagingType, s.PackagingUnit, 0, "", "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut)).ToList());
                   
            var tes3 = new List<DyeingPrintingAreaOutputModel>() { tes2 };
            var json = JsonConvert.SerializeObject(tes3);
            var tes = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, "PC.IM.2020.002", false, ViewModel.DestinationArea, ViewModel.Group, ViewModel.Type, ViewModel.PackagingProductionOrders.Select(s =>
                         new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                             s.Motif, s.UomUnit, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.QtyOut, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, 0, s.Id, s.BuyerId, json,
                             s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut)).ToList());
                         
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tes);

            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, s.Balance, s.BuyerId, s.Id, s.Remark, s.MaterialProduct.Id, s.MaterialProduct.Name,
                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Balance, s.FinishWidth, s.DateIn)).ToList());
                    

            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            var testing = testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable();
            foreach (var i in testing)
            {
                i.Id = 0;
            }
            //inputProductionOrderRepoMock.Setup(s => s.ReadAll())
            //    .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testing);


            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object, inputRepoMock.Object).Object);


            var result = await service.DeleteV2(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_Create_Packing()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateV2_Packing()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateV2_Packing_Duplicate()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateV2_IM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "INSPECTION MATERIAL";
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateV2_Packing2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            foreach (var t in vm.PackagingProductionOrders)
            {
                t.QtyOut = 5;
            }
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_CreateV2_Packing3()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingCodes = new List<string>() { "c" },
                    ProductPackingId = 1
                });

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            foreach (var t in vm.PackagingProductionOrders)
            {
                t.QtyOut = 10;
            }
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_CreateV2_Packing4()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            var testModel = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, false, 10, "A", Convert.ToDouble(10), 1, 1, "r", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a", DateTimeOffset.Now);
            
            //testModel.SetBalanceRemains(10, "unittest", "unittest");

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    testModel
                }.AsQueryable());

            sppRepoMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.PackingData>>(1, new List<Inventory.Infrastructure.Utilities.PackingData>()));

            outputSppRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            foreach (var t in vm.PackagingProductionOrders)
            {
                t.QtyOut = 10;
            }
            var result = await service.CreateV2(vm);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_Create_Aval()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1,1,1,"s",1,"s",1,"s","1",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now)

                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "GUDANG JADI";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public void Should_Success_ReadBonOutFromPack()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputBonRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            inputBonRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputBonRepoMock.Object).Object);

            var result = service.ReadBonOutFromPack(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_ReadBonOutFromPackGroup()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputBonRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            inputBonRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(InputModel.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputBonRepoMock.Object).Object);

            var result = service.ReadSppInFromPackGroup(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "remark", "zimmer", "grade", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 1, "a", "a", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1, "a", DateTimeOffset.Now));
                


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_Success_ReadByIdAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "remark", "zimmer", "grade", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 1, "a", "a", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1, "a", DateTimeOffset.Now));
                


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.GenerateExcel(1,1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.GenerateExcel(1,7);

            Assert.NotNull(result);
        }
        [Fact]
        public void Should_Success_GenerateExcelAll()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll2()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(Model.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll3()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll4()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateBon()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);


            var resultIM = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "INSPECTION MATERIAL");
            var resultAV = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "GUDANG AVAL");
            var resultSH = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "SHIPPING");

            Assert.NotNull(resultIM);
            Assert.NotNull(resultAV);
            Assert.NotNull(resultSH);
        }
        [Fact]
        public void Should_Success_GenerateBonAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);


            var result = service.GenerateBonNoAdj(1, DateTimeOffset.UtcNow, "PACKING", new List<double> { 1, 2 });
            var result2 = service.GenerateBonNoAdj(1, DateTimeOffset.UtcNow, "PACKING", new List<double> { -11, -2 });


            Assert.NotNull(result);
            Assert.NotNull(result2);
        }

        [Fact]
        public async Task Should_Success_CreateAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);


            var result = await service.CreateAdj(ViewModelAdj);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj_Duplicate()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;
            model.SetType("ADJ IN", "", "");

            outputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object, inputRepoMock.Object, outputProductionOrderRepoMock.Object).Object);


            var result = await service.CreateAdj(ViewModelAdj);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdjOut()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var vm = ViewModelAdj;

            foreach (var item in vm.PackagingProductionOrdersAdj)
            {
                item.Balance = -1;
            }

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object);


            var result = await service.CreateAdj(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.PackagingProductionOrders.FirstOrDefault().Balance = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdatePackingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1,
                    ProductPackingCodes = new List<string>() { "c" }
                });

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateAdjIN()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            foreach (var item in vm.PackagingProductionOrdersAdj)
            {
                item.Balance = 1;
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
                .Returns(new FabricPackingIdCodeDto()
                {
                    FabricPackingId = 1,
                    ProductPackingCode = "c",
                    ProductPackingId = 1
                });

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateAdjOut()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            vm.PackagingProductionOrdersAdj.FirstOrDefault().Balance = -1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
               .Returns(new FabricPackingIdCodeDto()
               {
                   FabricPackingId = 1,
                   ProductPackingCode = "c",
                   ProductPackingId = 1
               });

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(false, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.PackagingProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.PackagingProductionOrders = new List<OutputPackagingProductionOrderViewModel>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
               .Returns(new FabricPackingIdCodeDto()
               {
                   FabricPackingId = 1,
                   ProductPackingCode = "c",
                   ProductPackingId = 1,
                   ProductPackingCodes = new List<string>() { "c" }
               });

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Delete_Adj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(true, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.PackagingProductionOrdersAdj.FirstOrDefault();
            detail.Id = 0;
            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreatePacking(It.IsAny<FabricPackingAutoCreateFormDto>()))
               .Returns(new FabricPackingIdCodeDto()
               {
                   FabricPackingId = 1,
                   ProductPackingCode = "c",
                   ProductPackingId = 1
               });

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object, fabricService.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_ReadSPPInPackingGroupBySPPGrade()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputBonRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            inputBonRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(InputModel.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputBonRepoMock.Object).Object);

            var result = service.ReadSPPInPackingGroupBySPPGrade(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_Get_Set()
        {
            var test = InputModelPlain;
            var testval9 = test.Material;
            var testval = test.Construction;
            testval = test.MaterialWidth;
            testval = test.Area;
            testval = test.CartNo;
            testval = test.PackingInstruction;
            testval = test.Unit;
            var testval1 = test.BuyerId;
            testval = test.Buyer;
            testval = test.Color;
            testval = test.Motif;
            testval = test.UomUnit;
            var testval2 = test.Balance;
            var testval3 = test.HasOutputDocument;
            testval3 = test.IsChecked;
            testval = test.Grade;
            testval = test.Remark;
            testval = test.Status;
            testval2 = test.BalanceRemains;
            testval2 = test.PreviousBalance;
            testval1 = test.OutputId;
            testval1 = test.InputId;
            testval = test.PackagingType;
            testval = test.PackagingUnit;
            testval2 = Convert.ToDouble(test.PackagingQTY);
            testval1 = test.DyeingPrintingAreaInputProductionOrderId;
            testval1 = test.DyeingPrintingAreaOutputProductionOrderId;
            testval2 = test.AtQty;


            var tes = ViewModelAdj.PackagingProductionOrdersAdj.FirstOrDefault();
            var z = tes.Id;
            var x = tes.ProductionOrder;
            var y = tes.MaterialObj;
            var f = tes.MaterialConstruction;
            var a = tes.Material;
            a = tes.Construction;
            a = tes.NoDocument;
            a = tes.MaterialWidth;
            a = tes.Area;
            a = tes.CartNo;
            a = tes.PackingInstruction;
            a = tes.Unit;
            var b = tes.BuyerId;
            a = tes.Buyer;
            a = tes.Color;
            a = tes.Motif;
            a = tes.UomUnit;
            var c = tes.Balance;
            var d = tes.HasOutputDocument;
            d = tes.IsChecked;
            a = tes.Grade;
            a = tes.Remark;
            a = tes.Status;
            c = tes.BalanceRemains;
            c = tes.PreviousBalance;
            b = tes.OutputId;
            b = tes.InputId;
            a = tes.PackagingType;
            a = tes.PackagingUnit;
            c = Convert.ToDouble(tes.PackagingQty);
            b = tes.DyeingPrintingAreaInputProductionOrderId;
            b = tes.DyeingPrintingAreaOutputProductionOrderId;
            c = tes.AtQty;
        }

        [Fact]
        public void ValidateVM()
        {
            var spp = new OutputPackagingProductionOrderViewModel();
            Assert.Equal(0, spp.PreviousBalance);
            Assert.Equal(0, spp.PackingLength);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            var serviceProvider = GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new OutputPackagingViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.PackagingProductionOrders = new List<OutputPackagingProductionOrderViewModel>()
            {
                new OutputPackagingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = 1,
                        No = "a"
                    },
                    IsSave = true,
                    Balance = 0
                },
                new OutputPackagingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = 1,
                        No = "a"
                    },
                    IsSave = true,
                    Balance = 0,
                    QtyOut = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = null;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 0
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
                new InputPlainAdjPackagingProductionOrder()
                {
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ IN";
            vm.Id = 1;
            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ OUT";
            vm.Id = 1;
            vm.PackagingProductionOrdersAdj = new List<InputPlainAdjPackagingProductionOrder>()
            {
                new InputPlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }
    }
}
