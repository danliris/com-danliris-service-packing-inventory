﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping;
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
    public class InputShippingServiceTest
    {
        public InputShippingService GetService(IServiceProvider serviceProvider)
        {
            return new InputShippingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaOutputRepository outputRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo,
           IDyeingPrintingAreaOutputProductionOrderRepository outputSPPRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPRepo);


            return spMock;
        }

        private InputShippingViewModel ViewModel
        {
            get
            {
                return new InputShippingViewModel()
                {
                    Area = "SHIPPING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    OutputId = 1,
                    ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>()
                    {
                        new InputShippingProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s",
                                Name = "a"
                            },
                            DeliveryOrderRetur = new DeliveryOrderRetur()
                            {
                                Id = 1,
                                No = "s"
                            },
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
                            ProcessType = new ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            Packing = "s",
                            PackingType = "sd",
                            Qty = 1,
                            QtyPacking = 1,
                            Unit = "s",
                            UomUnit = "d",
                            InputId = 1,
                            OutputId = 1,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            DyeingPrintingAreaOutputProductionOrderId = 1
                        }
                    }
                };
            }
        }

        private InputShippingViewModel ViewModelPC
        {
            get
            {
                return new InputShippingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    OutputId = 1,
                    ShippingType = "ZONA GUDANG",
                    ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>()
                    {
                        new InputShippingProductionOrderViewModel()
                        {
                            Area = "PACKING",
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            DeliveryOrderRetur = new DeliveryOrderRetur()
                            {
                                Id = 1,
                                No = "s"
                            },
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
                            ProcessType = new ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            Packing = "s",
                            PackingType = "sd",
                            Qty = 1,
                            QtyPacking = 1,
                            Unit = "s",
                            UomUnit = "d",
                            InputId = 1,
                            OutputId = 1,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            DyeingPrintingAreaOutputProductionOrderId = 1
                        }
                    }
                };
            }
        }

        private InputShippingViewModel ViewModelGA
        {
            get
            {
                return new InputShippingViewModel()
                {
                    Area = "GUDANG AVAL",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    OutputId = 1,
                    ShippingType = "ZONA GUDANG",
                    ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>()
                    {
                        new InputShippingProductionOrderViewModel()
                        {
                            Area = "GUDANG AVAL",
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s"
                            },
                            DeliveryOrderRetur = new DeliveryOrderRetur()
                            {
                                Id = 1,
                                No = "s"
                            },
                            InputQtyPacking = 1,
                            InputQuantity = 1,
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
                            ProcessType = new ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            Packing = "s",
                            Id = 1,
                            PackingType = "sd",
                            Qty = 1,
                            QtyPacking = 1,
                            Unit = "s",
                            UomUnit = "d",
                            InputId = 1,
                            OutputId = 1,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            DyeingPrintingAreaOutputProductionOrderId = 1
                        }
                    }
                };
            }
        }

        private InputShippingViewModel ViewModelGJ
        {
            get
            {
                return new InputShippingViewModel()
                {
                    Area = "GUDANG JADI",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    OutputId = 1,
                    ShippingType = "ZONA GUDANG",
                    ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>()
                    {
                        new InputShippingProductionOrderViewModel()
                        {
                            Area = "GUDANG JADI",
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Id = 1,
                            HasOutputDocument = false,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "s",
                                Name = "a"
                            },
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
                            ProcessType = new ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            DeliveryOrderRetur = new DeliveryOrderRetur()
                            {
                                Id = 1,
                                No = "s"
                            },
                            MaterialWidth = "1",
                            Packing = "s",
                            PackingType = "sd",
                            Qty = 1,
                            QtyPacking = 1,
                            Remark = "s",
                            Unit = "s",
                            UomUnit = "d",
                            InputId = 1,
                            OutputId = 1,
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            DyeingPrintingAreaOutputProductionOrderId = 1,
                            ProductSKUId = 1,
                            FabricSKUId = 1,
                            ProductSKUCode = "c",
                            HasPrintingProductSKU = false,
                            ProductPackingId = 1,
                            FabricPackingId = 1,
                            ProductPackingCode = "c",
                            HasPrintingProductPacking = false
                        }
                    }
                };
            }
        }

        private OutputShippingViewModel ViewModelFDO
        {
            get
            {
                return new OutputShippingViewModel()
                {
                    Area = "GUDANG JADI",
                    BonNo = "s",
                    Type = "OUT",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "SHIPPING",
                    Group = "A",
                    ShippingCode = "AS",
                    InputShippingId = 1,
                    HasSalesInvoice = false,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "no",
                        Name = "name"
                    },
                    ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
                    {
                        new OutputShippingProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            DeliveryNote = "s",
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            IsSave = true,
                            Remark = "remar",
                            ShippingRemark = "re",
                            ShippingGrade = "gra",
                            Weight = 1,
                            Motif = "sd",

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
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "sd",
                                Name = "a"
                            },
                            ProcessType = new ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            YarnMaterial = new YarnMaterial()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            Packing ="s",
                            QtyPacking = 1,
                            Qty = 1,
                            Id = 1,
                            PackingType = "sd",

                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd",
                                OrderQuantity = 100
                            },
                            Unit = "s",
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel Model
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.PackingType, s.Color, s.Motif, s.Grade, s.QtyPacking, s.Packing, s.Qty, s.UomUnit, s.HasOutputDocument, s.Qty, s.Unit, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, s.ProcessType.Id, s.ProcessType.Name,
                        s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, s.FinishWidth, s.DateIn, s.DeliveryOrder.Name)).ToList());
                   
            }
        }

        private DyeingPrintingAreaInputModel ModelPC
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelPC.Date, ViewModelPC.Area, ViewModelPC.Shift, ViewModelPC.BonNo, ViewModelPC.Group, ViewModelPC.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelPC.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.PackingType, s.Color, s.Motif, s.Grade, s.QtyPacking, s.Packing, s.Qty, s.UomUnit, s.HasOutputDocument, s.Qty, s.Unit, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, s.ProcessType.Id, s.ProcessType.Name,
                        s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, s.FinishWidth, s.DateIn, s.DeliveryOrder.Name)).ToList());
                                }
        }

        private DyeingPrintingAreaInputModel ModelGJ
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelGJ.Date, ViewModelGJ.Area, ViewModelGJ.Shift, ViewModelGJ.BonNo, ViewModelGJ.Group, ViewModelGJ.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelGJ.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.PackingType, s.Color, s.Motif, s.Grade, s.QtyPacking, s.Packing, s.Qty, s.UomUnit, s.HasOutputDocument, s.Qty, s.Unit, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, s.ProcessType.Id, s.ProcessType.Name,
                        s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, s.FinishWidth, s.DateIn, s.DeliveryOrder.Name)).ToList());
                    
            }
        }

        private DyeingPrintingAreaInputModel ModelGA
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelGA.Date, ViewModelGA.Area, ViewModelGA.Shift, ViewModelGA.BonNo, ViewModelGA.Group, ViewModelGA.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelGA.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.PackingType, s.Color, s.Motif, s.Grade, s.QtyPacking, s.Packing, s.Qty, s.UomUnit, s.HasOutputDocument, s.Qty, s.Unit, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, s.ProcessType.Id, s.ProcessType.Name,
                        s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, s.FinishWidth, DateTimeOffset.Now, s.DeliveryOrder.Name)).ToList());
                    
            }
        }

        private DyeingPrintingAreaOutputModel ModelFDO
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelFDO.Date, ViewModelFDO.Area, ViewModelFDO.Shift, ViewModelFDO.BonNo, ViewModelFDO.HasNextAreaDocument, ViewModelFDO.DestinationArea,
                   ViewModelFDO.Group, ViewModelFDO.DeliveryOrder.Id, ViewModelFDO.DeliveryOrder.No, ViewModelFDO.HasSalesInvoice, ViewModelFDO.Type, ViewModelFDO.ShippingCode, ViewModelFDO.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModelFDO.Area, ViewModelFDO.DestinationArea, ViewModelFDO.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, s.DateOut, s.DeliveryOrder.Name)).ToList());

            }
        }

        private OutputPreShippingProductionOrderViewModel OutputModel
        {
            get
            {
                return new OutputPreShippingProductionOrderViewModel()
                {
                    DeliveryOrder = new DeliveryOrderSales
                    {
                        Id = 1,
                        No = "12",
                        Name = "a"
                    },
                    ProductionOrder = new ProductionOrder
                    {
                        Code = "Code",
                        Id = 1,
                        No = "daf",
                        OrderQuantity = 12,
                        Type = "type",
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
                    MaterialWidth = "1",
                    CartNo = "adsf",
                    Construction = "df",
                    Unit = "df",
                    Buyer = "df",
                    Color = "df",
                    Motif = "df",
                    Remark = "s",
                    UomUnit = "df",
                    Grade = "fd",
                    Packing = "dsaf",
                    QtyPacking = 1,
                    Qty = 1,
                    PackingType = "df",
                    OutputId = 1,
                    DyeingPrintingAreaInputProductionOrderId = 1
                };
            }
        }

        [Fact]
        public void Should_Success_GetDistinctProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            //var fabricService = new Mock<IFabricPackingSKUService>();







            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { ModelGJ }.AsQueryable());

            sppoutRepoMock.Setup(s => s.ReadAll()).Returns(ModelFDO.DyeingPrintingAreaOutputProductionOrders.AsQueryable());
            var service = GetService(GetServiceProvider(inputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, repoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetDistinctDO(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_If_Summary_Null()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();

            //summaryRepoMock.Setup(s => s.ReadAll())
            //     .Returns(new List<DyeingPrintingAreaSummaryModel>() {
            //         new DyeingPrintingAreaSummaryModel()
            //     }.AsQueryable());
            //summaryRepoMock.Setup(s => s.ReadAll())
            //     .Returns<IQueryable<DyeingPrintingAreaSummaryModel>>(null);

            //summaryRepoMock.Setup(s => s.ReadAll().FirstOrDefault())
            //    .Returns<DyeingPrintingAreaSummaryModel>(null);
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                                 new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", 0, ViewModel.BonNo, item.ProductionOrder.Id,
                                 item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_If_Model_Null()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            var modelModif = Model;
            modelModif.SetArea("error", "unittest", "");
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { modelModif }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                                 new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", 0, ViewModel.BonNo, item.ProductionOrder.Id,
                                 item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_If_Model_Null_Set_Balance_Output()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            var modelModif = Model;
            modelModif.SetArea("error", "unittest", "");
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { modelModif }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                                 new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", 0, ViewModel.BonNo, item.ProductionOrder.Id,
                                 item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_DuplicateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaInputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_ReadPreShipping()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "GUDANG JADI","pagi","no",false,
                    "SHIPPING","A","OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>(){
                        new DyeingPrintingAreaOutputProductionOrderModel("PACKING","SHIPPING", false, 1,"no","t",1,"1","1","sd","cs","sd","as","sd","asd","asd","zimmer","sd","sd",1, 1,1,1,"a",1,"a","1","",1,"a","a",1,"a","a",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a",DateTimeOffset.Now,DateTimeOffset.Now)

                    }) }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = service.ReadOutputPreShipping(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_ReadPreShipping_SPP()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            productionOrderRepoMock.Setup(s => s.ReadAll())
                 .Returns(Model.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = service.ReadProductionOrders(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GetOutputPreShippingProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            outputSPPRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("GUDANG JADI", "SHIPPING", false, 1, "a", "e", 1,"rr", "1", "as", "test", "unit", "color", "motif", "mtr", "remark","zimmer", "a", "a", 1, 1,1,1,"a",1,"a","1","",1,"a","a",1,"a","a",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now,DateTimeOffset.Now)

            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = service.GetOutputPreShippingProductionOrders(0);
            var result2 = service.GetOutputPreShippingProductionOrders(1);

            Assert.NotEmpty(result);
            Assert.NotEmpty(result2);
        }

        [Fact]
        public async Task Should_Success_Reject()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelGJ }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModelGJ.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelGJ.Date, ViewModelGJ.Area, "IN", item.OutputId, ViewModelGJ.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelGJ);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_Packing()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", item.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelPC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_NoSummary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelGA }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModelGA.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelGA);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelGA }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelGA }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModelGA.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelGA.Date, ViewModelGA.Area, "IN", item.OutputId, ViewModelGA.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelGA);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_Duplicate_Shift_NoSummary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            productionOrderRepoMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.ShippingProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelPC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(0, "", "");
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(1));

        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.ShippingProductionOrders.FirstOrDefault().Qty = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateShippingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);


            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(1, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateShippingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);


            await Assert.ThrowsAnyAsync<Exception>(() => service.Update(1, vm));
        }

        [Fact]
        public async Task Should_Success_Update_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.ShippingProductionOrders = new List<InputShippingProductionOrderViewModel>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateShippingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object);


            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Get_Set_OutputPreShiping()
        {
            var outPre = OutputModel;
            var test = outPre.DeliveryOrder;
            var test2 = outPre.ProductionOrder;
            var test3 = outPre.CartNo;
            var test4 = outPre.Construction;
            var Unit = outPre.Unit;
            var buyre = outPre.Buyer;
            var color = outPre.Color;
            var motif = outPre.Motif;
            var uomUnit = outPre.UomUnit;
            var grade = outPre.Grade;
            var packing = outPre.Packing;
            var qtyPacking = outPre.QtyPacking;
            var qty = outPre.Qty;
            var packingTYpe = outPre.PackingType;
            var OuptuId = outPre.OutputId;
            var dyeingid = outPre.DyeingPrintingAreaInputProductionOrderId;
            var act = outPre.Active;
            var cre = outPre.CreatedAgent;
            var creut = outPre.CreatedUtc;
            var create = outPre.CreatedBy;
            var lastMod = outPre.LastModifiedBy;
            var lstt = outPre.LastModifiedAgent;
            var dle = outPre.DeletedAgent;
            var dleteis = outPre.IsDeleted;
            var dlet = outPre.DeletedBy;
            var delag = outPre.DeletedUtc;
            //var test = outPre.Grade;
            //var test2 = outPre.DeliveryOrder
            Assert.NotNull(test);
            Assert.NotNull(outPre.Material);
            Assert.NotNull(outPre.MaterialConstruction);
            Assert.NotNull(outPre.MaterialWidth);
            Assert.NotNull(outPre.Remark);

            Assert.Null(outPre.ProductPackingCode);
            Assert.Null(outPre.ProductSKUCode);
            Assert.Equal(0, outPre.ProductSKUId);
            Assert.Equal(0, outPre.FabricSKUId);
            Assert.Equal(0, outPre.ProductPackingId);
            Assert.Equal(0, outPre.FabricPackingId);
            Assert.False(outPre.HasPrintingProductPacking);
            Assert.False(outPre.HasPrintingProductSKU);
        }

        [Fact]
        public void Get_Set_InputSPPShiping()
        {
            var outPre = ViewModel;
            var inputId = outPre.ShippingProductionOrders.FirstOrDefault().InputId;
            //var test = outPre.Grade;
            //var test2 = outPre.DeliveryOrder
            Assert.NotEqual(0, inputId);
            Assert.Equal(0, outPre.ShippingProductionOrders.FirstOrDefault().PackingLength);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new InputShippingViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(3);
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddHours(-2);

            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddHours(2);
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Id = 1;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel3()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(null, Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel4()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel_Empty()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var productionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, productionOrderRepoMock.Object, outputSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

    }
}
