using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN
{
    public class DPWarehouseInService : IDPWarehouseInService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        public DPWarehouseInService(IServiceProvider serviceProvider)
        {

            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        public List<OutputPreWarehouseItemListViewModel> PreInputWarehouse(string packingCode)
        {
            //List<OutputPreWarehouseItemListViewModel> queryResult;

            var queryResult = new List<OutputPreWarehouseItemListViewModel>();
            var query = _outputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc).
                                                                   Where(s => s.ProductPackingCode.Contains(packingCode)
                                                                  && s.DestinationArea == DyeingPrintingArea.GUDANGJADI &&
                                                                  s.Balance > 0 && !s.HasNextAreaDocument);

            if (query != null)
            {
                queryResult = query.Select(p => new OutputPreWarehouseItemListViewModel()
                {
                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = p.ProductionOrderId,
                        No = p.ProductionOrderNo,
                        Type = p.ProductionOrderType,
                        OrderQuantity = p.ProductionOrderOrderQuantity
                    },
                    MaterialWidth = p.MaterialWidth,
                    MaterialOrigin = p.MaterialOrigin,
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
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = p.ProcessTypeId,
                        Name = p.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = p.YarnMaterialId,
                        Name = p.YarnMaterialName
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
                    OutputId = p.DyeingPrintingAreaOutputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    InputQuantity = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingLength = p.PackagingLength,
                    InputPackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    Description = p.Description,
                    DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    DestinationArea = p.DestinationArea,
                    HasNextAreaDocument = p.HasNextAreaDocument,
                    DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingLength,
                    ProductSKUId = p.ProductSKUId,
                    FabricSKUId = p.FabricSKUId,
                    ProductSKUCode = p.ProductSKUCode,
                    HasPrintingProductSKU = p.HasPrintingProductSKU,
                    ProductPackingId = p.ProductPackingId,
                    FabricPackingId = p.FabricPackingId,
                    ProductPackingCode = p.ProductPackingCode,
                    HasPrintingProductPacking = p.HasPrintingProductPacking,
                    PreviousOutputPackagingQty = p.PackagingQty,
                    PrevSppInJson = p.PrevSppInJson
                }).ToList();


            }
            //else { 

            //}
            return queryResult;
        }

       
    }

    


}
