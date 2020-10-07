using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Data;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping
{
    public class InputShippingService : IInputShippingService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSPPRepository;


        public InputShippingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputSPPRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private InputShippingViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputShippingViewModel()
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
                DeletedBy = model.DeletedBy,
                Group = model.Group,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                ShippingType = model.ShippingType,
                ShippingProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputShippingProductionOrderViewModel()
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
                    Remark = s.Remark,
                    PackingInstruction = s.PackingInstruction,
                    Area = s.Area,
                    HasOutputDocument = s.HasOutputDocument,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Packing = s.PackagingUnit,
                    Motif = s.Motif,
                    Grade = s.Grade,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo
                    },
                    QtyPacking = s.PackagingQty,
                    InputQtyPacking = s.InputPackagingQty,
                    PackingType = s.PackagingType,
                    Qty = s.Balance,
                    InputQuantity = s.InputQuantity,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    DeliveryOrderRetur = new DeliveryOrderRetur()
                    {
                        Id = s.DeliveryOrderReturId,
                        No = s.DeliveryOrderReturNo
                    },
                    MaterialWidth = s.MaterialWidth,
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
            else if (area == DyeingPrintingArea.GUDANGAVAL)
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == DyeingPrintingArea.GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {

                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        public async Task<int> Create(InputShippingViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.ShippingType == viewModel.ShippingType);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.ShippingType, viewModel.ShippingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type,
                     s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction, s.PackingType, s.Color, s.Motif, s.Grade, s.InputQtyPacking, s.Packing, s.InputQuantity, s.UomUnit, false, s.InputQuantity, s.Unit,
                     s.BuyerId, s.Id, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark, s.ProcessType.Id, s.ProcessType.Name,
                     s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, 
                     s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, viewModel.Date, s.FinishWidth)).ToList());
                     

                result = await _repository.InsertAsync(model);

                //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);
                if(viewModel.ShippingType == DyeingPrintingArea.ZONAGUDANG)
                {
                    result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.ShippingProductionOrders.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TERIMA);
                }
               
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {

                    var itemVM = viewModel.ShippingProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaOutputProductionOrderId);
                    if(viewModel.ShippingType == DyeingPrintingArea.ZONAGUDANG)
                    {

                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputPackagingQty);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, item.Id, item.ProductionOrderType, item.Grade, 
                        null, item.PackagingType, item.InputPackagingQty, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            else
            {
                foreach (var item in viewModel.ShippingProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.DeliveryOrder.Id, item.DeliveryOrder.No, item.ProductionOrder.Id,
                        item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction, item.PackingType, item.Color, item.Motif,
                        item.Grade, item.InputQtyPacking, item.Packing, item.InputQuantity, item.UomUnit, false, item.InputQuantity, item.Unit, item.BuyerId, item.Id,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.CartNo, item.Remark,
                        item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name, item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, 
                        item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.InputQuantity, 
                        item.InputQtyPacking, item.DeliveryOrderRetur.Id, item.DeliveryOrderRetur.No, viewModel.Date, item.FinishWidth);
                        
                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    result += await _productionOrderRepository.InsertAsync(modelItem);

                    if(viewModel.ShippingType == DyeingPrintingArea.ZONAGUDANG)
                    {
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.InputQuantity, item.InputQtyPacking);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, modelItem.Id, item.ProductionOrder.Type, 
                        item.Grade, null, item.PackingType, item.InputQtyPacking, item.Packing, item.PackingLength);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
                if(viewModel.ShippingType == DyeingPrintingArea.ZONAGUDANG)
                {

                    result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.ShippingProductionOrders.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TERIMA);
                }
            }



            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.SHIPPING);
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
                ShippingType = s.ShippingType,
                ShippingProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputShippingProductionOrderViewModel()
                {
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    HasOutputDocument = d.HasOutputDocument,
                    Id = d.Id,
                    Motif = d.Motif,
                    Grade = d.Grade,
                    PackingType = d.PackagingType,
                    PackingLength = d.PackagingLength,
                    Qty = d.Balance,
                    InputQuantity = d.InputQuantity,
                    QtyPacking = d.PackagingQty,
                    InputQtyPacking = d.InputPackagingQty,
                    Packing = d.PackagingUnit,
                    DyeingPrintingAreaOutputProductionOrderId = d.DyeingPrintingAreaOutputProductionOrderId,
                    PackingInstruction = d.PackingInstruction,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo
                    },
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        OrderQuantity = d.ProductionOrderOrderQuantity,
                        Type = d.ProductionOrderType
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
                    DeliveryOrderRetur = new DeliveryOrderRetur()
                    {
                        Id = d.DeliveryOrderReturId,
                        No = d.DeliveryOrderReturNo
                    },
                    Unit = d.Unit,
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


        public async Task<InputShippingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<PreShippingIndexViewModel> ReadOutputPreShipping(int page, int size, string filter, string order, string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.DestinationArea == DyeingPrintingArea.SHIPPING && !s.HasNextAreaDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new PreShippingIndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument
            });

            return new ListResult<PreShippingIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<InputShippingProductionOrderViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword)
        {
            var query = _productionOrderRepository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputShippingProductionOrderViewModel()
            {
                Id = s.Id,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                DeliveryOrderRetur = new DeliveryOrderRetur()
                {
                    Id = s.DeliveryOrderReturId,
                    No = s.DeliveryOrderReturNo
                },
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                MaterialWidth = s.MaterialWidth,
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
                Remark = s.Remark,
                CartNo = s.CartNo,
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                PackingType = s.PackagingType,
                Grade = s.Grade,
                QtyPacking = s.PackagingQty,
                InputQtyPacking = s.InputPackagingQty,
                Packing = s.PackagingUnit,
                Qty = s.Balance,
                InputQuantity = s.InputQuantity,
                UomUnit = s.UomUnit,
                PackingLength = s.PackagingLength,
                HasOutputDocument = s.HasOutputDocument,
                ProductSKUId = s.ProductSKUId,
                FabricSKUId = s.FabricSKUId,
                ProductSKUCode = s.ProductSKUCode,
                HasPrintingProductSKU = s.HasPrintingProductSKU,
                ProductPackingId = s.ProductPackingId,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                HasPrintingProductPacking = s.HasPrintingProductPacking,
                DateIn=s.DateIn
            });

            return new ListResult<InputShippingProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public List<OutputPreShippingProductionOrderViewModel> GetOutputPreShippingProductionOrders()
        {
            var productionOrders = _outputSPPRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == DyeingPrintingArea.SHIPPING && !s.HasNextAreaDocument);
            var data = productionOrders.Select(s => new OutputPreShippingProductionOrderViewModel()
            {
                Id = s.Id,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                DeliveryOrderRetur = new DeliveryOrderRetur(),
                MaterialWidth = s.MaterialWidth,
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
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                Unit = s.Unit,
                CartNo = s.CartNo,
                Area = s.Area,
                PackingType = s.PackagingType,
                Remark = s.Remark,
                Grade = s.Grade,
                QtyPacking = s.PackagingQty,
                InputQtyPacking = s.PackagingQty,
                Packing = s.PackagingUnit,
                PackingInstruction = s.PackingInstruction,
                Qty = s.Balance,
                InputQuantity = s.Balance,
                PackingLength = s.PackagingLength,
                UomUnit = s.UomUnit,
                OutputId = s.DyeingPrintingAreaOutputId,
                DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
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

        public async Task<int> Reject(InputShippingViewModel viewModel)
        {
            int result = 0;
            var groupedProductionOrders = viewModel.ShippingProductionOrders.GroupBy(s => s.Area);

            if(viewModel.ShippingType != DyeingPrintingArea.ZONAGUDANG)
            {
                return result;
            }

            foreach (var item in groupedProductionOrders)
            {
                var model = _repository.GetDbSet().AsNoTracking()
                                .FirstOrDefault(s => s.Area == item.Key && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key && s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, item.Key);
                    model = new DyeingPrintingAreaInputModel(viewModel.Date, item.Key, viewModel.Shift, bonNo, viewModel.Group,
                                    item.Select(s => new DyeingPrintingAreaInputProductionOrderModel(item.Key, s.ProductionOrder.Id,
                                        s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit,
                                        s.InputQuantity, false, s.Packing, s.PackingType, s.InputQtyPacking, s.Grade, s.ProductionOrder.OrderQuantity, s.BuyerId, s.Id, s.Remark, s.InputQuantity,
                                        s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode,
                     s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DateIn, s.FinishWidth)).ToList());
                     

                    result = await _repository.InsertAsync(model);
                    result += await _outputSPPRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);
                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id);
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, 
                            detail.Grade, null, detail.PackingType, detail.InputQtyPacking, detail.Packing, detail.PackingLength);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }
                }
                else
                {
                    foreach (var detail in item)
                    {

                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(item.Key, detail.ProductionOrder.Id, detail.ProductionOrder.No, detail.ProductionOrder.Type,
                                        detail.PackingInstruction, detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit,
                                        detail.InputQuantity, false, detail.Packing, detail.PackingType, detail.InputQtyPacking, detail.Grade, detail.ProductionOrder.OrderQuantity, detail.BuyerId,
                                        detail.Id, detail.Remark, detail.InputQuantity,
                                        detail.Material.Id, detail.Material.Name, detail.MaterialConstruction.Id, detail.MaterialConstruction.Name, detail.MaterialWidth, detail.ProcessType.Id, detail.ProcessType.Name, detail.YarnMaterial.Id, detail.YarnMaterial.Name, detail.ProductSKUId, detail.FabricSKUId, detail.ProductSKUCode,
                     detail.HasPrintingProductSKU, detail.ProductPackingId, detail.FabricPackingId, detail.ProductPackingCode, detail.HasPrintingProductPacking, detail.PackingLength,
                     detail.InputQuantity, detail.InputQtyPacking, detail.DateIn, detail.FinishWidth);
                     
                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _productionOrderRepository.InsertAsync(modelItem);
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputQtyPacking);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type, 
                            detail.Grade, null, detail.PackingType, detail.InputQtyPacking, detail.Packing, detail.PackingLength);

                        result += await _movementRepository.InsertAsync(movementModel);
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
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Shipping!");
            }

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                if (!item.HasOutputDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, 
                       item.Grade, null, item.PackagingType, item.InputPackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteShippingArea(model);

            return result;
        }

        public async Task<int> Update(int id, InputShippingViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.Group, viewModel.ShippingType, viewModel.ShippingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type,
                     s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction, s.PackingType, s.Color, s.Motif, s.Grade, s.InputQtyPacking, s.Packing, s.InputQuantity, s.UomUnit, s.HasOutputDocument,
                     s.InputQuantity, s.Unit, s.BuyerId, s.DyeingPrintingAreaInputProductionOrderId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.CartNo, s.Remark,
                     s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, 
                     s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputQtyPacking, s.DeliveryOrderRetur.Id, s.DeliveryOrderRetur.No, s.DateIn, s.FinishWidth)
                     
                     {
                         Id = s.Id
                     }).ToList());

            var deletedData = dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument && !viewModel.ShippingProductionOrders.Any(d => d.Id == s.Id)).ToList();

            if (deletedData.Any(item => !item.HasOutputDocument && item.BalanceRemains != item.Balance))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Shipping!");
            }

            result = await _repository.UpdateShippingArea(id, model, dbModel);

            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, DyeingPrintingArea.IN, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, 
                       item.Grade, null, item.PackagingType, item.InputPackagingQty * -1, item.PackagingUnit, item.PackagingLength);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _repository.ReadAll()
            //    .Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll()
                .Where(s => s.Area == DyeingPrintingArea.SHIPPING);

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
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Delivery Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            //dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "","", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument).OrderBy(s => s.ProductionOrderNo))
                    foreach (var item in model.DyeingPrintingAreaInputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                    {
                        dt.Rows.Add(model.BonNo, item.DeliveryOrderSalesNo,model.Date.ToOffset(new TimeSpan(offSet,0,0)).Date.ToString("d"), item.ProductionOrderNo, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                             item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Grade, item.Remark, item.InputPackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                             item.PackagingUnit, item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture), item.UomUnit);
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);
        }
    }
}
