using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Service
{
    public class InspectionMaterialServiceTest
    {
        public InspectionMaterialService GetService(IServiceProvider serviceProvider)
        {
            return new InspectionMaterialService(serviceProvider);
        }

        private DyeingPrintingAreaMovementModel Model
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, ViewModel.BonNo, ViewModel.Date, ViewModel.Shift, ViewModel.ProductionOrder.Id,
                    ViewModel.ProductionOrder.Code, ViewModel.ProductionOrder.No, ViewModel.ProductionOrderQuantity, ViewModel.ProductionOrder.Type,
                    ViewModel.Buyer, ViewModel.PackingInstruction, ViewModel.CartNo, ViewModel.Material.Id, ViewModel.Material.Code, ViewModel.Material.Name,
                    ViewModel.MaterialConstruction.Id, ViewModel.MaterialConstruction.Code, ViewModel.MaterialConstruction.Name, ViewModel.MaterialWidth,
                    ViewModel.Unit.Id, ViewModel.Unit.Code, ViewModel.Unit.Name, ViewModel.Color, ViewModel.Motif, ViewModel.Mutation, ViewModel.Length,
                    ViewModel.UOMUnit, ViewModel.Balance, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, AreaEnum.IM)
                    });
            }
        }

        private DyeingPrintingAreaMovementModel ModelYard
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(ViewModel.Area, ViewModel.BonNo, ViewModel.Date, ViewModel.Shift, ViewModel.ProductionOrder.Id,
                    ViewModel.ProductionOrder.Code, ViewModel.ProductionOrder.No, ViewModel.ProductionOrderQuantity, ViewModel.ProductionOrder.Type,
                    ViewModel.Buyer, ViewModel.PackingInstruction, ViewModel.CartNo, ViewModel.Material.Id, ViewModel.Material.Code, ViewModel.Material.Name,
                    ViewModel.MaterialConstruction.Id, ViewModel.MaterialConstruction.Code, ViewModel.MaterialConstruction.Name, ViewModel.MaterialWidth,
                    ViewModel.Unit.Id, ViewModel.Unit.Code, ViewModel.Unit.Name, ViewModel.Color, ViewModel.Motif, ViewModel.Mutation, ViewModel.Length,
                    "YDS", ViewModel.Balance, new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, AreaEnum.IM)
                    });
            }
        }

        private InspectionMaterialViewModel ViewModel
        {
            get
            {
                return new InspectionMaterialViewModel()
                {
                    Id = 1,
                    Area = "area",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "shift",
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = 1,
                        No = "no"
                    },
                    CartNo = "no",
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
                    MaterialWidth = "width",
                    Unit = new Unit()
                    {
                        Id = 1,
                        Name = "name"
                    },

                    Color = "color",
                    Mutation = "mutation",
                    UOMUnit = "MTR"
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

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter()).Returns(new List<DyeingPrintingAreaMovementModel>() { new DyeingPrintingAreaMovementModel(){
                CreatedUtc = DateTime.UtcNow
            } }.AsQueryable());

            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
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
        public async Task Should_Success_ReadByIdYDS()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelYard);

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
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaMovementModel>()))
                .ReturnsAsync(1);


            var historyRepo = new Mock<IDyeingPrintingAreaMovementHistoryRepository>();
            historyRepo.Setup(s => s.UpdateAsyncFromParent(It.IsAny<int>(), It.IsAny<AreaEnum>(), It.IsAny<DateTimeOffset>(), It.IsAny<string>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, historyRepo.Object).Object);

            var result = await service.Update(1, ViewModel);

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
            Assert.Equal(model.Buyer, data.Buyer);
            Assert.Equal(model.CartNo, data.CartNo);
            Assert.Equal(model.Color, data.Color);
            Assert.Equal(model.Date, data.Date);
            Assert.Equal(model.Id, data.Id);
            Assert.Equal(model.MaterialConstructionName, data.MaterialConstructionName);
            Assert.Equal(model.MaterialName, data.MaterialName);
            Assert.Equal(model.MaterialWidth, data.MaterialWidth);
            Assert.Equal(model.MeterLength, data.MeterLength);
            Assert.Equal(model.Motif, data.Motif);
            Assert.Equal(model.Mutation, data.Mutation);
            Assert.Equal(model.ProductionOrderId, data.ProductionOrderId);
            Assert.Equal(model.ProductionOrderNo, data.ProductionOrderNo);
            Assert.Equal(model.ProductionOrderQuantity, data.ProductionOrderQuantity);
            Assert.Equal(model.ProductionOrderType, data.ProductionOrderType);
            Assert.Equal(model.Shift, data.Shift);
            Assert.Equal(model.Status, data.Status);
            Assert.Equal(model.UnitName, data.UnitName);
            Assert.Equal(model.UOMUnit, data.UomUnit);
            Assert.Equal(model.YardsLength, data.YardsLength);
        }
    }
}
