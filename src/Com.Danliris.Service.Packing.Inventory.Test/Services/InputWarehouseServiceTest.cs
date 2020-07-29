using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
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
    public class InputWarehouseServiceTest
    {
        public InputWarehouseService GetService(IServiceProvider serviceProvider)
        {
            return new InputWarehouseService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputRepository inputRepository,
                                                         IDyeingPrintingAreaInputProductionOrderRepository inputProductionOrderRepo,
                                                         IDyeingPrintingAreaMovementRepository movementRepo,
                                                         IDyeingPrintingAreaSummaryRepository summaryRepo,
                                                         IDyeingPrintingAreaOutputRepository outputRepo,
                                                         IDyeingPrintingAreaOutputProductionOrderRepository outputProductionOrderRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepository);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputProductionOrderRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(outputRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputProductionOrderRepo);

            return spMock;
        }

        private InputWarehouseCreateViewModel ViewModelIM
        {
            get
            {
                return new InputWarehouseCreateViewModel()
                {
                    Area = "INSPECTION MATERIAL",
                    BonNo = "GJ.20.0001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 195,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<InputWarehouseProductionOrderCreateViewModel>
                    {
                        new InputWarehouseProductionOrderCreateViewModel()
                        {
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name  = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            ProductionOrderNo = "F/2020/0009",
                            CartNo = "9",
                            PackingInstruction = "d",
                            Construction = "Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100",
                            Unit = "DYEING",
                            Buyer = "ERWAN KURNIADI",
                            Color = "Grey",
                            Motif = "a",
                            UomUnit = "MTR",
                            Balance = 1,
                            HasOutputDocument = false,
                            IsChecked = false,
                            Grade = "A",
                            Remark = "a",
                            Status = "a",
                            //Material = "",
                            //MtrLength = 0,
                            //YdsLength = 0,
                            PackagingUnit ="ROLL",
                            PackagingQty = 10,
                            PackagingType ="WHITE",
                            QtyOrder = 2000,
                            OutputId = 195,

                            //InputId = 195
                        }
                    }
                };
            }
        }

        private InputWarehouseCreateViewModel ViewModelPC
        {
            get
            {
                return new InputWarehouseCreateViewModel()
                {
                    Area = "PACKING",
                    BonNo = "GJ.20.0001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 195,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<InputWarehouseProductionOrderCreateViewModel>
                    {
                        new InputWarehouseProductionOrderCreateViewModel()
                        {
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name  = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            ProductionOrderNo = "F/2020/0009",
                            CartNo = "9",
                            PackingInstruction = "d",
                            Construction = "Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100",
                            Unit = "DYEING",
                            Buyer = "ERWAN KURNIADI",
                            Color = "Grey",
                            Motif = "a",
                            UomUnit = "MTR",
                            Balance = 1,
                            HasOutputDocument = false,
                            IsChecked = false,
                            Grade = "A",
                            Remark = "a",
                            Status = "a",
                            //Material = "",
                            //MtrLength = 0,
                            //YdsLength = 0,
                            PackagingUnit ="ROLL",
                            PackagingQty = 10,
                            PackagingType ="WHITE",
                            QtyOrder = 2000,
                            OutputId = 195
                            //InputId = 195
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel InputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            }
        }
        private DyeingPrintingAreaInputModel InputModelExcel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        "GUDANG JADI",
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI",
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ExistingInputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        "GUDANG JADI",
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            }
        }

        //private DyeingPrintingAreaInputModel ExistingInputModel
        //{
        //    get
        //    {
        //        return new DyeingPrintingAreaInputModel(ViewModelIM.Date,
        //                                                "GUDANG JADI",
        //                                                ViewModelIM.Shift,
        //                                                ViewModelIM.BonNo,
        //                                                ViewModelIM.Group,
        //                                                ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
        //                                                    new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
        //                                                                                                    s.ProductionOrder.Id,
        //                                                                                                    s.ProductionOrder.No,
        //                                                                                                    s.ProductionOrder.Type,
        //                                                                                                    s.PackingInstruction,
        //                                                                                                    s.CartNo,
        //                                                                                                    s.Buyer,
        //                                                                                                    s.Construction,
        //                                                                                                    s.Unit,
        //                                                                                                    s.Color,
        //                                                                                                    s.Motif,
        //                                                                                                    s.UomUnit,
        //                                                                                                    s.Balance,
        //                                                                                                    s.HasOutputDocument,
        //                                                                                                    s.PackagingUnit,
        //                                                                                                    s.PackagingType,
        //                                                                                                    s.PackagingQty,
        //                                                                                                    s.BuyerId)).ToList());
        //    }
        //}

        private DyeingPrintingAreaOutputModel OutputModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModelIM.Date,
                                                         ViewModelIM.Area,
                                                         ViewModelIM.Shift,
                                                         ViewModelIM.BonNo,
                                                         true,
                                                         "GUDANG JADI",
                                                         ViewModelIM.Group,
                                                         "OUT",
                                                         ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area,
                                                                                                             "GUDANG JADI",
                                                                                                             true,
                                                                                                             s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark,
                                                                                                             s.Grade, s.Status, s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "",
                                                                                                             s.PackagingQty, s.PackagingType, s.PackagingUnit, 1, s.DeliveryOrderSalesNo, "", s.ProcessType.Id,
                                                                                                             s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name
                                                                                                             )).ToList());
            }
        }

        private DyeingPrintingAreaSummaryModel SummaryModel
        {
            get
            {
                return new DyeingPrintingAreaSummaryModel(ViewModelIM.Date,
                                                          ViewModelIM.Area,
                                                          ViewModelIM.Shift,
                                                          1,
                                                          ViewModelIM.BonNo,
                                                          12,
                                                          "sd",
                                                          "io1",
                                                          "rest",
                                                          "asdf",
                                                          "asdfas",
                                                          "dafsd",
                                                          "asdfsd",
                                                          "asdfsd",
                                                          123);
                //{
                //    Id = 10
                //};
            }
        }

        private RejectedInputWarehouseViewModel RejectedInputWarehouseViewModel_IM
        {
            get
            {
                return new RejectedInputWarehouseViewModel
                {
                    Id = 1,
                    Area = "INSPECTION MATERIAL",
                    BonNo = "IM.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 2,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<RejectedInputWarehouseProductionOrderViewModel>()
                    {
                        new RejectedInputWarehouseProductionOrderViewModel()
                        {
                            Id = 10,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000",
                                OrderQuantity = 12
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name  = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialWidth = "1",
                            CartNo = "9",
                            Buyer = "ANAS",
                            Construction = "a",
                            Unit = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 100,
                            PackingInstruction = "a",
                            PackagingType = "a",
                            PackagingQty = 10,
                            PackagingUnit = "a",
                            AvalALength = 10,
                            AvalBLength = 10,
                            AvalConnectionLength = 10,
                            DeliveryOrderSalesId = 3,
                            DeliveryOrderSalesNo = "a",
                            AvalType = "a",
                            AvalCartNo = "a",
                            AvalQuantityKg = 5,
                            Description = "a",
                            DeliveryNote = "a",
                            HasOutputDocument = true,
                            IsChecked = false,
                            Area = "INSPECTION MATERIAL",
                            InputId = 12,
                            OutputId = 10,
                            DyeingPrintingAreaInputProductionOrderId = 4
                        }
                    }
                };
            }
        }

        private RejectedInputWarehouseViewModel RejectedInputWarehouseViewModel_PC
        {
            get
            {
                return new RejectedInputWarehouseViewModel
                {
                    Id = 1,
                    Area = "PACKING",
                    BonNo = "PC.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 2,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<RejectedInputWarehouseProductionOrderViewModel>()
                    {
                        new RejectedInputWarehouseProductionOrderViewModel()
                        {
                            Id = 10,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000",
                                OrderQuantity = 12
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name  = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            CartNo = "9",
                            Buyer = "ANAS",
                            Construction = "a",
                            Unit = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 100,
                            PackingInstruction = "a",
                            PackagingType = "a",
                            PackagingQty = 10,
                            PackagingUnit = "a",
                            AvalALength = 10,
                            AvalBLength = 10,
                            AvalConnectionLength = 10,
                            DeliveryOrderSalesId = 3,
                            DeliveryOrderSalesNo = "a",
                            AvalType = "a",
                            AvalCartNo = "a",
                            AvalQuantityKg = 5,
                            Description = "a",
                            DeliveryNote = "a",
                            HasOutputDocument = true,
                            IsChecked = false,
                            Area = "PACKING",
                            InputId = 12,
                            OutputId = 10,
                            DyeingPrintingAreaInputProductionOrderId = 4
                        }
                    }
                };
            }
        }

        private RejectedInputWarehouseViewModel RejectedInputWarehouseViewModel_TR
        {
            get
            {
                return new RejectedInputWarehouseViewModel
                {
                    Id = 1,
                    Area = "TRANSIT",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    OutputId = 2,
                    Group = "A",
                    MappedWarehousesProductionOrders = new List<RejectedInputWarehouseProductionOrderViewModel>()
                    {
                        new RejectedInputWarehouseProductionOrderViewModel()
                        {
                            Id = 10,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000",
                                OrderQuantity = 12
                            },
                            MaterialProduct = new Material()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            MaterialConstruction = new MaterialConstruction()
                            {
                                Id = 1,
                                Name = "name"
                            },
                            ProcessType = new Application.ToBeRefactored.CommonViewModelObjectProperties.ProcessType()
                            {
                                Id = 1,
                                Name  = "s"
                            },
                            YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                            {
                                Id = 1,
                                Name = "s"
                            },
                            MaterialWidth = "1",
                            CartNo = "9",
                            Buyer = "ANAS",
                            Construction = "a",
                            Unit = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 100,
                            PackingInstruction = "a",
                            PackagingType = "a",
                            PackagingQty = 10,
                            PackagingUnit = "a",
                            AvalALength = 10,
                            AvalBLength = 10,
                            AvalConnectionLength = 10,
                            DeliveryOrderSalesId = 3,
                            DeliveryOrderSalesNo = "a",
                            AvalType = "a",
                            AvalCartNo = "a",
                            AvalQuantityKg = 5,
                            Description = "a",
                            DeliveryNote = "a",
                            HasOutputDocument = true,
                            IsChecked = false,
                            Area = "TRANSIT",
                            InputId = 12,
                            OutputId = 10,
                            DyeingPrintingAreaInputProductionOrderId = 4
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel ModelIM
        {
            get
            {
                return new DyeingPrintingAreaInputModel(RejectedInputWarehouseViewModel_IM.Date, RejectedInputWarehouseViewModel_IM.Area, RejectedInputWarehouseViewModel_IM.Shift, RejectedInputWarehouseViewModel_IM.BonNo, RejectedInputWarehouseViewModel_IM.Group, RejectedInputWarehouseViewModel_IM.MappedWarehousesProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(RejectedInputWarehouseViewModel_IM.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id,
                    s.MaterialConstruction.Name, s.MaterialWidth, s.PackagingQty, s.PackagingUnit, s.PackagingType, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, s.AvalType,
                    s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false)).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ModelPC
        {
            get
            {
                return new DyeingPrintingAreaInputModel(RejectedInputWarehouseViewModel_PC.Date, RejectedInputWarehouseViewModel_PC.Area, RejectedInputWarehouseViewModel_PC.Shift, RejectedInputWarehouseViewModel_PC.BonNo, RejectedInputWarehouseViewModel_PC.Group, RejectedInputWarehouseViewModel_PC.MappedWarehousesProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(RejectedInputWarehouseViewModel_PC.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id,
                    s.MaterialConstruction.Name, s.MaterialWidth, s.PackagingQty, s.PackagingUnit, s.PackagingType, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, s.AvalType,
                    s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false)).ToList());
            }
        }

        private DyeingPrintingAreaInputModel ModelTR
        {
            get
            {
                return new DyeingPrintingAreaInputModel(RejectedInputWarehouseViewModel_TR.Date, RejectedInputWarehouseViewModel_TR.Area, RejectedInputWarehouseViewModel_TR.Shift, RejectedInputWarehouseViewModel_TR.BonNo, RejectedInputWarehouseViewModel_TR.Group, RejectedInputWarehouseViewModel_TR.MappedWarehousesProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(RejectedInputWarehouseViewModel_TR.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.HasOutputDocument, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id,
                    s.MaterialConstruction.Name, s.MaterialWidth, s.PackagingQty, s.PackagingUnit, s.PackagingType, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, s.AvalType,
                    s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, 1, 1, "a", false, 1, 1, "a", false)).ToList());
            }
        }
        [Fact]
        public async Task Should_Success_Delete()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes = new DyeingPrintingAreaOutputModel(ViewModelIM.Date,
                                                         ViewModelIM.Area,
                                                         ViewModelIM.Shift,
                                                         ViewModelIM.BonNo,
                                                         true,
                                                         "GUDANG JADI",
                                                         ViewModelIM.Group,
                                                         "OUT",
                                                         ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area,
                                                                                                             "GUDANG JADI",
                                                                                                             true,
                                                                                                             s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark,
                                                                                                             s.Grade, s.Status, s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "",
                                                                                                             s.PackagingQty, s.PackagingType, s.PackagingUnit, 1, s.DeliveryOrderSalesNo, "", s.ProcessType.Id,
                                                                                                             s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            testinput.Id = 1;
            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
                j.DyeingPrintingAreaOutputProductionOrderId = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { testinput }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            outputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(tes.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);


            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes = new DyeingPrintingAreaOutputModel(ViewModelIM.Date,
                                                         ViewModelIM.Area,
                                                         ViewModelIM.Shift,
                                                         ViewModelIM.BonNo,
                                                         true,
                                                         "GUDANG JADI",
                                                         ViewModelIM.Group,
                                                         "OUT",
                                                         ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area,
                                                                                                             "GUDANG JADI",
                                                                                                             true,
                                                                                                             s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark,
                                                                                                             s.Grade, s.Status, s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "",
                                                                                                             s.PackagingQty, s.PackagingType, s.PackagingUnit, 1, s.DeliveryOrderSalesNo, "", s.ProcessType.Id,
                                                                                                             s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            testinput.Id = 1;
            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
                j.HasOutputDocument = true;
                j.DyeingPrintingAreaOutputProductionOrderId = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { testinput }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            outputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(tes.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);


            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(1));

        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes = new DyeingPrintingAreaOutputModel(ViewModelIM.Date,
                                                         ViewModelIM.Area,
                                                         ViewModelIM.Shift,
                                                         ViewModelIM.BonNo,
                                                         true,
                                                         "GUDANG JADI",
                                                         ViewModelIM.Group,
                                                         "OUT",
                                                         ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(ViewModelIM.Area,
                                                                                                             "GUDANG JADI",
                                                                                                             true,
                                                                                                             s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark,
                                                                                                             s.Grade, s.Status, s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id,
                                                                                                             s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "",
                                                                                                             s.PackagingQty, s.PackagingType, s.PackagingUnit, 1, s.DeliveryOrderSalesNo, "", s.ProcessType.Id,
                                                                                                             s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name)).ToList());
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        ViewModelIM.Shift,
                                                        ViewModelIM.BonNo,
                                                        ViewModelIM.Group,
                                                        ViewModelIM.MappedWarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaInputProductionOrderModel(ViewModelIM.Area,
                                                                                                            s.ProductionOrder.Id,
                                                                                                            s.ProductionOrder.No,
                                                                                                            s.ProductionOrder.Type,
                                                                                                            s.PackingInstruction,
                                                                                                            s.CartNo,
                                                                                                            s.Buyer,
                                                                                                            s.Construction,
                                                                                                            s.Unit,
                                                                                                            s.Color,
                                                                                                            s.Motif,
                                                                                                            s.UomUnit,
                                                                                                            s.Balance,
                                                                                                            s.HasOutputDocument,
                                                                                                            s.PackagingUnit,
                                                                                                            s.PackagingType,
                                                                                                            s.PackagingQty,
                                                                                                            s.Grade,
                                                                                                            s.ProductionOrder.OrderQuantity,
                                                                                                            s.BuyerId,
                                                                                                            s.Id,
                                                                                                            s.Remark,
                                                                                                            s.Balance,
                                                                                                            s.MaterialProduct.Id,
                                                                                                            s.MaterialProduct.Name,
                                                                                                            s.MaterialConstruction.Id,
                                                                                                            s.MaterialConstruction.Name,
                                                                                                            s.MaterialWidth,
                                                                                                            s.ProcessType.Id,
                                                                                                            s.ProcessType.Name,
                                                                                                            s.YarnMaterial.Id,
                                                                                                            s.YarnMaterial.Name)).ToList());
            testinput.Id = 1;
            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
                j.DyeingPrintingAreaOutputProductionOrderId = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { testinput }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            outputProductionOrderRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            outputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(tes.DyeingPrintingAreaOutputProductionOrders.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);


            var result = await service.Update(1, ViewModelIM);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_InsertNewWarehouse()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        "IN",
                                                        ViewModelIM.OutputId,
                                                        ViewModelIM.BonNo,
                                                        item.ProductionOrder.Id,
                                                        item.ProductionOrder.No,
                                                        item.CartNo,
                                                        item.Buyer,
                                                        item.Construction,
                                                        item.Unit,
                                                        item.Color,
                                                        item.Motif,
                                                        item.UomUnit,
                                                        item.Balance)
                 }.AsQueryable());

            //summaryRepoMock.Setup(s => s.ReadAll())
            //     .Returns(new List<DyeingPrintingAreaSummaryModel>() { SummaryModel }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagParentOnlyAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            //inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            //summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
            //     .ReturnsAsync(1);

            //outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_InsertNewWarehouse_SummaryNull()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            //Mock for totalCurrentYear
            outputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            //summaryRepoMock.Setup(s => s.ReadAll())
            //     .Returns(new List<DyeingPrintingAreaSummaryModel>() { SummaryModel }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagParentOnlyAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), true))
                 .ReturnsAsync(1);

            //inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            //summaryRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaSummaryModel>()))
            //     .ReturnsAsync(1);

            //outputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputModel>()))
            //    .ReturnsAsync(1);
            //outputRepoMock.Setup(s => s.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaOutputModel>() { OutputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateExistingWarehouse()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ExistingInputModel }.AsQueryable());

            //inputRepoMock.Setup(o => o.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date,
                                                        ViewModelIM.Area,
                                                        "IN",
                                                        ViewModelIM.OutputId,
                                                        ViewModelIM.BonNo,
                                                        item.ProductionOrder.Id,
                                                        item.ProductionOrder.No,
                                                        item.CartNo,
                                                        item.Buyer,
                                                        item.Construction,
                                                        item.Unit,
                                                        item.Color,
                                                        item.Motif,
                                                        item.UomUnit,
                                                        item.Balance)
                 }.AsQueryable());

            inputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), SummaryModel))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(item.Id, true))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateExistingWarehouse_SummaryNull()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ExistingInputModel }.AsQueryable());

            //inputRepoMock.Setup(o => o.ReadAll())
            //    .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>()
                 {

                 }.AsQueryable());

            inputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), SummaryModel))
                 .ReturnsAsync(1);

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(item.Id, true))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Create(ViewModelIM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(InputModel);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaInputModel));

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Success_ReadProductionOrders()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputProductionOrderRepoMock.Setup(s => s.ReadAll()).Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM", "GUDANG JADI", false, 1, "a", "e", 1,"rr", "1", "as", "test", "unit", "color", "motif", "mtr", "rem", "a", "a", 1, 1, 1,1,"a",1,"a","1","",1,"a","a",1,"a","a",1,"a",1,"a")
            }.AsQueryable());

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GetOutputPreWarehouseProductionOrders();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_IM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM_Previous_Summary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", item.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_IM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_PC()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);


            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_PC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_PC_Duplicate_Shift()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelPC }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_PC);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_TR()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelTR }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_TR);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_TR_Duplicate_Shift()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelTR }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelTR }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelPC.Date, ViewModelPC.Area, "IN", ViewModelPC.OutputId, ViewModelPC.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_TR);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_SP()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var vm = RejectedInputWarehouseViewModel_TR;
            vm.Area = "SHIPPING";
            foreach (var spp in vm.MappedWarehousesProductionOrders)
            {
                spp.Area = "SHIPPING";
            }

            var model = ModelTR;
            model.SetArea("SHIPPING", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("SHIPPING", "", "");
            }

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = vm.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_SP_Duplicate_Shift()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var vm = RejectedInputWarehouseViewModel_TR;
            vm.Area = "SHIPPING";
            foreach (var spp in vm.MappedWarehousesProductionOrders)
            {
                spp.Area = "SHIPPING";
            }

            var model = ModelTR;
            model.SetArea("SHIPPING", "", "");
            foreach (var sppModel in model.DyeingPrintingAreaInputProductionOrders)
            {
                sppModel.SetArea("SHIPPING", "", "");
            }

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelPC.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(vm.Date, vm.Area, "IN", vm.OutputId, vm.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(vm);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Reject_IM_Duplicate_Shift()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", ViewModelIM.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_IM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Reject_IM_Duplicate_Shift_Previous_Summary()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaInputModel>()))
                .ReturnsAsync(1);

            inputRepoMock.Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaInputModel>() { ModelIM }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            outputRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var item = ViewModelIM.MappedWarehousesProductionOrders.FirstOrDefault();

            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModelIM.Date, ViewModelIM.Area, "IN", item.OutputId, ViewModelIM.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            outputProductionOrderRepoMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = await service.Reject(RejectedInputWarehouseViewModel_IM);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { InputModelExcel }.AsQueryable());


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(InputModelExcel.Date.AddDays(-1), InputModelExcel.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll2()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { InputModelExcel }.AsQueryable());


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(InputModelExcel.Date.AddDays(-1), null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll3()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { InputModelExcel }.AsQueryable());


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, InputModelExcel.Date.AddDays(1), 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GenerateExcelAll4()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel> { InputModelExcel }.AsQueryable());


            var service = GetService(GetServiceProvider(inputRepoMock.Object,
                                                        inputProductionOrderRepoMock.Object,
                                                        movementRepoMock.Object,
                                                        summaryRepoMock.Object,
                                                        outputRepoMock.Object,
                                                        outputProductionOrderRepoMock.Object).Object);

            var result = service.GenerateExcelAll(null, null, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_Compare()
        {
            var y = new ProductionOrderItemListDetailViewModel
            {
                Id = 1
            };
            var x = new ProductionOrderItemListDetailViewModel
            {
                Id = 1
            };
            PackingComparer compare = new PackingComparer();
            var test = compare.Equals(y, x);
            Assert.True(test);

        }

        [Fact]
        public void ValidateVM()
        {

            var vm = new InputWarehouseProductionOrderCreateViewModel()
            {
            };

            Assert.Null(vm.MaterialProduct);
            Assert.Null(vm.MaterialConstruction);
            Assert.Null(vm.MaterialWidth);
        }
    }
}
