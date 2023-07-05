using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using OfficeOpenXml;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Newtonsoft.Json.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingService : IOutputPackagingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDPWarehousePreInputRepository _preInputrepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IDyeingPrintingAreaReferenceRepository _areaReferenceRepository;

        public enum AreaAbbr
        {
            [Description("IM")]
            INSPECTIONMATERIAL = 0,
            [Description("TR")]
            TRANSIT,
            [Description("PC")]
            PACKING,
            [Description("GJ")]
            GUDANGJADI,
            [Description("GA")]
            GUDANGAVAL,
            [Description("SP")]
            SHIPPING,
        }

        public OutputPackagingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _fabricPackingSKUService = serviceProvider.GetService<IFabricPackingSKUService>();
            _areaReferenceRepository = serviceProvider.GetService<IDyeingPrintingAreaReferenceRepository>();
            _preInputrepository = serviceProvider.GetService<IDPWarehousePreInputRepository>();
        }

        private async Task<OutputPackagingViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            if (model.Type == DyeingPrintingArea.ADJ_IN || model.Type == DyeingPrintingArea.ADJ_OUT)
            {
                var vm = new OutputPackagingViewModel()
                {
                    Type = DyeingPrintingArea.ADJ,
                    AdjType = model.Type,
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,
                    PackagingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputPackagingProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Balance = s.Balance,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        PackingInstruction = s.PackingInstruction,
                        Remark = s.Remark,
                        ProductionMachine = s.ProductionMachine,
                        Status = s.Status,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        MaterialProduct = new Material()
                        {
                            Name = s.MaterialName,
                            Id = s.MaterialId
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Id = s.MaterialConstructionId,
                            Name = s.MaterialConstructionName
                        },
                        ProductTextile = new ProductTextile()
                        { 
                            Id = s.ProductTextileId,
                            Code = s.ProductTextileCode,
                            Name = s.ProductTextileName
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        PackagingQTY = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        PackingLength = s.PackagingLength,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                        QtyOut = s.Balance,
                        ProductionOrderNo = s.ProductionOrderNo,
                        Keterangan = s.Description,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DateIn=s.DateIn,
                        DateOut=s.DateOut
                    }).ToList(),
                    PackagingProductionOrdersAdj = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new InputPlainAdjPackagingProductionOrder()
                    {
                        Balance = s.Balance,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        Grade = s.Grade,
                        PackingInstruction = s.PackingInstruction,
                        Remark = s.Remark,
                        Status = s.Status,
                        Id = s.Id,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                        },
                        PackingLength = s.PackagingLength,
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        MaterialWidth = s.MaterialWidth,
                        FinishWidth = s.FinishWidth,
                        MaterialObj = new Material()
                        {
                            Name = s.MaterialName,
                            Id = s.MaterialId
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Id = s.MaterialConstructionId,
                            Name = s.MaterialConstructionName
                        },
                        ProductTextile = new ProductTextile()
                        {
                            Id = s.ProductTextileId,
                            Code = s.ProductTextileCode,
                            Name = s.ProductTextileName
                        },
                        Unit = s.Unit,
                        AtQty = s.PackagingLength,
                        UomUnit = s.UomUnit,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        PackagingQty = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        NoDocument = s.AdjDocumentNo,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DateIn = s.DateIn,
                        DateOut = s.DateOut
                    }).ToList()
                };

                foreach (var item in vm.PackagingProductionOrdersAdj)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        item.BalanceRemains = inputData.BalanceRemains + (item.Balance * -1);
                    }
                }

                return vm;
            }
            else
            {
                var vm = new OutputPackagingViewModel()
                {
                    Type = DyeingPrintingArea.OUT,
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,
                    PackagingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputPackagingProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Balance = s.Balance,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        PackingInstruction = s.PackingInstruction,
                        Remark = s.Remark,
                        ProductionMachine = s.ProductionMachine,
                        Status = s.Status,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                        },
                        PackingLength = s.PackagingLength,
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        MaterialProduct = new Material()
                        {
                            Name = s.MaterialName,
                            Id = s.MaterialId
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Id = s.MaterialConstructionId,
                            Name = s.MaterialConstructionName
                        },
                        ProductTextile = new ProductTextile()
                        {
                            Id = s.ProductTextileId,
                            Code = s.ProductTextileCode,
                            Name = s.ProductTextileName
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        PackagingQTY = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                        QtyOut = s.Balance,
                        ProductionOrderNo = s.ProductionOrderNo,
                        Keterangan = s.Description,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DateIn = s.DateIn,
                        DateOut = s.DateOut,
                        NextAreaInputStatus = s.NextAreaInputStatus

                    }).ToList()
                };

                //foreach (var item in vm.PackagingProductionOrders.GroupBy(s => s.DyeingPrintingAreaInputProductionOrderId))
                //{
                //    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.Key);
                //    var sumQtyOut = item.Sum(s => s.QtyOut);
                //    if (inputData != null)
                //    {
                //        foreach (var detail in item)
                //        {
                //            detail.Balance = inputData.Balance;
                //            detail.BalanceRemains = inputData.BalanceRemains;
                //            detail.PreviousBalance = inputData.BalanceRemains + sumQtyOut;
                //        }

                //    }
                //}

                foreach (var item in vm.PackagingProductionOrders.GroupBy(s => new { s.ProductionOrderNo, s.Grade }))
                {
                    var inputData = _inputProductionOrderRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.PACKING && s.ProductionOrderNo == item.Key.ProductionOrderNo && s.Grade == item.Key.Grade);
                    var sumQtyOut = item.Sum(s => s.QtyOut);
                    foreach (var detail in item)
                    {
                        if (vm.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            detail.Balance = inputData.Sum(e => e.Balance) + sumQtyOut;
                        }
                        else
                        {
                            detail.Balance = inputData.Sum(e => e.Balance);
                        }
                        detail.BalanceRemains = inputData.Sum(e => e.BalanceRemains);
                        detail.PreviousBalance = inputData.Sum(e => e.BalanceRemains) + sumQtyOut;
                    }

                }

                return vm;
            }
        }

        private async Task<OutputPackagingViewModel> MapToViewModelBon(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputPackagingViewModel()
            {
                Type = DyeingPrintingArea.OUT,
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                DestinationArea = model.DestinationArea,
                HasNextAreaDocument = model.HasNextAreaDocument,
                Group = model.Group,
                PackagingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.GroupBy( x => new { x.ProductionOrderNo, x.Grade, x.NextAreaInputStatus}).Select(s => new OutputPackagingProductionOrderViewModel()
                {
                    Active = s.First().Active,
                    LastModifiedUtc = s.First().LastModifiedUtc,
                    Balance = s.Sum(r => r.Balance),
                    HasNextAreaDocument = s.First().HasNextAreaDocument,
                    Buyer = s.First().Buyer,
                    BuyerId = s.First().BuyerId,
                    CartNo = s.First().CartNo,
                    Color = s.First().Color,
                    Construction = s.First().Construction,
                    DyeingPrintingAreaInputProductionOrderId = s.First().DyeingPrintingAreaInputProductionOrderId,
                    CreatedAgent = s.First().CreatedAgent,
                    CreatedBy = s.First().CreatedBy,
                    CreatedUtc = s.First().CreatedUtc,
                    DeletedAgent = s.First().DeletedAgent,
                    DeletedBy = s.First().DeletedBy,
                    DeletedUtc = s.First().DeletedUtc,
                    Grade = s.Key.Grade,
                    PackingInstruction = s.First().PackingInstruction,
                    Remark = s.First().Remark,
                    ProductionMachine = s.First().ProductionMachine,
                    Status = s.First().Status,
                    Id = s.First().Id,
                    IsDeleted = s.First().IsDeleted,
                    LastModifiedAgent = s.First().LastModifiedAgent,
                    LastModifiedBy = s.First().LastModifiedBy,
                    Motif = s.First().Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.First().ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.First().ProductionOrderType,
                        OrderQuantity = s.First().ProductionOrderOrderQuantity,
                    },
                    PackingLength = s.First().PackagingLength,
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.First().ProcessTypeId,
                        Name = s.First().ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.First().YarnMaterialId,
                        Name = s.First().YarnMaterialName
                    },
                    MaterialWidth = s.First().MaterialWidth,
                    MaterialOrigin = s.First().MaterialOrigin,
                    FinishWidth = s.First().FinishWidth,
                    MaterialProduct = new Material()
                    {
                        Name = s.First().MaterialName,
                        Id = s.First().MaterialId
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = s.First().MaterialConstructionId,
                        Name = s.First().MaterialConstructionName
                    },
                    Unit = s.First().Unit,
                    UomUnit = s.First().UomUnit,
                    PackagingQTY = s.Sum( r => r.PackagingQty),
                    PackagingType = s.First().PackagingType,
                    PackagingUnit = s.First().PackagingUnit,
                    QtyOrder = s.First().ProductionOrderOrderQuantity,
                    QtyOut = s.Sum( r => r.Balance),
                    ProductionOrderNo = s.First().ProductionOrderNo,
                    Keterangan = s.First().Description,
                    ProductSKUId = s.First().ProductSKUId,
                    FabricSKUId = s.First().FabricSKUId,
                    ProductSKUCode = s.First().ProductSKUCode,
                    HasPrintingProductSKU = s.First().HasPrintingProductSKU,
                    ProductPackingId = s.First().ProductPackingId,
                    FabricPackingId = s.First().FabricPackingId,
                    ProductPackingCode = s.First().ProductPackingCode,
                    HasPrintingProductPacking = s.First().HasPrintingProductPacking,
                    DateIn = s.First().DateIn,
                    DateOut = s.First().DateOut,
                    NextAreaInputStatus = s.First().NextAreaInputStatus

                }).ToList()
            };

            //foreach (var item in vm.PackagingProductionOrders.GroupBy(s => s.DyeingPrintingAreaInputProductionOrderId))
            //{
            //    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.Key);
            //    var sumQtyOut = item.Sum(s => s.QtyOut);
            //    if (inputData != null)
            //    {
            //        foreach (var detail in item)
            //        {
            //            detail.Balance = inputData.Balance;
            //            detail.BalanceRemains = inputData.BalanceRemains;
            //            detail.PreviousBalance = inputData.BalanceRemains + sumQtyOut;
            //        }

            //    }
            //}

            foreach (var item in vm.PackagingProductionOrders.GroupBy(s => new { s.ProductionOrderNo, s.Grade }))
            {
                var inputData = _inputProductionOrderRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.PACKING && s.ProductionOrderNo == item.Key.ProductionOrderNo && s.Grade == item.Key.Grade);
                var sumQtyOut = item.Sum(s => s.QtyOut);
                foreach (var detail in item)
                {
                    if (vm.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {
                        detail.Balance = inputData.Sum(e => e.Balance) + sumQtyOut;
                    }
                    else
                    {
                        detail.Balance = inputData.Sum(e => e.Balance);
                    }
                    detail.BalanceRemains = inputData.Sum(e => e.BalanceRemains);
                    detail.PreviousBalance = inputData.Sum(e => e.BalanceRemains) + sumQtyOut;
                }

            }

            return vm;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationAreaName)
        {
            string sourceArea = AreaAbbr.PACKING.ToDescription();
            if (destinationAreaName == DyeingPrintingArea.TRANSIT)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == DyeingPrintingArea.INSPECTIONMATERIAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == DyeingPrintingArea.GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == DyeingPrintingArea.GUDANGAVAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == DyeingPrintingArea.SHIPPING)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }
        public string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_IN, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_OUT, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }
        public async Task<int> Create(OutputPackagingViewModel viewModel)
        {
            int result = 0;
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.PACKING && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            viewModel.PackagingProductionOrders = viewModel.PackagingProductionOrders.Where(s => s.IsSave).ToList();

            //get BonNo with shift
            var hasBonNoWithShift = _repository.ReadAll().Where(x => x.Shift == viewModel.Shift && x.Area == DyeingPrintingArea.PACKING && x.Date == viewModel.Date).FirstOrDefault();
            DyeingPrintingAreaOutputModel model = new DyeingPrintingAreaOutputModel();
            if (hasBonNoWithShift == null)
            {

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, DyeingPrintingArea.OUT, viewModel.PackagingProductionOrders.Select(s =>
                      new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                      s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.QtyOut, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, 0, s.Id, s.BuyerId,
                      s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name,
                     s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.MaterialOrigin)).ToList());
                result += await _repository.InsertAsync(model);
            }
            else
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, hasBonNoWithShift.BonNo, false, viewModel.DestinationArea, viewModel.Group, DyeingPrintingArea.OUT, viewModel.PackagingProductionOrders.Select(s =>
                      new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                      s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.QtyOut, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, hasBonNoWithShift.Id, s.Id, s.BuyerId,
                      s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name,
                     s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.MaterialOrigin)).ToList());
                model.Id = hasBonNoWithShift.Id;
                bonNo = model.BonNo;
            }
            var modelInput = _inputRepository.ReadAll().Where(x => x.BonNo == viewModel.BonNoInput && x.Area == DyeingPrintingArea.PACKING);

            var modelInputProductionOrder = _inputProductionOrderRepository.ReadAll().Join(modelInput,
                                                                                s => s.DyeingPrintingAreaInputId,
                                                                                s2 => s2.Id,
                                                                                (s, s2) => s);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var vmItem = viewModel.PackagingProductionOrders.FirstOrDefault(s => s.ProductionOrder.Id == item.ProductionOrderId);
                //update balance SPP Input
                var inputSPP = _inputProductionOrderRepository.ReadAll().FirstOrDefault(x => x.Id == item.DyeingPrintingAreaInputProductionOrderId);
                inputSPP.SetBalanceRemains(inputSPP.BalanceRemains - item.Balance, "REPOSITORY", "");
                inputSPP.SetHasOutputDocument(true, "REPOSITORY", "");
                result += await _inputProductionOrderRepository.UpdateAsync(inputSPP.Id, inputSPP);

                //var previousProductionOrder = modelInputProductionOrder.Where(x => x.ProductionOrderNo == vmItem.ProductionOrder.No).FirstOrDefault();
                //var lastBalance = previousProductionOrder.Balance - vmItem.QtyOut;
                //previousProductionOrder.SetBalance(lastBalance,"REPOSITORY","");

                //result += await _inputProductionOrderRepository.UpdateAsync(previousProductionOrder.Id,previousProductionOrder);
                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);


                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName);

                //var updateBalance = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNoInput, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                // new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                // s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance-vmItem.QtyOut, false, s.QtyOrder, s.Grade)).ToList());

                if (hasBonNoWithShift != null)
                    result += await _outputProductionOrderRepository.InsertAsync(item);

                result += await _movementRepository.InsertAsync(movementModel);

            }

            return result;
        }
        private async Task<int> InsertAdj(OutputPackagingViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.PackagingProductionOrdersAdj.All(d => d.Balance > 0))
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.PACKING && s.Type == DyeingPrintingArea.ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.PackagingProductionOrdersAdj.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.PACKING && s.Type == DyeingPrintingArea.ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.PackagingProductionOrdersAdj.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _repository.GetDbSet().AsNoTracking()
                  .FirstOrDefault(s => s.Area == DyeingPrintingArea.PACKING && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type);

            if (model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group, type,
                    viewModel.PackagingProductionOrdersAdj.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                        item.MaterialObj.Id, item.MaterialObj.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.NoDocument,
                        item.PackagingType, item.PackagingQty, item.PackagingUnit, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.FinishWidth,item.DateIn,viewModel.Date, item.MaterialOrigin)).ToList());

                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    result += await _inputProductionOrderRepository.UpdateDateOutsync(item.DyeingPrintingAreaInputProductionOrderId, viewModel.Date);

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.PackagingProductionOrdersAdj)
                {
                    result += await _inputProductionOrderRepository.UpdateDateOutsync(item.DyeingPrintingAreaInputProductionOrderId, viewModel.Date);

                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                        item.MaterialObj.Id, item.MaterialObj.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.NoDocument,
                        item.PackagingType, item.PackagingQty, item.PackagingUnit, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.FinishWidth,item.DateIn,viewModel.Date, item.MaterialOrigin);

                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);


                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, item.Grade);


                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }



            return result;
        }


        //public async Task<int> CreateV2(OutputPackagingViewModel viewModel)
        //{
        //    int result = 0;
        //    int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.PACKING && s.DestinationArea == viewModel.DestinationArea
        //        && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);
        //    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
        //    viewModel.PackagingProductionOrders = viewModel.PackagingProductionOrders.Where(s => s.IsSave).ToList();
        //    List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();
        //    //get BonNo with shift
        //    var hasBonNoWithShift = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.DestinationArea == viewModel.DestinationArea
        //        && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == DyeingPrintingArea.OUT).FirstOrDefault();
        //    DyeingPrintingAreaOutputModel model = new DyeingPrintingAreaOutputModel();
        //    foreach (var item in viewModel.PackagingProductionOrders)
        //    {
        //        //get spp in that will be decrease balance
        //        var sppInDecrease = _inputProductionOrderRepository.ReadAll().Where(x => x.ProductionOrderId == item.ProductionOrder.Id && x.Area == DyeingPrintingArea.PACKING && !x.HasOutputDocument && x.BalanceRemains > 0).OrderBy(x => x.Id).ToList();
        //        List<DyeingPrintingAreaInputProductionOrderModel> listSppHasDescrease = new List<DyeingPrintingAreaInputProductionOrderModel>();
        //        var qtyOut = item.QtyOut;
        //        if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
        //        {

        //            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, qtyOut);
        //        }
        //        else
        //        {

        //            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, qtyOut);
        //        }
        //        //foreach (var spp in sppInDecrease)
        //        //{
        //        //    if (qtyOut <= 0)
        //        //        break;
        //        //    else
        //        //    {
        //        //        double qtyDecrase = 0;
        //        //        if (qtyOut >= spp.BalanceRemains)
        //        //        //balance remains empty
        //        //        {
        //        //            qtyDecrase = spp.BalanceRemains;
        //        //            qtyOut -= qtyDecrase;
        //        //            listSppHasDescrease.Add(spp);
        //        //            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(spp.Id, true);

        //        //        }
        //        //        else
        //        //        //balance remains has residu
        //        //        {
        //        //            qtyDecrase = qtyOut;
        //        //            qtyOut -= qtyDecrase;
        //        //            spp.SetBalanceRemains(qtyDecrase, "OUTPUTPACKAGING", "SERVICE");
        //        //            listSppHasDescrease.Add(spp);
        //        //            result += await _inputProductionOrderRepository.UpdateAsync(spp.Id, spp);
        //        //        }
        //        //    }
        //        //}

        //        var jsonSetting = new JsonSerializerSettings();
        //        jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //        jsonSetting.NullValueHandling = NullValueHandling.Ignore;
        //        jsonSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
        //        jsonSetting.Formatting = Formatting.None;
        //        jsonSetting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        //        jsonSetting.FloatParseHandling = FloatParseHandling.Double;

        //        var jsonLIstSppHasDecrease = JsonConvert.SerializeObject(listSppHasDescrease, Formatting.Indented, jsonSetting);

        //        var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
        //        {
        //            FabricSKUId = item.FabricSKUId,
        //            PackingType = item.PackagingUnit,
        //            Quantity = (int)item.PackagingQTY,
        //            Length = item.PackingLength
        //        });

        //        string packingCodes = string.Join(',', packingData.ProductPackingCodes);

        //        var productionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
        //             item.Motif, item.UomUnit, item.Remark,item.ProductionMachine, item.Grade, item.Status, item.QtyOut, item.PackingInstruction, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity,
        //             item.PackagingType, item.PackagingQTY, item.PackagingUnit, item.QtyOrder, item.Keterangan, 0, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId, jsonLIstSppHasDecrease,
        //             item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.ProcessType.Id, item.ProcessType.Name,
        //             item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false, item.PackingLength);
        //        productionOrders.Add(productionOrder);
        //    }
        //    if (hasBonNoWithShift == null)
        //    {
        //        model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, DyeingPrintingArea.OUT, productionOrders);
        //        result += await _repository.InsertAsync(model);
        //    }
        //    else
        //    {
        //        foreach (var po in productionOrders)
        //        {
        //            po.DyeingPrintingAreaOutputId = hasBonNoWithShift.Id;
        //        }

        //        model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, hasBonNoWithShift.BonNo, false, viewModel.DestinationArea, viewModel.Group, DyeingPrintingArea.OUT, productionOrders);
        //        model.Id = hasBonNoWithShift.Id;
        //        bonNo = model.BonNo;
        //    }

        //    foreach (var items in model.DyeingPrintingAreaOutputProductionOrders)
        //    {
        //        if (hasBonNoWithShift != null)
        //            result += await _outputProductionOrderRepository.InsertAsync(items);

        //        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, items.ProductionOrderId, items.ProductionOrderNo,
        //            items.CartNo, items.Buyer, items.Construction, items.Unit, items.Color, items.Motif, items.UomUnit, items.Balance, items.Id, items.ProductionOrderType, items.Grade);

        //        result += await _movementRepository.InsertAsync(movementModel);


        //    }

        //    return result;
        //}

        public async Task<int> CreateV2(OutputPackagingViewModel viewModel)
        {
            int result = 0;
            viewModel.PackagingProductionOrders = viewModel.PackagingProductionOrders.Where(s => s.IsSave).ToList();
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.PACKING && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == DyeingPrintingArea.OUT);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.PACKING && s.DestinationArea == viewModel.DestinationArea
                    && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);

                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

                foreach (var item in viewModel.PackagingProductionOrders)
                {
                   
                    var transform = await _inputProductionOrderRepository.UpdatePackingFromOut(viewModel.DestinationArea, item.ProductionOrderNo, item.Grade, item.QtyOut);
                    
                    result += transform.Item1;
                    var prevPacking = JsonConvert.SerializeObject(transform.Item2);

                    var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = item.FabricSKUId,
                        PackingType = item.PackagingUnit,
                        Quantity = (int)item.PackagingQTY,
                        Length = item.PackingLength,
                        Description = item.Keterangan
                    });

                    string packingCodes = string.Join(',', packingData.ProductPackingCodes);

                    var productionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                        item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, item.Grade, item.Status, item.QtyOut, item.PackingInstruction, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity,
                        item.PackagingType, item.PackagingQTY, item.PackagingUnit, item.QtyOrder, item.Keterangan, 0, 0, item.BuyerId, prevPacking,
                        item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.ProcessType.Id, item.ProcessType.Name,
                        item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false,

                        item.PackingLength, item.FinishWidth, item.DateIn, viewModel.Date, item.MaterialOrigin, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name);


                    //productionOrder.SetPackagingQuantity(packingData.ProductPackingCodes.Count);
                    //productionOrder.SetPackagingQuantityBalance(packingData.ProductPackingCodes.Count, 0);
                    productionOrders.Add(productionOrder);

                    var preInput = _preInputrepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.ProductPackingCode == packingData.ProductPackingCode);
                    if (viewModel.DestinationArea == "GUDANG JADI")
                    {
                        if (preInput == null)
                        {
                            var preInputModel = new DPWarehousePreInputModel(
                                item.QtyOut, item.QtyOut, 0, 0, item.BuyerId, item.Buyer, item.Color, item.Construction, item.Grade, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialWidth,
                                item.Motif, item.PackingInstruction, item.PackagingQTY, item.PackagingQTY, 0, 0, item.PackingLength, item.PackagingType, item.PackagingUnit, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity,
                                item.ProductionOrder.CreatedUtc, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.Unit, item.UomUnit, item.Remark, item.Keterangan,
                                packingData.ProductSKUId, packingData.FabricSKUId, packingData.ProductSKUCode, packingData.ProductPackingId, packingData.FabricPackingId, packingData.ProductPackingCode, item.MaterialOrigin, item.FinishWidth
                                );

                            result += await _preInputrepository.InsertAsync(preInputModel);
                        }
                        else
                        {
                            await _preInputrepository.UpdateBalance(preInput.Id, item.QtyOut);
                            await _preInputrepository.UpdateBalanceRemainsIn(preInput.Id, item.QtyOut);
                        }
                    }


                }
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, DyeingPrintingArea.OUT, productionOrders);
                result += await _repository.InsertAsync(model);

                foreach (var items in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, items.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, items.ProductionOrderId, items.ProductionOrderNo,
                        items.CartNo, items.Buyer, items.Construction, items.Unit, items.Color, items.Motif, items.UomUnit, items.Balance, items.Id, items.ProductionOrderType, items.ProductTextileId, items.ProductTextileCode, items.ProductTextileName, items.Grade);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            else
            {
                foreach (var item in viewModel.PackagingProductionOrders)
                {
                    DyeingPrintingAreaOutputProductionOrderModel modelItem = null;

                   
                    var transform = await _inputProductionOrderRepository.UpdatePackingFromOut(viewModel.DestinationArea, item.ProductionOrderNo, item.Grade, item.QtyOut);
                    result += transform.Item1;
                    var prevPacking = JsonConvert.SerializeObject(transform.Item2);

                    var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                    {
                        FabricSKUId = item.FabricSKUId,
                        PackingType = item.PackagingUnit,
                        Quantity = (int)item.PackagingQTY,
                        Length = item.PackingLength,
                        Description = item.Keterangan
                    });

                    string packingCodes = string.Join(',', packingData.ProductPackingCodes);


                    modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                        item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, item.Grade, item.Status, item.QtyOut, item.PackingInstruction, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity,
                        item.PackagingType, item.PackagingQTY, item.PackagingUnit, item.QtyOrder, item.Keterangan, 0, 0, item.BuyerId, prevPacking,
                        item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.ProcessType.Id, item.ProcessType.Name,
                        item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes, false,
                        item.PackingLength, item.FinishWidth, item.DateIn, viewModel.Date, item.MaterialOrigin, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name);

                    //modelItem.SetPackagingQuantity(packingData.ProductPackingCodes.Count);
                    //modelItem.SetPackagingQuantityBalance(packingData.ProductPackingCodes.Count, 0);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;
                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                   
                    if( viewModel.DestinationArea == "GUDANG JADI")
                    {
                        var preInput = _preInputrepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.ProductPackingCode == packingData.ProductPackingCode);
                        if (preInput == null)
                        {
                            var preInputModel = new DPWarehousePreInputModel(
                                item.QtyOut, item.QtyOut, 0, 0, item.BuyerId, item.Buyer, item.Color, item.Construction, item.Grade, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialWidth,
                                item.Motif, item.PackingInstruction, item.PackagingQTY, item.PackagingQTY, 0, 0, item.PackingLength, item.PackagingType, item.PackagingUnit, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity,
                                item.ProductionOrder.CreatedUtc, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.Unit, item.UomUnit, item.Remark, item.Keterangan,
                                packingData.ProductSKUId, packingData.FabricSKUId, packingData.ProductSKUCode, packingData.ProductPackingId, packingData.FabricPackingId, packingData.ProductPackingCode, item.MaterialOrigin, item.FinishWidth
                                );

                            result += await _preInputrepository.InsertAsync(preInputModel);
                        }
                        else
                        {
                            await _preInputrepository.UpdateBalance(preInput.Id, item.QtyOut);
                            await _preInputrepository.UpdateBalanceRemainsIn(preInput.Id, item.QtyOut);
                        }
                    }
                    


                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.QtyOut, modelItem.Id, item.ProductionOrder.Type, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, item.Grade);

                    result += await _movementRepository.InsertAsync(movementModel);

                    var areaReference = new DyeingPrintingAreaReferenceModel("OUT", item.Id, item.DyeingPrintingAreaInputProductionOrderId);
                    await _areaReferenceRepository.InsertAsync(areaReference);
                }
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == PACKING &&
            //    (((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument,
                PackagingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPackagingProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    MaterialWidth = d.MaterialWidth,
                    MaterialOrigin = d.MaterialOrigin,
                    FinishWidth = d.FinishWidth,
                    MaterialProduct = new Material()
                    {
                        Name = d.MaterialName,
                        Id = d.MaterialId
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = d.MaterialConstructionId,
                        Name = d.MaterialConstructionName
                    },
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    Status = d.Status,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit,
                    PackagingQTY = d.PackagingQty,
                    PackagingType = d.PackagingType,
                    PackagingUnit = d.PackagingUnit,
                    Material = d.Construction,
                    QtyOrder = d.ProductionOrderOrderQuantity,
                    ProductionOrderNo = d.ProductionOrderNo,
                    Keterangan = d.Description,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputPackagingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputPackagingViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public async Task<OutputPackagingViewModel> ReadByIdBon(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputPackagingViewModel vm = await MapToViewModelBon(model);

            return vm;
        }


        public async Task<MemoryStream> GenerateExcel(int id,int timeZone)
        {

            var model = await _repository.ReadByIdAsync(id);
            var query = model.DyeingPrintingAreaOutputProductionOrders;
            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"ProductionOrderNo","No SPP" },
                {"DateOut","Tanggal Keluar" },
                {"ProductionOrderOrderQuantity","Qty Order" },
                {"Buyer","Buyer" },
                {"Unit","Unit"},
                {"MaterialOrigin","Asal Material" },
                {"Construction","Material "},
                {"Color","Warna"},
                {"Motif","Motif"},
                {"ProductionMachine","Mesin Produksi"},
                {"PackagingType","Jenis"},
                {"Grade","Grade"},
                {"PackagingQty","Qty Packaging"},
                {"PackagingUnit","Packaging"},
                {"UomUnit","Satuan"},
                {"Balance","Saldo"},
                //{"Balance","Qty Keluar" },
                {"Description","Keterangan" },
                {"Menyerahkan","Menyerahkan" },
                {"Menerima","Menerima" },
            };
            var listClass = query.ToList().FirstOrDefault().GetType().GetProperties();
            #endregion
            #region Assign Column
            foreach (var prop in mappedClass.Select((item, index) => new { Index = index, Items = item }))
            {
                string fieldName = prop.Items.Value;
                dt.Columns.Add(new DataColumn() { ColumnName = fieldName, DataType = typeof(string) });
            }
            #endregion
            #region Assign Data
            foreach (var item in query)
            {
                List<string> data = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName && column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima");
                    string valueClass = "";
                    if (searchMappedClass != null && searchMappedClass != null && searchMappedClass.FirstOrDefault().Key != null)
                    {
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        
                        var searchValue = searchProperty.GetValue(item, null);
                        if ( searchProperty.Name.Equals("DateOut"))
                        {
                          
                            var date = DateTimeOffset.Parse(searchValue.ToString());
                            valueClass = date.Equals(DateTimeOffset.MinValue) ? "" : date.ToOffset(new TimeSpan(timeZone, 0, 0)).Date.ToString("d");  
                        }
                        else
                        {
                            valueClass = searchValue == null ? "" : searchValue.ToString();
                        }
                        
                    }
                    else
                    {
                        valueClass = "";
                    }
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("BON PACKAGING");
            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = model.Date.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "SHIFT";
            sheet.Cells[2, 2].Value = model.Shift;

            sheet.Cells[3, 1].Value = "ZONA";
            sheet.Cells[3, 2].Value = model.DestinationArea;

            sheet.Cells[4, 1].Value = "NOMOR BON";
            sheet.Cells[4, 2].Value = model.BonNo;

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 6, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima")
                {
                    sheet.Cells[6, startHeaderColumn].Value = column.ColumnName;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Merge = true;
                    startHeaderColumn++;
                }
            }

            sheet.Cells[6, startHeaderColumn].Value = "Paraf";
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Merge = true;

            sheet.Cells[7, startHeaderColumn].Value = "Menyerahkan";
            sheet.Cells[7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            startHeaderColumn++;

            sheet.Cells[7, startHeaderColumn].Value = "Menerima";
            sheet.Cells[7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 8;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet)
        {
            //var model = await _repository.ReadByIdAsync(id);
            //var modelAll = _repository.ReadAll().Where(s => s.Area == PACKING && !s.HasNextAreaDocument).ToList().Select(s =>
            //    new
            //    {
            //        SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
            //        {
            //            BonNo = s.BonNo,
            //            NoSPP = d.ProductionOrderNo,
            //            QtyOrder = d.ProductionOrderOrderQuantity,
            //            Material = d.Construction,
            //            Unit = d.Unit,
            //            Buyer = d.Buyer,
            //            Warna = d.Color,
            //            Motif = d.Motif,
            //            Jenis = d.PackagingType,
            //            Grade = d.Grade,
            //            Ket = d.Description,
            //            QtyPack = d.PackagingQty,
            //            Pack = d.PackagingUnit,
            //            Qty = d.Balance,
            //            SAT = d.UomUnit
            //        })
            //    });

            var packingData = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING);
            if (dateFrom.HasValue && dateTo.HasValue)
            {
                packingData = packingData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                packingData = packingData.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                packingData = packingData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            packingData = packingData.OrderBy(s => s.BonNo);

            var modelAll = packingData.Select(s =>
                new
                {
                    SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
                    {
                        BonNo = s.BonNo,
                        NoSPP = d.ProductionOrderNo,
                        QtyOrder = d.ProductionOrderOrderQuantity,
                        Material = d.Construction,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Color,
                        Motif = d.Motif,
                        ProductionMachine = d.ProductionMachine,
                        Jenis = d.PackagingType,
                        Grade = d.Grade,
                        Ket = d.Description,
                        QtyPack = d.PackagingQty,
                        Pack = d.PackagingUnit,
                        Qty = d.Balance,
                        d.NextAreaInputStatus,
                        SAT = d.UomUnit,
                        DateOut = d.DateOut,
                        ProductTextileName = d.ProductTextileName,
                    })
                });

            if (type == "BON")
            {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.GroupBy(r => new { r.NoSPP, r.Grade, r.NextAreaInputStatus }).Select(d => new
                    {
                        BonNo = d.First().BonNo,
                        NoSPP = d.Key.NoSPP,
                        QtyOrder = d.First().QtyOrder,
                        Material = d.First().Material,
                        MaterialOrigin = d.First().MaterialOrigin,
                        Unit = d.First().Unit,
                        Buyer = d.First().Buyer,
                        Warna = d.First().Warna,
                        Motif = d.First().Motif,
                        ProductionMachine = d.First().ProductionMachine,
                        Jenis = d.First().Jenis,
                        Grade = d.First().Grade,
                        Ket = d.First().Ket,
                        QtyPack = d.Sum( x=> x.QtyPack),
                        Pack = d.First().Pack,
                        Qty = d.Sum( x=> x.Qty),
                        NextAreaInputStatus = d.First().NextAreaInputStatus,
                        SAT = d.First().SAT,
                        DateOut = d.First().DateOut,
                        ProductTextileName = d.First().ProductTextileName,
                    })
                });


            }
            else
            {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.Select(d => new
                    {
                        BonNo = d.BonNo,
                        NoSPP = d.NoSPP,
                        QtyOrder = d.QtyOrder,
                        Material = d.Material,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Warna,
                        Motif = d.Motif,
                        ProductionMachine = d.ProductionMachine,
                        Jenis = d.Jenis,
                        Grade = d.Grade,
                        Ket = d.Ket,
                        QtyPack = d.QtyPack,
                        Pack = d.Pack,
                        Qty = d.Qty,
                        NextAreaInputStatus = d.NextAreaInputStatus,
                        SAT = d.SAT,
                        DateOut = d.DateOut,
                        ProductTextileName = d.ProductTextileName,
                    })
                });

            }


            //var modelAll = _repository.ReadAll().Where(s => s.Area == PACKING && !s.HasNextAreaDocument).ToList().SelectMany(s =>
            //    new
            //    {
            //        SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
            //        {
            //            BonNo = s.BonNo,
            //            NoSPP = d.ProductionOrderNo,
            //            QtyOrder = d.ProductionOrderOrderQuantity,
            //            Material = d.Construction,
            //            Unit = d.Unit,
            //            Buyer = d.Buyer,
            //            Warna = d.Color,
            //            Motif = d.Motif,
            //            Jenis = d.PackagingType,
            //            Grade = d.Grade,
            //            Ket = d.Description,
            //            QtyPack = d.PackagingQty,
            //            Pack = d.PackagingUnit,
            //            Qty = d.Balance,
            //            SAT = d.UomUnit
            //        })
            //    });
            modelAll.ToList();
            var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            var query = modelAll.SelectMany(s => s.SppList);


            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"NoSPP","NO SP" },
                {"DateOut","Tanggal Keluar" },
                {"QtyOrder","QTY ORDER" },
                {"Material","MATERIAL"},
                {"ProductTextileName", "NAMA BARANG" },
                {"MaterialOrigin", "ASAL MATERIAL" },
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"ProductionMachine","Mesin Produksi"},
                {"Jenis","JENIS"},
                {"Grade","GRADE"},
                {"Ket","KET"},
                {"QtyPack","QTY Pack"},
                {"Pack","PACK"},
                {"Qty","QTY" },
                {"SAT","SAT" },
                {"NextAreaInputStatus","Status" },
            };
            var listClass = query.ToList().FirstOrDefault().GetType().GetProperties();
            #endregion
            #region Assign Column
            foreach (var prop in mappedClass.Select((item, index) => new { Index = index, Items = item }))
            {
                string fieldName = prop.Items.Value;
                dt.Columns.Add(new DataColumn() { ColumnName = fieldName, DataType = typeof(string) });
            }
            #endregion
            #region Assign Data
            foreach (var item in query)
            {
                List<string> data = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName && column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima");
                    string valueClass = "";
                    if (searchMappedClass != null && searchMappedClass != null && searchMappedClass.FirstOrDefault().Key != null)
                    {
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        var searchValue = searchProperty.GetValue(item, null);

                        if ( searchProperty.Name.Equals("DateOut"))
                        {
                            var date = DateTimeOffset.Parse(searchValue.ToString());
                            valueClass = date.Equals(DateTimeOffset.MinValue) ? "" : date.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                        }
                        else
                        {
                            valueClass = searchValue == null ? "" : searchValue.ToString();
                        }
                    }
                    
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("BON PACKAGING");

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 1, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[1, startHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {

                sheet.Cells[1, startHeaderColumn].Value = column.ColumnName;
                sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                startHeaderColumn++;
            }
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 2;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public ListResult<IndexViewModel> ReadBonOutFromPack(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => Convert.ToInt32(d.BalanceRemains) > 0 && d.DyeingPrintingAreaInputId == s.Id && d.HasOutputDocument == false));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                PackagingProductionOrders = MapModeltoModelView(s.DyeingPrintingAreaInputProductionOrders.ToList())
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPack(int page, int size, string filter, string order, string keyword)
        {
            var query2 = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => d.DyeingPrintingAreaInputId == s.Id)); ;
            var query = _inputProductionOrderRepository.ReadAll().Join(query2,
                                                                        s => s.DyeingPrintingAreaInputId,
                                                                        s2 => s2.Id,
                                                                        (s, s2) => s);


            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            var data = query.ToList().Select(s => new InputPackagingProductionOrdersViewModel()
            {
                Id = s.Id,
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                Color = s.Color,
                Construction = s.Construction,
                //HasOutputDocument = s.HasOutputDocument,
                //IsChecked = s.IsChecked,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.ProcessTypeId,
                    Name = s.ProcessTypeName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                },
                MaterialWidth = s.MaterialWidth,
                MaterialOrigin = s.MaterialOrigin,
                FinishWidth = s.FinishWidth,
                MaterialProduct = new Material()
                {
                    Name = s.MaterialName,
                    Id = s.MaterialId
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = s.MaterialConstructionId,
                    Name = s.MaterialConstructionName
                },
                PackingType = s.PackagingType,
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                ProductionOrderNo = s.ProductionOrderNo,
                Area = s.Area,
                BuyerId = s.BuyerId,
                Grade = s.Grade,
                Status = s.Status,
                HasOutputDocument = s.HasOutputDocument,
                QtyOrder = s.ProductionOrderOrderQuantity,
                Remark = s.Remark,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                HasPrintingProductSKU = s.HasPrintingProductSKU,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                HasPrintingProductPacking = s.HasPrintingProductPacking
            });

            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }
        public ListResult<PlainAdjPackagingProductionOrder> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll()
                .Where(s => s.Area == DyeingPrintingArea.PACKING && !s.HasOutputDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Order(query, OrderDictionary);
            var data = query
                //.GroupBy(d => d.ProductionOrderId)
                //.Select(s => s.First())
                .OrderBy(s => s.ProductionOrderNo)
                .Skip((page - 1) * size).Take(size)
                .Select(s => new PlainAdjPackagingProductionOrder()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    },
                    PackagingQTY = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    Active = s.Active,
                    Area = s.Area,
                    Balance = s.Balance,
                    BalanceRemains = s.BalanceRemains,
                    Buyer = s.Buyer,
                    CartNo = s.CartNo,
                    BuyerId = s.BuyerId,
                    Color = s.Color,
                    Construction = s.Construction,
                    Status = s.Status,
                    DyeingPrintingAreaInputProductionOrderId = s.Id,
                    DyeingPrintingAreaOutputProductionOrderId = s.DyeingPrintingAreaOutputProductionOrderId,
                    Grade = s.Grade,
                    HasOutputDocument = s.HasOutputDocument,
                    Id = s.Id,
                    Material = new Material
                    {
                        Name = s.MaterialName,
                        Id = s.MaterialId
                    },
                    MaterialConstruction = new MaterialConstruction
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    UomUnit = s.UomUnit,
                    Unit = s.Unit,
                    Motif = s.Motif,
                    PackingInstruction = s.PackingInstruction,
                    Remark = s.Remark,
                    //AtQty = s.Balance / Convert.ToDouble(s.PackagingQty),
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    HasPrintingProductPacking = s.HasPrintingProductPacking
                });

            return new ListResult<PlainAdjPackagingProductionOrder>(data.ToList(), page, size, query.Count());
        }

        public ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPackSumBySPPNo(int page, int size, string filter, string order, string keyword)
        {
            var query2 = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => d.DyeingPrintingAreaInputId == s.Id));
            var query = _inputProductionOrderRepository.ReadAll().Join(query2,
                                                                        s => s.DyeingPrintingAreaInputId,
                                                                        s2 => s2.Id,
                                                                        (s, s2) => s);


            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            //var queryGroup = query.GroupBy(
            //                        s => s.ProductionOrderId,
            //                        s => s,
            //                        (key, item) => new { Key = key, Items = item })
            //                        .Select(s=> new InputPackagingProductionOrdersViewModel()
            //                        {
            //                            Id = s.Items.FirstOrDefault().Id,
            //                            Balance = s.Items.Sum(d=> d.Balance),
            //                            Buyer = s.Items.First().Buyer,
            //                            CartNo = s.Items.First().CartNo,
            //                            Color = s.Items.First().Color,
            //                            Construction = s.Items.First().Construction,
            //                            //HasOutputDocument = s.HasOutputDocument,
            //                            //IsChecked = s.IsChecked,
            //                            Motif = s.Items.First().Motif,
            //                            PackingInstruction = s.Items.First().PackingInstruction,
            //                            ProductionOrder = new ProductionOrder()
            //                            {
            //                                Id = s.Items.First().ProductionOrderId,
            //                                No = s.Items.First().ProductionOrderNo,
            //                                Type = s.Items.First().ProductionOrderType
            //                            },
            //                            Unit = s.Items.First().Unit,
            //                            UomUnit = s.Items.First().UomUnit,
            //                            ProductionOrderNo = s.Items.First().ProductionOrderNo,
            //                            Area = s.Items.First().Area,
            //                            BuyerId = s.Items.First().BuyerId,
            //                            Grade = s.Items.First().Grade,
            //                            Status = s.Items.First().Status,
            //                            HasOutputDocument = s.Items.First().HasOutputDocument,
            //                            QtyOrder = s.Items.First().ProductionOrderOrderQuantity,
            //                            Remark = s.Items.First().Remark,
            //                        });
            var queryGroup = query.GroupBy(
                                   s => s.ProductionOrderId,
                                   s => s,
                                   (key, item) => new { Key = key, Items = item });

            var data = queryGroup.ToList().Select(s => new InputPackagingProductionOrdersViewModel()
            {
                Id = 0,
                Balance = s.Items.Sum(d => d.Balance),
                Buyer = s.Items.First().Buyer,
                CartNo = s.Items.First().CartNo,
                Color = s.Items.First().Color,
                Construction = s.Items.First().Construction,
                //HasOutputDocument = s.HasOutputDocument,
                //IsChecked = s.IsChecked,
                Motif = s.Items.First().Motif,
                PackingInstruction = s.Items.First().PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.Items.First().ProductionOrderId,
                    No = s.Items.First().ProductionOrderNo,
                    Type = s.Items.First().ProductionOrderType
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = s.Items.First().MaterialConstructionId,
                    Name = s.Items.First().MaterialConstructionName
                },
                MaterialProduct = new Material()
                {
                    Id = s.Items.First().MaterialId,
                    Name = s.Items.First().MaterialName
                },
                MaterialWidth = s.Items.First().MaterialWidth,
                MaterialOrigin = s.Items.First().MaterialOrigin,
                FinishWidth = s.Items.First().FinishWidth,
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.Items.First().ProcessTypeId,
                    Name = s.Items.First().ProcessTypeName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.Items.First().YarnMaterialId,
                    Name = s.Items.First().YarnMaterialName
                },
                Unit = s.Items.First().Unit,
                UomUnit = s.Items.First().UomUnit,
                ProductionOrderNo = s.Items.First().ProductionOrderNo,
                Area = s.Items.First().Area,
                BuyerId = s.Items.First().BuyerId,
                Grade = s.Items.First().Grade,
                Status = s.Items.First().Status,
                HasOutputDocument = s.Items.First().HasOutputDocument,
                QtyOrder = s.Items.First().ProductionOrderOrderQuantity,
                Remark = s.Items.First().Remark,
                ProductSKUId = s.Items.First().ProductSKUId,
                FabricSKUId = s.Items.First().FabricSKUId,
                ProductSKUCode = s.Items.First().ProductSKUCode,
                HasPrintingProductSKU = s.Items.First().HasPrintingProductSKU,
                ProductPackingId = s.Items.First().ProductPackingId,
                FabricPackingId = s.Items.First().FabricPackingId,
                ProductPackingCode = s.Items.First().ProductPackingCode,
                HasPrintingProductPacking = s.Items.First().HasPrintingProductPacking
            });

            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }



        public ListResult<OutputPackagingProductionOrderGroupedViewModel> ReadSppInFromPackGroup(int page, int size, string filter, string order, string keyword)
        {
            //var query2 = _inputRepository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => d.DyeingPrintingAreaInputId == s.Id)); ;
            //var query = _inputProductionOrderRepository.ReadAll().Join(query2,
            //                                                            s => s.DyeingPrintingAreaInputId,
            //                                                            s2 => s2.Id,
            //                                                            (s, s2) => s);

            var query = _inputProductionOrderRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && !s.HasOutputDocument);


            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            var datas = query.ToList().Select(s => new InputPackagingProductionOrdersViewModel()
            {
                Id = s.Id,
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                BalanceRemains = s.BalanceRemains,
                PreviousBalance = s.BalanceRemains,
                Color = s.Color,
                Construction = s.Construction,
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = s.MaterialConstructionId,
                    Name = s.MaterialConstructionName
                },
                PackingType = s.PackagingType,
                MaterialWidth = s.MaterialWidth,
                MaterialOrigin = s.MaterialOrigin,
                FinishWidth = s.FinishWidth,
                MaterialProduct = new Material()
                {
                    Name = s.MaterialName,
                    Id = s.MaterialId
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.ProcessTypeId,
                    Name = s.ProcessTypeName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                },
                Motif = s.Motif,
                ProductionMachine = s.ProductionMachine,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType,
                    OrderQuantity = s.ProductionOrderOrderQuantity
                },
                Unit = s.Unit,
                DyeingPrintingAreaInputProductionOrderId = s.Id,
                UomUnit = s.UomUnit,
                ProductionOrderNo = s.ProductionOrderNo,
                Area = s.Area,
                BuyerId = s.BuyerId,
                Grade = s.Grade,
                Status = s.Status,
                HasOutputDocument = s.HasOutputDocument,
                QtyOrder = s.ProductionOrderOrderQuantity,
                Remark = s.Remark,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                HasPrintingProductSKU = s.HasPrintingProductSKU,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                HasPrintingProductPacking = s.HasPrintingProductPacking
            });

            var data = datas.GroupBy(s => s.ProductionOrderNo).Select(s => new OutputPackagingProductionOrderGroupedViewModel
            {
                ProductionOrderNo = s.Key,
                ProductionOrderList = s.ToList()
            });

            return new ListResult<OutputPackagingProductionOrderGroupedViewModel>(data.ToList(), page, size, data.Count());
        }


        public ICollection<OutputPackagingProductionOrderViewModel> MapModeltoModelView(List<DyeingPrintingAreaInputProductionOrderModel> source)
        {
            List<OutputPackagingProductionOrderViewModel> result = new List<OutputPackagingProductionOrderViewModel>();
            foreach (var d in source)
            {
                result.Add(new OutputPackagingProductionOrderViewModel
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Remark = d.Remark,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    MaterialWidth = d.MaterialWidth,
                    MaterialOrigin = d.MaterialOrigin,
                    FinishWidth = d.FinishWidth,
                    MaterialProduct = new Material()
                    {
                        Name = d.MaterialName,
                        Id = d.MaterialId
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = d.MaterialConstructionId,
                        Name = d.MaterialConstructionName
                    },
                    Grade = d.Grade,
                    Id = d.Id,
                    Unit = d.Unit,
                    Material = d.Construction,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit,
                    ProductionOrderNo = d.ProductionOrderNo,
                    QtyOrder = d.ProductionOrderOrderQuantity,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking,
                    DateIn=d.DateIn
                });
            }
            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            var bonOutput = _repository.ReadAll().FirstOrDefault(x => x.Id == bonId && x.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument));
            if (bonOutput != null)
            {
                var listBonInputByBonOutput = _inputProductionOrderRepository.ReadAll().Join(bonOutput.DyeingPrintingAreaOutputProductionOrders,
                                                                              sppInput => sppInput.Id,
                                                                              sppOutput => sppOutput.DyeingPrintingAreaInputProductionOrderId,
                                                                              (sppInput, sppOutput) => new { Input = sppInput, Output = sppOutput });
                foreach (var spp in listBonInputByBonOutput)
                {
                    spp.Input.SetHasOutputDocument(false, "OUTPACKINGSERVICE", "SERVICE");

                    //update balance remains
                    var newBalance = spp.Input.BalanceRemains + spp.Output.Balance;

                    spp.Input.SetBalanceRemains(newBalance, "OUTPACKINGSERVICE", "SERVICE");
                    result += await _inputProductionOrderRepository.UpdateAsync(spp.Input.Id, spp.Input);
                }
            }
            result += await _repository.DeleteAsync(bonId);

            return result;
        }

        private async Task<int> DeleteOutV2(DyeingPrintingAreaOutputModel bonOutput)
        {
            var result = 0;
            //var bonOutput = _repository.ReadAll().FirstOrDefault(x => x.Id == bonId && x.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument));
            //var bonOutput = await _repository.ReadByIdAsync(bonId);
            // get all SPP backup balance remains
            List<DyeingPrintingAreaInputProductionOrderModel> listBackUpSpp = new List<DyeingPrintingAreaInputProductionOrderModel>();
            if (bonOutput != null)
            {
                var listBonInputByBonOutput = bonOutput.DyeingPrintingAreaOutputProductionOrders.Select(s => s.PrevSppInJson).ToList();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Double
                };
                //var str = "[{'ProductionOrderId':63,'ProductionOrderNo':'F/2020/0010','MaterialId':0,'MaterialName':null,'MaterialConstructionId':0,'MaterialConstructionName':null,'MaterialWidth':null,'CartNo':'KER','BuyerId':551,'Buyer':'ERWAN KURNIADI','Construction':'Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100','Unit':'DYEING','Color':'Brown','Motif':null,'UomUnit':'MTR','Balance':5000.0,'HasOutputDocument':false,'IsChecked':false,'PackingInstruction':'a','ProductionOrderType':'SOLID','ProductionOrderOrderQuantity':5000.0,'Remark':'A','Grade':'A','Status':null,'InitLength':0.0,'AvalALength':0.0,'AvalBLength':0.0,'AvalConnectionLength':0.0,'AvalType':null,'AvalCartNo':null,'AvalMachine':null,'DeliveryOrderSalesId':0,'DeliveryOrderSalesNo':null,'PackagingUnit':null,'PackagingType':null,'PackagingQty':0.00,'Area':'PACKING','BalanceRemains':2500.0,'InputAvalBonNo':null,'AvalQuantityKg':0.0,'AvalQuantity':0.0,'DyeingPrintingAreaInputId':6,'DyeingPrintingAreaOutputProductionOrderId':3,'DyeingPrintingAreaInput':{'Date':'2020-06-26T17:00:00+00:00','Area':'PACKING','Shift':'PAGI','BonNo':'PC.20.0003','Group':'A','AvalType':null,'TotalAvalQuantity':0.0,'TotalAvalWeight':0.0,'IsTransformedAval':false,'DyeingPrintingAreaInputProductionOrders':[{'ProductionOrderId':63,'ProductionOrderNo':'F/2020/0010','MaterialId':0,'MaterialName':null,'MaterialConstructionId':0,'MaterialConstructionName':null,'MaterialWidth':null,'CartNo':'KAR','BuyerId':551,'Buyer':'ERWAN KURNIADI','Construction':'Greige Test Dyeing Printing / TWILL 3/1. 104 x 52 / 100','Unit':'DYEING','Color':'Brown','Motif':null,'UomUnit':'MTR','Balance':2500.0,'HasOutputDocument':false,'IsChecked':false,'PackingInstruction':'a','ProductionOrderType':'SOLID','ProductionOrderOrderQuantity':5000.0,'Remark':'A','Grade':'A','Status':null,'InitLength':0.0,'AvalALength':0.0,'AvalBLength':0.0,'AvalConnectionLength':0.0,'AvalType':null,'AvalCartNo':null,'AvalMachine':null,'DeliveryOrderSalesId':0,'DeliveryOrderSalesNo':null,'PackagingUnit':null,'PackagingType':null,'PackagingQty':0.00,'Area':'PACKING','BalanceRemains':2500.0,'InputAvalBonNo':null,'AvalQuantityKg':0.0,'AvalQuantity':0.0,'DyeingPrintingAreaInputId':6,'DyeingPrintingAreaOutputProductionOrderId':1,'Active':false,'CreatedUtc':'2020-06-24T20:36:35.9994134','CreatedBy':'dev2','CreatedAgent':'Repository','LastModifiedUtc':'2020-06-24T20:36:35.9994138','LastModifiedBy':'dev2','LastModifiedAgent':'Repository','IsDeleted':false,'DeletedUtc':'0001-01-01T00:00:00','DeletedBy':'','DeletedAgent':'','Id':6}],'Active':false,'CreatedUtc':'2020-06-24T20:36:35.9994044','CreatedBy':'dev2','CreatedAgent':'Repository','LastModifiedUtc':'2020-06-24T20:36:35.9994055','LastModifiedBy':'dev2','LastModifiedAgent':'Repository','IsDeleted':false,'DeletedUtc':'0001-01-01T00:00:00','DeletedBy':'','DeletedAgent':'','Id':6},'Active':false,'CreatedUtc':'2020-06-24T20:36:35.9994089','CreatedBy':'dev2','CreatedAgent':'Repository','LastModifiedUtc':'2020-06-24T21:08:44.9945567Z','LastModifiedBy':'OUTPUTPACKAGING','LastModifiedAgent':'SERVICE','IsDeleted':false,'DeletedUtc':'0001-01-01T00:00:00','DeletedBy':'','DeletedAgent':'','Id':4}]";
                //var test = JArray.Parse(str);
                //var test2 = test[0][]
                //foreach(var i in test)
                //{
                //    var a = i.ToObject<DyeingPrintingAreaInputProductionOrderModel>();
                //}
                //var listtest = test.Select(s => new DyeingPrintingAreaInputProductionOrderModel((string)s["Area"], (int)s["ProductionOrderId"], (string)s["ProductionOrderNo"], (string)s["ProductionOrder"], (string)s["PackingInstruction"], (string)s["CartNo"], (string)s["Buyer"], (string)s["Construction"],
                //     (string)s["Unit"], (string)s["Color"], (string)s["Motif"], (string)s["UomUnit"], (double)s["ProductionOrderOrderQuantity"], false, (double)s["QtyOrder"], (string)s["Grade"], (double)s["Balance"], (int)s["BuyerId"], (int)s["Id"], (string)s["Remark"]));

                //var listtest = test.First.ToObject<DyeingPrintingAreaInputProductionOrderModel>();
                //foreach (var spp in listBonInputByBonOutput)
                //{
                //    var listSPP = JsonConvert.DeserializeObject<DyeingPrintingAreaInputProductionOrderModel[]>(spp, settings);
                //    //var sppObject = 

                //    foreach (var sppBck in listSPP)
                //    {
                //        //update balance remains
                //        var modelToUpdate = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == sppBck.Id).ToList();
                //        foreach (var model in modelToUpdate)
                //        {
                //            var newBalance = model.BalanceRemains + sppBck.BalanceRemains;
                //            model.SetBalanceRemains(newBalance, "OUTPUTPACKING", "SERVICE");
                //            model.SetHasOutputDocument(false, "OUTPUTPACKING", "SERVICE");

                //            result += await _inputProductionOrderRepository.UpdateAsync(sppBck.Id, model);
                //        }
                //    }
                //}
                result += await _repository.DeletePackingArea(bonOutput);
                foreach (var items in bonOutput.DyeingPrintingAreaOutputProductionOrders)
                {
                    if (!items.HasNextAreaDocument)
                    {
                        var movementModel = new DyeingPrintingAreaMovementModel(bonOutput.Date, items.MaterialOrigin, bonOutput.Area, DyeingPrintingArea.OUT, bonOutput.Id, bonOutput.BonNo, items.ProductionOrderId, items.ProductionOrderNo,
                        items.CartNo, items.Buyer, items.Construction, items.Unit, items.Color, items.Motif, items.UomUnit, items.Balance * -1, items.Id, items.ProductionOrderType, items.ProductTextileId, items.ProductTextileCode, items.ProductTextileName, items.Grade);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }
                }
            }
            //result += await _repository.DeleteAsync(bonId);

            return result;
        }

        private async Task<int> DeleteAdj(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            string type;
            if (model.DyeingPrintingAreaOutputProductionOrders.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(model.Date, item.MaterialOrigin, model.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);

                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _repository.DeleteAdjustment(model);

            return result;
        }

        public async Task<int> DeleteV2(int bonId)
        {
            var model = await _repository.ReadByIdAsync(bonId);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return await DeleteOutV2(model);
            }
            else
            {
                return await DeleteAdj(model);
            }
        }

        public async Task<int> CreateAdj(OutputPackagingViewModel viewModel)
        {
            return await InsertAdj(viewModel);
        }

        private async Task<int> UpdateOut(int id, OutputPackagingViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();
            foreach (var item in viewModel.PackagingProductionOrders)
            {
                var packingData = _fabricPackingSKUService.AutoCreatePacking(new FabricPackingAutoCreateFormDto()
                {
                    FabricSKUId = item.FabricSKUId,
                    PackingType = item.PackagingUnit,
                    Quantity = (int)item.PackagingQTY,
                    Length = item.PackingLength
                });
                string packingCodes = string.Join(',', packingData.ProductPackingCodes);
                var productionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, item.HasNextAreaDocument, item.ProductionOrder.Id,
                    item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.QtyOut,
                    item.PackingInstruction, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackagingType, item.PackagingQTY, item.PackagingUnit, item.QtyOrder,
                    item.Keterangan, id, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId, item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialConstruction.Id,
                    item.MaterialConstruction.Name, item.MaterialWidth, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId,
                    item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, packingData.ProductPackingId, packingData.FabricPackingId, packingCodes,
                    packingCodes == item.ProductPackingCode && item.HasPrintingProductPacking, item.PackingLength, item.FinishWidth, item.MaterialOrigin)
                {
                    Id = item.Id
                };
                productionOrders.Add(productionOrder);
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea, viewModel.Group, viewModel.Type, productionOrders)
            {
                Id = id
            };


            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;

                    dictBalance.Add(lclModel.Id, diffBalance);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument && !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdatePackingArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputPackagingViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            if (viewModel.PackagingProductionOrdersAdj.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }

            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group, type,
                   viewModel.PackagingProductionOrdersAdj.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                       item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                       item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                       item.MaterialObj.Id, item.MaterialObj.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.NoDocument,
                       item.PackagingType, item.PackagingQty, item.PackagingUnit, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                       item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.FinishWidth,item.DateIn,viewModel.Date, item.MaterialOrigin)
                   {
                       Id = item.Id
                   }).ToList());


            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;

                    dictBalance.Add(lclModel.Id, diffBalance);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdateAdjustmentData(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputPackagingViewModel viewModel)
        {
            if (viewModel.Type == DyeingPrintingArea.OUT)
            {
                return await UpdateOut(id, viewModel);
            }
            else
            {
                return await UpdateAdj(id, viewModel);
            }
        }

        public ListResult<OutputPackagingProductionOrderGroupedViewModel> ReadSPPInPackingGroupBySPPGrade(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.Balance > 0 && s.IsAfterStockOpname);


            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            var data = query.GroupBy(s => s.ProductionOrderNo)
                .Select(s => new OutputPackagingProductionOrderGroupedViewModel()
                {
                    ProductionOrderNo = s.Key,
                    ProductionOrderList = s.GroupBy(d => d.Grade).Select(e => new InputPackagingProductionOrdersViewModel()
                    {
                        //Id = s.Id,
                        Balance = e.Sum(i => i.Balance),
                        Buyer = e.First().Buyer,
                        CartNo = e.First().CartNo,
                        BalanceRemains = e.Sum(i => i.BalanceRemains),
                        PreviousBalance = e.Sum(i => i.BalanceRemains),
                        Color = e.First().Color,
                        Construction = e.OrderByDescending(a => a.CreatedUtc).First().Construction,
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Id = e.First().MaterialConstructionId,
                            Name = e.First().MaterialConstructionName
                        },
                        PackingType = e.First().PackagingType,
                        MaterialWidth = e.First().MaterialWidth,
                        MaterialOrigin = e.First().MaterialOrigin,
                        FinishWidth = e.OrderByDescending(a => a.CreatedUtc).First().FinishWidth,
                        MaterialProduct = new Material()
                        {
                            Name = e.First().MaterialName,
                            Id = e.First().MaterialId
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = e.First().ProcessTypeId,
                            Name = e.First().ProcessTypeName
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = e.First().YarnMaterialId,
                            Name = e.First().YarnMaterialName
                        },
                        Motif = e.First().Motif,
                        ProductionMachine = e.First().ProductionMachine,
                        PackingInstruction = e.First().PackingInstruction,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = e.First().ProductionOrderId,
                            No = e.First().ProductionOrderNo,
                            Type = e.First().ProductionOrderType,
                            OrderQuantity = e.First().ProductionOrderOrderQuantity,
                            CreatedUtc = e.First().CreatedUtcOrderNo
                        },
                        ProductTextile = new ProductTextile()
                        { 
                            Id = e.First().ProductTextileId,
                            Code = e.First().ProductTextileCode,
                            Name = e.First().ProductTextileName
                        },
                        Unit = e.First().Unit,
                        //DyeingPrintingAreaInputProductionOrderId = s.Id,
                        UomUnit = e.First().UomUnit,
                        ProductionOrderNo = e.First().ProductionOrderNo,
                        Area = e.First().Area,
                        BuyerId = e.First().BuyerId,
                        Grade = e.Key,
                        Status = e.First().Status,
                        //HasOutputDocument = e.First().HasOutputDocument,
                        QtyOrder = e.First().ProductionOrderOrderQuantity,
                        Remark = e.First().Remark,
                        ProductSKUId = e.First().ProductSKUId,
                        FabricSKUId = e.First().FabricSKUId,
                        ProductSKUCode = e.First().ProductSKUCode,
                        HasPrintingProductSKU = e.First().HasPrintingProductSKU,
                        ProductPackingId = e.First().ProductPackingId,
                        FabricPackingId = e.First().FabricPackingId,
                        ProductPackingCode = e.First().ProductPackingCode,
                        HasPrintingProductPacking = e.First().HasPrintingProductPacking,
                        DateIn=e.First().DateIn

                    }).Where(e => e.BalanceRemains > 0).ToList()
                })
                .Where(s => s.ProductionOrderList.Count > 0)
                .OrderBy(s => s.ProductionOrderNo)
                .Skip((page - 1) * size).Take(size);

            return new ListResult<OutputPackagingProductionOrderGroupedViewModel>(data.ToList(), page, size, data.Count());
        }

        public Task<MemoryStream> GenerateExcel(int id)
        {
            //int clientTimeZoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            throw new NotImplementedException();
        }
    }
    internal static class Extensions
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        //public static Dictionary<int, string> ToDictionaries(Enum myEnum)
        //{
        //    var myEnumType = myEnum.GetType();
        //    var names = myEnumType.GetFields()
        //        .Where(m => m.GetCustomAttribute<DisplayAttribute>() != null)
        //        .Select(e => e.GetCustomAttribute<DisplayAttribute>().Name);
        //    var values = Enum.GetValues(myEnumType).Cast<int>();
        //    return names.Zip(values, (n, v) => new KeyValuePair<int, string>(v, n))
        //        .ToDictionary(kv => kv.Key, kv => kv.Value);
        //}
        //public static Enum ToObject(this Enum value)
        //{
        //    FieldInfo field = value.GetType().GetField(value.ToString());
        //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        //    return attribute == null ? value.ToString() : attribute.Description;
        //}
    }
}
