﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
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
    public class OutputInspectionMaterialServiceTest
    {
        public OutputInspectionMaterialService GetService(IServiceProvider serviceProvider)
        {
            return new OutputInspectionMaterialService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaOutputProductionOrderRepository outSppRepo,
           IFabricPackingSKUService fabricService, IDyeingPrintingAreaReferenceRepository areaReferenceRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outSppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaReferenceRepository)))
                .Returns(areaReferenceRepo);
            spMock.Setup(s => s.GetService(typeof(IFabricPackingSKUService)))
                .Returns(fabricService);

            return spMock;
        }

        private OutputInspectionMaterialViewModel ViewModel
        {
            get
            {
                return new OutputInspectionMaterialViewModel()
                {
                    Area = "INSPECTION MATERIAL",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Type = "OUT",
                    Shift = "pas",
                    Group = "A",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputInspectionMaterialId = 1,
                    InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
                    {
                        new OutputInspectionMaterialProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Id = 1,
                            Construction = "sd",
                            IsSave = true,
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            Machine = "a",
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "a"
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
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            ProductionOrderDetails = new List<OutputInspectionMaterialProductionOrderDetailViewModel>()
                            {
                                new OutputInspectionMaterialProductionOrderDetailViewModel()
                                {
                                    Balance = 1,
                                    Id = 1,
                                    Grade = "a",
                                    HasNextAreaDocument = false,
                                    Remark = "re",

                                }
                            },
                            BuyerId = 1,
                            InputId = 1,
                        },
                        new OutputInspectionMaterialProductionOrderViewModel()
                        {
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            IsSave = true,
                            Status = "Ok",
                            Id = 2,
                            Motif = "sd",
                            FinishWidth = "s",
                            PackingInstruction = "d",
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
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            ProductionOrderDetails = new List<OutputInspectionMaterialProductionOrderDetailViewModel>()
                            {
                                new OutputInspectionMaterialProductionOrderDetailViewModel()
                                {
                                    Balance = 1,
                                    Grade = "a",
                                    Id = 2,
                                    HasNextAreaDocument = false,
                                    Remark = "re",
                                    AvalType = "type"
                                }
                            },
                            BuyerId = 1,
                            InputId = 1,

                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel Model
        {
            get
            {
                List<DyeingPrintingAreaOutputProductionOrderModel> productionOrderModels = new List<DyeingPrintingAreaOutputProductionOrderModel>();

                foreach (var item in ViewModel.InspectionMaterialProductionOrders)
                {
                    foreach (var detail in item.ProductionOrderDetails)
                    {
                        var productionOrders = new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument,
                            item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo,
                            item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId,
                            detail.AvalType, item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.Machine,item.ProductionMachine,
                            item.AdjDocumentNo, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, 1, 1, "a", false, item.FinishWidth,item.DateIn,item.DateOut, item.MaterialOrigin)
                           
                        {
                            Id = detail.Id
                        };
                        productionOrderModels.Add(productionOrders);
                    }
                }


                var model = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.Type, productionOrderModels);


                return model;
            }
        }

        private OutputInspectionMaterialViewModel ViewModelAdj
        {
            get
            {
                return new OutputInspectionMaterialViewModel()
                {
                    Area = "INSPECTION MATERIAL",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Type = "Adj",
                    Shift = "pas",
                    Group = "A",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputInspectionMaterialId = 1,
                    InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
                    {
                        new OutputInspectionMaterialProductionOrderViewModel()
                        {
                            Balance= 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Id = 1,
                            Construction = "sd",
                            IsSave = true,
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            Machine = "a",
                            Material = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            FinishWidth = "s",
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            ProductionOrderDetails = new List<OutputInspectionMaterialProductionOrderDetailViewModel>()
                            {
                                new OutputInspectionMaterialProductionOrderDetailViewModel()
                                {
                                    Balance = 1,
                                    Id = 1,
                                    Grade = "a",
                                    HasNextAreaDocument = false,
                                    Remark = "re",

                                }
                            },
                            BuyerId = 1,
                            InputId = 1,
                        },
                        new OutputInspectionMaterialProductionOrderViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            IsSave = true,
                            Status = "Ok",
                            Id = 2,
                            Motif = "sd",
                            PackingInstruction = "d",
                            Material = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "a"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            AdjDocumentNo = "ss",
                            UomUnit = "d",
                            ProductionOrderDetails = new List<OutputInspectionMaterialProductionOrderDetailViewModel>()
                            {
                                new OutputInspectionMaterialProductionOrderDetailViewModel()
                                {
                                    Balance = 1,
                                    Grade = "a",
                                    Id = 2,
                                    HasNextAreaDocument = false,
                                    Remark = "re",
                                    AvalType = "type"
                                }
                            },
                            BuyerId = 1,
                            InputId = 1,

                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel ModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAdj.Date, ViewModelAdj.Area, ViewModelAdj.Shift, ViewModelAdj.BonNo, ViewModelAdj.HasNextAreaDocument,
                    ViewModelAdj.DestinationArea, ViewModelAdj.Group, ViewModelAdj.Type,
                    ViewModelAdj.InspectionMaterialProductionOrders.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument,
                            item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo,
                            item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, "", "",item.Status, item.Balance, item.Id, item.BuyerId,
                            "", item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.Machine,item.ProductionMachine, item.AdjDocumentNo,
                            item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, 0, 0, null, false, item.FinishWidth,item.DateIn,item.DateOut, item.MaterialOrigin)
                           
                    {
                        Id = item.Id
                    }).ToList());
            }
        }


        [Fact]
        public async Task Should_Success_Create()   
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);


            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModelAdj);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj_Duplicate()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var model = ModelAdj;
            model.SetType("ADJ IN", "", "");

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            sppoutRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModelAdj);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_CreateAdjIn()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var vm = ViewModelAdj;
            foreach (var item in vm.InspectionMaterialProductionOrders)
            {
                item.Balance = 1;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Create_WithPrevSummary_Isnull()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);


            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaSummaryModel>() { new DyeingPrintingAreaSummaryModel() }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_Create_DuplicateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);


            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            sppoutRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>())).ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_DuplicateShift_Produksi()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);
            var model = Model;
            model.SetDestinationArea("PRODUKSI", "", "");
            var vm = ViewModel;
            vm.DestinationArea = "PRODUKSI";

            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", item.InputId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            sppoutRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>())).ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_DuplicateShift_PReviousNull()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);


            repoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            sppoutRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>())).ReturnsAsync(1);
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
               .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
               .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Packing()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
               .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputInspectionMaterialId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
               .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Aval()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.GetDbSet())
               .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputInspectionMaterialId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
               .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "GUDANG AVAL";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Produksi()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);
            var model = Model;
            model.SetDestinationArea("PRODUKSI", "", "");
            var vm = ViewModel;
            vm.DestinationArea = "PRODUKSI";
            repoMock.Setup(s => s.GetDbSet())
               .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputInspectionMaterialId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
               .ReturnsAsync(1);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Aval_DuplicateShift()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            var mdl = Model;
            mdl.SetDestinationArea("GUDANG AVAL", "", "");
            repoMock.Setup(s => s.GetDbSet())
               .Returns(new List<DyeingPrintingAreaOutputModel>() { mdl }.AsQueryable());

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { mdl }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.InspectionMaterialProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputInspectionMaterialId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, 0)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputIMAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
               .ReturnsAsync(1);
            sppoutRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>())).ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                 .Returns(new FabricSKUIdCodeDto()
                 {
                     FabricSKUId = 1,
                     ProductSKUCode = "c",
                     ProductSKUId = 1
                 });
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "GUDANG AVAL";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, 0, true, 1, 0, 1, "name", 1, "name", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a"));


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, 0, true, 1, 0, 1, "name", 1, "name", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a"));



            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var model = Model;
            model.SetType(null, "", "");

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            sppRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputProductionOrderModel(Model.Area, 1, "no", "type", 1, "ins", "cat", "buyer", "const", "uin", "col", "mot", "unit", 1, 0, true, 1, 0, 1, "name", 1, "name", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a"));


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GenerateExcel(ViewModel,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var vm = ViewModel;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>();

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);


            var result = service.GenerateExcel(vm,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetInputInspectionMaterialProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GetInputInspectionMaterialProductionOrders(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_Success_GetInputInspectionMaterialProductionOrders_All()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GetInputInspectionMaterialProductionOrders(0);

            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            repoMock.Setup(s => s.DeleteIMArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete_Produksi()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var model = Model;
            model.SetDestinationArea("PRODUKSI", "", "");

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteIMArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteAdjIN()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var model = ModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetBalance(1, "", "");
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_DeleteNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var model = Model;
            model.SetType(null, "", "");
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Update()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateIMArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Produksi()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.SetDestinationArea("PRODUKSI", "", "");
            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";
            vm.DestinationArea = "PRODUKSI";
            vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateIMArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateAdjIN()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            foreach (var item in vm.InspectionMaterialProductionOrders)
            {
                item.Balance = 1;
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateAdjOut()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = -1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(true, "", "");

            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.InspectionMaterialProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateIMArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Delete_Produksi()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = Model;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(true, "", "");
            model.SetDestinationArea("PRODUKSI", "", "");
            var vm = ViewModel;
            vm.Shift = vm.Shift + "new";
            vm.DestinationArea = "PRODUKSI";
            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.InspectionMaterialProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>() { detail };
            fabricService.Setup(s => s.AutoCreateSKU(It.IsAny<FabricSKUAutoCreateFormDto>()))
                .Returns(new FabricSKUIdCodeDto()
                {
                    FabricSKUId = 1,
                    ProductSKUCode = "c",
                    ProductSKUId = 1
                });
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateIMArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update_Delete_Adj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var model = ModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetHasNextAreaDocument(true, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            //vm.InspectionMaterialProductionOrders.FirstOrDefault().Balance = 1;
            var detail = vm.InspectionMaterialProductionOrders.FirstOrDefault();
            detail.Id = 0;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>() { detail };

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GetDistinctProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());
            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GetDistinctProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll3()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = service.GenerateExcel(null, Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll4()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var modelN = Model;
            modelN.SetType(null, "", "");
            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model, ModelAdj, modelN }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel_Empty()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);
            var result = service.GenerateExcel(Model.Date.AddDays(-1), Model.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetDistinctAllProductionOrders()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();


            sppoutRepoMock.Setup(s => s.ReadAll()).Returns(Model.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            sppRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "a", "e", "rr", "1", "as", "test", "unit", "color", "motif", "mtr", 2, false, 1)
            }.AsQueryable());
            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GetDistinctAllProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GenerateExcelAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelAdj);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var result = service.GenerateExcel(ViewModelAdj,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelNoType()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);

            var vm = ViewModel;
            vm.Type = null;

            var result = service.GenerateExcel(vm,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Empty_GenerateExcelAdj()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();
            var vm = ViewModelAdj;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>();

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object);


            var result = service.GenerateExcel(vm,7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Validate_VM()
        {

            var spp = new OutputInspectionMaterialProductionOrderViewModel()
            {
                PreviousBalance = 1,
                InitLength = 1,
                InputId = 1,
                BalanceRemains = 1,
                Balance = 1,
                HasNextAreaDocument = true
            };

            Assert.NotEqual(0, spp.PreviousBalance);
            Assert.NotEqual(0, spp.InitLength);
            Assert.NotEqual(0, spp.InputId);
            Assert.NotEqual(0, spp.BalanceRemains);
            Assert.NotEqual(0, spp.Balance);
            Assert.True(spp.HasNextAreaDocument);

            var date = DateTimeOffset.UtcNow;

            var index = new Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial.IndexViewModel()
            {
                Date = date
            };

            Assert.Null(index.Area);
            Assert.Equal(0, index.Id);
            Assert.Null(index.BonNo);
            Assert.Equal(date, index.Date);
            Assert.Null(index.DestinationArea);
            Assert.False(index.HasNextAreaDocument);
            Assert.Null(index.Shift);
            Assert.Null(index.Type);
            Assert.Null(index.Group);
            Assert.Empty(index.InspectionMaterialProductionOrders);

            var detail = new OutputInspectionMaterialProductionOrderDetailViewModel()
            {
                HasNextAreaDocument = false
            };
            Assert.False(detail.HasNextAreaDocument);

            var adjvm = new AdjInspectionMaterialProductionOrderViewModel();
            Assert.Null(adjvm.ProductionOrder);
            Assert.Null(adjvm.Material);
            Assert.Null(adjvm.MaterialConstruction);
            Assert.Null(adjvm.MaterialWidth);
            Assert.Null(adjvm.CartNo);
            Assert.Null(adjvm.Construction);
            Assert.Null(adjvm.Unit);
            Assert.Equal(0, adjvm.BuyerId);
            Assert.Null(adjvm.Buyer);
            Assert.Null(adjvm.Color);
            Assert.Null(adjvm.Motif);
            Assert.Null(adjvm.UomUnit);
            Assert.Null(adjvm.FinishWidth);
            Assert.Equal(0, adjvm.DyeingPrintingAreaInputProductionOrderId);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var fabricService = new Mock<IFabricPackingSKUService>();

            var areaReferenceMock = new Mock<IDyeingPrintingAreaReferenceRepository>();
            areaReferenceMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaReferenceModel>()))
                .ReturnsAsync(1);
            var serviceProvider = GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, sppoutRepoMock.Object, fabricService.Object, areaReferenceMock.Object).Object;
            var service = GetService(serviceProvider);

            var vm = new OutputInspectionMaterialViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    IsSave = true,
                },
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    IsSave = true,
                    BalanceRemains = 0
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    IsSave = true,
                    BalanceRemains = 1,
                    ProductionOrderDetails = new List<OutputInspectionMaterialProductionOrderDetailViewModel>()
                    {
                        new OutputInspectionMaterialProductionOrderDetailViewModel()
                        {
                            Balance = 0
                        },
                        new OutputInspectionMaterialProductionOrderDetailViewModel()
                        {
                            Balance = 2
                        }
                    }
                },
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    IsSave = true,
                    BalanceRemains = 0,

                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.DestinationArea = "GUDANG AVAL";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = null;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    Balance = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ IN";
            vm.Id = 1;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = -1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AdjType = "ADJ OUT";
            vm.Id = 1;
            vm.InspectionMaterialProductionOrders = new List<OutputInspectionMaterialProductionOrderViewModel>()
            {
                new OutputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder(),
                    Balance = 1
                },
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }
    }
}
