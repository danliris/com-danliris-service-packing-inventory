using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation
{
    public class InputAvalTransformationService : IInputAvalTransformationService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string TYPE = "TRANSFORM";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";

        public InputAvalTransformationService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}Y", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private InputAvalTransformationViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputAvalTransformationViewModel()
            {
                Active = model.Active,
                TotalWeight = model.TotalAvalWeight,
                TotalQuantity = model.TotalAvalQuantity,
                AvalType = model.AvalType,
                Area = model.Area,
                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                Group = model.Group,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                IsTransformedAval = model.IsTransformedAval,
                Shift = model.Shift,
                AvalTransformationProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(d => new InputAvalTransformationProductionOrderViewModel()
                {
                    Active = d.Active,
                    AvalType = d.AvalType,
                    BonNo = d.InputAvalBonNo,
                    Machine = d.Machine,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    CreatedAgent = d.CreatedAgent,
                    CreatedBy = d.CreatedBy,
                    CreatedUtc = d.CreatedUtc,
                    DeletedAgent = d.DeletedAgent,
                    DeletedBy = d.DeletedBy,
                    DeletedUtc = d.DeletedUtc,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaOutputProductionOrderId,
                    Id = d.Id,
                    IsDeleted = d.IsDeleted,
                    LastModifiedAgent = d.LastModifiedAgent,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedUtc = d.LastModifiedUtc,
                    Motif = d.Motif,
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
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        OrderQuantity = d.ProductionOrderOrderQuantity,
                        Type = d.ProductionOrderType
                    },
                    Quantity = d.Balance,
                    Unit = d.Unit,
                    UomUnit = d.UomUnit,
                    HasOutputDocument = d.HasOutputDocument,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking
                }).ToList()
            };

            return vm;
        }

        public async Task<int> Create(InputAvalTransformationViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == GUDANGAVAL && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.AvalType == viewModel.AvalType && s.IsTransformedAval);
            viewModel.AvalTransformationProductionOrders = viewModel.AvalTransformationProductionOrders.Where(s => s.IsSave).ToList();
            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.IsTransformedAval && s.Area == GUDANGAVAL && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.AvalType, true,
                    viewModel.TotalQuantity, viewModel.TotalWeight,
                    viewModel.AvalTransformationProductionOrders.Select(d => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, d.BonNo, d.ProductionOrder.Id, d.ProductionOrder.No,
                    d.ProductionOrder.Type, d.ProductionOrder.OrderQuantity, d.CartNo, d.Construction, d.Unit, d.Buyer, d.BuyerId, d.Color, d.Motif, d.AvalType, d.UomUnit, d.Quantity,
                     false, d.Id, d.Material.Id, d.Material.Name, d.MaterialConstruction.Id, d.MaterialConstruction.Name, d.MaterialWidth, d.Machine, d.ProcessType.Id, d.ProcessType.Name,
                     d.YarnMaterial.Id, d.YarnMaterial.Name, d.ProductSKUId, d.FabricSKUId, d.ProductSKUCode, d.HasPrintingProductSKU, d.ProductPackingId, d.FabricPackingId, d.ProductPackingCode, d.HasPrintingProductPacking)).ToList());

                result = await _repository.InsertAsync(model);
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, model.TotalAvalQuantity, model.TotalAvalWeight, model.AvalType);

                result += await _movementRepository.InsertAsync(movementModel);
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {
                    var itemVM = viewModel.AvalTransformationProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaOutputProductionOrderId);
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaOutputProductionOrderId, true);

                }
            }
            else
            {
                foreach (var item in viewModel.AvalTransformationProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity, item.CartNo, item.Construction, item.Unit, item.Buyer, item.BuyerId, item.Color, item.Motif, item.AvalType, item.UomUnit,
                        item.Quantity, false, item.Id, item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name,
                        item.MaterialWidth, item.Machine, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking);

                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    result += await _inputProductionOrderRepository.InsertAsync(modelItem);
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, true);

                }
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, viewModel.TotalQuantity, viewModel.TotalWeight, viewModel.AvalType);

                result += await _movementRepository.InsertAsync(movementModel);
                result += await _repository.UpdateHeaderAvalTransform(model, viewModel.TotalQuantity, viewModel.TotalWeight);
            }



            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var model = await _repository.ReadByIdAsync(id);

            if (model.TotalAvalQuantity == 0 && model.TotalAvalWeight == 0)
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Aval!");
            }

            result += await _repository.DeleteAvalTransformationArea(model);
            var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, TYPE, model.Id, model.BonNo, model.TotalAvalQuantity * -1, model.TotalAvalWeight * -1, model.AvalType);

            result += await _movementRepository.InsertAsync(movementModel);
            return result;
        }

        public List<InputAvalTransformationProductionOrderViewModel> GetInputAvalProductionOrders(string avalType)
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                 .Where(s => s.Area == GUDANGAVAL && !s.HasOutputDocument && !s.DyeingPrintingAreaInput.IsTransformedAval && s.AvalType == avalType);
            var data = productionOrders.Select(s => new InputAvalTransformationProductionOrderViewModel()
            {
                Id = s.Id,
                Machine = s.Machine,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
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
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                InputId = s.DyeingPrintingAreaInputId,
                AvalType = s.AvalType,
                BonNo = s.DyeingPrintingAreaInput.BonNo,
                CartNo = s.CartNo,
                Quantity = s.Balance,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                HasPrintingProductSKU = s.HasPrintingProductSKU,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                HasPrintingProductPacking = s.HasPrintingProductPacking
            });

            return data.ToList();
        }

        public ListResult<InputAvalTransformationViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.IsTransformedAval && s.TotalAvalQuantity != 0 && s.TotalAvalWeight != 0);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputAvalTransformationViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Group = s.Group,
                Shift = s.Shift,
                AvalType = s.AvalType,
                TotalQuantity = s.TotalAvalQuantity,
                TotalWeight = s.TotalAvalWeight
            });

            return new ListResult<InputAvalTransformationViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputAvalTransformationViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public async Task<int> Update(int id, InputAvalTransformationViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);

            var model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.Group, viewModel.AvalType, viewModel.IsTransformedAval,
                    viewModel.TotalQuantity, viewModel.TotalWeight,
                    viewModel.AvalTransformationProductionOrders.Select(d => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, d.BonNo, d.ProductionOrder.Id, d.ProductionOrder.No,
                    d.ProductionOrder.Type, d.ProductionOrder.OrderQuantity, d.CartNo, d.Construction, d.Unit, d.Buyer, d.BuyerId, d.Color, d.Motif, d.AvalType, d.UomUnit, d.Quantity,
                    d.HasOutputDocument, d.DyeingPrintingAreaInputProductionOrderId, d.Material.Id, d.Material.Name, d.MaterialConstruction.Id, d.MaterialConstruction.Name, d.MaterialWidth,
                    d.Machine, d.ProcessType.Id, d.ProcessType.Name, d.YarnMaterial.Id, d.YarnMaterial.Name, d.ProductSKUId, d.FabricSKUId, d.ProductSKUCode, d.HasPrintingProductSKU, d.ProductPackingId, d.FabricPackingId, d.ProductPackingCode, d.HasPrintingProductPacking)
                    {
                        Id = d.Id
                    }).ToList());

            result = await _repository.UpdateAvalTransformationArea(id, model, dbModel);
            var diffAvalQuantity = dbModel.TotalAvalQuantity - model.TotalAvalQuantity;
            var diffAvalWeight = dbModel.TotalAvalWeight - model.TotalAvalWeight;
            var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, diffAvalQuantity * -1, diffAvalWeight * -1, model.AvalType);

            result += await _movementRepository.InsertAsync(movementModel);
            return result;
        }
    }
}
