using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class OutputTransitServiceTest
    {
        public OutputTransitService GetService(IServiceProvider serviceProvider)
        {
            return new OutputTransitService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaOutputProductionOrderRepository outSppRepo)
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
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outSppRepo);

            return spMock;
        }

        private OutputTransitViewModel ViewModel
        {
            get
            {
                return new OutputTransitViewModel()
                {
                    Type = "OUT",
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "GUDANG JADI",
                    Group = "A",
                    InputTransitId = 1,
                    TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
                    {
                        new OutputTransitProductionOrderViewModel()
                        {
                            DeliveryOrder = new Application.ToBeRefactored.CommonViewModelObjectProperties.DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Id = 1,
                            IsSave = true,
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
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
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        private OutputTransitViewModel ViewModelAdj
        {
            get
            {
                return new OutputTransitViewModel()
                {
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Type = "ADJ",
                    HasNextAreaDocument = false,
                    DestinationArea = "GUDANG JADI",
                    Group = "A",
                    InputTransitId = 1,
                    TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
                    {
                        new OutputTransitProductionOrderViewModel()
                        {
                            DeliveryOrder = new Application.ToBeRefactored.CommonViewModelObjectProperties.DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            AdjDocumentNo = "no",
                            Balance = -1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Id = 1,
                            IsSave = true,
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
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

        private OutputTransitViewModel ViewModelAval
        {
            get
            {
                return new OutputTransitViewModel()
                {
                    Type = "OUT",
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "GUDANG AVAL",
                    InputTransitId = 1,
                    TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
                    {
                        new OutputTransitProductionOrderViewModel()
                        {
                            DeliveryOrder = new Application.ToBeRefactored.CommonViewModelObjectProperties.DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            IsSave = true,
                            Remark = "remar",
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
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

        private OutputTransitViewModel ViewModelPC
        {
            get
            {
                return new OutputTransitViewModel()
                {
                    Type = "OUT",
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "PACKING",
                    InputTransitId = 1,
                    TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
                    {
                        new OutputTransitProductionOrderViewModel()
                        {
                            DeliveryOrder = new Application.ToBeRefactored.CommonViewModelObjectProperties.DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
                            IsSave = true,
                            Motif = "sd",
                            PackingInstruction = "d",
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
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        private OutputTransitViewModel ViewModelIM
        {
            get
            {
                return new OutputTransitViewModel()
                {
                    Type = "OUT",
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "INSPECTION MATERIAL",
                    InputTransitId = 1,
                    TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
                    {
                        new OutputTransitProductionOrderViewModel()
                        {
                            DeliveryOrder = new Application.ToBeRefactored.CommonViewModelObjectProperties.DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            IsSave = true,
                            Status = "Ok",
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
                            MaterialWidth = "1",
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
                   ViewModel.Group, ViewModel.Type, ViewModel.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, s.AdjDocumentNo, s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)
                    {
                        Id = s.Id
                    }).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel ModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAdj.Date, ViewModelAdj.Area, ViewModelAdj.Shift, ViewModelAdj.BonNo, ViewModelAdj.HasNextAreaDocument, ViewModelAdj.DestinationArea,
                   ViewModelAdj.Group, ViewModelAdj.Type, ViewModelAdj.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModelAdj.Area, "", ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, s.AdjDocumentNo, s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)
                    {
                        Id = s.Id
                    }).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel ModelAval
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAval.Date, ViewModelAval.Area, ViewModelAval.Shift, ViewModelAval.BonNo, ViewModelAval.HasNextAreaDocument, ViewModelAval.DestinationArea,
                   ViewModelAval.Group, ViewModel.Type, ViewModelAval.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, s.AdjDocumentNo, s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel ModelPC
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelPC.Date, ViewModelPC.Area, ViewModelPC.Shift, ViewModelPC.BonNo, ViewModelPC.HasNextAreaDocument, ViewModelPC.DestinationArea,
                   ViewModelPC.Group, ViewModel.Type, ViewModelPC.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModelPC.Area, ViewModelPC.DestinationArea, ViewModelPC.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, s.AdjDocumentNo, s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel ModelIM
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelIM.Date, ViewModelIM.Area, ViewModelIM.Shift, ViewModelIM.BonNo, ViewModelIM.HasNextAreaDocument, ViewModelIM.DestinationArea,
                   ViewModelIM.Group, ViewModel.Type, ViewModelIM.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area, ViewModelIM.DestinationArea, ViewModelPC.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, s.AdjDocumentNo, s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
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

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModelAdj);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_CreateAdjIn()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var vm = ViewModelAdj;
            foreach (var item in vm.TransitProductionOrders)
            {
                item.Balance = 1;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_DuplicateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputTransitId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_DuplicateShift_PreviousSUmmary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAVAL()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelAval }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelAval.Date, ViewModelAval.Area, "IN", ViewModelAval.InputTransitId, ViewModelAval.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModelAval);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreatePacking()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.InputTransitId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModelPC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateIM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.InputTransitId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Create(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "grade", "status", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 1, "a","a", 1, "a", 1, "a"));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

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
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.SetType(null, "", "");

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "grade", "status", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 0, "","a", 1, "a", 1, "a"));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = service.GenerateExcel(ViewModel);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GenerateExcel(ViewModelAdj);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var vm = ViewModel;
            vm.Type = null;

            var result = service.GenerateExcel(vm);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcelAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModelAdj;
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>();

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);


            var result = service.GenerateExcel(vm);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(EmptyDetailModel);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = service.GenerateExcel(vm);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetInputTransitProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false,1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = service.GetInputTransitProductionOrders(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_Success_GetInputTransitProductionOrders_All()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetInputTransitProductionOrders(0);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_Success_GetDistinctProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());
            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetDistinctProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new OutputTransitViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
            {
                new OutputTransitProductionOrderViewModel()
                {
                    IsSave = true,
                    Balance = 0
                },
                new OutputTransitProductionOrderViewModel()
                {
                    IsSave = true,
                    Balance = 1,
                    BalanceRemains = 0
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = null;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
            {
                new OutputTransitProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
            {
                new OutputTransitProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
            {
                new OutputTransitProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
                new OutputTransitProductionOrderViewModel()
                {
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            repoMock.Setup(s => s.DeleteTransitArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdjIN()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetBalance(1, "", "");
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.SetType(null, "", "");
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.TransitProductionOrders.FirstOrDefault().Balance = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
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
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            foreach (var item in vm.TransitProductionOrders)
            {
                item.Balance = 1;
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
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
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            vm.TransitProductionOrders.FirstOrDefault().Balance = -1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
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
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(false, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.TransitProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
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
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(true, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.TransitProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll3()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(null, Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll4()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll_Empty()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetDistinctAllProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            sppoutRepoMock.Setup(s => s.ReadAll()).Returns(Model.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetDistinctAllProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Validate_VM()
        {
            var date = DateTimeOffset.UtcNow;

            var index = new IndexViewModel()
            {
                Date = date
            };

            Assert.Null(index.Area);
            Assert.Equal(0, index.Id);
            Assert.Null(index.BonNo);
            Assert.Equal(date, index.Date);
            Assert.Null(index.DestinationArea);
            Assert.False(index.HasNextAreaDocument);
            Assert.Null(index.Shift);
            Assert.Null(index.Type);
            Assert.Null(index.Group);
            Assert.Empty(index.TransitProductionOrders);


            var adjvm = new AdjTransitProductionOrderViewModel();
            Assert.Null(adjvm.ProductionOrder);
            Assert.Null(adjvm.Material);
            Assert.Null(adjvm.MaterialConstruction);
            Assert.Null(adjvm.MaterialWidth);
            Assert.Null(adjvm.Remark);
            Assert.Null(adjvm.Construction);
            Assert.Null(adjvm.Unit);
            Assert.Equal(0, adjvm.BuyerId);
            Assert.Null(adjvm.Buyer);
            Assert.Null(adjvm.Color);
            Assert.Null(adjvm.Motif);
            Assert.Null(adjvm.UomUnit);

        }
    }
}
