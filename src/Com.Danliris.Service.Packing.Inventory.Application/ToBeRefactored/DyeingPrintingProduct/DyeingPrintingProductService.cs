using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct
{
    public class DyeingPrintingProductService : IDyeingPrintingProductService
    {
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _stockOpnameProductionOrderRepository;


        private readonly PackingInventoryDbContext _dbContext;

        public DyeingPrintingProductService(IServiceProvider serviceProvider)
        {
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _stockOpnameProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _dbContext = serviceProvider.GetService<PackingInventoryDbContext>();
        }

        public ListResult<DyeingPrintingProductPackingViewModel> GetDataProductPacking(int page, int size, string filter, string order, string keyword, bool isStockOpname)
        {
            if (isStockOpname)
            {
                var query = _stockOpnameProductionOrderRepository.ReadAll().Where(entity => !string.IsNullOrWhiteSpace(entity.ProductPackingCode));
                List<string> SearchAttributes = new List<string>()
                {
                    "ProductionOrderNo"
                };

                query = QueryHelper<DyeingPrintingStockOpnameProductionOrderModel>.Search(query, SearchAttributes, keyword);

                Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
                query = QueryHelper<DyeingPrintingStockOpnameProductionOrderModel>.Filter(query, FilterDictionary);

                Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
                query = QueryHelper<DyeingPrintingStockOpnameProductionOrderModel>.Order(query, OrderDictionary);

                var data = query.Skip((page - 1) * size).Take(size).Select(s => new DyeingPrintingProductPackingViewModel()
                {
                    Color = s.Color,
                    FabricPackingId = s.FabricPackingId,
                    FabricSKUId = s.FabricSKUId,
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    HasPrintingProductPacking = s.HasPrintingProductPacking,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    Id = s.Id,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    MaterialWidth = s.MaterialWidth,
                    Motif = s.Motif,
                    ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    ProductPackingId = s.ProductPackingId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductSKUId = s.ProductSKUId,
                    UomUnit = s.UomUnit,
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    Quantity = s.PackagingQty,
                    ProductPackingLength = s.PackagingLength,
                    ProductPackingType = s.PackagingUnit,
                    DocumentNo = s.DocumentNo,
                    Grade = s.Grade
                });

                //int result = 0;
                //foreach (var item in data)
                //{
                //    result += await UpdatePrintingStatusProductPacking(item.Id, true);
                //}

                return new ListResult<DyeingPrintingProductPackingViewModel>(data.ToList(), page, size, query.Count());
            }
            else
            {
                var query = _outputProductionOrderRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.PACKING && s.DyeingPrintingAreaOutput.Type == DyeingPrintingArea.OUT);
                List<string> SearchAttributes = new List<string>()
                {
                    "ProductionOrderNo"
                };

                query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Search(query, SearchAttributes, keyword);

                Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
                query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Filter(query, FilterDictionary);

                Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
                query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Order(query, OrderDictionary);

                var data = query.Skip((page - 1) * size).Take(size).Select(s => new DyeingPrintingProductPackingViewModel()
                {
                    Color = s.Color,
                    FabricPackingId = s.FabricPackingId,
                    FabricSKUId = s.FabricSKUId,
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    HasPrintingProductPacking = s.HasPrintingProductPacking,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    Id = s.Id,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    MaterialWidth = s.MaterialWidth,
                    FinishWidth = s.FinishWidth,
                    Motif = s.Motif,
                    ProductPackingCodes = s.ProductPackingCode.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    ProductPackingId = s.ProductPackingId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductSKUId = s.ProductSKUId,
                    UomUnit = s.UomUnit,
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    Quantity = s.PackagingQty,
                    ProductPackingLength = s.PackagingLength,
                    ProductPackingType = s.PackagingUnit,
                    Grade = s.Grade
                });

                //int result = 0;
                //foreach (var item in data)
                //{
                //    result += await UpdatePrintingStatusProductPacking(item.Id, true);
                //}

                return new ListResult<DyeingPrintingProductPackingViewModel>(data.ToList(), page, size, query.Count());
            }
        }

        public Task<int> UpdatePrintingStatusProductPacking(int id, bool hasPrintingProductPacking)
        {
            return _outputProductionOrderRepository.UpdateHasPrintingProductPacking(id, hasPrintingProductPacking);
        }
    }
}
