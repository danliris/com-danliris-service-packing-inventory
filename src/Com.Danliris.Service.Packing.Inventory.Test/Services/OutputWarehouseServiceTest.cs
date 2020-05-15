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

        private OutputWarehouseViewModel ViewModelToShipping
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
                                                                                                             s.Id)).ToList());
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
                                                                                                             s.Id)).ToList());
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
                                                          ViewModelToShipping.InputWarehouseId,
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
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false)
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
    }
}
