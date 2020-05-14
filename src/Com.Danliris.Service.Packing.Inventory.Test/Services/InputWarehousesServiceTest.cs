using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses;
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
    public class InputWarehousesServiceTest
    {
        public InputWarehousesService GetService(IServiceProvider serviceProvider)
        {
            return new InputWarehousesService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository inputRepository,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputProductionOrderRepo,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo, 
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaOutputProductionOrderRepository outputProductionOrderRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepository);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionOrderRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionOrderRepo);

            return spMock;
        }

        private InputWarehousesViewModel ViewModel
        {
            get
            {
                return new InputWarehousesViewModel()
                {
                    Area = "INSPECTION MATERIAL",
                    BonNo = "GJ.20.0001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 195,
                    Group = "A",
                    WarehousesProductionOrders = new List<InputWarehousesProductionOrdersViewModel>
                    {
                        new InputWarehousesProductionOrdersViewModel()
                        {
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            ProductionOrderNo = "F/2020/0009",
                            CartNo = "9",
                            PackingInstruction = "d",
                            Construction = "Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100",
                            Unit = "DYEING",
                            Buyer = "ERWAN KURNIADI",
                            Color = "Grey",
                            //Motif = "",
                            UomUnit = "MTR",
                            Balance = 1,
                            HasOutputDocument = false,
                            IsChecked = false,
                            Grade = "A",
                            //Remark = "",
                            //Status = "",
                            //Material = "",
                            MtrLength = 0,
                            YdsLength = 0,
                            PackagingUnit ="ROLL",
                            PackagingQty = 10,
                            PackagingType ="WHITE",
                            QtyOrder = 2000,
                            OutputId = 195
                            //InputId = 0
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel InputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date,
                                                        ViewModel.Area,
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        ViewModel.Group,
                                                        ViewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty)).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ExistingInputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date,
                                                        "GUDANG JADI",
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        ViewModel.Group,
                                                        ViewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel OutputModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, 
                                                         ViewModel.Area, 
                                                         ViewModel.Shift, 
                                                         ViewModel.BonNo, 
                                                         true, 
                                                         "GUDANG JADI", 
                                                         ViewModel.Group, 
                                                         ViewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, 
                                                                                                             "GUDANG JADI", 
                                                                                                             true, 
                                                                                                             s.ProductionOrder.Id, 
                                                                                                             s.ProductionOrder.No, 
                                                                                                             s.ProductionOrder.Type, 
                                                                                                             s.PackingInstruction, 
                                                                                                             s.CartNo, 
                                                                                                             s.Buyer, 
                                                                                                             s.Construction,
                                                                                                             s.Unit, 
                                                                                                             s.Color, 
                                                                                                             s.Motif, 
                                                                                                             s.UomUnit, 
                                                                                                             s.Remark, 
                                                                                                             s.Balance, 
                                                                                                             s.Status, 
                                                                                                             s.ProductionOrder.Code, 
                                                                                                             s.ProductionOrder.OrderQuantity, 
                                                                                                             s.PackagingType, 
                                                                                                             s.PackagingQty, 
                                                                                                             s.PackagingUnit)).ToList());
            }
        }

        private DyeingPrintingAreaOutputProductionOrderModel OutputProductionOrderModel
        {
            get
            {
                return new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area,
                                                                        "GUDANG JADI",
                                                                        true,
                                                                        It.IsAny<long>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<double>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<double>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<decimal>(),
                                                                        It.IsAny<string>());
            }
        }

        private DyeingPrintingAreaSummaryModel SummaryModel
        {
            get
            {
                return new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                          ViewModel.Area,
                                                          ViewModel.Shift,
                                                          1,
                                                          ViewModel.BonNo,
                                                          12,
                                                          "sd",
                                                          "io1",
                                                          "rest",
                                                          "asdf",
                                                          "asdfas",
                                                          "dafsd",
                                                          "asdfsd",
                                                          "asdfsd",
                                                          123);
                //{
                //    Id = 10
                //};
            }
        }

        [Fact]
        public async Task Should_Success_InsertNewWarehouse()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var item = ViewModel.WarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                        ViewModel.Area,
                                                        "IN",
                                                        ViewModel.OutputId,
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

            //summaryRepoMock.Setup(s => s.ReadAll())
            //     .Returns(new List<DyeingPrintingAreaSummaryModel>() { SummaryModel }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagParentOnlyAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            //inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            //summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
            //     .ReturnsAsync(1);

            //outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateExistingWarehouse()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ExistingInputModel }.AsQueryable());

            //inputRepoMock.Setup(o => o.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var item = ViewModel.WarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                        ViewModel.Area,
                                                        "IN",
                                                        ViewModel.OutputId,
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

            inputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), SummaryModel))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(item.Id, true))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(InputModel);

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
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaInputModel));

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
        public void Should_Success_ReadProductionOrders()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM", "GUDANG JADI", false, 1, "a", "e", 1,"rr", "1", "as", "test", "unit", "color", "motif", "mtr", "rem", "a", "a", 1,1)
            }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetOutputPreWarehouseProductionOrders();

            Assert.NotEmpty(result);
        }
    }
}
