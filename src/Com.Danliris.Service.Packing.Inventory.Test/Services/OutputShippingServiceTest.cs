using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
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
    public class OutputShippingServiceTest
    {
        public OutputShippingService GetService(IServiceProvider serviceProvider)
        {
            return new OutputShippingService(serviceProvider);
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

        private OutputShippingViewModel ViewModel
        {
            get
            {
                return new OutputShippingViewModel()
                {
                    Area = "SHIPPING",
                    BonNo = "s",
                    Type = "OUT",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "PENJUALAN",
                    Group = "A",
                    ShippingCode = "AS",
                    InputShippingId = 1,
                    HasSalesInvoice = false,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "no"
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
                                No = "sd"
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

        private OutputShippingViewModel ViewModelAdj
        {
            get
            {
                return new OutputShippingViewModel()
                {
                    Area = "SHIPPING",
                    BonNo = "s",
                    Type = "ADJ",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "PENJUALAN",
                    Group = "A",
                    InputShippingId = 1,
                    HasSalesInvoice = false,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "no"
                    },
                    ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
                    {
                        new OutputShippingProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Balance = -1,
                            Grade = "s",
                            DeliveryNote = "s",
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            IsSave = true,
                            Remark = "remar",
                            ShippingRemark = "re",
                            ShippingGrade = "gra",
                            Weight = 1,
                            Motif = "sd",
                            HasNextAreaDocument = true,
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
                                No = "sd"
                            },
                            Packing ="s",
                            QtyPacking = 1,
                            Qty = 1,
                            Id = 1,
                            PackingType = "sd",
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

        private OutputShippingViewModel ViewModelPJ
        {
            get
            {
                return new OutputShippingViewModel()
                {
                    Area = "SHIPPING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    HasNextAreaDocument = false,
                    DestinationArea = "PENJUALAN",
                    Group = "A",
                    InputShippingId = 1,
                    ShippingCode = "AS",
                    HasSalesInvoice = false,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "no"
                    },
                    ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
                    {
                        new OutputShippingProductionOrderViewModel()
                        {
                            Buyer = "s",
                            BuyerId = 1,
                            IsSave = true,
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            DeliveryNote = "s",
                            DyeingPrintingAreaInputProductionOrderId = 1,
                            HasSalesInvoice = false,
                            Remark = "remar",
                            ShippingGrade = "s",
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
                            ShippingRemark = "d",
                            Weight = 1,
                            Motif = "sd",
                            DeliveryOrder = new DeliveryOrderSales()
                            {
                                Id = 1,
                                No = "sd"
                            },
                            Packing ="s",
                            QtyPacking = 1,
                            Qty = 1,
                            Id = 1,
                            PackingType = "sd",
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

        private DyeingPrintingAreaOutputModel Model
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.DeliveryOrder.Id, ViewModel.DeliveryOrder.No, ViewModel.HasSalesInvoice, ViewModel.Type, ViewModel.ShippingCode, ViewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth,  s.DateIn,s.DateOut, s.DeliveryOrder.Name, s.InventoryType)).ToList());
                    
            }
        }

        private DyeingPrintingAreaOutputModel ModelPJ
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelPJ.Date, ViewModelPJ.Area, ViewModelPJ.Shift, ViewModelPJ.BonNo, ViewModelPJ.HasNextAreaDocument, ViewModelPJ.DestinationArea,
                   ViewModelPJ.Group, ViewModelPJ.DeliveryOrder.Id, ViewModelPJ.DeliveryOrder.No, ViewModelPJ.HasSalesInvoice, ViewModel.Type, ViewModel.ShippingCode, ViewModelPJ.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn,s.DateOut, s.DeliveryOrder.Name, s.InventoryType)).ToList());
                    
            }
        }

        private DyeingPrintingAreaOutputModel ModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAdj.Date, ViewModelAdj.Area, ViewModelAdj.Shift, ViewModelAdj.BonNo, ViewModelAdj.HasNextAreaDocument, ViewModelAdj.DestinationArea,
                   ViewModelAdj.Group, ViewModelAdj.Type, ViewModelAdj.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModelAdj.Area, ViewModelAdj.DestinationArea, ViewModelAdj.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Balance, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Qty, s.FinishWidth, s.DateIn,s.DateOut, s.DeliveryOrder.Name, s.InventoryType)).ToList());
                    
            }
        }

        private DyeingPrintingAreaOutputModel EmptyDetailModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                  ViewModel.Group, ViewModel.DeliveryOrder.Id, ViewModel.DeliveryOrder.No, ViewModel.HasSalesInvoice, ViewModel.Type, ViewModel.ShippingCode, new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputShippingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_BQ()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputShippingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var vm = ViewModel; 

            vm.ShippingProductionOrders.First().ShippingGrade = "BQ";

            var result = await service.Create(vm);

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
        public async Task Should_Success_CreateAdj_Duplicate()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = ModelAdj;
            model.SetType("ADJ OUT", "", "");

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            outSppRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
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
            foreach (var item in vm.ShippingProductionOrders)
            {
                item.Balance = 1;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Buyer()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var vm = ViewModel;
            vm.DestinationArea = "BUYER";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Buyer_BQ()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var vm = ViewModel;
            vm.DestinationArea = "BUYER";
            vm.ShippingProductionOrders.First().ShippingGrade = "BQ";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_IM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = vm.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.InputShippingId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            vm.DestinationArea = "INSPECTION MATERIAL";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Transit()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = vm.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.InputShippingId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            vm.DestinationArea = "TRANSIT";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_PACKING()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = vm.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.InputShippingId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            vm.DestinationArea = "PACKING";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_GUDANGJADI()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = vm.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.InputShippingId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            vm.DestinationArea = "GUDANG JADI";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_GUDANGJADI_from_PENJUALAN()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAll())
                        .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            outSPPRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = vm.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.InputShippingId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            vm.DestinationArea = "GUDANG JADI";
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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());
            repoMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputShippingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Create_Buyer_DuplikateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model2 = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.DeliveryOrder.Id, ViewModel.DeliveryOrder.No, ViewModel.HasSalesInvoice, ViewModel.Type, ViewModel.ShippingCode, ViewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, s.DateOut, s.DeliveryOrder.Name, s.InventoryType)).ToList());


            var model = Model;
            model.SetDestinationArea("BUYER", "", "");

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            repoMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var vm = ViewModel;
            vm.DestinationArea = "BUYER";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_GudangJadi_Penjualan_DuplikateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var vm2 = new OutputShippingViewModel()
            {
                Area = "SHIPPING",
                BonNo = "s",
                Type = "OUT",
                Date = DateTimeOffset.UtcNow,
                Shift = "pas",
                HasNextAreaDocument = false,
                DestinationArea = "PENJUALAN",
                Group = "A",
                ShippingCode = "AS",
                InputShippingId = 1,
                HasSalesInvoice = false,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = 0,
                    No = ""
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
                                No = "sd"
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


            var model2 = new DyeingPrintingAreaOutputModel(vm2.Date, vm2.Area, vm2.Shift, vm2.BonNo, vm2.HasNextAreaDocument, vm2.DestinationArea,
                       vm2.Group, vm2.DeliveryOrder.Id, vm2.DeliveryOrder.No, vm2.HasSalesInvoice, vm2.Type, vm2.ShippingCode, vm2.ShippingProductionOrders.Select(s =>
                        new DyeingPrintingAreaOutputProductionOrderModel(vm2.Area, vm2.DestinationArea, vm2.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                            s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                            s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                            s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, s.DateOut, s.DeliveryOrder.Name, s.InventoryType)).ToList());





            var model = model2;
            model.SetDestinationArea("GUDANG JADI", "", "");

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            outSPPRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            repoMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.ShippingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Qty)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "remark", "zimmer", "grade", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 1, "a", "a", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1, "a", DateTimeOffset.Now));
                 


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
                 .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, true, "remark", "zimmer", "grade", "status", 0, 1, 1, 1, "nam", 1, "na", "1", 1, "a", "a", 1, "a", "a", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1, "a", DateTimeOffset.Now));
                 

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = service.GenerateExcel(ViewModel,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(EmptyDetailModel);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = service.GenerateExcel(vm,7);

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

            var result = service.GenerateExcel(ViewModelAdj,7);

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

            var result = service.GenerateExcel(vm,7);

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
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>();

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);


            var result = service.GenerateExcel(vm,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetInputShippingProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;

            var input = new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "NO", 1, "no", "tue", 1, "buyer", "c", "c", "c", "c", "c", 1, "c", 1, "c", false, 1, "unit", 1, 1, 1, "n", 1, "m", "1", "1", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1, 1, "a", "a", DateTimeOffset.Now, "a","LAMA");
            input.Id = 1;
            sppRepoMock.Setup(x => x.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                //new DyeingPrintingAreaInputProductionOrderModel("SHIPPING",1,"NO",1,"no","tue",1,"buyer","c","c","c","c","c",1,"c",1,"c",false,1,"unit",1,1, 1,"n",1,"m","1", "1","1",1,"a",1,"a",1,1,"a",false,1,1,"a",false,1,1,1,1,"a","a", DateTimeOffset.Now)
                input
            }.AsQueryable());

            var s = vm.ShippingProductionOrders.FirstOrDefault();
            var ViewModel1 = ViewModel;

            ViewModel1.DestinationArea = "BUYER";           
            outSPPRepoMock.Setup(x => x.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() {
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel1.DestinationArea, ViewModel.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer,
                        s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, 1 , s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                        s.MaterialWidth, s.CartNo, s.Remark, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                        s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth,  s.DateIn,s.DateOut, s.DeliveryOrder.Name, s.InventoryType )
                }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = service.GetInputShippingProductionOrdersByDeliveryOrder(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void ValidateVM()
        {
            var spp = new OutputShippingProductionOrderViewModel()
            {
                InputId = 1
            };

            Assert.NotEqual(0, spp.InputId);
            Assert.Null(spp.Packing);
            Assert.Equal(0, spp.QtyPacking);
            Assert.Null(spp.PackingType);
            Assert.Null(spp.Remark);
            Assert.Equal(0, spp.DyeingPrintingAreaInputProductionOrderId);
            Assert.Equal(0, spp.PackingLength);
            var date = DateTimeOffset.UtcNow;
            var index = new IndexViewModel()
            {
                Date = date,
                HasSalesInvoice = false,
                Group = "group"
            };
            Assert.False(index.HasSalesInvoice);
            Assert.NotNull(index.Group);
            Assert.Null(index.Area);
            Assert.Equal(0, index.Id);
            Assert.Null(index.BonNo);
            Assert.Equal(date, index.Date);
            Assert.Null(index.DestinationArea);
            Assert.False(index.HasNextAreaDocument);
            Assert.Null(index.Shift);
            Assert.Null(index.Type);
            Assert.Empty(index.ShippingProductionOrders);

            var adjvm = new AdjShippingProductionOrderViewModel();
            Assert.Null(adjvm.ProductionOrder);
            Assert.Null(adjvm.Material);
            Assert.Null(adjvm.MaterialConstruction);
            Assert.Null(adjvm.MaterialWidth);
            Assert.Null(adjvm.Construction);
            Assert.Null(adjvm.Unit);
            Assert.Null(adjvm.Grade);
            Assert.Null(adjvm.Packing);
            Assert.Equal(0, adjvm.BuyerId);
            Assert.Equal(0, adjvm.BuyerId);
            Assert.Null(adjvm.Buyer);
            Assert.Null(adjvm.Color);
            Assert.Null(adjvm.Motif);
            Assert.Null(adjvm.UomUnit);
            Assert.Null(adjvm.PackingType);
        }

        [Fact]
        public void Should_Success_ReadForSales()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelPJ }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);
            string filter = "{'BuyerId': " + ModelPJ.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().BuyerId + ", 'Area': '" + ModelPJ.Area + "' }";
            var result = service.ReadForSales(1, 25, filter, "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_UpdateHasSalesInvoice()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outSPPRepoMock.Setup(s => s.UpdateHasSalesInvoice(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.UpdateHasSalesInvoice(1, new OutputShippingUpdateSalesInvoiceViewModel() { HasSalesInvoice = true, ItemIds = new List<int>() { 1 } });

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete_NotPenjualan()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;
            model.SetDestinationArea("BUYER", "", "");

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var model = Model;

            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasSalesInvoice(true, "", "");

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(1));
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
            repoMock.Setup(s => s.DeleteShippingArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = Model;

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateShippingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasSalesInvoice(true, "", "");
            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateShippingArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ThrowsAsync(new Exception());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            vm.ShippingProductionOrders.Clear();
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Update(1, vm));
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

            foreach (var item in vm.ShippingProductionOrders)
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

            vm.ShippingProductionOrders.FirstOrDefault().Balance = -1;

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
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new OutputShippingViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    IsSave = true,
                    Qty = 0
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
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
                new OutputShippingProductionOrderViewModel()
                {
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ IN";
            vm.Id = 1;
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ OUT";
            vm.Id = 1;
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>()
            {
                new OutputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
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
            var detail = vm.ShippingProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.ShippingProductionOrders = new List<OutputShippingProductionOrderViewModel>() { detail };

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
                new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetDistinctAllProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
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
                new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

            var result = service.GetDistinctProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GetInputShippingProductionOrders_SPP()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false,1)
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSppRepoMock.Object).Object);

            var result = service.GetInputShippingProductionOrdersByProductionOrder(1);

            Assert.NotEmpty(result);
        }

        //[Fact]
        //public void Should_Success_GetInputTransitProductionOrders_SPP_All()
        //{
        //    var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
        //    {
        //        new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
        //    }.AsQueryable());
        //    var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object).Object);

        //    var result = service.GetInputShippingProductionOrdersByProductionOrder(0);

        //    Assert.NotEmpty(result);
        //}
    }
}
