using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class InputAvalServiceTest
    {
        public InputAvalService GetService(IServiceProvider serviceProvider)
        {
            return new InputAvalService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo)
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

            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputSppRepo)
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
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSppRepo);
            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository repository,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputSppRepo,
                                                         IDyeingPrintingAreaOutputProductionOrderRepository outputSppRepo)
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
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSppRepo);
            return spMock;
        }

        private InputAvalViewModel ViewModel
        {
            get
            {
                return new InputAvalViewModel()
                {
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    BonNo = "GA.20.0001",
                    Group = "A",
                    AvalItems = new List<InputAvalItemViewModel>()
                    {
                        new InputAvalItemViewModel()
                        {
                            ProductionOrder = new ProductionOrder()
                            {
                                Id = 1,
                                No = "a",
                                OrderQuantity = 1,
                                Type ="a"
                            },
                            PackagingType = "a",
                            Remark = "s",
                            AvalType = "KAIN KOTOR",
                            AvalCartNo = "5",
                            AvalUomUnit = "MTR",
                            AvalQuantity = 5,
                            AvalQuantityKg = 1,
                            HasOutputDocument = false,
                            InputQuantity = 1,
                            InputPackagingQty = 1,
                            IsChecked = false,
                            Area = "INSPECTION MATERIAL",
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
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
                        }
                    },
                    DyeingPrintingMovementIds = new List<InputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new InputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 51,
                            ProductionOrderIds = new List<int>()
                            {
                                11,
                                12
                            }
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel Model
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date,
                                                        ViewModel.Area,
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        ViewModel.Group,
                                                        ViewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.AvalType, s.AvalCartNo, s.UomUnit,
                                                        s.AvalQuantity, s.AvalQuantityKg, s.HasOutputDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.BuyerId, s.Buyer, s.Construction,
                                                        s.Unit, s.Color, s.Motif, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                        s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DyeingPrintingAreaOutputProductionOrderId, s.Machine, s.Material.Id, s.Material.Name,
                                                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputPackagingQty,s.DateIn, s.FinishWidth))
                                                        
                                                                           .ToList());
            }
        }
        private DyeingPrintingAreaInputModel ModelPC
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date,
                                                        "PACKING",
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        ViewModel.Group,
                                                        ViewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.AvalType, s.AvalCartNo, s.UomUnit,
                                                        s.AvalQuantity, s.AvalQuantityKg, s.HasOutputDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.BuyerId, s.Buyer, s.Construction,
                                                        s.Unit, s.Color, s.Motif, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                        s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DyeingPrintingAreaOutputProductionOrderId, s.Machine, s.Material.Id, s.Material.Name,
                                                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputPackagingQty,s.DateIn, s.FinishWidth))
                                                        
                                                                           .ToList());
            }
        }
        private DyeingPrintingAreaInputModel ModelDelete
        {
            get
            {
                var data = new DyeingPrintingAreaInputModel(ViewModel.Date,
                                                        "PACKING",
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        ViewModel.Group,
                                                        ViewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.AvalType, s.AvalCartNo, s.UomUnit,
                                                        s.AvalQuantity, s.AvalQuantityKg, s.HasOutputDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.BuyerId, s.Buyer, s.Construction,
                                                        s.Unit, s.Color, s.Motif, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                        s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DyeingPrintingAreaOutputProductionOrderId, s.Machine, s.Material.Id, s.Material.Name,
                                                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputPackagingQty,s.DateIn, s.FinishWidth))
                                                        
                                                                           .ToList());
                foreach (var t in data.DyeingPrintingAreaInputProductionOrders)
                {
                    t.SetHasOutputDocument(false, "unittest", "unittest");
                }
                return data;
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
                                                        false,
                                                        "SHIPPING",
                                                        ViewModel.Group,
                                                        1,
                                                        "DO01",
                                                        true,
                                                        "OUT",
                                                        "A",
                                                        ViewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType, s.AvalCartNo, s.AvalUomUnit, s.AvalQuantity,
                                                        s.AvalQuantityKg, s.AvalQuantity, s.AvalQuantity, ViewModel.Id, ViewModel.Area, "SHIPPING", "note"))
                                                                           .ToList());
            }
        }
        private DyeingPrintingAreaOutputModel OutputModelDelete
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date,
                                                        ViewModel.Area,
                                                        ViewModel.Shift,
                                                        ViewModel.BonNo,
                                                        false,
                                                        "SHIPPING",
                                                        ViewModel.Group,
                                                        1,
                                                        "DO01",
                                                        true,
                                                        "OUT",
                                                        "A",
                                                        ViewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType, s.AvalCartNo, s.AvalUomUnit, s.AvalQuantity,
                                                        s.AvalQuantityKg, s.AvalQuantity, s.AvalQuantity, ViewModel.Id, ViewModel.Area, "SHIPPING", "note"))
                                                                           .ToList());
            }
        }
        [Fact]
        public async Task Should_BonExist_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(Model.DyeingPrintingAreaInputProductionOrders.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            inputSppMock.Setup(s => s.ReadAll())
                .Returns(Model.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

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


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_ReadPreAval()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>()
                 {
                     new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow,
                                                       "INSPECTION MATERIAL",
                                                       "PAGI",
                                                       "no",
                                                       false,
                                                       "GUDANG AVAL",
                                                       "A",
                                                       "OUT",
                                                       new List<DyeingPrintingAreaOutputProductionOrderModel>()
                                                       {
                                                           new DyeingPrintingAreaOutputProductionOrderModel("IM",
                                                                                                            "GUDANG AVAL",
                                                                                                            false,
                                                                                                            1,
                                                                                                            "no",
                                                                                                            "t",
                                                                                                            1,
                                                                                                            "1",
                                                                                                            "1",
                                                                                                            "sd",
                                                                                                            "cs",
                                                                                                            "sd",
                                                                                                            "as",
                                                                                                            "sd",
                                                                                                            "asd",
                                                                                                            "",
                                                                                                            "zimmer",
                                                                                                            "sd",
                                                                                                            "sd",
                                                                                                            1,
                                                                                                            1,
                                                                                                            1,
                                                                                                            1,
                                                                                                            "a",
                                                                                                            1,
                                                                                                            "a",
                                                                                                            "1","",1,"a","a",1,"a","a",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false,1,"a", DateTimeOffset.Now,DateTimeOffset.Now)

                     })
                 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var result = service.ReadOutputPreAval(DateTimeOffset.UtcNow, "PAGI", "A", 1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_ReadAllOutputPreAva()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            OutputModel.SetDestinationArea("GUDANG AVAL", "unittest", "unittest");
            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var result = service.ReadAllOutputPreAval(1, 1, "{}", "{}", "{}");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaInputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_GenerateBonNo()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();

            string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
            string TRANSIT = "TRANSIT";
            string PACKING = "PACKING";
            string GUDANGJADI = "GUDANG JADI";
            string GUDANGAVAL = "GUDANG AVAL";
            string SHIPPING = "SHIPPING";

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object).Object);

            var test = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now));

            var test1 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), INSPECTIONMATERIAL);
            var test2 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), TRANSIT);
            var test3 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), PACKING);
            var test4 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), GUDANGAVAL);
            var test5 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), GUDANGJADI);
            var test6 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), SHIPPING);
            var test7 = service.GenerateBonNo(1, new DateTimeOffset(DateTime.Now), "error");

            Assert.NotNull(test);
            Assert.NotNull(test1);
            Assert.NotNull(test2);
            Assert.NotNull(test3);
            Assert.NotNull(test4);
            Assert.NotNull(test5);
            Assert.NotNull(test6);
            Assert.NotNull(test7);

        }

        [Fact]
        public async Task Should_Success_Reject_PC()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var testPayloadReject = "{'Area':'GUDANG AVAL','Shift':'PAGI','Group':'A','Date':'2020-06-12','DyeingPrintingMovementIds':[{'DyeingPrintingAreaMovementId':82,'ProductionOrderIds':[202]}],'AvalItems':[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]}";
            var testinputPrevSPp = "[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]";
            var ObjectTestPayload = JsonConvert.DeserializeObject<InputAvalViewModel>(testPayloadReject);
            foreach (var item in ObjectTestPayload.AvalItems)
            {
                item.Material = new Material()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialConstruction = new MaterialConstruction()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialWidth = "1";
                item.ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                {
                    Id = 1,
                    Name = "s"
                };
                item.YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = 1,
                    Name = "s"
                };
                item.PrevSppInJson = "[]";

            }
            var objectInputSppPrev = JsonConvert.DeserializeObject<List<DyeingPrintingAreaInputProductionOrderModel>>(testinputPrevSPp);

            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(objectInputSppPrev.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);
            inputSppMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            inputSppMock.Setup(s => s.UpdateFromNextAreaInputPackingAsync(It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(ObjectTestPayload);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_PC_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            vm.Area = "PACKING";
            foreach (var spp in vm.AvalItems)
            {
                spp.Area = "PACKING";
                spp.PrevSppInJson = "[]";
            }

            var model = Model;
            model.SetArea("PACKING", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("PACKING", "", "");
            }
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

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

            var item = vm.AvalItems.FirstOrDefault();

            outputSppRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            inputSppMock.Setup(s => s.UpdateFromNextAreaInputPackingAsync(It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var vm = ViewModel;
            vm.Area = "INSPECTION MATERIAL";
            foreach (var spp in vm.AvalItems)
            {
                spp.Area = "INSPECTION MATERIAL";
            }

            var model = Model;
            model.SetArea("INSPECTION MATERIAL", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("INSPECTION MATERIAL", "", "");
            }
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = vm.AvalItems.FirstOrDefault();

            outputSppRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);


            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            vm.Area = "INSPECTION MATERIAL";
            foreach (var spp in vm.AvalItems)
            {
                spp.Area = "INSPECTION MATERIAL";
            }

            var model = Model;
            model.SetArea("INSPECTION MATERIAL", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("INSPECTION MATERIAL", "", "");
            }
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

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

            var item = vm.AvalItems.FirstOrDefault();

            outputSppRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_TR()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var vm = ViewModel;
            vm.Area = "TRANSIT";
            foreach (var spp in vm.AvalItems)
            {
                spp.Area = "TRANSIT";
            }

            var model = Model;
            model.SetArea("TRANSIT", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("TRANSIT", "", "");
            }
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = vm.AvalItems.FirstOrDefault();

            outputSppRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);


            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_TR_Duplicate_Shift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var vm = ViewModel;
            vm.Area = "TRANSIT";
            foreach (var spp in vm.AvalItems)
            {
                spp.Area = "TRANSIT";
            }

            var model = Model;
            model.SetArea("TRANSIT", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("TRANSIT", "", "");
            }
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

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

            var item = vm.AvalItems.FirstOrDefault();

            outputSppRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_BonExist_Reject()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var testPayloadReject = "{'Area':'GUDANG AVAL','Shift':'PAGI','Group':'A','Date':'2020-06-12','DyeingPrintingMovementIds':[{'DyeingPrintingAreaMovementId':82,'ProductionOrderIds':[202]}],'AvalItems':[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]}";
            var testinputPrevSPp = "[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]";
            var ObjectTestPayload = JsonConvert.DeserializeObject<InputAvalViewModel>(testPayloadReject);
            ObjectTestPayload.Date = ViewModel.Date;
            foreach (var item in ObjectTestPayload.AvalItems)
            {
                item.Material = new Material()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialConstruction = new MaterialConstruction()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialWidth = "1";
                item.ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                {
                    Id = 1,
                    Name = "s"
                };
                item.YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = 1,
                    Name = "s"
                };
                item.PrevSppInJson = "[]";
            }
            var objectInputSppPrev = JsonConvert.DeserializeObject<List<DyeingPrintingAreaInputProductionOrderModel>>(testinputPrevSPp);
            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            //Model.SetArea("PACKING","unittest","unittest");
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(objectInputSppPrev.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);
            inputSppMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            inputSppMock.Setup(s => s.UpdateFromNextAreaInputPackingAsync(It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(ObjectTestPayload);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_BonExist_Reject2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var testPayloadReject = "{'Area':'GUDANG AVAL','Shift':'PAGI','Group':'A','Date':'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','DyeingPrintingMovementIds':[{'DyeingPrintingAreaMovementId':82,'ProductionOrderIds':[202]}],'AvalItems':[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'GUDANG AVAL','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]}";
            var testinputPrevSPp = "[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'GUDANG AVAL','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]";
            var ObjectTestPayload = JsonConvert.DeserializeObject<InputAvalViewModel>(testPayloadReject);
            ObjectTestPayload.Date = ViewModel.Date;
            foreach (var item in ObjectTestPayload.AvalItems)
            {
                item.Material = new Material()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialConstruction = new MaterialConstruction()
                {
                    Id = 1,
                    Name = "a"
                };
                item.MaterialWidth = "1";
                item.ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                {
                    Id = 1,
                    Name = "s"
                };
                item.YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = 1,
                    Name = "s"
                };
            }
            var objectInputSppPrev = JsonConvert.DeserializeObject<List<DyeingPrintingAreaInputProductionOrderModel>>(testinputPrevSPp);
            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            //Model.SetArea("PACKING","unittest","unittest");
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(objectInputSppPrev.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);
            inputSppMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Reject(ObjectTestPayload);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var testPayloadReject = "{'Area':'GUDANG AVAL','Shift':'PAGI','Group':'A','Date':'2020-06-12','DyeingPrintingMovementIds':[{'DyeingPrintingAreaMovementId':82,'ProductionOrderIds':[202]}],'AvalItems':[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'GUDANG AVAL','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]}";
            var testinputPrevSPp = "[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':0,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2,}]";
            var ObjectTestPayload = JsonConvert.DeserializeObject<InputAvalViewModel>(testPayloadReject);
            var objectInputSppPrev = JsonConvert.DeserializeObject<List<DyeingPrintingAreaInputProductionOrderModel>>(testinputPrevSPp);
            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            //Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelDelete }.AsQueryable());
            //Model.SetArea("PACKING","unittest","unittest");
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            foreach (var t in OutputModel.DyeingPrintingAreaOutputProductionOrders)
            {
                t.DyeingPrintingAreaOutputId = 0;
            }
            outputRepoMock.Setup(s => s.ReadAll())
                    .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelDelete }.AsQueryable());
            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModelDelete.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(objectInputSppPrev.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);
            inputSppMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = await service.Delete(0);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var testPayloadReject = "{'Area':'GUDANG AVAL','Shift':'PAGI','Group':'A','Date':'2020-06-12','DyeingPrintingMovementIds':[{'DyeingPrintingAreaMovementId':82,'ProductionOrderIds':[202]}],'AvalItems':[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':202,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'GUDANG AVAL','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2}]}";
            var testinputPrevSPp = "[{'productionOrder':{'id':54,'code':null,'no':'F/2020/0001','type':'GRADE AB','orderQuantity':0},'cartNo':'12345','buyerId':521,'buyer':'IBU ELIZABETH SINDORO','construction':'TC CD OXFORD / TC CM OXF / 222','unit':'DYEING','color':'Purple','motif':null,'uomUnit':'MTR','remark':null,'grade':'A','status':null,'balance':4,'packingInstruction':'112','avalConnectionLength':0,'avalALength':0,'avalBLength':0,'qtyOrder':2,'avalType':null,'dyeingPrintingAreaInputProductionOrderId':50,'id':0,'active':false,'createdUtc':'0001-01-01T00:00:00','createdBy':null,'createdAgent':null,'lastModifiedUtc':'0001-01-01T00:00:00','lastModifiedBy':null,'lastModifiedAgent':null,'isDeleted':false,'deletedUtc':'0001-01-01T00:00:00','deletedBy':null,'deletedAgent':null,'area':'PACKING','bonId':82,'IsSave':true,'AvalCartNo':'12345','AvalQuantity':0,'AvalQuantityKg':0,'productionOrderId':54,'productionOrderNo':'F/2020/0001','dyeingPrintingAreaOutputProductionOrderId':202,'productionOrderOrderQuantity':2,}]";
            var ObjectTestPayload = JsonConvert.DeserializeObject<InputAvalViewModel>(testPayloadReject);
            var objectInputSppPrev = JsonConvert.DeserializeObject<List<DyeingPrintingAreaInputProductionOrderModel>>(testinputPrevSPp);
            //Mock for totalCurrentYear
            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var model = ModelDelete;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(true, "", "");
            }

            //Model.Id = 1;
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());
            //Model.SetArea("PACKING","unittest","unittest");
            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var avalItem = ViewModel.AvalItems.FirstOrDefault();
            var areaMovement = ViewModel.DyeingPrintingMovementIds.FirstOrDefault();
            var productionOrderId = areaMovement.ProductionOrderIds.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(
                    new List<DyeingPrintingAreaSummaryModel>()
                    {
                        new DyeingPrintingAreaSummaryModel(ViewModel.Date,
                                                           ViewModel.Area,
                                                           "IN",
                                                           areaMovement.DyeingPrintingAreaMovementId,
                                                           ViewModel.BonNo,
                                                           avalItem.AvalCartNo,
                                                           avalItem.AvalUomUnit,
                                                           avalItem.AvalQuantity)
                    }
                    .AsQueryable());

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            foreach (var t in OutputModel.DyeingPrintingAreaOutputProductionOrders)
            {
                t.DyeingPrintingAreaOutputId = 0;
            }
            outputRepoMock.Setup(s => s.ReadAll())
                    .Returns(new List<DyeingPrintingAreaOutputModel> { OutputModelDelete }.AsQueryable());
            outputSppRepoMock.Setup(s => s.ReadAll())
                .Returns(OutputModelDelete.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateToAvalAsync(It.IsAny<DyeingPrintingAreaSummaryModel>(), ViewModel.Date, ViewModel.Area, "IN"))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            inputSppMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);
            inputSppMock.Setup(s => s.ReadAll())
                .Returns(objectInputSppPrev.AsQueryable());
            inputSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
               .ReturnsAsync(1);
            inputSppMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(0));

        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }


        [Fact]
        public void Should_Success_GenerateExcel2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = service.GenerateExcel(Model.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel3()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = service.GenerateExcel(null, Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel4()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel_Empty()
        {
            var repoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputSppMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, outputRepoMock.Object, inputSppMock.Object, outputSppRepoMock.Object).Object);

            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetSetModel()
        {
            var test = new OutputPreAvalProductionOrderViewModel
            {
                BuyerId = 1,
                QtyOrder = 1,
                AvalType = "KAINKOTOR",
                DyeingPrintingAreaInputProductionOrderId = 1,
                ProductionOrder = new ProductionOrder
                {
                    Id = 1,
                    No = "test1"
                },
                Material = new Material()
                {
                    Id = 1,
                    Name = "a"
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = 1,
                    Name = "a"
                },
                MaterialWidth = "1",
                Machine = "a",
                Area = "arae",
                PrevSppInJson = "[]"
            };
            var test1 = test.BuyerId;
            var test2 = test.ProductionOrder;
            var test3 = test.QtyOrder;
            var test4 = test.AvalType;

            Assert.NotNull(test);
            Assert.NotNull(test.Material);
            Assert.NotNull(test.MaterialConstruction);
            Assert.NotNull(test.MaterialWidth);
            Assert.NotNull(test.Machine);

            Assert.NotNull(test.Area);
            Assert.NotNull(test.PrevSppInJson);

            Assert.Null(test.ProductPackingCode);
            Assert.Null(test.ProductSKUCode);
            Assert.Equal(0, test.ProductSKUId);
            Assert.Equal(0, test.FabricSKUId);
            Assert.Equal(0, test.ProductPackingId);
            Assert.Equal(0, test.FabricPackingId);
            Assert.False(test.HasPrintingProductPacking);
            Assert.False(test.HasPrintingProductSKU);
        }
    }
}
