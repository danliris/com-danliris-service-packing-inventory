using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class InputTransitServiceTest
    {
        public InputTransitService GetService(IServiceProvider serviceProvider)
        {
            return new InputTransitService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaOutputRepository outputRepo, IDyeingPrintingAreaOutputProductionOrderRepository outSPPRepo,
           IDyeingPrintingAreaInputProductionOrderRepository inSPPRepo)
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
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outSPPRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inSPPRepo);

            return spMock;
        }

        private InputTransitViewModel ViewModel
        {
            get
            {
                return new InputTransitViewModel()
                {
                    Area = "TRANSIT",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    OutputId = 1,
                    Group = "A",
                    TransitProductionOrders = new List<InputTransitProductionOrderViewModel>()
                    {
                        new InputTransitProductionOrderViewModel()
                        {
                            PackingType = "a",
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            Id = 1,
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
                            Unit = "s",
                            UomUnit = "d",
                            OutputId = 0,
                            DyeingPrintingAreaInputProductionOrderId =1
                        }
                    }
                };
            }
        }

        private InputTransitViewModel ViewModelIM
        {
            get
            {
                return new InputTransitViewModel()
                {
                    Area = "INSPECTION MATERIAL",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    OutputId = 1,
                    Group = "A",
                    TransitProductionOrders = new List<InputTransitProductionOrderViewModel>()
                    {
                        new InputTransitProductionOrderViewModel()
                        {
                            PackingType = "a",
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
                            Unit = "s",
                            UomUnit = "d",
                            OutputId = 0,
                            DyeingPrintingAreaInputProductionOrderId =1
                        }
                    }
                };
            }
        }

        private InputTransitViewModel ViewModelPC
        {
            get
            {
                return new InputTransitViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    OutputId = 1,
                    Group = "A",
                    TransitProductionOrders = new List<InputTransitProductionOrderViewModel>()
                    {
                        new InputTransitProductionOrderViewModel()
                        {
                            PackingType = "a",
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Area = "PACKING",
                            Grade = "s",
                            HasOutputDocument = false,
                            IsChecked = false,
                            Motif = "sd",
                            PackingInstruction = "d",
                            Remark = "RE",
                            DyeingPrintingAreaOutputProductionOrderId = 1,
                            Status = "s",
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
                            MaterialWidth = "1",
                            Unit = "s",
                            UomUnit = "d",
                            OutputId = 0,
                            DyeingPrintingAreaInputProductionOrderId =1
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel Model
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                    s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.QtyPacking, s.PackingUnit, s.PackingType)
                    {
                        Id = s.Id
                    }).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ModelIM
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelIM.Date, ViewModelIM.Area, ViewModelIM.Shift, ViewModelIM.BonNo, ViewModelIM.Group, ViewModelIM.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                    s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.QtyPacking, s.PackingUnit, s.PackingType)).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ModelPC
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelPC.Date, ViewModelPC.Area, ViewModelPC.Shift, ViewModelPC.BonNo, ViewModelPC.Group, ViewModelPC.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelPC.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId,
                    s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.QtyPacking, s.PackingUnit, s.PackingType)).ToList());
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Previous_Summary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create2_DuplicateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);
            var modelItem = Model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaSummaryModel>() { new DyeingPrintingAreaSummaryModel(Model.Date, Model.Area, "IN",Model.Id, Model.BonNo, modelItem.ProductionOrderId,
                modelItem.ProductionOrderNo, modelItem.CartNo, modelItem.Buyer, modelItem.Construction, modelItem.Unit, modelItem.Color, modelItem.Motif, modelItem.UomUnit, modelItem.Balance)}.AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModel.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.OutputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaInputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_ReadPreTransit()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "INSPECTION MATERIAL","pagi","no",false,
                    "TRANSIT", "A",new List<DyeingPrintingAreaOutputProductionOrderModel>(){
                        new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT", false,1,"no","t",1,"1","1","sd","cs","sd","as","sd","asd","asd","sd","sd",1,1,1,1,"a",1,"a","1","",1,"a","a")
                    }) }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = service.ReadOutputPreTransit(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GetOutputPreTransitProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            outputSPPRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM", "TRANSIT", false, 1, "a", "e", 1,"rr", "1", "as", "test", "unit", "color", "motif", "mtr", "rem", "a", "a", 1, 1,1,1,"a",1,"a","1","",1,"a","a")
            }.AsQueryable());
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);
            var result = service.GetOutputPreTransitProductionOrders();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM_Previous_Summary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", item.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_PC()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelPC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_PC_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

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

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelPC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_GJ()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var vm = ViewModelPC;
            vm.Area = "GUDANG JADI";
            foreach (var spp in vm.TransitProductionOrders)
            {
                spp.Area = "GUDANG JADI";
            }

            var model = ModelPC;
            model.SetArea("GUDANG JADI", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("GUDANG JADI", "", "");
            }
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_GJ_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);
            var vm = ViewModelPC;
            vm.Area = "GUDANG JADI";
            foreach (var spp in vm.TransitProductionOrders)
            {
                spp.Area = "GUDANG JADI";
            }

            var model = ModelPC;
            model.SetArea("GUDANG JADI", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("GUDANG JADI", "", "");
            }
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_SP()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var vm = ViewModelPC;
            vm.Area = "SHIPPING";
            foreach (var spp in vm.TransitProductionOrders)
            {
                spp.Area = "SHIPPING";
            }

            var model = ModelPC;
            model.SetArea("SHIPPING", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("SHIPPING", "", "");
            }
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_SP_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);
            var vm = ViewModelPC;
            vm.Area = "SHIPPING";
            foreach (var spp in vm.TransitProductionOrders)
            {
                spp.Area = "SHIPPING";
            }

            var model = ModelPC;
            model.SetArea("SHIPPING", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("SHIPPING", "", "");
            }
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Reject_IM_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM_Duplicate_Shift_Previous_Summary()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.TransitProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", item.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputSPPRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object);

            var result = await service.Reject(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Validate_VM()
        {
            var outputPreTransitSPP = new OutputPreTransitProductionOrderViewModel()
            {

            };

            Assert.NotNull(outputPreTransitSPP);
            Assert.Null(outputPreTransitSPP.ProductionOrder);
            Assert.Null(outputPreTransitSPP.CartNo);
            Assert.Null(outputPreTransitSPP.PackingInstruction);
            Assert.Null(outputPreTransitSPP.Construction);
            Assert.Null(outputPreTransitSPP.Unit);
            Assert.Null(outputPreTransitSPP.Buyer);
            Assert.Null(outputPreTransitSPP.Color);
            Assert.Null(outputPreTransitSPP.Motif);
            Assert.Null(outputPreTransitSPP.UomUnit);
            Assert.Null(outputPreTransitSPP.Remark);
            Assert.Null(outputPreTransitSPP.Grade);
            Assert.Null(outputPreTransitSPP.Status);
            Assert.Equal(0, outputPreTransitSPP.Balance);
            Assert.Equal(0, outputPreTransitSPP.OutputId);
            Assert.Null(outputPreTransitSPP.Material);
            Assert.Null(outputPreTransitSPP.MaterialConstruction);
            Assert.Null(outputPreTransitSPP.MaterialWidth);
            Assert.Null(outputPreTransitSPP.Area);
            Assert.Equal(0, outputPreTransitSPP.BuyerId);
            Assert.Null(outputPreTransitSPP.PackingType);
            Assert.Equal(0, outputPreTransitSPP.DyeingPrintingAreaInputProductionOrderId);

            var inputSPP = new InputTransitProductionOrderViewModel();
            Assert.False(inputSPP.IsChecked);
            Assert.Equal(0, inputSPP.BalanceRemains);
            Assert.Equal(0, inputSPP.PreviousBalance);
            Assert.Equal(0, inputSPP.InputId);
            Assert.Null(inputSPP.PackingType);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new InputTransitViewModel();
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
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            repoMock.Setup(s => s.DeleteTransitArea(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(0, "", "");
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteTransitArea(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(1));

        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.TransitProductionOrders.FirstOrDefault().Balance = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(1, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.TransitProductionOrders = new List<InputTransitProductionOrderViewModel>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Update(1, vm));
        }

        [Fact]
        public async Task Should_Success_Update_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = Model;
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalanceRemains(2, "", "");
            model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.TransitProductionOrders = new List<InputTransitProductionOrderViewModel>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateTransitArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);

            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
                outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
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
            var outputSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inSPPRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object,
               outputSPPRepoMock.Object, inSPPRepoMock.Object).Object;
            var service = GetService(serviceProvider);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }
    }
}
