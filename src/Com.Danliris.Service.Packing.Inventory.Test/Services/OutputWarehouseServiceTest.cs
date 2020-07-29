using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class OutputWarehouseServiceTest
    {
        public OutputWarehouseService GetService(IServiceProvider serviceProvider)
        {
            return new OutputWarehouseService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository inputRepo,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputProductionRepo,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaOutputProductionOrderRepository outputProductionRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionRepo);

            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository inputRepo,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputProductionRepo,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaOutputProductionOrderRepository outputProductionRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionRepo);

            return spMock;
        }

        private OutputWarehouseViewModel ViewModelToShipping
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Area = "GUDANG JADI",
                    Type = "OUT",
                    BonNo = "GJ.SP.20.001",
                    Bon = new IndexViewModel
                    {
                        Area = "GUDANG JADI",
                        BonNo = "GJ.SP.20.001",
                        Date = DateTimeOffset.UtcNow,
                        DestinationArea = "GUDANG JADI",
                        Group = "A",
                        Id = 1,
                        HasNextAreaDocument = false,
                        Shift = "PAGI"
                    },
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    InputWarehouseId = 12,
                    Group = "A",
                    WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
                    {
                        new OutputWarehouseProductionOrderViewModel()
                        {
                            IsSave = true,
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
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
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            HasNextAreaDocument = false,
                            Material = "a",
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        private OutputWarehouseViewModel ViewModelToIM
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Area = "GUDANG JADI",
                    BonNo = "GJ.IM.20.001",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "INSPECTION MATERIAL",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    InputWarehouseId = 12,
                    Type = "OUT",
                    Group = "A",
                    WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
                    {
                        new OutputWarehouseProductionOrderViewModel()
                        {
                            IsSave = true,
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
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
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            HasNextAreaDocument = false,
                            Material = "a",
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel OutputModelToShippingArea
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelToShipping.Date,
                                                         ViewModelToShipping.Area,
                                                         ViewModelToShipping.Shift,
                                                         ViewModelToShipping.BonNo,
                                                         ViewModelToShipping.HasNextAreaDocument,
                                                         ViewModelToShipping.DestinationArea,
                                                         ViewModelToShipping.Group,
                                                         ViewModelToShipping.Type,
                                                         ViewModelToShipping.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelToShipping.Area,
                                                                                                             ViewModelToShipping.DestinationArea,
                                                                                                             ViewModelToShipping.HasNextAreaDocument,
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction,
                                                                                                             s.CartNo,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.Unit,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.UomUnit,
                                                                                                             s.Remark,
                                                                                                             s.Grade,
                                                                                                             s.Status,
                                                                                                             s.Balance,
                                                                                                             s.Id,
                                                                                                             s.BuyerId,
                                                                                                             s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, "", s.PackagingQty, s.PackagingType, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }
        }
        private DyeingPrintingAreaOutputModel OutputModelToGAArea
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelToIM.Date,
                                                         ViewModelToIM.Area,
                                                         ViewModelToIM.Shift,
                                                         ViewModelToIM.BonNo,
                                                         ViewModelToIM.HasNextAreaDocument,
                                                         ViewModelToIM.DestinationArea,
                                                         ViewModelToIM.Group,
                                                         ViewModelToShipping.Type,
                                                         ViewModelToIM.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel("GUDANG JADI",
                                                                                                             ViewModelToIM.DestinationArea,
                                                                                                             ViewModelToIM.HasNextAreaDocument,
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction,
                                                                                                             s.CartNo,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.Unit,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.UomUnit,
                                                                                                             s.Remark,
                                                                                                             s.Grade,
                                                                                                             s.Status,
                                                                                                             s.Balance,
                                                                                                             s.Id,
                                                                                                             s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, "", s.PackagingQty, s.PackagingType, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }

        }
        private DyeingPrintingAreaOutputModel OutputModelToIMArea
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelToIM.Date,
                                                         ViewModelToIM.DestinationArea,
                                                         ViewModelToIM.Shift,
                                                         ViewModelToIM.BonNo,
                                                         ViewModelToIM.HasNextAreaDocument,
                                                         ViewModelToIM.DestinationArea,
                                                         ViewModelToIM.Group,
                                                         ViewModelToShipping.Type,
                                                         ViewModelToIM.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelToIM.Area,
                                                                                                             ViewModelToIM.DestinationArea,
                                                                                                             ViewModelToIM.HasNextAreaDocument,
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction,
                                                                                                             s.CartNo,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.Unit,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.UomUnit,
                                                                                                             s.Remark,
                                                                                                             s.Grade,
                                                                                                             s.Status,
                                                                                                             s.Balance,
                                                                                                             s.Id,
                                                                                                             s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, "", s.PackagingQty, s.PackagingType, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }

        }
        private DyeingPrintingAreaInputModel InputModelToShippingArea
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelToShipping.Date,
                                                         ViewModelToShipping.Area,
                                                         ViewModelToShipping.Shift,
                                                         ViewModelToShipping.BonNo,
                                                         ViewModelToShipping.Group,
                                                         ViewModelToShipping.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelToShipping.Area,
                                                                                                             1,
                                                                                                             "1",
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.PackagingType,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.Grade,
                                                                                                             s.PackagingQty,
                                                                                                             s.PackagingUnit,
                                                                                                             s.QtyOrder,
                                                                                                             s.UomUnit,
                                                                                                             s.HasNextAreaDocument,
                                                                                                             s.Balance,
                                                                                                             s.Unit,
                                                                                                             s.BuyerId,
                                                                                                             1,
                                                                                                             s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name,
                                                                                                             s.MaterialConstruction.Id,
                                                                                                             s.MaterialConstruction.Name,
                                                                                                             s.MaterialWidth, s.CartNo, s.Remark)).ToList());
            }
        }


        private DyeingPrintingAreaOutputModel EmptyOutputProductionOrderModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelToShipping.Date,
                                                         ViewModelToShipping.Area,
                                                         ViewModelToShipping.Shift,
                                                         ViewModelToShipping.BonNo,
                                                         ViewModelToShipping.HasNextAreaDocument,
                                                         ViewModelToShipping.DestinationArea,
                                                         ViewModelToShipping.Group,
                                                         ViewModelToShipping.Type,
                                                         new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }

        private DyeingPrintingAreaSummaryModel SummaryModel
        {
            get
            {
                return new DyeingPrintingAreaSummaryModel(ViewModelToShipping.Date,
                                                          ViewModelToShipping.Area,
                                                          "OUT",
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().InputId,
                                                          ViewModelToShipping.BonNo,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().ProductionOrder.Id,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().ProductionOrderNo,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().CartNo,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Buyer,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Construction,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Unit,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Color,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Motif,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().UomUnit,
                                                          ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault().Balance)
                {
                    Id = 8
                };
            }
        }

        private InputSppWarehouseViewModel InputSPP
        {
            get
            {
                return new InputSppWarehouseViewModel
                {
                    ProductionOrderId = 1,
                    ProductionOrderCode = "F/2020/000",
                    ProductionOrderNo = "F/2020/000",
                    ProductionOrderType = "REST",
                    ProductionOrderOrderQuantity = 10,
                    OutputId = 1,
                    ProductionOrderItems = new List<InputSppWarehouseItemListViewModel>
                    {
                        new InputSppWarehouseItemListViewModel
                        {
                            CartNo = "",
                            BuyerId = 1,
                            Buyer = "",
                            Construction = "",
                            Unit = "",
                            Color = "",
                            Motif = "",
                            UomUnit = "",
                            Remark = "",
                            Grade = "",
                            Status = "",
                            Balance = 1,
                            PackingInstruction = "",
                            PackagingType = "",
                            PackagingQty = 1,
                            PackagingUnit = "",
                            AvalALength = 1,
                            AvalBLength = 1,
                            AvalConnectionLength = 1,
                            DeliveryOrderSalesId = 1,
                            DeliveryOrderSalesNo = "",
                            AvalType = "",
                            AvalCartNo = "",
                            AvalQuantityKg = 1,
                            Description = "",
                            DeliveryNote = "",
                            Area = "",
                            DestinationArea = "",
                            HasOutputDocument = false,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            Qty =1,
                            InputId = 1,
                        }
                    }
                };
            }
        }

        private OutputWarehouseViewModel ViewModelAdj
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Area = "GUDANG JADI",
                    Type = "ADJ",
                    BonNo = "GJ.SP.20.001",
                    Bon = new IndexViewModel
                    {
                        Area = "GUDANG JADI",
                        BonNo = "GJ.SP.20.001",
                        Date = DateTimeOffset.UtcNow,
                        DestinationArea = "GUDANG JADI",
                        Group = "A",
                        Id = 1,
                        HasNextAreaDocument = false,
                        Shift = "PAGI"
                    },
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    InputWarehouseId = 12,
                    Group = "A",
                    WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
                    {
                        new OutputWarehouseProductionOrderViewModel()
                        {
                            IsSave = true,
                            AdjDocumentNo = "a",
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
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
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            HasNextAreaDocument = false,
                            Material = "a",
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel ModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAdj.Date, ViewModelAdj.Area, ViewModelAdj.Shift, ViewModelAdj.BonNo, ViewModelAdj.HasNextAreaDocument, ViewModelAdj.DestinationArea,
                   ViewModelAdj.Group, ViewModelAdj.Type, ViewModelAdj.WarehousesProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade,
                    s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo,
                    true, ViewModelAdj.Area, "", 0, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.AdjDocumentNo,
                    s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            }
        }

        [Fact]
        public async Task Should_Success_InsertNewToShippingNoSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.InsertAsync(OutputModelToShippingArea))
                .ReturnsAsync(1);

            //var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(SummaryModel))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertNewToShippingHasPreviousSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.InsertAsync(OutputModelToShippingArea))
                .ReturnsAsync(1);

            //var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     SummaryModel
                 }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(SummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertNewToIMNoSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.InsertAsync(OutputModelToIMArea))
                .ReturnsAsync(1);

            //var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(SummaryModel))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertNewToIMHasPreviousSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            outputRepoMock.Setup(s => s.InsertAsync(OutputModelToIMArea))
                .ReturnsAsync(1);

            //var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     SummaryModel
                 }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(SummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertToShippingExistingOutputHasSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());

            var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {
                     SummaryModel
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(SummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertToShippingExistingOutputNoSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());

            var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(SummaryModel))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertToIMExistingOutputHasSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToIMArea }.AsQueryable());

            var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {
                     SummaryModel
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(SummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
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
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var vm = ViewModelAdj;
            foreach (var item in vm.WarehousesProductionOrders)
            {
                item.Balance = -1;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_CreateAdjIn()
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
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var vm = ViewModelAdj;
            foreach (var item in vm.WarehousesProductionOrders)
            {
                item.Balance = 1;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
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

            var tes = new DyeingPrintingAreaOutputModel(ViewModelToIM.Date,
                                                         ViewModelToIM.DestinationArea,
                                                         ViewModelToIM.Shift,
                                                         ViewModelToIM.BonNo,
                                                         ViewModelToIM.HasNextAreaDocument,
                                                         ViewModelToIM.DestinationArea,
                                                         ViewModelToIM.Group,
                                                         ViewModelToIM.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelToIM.Area,
                                                                                                             ViewModelToIM.DestinationArea,
                                                                                                             false,
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction,
                                                                                                             s.CartNo,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.Unit,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.UomUnit,
                                                                                                             s.Remark,
                                                                                                             s.Grade,
                                                                                                             s.Status,
                                                                                                             s.Balance,
                                                                                                             s.Id,
                                                                                                             s.BuyerId,
                                                                                                             s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                    s.MaterialWidth, "", s.PackagingQty, s.PackagingType, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            tes.Id = 1;
            tes.SetType("OUT", "", "");
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            outputRepoMock.Setup(o => o.DeleteWarehouseArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            outputRepoMock.Setup(o => o.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tes);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToIMArea }.AsQueryable());

            var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            var testinput = new DyeingPrintingAreaInputModel(ViewModelToShipping.Date,
                                                         ViewModelToShipping.Area,
                                                         ViewModelToShipping.Shift,
                                                         ViewModelToShipping.BonNo,
                                                         ViewModelToShipping.Group,
                                                         ViewModelToShipping.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelToShipping.Area,
                                                                                                             1,
                                                                                                             "1",
                                                                                                             s.ProductionOrder.Id,
                                                                                                             s.ProductionOrder.No,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.PackagingType,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.Grade,
                                                                                                             s.PackagingQty,
                                                                                                             s.PackagingUnit,
                                                                                                             s.QtyOrder,
                                                                                                             s.UomUnit,
                                                                                                             false,
                                                                                                             s.Balance,
                                                                                                             s.Unit,
                                                                                                             s.BuyerId,
                                                                                                             1,
                                                                                                             s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name,
                                                                                                             s.MaterialConstruction.Id,
                                                                                                             s.MaterialConstruction.Name,
                                                                                                             s.MaterialWidth, s.CartNo, s.Remark)).ToList());
            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {
                     SummaryModel
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(SummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteNoType()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = OutputModelToShippingArea;
            model.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            outputRepoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                         inputProductionOrderRepoMock.Object,
                                                         movementRepoMock.Object,
                                                         summaryRepoMock.Object,
                                                         outputRepoMock.Object,
                                                         outputProductionOrderRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = ModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetBalance(-1, "", "");
            }

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            outputRepoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdjIN()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetBalance(1, "", "");
            }

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(model);
            outputRepoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertToIMExistingOutputNoSummary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToIMArea }.AsQueryable());

            var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(SummaryModel))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelToShipping);

            Assert.NotEqual(0, result);
        }

        //[Fact]
        //public async Task Should_Success_CreateNewToShippingHasPreviousSummary()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    outputRepoMock.Setup(o => o.GetDbSet())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

        //    inputRepoMock.Setup(s => s.InsertAsync(OutputModelToShippingArea))
        //        .ReturnsAsync(1);

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);

        //    var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             DyeingPrintingAreaSummaryModel
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(DyeingPrintingAreaSummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModelToShipping);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_CreateNewToIMHasPreviousSummary()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    outputRepoMock.Setup(o => o.GetDbSet())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

        //    inputRepoMock.Setup(s => s.InsertAsync(OutputModelToIMArea))
        //        .ReturnsAsync(1);

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);

        //    var item = ViewModelToShipping.WarehousesProductionOrders.FirstOrDefault();

        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             DyeingPrintingAreaSummaryModel
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(DyeingPrintingAreaSummaryModel.Id, It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModelToShipping);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public void Should_Success_Read()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModelToShippingArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.Read("0");

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GetInputSppWarehouseItemList()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModelToShippingArea }.AsQueryable());

            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(InputModelToShippingArea.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetInputSppWarehouseItemList();

            Assert.NotEmpty(result);
        }
        [Fact]
        public void Should_Success_GetInputSppWarehouseItemListByBonId()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModelToShippingArea }.AsQueryable());
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(InputModelToShippingArea.DyeingPrintingAreaInputProductionOrders.AsQueryable());
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetInputSppWarehouseItemList(0);

            Assert.NotEmpty(result);
        }
        [Fact]
        public void Should_Success_GetOutputSppWarehouseItemListByBonId()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModelToShippingArea }.AsQueryable());
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModelToShippingArea }.AsQueryable());
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(InputModelToShippingArea.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.ReadAll())
                 .Returns(OutputModelToShippingArea.DyeingPrintingAreaOutputProductionOrders.AsQueryable());
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetOutputSppWarehouseItemList(0);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModelToShippingArea);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadById_PackagingQtyNotZero()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = OutputModelToShippingArea;

            foreach(var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetPackagingQty(0, "", "");
            }

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdAdj_PackagingQtyZero()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetPackagingQty(0, "", "");
            }

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdNoType()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = OutputModelToShippingArea;
            model.SetType(null, "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModelToShippingArea);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel_NoType()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = OutputModelToShippingArea;
            model.SetType(null, "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                         inputProductionOrderRepoMock.Object,
                                                         movementRepoMock.Object,
                                                         summaryRepoMock.Object,
                                                         outputRepoMock.Object,
                                                         outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAdj_PackagingQtyNotZero()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetPackagingQty(10, "", "");
            }


            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                         inputProductionOrderRepoMock.Object,
                                                         movementRepoMock.Object,
                                                         summaryRepoMock.Object,
                                                         outputRepoMock.Object,
                                                         outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAdj_Empty()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                         inputProductionOrderRepoMock.Object,
                                                         movementRepoMock.Object,
                                                         summaryRepoMock.Object,
                                                         outputRepoMock.Object,
                                                         outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcel(1);

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
                .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelToGAArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(OutputModelToGAArea.Date.AddDays(-1), OutputModelToGAArea.Date.AddDays(1), 7);

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
                .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelToGAArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(OutputModelToGAArea.Date.AddDays(-1), null, 7);

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
                .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelToGAArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, OutputModelToGAArea.Date.AddDays(1), 7);

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
                .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelToGAArea }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(EmptyOutputProductionOrderModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetInputTransitProductionOrders()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetInputWarehouseProductionOrders();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_Success_GenerateBonIM()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "INSPECTION MATERIAL");
            var result1 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "SHIPPING");
            var result2 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "PACKING");
            var result3 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "TRANSIT");

            Assert.NotEmpty(result);
            result = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "xx");

            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void Get_InputSPPModel()
        {
            var result = InputSPP;
            var test = result.ProductionOrderId;
            var test1 = result.ProductionOrderCode;
            var test2 = result.ProductionOrderNo;
            var test3 = result.ProductionOrderType;
            var test4 = result.ProductionOrderOrderQuantity;
            var test5 = result.OutputId;
            var test6 = result.ProductionOrderItems;

            var member6 = test6.FirstOrDefault().GetType().GetProperties();
            foreach (var i in member6)
            {

                var val = i.GetValue(new InputSppWarehouseItemListViewModel(), null);
            }
            Assert.NotNull(InputSPP);
        }

        [Fact]
        public void Should_Success_GetDistinctAllProductionOrders()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = OutputModelToShippingArea;
            foreach(var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetPackagingQty(10, "", "");
            }
            outputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(model.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            inputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);
            var result = service.GetDistinctAllProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
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


            var serviceProvider = GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new OutputWarehouseViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
            {
                new OutputWarehouseProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 0
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
            vm.WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
            {
                new OutputWarehouseProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
            {
                new OutputWarehouseProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
            {
                new OutputWarehouseProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
                new OutputWarehouseProductionOrderViewModel()
                {
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }

        [Fact]
        public void ValidateVM()
        {
            
            var date = DateTimeOffset.UtcNow;
            var index = new IndexViewModel()
            {
                Date = date
            };
            Assert.Null(index.Group);
            Assert.Null(index.Area);
            Assert.Equal(0, index.Id);
            Assert.Null(index.BonNo);
            Assert.Equal(date, index.Date);
            Assert.Null(index.DestinationArea);
            Assert.False(index.HasNextAreaDocument);
            Assert.Null(index.Shift);
            Assert.Null(index.Type);

            var adjvm = new AdjWarehouseProductionOrderViewModel();
            Assert.Null(adjvm.ProductionOrder);
            Assert.Null(adjvm.MaterialProduct);
            Assert.Null(adjvm.MaterialConstruction);
            Assert.Null(adjvm.MaterialWidth);
            Assert.Null(adjvm.Construction);
            Assert.Null(adjvm.Unit);
            Assert.Null(adjvm.Grade);
            Assert.Null(adjvm.PackagingUnit);
            Assert.Equal(0, adjvm.BuyerId);
            Assert.Null(adjvm.Buyer);
            Assert.Null(adjvm.Color);
            Assert.Null(adjvm.Motif);
            Assert.Null(adjvm.UomUnit);
            Assert.Equal(0, adjvm.PackagingQty);
            Assert.Equal(0, adjvm.Quantity);
            Assert.Null(adjvm.PackagingType);
        }
    }
}
