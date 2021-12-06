using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;


namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseService : IOutputWarehouseService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaReferenceRepository _areaReferenceRepository;

        public OutputWarehouseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _areaReferenceRepository = serviceProvider.GetService<IDyeingPrintingAreaReferenceRepository>();
        }

        private async Task<OutputWarehouseViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputWarehouseViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputWarehouseViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    Type = DyeingPrintingArea.OUT,
                    Bon = new IndexViewModel
                    {
                        Area = model.Area,
                        BonNo = model.BonNo,
                        DestinationArea = model.DestinationArea,
                        Shift = model.Shift,
                        Group = model.Group,
                        Date = model.Date,
                        HasNextAreaDocument = model.HasNextAreaDocument,
                        Id = model.Id
                    },
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    Group = model.Group,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    WarehousesProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputWarehouseProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Balance = s.Balance,
                        Buyer = s.Buyer,
                        CartNo = s.CartNo,
                        BuyerId = s.BuyerId,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        PackingInstruction = s.PackingInstruction,
                        Remark = s.Remark,
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
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
                        },
                        Unit = s.Unit,
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        MaterialProduct = new Material()
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
                        UomUnit = s.UomUnit,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        PackagingQty = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        ProductionOrderNo = s.ProductionOrderNo,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                        DeliveryOrderSalesId = s.DeliveryOrderSalesId,
                        DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                        AdjDocumentNo = s.AdjDocumentNo,
                        Quantity = s.PackagingLength,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                        DateIn = s.DateIn,
                        DateOut = s.DateOut,
                    }).ToList()
                };
                foreach (var item in vm.WarehousesProductionOrders)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        item.BalanceRemains = inputData.BalanceRemains + item.Balance;
                    }
                }
            }
            else
            {
                vm = new OutputWarehouseViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    AdjType = model.Type,
                    Bon = new IndexViewModel
                    {
                        Area = model.Area,
                        BonNo = model.BonNo,
                        DestinationArea = model.DestinationArea,
                        Shift = model.Shift,
                        Group = model.Group,
                        Date = model.Date,
                        HasNextAreaDocument = model.HasNextAreaDocument,
                        Id = model.Id
                    },
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    Group = model.Group,
                    DeletedUtc = model.DeletedUtc,
                    Type = DyeingPrintingArea.ADJ,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    WarehousesProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputWarehouseProductionOrderViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Balance = s.Balance,
                        Buyer = s.Buyer,
                        AdjDocumentNo = s.AdjDocumentNo,
                        Quantity = s.PackagingLength,
                        MaterialWidth = s.MaterialWidth,
                        MaterialOrigin = s.MaterialOrigin,
                        FinishWidth = s.FinishWidth,
                        MaterialProduct = new Material()
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
                        CartNo = s.CartNo,
                        BuyerId = s.BuyerId,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Grade = s.Grade,
                        PackingInstruction = s.PackingInstruction,
                        Remark = s.Remark,
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
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
                        },
                        Unit = s.Unit,
                        UomUnit = s.UomUnit,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        PackagingQty = s.PackagingQty,
                        PackagingType = s.PackagingType,
                        PackagingUnit = s.PackagingUnit,
                        ProductionOrderNo = s.ProductionOrderNo,
                        QtyOrder = s.ProductionOrderOrderQuantity,
                        DeliveryOrderSalesId = s.DeliveryOrderSalesId,
                        DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                    }).ToList()
                };

                foreach (var item in vm.WarehousesProductionOrders)
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

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            var bonNo = "";
            if (destinationArea == DyeingPrintingArea.SHIPPING)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.PACKING)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.TRANSIT)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

            return bonNo;
        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_IN, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_OUT, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }


        private double GetBalance(OutputWarehouseProductionOrderViewModel productionOrder)
        {

            var balance = productionOrder.Quantity;
            if (!string.IsNullOrWhiteSpace(productionOrder.ProductPackingCode))
            {
                var splitedCode = productionOrder.ProductPackingCode.Split(",");
                var productPackingCodeCount = splitedCode.Count();

                balance = productPackingCodeCount * productionOrder.Quantity;
            }


            return balance;
        }

        private decimal GetPackagingQty(OutputWarehouseProductionOrderViewModel productionOrder)
        {
            var productPackingCodeCount = productionOrder.PackagingQty;
            if (!string.IsNullOrWhiteSpace(productionOrder.ProductPackingCode))
            {
                var splitedCode = productionOrder.ProductPackingCode.Split(",");
                productPackingCodeCount = splitedCode.Count();
            }

            return productPackingCodeCount;
        }

        private async Task<int> CreateOut(OutputWarehouseViewModel viewModel)
        {
            int result = 0;
            var model = _outputRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                        s.DestinationArea == viewModel.DestinationArea &&
                                                                                        s.Date.Date == viewModel.Date.Date &&
                                                                                        s.Shift == viewModel.Shift &&
                                                                                        s.Group == viewModel.Group &&
                                                                                        s.Type == DyeingPrintingArea.OUT);

            viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders.Where(s => s.IsSave).ToList();
            var dateData = viewModel.Date;
            var ids = _inputRepository.GetDbSet().Where(s => s.Area != null && s.Area == DyeingPrintingArea.GUDANGJADI).Select(x => x.Id).ToList();
            var errorResult = new List<ValidationResult>();

            foreach (var item in viewModel.WarehousesProductionOrders)
            {
                if (!string.IsNullOrWhiteSpace(item.ProductPackingCode))
                {
                    var splitedCode = item.ProductPackingCode.Split(",");
                    foreach (var code in splitedCode)
                    {
                        var latestDataOnOut = _outputProductionOrderRepository.GetDbSet()
                                .OrderByDescending(o => o.CreatedUtc)
                                .FirstOrDefault(x =>
                                    x.ProductPackingCode.Contains(code)
                                );

                        if (latestDataOnOut != null)
                        {
                            var latestDataOnIn = _inputProductionOrderRepository.GetDbSet()
                                .OrderByDescending(o => o.CreatedUtc).FirstOrDefault(x =>
                                x.Area == DyeingPrintingArea.GUDANGJADI &&
                                x.ProductPackingCode.Contains(code) &&
                                x.CreatedUtc > latestDataOnOut.CreatedUtc
                            );

                            if (latestDataOnIn == null)
                            {
                                errorResult.Add(new ValidationResult("Kode " + code + " belum masuk", new List<string> { "Kode" }));
                            }
                        }
                    }
                }
            }

            if (errorResult.Count > 0)
            {
                var validationContext = new ValidationContext(viewModel, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (model == null)
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter()
                                                            .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                        s.DestinationArea == viewModel.DestinationArea &&
                                                                        s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);

                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                model = new DyeingPrintingAreaOutputModel(viewModel.Date,
                                                          viewModel.Area,
                                                          viewModel.Shift,
                                                          bonNo,
                                                          false,
                                                          viewModel.DestinationArea,
                                                          viewModel.Group,
                                                           DyeingPrintingArea.OUT,
                                                          viewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id,
                                                                s.ProductionOrder.No,
                                                                s.CartNo,
                                                                s.Buyer,
                                                                s.Construction,
                                                                s.Unit,
                                                                s.Color,
                                                                s.Motif,
                                                                s.UomUnit,
                                                                s.Remark,
                                                                s.Grade,
                                                                s.Status,
                                                                s.ProductPackingCode.Split(",").Count() * s.Quantity,
                                                                s.PackingInstruction,
                                                                s.ProductionOrder.Type,
                                                                s.ProductionOrder.OrderQuantity,
                                                                s.PackagingType,
                                                                s.ProductPackingCode.Split(",").Count(),
                                                                s.PackagingUnit,
                                                                s.DeliveryOrderSalesId,
                                                                s.DeliveryOrderSalesNo,
                                                                false,
                                                                viewModel.Area,
                                                                viewModel.DestinationArea,
                                                                s.Id,
                                                                s.BuyerId,
                                                                s.MaterialProduct.Id,
                                                                s.MaterialProduct.Name,
                                                                s.MaterialConstruction.Id,
                                                                s.MaterialConstruction.Name,
                                                                s.MaterialWidth,
                                                                s.AdjDocumentNo,
                                                                s.ProcessType.Id,
                                                                s.ProcessType.Name,
                                                                s.YarnMaterial.Id,
                                                                s.YarnMaterial.Name,
                                                                s.ProductSKUId,
                                                                s.FabricSKUId,
                                                                s.ProductSKUCode,
                                                                s.HasPrintingProductSKU,
                                                                s.ProductPackingId,
                                                                s.FabricPackingId,
                                                                s.ProductPackingCode,
                                                                s.HasPrintingProductPacking,
                                                                s.Quantity,
                                                                s.FinishWidth,
                                                                s.DateIn,
                                                                viewModel.Date, s.DestinationBuyerName,
                                                                s.InventoryType,
                                                                s.MaterialOrigin)).ToList());



                result = await _outputRepository.InsertAsync(model);
                //viewModel.WarehousesProductionOrders = viewModel.WarehousesProductionOrders
                foreach (var item in viewModel.WarehousesProductionOrders)
                {


                    if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {
                        //var newBalance = item.Quantity;
                        var newBalance = GetBalance(item);
                        var packagingQty = GetPackagingQty(item);
                        //result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, newBalance, item.PackagingQty);
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, newBalance, packagingQty);
                    }
                    else
                    {
                        //var newBalance = item.Quantity * (double)1;
                        var newBalance = GetBalance(item);
                        var packagingQty = GetPackagingQty(item);
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, newBalance);
                        result += await _inputProductionOrderRepository.UpdatePackingQtyFromOutputAsync(item.Id, packagingQty);

                    }
                    //var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                    //    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrder.Type, item.Grade, null,
                    //    item.PackagingType, item.PackagingQty, item.PackagingUnit, item.Quantity, item.InventoryType);

                    // update productPackingCodeRemains
                    if (item.ProductPackingCode != null && item.ProductPackingCode != "")
                    {
                        await _inputProductionOrderRepository.UpdateProductPackingCodeRemains(item.Id, item.ProductPackingCode);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.ProductPackingCode.Split(",").Count() * item.Quantity, item.Id, item.ProductionOrder.Type, item.Grade, null,
                        item.PackagingType, item.ProductPackingCode.Split(",").Count(), item.PackagingUnit, item.Quantity, item.InventoryType);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            else
            {
                foreach (var item in viewModel.WarehousesProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(item.ProductionOrder.Id,
                        item.ProductionOrder.No,
                        item.CartNo,
                        item.Buyer,
                        item.Construction,
                        item.Unit,
                        item.Color,
                        item.Motif,
                        item.UomUnit,
                        item.Remark,
                        item.Grade,
                        item.Status,
                        item.ProductPackingCode.Split(",").Count() * item.Quantity,
                        item.PackingInstruction,
                        item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity,
                        item.PackagingType,
                        item.ProductPackingCode.Split(",").Count(),
                        item.PackagingUnit,
                        item.DeliveryOrderSalesId,
                        item.DeliveryOrderSalesNo,
                        false,
                        viewModel.Area,
                        viewModel.DestinationArea,
                        item.Id,
                        item.BuyerId,
                        item.MaterialProduct.Id,
                        item.MaterialProduct.Name,
                        item.MaterialConstruction.Id,
                        item.MaterialConstruction.Name,
                        item.MaterialWidth,
                        item.AdjDocumentNo,
                        item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.Quantity, item.FinishWidth, item.DateIn, viewModel.Date,
                        item.DestinationBuyerName, item.InventoryType, item.MaterialOrigin);

                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {
                        var newBalance = GetBalance(item);
                        var packagingQty = GetPackagingQty(item);
                        //result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, newBalance, item.PackagingQty);
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, newBalance, packagingQty);
                    }
                    else
                    {
                        //result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);
                        var newBalance = GetBalance(item);
                        var packagingQty = GetPackagingQty(item);
                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, newBalance);
                        result += await _inputProductionOrderRepository.UpdatePackingQtyFromOutputAsync(item.Id, packagingQty);
                    }

                    // update productPackingCodeRemains
                    if (item.ProductPackingCode != null && item.ProductPackingCode != "")
                    {
                        await _inputProductionOrderRepository.UpdateProductPackingCodeRemains(item.Id, item.ProductPackingCode);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, GetBalance(item), item.Id, item.ProductionOrder.Type, item.Grade, null,
                        item.PackagingType, GetPackagingQty(item), item.PackagingUnit, item.Quantity, item.InventoryType);

                    result += await _movementRepository.InsertAsync(movementModel);

                    var areaReference = new DyeingPrintingAreaReferenceModel("OUT", item.Id, item.DyeingPrintingAreaInputProductionOrderId);
                    await _areaReferenceRepository.InsertAsync(areaReference);

                }

            }

            return result;
        }

        private async Task<int> CreateAdj(OutputWarehouseViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.WarehousesProductionOrders.All(d => d.Balance > 0))
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGJADI && s.Type == DyeingPrintingArea.ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.WarehousesProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGJADI && s.Type == DyeingPrintingArea.ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.WarehousesProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _outputRepository.GetDbSet().AsNoTracking()
                    .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type);

            if (model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group, type,
                        viewModel.WarehousesProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id, s.ProductionOrder.No,
                            s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type,
                            s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, true, viewModel.Area,
                            "", s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name,
                            s.MaterialWidth, s.AdjDocumentNo, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                            s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.Quantity, s.FinishWidth, s.DateIn, viewModel.Date, s.DestinationBuyerName, s.InventoryType, s.MaterialOrigin)).ToList());


                result = await _outputRepository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Grade, null,
                            item.PackagingType, item.PackagingQty, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.WarehousesProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.Balance, item.PackingInstruction, item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity, item.PackagingType, item.PackagingQty, item.PackagingUnit, item.DeliveryOrderSalesId, item.DeliveryOrderSalesNo, true, viewModel.Area,
                        "", item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId, item.MaterialProduct.Id, item.MaterialProduct.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name,
                        item.MaterialWidth, item.AdjDocumentNo, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode,
                        item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.Quantity, item.FinishWidth, item.DateIn, viewModel.Date, item.DestinationBuyerName, item.InventoryType, item.MaterialOrigin);

                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, item.Grade, null,
                        item.PackagingType, item.PackagingQty, item.PackagingUnit, item.Quantity);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }



            return result;
        }

        public async Task<int> Create(OutputWarehouseViewModel viewModel)
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

        public async Task<OutputWarehouseViewModel> ReadById(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputWarehouseViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGJADI &&
            //   (((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var query = _outputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);


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
                Id = s.Id,
                Area = s.Area,
                Type = s.Type == null || s.Type == DyeingPrintingArea.OUT ? DyeingPrintingArea.OUT : DyeingPrintingArea.ADJ,
                BonNo = s.BonNo,
                Date = s.Date,
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument,
                Shift = s.Shift,
                Group = s.Group
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<IndexViewModel> Read(string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            var data = query.Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                //DestinationArea = s.DestinationArea,
                //HasNextAreaDocument = s.HasOutputDocument,
                Shift = s.Shift,
                Group = s.Group
            });

            return new ListResult<IndexViewModel>(data.ToList(), 0, data.Count(), query.Count());
        }

        private MemoryStream GenerateExcelOut(DyeingPrintingAreaOutputModel model, int offSet)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders;

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. DO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "TANGGAL MASUK", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "TANGGAL KELUAR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY ORDER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "ASAL MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "UNIT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "WARNA", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MOTIF", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "PACKAGING", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PACKAGING", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JENIS", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "GRADE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SALDO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KETERANGAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENYERAHKAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENERIMA", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    var dataIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                    var dataOut = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                    dt.Rows.Add(indexNumber,
                                item.DeliveryOrderSalesNo,
                                item.ProductionOrderNo,
                                dataIn,
                                dataOut,
                                item.ProductionOrderOrderQuantity,
                                item.Construction,
                                item.MaterialOrigin,
                                item.Unit,
                                item.Buyer,
                                item.Color,
                                item.Motif,
                                item.PackagingType,
                                item.PackagingQty,
                                item.ProductionOrderType,
                                item.Grade,
                                item.UomUnit,
                                item.Balance,
                                "",
                                "",
                                "");
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");

            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "SHIFT";
            sheet.Cells[2, 2].Value = model.Shift;

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = model.Shift;

            sheet.Cells[4, 1].Value = "KELUAR KE";
            sheet.Cells[4, 2].Value = model.DestinationArea;

            sheet.Cells[5, 1].Value = "NO. BON";
            sheet.Cells[5, 2].Value = model.BonNo;
            sheet.Cells[5, 2, 5, 3].Merge = true;

            sheet.Cells[6, 1].Value = "NO.";
            sheet.Cells[6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 1, 7, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 1, 7, 1].Merge = true;

            sheet.Cells[6, 2].Value = "NO. DO";
            sheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 2, 7, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 2, 7, 2].Merge = true;

            sheet.Cells[6, 3].Value = "NO. SPP";
            sheet.Cells[6, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 3, 7, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 3, 7, 3].Merge = true;

            sheet.Cells[6, 4].Value = "QTY ORDER";
            sheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 4, 7, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 4, 7, 4].Merge = true;

            sheet.Cells[6, 5].Value = "MATERIAL";
            sheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 5, 7, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 5, 7, 5].Merge = true;

            sheet.Cells[6, 6].Value = "UNIT";
            sheet.Cells[6, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 6, 7, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 6, 7, 6].Merge = true;

            sheet.Cells[6, 7].Value = "BUYER";
            sheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 7, 7, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 7, 7, 7].Merge = true;

            sheet.Cells[6, 8].Value = "WARNA";
            sheet.Cells[6, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 8, 7, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 8, 7, 8].Merge = true;

            sheet.Cells[6, 9].Value = "MOTIF";
            sheet.Cells[6, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 9, 7, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 9, 7, 9].Merge = true;

            sheet.Cells[6, 10].Value = "PACKAGING";
            sheet.Cells[6, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 10, 7, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 10, 7, 10].Merge = true;

            sheet.Cells[6, 11].Value = "QTY PACKAGING";
            sheet.Cells[6, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 11, 7, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 11, 7, 11].Merge = true;

            sheet.Cells[6, 12].Value = "JENIS";
            sheet.Cells[6, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 12, 7, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 12, 7, 12].Merge = true;

            sheet.Cells[6, 13].Value = "GRADE";
            sheet.Cells[6, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 13, 7, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 13, 7, 13].Merge = true;

            sheet.Cells[6, 14].Value = "SATUAN";
            sheet.Cells[6, 14].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 14].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 14, 7, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 14, 7, 14].Merge = true;

            sheet.Cells[6, 15].Value = "SALDO";
            sheet.Cells[6, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 15].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 15, 7, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 15, 7, 15].Merge = true;

            sheet.Cells[6, 16].Value = "KETERANGAN";
            sheet.Cells[6, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 16].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 16, 7, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 16, 7, 16].Merge = true;

            sheet.Cells[6, 17].Value = "PARAF";
            sheet.Cells[6, 17].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 17].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 17].AutoFitColumns();
            sheet.Cells[6, 17, 6, 18].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 18].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17, 6, 18].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17, 6, 18].Merge = true;

            sheet.Cells[7, 17].Value = "MENERIMA";
            sheet.Cells[7, 17].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 17].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 17].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 17].AutoFitColumns();

            sheet.Cells[7, 18].Value = "MENYERAHKAN";
            sheet.Cells[7, 18].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 18].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 18].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 18].AutoFitColumns();
            #endregion

            int tableRowStart = 8;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        private MemoryStream GenerateExcelAdj(DyeingPrintingAreaOutputModel model, int timeOffset)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal. Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity Total", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {

                    var dateOut = item.DateOut.Equals(DateTimeOffset.MinValue) ? "" : item.DateOut.ToOffset(new TimeSpan(timeOffset, 0, 0)).Date.ToString("d");

                    dt.Rows.Add(item.ProductionOrderNo, dateOut, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.ProductionOrderType,
                        item.Construction, item.MaterialOrigin, item.Unit, item.Buyer, item.Color, item.Motif, item.Grade, item.PackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                        item.PackagingUnit, item.UomUnit, item.PackagingLength.ToString("N2", CultureInfo.InvariantCulture), item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Gudang Jadi") }, true);
        }



        public List<InputWarehouseProductionOrderCreateViewModel> GetInputWarehouseProductionOrders()
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
               .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument);
            var data = productionOrders.Select(s => new InputWarehouseProductionOrderCreateViewModel()
            {
                Id = s.Id,
                PreviousBalance = s.BalanceRemains,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType,
                    OrderQuantity = s.ProductionOrderOrderQuantity
                },
                MaterialProduct = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = s.MaterialConstructionId,
                    Name = s.MaterialConstructionName
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
                MaterialWidth = s.MaterialWidth,
                MaterialOrigin = s.MaterialOrigin,
                FinishWidth = s.FinishWidth,
                ProductionOrderNo = s.ProductionOrderNo,
                CartNo = s.CartNo,
                PackingInstruction = s.PackingInstruction,
                Construction = s.Construction,
                Unit = s.Unit,
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Color = s.Color,
                Motif = s.Motif,
                UomUnit = s.UomUnit,
                Balance = s.Balance,
                HasOutputDocument = s.HasOutputDocument,
                IsChecked = s.IsChecked,
                Grade = s.Grade,
                Remark = s.Remark,
                Status = s.Status,
                //MtrLength = ,
                //YdsLength = ,
                //Quantity = ,
                PackagingType = s.PackagingType,
                PackagingUnit = s.PackagingUnit,
                PackagingQty = s.PackagingQty,
                Qty = s.PackagingLength,
                Quantity = s.PackagingLength,
                QtyOrder = s.ProductionOrderOrderQuantity,
                InputId = s.DyeingPrintingAreaInputId,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                HasPrintingProductSKU = s.HasPrintingProductSKU,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                HasPrintingProductPacking = s.HasPrintingProductPacking,
                //DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                DateIn = s.DateIn,

            });

            return data.ToList();
        }

        public List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList()
        {
            var query = _inputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                    !s.HasOutputDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {
                    PreviousBalance = p.BalanceRemains,
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.MaterialId,
                        Name = p.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.MaterialConstructionId,
                        Name = p.MaterialConstructionName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    MaterialWidth = p.MaterialWidth,
                    FinishWidth = p.FinishWidth,
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    InputId = p.DyeingPrintingAreaInputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    //DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    //DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasOutputDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingLength,
                    Quantity = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    HasPrintingProductPacking = p.HasPrintingProductPacking,
                    DyeingPrintingAreaInputProductionOrderId = p.Id
                }).ToList()

            });

            return data.ToList();
        }
        public List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList(int bonId)
        {
            var query = _inputProductionOrderRepository.ReadAll()
                                                        .Join(_inputRepository.ReadAll().Where(x => x.Id == bonId),
                                                        spp => spp.DyeingPrintingAreaInputId,
                                                        bon => bon.Id,
                                                        (spp, bon) => spp)
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                    !s.HasOutputDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {
                    MaterialWidth = p.MaterialWidth,
                    FinishWidth = p.FinishWidth,
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.MaterialConstructionId,
                        Name = p.MaterialConstructionName
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.MaterialId,
                        Name = p.MaterialName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    PreviousBalance = p.BalanceRemains,
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    InputId = p.DyeingPrintingAreaInputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    //DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    //DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasOutputDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingLength,
                    Quantity = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    HasPrintingProductPacking = p.HasPrintingProductPacking
                }).ToList()

            });

            return data.ToList();
        }

        public async Task<List<InputSppWarehouseViewModel>> GetOutputSppWarehouseItemListAsync(int bonId)
        {
            //var query = _outputProductionOrderRepository.ReadAll()
            //                                            .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
            //                                            spp => spp.DyeingPrintingAreaOutputId,
            //                                            bon => bon.Id,
            //                                            (spp, bon) => spp)
            //                                            .OrderByDescending(s => s.LastModifiedUtc)
            //                                            .Where(s => s.Area == GUDANGJADI &&
            //                                                        !s.HasNextAreaDocument);
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
                                                        spp => spp.DyeingPrintingAreaOutputId,
                                                        bon => bon.Id,
                                                        (spp, bon) => spp)
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.MaterialConstructionId,
                        Name = p.MaterialConstructionName
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.MaterialId,
                        Name = p.MaterialName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    MaterialWidth = p.MaterialWidth,
                    FinishWidth = p.FinishWidth,
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    //InputId = p.DyeingPrintingAreaOutputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasNextAreaDocument,
                    HasNextAreaDocument = p.HasNextAreaDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingLength,
                    Quantity = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    HasPrintingProductPacking = p.HasPrintingProductPacking,
                    DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId
                }).ToList()

            }).ToList();
            foreach (var item in data)
            {
                foreach (var detail in item.ProductionOrderItems)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(detail.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        detail.BalanceRemains = inputData.BalanceRemains + detail.Balance;
                        detail.PreviousBalance = detail.BalanceRemains;
                    }
                }
            }
            return data;
        }

        public async Task<List<InputSppWarehouseViewModel>> GetOutputSppWarehouseItemListAsyncBon(int bonId)
        {
            //var query = _outputProductionOrderRepository.ReadAll()
            //                                            .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
            //                                            spp => spp.DyeingPrintingAreaOutputId,
            //                                            bon => bon.Id,
            //                                            (spp, bon) => spp)
            //                                            .OrderByDescending(s => s.LastModifiedUtc)
            //                                            .Where(s => s.Area == GUDANGJADI &&
            //                                                        !s.HasNextAreaDocument);
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
                                                        spp => spp.DyeingPrintingAreaOutputId,
                                                        bon => bon.Id,
                                                        (spp, bon) => spp)
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.GroupBy( r => new { r.ProductionOrderId, r.Grade, r.NextAreaInputStatus}).Select(p => new InputSppWarehouseItemListViewModel()
                {
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.First().MaterialConstructionId,
                        Name = p.First().MaterialConstructionName
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.First().MaterialId,
                        Name = p.First().MaterialName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.First().YarnMaterialId,
                        Name = p.First().YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.First().ProcessTypeId,
                        Name = p.First().ProcessTypeName
                    },
                    MaterialWidth = p.First().MaterialWidth,
                    FinishWidth = p.First().FinishWidth,
                    Id = p.First().Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.First().CartNo,
                    Buyer = p.First().Buyer,
                    BuyerId = p.First().BuyerId,
                    Construction = p.First().Construction,
                    Unit = p.First().Unit,
                    Color = p.First().Color,
                    Motif = p.First().Motif,
                    UomUnit = p.First().UomUnit,
                    Remark = p.First().Remark,
                    //InputId = p.DyeingPrintingAreaOutputId,
                    Grade = p.Key.Grade,
                    Status = p.First().Status,
                    Balance = p.Sum( x => x.Balance),
                    PackingInstruction = p.First().PackingInstruction,
                    PackagingType = p.First().PackagingType,
                    PackagingQty = p.Sum( x => x.PackagingQty),
                    PackagingUnit = p.First().PackagingUnit,
                    AvalALength = p.First().AvalALength,
                    AvalBLength = p.First().AvalBLength,
                    AvalConnectionLength = p.First().AvalConnectionLength,
                    DeliveryOrderSalesId = p.First().DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.First().DeliveryOrderSalesNo,
                    AvalType = p.First().AvalType,
                    AvalCartNo = p.First().AvalCartNo,
                    AvalQuantityKg = p.First().AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.First().Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.First().HasNextAreaDocument,
                    HasNextAreaDocument = p.First().HasNextAreaDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.First().PackagingLength,
                    Quantity = p.First().PackagingLength,
                    ProductSKUId = p.First().ProductSKUId,
                    FabricSKUId = p.First().FabricSKUId,
                    ProductSKUCode = p.First().ProductSKUCode,
                    HasPrintingProductSKU = p.First().HasPrintingProductSKU,
                    ProductPackingId = p.First().ProductPackingId,
                    FabricPackingId = p.First().FabricPackingId,
                    ProductPackingCode = p.First().ProductPackingCode,
                    HasPrintingProductPacking = p.First().HasPrintingProductPacking,
                    DyeingPrintingAreaInputProductionOrderId = p.First().DyeingPrintingAreaInputProductionOrderId
                }).ToList()

            }).ToList();
            foreach (var item in data)
            {
                foreach (var detail in item.ProductionOrderItems)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(detail.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        detail.BalanceRemains = inputData.BalanceRemains + detail.Balance;
                        detail.PreviousBalance = detail.BalanceRemains;
                    }
                }
            }
            return data;
        }

        private async Task<int> DeleteOut(DyeingPrintingAreaOutputModel model)
        {
            //var result = 0;
            //var bonOutput = _outputRepository.ReadAll().FirstOrDefault(x => x.Id == bonId && x.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument));
            //if (bonOutput != null)
            //{
            //    var listBonInputByBonOutput = _inputProductionOrderRepository.ReadAll().Join(bonOutput.DyeingPrintingAreaOutputProductionOrders,
            //                                                                  sppInput => sppInput.Id,
            //                                                                  sppOutput => sppOutput.DyeingPrintingAreaInputProductionOrderId,
            //                                                                  (sppInput, sppOutput) => new { Input = sppInput, Output = sppOutput });
            //    foreach (var spp in listBonInputByBonOutput)
            //    {
            //        spp.Input.SetHasOutputDocument(false, "OUTWAREHOUSESERVICE", "SERVICE");
            //        //update balance remains
            //        var newBalance = spp.Input.BalanceRemains + spp.Output.Balance;
            //        spp.Input.SetBalanceRemains(newBalance, "OUTWAREHOUSESERVICE", "SERVICE");
            //        result += await _inputProductionOrderRepository.UpdateAsync(spp.Input.Id, spp.Input);
            //    }
            //}
            //result += await _outputRepository.DeleteAsync(bonId);
            //return result;

            int result = 0;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, item.MaterialOrigin, model.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null,
                        item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _outputRepository.DeleteWarehouseArea(model);

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
                   item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null,
                        item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _outputRepository.DeleteAdjustment(model);

            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var model = await _outputRepository.ReadByIdAsync(bonId);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return await DeleteOut(model);
            }
            else
            {
                return await DeleteAdj(model);
            }
        }

        public MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, string type, int offSet)
        {
            //var model = await _repository.ReadByIdAsync(id);
            //var warehouseData = _outputRepository.ReadAll().Where(s => s.Area == GUDANGJADI &&
            //   (((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var warehouseData = _outputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                warehouseData = warehouseData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            warehouseData = warehouseData.OrderBy(s => s.BonNo);



            var modelAll = warehouseData.Select(s =>
                 new
                 {
                     SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
                     {
                         BonNo = s.BonNo,
                         DoNo = s.DeliveryOrderSalesNo,
                         ProductionOrderId = d.ProductionOrderId,
                         NoSPP = d.ProductionOrderNo,
                         DateIn = d.DateIn,
                         DateOut = d.DateOut,
                         QtyOrder = d.ProductionOrderOrderQuantity,
                         Material = d.Construction,
                         MaterialOrigin = d.MaterialOrigin,
                         Unit = d.Unit,
                         Buyer = d.Buyer,
                         Warna = d.Color,
                         Motif = d.Motif,
                         Jenis = d.PackagingType,
                         Grade = d.Grade,
                         Ket = d.Description,
                         QtyPack = d.PackagingQty,
                         Pack = d.PackagingUnit,
                         Qty = d.Balance,
                         d.NextAreaInputStatus,
                         SAT = d.UomUnit
                     })
                 });

            if (type == "BON")
            {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.GroupBy(r => new { r.ProductionOrderId, r.Grade, r.NextAreaInputStatus }).Select(d => new
                    {
                        BonNo = d.First().BonNo,
                        DoNo = d.First().DoNo,
                        ProductionOrderId = d.Key.ProductionOrderId,
                        NoSPP = d.First().NoSPP,
                        DateIn = d.First().DateIn,
                        DateOut = d.First().DateOut,
                        QtyOrder = d.First().QtyOrder,
                        Material = d.First().Material,
                        MaterialOrigin = d.First().MaterialOrigin,
                        Unit = d.First().Unit,
                        Buyer = d.First().Buyer,
                        Warna = d.First().Warna,
                        Motif = d.First().Motif,
                        Jenis = d.First().Jenis,
                        Grade = d.First().Grade,
                        Ket = d.First().Ket,
                        QtyPack = d.Sum(x => x.QtyPack),
                        Pack = d.First().Pack,
                        Qty = d.Sum(x => x.Qty),
                        d.Key.NextAreaInputStatus,
                        SAT = d.First().SAT


                    })

                });
            }
            else {
                modelAll = modelAll.Select(s => new
                {
                    SppList = s.SppList.Select(d => new
                    {
                        BonNo = d.BonNo,
                        DoNo = d.DoNo,
                        ProductionOrderId = d.ProductionOrderId,
                        NoSPP = d.NoSPP,
                        DateIn = d.DateIn,
                        DateOut = d.DateOut,
                        QtyOrder = d.QtyOrder,
                        Material = d.Material,
                        MaterialOrigin = d.MaterialOrigin,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Warna,
                        Motif = d.Motif,
                        Jenis = d.Jenis,
                        Grade = d.Grade,
                        Ket = d.Ket,
                        QtyPack = d.QtyPack,
                        Pack = d.Pack,
                        Qty = d.Qty,
                        d.NextAreaInputStatus,
                        SAT = d.SAT

                    })

                });
            }

            //var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            var query = modelAll.SelectMany(s => s.SppList);

            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"DoNo","NO DO" },
                {"NoSPP","NO SP" },
                {"DateIn","TANGGAL MASUK" },
                {"DateOut","TANGGAL KELUAR" },
                {"QtyOrder","QTY ORDER" },
                {"Material","MATERIAL"},
                {"MaterialOrigin","ASAL MATERIAL"},
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"Jenis","JENIS"},
                {"Grade","GRADE"},
                {"Ket","KET" },
                {"QtyPack","QTY Pack"},
                {"Pack","PACK"},
                {"Qty","QTY" },
                {"SAT","SAT" },
                {"NextAreaInputStatus","Status" }
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
                        if (searchProperty.Name.Equals("DateIn") || searchProperty.Name.Equals("DateOut"))
                        {
                            var date = DateTimeOffset.Parse(searchValue.ToString());
                            valueClass = date.Equals(DateTimeOffset.MinValue) ? "" : date.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                        }
                        else
                        {
                            valueClass = searchValue == null ? "" : searchValue.ToString();
                        }

                    }
                    //else
                    //{
                    //    valueClass = "";
                    //}
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("PENCATATAN KELUAR GUDANG JADI");

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

        public ListResult<AdjWarehouseProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputProductionOrderRepository.ReadAll()
            //    .Where(s => s.Area == GUDANGJADI && !s.HasOutputDocument)
            //    .Select(d => new PlainAdjWarehouseProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Grade = d.Grade,
            //        Packing = d.PackagingUnit,
            //        PackagingQty = d.PackagingQty,
            //        Balance = d.Balance,
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
            //        PackagingType = d.PackagingType,
            //        ProductSKUId = d.ProductSKUId,
            //        FabricSKUId = d.FabricSKUId,
            //        ProductSKUCode = d.ProductSKUCode,
            //        HasPrintingProductSKU = d.HasPrintingProductSKU,
            //        ProductPackingId = d.ProductPackingId,
            //        FabricPackingId = d.FabricPackingId,
            //        ProductPackingCode = d.ProductPackingCode,
            //        HasPrintingProductPacking = d.HasPrintingProductPacking
            //    })
            //    .Union(_outputProductionOrderRepository.ReadAll()
            //    .Where(s => s.Area == GUDANGJADI && !s.HasNextAreaDocument)
            //    .Select(d => new PlainAdjWarehouseProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Grade = d.Grade,
            //        PackagingQty = d.PackagingQty,
            //        Balance = d.Balance,
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
            //        PackagingType = d.PackagingType,
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
                .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument)
                .Select(d => new PlainAdjWarehouseProductionOrder()
                {
                    Id = d.Id,
                    BalanceRemains = d.BalanceRemains,
                    Area = d.Area,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    Grade = d.Grade,
                    Packing = d.PackagingUnit,
                    PackagingQty = d.PackagingQty,
                    Balance = d.Balance,
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
                    Unit = d.Unit,
                    UomUnit = d.UomUnit,
                    PackagingLength = d.PackagingLength,
                    PackagingType = d.PackagingType,
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

            query = QueryHelper<PlainAdjWarehouseProductionOrder>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjWarehouseProductionOrder>.Filter(query, FilterDictionary);

            var data = query.ToList()
                //.GroupBy(d => d.ProductionOrderId)
                //.Select(s => s.First())
                .OrderBy(s => s.ProductionOrderNo)
                .Skip((page - 1) * size).Take(size)
                .Select(s => new AdjWarehouseProductionOrderViewModel()
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
                    PackagingUnit = s.Packing,
                    Color = s.Color,
                    Construction = s.Construction,
                    MaterialProduct = new Material()
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
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    Motif = s.Motif,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    Quantity = s.PackagingLength,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    HasPrintingProductPacking = s.HasPrintingProductPacking
                });

            return new ListResult<AdjWarehouseProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Update(int id, OutputWarehouseViewModel viewModel)
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

        private async Task<int> UpdateOut(int id, OutputWarehouseViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _outputRepository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea,
                    viewModel.Group, viewModel.Type, viewModel.WarehousesProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id, s.ProductionOrder.No,
                        s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type,
                        s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, s.HasNextAreaDocument, viewModel.Area, viewModel.DestinationArea,
                        s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.AdjDocumentNo, s.ProcessType.Id,
                        s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.Quantity, s.FinishWidth, s.DateIn, viewModel.Date, s.DestinationBuyerName, s.InventoryType, s.MaterialOrigin)

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

            //if (deletedData.Any(item => item.HasNextAreaDocument))
            //{
            //    throw new Exception(string.Format("Ada SPP yang Sudah Dibuat di Penerimaan Area {0}!", dbModel.DestinationArea));
            //}
            result = await _outputRepository.UpdateWarehouseArea(id, model, dbModel);
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

                if (newQtyPacking != 0 && newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.Grade, null,
                        item.PackagingType, newQtyPacking, item.PackagingUnit, item.PackagingLength);
                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                         item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null,
                         item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputWarehouseViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _outputRepository.ReadByIdAsync(id);
            if (viewModel.WarehousesProductionOrders.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "",
                    viewModel.Group, type, viewModel.WarehousesProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id, s.ProductionOrder.No,
                        s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type,
                        s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQty, s.PackagingUnit, s.DeliveryOrderSalesId, s.DeliveryOrderSalesNo, true, viewModel.Area, "",
                        s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.AdjDocumentNo, s.ProcessType.Id,
                        s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId,
                        s.ProductPackingCode, s.HasPrintingProductPacking, s.Quantity, s.FinishWidth, s.DateIn, viewModel.Date, s.DestinationBuyerName, s.InventoryType, s.MaterialOrigin)

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

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _outputRepository.UpdateAdjustmentData(id, model, dbModel);
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
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                         item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, item.Grade, null,
                         item.PackagingType, newQtyPacking, item.PackagingUnit, item.PackagingLength);
                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                         item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade, null,
                         item.PackagingType, item.PackagingQty * -1, item.PackagingUnit, item.PackagingLength);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public List<InputSppWarehouseViewModel> GetInputSppWarehouseItemListV2(long productionOrderId)
        {
            IQueryable<DyeingPrintingAreaInputProductionOrderModel> query;

            if (productionOrderId == 0)
            {

                query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument && s.PackagingQty != 0).Take(100);
            }
            else
            {
                query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument && s.ProductionOrderId == productionOrderId && s.PackagingQty != 0).Take(100);

            }

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {
                    BalanceRemains = p.BalanceRemains,
                    ProductPackingCodeRemains = p.ProductPackingCodeRemains,
                    PreviousBalance = p.BalanceRemains,
                    PreviousQtyPacking = p.PackagingQty,
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    MaterialProduct = new Material()
                    {
                        Id = p.MaterialId,
                        Name = p.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = p.MaterialConstructionId,
                        Name = p.MaterialConstructionName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    MaterialWidth = p.MaterialWidth,
                    MaterialOrigin = p.MaterialOrigin,
                    FinishWidth = p.FinishWidth,
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    InputId = p.DyeingPrintingAreaInputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    Area = p.Area,
                    HasOutputDocument = p.HasOutputDocument,
                    Qty = p.PackagingLength,
                    Quantity = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    HasPrintingProductPacking = p.HasPrintingProductPacking,
                    DyeingPrintingAreaInputProductionOrderId = p.Id,
                    DateIn = p.DateIn,
                    InventoryType = p.InventoryType

                }).ToList()

            });

            return data.ToList();
        }

        public ListResult<InputWarehouseProductionOrderCreateViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument);
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
                .Select(s => new InputWarehouseProductionOrderCreateViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    }
                });

            return new ListResult<InputWarehouseProductionOrderCreateViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<MemoryStream> GenerateExcel(int id, int offSet)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return GenerateExcelOut(model, offSet);
            }
            else
            {
                return GenerateExcelAdj(model, offSet);
            }
        }

        public Task<MemoryStream> GenerateExcel(int id)
        {
            throw new NotImplementedException();
        }

        public InputSppWarehouseItemListViewModel GetInputSppWarehouseItemListV2(string productPackingCode)
        {
            IQueryable<DyeingPrintingAreaInputProductionOrderModel> query;
            query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && !s.HasOutputDocument && s.ProductPackingCode.Contains(productPackingCode));


            var data = query.Select(p => new InputSppWarehouseItemListViewModel()
            {
                PreviousBalance = p.BalanceRemains,
                Id = p.Id,
                ProductionOrder = new ProductionOrder()
                {
                    Id = p.ProductionOrderId,
                    No = p.ProductionOrderNo,
                    Type = p.ProductionOrderType,
                    OrderQuantity = p.ProductionOrderOrderQuantity
                },
                MaterialProduct = new Material()
                {
                    Id = p.MaterialId,
                    Name = p.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Id = p.MaterialConstructionId,
                    Name = p.MaterialConstructionName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = p.YarnMaterialId,
                    Name = p.YarnMaterialName
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = p.ProcessTypeId,
                    Name = p.ProcessTypeName
                },
                MaterialWidth = p.MaterialWidth,
                FinishWidth = p.FinishWidth,
                CartNo = p.CartNo,
                Buyer = p.Buyer,
                BuyerId = p.BuyerId,
                Construction = p.Construction,
                Unit = p.Unit,
                Color = p.Color,
                Motif = p.Motif,
                UomUnit = p.UomUnit,
                Remark = p.Remark,
                InputId = p.DyeingPrintingAreaInputId,
                Grade = p.Grade,
                Status = p.Status,
                Balance = p.Balance,
                PackingInstruction = p.PackingInstruction,
                PackagingType = p.PackagingType,
                PackagingQty = p.PackagingQty,
                PackagingUnit = p.PackagingUnit,
                AvalALength = p.AvalALength,
                AvalBLength = p.AvalBLength,
                AvalConnectionLength = p.AvalConnectionLength,
                AvalType = p.AvalType,
                AvalCartNo = p.AvalCartNo,
                AvalQuantityKg = p.AvalQuantityKg,
                Area = p.Area,
                HasOutputDocument = p.HasOutputDocument,
                Qty = p.PackagingLength,
                Quantity = p.PackagingLength,
                ProductSKUId = p.ProductSKUId,
                FabricSKUId = p.FabricSKUId,
                ProductSKUCode = p.ProductSKUCode,
                HasPrintingProductSKU = p.HasPrintingProductSKU,
                ProductPackingId = p.ProductPackingId,
                FabricPackingId = p.FabricPackingId,
                ProductPackingCode = p.ProductPackingCode,
                HasPrintingProductPacking = p.HasPrintingProductPacking,
                DyeingPrintingAreaInputProductionOrderId = p.Id,
                DateIn = p.DateIn,
                PreviousQtyPacking = p.PackagingQty
            });

            return data.FirstOrDefault();
        }
    }
}