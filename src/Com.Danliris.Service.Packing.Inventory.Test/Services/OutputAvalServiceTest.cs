using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class OutputAvalServiceTest
    {
        public OutputAvalService GetService(IServiceProvider serviceProvider)
        {
            return new OutputAvalService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository inputRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputProductionOrderRepo)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionOrderRepo);
            //spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
            //    .Returns(new DyeingPrintingAreaInputProductionOrderRepository());

            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository inputRepo,
                                                        IDyeingPrintingAreaOutputRepository outputRepo,
                                                        IDyeingPrintingAreaMovementRepository movementRepo,
                                                        IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                        IDyeingPrintingAreaInputProductionOrderRepository inputProductionOrderRepo,
                                                        IDyeingPrintingAreaOutputProductionOrderRepository outputProductionOrderRepo)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionOrderRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionOrderRepo);
            //spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
            //    .Returns(new DyeingPrintingAreaInputProductionOrderRepository());

            return spMock;
        }
        private OutputAvalViewModel ViewModel
        {
            get
            {
                return new OutputAvalViewModel()
                {
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "PENJUALAN",
                    Shift = "PAGI",
                    Type = "OUT",
                    BonNo = "GA.20.0001",
                    Group = "A",
                    DeliveryOrderSalesNo = "Do01",
                    DeliveryOrdeSalesId = 1,
                    HasNextAreaDocument = false,
                    AvalItems = new List<OutputAvalItemViewModel>()
                    {
                        new OutputAvalItemViewModel()
                        {
                            AvalItemId = 122,
                            AvalType = "KAIN KOTOR",
                            AvalCartNo = "5",
                            AvalUomUnit = "MTR",
                            AvalQuantity = 5,
                            AvalQuantityKg = 1,
                            AvalOutQuantity = 1,
                            AvalOutSatuan= 1
                        }
                    },
                    DyeingPrintingMovementIds = new List<OutputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new OutputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 51,
                            AvalItemId = 122
                        }
                    }
                };
            }
        }

        private OutputAvalViewModel ViewModelAdj
        {
            get
            {
                return new OutputAvalViewModel()
                {
                    Area = "GUDANG AVAL",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    Shift = "PAGI",
                    Type = "ADJ",
                    BonNo = "GA.20.0001",
                    Group = "A",
                    DeliveryOrderSalesNo = "Do01",
                    DeliveryOrdeSalesId = 1,
                    HasNextAreaDocument = false,
                    AvalItems = new List<OutputAvalItemViewModel>()
                    {
                        new OutputAvalItemViewModel()
                        {
                            AvalItemId = 122,
                            AvalType = "KAIN KOTOR",
                            AvalCartNo = "5",
                            AvalUomUnit = "MTR",
                            AvalQuantity = -5,
                            AvalQuantityKg = -1,
                            AvalOutQuantity = 1,
                            AvalOutSatuan= 1,
                            AdjDocumentNo = "a",
                        }
                    },
                    DyeingPrintingMovementIds = new List<OutputAvalDyeingPrintingAreaMovementIdsViewModel>()
                    {
                        new OutputAvalDyeingPrintingAreaMovementIdsViewModel()
                        {
                            DyeingPrintingAreaMovementId = 51,
                            AvalItemId = 122
                        }
                    }
                };
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
                                                         ViewModel.DestinationArea,
                                                         ViewModel.Group,
                                                         ViewModel.Type,
                                                         ViewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(
                                                             ViewModel.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo, s.AvalTransformationId)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel OutputModelAdj
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelAdj.Date, ViewModelAdj.Area, ViewModelAdj.Shift, ViewModelAdj.BonNo, ViewModelAdj.HasNextAreaDocument, ViewModelAdj.DestinationArea,
                    ViewModelAdj.Group, ViewModelAdj.Type, ViewModelAdj.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(
                                                             ViewModelAdj.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo, s.AvalTransformationId)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel OutputModelExist
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date,
                                                         ViewModel.Area,
                                                         ViewModel.Shift,
                                                         ViewModel.BonNo,
                                                         ViewModel.DeliveryOrderSalesNo,
                                                         ViewModel.DeliveryOrdeSalesId,
                                                         false,
                                                         ViewModel.DestinationArea,
                                                         ViewModel.Group,
                                                         ViewModel.Type,
                                                         ViewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(
                                                             ViewModel.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo, s.AvalTransformationId)).ToList());
            }
        }
        private DyeingPrintingAreaOutputModel OutputEmptyModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date,
                                                         ViewModel.Area,
                                                         ViewModel.Shift,
                                                         ViewModel.BonNo,
                                                         false,
                                                         ViewModel.DestinationArea,
                                                         ViewModel.Group,
                                                         ViewModel.Type,
                                                         new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }
        private InputAvalViewModel ViewModelInput
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
                        {ProductionOrder = new ProductionOrder()
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
                            ProductSKUId = 1,
                            FabricSKUId = 1,
                            ProductSKUCode = "c",
                            HasPrintingProductSKU = false,
                            ProductPackingId = 1,
                            FabricPackingId = 1,
                            ProductPackingCode = "c",
                            HasPrintingProductPacking = false
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
        private DyeingPrintingAreaInputModel ModelInput
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelInput.Date,
                                                        ViewModelInput.Area,
                                                        ViewModelInput.Shift,
                                                        ViewModelInput.BonNo,
                                                        ViewModelInput.Group,
                                                        "KAIN KOTOR",
                                                        true,
                                                        10,
                                                        10,
                                                        ViewModelInput.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(ViewModelInput.Area, s.AvalType, s.AvalCartNo, s.UomUnit,
                                                        s.AvalQuantity, s.AvalQuantityKg, s.HasOutputDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.BuyerId, s.Buyer, s.Construction,
                                                        s.Unit, s.Color, s.Motif, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                        s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DyeingPrintingAreaOutputProductionOrderId, s.Machine, s.Material.Id, s.Material.Name,
                                                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking))
                                                                           .ToList());
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            inputProductionOrdersRepoMock.Setup(s => s.GetInputProductionOrder(It.IsAny<int>()))
                .Returns(new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL", "type", "1", "a", 1, 1, false, 1, "m", "a", 1, "a", "c", "u", "c", "m", "r", "g", "s", 1, "a", "a", 1, "a", 1, "a", 1, "a", 1, "a", 1, "d", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false));

            inputProductionOrdersRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.UpdateAvalTransformationFromOut(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.AvalData>>(1, new List<Inventory.Infrastructure.Utilities.AvalData>()));

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, outputProductionOrdersRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Buyer()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            var model = OutputModel;
            model.SetDestinationArea("BUYER", "", "");
            var vm = ViewModel;
            vm.DestinationArea = "BUYER";

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            inputProductionOrdersRepoMock.Setup(s => s.GetInputProductionOrder(It.IsAny<int>()))
                .Returns(new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL", "type", "1", "a", 1, 1, false, 1, "m", "a", 1, "a", "c", "u", "c", "m", "r", "g", "s", 1, "a", "a", 1, "a", 1, "a", 1, "a", 1, "a", 1, "d", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false));

            inputProductionOrdersRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, outputProductionOrdersRepoMock.Object).Object);

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_bonExist()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            //OutputModel.SetDeliveryOrderSales( ViewModel.DeliveryOrdeSalesId,ViewModel.DeliveryOrderSalesNo,"unitetest","unittest");
            //var outputmodel = OutputModel;
            //Mock for totalCurrentYear

            var model = OutputModelExist;
            var vm = ViewModel;

            vm.DestinationArea = "BUYER";
            model.SetDestinationArea("BUYER", "", "");

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputSppRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            var test = ModelInput;
            test.SetIsTransformedAval(true, "unittest", "unittest");
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { test }.AsQueryable());



            inputProductionOrdersRepoMock.Setup(s => s.GetInputProductionOrder(It.IsAny<int>()))
                .Returns(new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL", "type", "1", "a", 1, 1, false, 1, "m", "a", 1, "a", "c", "u", "c", "m", "r", "g", "s", 1, "a", "a", 1, "a", 1, "a", 1, "a", 1, "a", 1, "d", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false));

            inputProductionOrdersRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object,
                                                        outputSppRepoMock.Object).Object);

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_bonExist_Penjualan()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            //OutputModel.SetDeliveryOrderSales( ViewModel.DeliveryOrdeSalesId,ViewModel.DeliveryOrderSalesNo,"unitetest","unittest");
            //var outputmodel = OutputModel;
            //Mock for totalCurrentYear

            var model = OutputModelExist;
            var vm = ViewModel;

            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            outputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());


            inputRepoMock.Setup(s => s.UpdateAvalTransformationFromOut(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<Inventory.Infrastructure.Utilities.AvalData>>(1, new List<Inventory.Infrastructure.Utilities.AvalData>()));

            outputSppRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            //Mock for Create New Row in Input and ProductionOrdersInput in Each Repository 
            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            var test = ModelInput;
            test.SetIsTransformedAval(true, "unittest", "unittest");
            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { test }.AsQueryable());



            inputProductionOrdersRepoMock.Setup(s => s.GetInputProductionOrder(It.IsAny<int>()))
                .Returns(new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL", "type", "1", "a", 1, 1, false, 1, "m", "a", 1, "a", "c", "u", "c", "m", "r", "g", "s", 1, "a", "a", 1, "a", 1, "a", 1, "a", 1, "a", 1, "d", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false));

            inputProductionOrdersRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object,
                                                        outputSppRepoMock.Object).Object);

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object,
                                                        outputSppRepoMock.Object).Object);

            var vm = ViewModelAdj;
            foreach (var item in vm.AvalItems)
            {
                item.AvalQuantity = -1;
                item.AvalQuantityKg = -5;
            }
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_CreateAdj_Duplicate()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            var model = OutputModelAdj;
            model.SetType("ADJ OUT", "", "");

            outputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object,
                                                        outputSppRepoMock.Object).Object);

            var vm = ViewModelAdj;
            foreach (var item in vm.AvalItems)
            {
                item.AvalQuantity = -1;
                item.AvalQuantityKg = -5;
            }
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_CreateAdjIn()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outputSppRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelInput = ModelInput;
            modelInput.SetIsTransformedAval(true, "", "");
            outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { modelInput }.AsQueryable());

            inputRepoMock.Setup(s => s.UpdateHeaderAvalTransform(It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object,
                                                        outputSppRepoMock.Object).Object);
            var vm = ViewModelAdj;
            foreach (var item in vm.AvalItems)
            {
                item.AvalQuantity = 1;
                item.AvalQuantityKg = 5;
            }

            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_ReadAllAvailableAval()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { ModelInput }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = service.ReadAllAvailableAval(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public void Should_Success_ReadByBonAvailableAval()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { ModelInput }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = service.ReadByBonAvailableAval(0, 1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public void Should_Success_ReadByTypeAvailableAval()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var test = ModelInput;
            test.SetIsTransformedAval(true, "unittest", "unittest");

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { test }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = service.ReadByTypeAvailableAval("KAIN KOTOR", 1, 25, "{ }", "{}", null);

            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public void Should_Success_ReadAvailableAval()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>()
                 {
                     new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow,
                                                      "GUDANG AVAL",
                                                      "PAGI",
                                                      "IM.GA.20.0001",
                                                      "A",
                                                      new List<DyeingPrintingAreaInputProductionOrderModel>()
                                                      {
                                                          new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL","type","1","a",1,1,false,1,"m","a",1,"a","c","u","c","m","r","g","s",1,"a","a",1,"a",1,"a",1,"a",1,"a",1,"d","1",1,"a",1,"a",1,1,"a",false,1,1,"a",false)
                     })
                 }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = service.ReadAvailableAval(DateTimeOffset.UtcNow, "PAGI", "A", 1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadById_Buyer()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = OutputModel;
            model.SetDestinationArea("BUYER", "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModelAdj);

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ModelInput);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadByIdNoType()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = OutputModel;
            model.SetType(null, "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);
            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcelNoType()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = OutputModel;
            model.SetType(null, "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputEmptyModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcelAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModelAdj);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                       outputRepoMock.Object,
                                                       movementRepoMock.Object,
                                                       summaryRepoMock.Object,
                                                       inputProductionOrdersRepoMock.Object).Object);
            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcelAdj()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = OutputModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();
            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                       outputRepoMock.Object,
                                                       movementRepoMock.Object,
                                                       summaryRepoMock.Object,
                                                       inputProductionOrdersRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcelOUT()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            var model = OutputModel;
            model.SetType("OUT", "", "");

            outputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                       outputRepoMock.Object,
                                                       movementRepoMock.Object,
                                                       summaryRepoMock.Object,
                                                       inputProductionOrdersRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetDistinctAllProductionOrders()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            sppoutRepoMock.Setup(s => s.ReadAll()).Returns(OutputModel.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaInputModel>() { ModelInput }.AsQueryable());
            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                       outputRepoMock.Object,
                                                       movementRepoMock.Object,
                                                       summaryRepoMock.Object,
                                                       inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GetDistinctAllProductionOrder(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = OutputModel;
            modelN.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel, OutputModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(OutputModel.Date.AddDays(-1), OutputModel.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll2()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = OutputModel;
            modelN.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel, OutputModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(OutputModel.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll3()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = OutputModel;
            modelN.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel, OutputModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(null, OutputModel.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll4()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = OutputModel;
            var model = OutputModel;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(false, "", "");
            }

            foreach (var item in modelN.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(false, "", "");
            }
            modelN.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { model, OutputModelAdj, modelN }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcel_Empty()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var modelN = OutputModel;
            var model = OutputModel;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(false, "", "");
            }

            foreach (var item in modelN.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(false, "", "");
            }
            modelN.SetType(null, "", "");
            outputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object);
            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModel);
            repoMock.Setup(s => s.DeleteAvalArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete_NotPenjualan()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            var model = OutputModel;
            model.SetDestinationArea("BUYER", "", "");
            foreach(var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetHasNextAreaDocument(false, "", "");
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAvalArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OutputModelAdj);
            repoMock.Setup(s => s.DeleteAdjustmentAval(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            var model = OutputModelAdj;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.SetBalance(1, "", "");
                item.SetAvalQuantityKg(1, "", "");
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAdjustmentAval(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var model = OutputModel;
            model.SetType(null, "", "");
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAvalArea(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var model = OutputModel;

            model.SetDestinationArea("BUYER", "", "");
            var vm = ViewModel;
            vm.DestinationArea = "BUYER";
            vm.Shift = vm.Shift + "new";

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAvalArea(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var model = OutputModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            foreach (var item in vm.AvalItems)
            {
                item.AvalQuantity = 1;
                item.AvalQuantityKg = 1;
            }

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentDataAval(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

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
            var outSPPRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var model = OutputModelAdj;
            model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = ViewModelAdj;
            vm.Shift = vm.Shift + "new";

            vm.AvalItems.FirstOrDefault().AvalQuantity = -1;
            vm.AvalItems.FirstOrDefault().AvalQuantityKg = -1;

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.UpdateAdjustmentData(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object, repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, outSPPRepoMock.Object).Object);

            var result = await service.Update(1, vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GeneratedBon()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object).Object);
            var res1 = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "SHIPPING");
            var res2 = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "PENJUALAN");
            var res3 = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "BUYER");
            var res4 = service.GenerateBonNo(1, DateTimeOffset.UtcNow, "test");

            Assert.NotNull(res1);
            Assert.NotNull(res2);
            Assert.NotNull(res3);
            Assert.NotNull(res4);


        }

        [Fact]
        public void Should_Success_GetSetModel()
        {
            var test = ViewModel;
            var did = test.DeliveryOrdeSalesId;
            var dno = test.DeliveryOrderSalesNo;
            var nexArea = test.HasNextAreaDocument;
            var dpIds = test.DyeingPrintingMovementIds;
            Assert.NotNull(test);
        }

        [Fact]
        public void Should_Exception_ValidationVM()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var inputProductionOrdersRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var sppoutRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var serviceProvider = GetServiceProvider(inputRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        inputProductionOrdersRepoMock.Object, sppoutRepoMock.Object).Object;

            var service = GetService(serviceProvider);

            var vm = new OutputAvalViewModel();
            vm.Type = "OUT";
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Date = DateTimeOffset.UtcNow.AddDays(-1);
            vm.AvalItems = new List<OutputAvalItemViewModel>()
            {
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AvalItems = new List<OutputAvalItemViewModel>()
            {
                new OutputAvalItemViewModel()
                {

                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = null;
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.Type = "ADJ";
            vm.AvalItems = new List<OutputAvalItemViewModel>();
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));

            vm.AvalItems = new List<OutputAvalItemViewModel>()
            {
                new OutputAvalItemViewModel()
                {

                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));


            vm.AvalItems = new List<OutputAvalItemViewModel>()
            {
                new OutputAvalItemViewModel()
                {
                    AvalQuantityKg= -1,
                    AvalQuantity = -1
                },
                new OutputAvalItemViewModel()
                {
                    AvalQuantityKg= 1,
                    AvalQuantity = 1
                }
            };
            validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(vm));
        }

        [Fact]
        public void ValidateVM()
        {
            var index = new Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval.IndexViewModel()
            {
            };
            Assert.Null(index.Type);

            var adjvm = new AdjAvalItemViewModel();
            Assert.Null(adjvm.AvalType);
            Assert.Equal(0, adjvm.AvalQuantity);
            Assert.Equal(0, adjvm.AvalQuantityKg);
        }

    }
}
