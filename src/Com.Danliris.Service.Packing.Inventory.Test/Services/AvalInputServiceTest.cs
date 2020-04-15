using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput;
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
    public class AvalInputServiceTest
    {
        public AvalInputService GetService(IServiceProvider serviceProvider)
        {
            return new AvalInputService(serviceProvider);
        }

        private DyeingPrintingAreaMovementModel Model
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, null, DateTimeOffset.UtcNow, ViewModel.Shift, 0,
                    null, null, ViewModel.ProductionOrderQuantity, null,
                    null, null, null, 0, null, null,
                    0, null, null, null,
                    0, null, null, null, null, null,0,
                    ViewModel.UOMUnit, 0, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, ViewModel.Area, ViewModel.Shift, AreaEnum.AVAL)
                    });
            }
        }

        private AvalInputViewModel ViewModel
        {
            get
            {
                return new AvalInputViewModel()
                {
                    Id = 1,
                    Area = "AVAL",
                    Shift = "PAGI",
                    UOMUnit = "MTR",
                    ProductionOrderQuantity = 2500,
                    QtyKg = 2
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

            repoMock.Setup(s => s.InsertFromAvalAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<DyeingPrintingAreaMovementHistoryModel>())).ReturnsAsync(1);

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.UpdateFromAvalAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), /*It.IsAny<string>(),*/ It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);


            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            historyRepo.Setup(s => s.UpdateAsyncFromParent(It.IsAny<int>(), It.IsAny<AreaEnum>(), It.IsAny<DateTimeOffset>(), It.IsAny<string>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.DeleteFromAvalAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
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
            Assert.Equal(model.Id, data.Id);
            Assert.Equal(model.Date, data.Date);
            Assert.Equal(model.BonNo, data.BonNo);
            Assert.Equal(model.Shift, data.Shift);
            Assert.Equal(model.CartNo, data.CartNo);
            Assert.Equal(model.UnitId, data.UnitId);
            Assert.Equal(model.UnitCode, data.UnitCode);
            Assert.Equal(model.UnitName, data.UnitName);
            Assert.Equal(model.Area, data.Area);
            Assert.Equal(model.ProductionOrderType, data.ProductionOrderType);
            Assert.Equal(model.UOMUnit, data.UOMUnit);
            Assert.Equal(model.ProductionOrderQuantity, data.ProductionOrderQuantity);
            Assert.Equal(model.QtyKg, data.QtyKg);
        }
    }
}
