using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct
{
    public class DyeingPrintingProductService : IDyeingPrintingProductService
    {
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string OUT = "OUT";

        public DyeingPrintingProductService(IServiceProvider serviceProvider)
        {
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        public async Task<ListResult<DyeingPrintingProductPackingViewModel>> GetDataProductPacking(int page, int size, string filter, string order, string keyword)
        {
            var query = _outputProductionOrderRepository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaOutput.Type == OUT);
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
                Motif = s.Motif,
                ProductPackingCode = s.ProductPackingCode,
                ProductPackingId = s.ProductPackingId,
                ProductSKUCode = s.ProductSKUCode,
                ProductSKUId = s.ProductSKUId,
                UomUnit = s.UomUnit,
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                }
            });

            int result = 0;
            foreach(var item in data)
            {
                result += await UpdatePrintingStatusProductPacking(item.Id, true);
            }

            return new ListResult<DyeingPrintingProductPackingViewModel>(data.ToList(), page, size, query.Count());
        }

        public Task<int> UpdatePrintingStatusProductPacking(int id, bool hasPrintingProduct)
        {
            return _outputProductionOrderRepository.UpdateHasPrintingProduct(id, hasPrintingProduct);
        }
    }
}
