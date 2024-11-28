using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingService : IOutputShippingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOrderRepository;
        protected readonly IIdentityProvider _identityProvider;


        public OutputShippingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private async Task<OutputShippingViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputShippingViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputShippingViewModel()
                {
                    ShippingCode = model.ShippingCode,
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Type = DyeingPrintingArea.OUT,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    HasSalesInvoice = model.HasSalesInvoice,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,
                    
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = model.DeliveryOrderSalesId,
                        No = model.DeliveryOrderSalesNo
                    },
                    PackingListNo = model.PackingListNo,
                    PackingType = model.PackingType,
                    PackingListRemark = model.PackingListRemark,
                    PackingListAuthorized =model.PackingListAuthorized,
                    PackingListLCNumber = model.PackingListLCNumber,
                    PackingListIssuedBy = model.PackingListIssuedBy,
                    PackingListDescription = model.PackingListDescription,
                    UpdateBySales = model.UpdateBySales,
                    UomUnit = model.UomUnit,
                    ShippingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputShippingProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        Remark = s.Remark,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        Packing = s.PackagingUnit,
                        QtyPacking = s.PackagingQty,
                        PackingType = s.PackagingType,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity
                        },
                        DeliveryOrder = new DeliveryOrderSales()
                        {
                            Id = s.DeliveryOrderSalesId,
                            No = s.DeliveryOrderSalesNo,
                            Name = s.DestinationBuyerName
                        },
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        Material = new Material()
                        {
                            Id = s.MaterialId,
                            Name = s.MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = s.MaterialConstructionName,
                            Id = s.MaterialConstructionId
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        ProductTextile = new ProductTextile()
                        { 
                            Id = s.ProductTextileId,
                            Code = s.ProductTextileCode,
                            Name = s.ProductTextileName
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        DeliveryNote = s.DeliveryNote,
                        Qty = s.Balance,
                        ShippingGrade = s.ShippingGrade,
                        ShippingRemark = s.ShippingRemark,
                        Weight = s.Weight,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        HasSalesInvoice = s.HasSalesInvoice,
                        PackingLength = s.PackagingLength,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        BalanceRemains = s.Balance,
                        DateIn = s.DateIn,
                        DateOut = s.DateOut,
                        PackingListBaleNo = s.PackingListBaleNo,
                        PackingListGross = s.PackingListGross,
                        PackingListNet = s.PackingListNet,
                        DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                        DestinationBuyerName = s.DestinationBuyerName

                    }).ToList()
                };

                if (vm.DestinationArea != DyeingPrintingArea.BUYER)
                {
                    foreach (var item in vm.ShippingProductionOrders)
                    {
                        var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                        if (inputData != null)
                        {
                            item.BalanceRemains = inputData.BalanceRemains + item.Qty;
                        }
                    }
                }

            }
            else
            {
                vm = new OutputShippingViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    AdjType = model.Type,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Type = DyeingPrintingArea.ADJ,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    HasSalesInvoice = model.HasSalesInvoice,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = model.DeliveryOrderSalesId,
                        No = model.DeliveryOrderSalesNo
                    },
                    PackingListNo = model.PackingListNo,
                    PackingType = model.PackingType,
                    PackingListRemark = model.PackingListRemark,
                    PackingListAuthorized = model.PackingListAuthorized,
                    ShippingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputShippingProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        AdjDocumentNo = s.AdjDocumentNo,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        Remark = s.Remark,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        Packing = s.PackagingUnit,
                        QtyPacking = s.PackagingQty,
                        PackingType = s.PackagingType,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity
                        },
                        DeliveryOrder = new DeliveryOrderSales()
                        {
                            Id = s.DeliveryOrderSalesId,
                            No = s.DeliveryOrderSalesNo
                        },
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        Material = new Material()
                        {
                            Id = s.MaterialId,
                            Name = s.MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = s.MaterialConstructionName,
                            Id = s.MaterialConstructionId
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        DeliveryNote = s.DeliveryNote,
                        Balance = s.Balance,
                        Qty = s.PackagingLength,
                        PackingLength = s.PackagingLength,
                        ShippingGrade = s.ShippingGrade,
                        ShippingRemark = s.ShippingRemark,
                        Weight = s.Weight,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        HasSalesInvoice = s.HasSalesInvoice,
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
                        PackingListBaleNo = s.PackingListBaleNo,
                        PackingListGross = s.PackingListGross,
                        PackingListNet = s.PackingListNet,
                        DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                        DestinationBuyerName = s.DestinationBuyerName
                    }).ToList()
                };
                foreach (var item in vm.ShippingProductionOrders)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        item.BalanceRemains = inputData.BalanceRemains + (item.Balance * -1);
                    }
                }
            }
            return vm;
        }

        private async Task<OutputShippingViewModel> MapToViewModelBon(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputShippingViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputShippingViewModel()
                {
                    ShippingCode = model.ShippingCode,
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Type = DyeingPrintingArea.OUT,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    HasSalesInvoice = model.HasSalesInvoice,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = model.DeliveryOrderSalesId,
                        No = model.DeliveryOrderSalesNo
                    },
                    ShippingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.GroupBy( r => new { r.ProductionOrderId, r.Grade}).Select(s => new OutputShippingProductionOrderViewModel()
                    {
                        Active = s.First().Active,
                        LastModifiedUtc = s.First().LastModifiedUtc,
                        Buyer = s.First().Buyer,
                        BuyerId = s.First().BuyerId,
                        CartNo = s.First().CartNo,
                        Color = s.First().Color,
                        Construction = s.First().Construction,
                        CreatedAgent = s.First().CreatedAgent,
                        CreatedBy = s.First().CreatedBy,
                        CreatedUtc = s.First().CreatedUtc,
                        DeletedAgent = s.First().DeletedAgent,
                        DeletedBy = s.First().DeletedBy,
                        DeletedUtc = s.First().DeletedUtc,
                        Grade = s.Key.Grade,
                        Remark = s.First().Remark,
                        Id = s.First().Id,
                        IsDeleted = s.First().IsDeleted,
                        LastModifiedAgent = s.First().LastModifiedAgent,
                        LastModifiedBy = s.First().LastModifiedBy,
                        Packing = s.First().PackagingUnit,
                        QtyPacking = s.Sum( m => m.PackagingQty),
                        PackingType = s.First().PackagingType,
                        HasNextAreaDocument = s.First().HasNextAreaDocument,
                        Motif = s.First().Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.First().ProductionOrderId,
                            No = s.First().ProductionOrderNo,
                            Type = s.First().ProductionOrderType,
                            OrderQuantity = s.First().ProductionOrderOrderQuantity
                        },
                        DeliveryOrder = new DeliveryOrderSales()
                        {
                            Id = s.First().DeliveryOrderSalesId,
                            No = s.First().DeliveryOrderSalesNo,
                            Name = s.First().DestinationBuyerName
                        },
                        MaterialWidth = s.First().MaterialWidth,
                        MaterialOrigin = s.First().MaterialOrigin,
                        FinishWidth = s.First().FinishWidth,
                        Material = new Material()
                        {
                            Id = s.First().MaterialId,
                            Name = s.First().MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = s.First().MaterialConstructionName,
                            Id = s.First().MaterialConstructionId
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.First().YarnMaterialId,
                            Name = s.First().YarnMaterialName
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.First().ProcessTypeId,
                            Name = s.First().ProcessTypeName
                        },
                        ProductTextile = new ProductTextile()
                        { 
                            Id = s.First().ProductTextileId,
                            Code = s.First().ProductTextileCode,
                            Name = s.First().ProductTextileName
                        },
                        Unit = s.First().Unit,
                        UomUnit = s.First().UomUnit,
                        DeliveryNote = s.First().DeliveryNote,
                        Qty = s.Sum( m => m.Balance),
                        ShippingGrade = s.First().ShippingGrade,
                        ShippingRemark = s.First().ShippingRemark,
                        Weight = s.First().Weight,
                        DyeingPrintingAreaInputProductionOrderId = s.First().DyeingPrintingAreaInputProductionOrderId,
                        HasSalesInvoice = s.First().HasSalesInvoice,
                        PackingLength = s.First().PackagingLength,
                        ProductSKUId = s.First().ProductSKUId,
                        FabricSKUId = s.First().FabricSKUId,
                        ProductSKUCode = s.First().ProductSKUCode,
                        HasPrintingProductSKU = s.First().HasPrintingProductSKU,
                        ProductPackingId = s.First().ProductPackingId,
                        FabricPackingId = s.First().FabricPackingId,
                        ProductPackingCode = s.First().ProductPackingCode,
                        HasPrintingProductPacking = s.First().HasPrintingProductPacking,
                        BalanceRemains = s.Sum( m => m.Balance),
                        DateIn = s.First().DateIn,
                        DateOut = s.First().DateOut,
                        DestinationBuyerName = s.First().DestinationBuyerName
                    }).ToList()
                };

                if (vm.DestinationArea != DyeingPrintingArea.BUYER)
                {
                    foreach (var item in vm.ShippingProductionOrders)
                    {
                        var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                        if (inputData != null)
                        {
                            item.BalanceRemains = inputData.BalanceRemains + item.Qty;
                        }
                    }
                }
            }
            return vm;
        }

        protected OutputShippingViewModel MapToViewModelPackingList(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputShippingViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputShippingViewModel()
                {
                    ShippingCode = model.ShippingCode,
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Type = DyeingPrintingArea.OUT,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    HasSalesInvoice = model.HasSalesInvoice,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Group = model.Group,

                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = model.DeliveryOrderSalesId,
                        No = model.DeliveryOrderSalesNo
                    },
                    PackingListNo = model.PackingListNo,
                    PackingType = model.PackingType,
                    PackingListRemark = model.PackingListRemark,
                    PackingListAuthorized = model.PackingListAuthorized,
                    PackingListLCNumber = model.PackingListLCNumber,
                    PackingListIssuedBy = model.PackingListIssuedBy,
                    PackingListDescription = model.PackingListDescription,
                    UpdateBySales = model.UpdateBySales,
                    UomUnit = model.UomUnit,
                    ShippingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputShippingProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        Remark = s.Remark,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        Packing = s.PackagingUnit,
                        QtyPacking = s.PackagingQty,
                        PackingType = s.PackagingType,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Motif = s.Motif,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            Type = s.ProductionOrderType,
                            OrderQuantity = s.ProductionOrderOrderQuantity
                        },
                        DeliveryOrder = new DeliveryOrderSales()
                        {
                            Id = s.DeliveryOrderSalesId,
                            No = s.DeliveryOrderSalesNo,
                            Name = s.DestinationBuyerName
                        },
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        Material = new Material()
                        {
                            Id = s.MaterialId,
                            Name = s.MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = s.MaterialConstructionName,
                            Id = s.MaterialConstructionId
                        },
                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                        {
                            Id = s.YarnMaterialId,
                            Name = s.YarnMaterialName
                        },
                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                        {
                            Id = s.ProcessTypeId,
                            Name = s.ProcessTypeName
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        DeliveryNote = s.DeliveryNote,
                        Qty = s.Balance,
                        ShippingGrade = s.ShippingGrade,
                        ShippingRemark = s.ShippingRemark,
                        Weight = s.Weight,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        HasSalesInvoice = s.HasSalesInvoice,
                        PackingLength = s.PackagingLength,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        BalanceRemains = s.Balance,
                        DateIn = s.DateIn,
                        DateOut = s.DateOut,
                        PackingListBaleNo = s.PackingListBaleNo,
                        PackingListGross = s.PackingListGross,
                        PackingListNet = s.PackingListNet,
                        DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                        DestinationBuyerName = s.DestinationBuyerName
                    }).ToList()
                };
            }
            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.SP, DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

            }
            else if (destinationArea == DyeingPrintingArea.TRANSIT)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.SP, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.PACKING)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.SP, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.SP, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.SP, DyeingPrintingArea.PJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        private string GeneratePackingListNo(string DOtype, int totalPackingListNo, DateTimeOffset date)
        {
            if (DOtype == "Lokal")
            {
                return string.Format("{0}{1}{2}", "LKL",  date.ToString("yy"), totalPackingListNo.ToString().PadLeft(4, '0'));

            }
           
            else
            {
                return string.Format("{0}{1}{2}", "EXP", date.ToString("yy"), totalPackingListNo.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoPenjualan(int totalPreviousData, DateTimeOffset date, string shippingCode, string shippingGrade)
        {
            return string.Format("{0}.{1}.{2}.{3}", shippingCode, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'), shippingGrade);
        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_IN, DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_OUT, DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputShippingViewModel viewModel)
        {
            int result = 0;
            DyeingPrintingAreaOutputModel model = null;

            if (viewModel.DestinationArea == DyeingPrintingArea.PENJUALAN)
            {
                //model = _repository.GetDbSet().AsNoTracking()
                //.FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                //&& s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode);

                if (viewModel.ShippingProductionOrders.First().ShippingGrade == "BQ")
                {

                    model = _repository.GetDbSet().AsNoTracking()
                    .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                    && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode
                    && s.BonNo.Contains("BQ") && !s.IsDeleted);
                }
                else
                {
                    model = _repository.GetDbSet().AsNoTracking()
                    .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                    && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode
                    && s.BonNo.Contains("BS") && !s.IsDeleted);
                }
            }
            else
            {
                if (viewModel.DestinationArea == DyeingPrintingArea.BUYER)
                {
                    if (viewModel.ShippingProductionOrders.First().ShippingGrade == "BQ")
                    {
                        model = _repository.GetDbSet().AsNoTracking()
                        .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                        && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.BonNo.Contains("BQ") && !s.IsDeleted);
                    }
                    else
                    {
                        model = _repository.GetDbSet().AsNoTracking()
                        .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                        && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.BonNo.Contains("BS") && !s.IsDeleted);
                    }

                }
                else
                {
                    model = _repository.GetDbSet().AsNoTracking()
                        .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                        && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == 0 && s.Type == DyeingPrintingArea.OUT && !s.IsDeleted);
                }

            }

            viewModel.ShippingProductionOrders = viewModel.ShippingProductionOrders.Where(s => s.IsSave).ToList();

            if (model == null)
            {
                if (viewModel.DestinationArea != DyeingPrintingArea.BUYER)
                {
                    int totalCurrentYearData = 0;
                    string bonNo = "";

                    if (viewModel.DestinationArea == DyeingPrintingArea.PENJUALAN)
                    {
                        //totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING
                        //    && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode);
                        //bonNo = GenerateBonNoPenjualan(totalCurrentYearData + 1, viewModel.Date, viewModel.ShippingCode, viewModel.ShippingProductionOrders.First().ShippingGrade);

                        totalCurrentYearData = _repository.GetDbSet().IgnoreQueryFilters().Count(s => s.Area == DyeingPrintingArea.SHIPPING
                           && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode);
                        bonNo = GenerateBonNoPenjualan(totalCurrentYearData + 1, viewModel.Date, viewModel.ShippingCode, viewModel.ShippingProductionOrders.First().ShippingGrade);

                        model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group,
                               viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                               viewModel.PackingListNo,
                                viewModel.PackingType,
                                viewModel.PackingListRemark,
                                viewModel.PackingListAuthorized,
                                viewModel.PackingListLCNumber,
                                viewModel.PackingListIssuedBy,
                                viewModel.PackingListDescription,
                                viewModel.UpdateBySales,
                                viewModel.UomUnit,
                               viewModel.ShippingProductionOrders.Select(s =>
                           new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                               s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                               s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                               s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, viewModel.Date, s.DeliveryOrder.Name, s.InventoryType, s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross,
                               s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());

                    }
                    else
                    {
                        //totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING
                        //    && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);
                        //bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                        totalCurrentYearData = _repository.GetDbSet().IgnoreQueryFilters().Count(s => s.Area == DyeingPrintingArea.SHIPPING
                            && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);
                        bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                        model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group,
                                0, "", viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                                viewModel.PackingListNo,
                                viewModel.PackingType,
                                viewModel.PackingListRemark,
                                viewModel.PackingListAuthorized,
                                viewModel.PackingListLCNumber,
                        viewModel.PackingListIssuedBy,
                        viewModel.PackingListDescription,
                        viewModel.UpdateBySales,
                        viewModel.UomUnit,
                                viewModel.ShippingProductionOrders.Select(s =>
                            new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                                s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                                s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                                s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, viewModel.Date, s.DeliveryOrder.Name, s.InventoryType, s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross,
                                s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());

                    }


                }
                else //Destination == Buyer
                {
                    //var testdata = _productionOrderRepository.ReadAllIgnoreQueryFilter();
                    //string DOType = _productionOrderRepository.ReadAllIgnoreQueryFilter().Where(x => x.DeliveryOrderSalesNo == viewModel.DeliveryOrder.No).Select(s => s.DeliveryOrderSalesType).First();
                    string DOType = viewModel.ShippingProductionOrders.Select(s => s.DeliveryOrder.Type).First();
                    

                    var dataDO= (from a in _repository.ReadAll()
                                       join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaOutputId
                                       where
                                        a.Area == DyeingPrintingArea.SHIPPING
                                        && a.DestinationArea == viewModel.DestinationArea
                                        && a.CreatedUtc.Year == viewModel.Date.Year
                                        && a.Type == DyeingPrintingArea.OUT
                                        && b.DeliveryOrderSalesType == DOType
                                       select new { 
                                            DeliveryOrderSalesType = b.DeliveryOrderSalesType,
                                            PackingListNo = a.PackingListNo

                                       }).GroupBy( x => new { x.PackingListNo, x.DeliveryOrderSalesType }).Select(d => new 
                                       {
                                           DeliveryOrderSalesType = d.Key.DeliveryOrderSalesType,
                                           PackingListNo = d.Key.PackingListNo

                                       });

                    //var totalPackingListNo = dataDO.Count() == 1 ?  dataDO.Count() : dataDO.Count() + 1;
                    var totalPackingListNo = dataDO.Count() ;

                    var packingListNo = GeneratePackingListNo(DOType, totalPackingListNo, viewModel.Date);

                    model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, false, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                        packingListNo,
                        viewModel.PackingType,
                        viewModel.PackingListRemark,
                        viewModel.PackingListAuthorized,
                        viewModel.PackingListLCNumber,
                        viewModel.PackingListIssuedBy,
                        viewModel.PackingListDescription,
                        viewModel.UpdateBySales,
                        viewModel.UomUnit,
                        viewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, viewModel.Date, s.DestinationBuyerName, s.InventoryType, 
                        s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross, s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());

                }


                result = await _repository.InsertAsync(model);
                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {


                    var itemVM = viewModel.ShippingProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaInputProductionOrderId);

                    if (model.DestinationArea != DyeingPrintingArea.BUYER)
                    {
                        if (model.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {

                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance, item.PackagingQty);
                        }
                        else
                        {
                            if (model.DestinationArea != DyeingPrintingArea.PENJUALAN)
                            {
                                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);

                                var modelOutput = _repository.ReadAll().AsNoTracking()
                                                  .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == DyeingPrintingArea.PENJUALAN
                                                   && s.DyeingPrintingAreaOutputProductionOrders.Any(x => x.DyeingPrintingAreaInputProductionOrderId == itemVM.Id));

                                if (modelOutput != null) {
                                    var output = modelOutput.DyeingPrintingAreaOutputProductionOrders.Where(x => x.DyeingPrintingAreaInputProductionOrderId == itemVM.Id);
                                    var vm = output.FirstOrDefault().Id;
                                    result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(vm, true);
                                }


                            }


                        }

                        if (model.DestinationArea != DyeingPrintingArea.PENJUALAN)
                        {
                            var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                                item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade,
                                null, item.PackagingType, item.PackagingQty, item.PackagingUnit, item.PackagingLength, item.InventoryType);

                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(itemVM.Id, true);
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.Balance, item.PackagingQty);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType,
                            item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName,item.Grade,
                            null, item.PackagingType, item.PackagingQty, item.PackagingUnit, item.PackagingLength, item.InventoryType);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }
                }
            }
            else
            {


                result += await _repository.UpdateHasSalesInvoice(model.Id, false);
                foreach (var item in viewModel.ShippingProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.DeliveryOrder.Id, item.DeliveryOrder.No,
                        item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.Grade, item.UomUnit, item.DeliveryNote, item.Qty, item.Id, item.Packing, item.PackingType, item.QtyPacking, item.BuyerId, item.HasSalesInvoice, item.ShippingGrade, item.ShippingRemark, item.Weight,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.CartNo, item.Remark, "",
                        item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.FinishWidth, item.DateIn, viewModel.Date, item.DestinationBuyerName, item.InventoryType, item.MaterialOrigin, item.DeliveryOrder.Type, item.PackingListBaleNo, item.PackingListNet, item.PackingListGross,
                        item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name);

                    modelItem.DyeingPrintingAreaOutputId = model.Id;
                    result += await _productionOrderRepository.InsertAsync(modelItem);
                    if (viewModel.DestinationArea != DyeingPrintingArea.BUYER)
                    {
                        if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {

                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, item.Qty, item.QtyPacking);
                        }
                        else
                        {
                            if (model.DestinationArea != DyeingPrintingArea.PENJUALAN)
                            {
                                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Qty);
                                var modelOutput = _repository.ReadAll().AsNoTracking()
                                                 .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == DyeingPrintingArea.PENJUALAN
                                                  && s.DyeingPrintingAreaOutputProductionOrders.Any(x => x.DyeingPrintingAreaInputProductionOrderId == item.Id));
                                if (modelOutput != null)
                                {
                                    var output = modelOutput.DyeingPrintingAreaOutputProductionOrders.Where(x => x.DyeingPrintingAreaInputProductionOrderId == item.Id);
                                    var vm = output.FirstOrDefault().Id;
                                    result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(vm, true);
                                }
                                //result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                            }

                        }

                        if (viewModel.DestinationArea != DyeingPrintingArea.PENJUALAN)
                        {
                            var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                                item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty, modelItem.Id, item.ProductionOrder.Type,
                                item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, item.Grade,
                                null, item.PackingType, item.QtyPacking, item.Packing, item.PackingLength, item.InventoryType);

                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Qty, item.QtyPacking);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty, modelItem.Id, item.ProductionOrder.Type, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, item.Grade,
                           null, item.PackingType, item.QtyPacking, item.Packing, item.PackingLength, item.InventoryType);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }
                }
            }

            return result;
        }

        private async Task<int> CreateAdj(OutputShippingViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.ShippingProductionOrders.All(d => d.Balance > 0))
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING && s.Type == DyeingPrintingArea.ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.ShippingProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING && s.Type == DyeingPrintingArea.ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.ShippingProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _repository.GetDbSet().AsNoTracking()
                    .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type);

            if (model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group,
                        0, "", false, type, "",
                        viewModel.PackingListNo,
                        viewModel.PackingType,
                        viewModel.PackingListRemark,
                        viewModel.PackingListAuthorized,
                        viewModel.PackingListLCNumber,
                        viewModel.PackingListIssuedBy,
                        viewModel.PackingListDescription,
                        viewModel.UpdateBySales,
                        viewModel.UomUnit,
                        viewModel.ShippingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                         s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, "", s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, false, "", "", 0,
                         s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "", "", s.AdjDocumentNo,
                         s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                         s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Qty, s.FinishWidth, s.DateIn, viewModel.Date, s.DeliveryOrder.Name, s.InventoryType, s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross,
                         s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());


                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                                 item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade,
                                 null, item.PackagingType, item.PackagingQty, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.ShippingProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.Grade, item.UomUnit, "", item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.Packing, item.PackingType, item.QtyPacking, item.BuyerId, false, "", "", 0,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, "", "", item.AdjDocumentNo,
                        item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.Qty, item.FinishWidth, item.DateIn, viewModel.Date, item.DeliveryOrder.Name, item.InventoryType, item.MaterialOrigin, item.DeliveryOrder.Type, item.PackingListBaleNo, item.PackingListNet, item.PackingListGross,
                        item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name);

                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _productionOrderRepository.InsertAsync(modelItem);

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.QtyPacking * -1);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                                  item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, item.Grade,
                                  null, item.PackingType, item.QtyPacking, item.Packing, item.PackingLength);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }



            return result;
        }

        public async Task<int> Create(OutputShippingViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == DyeingPrintingArea.OUT)
            {
                result = await CreateOut(viewModel);
            }
            else
            {
                result = await CreateAdj(viewModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == SHIPPING &&
            //(((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.SHIPPING);
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
                Group = s.Group,
                ShippingCode = s.ShippingCode,
                Type = s.Type == null || s.Type == DyeingPrintingArea.OUT ? DyeingPrintingArea.OUT : DyeingPrintingArea.ADJ,
                Shift = s.Shift,
                HasSalesInvoice = s.HasSalesInvoice,
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                HasNextAreaDocument = s.HasNextAreaDocument,
                UpdateBySales = s.UpdateBySales,
                ShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputShippingProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    BalanceRemains = d.Balance,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo,
                        Type = d.DeliveryOrderSalesType
                    },
                    MaterialWidth = d.MaterialWidth,
                    MaterialOrigin = d.MaterialOrigin,
                    FinishWidth = d.FinishWidth,
                    Material = new Material()
                    {
                        Id = d.MaterialId,
                        Name = d.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = d.MaterialConstructionName,
                        Id = d.MaterialConstructionId
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    BuyerId = d.BuyerId,
                    Buyer = d.Buyer,
                    HasSalesInvoice = d.HasSalesInvoice,
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
                    ProductTextile = new ProductTextile()
                    { 
                        Id = d.ProductTextileId,
                        Code = d.ProductTextileCode,
                        Name = d.ProductTextileName
                    },
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    HasNextAreaDocument = d.HasNextAreaDocument,
                    AdjDocumentNo = d.AdjDocumentNo,
                    PackingLength = d.PackagingLength,
                    PackingType = d.PackagingType,
                    QtyPacking = d.PackagingQty,
                    Packing = d.PackagingUnit,
                    ShippingGrade = d.ShippingGrade,
                    ShippingRemark = d.ShippingRemark,
                    Weight = d.Weight,
                    UomUnit = d.UomUnit,
                    DeliveryNote = d.DeliveryNote,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                    Qty = d.Balance,
                    InputId = d.DyeingPrintingAreaOutputId,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking,
                    InventoryType = d.InventoryType,
                    DestinationBuyerName = d.DestinationBuyerName


                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputShippingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputShippingViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public async Task<OutputShippingViewModel> ReadByIdBon(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputShippingViewModel vm = await MapToViewModelBon(model);

            return vm;
        }

        public List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByDeliveryOrder(long deliveryOrderId)
        {
            var inputOrders = _inputProductionOrderRepository.ReadAll();
            var ouputOrders = _productionOrderRepository.ReadAll();
            var productionOrders = (from d in inputOrders
                                    join a in ouputOrders on d.Id equals a.DyeingPrintingAreaInputProductionOrderId into l
                                    from a in l.DefaultIfEmpty()
                                    where 
                                    d.IsDeleted == false 
                                    && a.IsDeleted == false
                                    && d.Area == DyeingPrintingArea.SHIPPING
                                    && d.HasOutputDocument == false
                                    && d.DeliveryOrderSalesId == deliveryOrderId
                                   && a.DestinationArea != (string.IsNullOrWhiteSpace(DyeingPrintingArea.PENJUALAN) ? a.DestinationArea : DyeingPrintingArea.PENJUALAN)

                                    orderby d.LastModifiedUtc
                                    select new InputShippingProductionOrderViewModel
                                    {
                                        Buyer = d.Buyer,
                                        BuyerId = d.BuyerId,
                                        CartNo = d.CartNo,
                                        Color = d.Color,
                                        Construction = d.Construction,
                                        HasOutputDocument = d.HasOutputDocument,
                                        Motif = d.Motif,
                                        BalanceRemains = d.BalanceRemains,
                                        ProductionOrder = new ProductionOrder()
                                        {
                                            Id = d.ProductionOrderId,
                                            No = d.ProductionOrderNo,
                                            Type = d.ProductionOrderType,
                                            OrderQuantity = d.ProductionOrderOrderQuantity,
                                        },
                                        MaterialWidth = d.MaterialWidth,
                                        MaterialOrigin = d.MaterialOrigin,
                                        FinishWidth = d.FinishWidth,
                                        Material = new Material()
                                        {
                                            Id = d.MaterialId,
                                            Name = d.MaterialName
                                        },
                                        MaterialConstruction = new MaterialConstruction()
                                        {
                                            Name = d.MaterialConstructionName,
                                            Id = d.MaterialConstructionId
                                        },
                                        YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                                        {
                                            Id = d.YarnMaterialId,
                                            Name = d.YarnMaterialName
                                        },
                                        ProcessType = new CommonViewModelObjectProperties.ProcessType()
                                        {
                                            Id = d.ProcessTypeId,
                                            Name = d.ProcessTypeName
                                        },
                                        Grade = d.Grade,
                                        Id = d.Id,
                                        Unit = d.Unit,
                                        UomUnit = d.UomUnit,
                                        InputId = d.DyeingPrintingAreaInputId,
                                        Remark = d.Remark,
                                        QtyPacking = d.PackagingQty,
                                        PackingType = d.PackagingType,
                                        Packing = d.PackagingUnit,
                                        DeliveryOrder = new DeliveryOrderSales()
                                        {
                                            Id = d.DeliveryOrderSalesId,
                                            No = d.DeliveryOrderSalesNo,
                                            Name = d.DestinationBuyerName,
                                            Type = d.DeliveryOrderSalesType
                                        },
                                        ProductTextile = new ProductTextile()
                                        { 
                                            Id = d.ProductTextileId,
                                            Code = d.ProductTextileCode,
                                            Name = d.ProductTextileName
                                        
                                        },
                                        Qty = Math.Round(d.Balance,2),
                                        PackingInstruction = d.PackingInstruction,
                                        PackingLength = d.PackagingLength,
                                        ProductSKUId = d.ProductSKUId,
                                        FabricSKUId = d.FabricSKUId,
                                        ProductSKUCode = d.ProductSKUCode,
                                        HasPrintingProductSKU = d.HasPrintingProductSKU,
                                        ProductPackingId = d.ProductPackingId,
                                        FabricPackingId = d.FabricPackingId,
                                        ProductPackingCode = d.ProductPackingCode,
                                        HasPrintingProductPacking = d.HasPrintingProductPacking,
                                        InventoryType = d.InventoryType,
                                        DeliveryOrderSalesType = d.DeliveryOrderSalesType


                                    }




                );
 
            //productionOrders = productionOrders.Where(s => s.Area == DyeingPrintingArea.SHIPPING && !s.HasOutputDocument && s.DeliveryOrderSalesId == deliveryOrderId).OrderByDescending(s => s.LastModifiedUtc);
            //var data = productionOrders.Select(d => new InputShippingProductionOrderViewModel()
            //{
            //    Buyer = d.Buyer,
            //    BuyerId = d.BuyerId,
            //    CartNo = d.CartNo,
            //    Color = d.Color,
            //    Construction = d.Construction,
            //    HasOutputDocument = d.HasOutputDocument,
            //    Motif = d.Motif,
            //    BalanceRemains = d.BalanceRemains,
            //    ProductionOrder = new ProductionOrder()
            //    {
            //        Id = d.ProductionOrderId,
            //        No = d.ProductionOrderNo,
            //        Type = d.ProductionOrderType,
            //        OrderQuantity = d.ProductionOrderOrderQuantity,
            //    },
            //    MaterialWidth = d.MaterialWidth,
            //    FinishWidth = d.FinishWidth,
            //    Material = new Material()
            //    {
            //        Id = d.MaterialId,
            //        Name = d.MaterialName
            //    },
            //    MaterialConstruction = new MaterialConstruction()
            //    {
            //        Name = d.MaterialConstructionName,
            //        Id = d.MaterialConstructionId
            //    },
            //    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
            //    {
            //        Id = d.YarnMaterialId,
            //        Name = d.YarnMaterialName
            //    },
            //    ProcessType = new CommonViewModelObjectProperties.ProcessType()
            //    {
            //        Id = d.ProcessTypeId,
            //        Name = d.ProcessTypeName
            //    },
            //    Grade = d.Grade,
            //    Id = d.Id,
            //    Unit = d.Unit,
            //    UomUnit = d.UomUnit,
            //    InputId = d.DyeingPrintingAreaInputId,
            //    Remark = d.Remark,
            //    QtyPacking = d.PackagingQty,
            //    PackingType = d.PackagingType,
            //    Packing = d.PackagingUnit,
            //    DeliveryOrder = new DeliveryOrderSales()
            //    {
            //        Id = d.DeliveryOrderSalesId,
            //        No = d.DeliveryOrderSalesNo
            //    },
            //    Qty = d.Balance,
            //    PackingInstruction = d.PackingInstruction,
            //    PackingLength = d.PackagingLength,
            //    ProductSKUId = d.ProductSKUId,
            //    FabricSKUId = d.FabricSKUId,
            //    ProductSKUCode = d.ProductSKUCode,
            //    HasPrintingProductSKU = d.HasPrintingProductSKU,
            //    ProductPackingId = d.ProductPackingId,
            //    FabricPackingId = d.FabricPackingId,
            //    ProductPackingCode = d.ProductPackingCode,
            //    HasPrintingProductPacking = d.HasPrintingProductPacking

            //});

            return productionOrders.ToList();
        }

        public ListResult<IndexViewModel> ReadForSales(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == DyeingPrintingArea.PENJUALAN && (s.Type == null || s.Type == DyeingPrintingArea.OUT));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            var parentFilterDictionary = FilterDictionary.Where(s => s.Key != DyeingPrintingArea.BuyerId).ToDictionary(s => s.Key, s => s.Value);
            object buyerData;
            int buyerId = 0;
            if (FilterDictionary.TryGetValue(DyeingPrintingArea.BuyerId, out buyerData))
            {
                buyerId = Convert.ToInt32(buyerData);
            }
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, parentFilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Where(s => s.DyeingPrintingAreaOutputProductionOrders.Any(d => d.BuyerId == buyerId)).Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
                HasSalesInvoice = s.HasSalesInvoice,
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                HasNextAreaDocument = s.HasNextAreaDocument,
                ShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Where(d => d.BuyerId == buyerId).Select(d => new OutputShippingProductionOrderViewModel()
                {
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    BuyerId = d.BuyerId,
                    Buyer = d.Buyer,
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
                    MaterialWidth = d.MaterialWidth,
                    MaterialOrigin = d.MaterialOrigin,
                    FinishWidth = d.FinishWidth,
                    Material = new Material()
                    {
                        Id = d.MaterialId,
                        Name = d.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = d.MaterialConstructionName,
                        Id = d.MaterialConstructionId
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    HasSalesInvoice = d.HasSalesInvoice,
                    Remark = d.Remark,
                    PackingType = d.PackagingType,
                    QtyPacking = d.PackagingQty,
                    Packing = d.PackagingUnit,
                    UomUnit = d.UomUnit,
                    DeliveryNote = d.DeliveryNote,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                    Qty = d.Balance,
                    InputId = d.DyeingPrintingAreaOutputId,
                    HasNextAreaDocument = d.HasNextAreaDocument,
                    PackingLength = d.PackagingLength,
                    AdjDocumentNo = d.AdjDocumentNo,
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

        public async Task<int> UpdateHasSalesInvoice(int id, OutputShippingUpdateSalesInvoiceViewModel salesInvoice)
        {
            int result = 0;
            var flag = salesInvoice.HasSalesInvoice;
            result += await _repository.UpdateHasSalesInvoice(id, flag);

            foreach (var item in salesInvoice.ItemIds)
            {
                result += await _productionOrderRepository.UpdateHasSalesInvoice(item, flag);
            }

            return result;
        }

        private async Task<int> DeleteOut(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;

            if (model.DestinationArea == DyeingPrintingArea.PENJUALAN && model.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument && s.HasSalesInvoice))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Sales Invoice!");
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument && model.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, item.MaterialOrigin, model.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade,
                       null, item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteShippingArea(model);

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
                   item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName,
                   item.Grade, null, item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _repository.DeleteAdjustment(model);

            return result;
        }

        public async Task<int> Delete(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return await DeleteOut(model);
            }
            else
            {
                return await DeleteAdj(model);
            }
        }

        private async Task<int> UpdateOut(int id, OutputShippingViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder == null ? 0 : viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder?.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                        viewModel.PackingListNo,
                        viewModel.PackingType,
                        viewModel.PackingListRemark,
                        viewModel.PackingListAuthorized,
                        viewModel.PackingListLCNumber,
                        viewModel.PackingListIssuedBy,
                        viewModel.PackingListDescription,
                        viewModel.UpdateBySales,
                        viewModel.UomUnit,
                        viewModel.ShippingProductionOrders.Select(s =>
                        new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, s.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id,
                            s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote,
                            s.Qty, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                            s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "",
                            s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                            s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.FinishWidth, s.DateIn, viewModel.Date, s.DeliveryOrder.Name, s.InventoryType, s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross,
                            s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)

                        {
                            Id = s.Id
                        }).ToList());

            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, decimal> dictQtyPacking = new Dictionary<int, decimal>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;
                    var diffQtyPacking = lclModel.PackagingQty - item.PackagingQty;
                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictQtyPacking.Add(lclModel.Id, diffQtyPacking);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument && !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();
            if (model.DestinationArea == DyeingPrintingArea.PENJUALAN && deletedData.Any(s => !s.HasNextAreaDocument && s.HasSalesInvoice))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Sales Invoice!");
            }
            result = await _repository.UpdateShippingArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                decimal newQtyPacking = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }

                if (!dictQtyPacking.TryGetValue(item.Id, out newQtyPacking))
                {
                    newQtyPacking = item.PackagingQty;
                }

                if (newQtyPacking != 0 && newBalance != 0 && dbModel.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName,
                           item.Grade, null, item.PackagingType, newQtyPacking, item.PackagingUnit, item.PackagingLength, item.InventoryType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                if (dbModel.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, item.Grade,
                       null, item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength, item.InventoryType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputShippingViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            if (viewModel.ShippingProductionOrders.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group, type,
                    viewModel.ShippingProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", s.ProductionOrder.Id,
                        s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, "", s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, "", s.QtyPacking, s.BuyerId, false, "", "", 0,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "", "", s.AdjDocumentNo,
                        s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Qty, s.FinishWidth, s.DateIn, viewModel.Date, s.DeliveryOrder.Name, s.InventoryType, s.MaterialOrigin, s.DeliveryOrder.Type, s.PackingListBaleNo, s.PackingListNet, s.PackingListGross,
                        s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)

                    {
                        Id = s.Id
                    }).ToList(), null);
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, decimal> dictQtyPacking = new Dictionary<int, decimal>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;
                    var diffQtyPacking = lclModel.PackagingQty - item.PackagingQty;
                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictQtyPacking.Add(lclModel.Id, diffQtyPacking);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdateAdjustmentData(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.IsDeleted))
            {
                double newBalance = 0;
                decimal newQtyPacking = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }

                if (!dictQtyPacking.TryGetValue(item.Id, out newQtyPacking))
                {
                    newQtyPacking = item.PackagingQty;
                }

                if (newQtyPacking != 0 && newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName,
                        item.Grade, null, item.PackagingType, newQtyPacking, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName,
                        item.Grade, null, item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputShippingViewModel viewModel)
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

        private MemoryStream GenerateExcelOut(OutputShippingViewModel viewModel, int offSet)
        {
            var query = viewModel.ShippingProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade 1", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade 2", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Berat", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SJ", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    var dateOut = item.DateOut.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                    dt.Rows.Add(item.ProductionOrder.No,
                        dateIn,
                        dateOut,
                        item.Buyer,
                        item.Construction,
                        item.MaterialOrigin,
                        item.Unit, item.Color,
                        item.Motif, item.PackingType, item.Grade,
                        item.ShippingGrade,
                        item.ShippingRemark,
                        item.QtyPacking.ToString("N2", CultureInfo.InvariantCulture),
                        item.Packing,
                        item.Qty.ToString("N2", CultureInfo.InvariantCulture), item.Weight.ToString("N2", CultureInfo.InvariantCulture), item.DeliveryNote, "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }

        private MemoryStream GenerateExcelAdj(OutputShippingViewModel viewModel)
        {
            var query = viewModel.ShippingProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade 1", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity Total", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.ProductionOrder.Type,
                        item.Construction, item.MaterialOrigin, item.Unit, item.Buyer, item.Color, item.Motif, item.Grade, item.QtyPacking.ToString("N2", CultureInfo.InvariantCulture),
                        item.Packing, item.UomUnit, item.Qty.ToString("N2", CultureInfo.InvariantCulture), item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }

        public MemoryStream GenerateExcel(OutputShippingViewModel viewModel, int clientTimeZoneOffset)
        {
            if (viewModel.Type == null || viewModel.Type == DyeingPrintingArea.OUT)
            {
                return GenerateExcelOut(viewModel, clientTimeZoneOffset);
            }
            else
            {
                return GenerateExcelAdj(viewModel);
            }
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == SHIPPING &&
            //       (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.SHIPPING);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }


            query = query.OrderBy(s => s.Type).ThenBy(s => s.DestinationArea).ThenBy(d => d.BonNo);

            var modelAll = query.Select(s => new
            {
                Type = s.Type,
                SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
                {
                    BonNo = s.BonNo,
                    ProductionOrderNo = d.ProductionOrderNo,
                    ProductionOrderId = d.ProductionOrderId,
                    DeliveryOrderSalesNo = d.DeliveryOrderSalesNo,
                    DateIn = d.DateIn,
                    DateOut = d.DateOut,
                    ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                    Construction = d.Construction,
                    MaterialOrigin = d.MaterialOrigin,
                    Unit = d.Unit,
                    Buyer = d.Buyer,
                    Color =d.Color,
                    Motif = d.Motif,
                    PackagingType = d.PackagingType,
                    Grade = d.Grade,
                    ShippingGrade = d.ShippingGrade,
                    ShippingRemark = d.ShippingRemark,
                    PackagingQty = d.PackagingQty,
                    PackagingUnit = d.PackagingUnit,
                    Balance = d.Balance,
                    
                    UomUnit = d.UomUnit,
                    DeliveryNote = d.DeliveryNote,
                    DestinationArea = s.DestinationArea,

                    NextAreaInputStatus = d.NextAreaInputStatus
                })
            });

            if (type == "BON")
            {
                modelAll = modelAll.Select(s => new
                {
                    Type = s.Type,
                    SppList = s.SppList.GroupBy(r => new { r.ProductionOrderId, r.Grade, r.NextAreaInputStatus }).Select(d => new
                    {
                        BonNo = d.First().BonNo,
                        ProductionOrderNo = d.First().ProductionOrderNo,
                        ProductionOrderId = d.Key.ProductionOrderId,
                        DeliveryOrderSalesNo = d.First().DeliveryOrderSalesNo,
                        DateIn = d.First().DateIn,
                        DateOut = d.First().DateOut,
                        ProductionOrderOrderQuantity = d.First().ProductionOrderOrderQuantity,
                        Construction = d.First().Construction,
                        MaterialOrigin = d.First().MaterialOrigin,
                        Unit = d.First().Unit,
                        Buyer = d.First().Buyer,
                        Color = d.First().Color,
                        Motif = d.First().Motif,
                        PackagingType = d.First().PackagingType,
                        Grade = d.First().Grade,
                        ShippingGrade = d.First().ShippingGrade,
                        ShippingRemark = d.First().ShippingRemark,
                        PackagingQty = d.Sum( x => x.PackagingQty),
                        PackagingUnit = d.First().PackagingUnit,
                        Balance = d.Sum(x => x.Balance),
                        UomUnit = d.First().UomUnit,
                        DeliveryNote = d.First().DeliveryNote,
                        DestinationArea = d.First().DestinationArea,

                        NextAreaInputStatus = d.First().NextAreaInputStatus


                    })
                });
            }
            else
            {
                modelAll = modelAll.Select(s => new
                {
                    Type = s.Type,
                    SppList = s.SppList.Select(d => new
                    {
                        BonNo = d.BonNo,
                        ProductionOrderNo = d.ProductionOrderNo,
                        ProductionOrderId = d.ProductionOrderId,
                        DeliveryOrderSalesNo = d.DeliveryOrderSalesNo,
                        DateIn = d.DateIn,
                        DateOut = d.DateOut,
                        ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                        Construction = d.Construction,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Color = d.Color,
                        Motif = d.Motif,
                        PackagingType = d.PackagingType,
                        Grade = d.Grade,
                        ShippingGrade = d.ShippingGrade,
                        ShippingRemark = d.ShippingRemark,
                        PackagingQty = d.PackagingQty,
                        PackagingUnit = d.PackagingUnit,
                        Balance = d.Balance,
                        UomUnit = d.UomUnit,
                        DeliveryNote = d.DeliveryNote,
                        DestinationArea = d.DestinationArea,
                        NextAreaInputStatus = d.NextAreaInputStatus
                    })
                });
            }

            var query1 = modelAll;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Delivery Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Keluar", DataType = typeof(string) });

            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });

            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SJ", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Zona Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(string) });

            if (query1.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query1)
                {
                    if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
                    {
                        //foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.ProductionOrderNo))
                        foreach (var item in model.SppList.OrderBy(s => s.ProductionOrderNo))
                        {
                            var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                            var dateOut = item.DateOut.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                            dt.Rows.Add(item.BonNo,
                                item.ProductionOrderNo,
                                item.DeliveryOrderSalesNo,
                                dateIn,
                                dateOut,
                                item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.Construction,
                                item.MaterialOrigin,
                                item.Unit,
                                item.Buyer,
                                item.Color,
                                item.Motif,
                                item.PackagingType,
                                item.Grade,
                                item.ShippingRemark,
                                item.PackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                                item.PackagingUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture),
                                item.UomUnit,
                                item.DeliveryNote,
                                item.DestinationArea,
                                DyeingPrintingArea.OUT,
                                item.NextAreaInputStatus);

                        }
                    }
                    else
                    {
                        foreach (var item in model.SppList.OrderBy(s => s.ProductionOrderNo))
                        {
                            var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                            var dateOut = item.DateOut.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");


                            dt.Rows.Add(item.BonNo,
                                item.ProductionOrderNo,
                                item.DeliveryOrderSalesNo,
                                dateIn,
                                dateOut,
                                item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.Construction,
                                item.MaterialOrigin,
                                item.Unit,
                                item.Buyer,
                                item.Color,
                                item.Motif,
                                item.PackagingType,
                                item.ShippingGrade,
                                item.ShippingRemark,
                                item.PackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                                item.PackagingUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture),
                                item.UomUnit,
                                item.DeliveryNote,
                                item.DestinationArea,
                                DyeingPrintingArea.ADJ,
                                "");

                        }
                    }

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }

        public ListResult<AdjShippingProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputProductionOrderRepository.ReadAll()
            //    .Where(s => s.Area == SHIPPING && !s.HasOutputDocument)
            //    .Select(d => new PlainAdjShippingProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Grade = d.Grade,
            //        Packing = d.PackagingUnit,
            //        Color = d.Color,
            //        Construction = d.Construction,
            //        MaterialConstructionId = d.MaterialConstructionId,
            //        MaterialConstructionName = d.MaterialConstructionName,
            //        MaterialId = d.MaterialId,
            //        MaterialName = d.MaterialName,
            //        MaterialWidth = d.MaterialWidth,
            //        Motif = d.Motif,
            //        ProductionOrderId = d.ProductionOrderId,
            //        ProductionOrderNo = d.ProductionOrderNo,
            //        ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
            //        ProductionOrderType = d.ProductionOrderType,
            //        ProcessTypeId = d.ProcessTypeId,
            //        ProcessTypeName = d.ProcessTypeName,
            //        YarnMaterialId = d.YarnMaterialId,
            //        YarnMaterialName = d.YarnMaterialName,
            //        Unit = d.Unit,
            //        UomUnit = d.UomUnit,
            //        PackingType = d.PackagingType,
            //        ProductSKUId = d.ProductSKUId,
            //        FabricSKUId = d.FabricSKUId,
            //        ProductSKUCode = d.ProductSKUCode,
            //        HasPrintingProductSKU = d.HasPrintingProductSKU,
            //        ProductPackingId = d.ProductPackingId,
            //        FabricPackingId = d.FabricPackingId,
            //        ProductPackingCode = d.ProductPackingCode,
            //        HasPrintingProductPacking = d.HasPrintingProductPacking
            //    })
            //    .Union(_productionOrderRepository.ReadAll()
            //    .Where(s => s.Area == SHIPPING && !s.HasNextAreaDocument)
            //    .Select(d => new PlainAdjShippingProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Grade = d.Grade,
            //        Packing = d.PackagingUnit,
            //        Color = d.Color,
            //        Construction = d.Construction,
            //        MaterialConstructionId = d.MaterialConstructionId,
            //        MaterialConstructionName = d.MaterialConstructionName,
            //        MaterialId = d.MaterialId,
            //        MaterialName = d.MaterialName,
            //        MaterialWidth = d.MaterialWidth,
            //        Motif = d.Motif,
            //        ProductionOrderId = d.ProductionOrderId,
            //        ProductionOrderNo = d.ProductionOrderNo,
            //        ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
            //        ProductionOrderType = d.ProductionOrderType,
            //        ProcessTypeId = d.ProcessTypeId,
            //        ProcessTypeName = d.ProcessTypeName,
            //        YarnMaterialId = d.YarnMaterialId,
            //        YarnMaterialName = d.YarnMaterialName,
            //        Unit = d.Unit,
            //        UomUnit = d.UomUnit,
            //        PackingType = d.PackagingType,
            //        ProductSKUId = d.ProductSKUId,
            //        FabricSKUId = d.FabricSKUId,
            //        ProductSKUCode = d.ProductSKUCode,
            //        HasPrintingProductSKU = d.HasPrintingProductSKU,
            //        ProductPackingId = d.ProductPackingId,
            //        FabricPackingId = d.FabricPackingId,
            //        ProductPackingCode = d.ProductPackingCode,
            //        HasPrintingProductPacking = d.HasPrintingProductPacking
            //    }));

            var query = _inputProductionOrderRepository.ReadAll()
               .Where(s => s.Area == DyeingPrintingArea.SHIPPING && !s.HasOutputDocument)
               .Select(d => new PlainAdjShippingProductionOrder()
               {
                   Balance = d.Balance,
                   PackagingQty = d.PackagingQty,
                   Id = d.Id,
                   BalanceRemains = d.BalanceRemains,
                   Area = d.Area,
                   Buyer = d.Buyer,
                   BuyerId = d.BuyerId,
                   Grade = d.Grade,
                   Packing = d.PackagingUnit,
                   Color = d.Color,
                   Construction = d.Construction,
                   MaterialConstructionId = d.MaterialConstructionId,
                   MaterialConstructionName = d.MaterialConstructionName,
                   MaterialId = d.MaterialId,
                   MaterialName = d.MaterialName,
                   MaterialWidth = d.MaterialWidth,
                   MaterialOrigin = d.MaterialOrigin,
                   FinishWidth = d.FinishWidth,
                   Motif = d.Motif,
                   ProductionOrderId = d.ProductionOrderId,
                   ProductionOrderNo = d.ProductionOrderNo,
                   ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                   ProductionOrderType = d.ProductionOrderType,
                   ProcessTypeId = d.ProcessTypeId,
                   ProcessTypeName = d.ProcessTypeName,
                   YarnMaterialId = d.YarnMaterialId,
                   YarnMaterialName = d.YarnMaterialName,
                   PackagingLength = d.PackagingLength,
                   Unit = d.Unit,
                   UomUnit = d.UomUnit,
                   PackingType = d.PackagingType,
                   ProductSKUId = d.ProductSKUId,
                   FabricSKUId = d.FabricSKUId,
                   ProductSKUCode = d.ProductSKUCode,
                   HasPrintingProductSKU = d.HasPrintingProductSKU,
                   ProductPackingId = d.ProductPackingId,
                   FabricPackingId = d.FabricPackingId,
                   ProductPackingCode = d.ProductPackingCode,
                   HasPrintingProductPacking = d.HasPrintingProductPacking
               });

            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<PlainAdjShippingProductionOrder>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjShippingProductionOrder>.Filter(query, FilterDictionary);

            var data = query.ToList()
                //.GroupBy(d => d.ProductionOrderId)
                //.Select(s => s.First())
                .OrderBy(s => s.ProductionOrderNo)
                .Skip((page - 1) * size).Take(size)
                .Select(s => new AdjShippingProductionOrderViewModel()
                {
                    DyeingPrintingAreaInputProductionOrderId = s.Id,
                    BalanceRemains = s.BalanceRemains,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    },
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    Grade = s.Grade,
                    Packing = s.Packing,
                    Color = s.Color,
                    Construction = s.Construction,
                    Material = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = s.MaterialConstructionId,
                        Name = s.MaterialConstructionName
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
                    QtyPacking = s.PackagingQty,
                    Qty = s.PackagingLength,
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    Motif = s.Motif,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    PackingType = s.PackingType,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    HasPrintingProductPacking = s.HasPrintingProductPacking,

                });

            return new ListResult<AdjShippingProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<InputShippingProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.Area == DyeingPrintingArea.SHIPPING && !s.HasOutputDocument);
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
                .GroupBy(d => d.ProductionOrderId)
                .Select(s => s.First())
                .Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.ProductionOrderNo)
                .Select(s => new InputShippingProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    }
                });

            return new ListResult<InputShippingProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByProductionOrder(long productionOrderId)
        {
            IQueryable<DyeingPrintingAreaInputProductionOrderModel> productionOrders;

            var data2 = new List<InputShippingProductionOrderViewModel>();

            //if (productionOrderId == 0)
            //{
            //    productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
            //                        .Where(s => s.Area == DyeingPrintingArea.SHIPPING && !s.HasOutputDocument).Take(75);
            //}
            //else
            //{
                productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                                    .Where(s => s.Area == DyeingPrintingArea.SHIPPING && !s.HasOutputDocument && s.ProductionOrderId == productionOrderId).Take(75);

            //}
            var data = productionOrders.Select(d => new InputShippingProductionOrderViewModel()
            {
                Buyer = d.Buyer,
                BuyerId = d.BuyerId,
                CartNo = d.CartNo,
                Color = d.Color,
                Construction = d.Construction,
                HasOutputDocument = d.HasOutputDocument,
                Motif = d.Motif,
                BalanceRemains = d.BalanceRemains,
                ProductionOrder = new ProductionOrder()
                {
                    Id = d.ProductionOrderId,
                    No = d.ProductionOrderNo,
                    Type = d.ProductionOrderType,
                    OrderQuantity = d.ProductionOrderOrderQuantity,
                },
                MaterialWidth = d.MaterialWidth,
                MaterialOrigin = d.MaterialOrigin,
                FinishWidth = d.FinishWidth,
                Material = new Material()
                {
                    Id = d.MaterialId,
                    Name = d.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = d.MaterialConstructionName,
                    Id = d.MaterialConstructionId
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = d.YarnMaterialId,
                    Name = d.YarnMaterialName
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = d.ProcessTypeId,
                    Name = d.ProcessTypeName
                },
                Grade = d.Grade,
                Id = d.Id,
                Unit = d.Unit,
                UomUnit = d.UomUnit,
                InputId = d.DyeingPrintingAreaInputId,
                Remark = d.Remark,
                QtyPacking = d.PackagingQty,
                PackingType = d.PackagingType,
                Packing = d.PackagingUnit,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = d.DeliveryOrderSalesId,
                    No = d.DeliveryOrderSalesNo,
                    Type = d.DeliveryOrderSalesType
                },
                Qty = Math.Round(d.Balance, 2),
                PackingInstruction = d.PackingInstruction,
                PackingLength = d.PackagingLength,
                ProductSKUId = d.ProductSKUId,
                FabricSKUId = d.FabricSKUId,
                ProductSKUCode = d.ProductSKUCode,
                HasPrintingProductSKU = d.HasPrintingProductSKU,
                ProductPackingId = d.ProductPackingId,
                FabricPackingId = d.FabricPackingId,
                ProductPackingCode = d.ProductPackingCode,
                HasPrintingProductPacking = d.HasPrintingProductPacking,
                DateIn = d.DateIn,
                ProductTextile = new ProductTextile()
                { 
                    Id = d.ProductTextileId,
                    Name = d.ProductTextileName,
                    Code = d.ProductTextileCode
                },
                BonNo = d.Id != 0 ? _repository.ReadAll().Where(x => x.DyeingPrintingAreaOutputProductionOrders.Any(s => s.DyeingPrintingAreaInputProductionOrderId == d.Id)).FirstOrDefault().BonNo == null ? "-" : _repository.ReadAll().Where(x => x.DyeingPrintingAreaOutputProductionOrders.Any(s => s.DyeingPrintingAreaInputProductionOrderId == d.Id)).FirstOrDefault().BonNo : "-",
                InventoryType = d.InventoryType
            });         

            return data.ToList();
        }
        public virtual async Task<MemoryStreamResult> ReadPdfPackingListById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var PdfTemplate = new OutputShippingPackingListPdfTemplate(_identityProvider);
            //var fob = _invoiceRepository.ReadAll().Where(w => w.PackingListId == data.Id).Select(s => s.CPrice == "FOB" || s.CPrice == "FCA" ? s.From : s.To).FirstOrDefault();
            //var cPrice = _invoiceRepository.ReadAll().Where(w => w.PackingListId == data.Id).Select(s => s.CPrice).FirstOrDefault();

            var viewModel = MapToViewModelPackingList(data);
            //viewModel.ShippingMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.ShippingMarkImagePath);
            //viewModel.SideMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.SideMarkImagePath);
            //viewModel.RemarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.RemarkImagePath);

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel);

            return new MemoryStreamResult(stream, "Packing List " + data.PackingListNo + ".pdf");
        }
        public MemoryStream GenerateExcel(OutputShippingViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
