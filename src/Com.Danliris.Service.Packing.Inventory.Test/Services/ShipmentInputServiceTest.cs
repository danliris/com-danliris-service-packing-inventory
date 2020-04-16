using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
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
    public class ShipmentInputServiceTest
    {
        public ShipmentInputService GetService(IServiceProvider serviceProvider)
        {
            return new ShipmentInputService(serviceProvider);
        }
        private DyeingPrintingAreaMovementModel Model
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, ViewModel.BonNo, DateTimeOffset.UtcNow, ViewModel.Shift, 0,
                    null, ViewModel.ProductionOrderNo, 0, null,
                    ViewModel.BuyerName, null, "", 0, null, "1",
                    0, null, "1", "1",
                    0, null, "", ViewModel.Color, ViewModel.Motif, null, 12,
                    ViewModel.UomUnit, 0, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, ViewModel.Area, ViewModel.Shift, AreaEnum.SHIP)
                    });
            }
        }

        private DyeingPrintingAreaMovementModel ModelPreShip
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, ViewModel.BonNo, DateTimeOffset.UtcNow, ViewModel.Shift, 0,
                    null, ViewModel.ProductionOrderNo, 0, null,
                    ViewModel.BuyerName, null, "", 0, null, "1",
                    0, null, "1", "1",
                    0, null, "", ViewModel.Color, ViewModel.Motif, null, 12,
                    ViewModel.UomUnit, 0, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, ViewModel.Area, ViewModel.Shift, AreaEnum.GUDANGJADI)
                    });
            }
        }

        private ShipmentInputViewModel ViewModel
        {
            get
            {
                return new ShipmentInputViewModel()
                {
                    Shift = "shif",
                    Active = true,
                    Area = "shipment",
                    BonNo = "no",
                    BuyerName = "ma,e",
                    Color = "red",
                    Construction = "1 / 1 / 1",
                    DeliveryOrderSales = new DeliveryOrderSales()
                    {
                        Id = 1,
                        No = "s"
                    },
                    Id = 1,
                    Grade = "a",
                    Motif = "a",
                    PackingBalance = 1,
                    PackingQTY = 1,
                    PackingUOM = "s",
                    PreShipmentAreaId = 1,
                    ProductionOrderNo = "as",
                    UomUnit = "as"

                };
            }
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaMovementRepository service, IDyeingPrintingAreaMovementHistoryRepository historyService)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(service);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementHistoryRepository)))
                .Returns(historyService);

            return spMock;
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.InsertFromShipmentAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<long>(), It.IsAny<string>(),
                It.IsAny<DyeingPrintingAreaMovementHistoryModel>())).ReturnsAsync(1);

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.DeleteFromShipmentAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var model = Model;
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { model }.AsQueryable());

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);

            var data = result.Data.First();
            Assert.Equal(model.Area, data.Area);
            Assert.Equal(model.BonNo, data.BonNo);
            Assert.Equal(model.Buyer, data.BuyerName);
            Assert.Equal(model.Construction, data.Construction);
            Assert.Equal(model.Color, data.Color);
            Assert.Equal(model.DeliveryOrderSalesNo, data.DeliveryOrderSalesNo);
            Assert.Equal(model.Date, data.Date);
            Assert.Equal(model.Grade, data.Grade);
            Assert.Equal(model.Id, data.Id);
            Assert.Equal(model.MeterLength, data.MeterLength);
            Assert.Equal(model.Motif, data.Motif);
            Assert.Equal(model.ProductionOrderNo, data.ProductionOrderNo);
            Assert.Equal(model.YardsLength, data.YardsLength);
            Assert.Equal(model.UOMUnit, data.UomUnit);
            Assert.Equal(0, data.PackingBalance);
            Assert.Equal(0, data.PackingQTY);
            Assert.Null(data.PackingUom);
            data.PackingBalance = 0;
            data.PackingQTY = 0;
            data.PackingUom = "e";
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
            Assert.Null(result.Grade);
            Assert.Null(result.PackingUOM);
            Assert.Equal(0, result.PackingQTY);
            Assert.Equal(0, result.PackingBalance);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.UpdateFromShipmentAsync(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(1);


            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            historyRepo.Setup(s => s.UpdateAsyncFromParent(It.IsAny<int>(), It.IsAny<AreaEnum>(), It.IsAny<DateTimeOffset>(), It.IsAny<string>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(DyeingPrintingAreaMovementModel));

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_LoaderPreShipment()
        {
            var model = ModelPreShip;
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { model }.AsQueryable());

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = service.LoaderPreShipmentData(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);

            var data = result.Data.First();
            Assert.Equal(model.Area, data.Area);
            Assert.Equal(model.BonNo, data.BonNo);
            Assert.Equal(model.Buyer, data.BuyerName);
            Assert.Equal(model.Construction, data.Construction);
            Assert.Equal(model.Color, data.Color);
            Assert.Equal(model.Grade, data.Grade);
            Assert.Equal(model.Id, data.Id);
            Assert.Equal(model.Motif, data.Motif);
            Assert.Equal(model.ProductionOrderNo, data.ProductionOrderNo);
            Assert.Equal(model.UOMUnit, data.UomUnit);
            Assert.Equal(0, data.PackingBalance);
            Assert.Equal(0, data.PackingQTY);
            Assert.Equal(model.Shift, data.Shift);
            Assert.Equal(model.UnitName, data.Unit);
            Assert.Null(data.PackingUOM);
            data.PackingBalance = 0;
            data.PackingQTY = 0;
            data.PackingUOM = "";
        }
    }


}
