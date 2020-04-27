using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
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
                    InspectionMaterialBonNo = "no",
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
                            AvalALength = 1,
                            AvalBLength = 1,
                            AvalConnectionLength = 1,
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
                return new FabricQualityControlModel(ViewModel.Code, ViewModel.DateIm.GetValueOrDefault(), ViewModel.Group, ViewModel.IsUsed.GetValueOrDefault(), ViewModel.InspectionMaterialId,
                ViewModel.InspectionMaterialBonNo, ViewModel.InspectionMaterialProductionOrderId, ViewModel.ProductionOrderNo,
                ViewModel.MachineNoIm, ViewModel.OperatorIm, ViewModel.PointLimit.GetValueOrDefault(), ViewModel.PointSystem.GetValueOrDefault(),
                ViewModel.FabricGradeTests.Select((s, i) =>
                    new FabricGradeTestModel(s.AvalALength.GetValueOrDefault(), s.AvalBLength.GetValueOrDefault(), s.AvalConnectionLength.GetValueOrDefault(), s.FabricGradeTest.GetValueOrDefault(), s.FinalArea.GetValueOrDefault(),
                    s.FinalGradeTest.GetValueOrDefault(), s.FinalLength.GetValueOrDefault(), s.FinalScore.GetValueOrDefault(), s.Grade, s.InitLength.GetValueOrDefault(),
                    s.PcsNo, s.PointLimit.GetValueOrDefault(), s.PointSystem.GetValueOrDefault(), s.SampleLength.GetValueOrDefault(), s.Score.GetValueOrDefault(),
                    s.Type, s.Width.GetValueOrDefault(), i,
                        s.Criteria.Select((d, cInd) => new CriteriaModel(d.Code, d.Group, cInd, d.Name, d.Score.A.GetValueOrDefault(), d.Score.B.GetValueOrDefault(),
                        d.Score.C.GetValueOrDefault(), d.Score.D.GetValueOrDefault())).ToList())).ToList());
            }
        }
        private InputInspectionMaterialViewModel DPViewModel
        {
            get
            {
                return new InputInspectionMaterialViewModel()
                {
                    Id = 1,
                    Area = "area",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "shift",
                    BonNo = "no",
                    InspectionMaterialProductionOrders = new List<InputInspectionMaterialProductionOrderViewModel>()
                    {
                        new InputInspectionMaterialProductionOrderViewModel()
                        {
                            Motif = "mot",
                            PackingInstruction = "in",
                            Balance = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Id = 1,
                                No = "no",
                                Type = "s"
                            },
                            Id = 1,
                            Buyer = "bu",
                            CartNo ="no",
                            Color = "colo",
                            Construction = "sad",
                            Unit = "s",
                            HasOutputDocument = false,
                            UomUnit = "sd"
                        }
                    }
                };
            }
        }
        private DyeingPrintingAreaInputProductionOrderModel DPModel
        {
            get
            {
                var model = new DyeingPrintingAreaInputProductionOrderModel(1, "np", "type", "ins", "Cartn", "biyer", "coms", "name", "col", "mot", "uni", 1, false);
                model.DyeingPrintingAreaInputId = 1;
                model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow, "INSPECTION MATERIAL", "pagi", "no", new List<DyeingPrintingAreaInputProductionOrderModel>()
                {
                    model
                });


                return model;
            }
        }

        public Mock<IServiceProvider> GetServiceProvider(IFabricQualityControlRepository service, IDyeingPrintingAreaInputProductionOrderRepository dpService,
            IFabricGradeTestRepository fgtRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IFabricQualityControlRepository)))
                .Returns(service);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(dpService);
            spMock.Setup(s => s.GetService(typeof(IFabricGradeTestRepository)))
                .Returns(fgtRepository);
            return spMock;
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
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
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.UpdateFromFabricQualityControlAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
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

            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(DyeingPrintingAreaInputProductionOrderModel));
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
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
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
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
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
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());
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
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.DyeingPrintingAreaInput.Shift, Model.DateIm.AddDays(-1).DateTime, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.DyeingPrintingAreaInput.Shift, Model.DateIm.AddDays(-1).DateTime, null, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.DyeingPrintingAreaInput.Shift, null, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotEmpty(result.Data);

            result = service.GetReport(1, 25, Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
               DPModel.DyeingPrintingAreaInput.Shift, null, null, 7);

            //Assert.NotEmpty(result.Data);

        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GenerateExcel(Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.DyeingPrintingAreaInput.Shift, Model.DateIm.AddDays(-1).DateTime, Model.DateIm.AddDays(1).DateTime, 7);

            Assert.NotNull(result);



        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<FabricQualityControlModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());
            var fgtMock = new Mock<IFabricGradeTestRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GenerateExcel(Model.Code, Model.DyeingPrintingAreaInputId, DPModel.ProductionOrderType, Model.ProductionOrderNo,
                DPModel.DyeingPrintingAreaInput.Shift, Model.DateIm.AddDays(2).DateTime, Model.DateIm.AddDays(2).DateTime, 7);

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

            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());

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

            var dpMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            dpMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { DPModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object, fgtMock.Object).Object);

            var result = service.GetForSPP(null);

            Assert.NotEmpty(result);



        }
    }
}
