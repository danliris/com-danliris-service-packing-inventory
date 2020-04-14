using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Packing;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Service
{
    public class PackingAreaNoteServiceTest
    {
        public PackingAreaNoteService GetService(IServiceProvider serviceProvider)
        {
            return new PackingAreaNoteService(serviceProvider);
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
                        new DyeingPrintingAreaMovementHistoryModel(ViewModel.Date, ViewModel.Area,"shift", AreaEnum.PACK)
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

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaMovementRepository service)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(service);

            return spMock;
        }

        [Fact]
        public void Should_Success_GetReport()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReport(Model.Date.ToOffset(new TimeSpan(7, 0, 0)), Model.Area, Model.Shift, Model.Mutation, 7);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(Model.Date.ToOffset(new TimeSpan(7, 0, 0)), Model.Area, Model.Shift, Model.Mutation, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(Model.Date.ToOffset(new TimeSpan(7, 0, 0)).AddDays(3), Model.Area, Model.Shift, Model.Mutation, 7);

            Assert.NotNull(result);
        }
    }
}
