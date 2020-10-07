using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit
{
    public class OutputTransitService : IOutputTransitService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;


        public OutputTransitService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private async Task<OutputTransitViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputTransitViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputTransitViewModel()
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
                    Type = DyeingPrintingArea.OUT,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    TransitProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputTransitProductionOrderViewModel()
                    {
                        AvalType = s.AvalType,
                        DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                        {
                            Id = s.DeliveryOrderSalesId,
                            No = s.DeliveryOrderSalesNo
                        },
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        Balance = s.Balance,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        CartNo = s.CartNo,
                        Color = s.Color,
                        Construction = s.Construction,
                        CreatedAgent = s.CreatedAgent,
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        QtyPacking = s.PackagingQty,
                        PackingType = s.PackagingType,
                        PackingUnit = s.PackagingUnit,
                        PackingLength = s.PackagingLength,
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
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
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
                        Unit = s.Unit,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
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

                foreach (var item in vm.TransitProductionOrders)
                {
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (inputData != null)
                    {
                        item.PreviousBalance = inputData.Balance;
                        item.BalanceRemains = inputData.BalanceRemains + item.Balance;
                    }
                }
            }
            else
            {
                vm = new OutputTransitViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Type = DyeingPrintingArea.ADJ,
                    AdjType = model.Type,
                    Group = model.Group,
                    BonNo = model.BonNo,
                    AdjItemCategory = model.AdjItemCategory,
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
                    TransitProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputTransitProductionOrderViewModel()
                    {
                        AvalType = s.AvalType,
                        DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                        {
                            Id = s.DeliveryOrderSalesId,
                            No = s.DeliveryOrderSalesNo
                        },
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
                        MaterialWidth = s.MaterialWidth,
                        FinishWidth = s.FinishWidth,
                        AdjDocumentNo = s.AdjDocumentNo,
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
                        HasNextAreaDocument = s.HasNextAreaDocument,
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
                        Remark = s.Remark,
                        ProductionMachine = s.ProductionMachine,
                        UomUnit = s.UomUnit,
                        QtyPacking = s.PackagingQty,
                        PackingInstruction = s.PackingInstruction,
                        Grade = s.Grade,
                        PackingLength = s.PackagingLength,
                        PackingUnit = s.PackagingUnit,
                        PackingType = s.PackagingType,
                        Status = s.Status,
                        ProductSKUId = s.ProductSKUId,
                        FabricSKUId = s.FabricSKUId,
                        ProductSKUCode = s.ProductSKUCode,
                        HasPrintingProductSKU = s.HasPrintingProductSKU,
                        ProductPackingId = s.ProductPackingId,
                        FabricPackingId = s.FabricPackingId,
                        ProductPackingCode = s.ProductPackingCode,
                        HasPrintingProductPacking = s.HasPrintingProductPacking,
                        DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId
                    }).ToList()
                };
                foreach (var item in vm.TransitProductionOrders)
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
            if (destinationArea == DyeingPrintingArea.GUDANGJADI)
            {

                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.TR, DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.GUDANGAVAL)
            {

                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.TR, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
            {

                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.TR, DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.TR, DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_IN, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_OUT, DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputTransitViewModel viewModel)
        {
            int result = 0;
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.TRANSIT && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == DyeingPrintingArea.OUT);

            viewModel.TransitProductionOrders = viewModel.TransitProductionOrders.Where(s => s.IsSave).ToList();
            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.TRANSIT && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.OUT);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, viewModel.Type,
                    viewModel.TransitProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id,
                    s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit,
                    s.Remark, s.ProductionMachine, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "",
                    s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                    s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.DateIn, s.FinishWidth)).ToList());
                    

                result = await _repository.InsertAsync(model);
                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {

                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                    }
                    else
                    {

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.TransitProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, item.Grade, item.Status, item.Balance, item.Id, item.BuyerId,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, "", item.QtyPacking, item.PackingType,
                        item.PackingUnit, item.DeliveryOrder.Id, item.DeliveryOrder.No, item.AvalType, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.DateIn item.FinishWidth);
                        
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);
                    if (viewModel.DestinationArea == DyeingPrintingArea.INSPECTIONMATERIAL)
                    {

                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.Id, item.Balance);
                    }
                    else
                    {

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);
                    }


                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }


            return result;
        }

        private async Task<int> CreateAdj(OutputTransitViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.TransitProductionOrders.All(d => d.Balance > 0))
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.TRANSIT && s.Type == DyeingPrintingArea.ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.TransitProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.TRANSIT && s.Type == DyeingPrintingArea.ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.TransitProductionOrders.Select(d => d.Balance));
                type = DyeingPrintingArea.ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _repository.GetDbSet().AsNoTracking()
                   .FirstOrDefault(s => s.Area == DyeingPrintingArea.TRANSIT && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type && s.AdjItemCategory == viewModel.AdjItemCategory);

            if (model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group, type, viewModel.AdjItemCategory,
                    viewModel.TransitProductionOrders.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, "", "", item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.AdjDocumentNo,
                        item.QtyPacking, item.PackingType, item.PackingUnit, 0, "", "", item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.DateIn item.FinishWidth)).ToList());
                        

                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    if (viewModel.AdjItemCategory == DyeingPrintingArea.KAIN)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.PackagingQty * -1);
                    }


                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.TransitProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, "", "", item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.AdjDocumentNo,
                        item.QtyPacking, item.PackingType, item.PackingUnit, 0, "", "", item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.DateIn, item.FinishWidth);
                        
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    if (viewModel.AdjItemCategory == DyeingPrintingArea.KAIN)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsWithFlagAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance * -1, item.QtyPacking * -1);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id, item.ProductionOrder.Type, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }



            return result;
        }

        public async Task<int> Create(OutputTransitViewModel viewModel)
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
            //var query = _repository.ReadAll().Where(s => s.Area == TRANSIT &&
            //(((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.TRANSIT);
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
                Type = s.Type == null || s.Type == DyeingPrintingArea.OUT ? DyeingPrintingArea.OUT : DyeingPrintingArea.ADJ,
                Shift = s.Shift,
                DestinationArea = s.DestinationArea,
                Group = s.Group,
                HasNextAreaDocument = s.HasNextAreaDocument,
                TransitProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputTransitProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Motif = d.Motif,
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
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType,
                        OrderQuantity = d.ProductionOrderOrderQuantity
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
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    Status = d.Status,
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

        public async Task<OutputTransitViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputTransitViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public List<InputTransitProductionOrderViewModel> GetInputTransitProductionOrders(long productionOrderId)
        {
            IQueryable<DyeingPrintingAreaInputProductionOrderModel> productionOrders;

            if (productionOrderId == 0)
            {
                productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                                    .Where(s => s.Area == DyeingPrintingArea.TRANSIT && !s.HasOutputDocument);
            }
            else
            {
                productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                                    .Where(s => s.Area == DyeingPrintingArea.TRANSIT && !s.HasOutputDocument && s.ProductionOrderId == productionOrderId);

            }
            var data = productionOrders.Select(d => new InputTransitProductionOrderViewModel()
            {
                AvalType = d.AvalType,
                DeliveryOrder = new CommonViewModelObjectProperties.DeliveryOrderSales()
                {
                    Id = d.DeliveryOrderSalesId,
                    No = d.DeliveryOrderSalesNo
                },
                Balance = d.Balance,
                PreviousBalance = d.Balance,
                Buyer = d.Buyer,
                BuyerId = d.BuyerId,
                CartNo = d.CartNo,
                Color = d.Color,
                Construction = d.Construction,
                HasOutputDocument = d.HasOutputDocument,
                Motif = d.Motif,
                QtyPacking = d.PackagingQty,
                PackingType = d.PackagingType,
                PackingLength = d.PackagingLength,
                PackingUnit = d.PackagingUnit,
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
                Grade = d.Grade,
                BalanceRemains = d.BalanceRemains,
                Id = d.Id,
                Unit = d.Unit,
                Remark = d.Remark,
                ProductionMachine = d.ProductionMachine,
                Status = d.Status,
                IsChecked = d.IsChecked,
                PackingInstruction = d.PackingInstruction,
                UomUnit = d.UomUnit,
                InputId = d.DyeingPrintingAreaInputId,
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

            return data.ToList();
        }

        private async Task<int> DeleteOut(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;

            //if (model.DyeingPrintingAreaOutputProductionOrders.Any(s => s.HasNextAreaDocument))
            //{
            //    throw new Exception(string.Format("Ada SPP yang Sudah Dibuat di Penerimaan Area {0}!", model.DestinationArea));
            //}

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteTransitArea(model);

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
                var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                   item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, null, item.Remark);

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

        private async Task<int> UpdateOut(int id, OutputTransitViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea,
                    viewModel.Group, viewModel.Type, viewModel.TransitProductionOrders.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea,
                    s.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer,
                    s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.Balance, s.DyeingPrintingAreaInputProductionOrderId, s.BuyerId, s.Material.Id,
                    s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, "", s.QtyPacking, s.PackingType, s.PackingUnit, s.DeliveryOrder.Id,
                    s.DeliveryOrder.No, s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                    s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.DateIn, s.FinishWidth)
                    
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

            //if (deletedData.Any(item => item.HasNextAreaDocument))
            //{
            //    throw new Exception(string.Format("Ada SPP yang Sudah Dibuat di Penerimaan Area {0}!", dbModel.DestinationArea));
            //}
            result = await _repository.UpdateTransitArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, DyeingPrintingArea.OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, null, item.Remark);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputTransitViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            if (viewModel.TransitProductionOrders.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group, type, viewModel.AdjItemCategory,
                    viewModel.TransitProductionOrders.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.ProductionMachine, "", "", item.Balance, item.DyeingPrintingAreaInputProductionOrderId, item.BuyerId,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.AdjDocumentNo, item.QtyPacking,
                        item.PackingType, item.PackingUnit, 0, "", "", item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.ProductSKUId, item.FabricSKUId, item.ProductSKUCode, item.HasPrintingProductSKU, item.ProductPackingId, item.FabricPackingId, item.ProductPackingCode, item.HasPrintingProductPacking, item.PackingLength, item.DateIn, item.FinishWidth)
                        
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
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, null, item.Remark);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, null, item.Remark);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputTransitViewModel viewModel)
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

        public ListResult<InputTransitProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.Area == DyeingPrintingArea.TRANSIT && !s.HasOutputDocument);
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
                .Select(s => new InputTransitProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    }
                });

            return new ListResult<InputTransitProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        private MemoryStream GenerateExcelOut(OutputTransitViewModel viewModel, int timeZone)
        {
            var query = viewModel.TransitProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin Produksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "","", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.DateIn.ToOffset(new TimeSpan(timeZone,0,0)).Date.ToString("d"), viewModel.Date.ToOffset(new TimeSpan(timeZone, 0, 0)).Date.ToString("d"), item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.Construction, item.Unit, item.Buyer,
                        item.Color, item.Motif, item.Remark, item.ProductionMachine, item.Grade, item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.UomUnit, "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Transit") }, true);
        }

        private MemoryStream GenerateExcelAdj(OutputTransitViewModel viewModel)
        {
            var query = viewModel.TransitProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.Construction, item.Unit,
                               item.Buyer, item.Color, item.Motif, item.Remark, item.UomUnit, item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Transit") }, true);
        }

        public MemoryStream GenerateExcel(OutputTransitViewModel viewModel,int TimeZone)
        {
            if (viewModel.Type == null || viewModel.Type == DyeingPrintingArea.OUT)
            {
                return GenerateExcelOut(viewModel, TimeZone);
            }
            else
            {
                return GenerateExcelAdj(viewModel);
            }
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == TRANSIT &&
            //    (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));
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


            query = query.OrderBy(s => s.Type).ThenBy(s => s.DestinationArea).ThenBy(d => d.BonNo);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin Produksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Zona Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "","", "", "","", "", "", "", "", "", "", "", "","", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
                    {
                        //foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.ProductionOrderNo))
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.ProductionOrderNo, item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d"), model.Date.ToOffset(new TimeSpan(offSet,0,0)).Date.ToString("d"), item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Remark,item.ProductionMachine, item.Grade, item.UomUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), model.DestinationArea, DyeingPrintingArea.OUT, item.NextAreaInputStatus);

                        }

                    }
                    else
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.ProductionOrderNo, item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d"),model.Date.ToOffset(new TimeSpan(offSet,0,0)).Date.ToString("d"), item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Remark,"", item.Grade, item.UomUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), model.DestinationArea, DyeingPrintingArea.ADJ, "");

                        }
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Transit") }, true);
        }

        public ListResult<AdjTransitProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword, string adjItemCategory)
        {
            //var query = _inputProductionOrderRepository.ReadAll()
            //    .Where(s => s.Area == TRANSIT && !s.HasOutputDocument)
            //    .Select(d => new PlainAdjTransitProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Remark = d.Remark,
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
            //    .Where(s => s.Area == TRANSIT && !s.HasNextAreaDocument)
            //    .Select(d => new PlainAdjTransitProductionOrder()
            //    {
            //        Area = d.Area,
            //        Buyer = d.Buyer,
            //        BuyerId = d.BuyerId,
            //        Remark = d.Remark,
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
            //        ProcessTypeId = d.ProcessTypeId,
            //        ProcessTypeName = d.ProcessTypeName,
            //        YarnMaterialId = d.YarnMaterialId,
            //        YarnMaterialName = d.YarnMaterialName,
            //        ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
            //        ProductionOrderType = d.ProductionOrderType,
            //        Unit = d.Unit,
            //        UomUnit = d.UomUnit,
            //        ProductSKUId = d.ProductSKUId,
            //        FabricSKUId = d.FabricSKUId,
            //        ProductSKUCode = d.ProductSKUCode,
            //        HasPrintingProductSKU = d.HasPrintingProductSKU,
            //        ProductPackingId = d.ProductPackingId,
            //        FabricPackingId = d.FabricPackingId,
            //        ProductPackingCode = d.ProductPackingCode,
            //        HasPrintingProductPacking = d.HasPrintingProductPacking
            //    }));

            var categoryQuery = _inputProductionOrderRepository.ReadAll();

            if (adjItemCategory == DyeingPrintingArea.KAIN)
            {
                categoryQuery = categoryQuery.Where(s => s.PackagingUnit == null);
            }
            else
            {
                categoryQuery = categoryQuery.Where(s => s.PackagingUnit != null);
            }

            var query = categoryQuery
                 .Where(s => s.Area == DyeingPrintingArea.TRANSIT && !s.HasOutputDocument)
                 .Select(d => new PlainAdjTransitProductionOrder()
                 {
                     Id = d.Id,
                     BalanceRemains = d.BalanceRemains,
                     Area = d.Area,
                     Buyer = d.Buyer,
                     BuyerId = d.BuyerId,
                     Remark = d.Remark,
                     Color = d.Color,
                     Construction = d.Construction,
                     MaterialConstructionId = d.MaterialConstructionId,
                     MaterialConstructionName = d.MaterialConstructionName,
                     MaterialId = d.MaterialId,
                     MaterialName = d.MaterialName,
                     MaterialWidth = d.MaterialWidth,
                     FinishWidth = d.FinishWidth,
                     PackagingLength = d.PackagingLength,
                     PackagingQty = d.PackagingQty,
                     PackagingType = d.PackagingType,
                     PackagingUnit = d.PackagingUnit,
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

            query = QueryHelper<PlainAdjTransitProductionOrder>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjTransitProductionOrder>.Filter(query, FilterDictionary);

            var data = query.ToList()
                //.GroupBy(d => d.ProductionOrderId)
                //.Select(s => s.First())
                .OrderBy(s => s.ProductionOrderNo)
                .Skip((page - 1) * size).Take(size)
                .Select(s => new AdjTransitProductionOrderViewModel()
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
                    Remark = s.Remark,
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
                    MaterialWidth = s.MaterialWidth,
                    FinishWidth = s.FinishWidth,
                    Motif = s.Motif,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    PackingLength = s.PackagingLength,
                    PackingType = s.PackagingType,
                    QtyPacking = s.PackagingQty,
                    PackingUnit = s.PackagingUnit,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    HasPrintingProductPacking = s.HasPrintingProductPacking
                });

            return new ListResult<AdjTransitProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public MemoryStream GenerateExcel(OutputTransitViewModel viewModel)
        {
            if (viewModel.Type == null || viewModel.Type == DyeingPrintingArea.OUT)
            {
                throw new NotImplementedException();
              //  return GenerateExcelOut(viewModel);
            }
            else
            {
                return GenerateExcelAdj(viewModel);
            }
        }
    }
}
