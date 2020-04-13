using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Service
{
    public class FabricQualityControlServiceTest
    {
        public FabricQualityControlService GetService(IServiceProvider serviceProvider)
        {
            return new FabricQualityControlService(serviceProvider);
        }

        private FabricQualityControlViewModel ViewModel
        {
            get
            {
                return new FabricQualityControlViewModel()
                {
                    Buyer = "buyer",
                    CartNo = "cartno",
                    Code = "code",
                    Color = "color",
                    Construction = "construction",
                    DateIm = DateTimeOffset.UtcNow,
                    DyeingPrintingAreaMovementBonNo = "no",
                    Group = "group",
                    IsUsed = false,
                    MachineNoIm = "no",
                    OperatorIm = "np",
                    OrderQuantity = 1,
                    PackingInstruction = "s",
                    PointLimit = 1,
                    PointSystem = 10,
                    ProductionOrderNo = "np",
                    ProductionOrderType = "type",
                    ShiftIm = "shif",
                    Uom = "mtr",
                    FabricGradeTests = new List<FabricGradeTestViewModel>()
                    {
                        new FabricGradeTestViewModel()
                        {
                            AvalLength = 1,
                            PointSystem =10,
                            FabricGradeTest = 1,
                            FinalArea = 1,
                            FinalLength = 1,
                            FinalGradeTest = 1,
                            FinalScore = 1,
                            Grade = "a",
                            InitLength = 2,
                            PcsNo = "np",
                            PointLimit = 1,
                            SampleLength = 1,
                            Score =1,
                            Type = "t",
                            Width = 1,
                            Criteria = new List<CriteriaViewModel>()
                            {
                                new CriteriaViewModel()
                                {
                                    Code = "c",
                                    Group = "a",
                                    Score = new Score()
                                    {
                                        A = 1,
                                        B = 1,
                                        C = 1,
                                        D = 1
                                    },
                                    Index = 1,
                                    Name = "na"
                                }
                            }
                        }
                    }
                };
            }
        }

        private FabricQualityControlModel Model
        {
            get
            {
                return new FabricQualityControlModel(ViewModel.Code, ViewModel.DateIm.GetValueOrDefault(), ViewModel.Group, ViewModel.IsUsed.GetValueOrDefault(), ViewModel.DyeingPrintingAreaMovementId,
                ViewModel.DyeingPrintingAreaMovementBonNo, ViewModel.ProductionOrderNo,
                ViewModel.MachineNoIm, ViewModel.OperatorIm, ViewModel.PointLimit.GetValueOrDefault(), ViewModel.PointSystem.GetValueOrDefault(),
                ViewModel.FabricGradeTests.Select((s, i) =>
                    new FabricGradeTestModel(s.AvalLength.GetValueOrDefault(), s.FabricGradeTest.GetValueOrDefault(), s.FinalArea.GetValueOrDefault(),
                    s.FinalGradeTest.GetValueOrDefault(), s.FinalLength.GetValueOrDefault(), s.FinalScore.GetValueOrDefault(), s.Grade, s.InitLength.GetValueOrDefault(),
                    s.PcsNo, s.PointLimit.GetValueOrDefault(), s.PointSystem.GetValueOrDefault(), s.SampleLength.GetValueOrDefault(), s.Score.GetValueOrDefault(),
                    s.Type, s.Width.GetValueOrDefault(), i,
                        s.Criteria.Select((d, cInd) => new CriteriaModel(d.Code, d.Group, cInd, d.Name, d.Score.A.GetValueOrDefault(), d.Score.B.GetValueOrDefault(),
                        d.Score.C.GetValueOrDefault(), d.Score.D.GetValueOrDefault())).ToList())).ToList());
            }
        }
        private InspectionMaterialViewModel DPViewModel
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
                        No = "no",
                        Type = "type"
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
        private DyeingPrintingAreaMovementModel DPModel
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DPViewModel.Area, DPViewModel.BonNo, DPViewModel.Date, DPViewModel.Shift, DPViewModel.ProductionOrder.Id,
                    DPViewModel.ProductionOrder.Code, DPViewModel.ProductionOrder.No, DPViewModel.ProductionOrderQuantity, DPViewModel.ProductionOrder.Type,
                    DPViewModel.Buyer, DPViewModel.PackingInstruction, DPViewModel.CartNo, DPViewModel.Material.Id, DPViewModel.Material.Code, DPViewModel.Material.Name,
                    DPViewModel.MaterialConstruction.Id, DPViewModel.MaterialConstruction.Code, DPViewModel.MaterialConstruction.Name, DPViewModel.MaterialWidth,
                    DPViewModel.Unit.Id, DPViewModel.Unit.Code, DPViewModel.Unit.Name, DPViewModel.Color, DPViewModel.Motif, DPViewModel.Mutation, DPViewModel.Length,
                    DPViewModel.UOMUnit, DPViewModel.Balance, DPViewModel.Status, DPViewModel.Grade, DPViewModel.SourceArea, null,
                    new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DPViewModel.Date, DPViewModel.Area, DPViewModel.Shift, AreaEnum.IM)
                    });
            }
        }

        public Mock<IServiceProvider> GetServiceProvider(IFabricQualityControlRepository service, IDyeingPrintingAreaMovementRepository dpService,
            IFabricGradeTestRepository fgtRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IFabricQualityControlRepository)))
                .Returns(service);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(dpService);
            spMock.Setup(s => s.GetService(typeof(IFabricGradeTestRepository)))
                .Returns(fgtRepository);
            return spMock;
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<FabricQualityControlModel>()))
                .ReturnsAsync(1);
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            repoMock.Setup(s => s.GetDbSet()).Returns(new List<FabricQualityControlModel>() { new FabricQualityControlModel(){
                
            } }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.UpdateFromFabricQualityControlAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(DyeingPrintingAreaMovementModel));
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(FabricQualityControlModel));
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<FabricQualityControlModel>()))
                .ReturnsAsync(1);
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GetReport()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.Shift, Model.DateIm.AddDays(-1).DateTime, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.Shift, Model.DateIm.AddDays(-1).DateTime, null, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.Shift, null, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
               DPModel.Shift, null, null, 7);

            Assert.NotEmpty(result.Data);

        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GenerateExcel(Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.Shift, Model.DateIm.AddDays(-1).DateTime, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotNull(result);

            

        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GenerateExcel(Model.Code, Model.DyeingPrintingAreaMovementId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.Shift, Model.DateIm.AddDays(2).DateTime, Model.DateIm.AddDays(2).DateTime, 7);

            Assert.NotNull(result);



        }

        [Fact]
        public void Should_Success_GetForSPP_WithNo()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());

            var fgtMock = new Mock<IFabricGradeTestRepository>();
            fgtMock.Setup(s => s.GetDbSet())
                .Returns(Model.FabricGradeTests.AsQueryable());

            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GetForSPP(Model.ProductionOrderNo);

            Assert.NotEmpty(result);



        }

        [Fact]
        public void Should_Success_GetForSPP_WithoutNo()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());

            var fgtMock = new Mock<IFabricGradeTestRepository>();
            fgtMock.Setup(s => s.GetDbSet())
                .Returns(Model.FabricGradeTests.AsQueryable());

            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaMovementModel>() { DPModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GetForSPP(null);

            Assert.NotEmpty(result);



        }
    }
}
