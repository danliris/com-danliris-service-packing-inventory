using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.TransitInput;
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
    public class TransitInputServiceTest
    {
        public TransitInputService GetService(IServiceProvider serviceProvider)
        {
            return new TransitInputService(serviceProvider);
        }

        private DyeingPrintingAreaMovementModel Model
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, ViewModel.BonNo, DateTimeOffset.UtcNow, ViewModel.Shift, 0,
                    null, ViewModel.ProductionOrderNo, 0, null,
                    null, null, ViewModel.CartNo, 0, null, ViewModel.Material,
                    0, null, ViewModel.MaterialConstruction, ViewModel.MaterialWidth,
                    0, null, ViewModel.Unit, ViewModel.Color, ViewModel.Motif, null,0,
                    ViewModel.UOMUnit, ViewModel.Balance, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, ViewModel.Area, ViewModel.Shift, AreaEnum.TRANSIT)
                    });
            }
        }

        private TransitInputViewModel ViewModel
        {
            get
            {
                return new TransitInputViewModel()
                {
                    Id = 1,
                    Area = "area",
                    Balance = 100,
                    Shift = "shift",
                    BonNo = "bonNo",
                    CartNo = "no",
                    Color = "cp",
                    InspectionAreaId = 1,
                    Material = "mater",
                    MaterialConstruction = "s",
                    MaterialWidth = "1",
                    Motif = "mo",
                    ProductionOrderNo = "no",
                    Remark = "ream",
                    Unit = "unit",
                    UOMUnit = "kg"
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

            repoMock.Setup(s => s.InsertFromTransitAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(),
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
            repoMock.Setup(s => s.DeleteFromTransitAsync(It.IsAny<int>()))
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
            Assert.Equal(model.Balance, data.Balance);
            Assert.Equal(model.BonNo, data.BonNo);
            Assert.Equal(model.CartNo, data.CartNo);
            Assert.Equal(model.Color, data.Color);
            Assert.Equal(model.Date, data.Date);
            Assert.Equal(model.Id, data.Id);
            Assert.Equal(model.MaterialConstructionName, data.MaterialConstructionName);
            Assert.Equal(model.MaterialName, data.MaterialName);
            Assert.Equal(model.MaterialWidth, data.MaterialWidth);
            Assert.Equal(model.MeterLength, data.MeterLength);
            Assert.Equal(model.Motif, data.Motif);
            Assert.Equal(model.ProductionOrderId, data.ProductionOrderId);
            Assert.Equal(model.ProductionOrderNo, data.ProductionOrderNo);
            Assert.Equal(model.Remark, data.Remark);
            Assert.Equal(model.Shift, data.Shift);
            Assert.Equal(model.UnitName, data.UnitName);
            Assert.Equal(model.YardsLength, data.YardsLength);
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
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.UpdateFromTransitAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
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
    }
}
