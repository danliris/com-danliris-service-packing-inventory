using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
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
                    DyeingPrintingAreaMovementId = 1,
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

        public Mock<IServiceProvider> GetServiceProvider(IFabricQualityControlRepository service)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IFabricQualityControlRepository)))
                .Returns(service);

            return spMock;
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IFabricQualityControlRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<FabricQualityControlModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet()).Returns(new List<FabricQualityControlModel>() { new FabricQualityControlModel(){
                
            } }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }
    }
}
