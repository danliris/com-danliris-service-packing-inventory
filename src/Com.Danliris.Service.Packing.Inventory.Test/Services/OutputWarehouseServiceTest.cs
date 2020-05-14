using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
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
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionRepo);

            return spMock;
        }

        private OutputWarehouseViewModel ViewModel
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Area = "GUDANG JADI",
                    BonNo = "GJ.SP.20.001",
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
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
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

        //private OutputTransitViewModel ViewModelAval
        //{
        //    get
        //    {
        //        return new OutputTransitViewModel()
        //        {
        //            Area = "TRANSIT",
        //            BonNo = "s",
        //            Date = DateTimeOffset.UtcNow,
        //            Shift = "pas",
        //            HasNextAreaDocument = false,
        //            DestinationArea = "GUDANG AVAL",
        //            InputTransitId = 1,
        //            TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
        //            {
        //                new OutputTransitProductionOrderViewModel()
        //                {
        //                    Balance = 1,
        //                    Buyer = "s",
        //                    CartNo = "1",
        //                    Color = "red",
        //                    Construction = "sd",
        //                    Grade = "s",
        //                    Remark = "remar",
        //                    Status = "Ok",
        //                    Motif = "sd",
        //                    PackingInstruction = "d",
        //                    ProductionOrder = new ProductionOrder()
        //                    {
        //                        Code = "sd",
        //                        Id = 1,
        //                        Type = "sd",
        //                        No = "sd"
        //                    },
        //                    Unit = "s",
        //                    UomUnit = "d"
        //                }
        //            }
        //        };
        //    }
        //}

        //private OutputTransitViewModel ViewModelPC
        //{
        //    get
        //    {
        //        return new OutputTransitViewModel()
        //        {
        //            Area = "TRANSIT",
        //            BonNo = "s",
        //            Date = DateTimeOffset.UtcNow,
        //            Shift = "pas",
        //            HasNextAreaDocument = false,
        //            DestinationArea = "PACKING",
        //            InputTransitId = 1,
        //            TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
        //            {
        //                new OutputTransitProductionOrderViewModel()
        //                {
        //                    Balance = 1,
        //                    Buyer = "s",
        //                    CartNo = "1",
        //                    Color = "red",
        //                    Construction = "sd",
        //                    Grade = "s",
        //                    Remark = "remar",
        //                    Status = "Ok",
        //                    Motif = "sd",
        //                    PackingInstruction = "d",
        //                    ProductionOrder = new ProductionOrder()
        //                    {
        //                        Code = "sd",
        //                        Id = 1,
        //                        Type = "sd",
        //                        No = "sd"
        //                    },
        //                    Unit = "s",
        //                    UomUnit = "d"
        //                }
        //            }
        //        };
        //    }
        //}

        //private OutputTransitViewModel ViewModelIM
        //{
        //    get
        //    {
        //        return new OutputTransitViewModel()
        //        {
        //            Area = "TRANSIT",
        //            BonNo = "s",
        //            Date = DateTimeOffset.UtcNow,
        //            Shift = "pas",
        //            HasNextAreaDocument = false,
        //            DestinationArea = "INSPECTION MATERIAL",
        //            InputTransitId = 1,
        //            TransitProductionOrders = new List<OutputTransitProductionOrderViewModel>()
        //            {
        //                new OutputTransitProductionOrderViewModel()
        //                {
        //                    Balance = 1,
        //                    Buyer = "s",
        //                    CartNo = "1",
        //                    Color = "red",
        //                    Construction = "sd",
        //                    Grade = "s",
        //                    Remark = "remar",
        //                    Status = "Ok",
        //                    Motif = "sd",
        //                    PackingInstruction = "d",
        //                    ProductionOrder = new ProductionOrder()
        //                    {
        //                        Code = "sd",
        //                        Id = 1,
        //                        Type = "sd",
        //                        No = "sd"
        //                    },
        //                    Unit = "s",
        //                    UomUnit = "d"
        //                }
        //            }
        //        };
        //    }
        //}


        private DyeingPrintingAreaOutputModel OutputModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, 
                                                         ViewModel.Area, 
                                                         ViewModel.Shift, 
                                                         ViewModel.BonNo, 
                                                         ViewModel.HasNextAreaDocument, 
                                                         ViewModel.DestinationArea,
                                                         ViewModel.Group, 
                                                         ViewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, 
                                                                                                             ViewModel.DestinationArea, 
                                                                                                             ViewModel.HasNextAreaDocument, 
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
                                                                                                             s.Id)).ToList());
            }
        }

        //private DyeingPrintingAreaOutputModel ModelAval
        //{
        //    get
        //    {
        //        return new DyeingPrintingAreaOutputModel(ViewModelAval.Date, ViewModelAval.Area, ViewModelAval.Shift, ViewModelAval.BonNo, ViewModelAval.HasNextAreaDocument, ViewModelAval.DestinationArea,
        //           ViewModelAval.Group, ViewModelAval.TransitProductionOrders.Select(s =>
        //            new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
        //            s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id)).ToList());
        //    }
        //}

        //private DyeingPrintingAreaOutputModel ModelPC
        //{
        //    get
        //    {
        //        return new DyeingPrintingAreaOutputModel(ViewModelPC.Date, ViewModelPC.Area, ViewModelPC.Shift, ViewModelPC.BonNo, ViewModelPC.HasNextAreaDocument, ViewModelPC.DestinationArea,
        //           ViewModelPC.Group, ViewModelPC.TransitProductionOrders.Select(s =>
        //            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelPC.Area, ViewModelPC.DestinationArea, ViewModelPC.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
        //            s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id)).ToList());
        //    }
        //}

        //private DyeingPrintingAreaOutputModel ModelIM
        //{
        //    get
        //    {
        //        return new DyeingPrintingAreaOutputModel(ViewModelIM.Date, ViewModelIM.Area, ViewModelIM.Shift, ViewModelIM.BonNo, ViewModelIM.HasNextAreaDocument, ViewModelIM.DestinationArea,
        //           ViewModelIM.Group, ViewModelIM.TransitProductionOrders.Select(s =>
        //            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area, ViewModelIM.DestinationArea, ViewModelPC.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
        //            s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id)).ToList());
        //    }
        //}

        private DyeingPrintingAreaOutputModel EmptyOutputProductionOrderModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, 
                                                         ViewModel.Area, 
                                                         ViewModel.Shift, 
                                                         ViewModel.BonNo, 
                                                         ViewModel.HasNextAreaDocument, 
                                                         ViewModel.DestinationArea,
                                                         ViewModel.Group, 
                                                         new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var item = ViewModel.WarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, 
                                                        ViewModel.Area, 
                                                        "IN", 
                                                        ViewModel.InputWarehouseId, 
                                                        ViewModel.BonNo, 
                                                        item.ProductionOrder.Id,
                                                        item.ProductionOrder.No, 
                                                        item.CartNo, 
                                                        item.Buyer, 
                                                        item.Construction,
                                                        item.Unit, 
                                                        item.Color,
                                                        item.Motif,
                                                        item.UomUnit, 
                                                        item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        //[Fact]
        //public async Task Should_Success_Create_DuplicateShift()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
        //        .ReturnsAsync(1);

        //    inputRepoMock.Setup(s => s.GetDbSet())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);
        //    var item = ViewModel.WarehousesProductionOrders.FirstOrDefault();
        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputWarehouseId, ViewModel.BonNo, item.ProductionOrder.Id,
        //             item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public async Task Should_Success_Create2()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.WarehousesProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        //[Fact]
        //public async Task Should_Success_CreateAVAL()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
        //        .ReturnsAsync(1);

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelAval }.AsQueryable());

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);
        //    var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             new DyeingPrintingAreaSummaryModel(ViewModelAval.Date, ViewModelAval.Area, "IN", ViewModelAval.InputTransitId, ViewModelAval.BonNo, item.ProductionOrder.Id,
        //             item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModelAval);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_CreatePacking()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
        //        .ReturnsAsync(1);

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelPC }.AsQueryable());

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);
        //    var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.InputTransitId, ViewModelPC.BonNo, item.ProductionOrder.Id,
        //             item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModelPC);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_CreateIM()
        //{
        //    var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
        //    var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
        //    var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
        //    var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

        //    inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
        //        .ReturnsAsync(1);

        //    inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
        //        .Returns(new List<DyeingPrintingAreaOutputModel>() { ModelIM }.AsQueryable());

        //    movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
        //         .ReturnsAsync(1);
        //    var item = ViewModelAval.TransitProductionOrders.FirstOrDefault();
        //    summaryRepoMock.Setup(s => s.ReadAll())
        //         .Returns(new List<DyeingPrintingAreaSummaryModel>() {
        //             new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.InputTransitId, ViewModelIM.BonNo, item.ProductionOrder.Id,
        //             item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
        //         }.AsQueryable());

        //    summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
        //         .ReturnsAsync(1);

        //    inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(inputRepoMock.Object,
        //                                                inputProductionOrderRepoMock.Object,
        //                                                movementRepoMock.Object,
        //                                                summaryRepoMock.Object,
        //                                                outputProductionOrderRepoMock.Object).Object);

        //    var result = await service.Create(ViewModelIM);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public void Should_Success_Read()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
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
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(EmptyOutputProductionOrderModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
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
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false)
            }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetInputWarehouseProductionOrders();

            Assert.NotEmpty(result);
        }
    }
}
