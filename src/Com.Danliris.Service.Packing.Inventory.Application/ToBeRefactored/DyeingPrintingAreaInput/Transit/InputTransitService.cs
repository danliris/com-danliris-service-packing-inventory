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
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Data;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit
{
    public class InputTransitService : IInputTransitService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSPPRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _SPPRepository;

        public InputTransitService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputSPPRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _SPPRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        }

        private InputTransitViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputTransitViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                Group = model.Group,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                TransitProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputTransitProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
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
                    InputQuantity = s.InputQuantity,
                    HasOutputDocument = s.HasOutputDocument,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Motif = s.Motif,
                    Grade = s.Grade,
                    IsChecked = s.IsChecked,
                    PackingInstruction = s.PackingInstruction,
                    Remark = s.Remark,
                    ProductionMachine=s.ProductionMachine,
                    QtyPacking = s.PackagingQty,
                    Area = s.Area,
                    AvalType = s.AvalType,
                    BalanceRemains = s.BalanceRemains,
                    DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo
                    },
                    PackingType = s.PackagingType,
                    PackingUnit = s.PackagingUnit,
                    InputQtyPacking = s.InputPackagingQty,
                    Status = s.Status,
                    PackingLength = s.PackagingLength,
                    DyeingPrintingAreaOutputProductionOrderId = s.DyeingPrintingAreaOutputProductionOrderId,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
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
                    ProductTextile = new ProductTextile()
                    { 
                        Id = s.ProductTextileId,
                        Code = s.ProductTextileCode,
                        Name = s.ProductTextileName
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
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


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == DyeingPrintingArea.PACKING)
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.INSPECTIONMATERIAL)
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.SHIPPING)
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        public async Task<int> Create(InputTransitViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.TRANSIT && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.TRANSIT && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.TransitProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                         s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, false, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.InputQuantity, s.BuyerId, s.Id, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id,
                         s.MaterialConstruction.Name, s.MaterialWidth, s.InputQtyPacking, s.PackingUnit, s.PackingType, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name,
                         s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode,
                         s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.FinishWidth, viewModel.Date, s.MaterialOrigin, s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());
                     

                result = await _repository.InsertAsync(model);

                //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.TransitProductionOrders.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TERIMA);
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {
                    var itemVM = viewModel.TransitProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaOutputProductionOrderId);
                    //result += await _SPPRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputPackagingQty);
                    if (itemVM.Area == DyeingPrintingArea.PACKING)
                    {
                        //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                        var packingData = JsonConvert.DeserializeObject<List<PackingData>>(itemVM.PrevSppInJson);
                        result += await _SPPRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                    }
                    else
                    {

                        result += await _SPPRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputPackagingQty);
                    }
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, item.Id, item.ProductionOrderType, 
                        item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.TransitProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, false, item.Remark, item.ProductionMachine, item.Grade, item.Status, item.InputQuantity, item.BuyerId, item.Id,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.InputQtyPacking, item.PackingUnit, item.PackingType,
                        item.DeliveryOrder.Id, item.DeliveryOrder.No, item.AvalType, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode,
                        item.HasPrintingProductPacking, item.PackingLength, item.InputQuantity, item.InputQtyPacking, item.FinishWidth, viewModel.Date, item.MaterialOrigin,
                        item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name);
                     
                    modelItem.DyeingPrintingAreaInputId = model.Id;
                    result += await _SPPRepository.InsertAsync(modelItem);

                    //result += await _SPPRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputQtyPacking);

                    if (item.Area == DyeingPrintingArea.PACKING)
                    {
                        //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                        var packingData = JsonConvert.DeserializeObject<List<PackingData>>(item.PrevSppInJson);
                        result += await _SPPRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                    }
                    else
                    {

                        result += await _SPPRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputQtyPacking);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.MaterialOrigin, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, modelItem.Id, item.ProductionOrder.Type,
                       item.ProductTextile.Id, item.ProductTextile.Code, item.ProductTextile.Name, null, item.Remark);
                    result += await _movementRepository.InsertAsync(movementModel);

                }
                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.TransitProductionOrders.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TERIMA);
            }



            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == TRANSIT && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.TRANSIT);
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
                Group = s.Group,
                Shift = s.Shift,
                TransitProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputTransitProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    InputQuantity = d.InputQuantity,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    HasOutputDocument = d.HasOutputDocument,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType,
                        OrderQuantity = d.ProductionOrderOrderQuantity,
                    },
                    MaterialWidth = d.MaterialWidth,
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
                    PackingLength = d.PackagingLength,
                    Area = d.Area,
                    AvalType = d.AvalType,
                    BalanceRemains = d.BalanceRemains,
                    DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    DyeingPrintingAreaOutputProductionOrderId = d.DyeingPrintingAreaOutputProductionOrderId,
                    PackingType = d.PackagingType,
                    QtyPacking = d.PackagingQty,
                    PackingUnit = d.PackagingUnit,
                    Grade = d.Grade,
                    Id = d.Id,
                    Unit = d.Unit,
                    InputQtyPacking = d.InputPackagingQty,
                    Remark = d.Remark,
                    Status = d.Status,
                    IsChecked = d.IsChecked,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit,
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

        public async Task<InputTransitViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<PreTransitIndexViewModel> ReadOutputPreTransit(int page, int size, string filter, string order, string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.DestinationArea == DyeingPrintingArea.TRANSIT && !s.HasNextAreaDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new PreTransitIndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument,
                PreTransitProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPreTransitProductionOrderViewModel()
                {
                    DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    PackingType = d.PackagingType,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Id = d.Id,
                    Motif = d.Motif,
                    Balance = d.Balance,
                    InputQuantity = d.Balance,
                    PackingInstruction = d.PackingInstruction,
                    Remark = d.Remark,
                    Status = d.Status,
                    Grade = d.Grade,
                    QtyPacking = d.PackagingQty,
                    InputQtyPacking = d.PackagingQty,
                    PackingUnit = d.PackagingUnit,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType,
                        OrderQuantity = d.ProductionOrderOrderQuantity
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
                    Unit = d.Unit,
                    UomUnit = d.UomUnit,
                    AvalType = d.AvalType,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    PackingLength = d.PackagingLength,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking,
                    PrevSppInJson = d.PrevSppInJson
                }).ToList()
            });

            return new ListResult<PreTransitIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public List<OutputPreTransitProductionOrderViewModel> GetOutputPreTransitProductionOrders()
        {
            var productionOrders = _outputSPPRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == DyeingPrintingArea.TRANSIT && !s.HasNextAreaDocument);
            var data = productionOrders.Select(d => new OutputPreTransitProductionOrderViewModel()
            {
                DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                {
                    Id = d.DeliveryOrderSalesId,
                    No = d.DeliveryOrderSalesNo
                },
                PackingType = d.PackagingType,
                Buyer = d.Buyer,
                BuyerId = d.BuyerId,
                CartNo = d.CartNo,
                Color = d.Color,
                Construction = d.Construction,
                Id = d.Id,
                Motif = d.Motif,
                Balance = d.Balance,
                InputQuantity = d.Balance,
                PackingInstruction = d.PackingInstruction,
                ProductionMachine=d.ProductionMachine,
                Remark = d.Remark,
                Area = d.Area,
                Status = d.Status,
                Grade = d.Grade,
                ProductionOrder = new ProductionOrder()
                {
                    Id = d.ProductionOrderId,
                    No = d.ProductionOrderNo,
                    Type = d.ProductionOrderType,
                    OrderQuantity = d.ProductionOrderOrderQuantity
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
                ProductTextile = new ProductTextile() { 
                    Id = d.ProductTextileId,
                    Code = d.ProductTextileCode,
                    Name = d.ProductTextileName
                },
                Unit = d.Unit,
                UomUnit = d.UomUnit,
                OutputId = d.DyeingPrintingAreaOutputId,
                QtyPacking = d.PackagingQty,
                InputQtyPacking = d.PackagingQty,
                PackingUnit = d.PackagingUnit,
                PackingLength = d.PackagingLength,
                DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                AvalType = d.AvalType,
                ProductSKUId = d.ProductSKUId,
                FabricSKUId = d.FabricSKUId,
                ProductSKUCode = d.ProductSKUCode,
                HasPrintingProductSKU = d.HasPrintingProductSKU,
                ProductPackingId = d.ProductPackingId,
                FabricPackingId = d.FabricPackingId,
                ProductPackingCode = d.ProductPackingCode,
                HasPrintingProductPacking = d.HasPrintingProductPacking,
                PrevSppInJson = d.PrevSppInJson,
                DateIn=d.DateIn,
            });

            return data.ToList();
        }

        public async Task<int> Reject(InputTransitViewModel viewModel)
        {
            int result = 0;

            var groupedProductionOrders = viewModel.TransitProductionOrders.GroupBy(s => s.Area);
            foreach (var item in groupedProductionOrders)
            {
                var model = _repository.GetDbSet().AsNoTracking()
                                .FirstOrDefault(s => s.Area == item.Key && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key && s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, item.Key);
                    model = new DyeingPrintingAreaInputModel(viewModel.Date, item.Key, viewModel.Shift, bonNo, viewModel.Group, item.Select(s =>
                         new DyeingPrintingAreaInputProductionOrderModel(item.Key, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                             s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, false, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.InputQuantity, s.BuyerId, s.Id, s.Material.Id,
                             s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.InputQtyPacking, s.PackingUnit, s.PackingType, s.DeliveryOrder.Id,
                             s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                             s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, 
                             s.FinishWidth, s.DateIn, s.MaterialOrigin, s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)).ToList());
                         

                    result = await _repository.InsertAsync(model);

                    //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                    result += await _outputSPPRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);
                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id);
                        //result += await _SPPRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _SPPRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _SPPRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);
                        }
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type,
                                detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.GUDANGJADI || item.Key == DyeingPrintingArea.SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, 
                                detail.ProductionOrder.Type, detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name, detail.Grade, null, detail.PackingType, detail.InputQtyPacking, detail.PackingUnit, detail.PackingLength);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                    }
                }
                else
                {
                    foreach (var detail in item)
                    {
                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(item.Key, detail.ProductionOrder.Id, detail.ProductionOrder.No, detail.ProductionOrder.Type, detail.ProductionOrder.OrderQuantity,
                            detail.PackingInstruction, detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, false, detail.Remark, detail.ProductionMachine, detail.Grade, detail.Status, detail.InputQuantity, detail.BuyerId, detail.Id, detail.Material.Id,
                            detail.Material.Name, detail.MaterialConstruction.Id, detail.MaterialConstruction.Name, detail.MaterialWidth, detail.InputQtyPacking, detail.PackingUnit, detail.PackingType,
                            detail.DeliveryOrder.Id, detail.DeliveryOrder.No, detail.AvalType, detail.ProcessType.Id, detail.ProcessType.Name, detail.YarnMaterial.Id, detail.YarnMaterial.Name,
                            detail.ProductSKUId, detail.FabricSKUId, detail.ProductSKUCode, detail.HasPrintingProductSKU, detail.ProductPackingId, detail.FabricPackingId, detail.ProductPackingCode,
                            detail.HasPrintingProductPacking, detail.PackingLength, detail.InputQuantity, detail.InputQtyPacking, detail.FinishWidth, detail.DateIn, detail.MaterialOrigin,
                            detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name);
                         
                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _SPPRepository.InsertAsync(modelItem);
                        //result += await _SPPRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _SPPRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _SPPRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);
                        }

                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type,
                                detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type,
                                detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.GUDANGJADI || item.Key == DyeingPrintingArea.SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, 
                                detail.ProductionOrder.Type, detail.ProductTextile.Id, detail.ProductTextile.Code, detail.ProductTextile.Name, detail.Grade, null, detail.PackingType, detail.InputQtyPacking, detail.PackingUnit, detail.PackingLength);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                    }
                    result += await _outputSPPRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);
                }
            }


            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var model = await _repository.ReadByIdAsync(id);

            if (model.DyeingPrintingAreaInputProductionOrders.Any(s => !s.HasOutputDocument && s.BalanceRemains != s.Balance))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Transit!");
            }

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                if (!item.HasOutputDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, item.MaterialOrigin, model.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteTransitArea(model);

            return result;
        }

        public async Task<int> Update(int id, InputTransitViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.Group, viewModel.TransitProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                        s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, s.HasOutputDocument, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.InputQuantity,
                        s.BuyerId, s.DyeingPrintingAreaOutputProductionOrderId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth,
                        s.InputQtyPacking, s.PackingUnit, s.PackingType, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                        s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking,
                        s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.FinishWidth, s.DateIn, s.MaterialOrigin, s.ProductTextile.Id, s.ProductTextile.Code, s.ProductTextile.Name)

                    {
                        Id = s.Id
                    }).ToList());

            var deletedData = dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument && !viewModel.TransitProductionOrders.Any(d => d.Id == s.Id)).ToList();

            if (deletedData.Any(item => !item.HasOutputDocument && item.BalanceRemains != item.Balance))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Inspection Material!");
            }

            result = await _repository.UpdateTransitArea(id, model, dbModel);

            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, item.MaterialOrigin, dbModel.Area, DyeingPrintingArea.IN, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.ProductTextileId, item.ProductTextileCode, item.ProductTextileName, null, item.Remark);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _repository.ReadAll()
            //    .Where(s => s.Area == TRANSIT && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.TRANSIT);

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


            query = query.OrderBy(s => s.BonNo);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin Produksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Terima", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "","", "", "", "", "", "", "", "","", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument).OrderBy(s => s.ProductionOrderNo))
                    foreach (var item in model.DyeingPrintingAreaInputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                    {
                        var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                        
                        dt.Rows.Add(model.BonNo, item.ProductionOrderNo, dateIn,  item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                            item.CartNo, item.Construction, item.ProductTextileName, item.MaterialOrigin, item.Unit, item.Buyer, item.Color, item.Motif, item.Remark,item.ProductionMachine, item.Grade, item.UomUnit, item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture));
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Transit") }, true);
        }
    }
}
