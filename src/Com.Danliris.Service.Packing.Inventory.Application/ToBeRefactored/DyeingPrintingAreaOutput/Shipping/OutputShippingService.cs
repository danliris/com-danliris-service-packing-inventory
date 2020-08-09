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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingService : IOutputShippingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOrderRepository;

        private const string OUT = "OUT";
        private const string ADJ = "ADJ";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";
        private const string PJ = "PJ";
        private const string ADJ_IN = "ADJ IN";
        private const string ADJ_OUT = "ADJ OUT";

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string PENJUALAN = "PENJUALAN";
        private const string BUYER = "BUYER";

        private const string BuyerId = "BuyerId";

        public OutputShippingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private async Task<OutputShippingViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputShippingViewModel();
            if (model.Type == null || model.Type == OUT)
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
                    Type = OUT,
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
                            No = s.DeliveryOrderSalesNo
                        },
                        MaterialWidth = s.MaterialWidth,
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
                        PackingLength = s.PackagingQty == 0 ? 0 : Convert.ToDecimal(s.Balance) / s.PackagingQty,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        BalanceRemains = s.Balance
                    }).ToList()
                };

                if(vm.DestinationArea != BUYER)
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
                    Type = ADJ,
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
                        Qty = s.PackagingQty == 0 ? 0 : s.Balance / Convert.ToDouble(s.PackagingQty),
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
                        HasPrintingProductPacking = s.HasPrintingProductPacking
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

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            if (destinationArea == INSPECTIONMATERIAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", SP, IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

            }
            else if (destinationArea == TRANSIT)
            {
                return string.Format("{0}.{1}.{2}.{3}", SP, TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == PACKING)
            {
                return string.Format("{0}.{1}.{2}.{3}", SP, PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}.{3}", SP, GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", SP, PJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoPenjualan(int totalPreviousData, DateTimeOffset date, string shippingCode)
        {
            return string.Format("{0}.{1}.{2}", shippingCode, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_IN, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_OUT, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputShippingViewModel viewModel)
        {
            int result = 0;
            DyeingPrintingAreaOutputModel model = null;

            if(viewModel.DestinationArea == PENJUALAN)
            {
                model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == SHIPPING && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == OUT && s.ShippingCode == viewModel.ShippingCode);
            }
            else
            {
                model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == SHIPPING && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == OUT);
            }

            viewModel.ShippingProductionOrders = viewModel.ShippingProductionOrders.Where(s => s.IsSave).ToList();

            if (model == null)
            {
                if (viewModel.DestinationArea != BUYER)
                {
                    int totalCurrentYearData = 0;
                    string bonNo = "";

                    if (viewModel.DestinationArea == PENJUALAN)
                    {
                        totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING
                            && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == OUT && s.ShippingCode == viewModel.ShippingCode);
                        bonNo = GenerateBonNoPenjualan(totalCurrentYearData + 1, viewModel.Date, viewModel.ShippingCode);
                    }
                    else
                    {
                        totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING
                            && s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == OUT);
                        bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
                    }


                    model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                        viewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                       s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                       s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                       s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking)).ToList());
                }
                else
                {
                    model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, false, viewModel.DestinationArea, viewModel.Group,
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode,
                        viewModel.ShippingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote, s.Qty, s.Id, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "", s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking)).ToList());
                }


                result = await _repository.InsertAsync(model);
                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    var itemVM = viewModel.ShippingProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaInputProductionOrderId);
                    if (model.DestinationArea != BUYER)
                    {
                        if (model.DestinationArea == INSPECTIONMATERIAL)
                        {

                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance, item.PackagingQty);
                        }
                        else
                        {

                            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                        }

                        if (model.DestinationArea != PENJUALAN)
                        {
                            var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Grade,
                            null, item.PackagingType);

                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(itemVM.Id, true);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.Balance, item.PackagingQty);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Grade,
                            null, item.PackagingType);

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
                       item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;
                    result += await _productionOrderRepository.InsertAsync(modelItem);
                    if (viewModel.DestinationArea != BUYER)
                    {
                        if (viewModel.DestinationArea == INSPECTIONMATERIAL)
                        {

                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, item.Balance, item.QtyPacking);
                        }
                        else
                        {

                            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);
                        }

                        if (viewModel.DestinationArea != PENJUALAN)
                        {
                            var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, item.Grade,
                           null, item.PackingType);

                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                    }
                    else
                    {
                        result += await _productionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Qty, item.QtyPacking);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty, modelItem.Id, item.ProductionOrder.Type, item.Grade,
                           null, item.PackingType);

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
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.Type == ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.ShippingProductionOrders.Select(d => d.Balance));
                type = ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.Type == ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.ShippingProductionOrders.Select(d => d.Balance));
                type = ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _repository.GetDbSet().AsNoTracking()
                    .FirstOrDefault(s => s.Area == SHIPPING && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type);

            if(model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group,
                        0, "", false, type, "", viewModel.ShippingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, "", s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, false, "", "", 0,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "", "", s.AdjDocumentNo,
                        s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking)).ToList());

                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                                 item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Grade,
                                 null, item.PackagingType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach(var item in viewModel.ShippingProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.Grade, item.UomUnit, "", item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.Packing, item.PackingType, item.QtyPacking, item.BuyerId, false, "", "", 0,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, "", "", item.AdjDocumentNo,
                        item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _productionOrderRepository.InsertAsync(modelItem);

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.QtyPacking * -1);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                                  item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, item.Grade,
                                  null, item.PackingType);

                    result += await _movementRepository.InsertAsync(movementModel);
                    
                }
            }

            

            return result;
        }

        public async Task<int> Create(OutputShippingViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == OUT)
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
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING);
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
                Type = s.Type == null || s.Type == OUT ? OUT : ADJ,
                Shift = s.Shift,
                HasSalesInvoice = s.HasSalesInvoice,
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                HasNextAreaDocument = s.HasNextAreaDocument,
                ShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputShippingProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    BalanceRemains = d.Balance,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    MaterialWidth = d.MaterialWidth,
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
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    HasNextAreaDocument = d.HasNextAreaDocument,
                    AdjDocumentNo = d.AdjDocumentNo,
                    PackingLength = d.PackagingQty == 0 ? 0 : Convert.ToDecimal(d.Balance) / d.PackagingQty,
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
                    HasPrintingProductPacking = d.HasPrintingProductPacking

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

        public List<InputShippingProductionOrderViewModel> GetInputShippingProductionOrdersByDeliveryOrder(long deliveryOrderId)
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll();

            productionOrders = productionOrders.Where(s => s.Area == SHIPPING && !s.HasOutputDocument && s.DeliveryOrderSalesId == deliveryOrderId).OrderByDescending(s => s.LastModifiedUtc);
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
                    No = d.DeliveryOrderSalesNo
                },
                Qty = d.Balance,
                PackingInstruction = d.PackingInstruction,
                PackingLength = d.PackagingLength,
                ProductSKUId = d.ProductSKUId,
                FabricSKUId = d.FabricSKUId,
                ProductSKUCode = d.ProductSKUCode,
                HasPrintingProductSKU = d.HasPrintingProductSKU,
                ProductPackingId = d.ProductPackingId,
                FabricPackingId = d.FabricPackingId,
                ProductPackingCode = d.ProductPackingCode,
                HasPrintingProductPacking = d.HasPrintingProductPacking

            });

            return data.ToList();
        }

        public ListResult<IndexViewModel> ReadForSales(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING && s.DestinationArea == PENJUALAN && (s.Type == null || s.Type == OUT));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            var parentFilterDictionary = FilterDictionary.Where(s => s.Key != BuyerId).ToDictionary(s => s.Key, s => s.Value);
            object buyerData;
            int buyerId = 0;
            if (FilterDictionary.TryGetValue(BuyerId, out buyerData))
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
                    PackingLength = d.PackagingQty == 0 ? 0 : Convert.ToDecimal(d.Balance) / d.PackagingQty,
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

            if (model.DestinationArea == PENJUALAN && model.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument && s.HasSalesInvoice))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Sales Invoice!");
            }

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument && model.DestinationArea != PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);

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
                type = ADJ_IN;
            }
            else
            {
                type = ADJ_OUT;
            }
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                   item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);

                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _repository.DeleteAdjustment(model);

            return result;
        }

        public async Task<int> Delete(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == OUT)
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
                        viewModel.DeliveryOrder.Id, viewModel.DeliveryOrder.No, viewModel.HasSalesInvoice, viewModel.Type, viewModel.ShippingCode, viewModel.ShippingProductionOrders.Select(s =>
                        new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, s.HasNextAreaDocument, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id,
                        s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, s.DeliveryNote,
                        s.Qty, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, s.PackingType, s.QtyPacking, s.BuyerId, s.HasSalesInvoice, s.ShippingGrade, s.ShippingRemark, s.Weight,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, "",
                        s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking)
                        {
                            Id = s.Id
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

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument && !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();
            if (model.DestinationArea == PENJUALAN && deletedData.Any(s => !s.HasNextAreaDocument && s.HasSalesInvoice))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Sales Invoice!");
            }
            result = await _repository.UpdateShippingArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0 && dbModel.DestinationArea != PENJUALAN)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                if (dbModel.DestinationArea != PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);

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
                type = ADJ_IN;
            }
            else
            {
                type = ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group, type,
                    viewModel.ShippingProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, 0, "", s.ProductionOrder.Id,
                        s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction,
                        s.Unit, s.Color, s.Motif, s.Grade, s.UomUnit, "", s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.Packing, "", s.QtyPacking, s.BuyerId, false, "", "", 0,
                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "", "", s.AdjDocumentNo,
                        s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking)
                    {
                        Id = s.Id
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
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null, item.PackagingType);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputShippingViewModel viewModel)
        {
            if (viewModel.Type == OUT)
            {
                return await UpdateOut(id, viewModel);
            }
            else
            {
                return await UpdateAdj(id, viewModel);
            }
        }

        private MemoryStream GenerateExcelOut(OutputShippingViewModel viewModel)
        {
            var query = viewModel.ShippingProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
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
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.PackingType, item.Grade, item.ShippingGrade,
                        item.ShippingRemark, item.QtyPacking.ToString("N2", CultureInfo.InvariantCulture), item.Packing,
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
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.ProductionOrder.Type,
                        item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Grade, item.QtyPacking.ToString("N2", CultureInfo.InvariantCulture),
                        item.Packing, item.UomUnit, item.Qty.ToString("N2", CultureInfo.InvariantCulture), item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }

        public MemoryStream GenerateExcel(OutputShippingViewModel viewModel)
        {
            if (viewModel.Type == null || viewModel.Type == OUT)
            {
                return GenerateExcelOut(viewModel);
            }
            else
            {
                return GenerateExcelAdj(viewModel);
            }
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == SHIPPING &&
            //       (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING);

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

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Delivery Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
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

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    if (model.Type == null || model.Type == OUT)
                    {
                        //foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.ProductionOrderNo))
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.DeliveryOrderSalesNo, item.ProductionOrderNo, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.PackagingType, item.ShippingGrade, item.ShippingRemark,
                                item.PackagingQty.ToString("N2", CultureInfo.InvariantCulture), item.PackagingUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.UomUnit, item.DeliveryNote, model.DestinationArea, OUT);

                        }
                    }
                    else
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.DeliveryOrderSalesNo, item.ProductionOrderNo, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.PackagingType, item.ShippingGrade, item.ShippingRemark,
                                item.PackagingQty.ToString("N2", CultureInfo.InvariantCulture), item.PackagingUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.UomUnit, item.DeliveryNote, model.DestinationArea, ADJ);

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
               .Where(s => s.Area == SHIPPING && !s.HasOutputDocument)
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
                   Motif = d.Motif,
                   ProductionOrderId = d.ProductionOrderId,
                   ProductionOrderNo = d.ProductionOrderNo,
                   ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                   ProductionOrderType = d.ProductionOrderType,
                   ProcessTypeId = d.ProcessTypeId,
                   ProcessTypeName = d.ProcessTypeName,
                   YarnMaterialId = d.YarnMaterialId,
                   YarnMaterialName = d.YarnMaterialName,
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
                    Qty = s.PackagingQty == 0 ? 0 : s.Balance / Convert.ToDouble(s.PackagingQty),
                    MaterialWidth = s.MaterialWidth,
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
                    HasPrintingProductPacking = s.HasPrintingProductPacking
                });

            return new ListResult<AdjShippingProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
