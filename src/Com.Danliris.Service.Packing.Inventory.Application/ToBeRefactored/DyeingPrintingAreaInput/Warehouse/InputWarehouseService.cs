using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public class InputWarehouseService : IInputWarehouseService
    {
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string TYPE = "IN";

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

        public InputWarehouseService(IServiceProvider serviceProvider)
        {
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        //Get All (List)
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument && d.Balance > 0));

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
                Shift = s.Shift,
                Group = s.Group,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        //Get By Id
        private InputWarehouseDetailViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputWarehouseDetailViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Group = model.Group,
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
                WarehousesProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputWarehouseProductionOrderDetailViewModel()
                {
                    Id = s.Id,
                    ProductionOrderId = s.ProductionOrderId,
                    ProductionOrderNo = s.ProductionOrderNo,
                    ProductionOrderType = s.ProductionOrderType,
                    ProductionOrderOrderQuantity = s.ProductionOrderOrderQuantity,
                    ProductionOrderItems = model.DyeingPrintingAreaInputProductionOrders.Select(o => new ProductionOrderItemListDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
                        },
                        CartNo = s.CartNo,
                        Buyer = s.Buyer,
                        BuyerId = s.BuyerId,
                        Construction = s.Construction,
                        Unit = s.Unit,
                        Color = s.Color,
                        Motif = s.Motif,
                        UomUnit = s.UomUnit,
                        Remark = s.Remark,
                        Grade = s.Grade,
                        Status = s.Status,
                        Balance = s.Balance,
                        PackingInstruction = s.PackingInstruction,
                        PackagingType = s.PackagingType,
                        PackagingQty = s.PackagingQty,
                        PackagingUnit = s.PackagingUnit,
                        AvalALength = s.AvalALength,
                        AvalBLength = s.AvalBLength,
                        AvalConnectionLength = s.AvalConnectionLength,
                        DeliveryOrderSalesId = s.DeliveryOrderSalesId,
                        DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                        AvalType = s.AvalType,
                        AvalCartNo = s.AvalCartNo,
                        AvalQuantityKg = s.AvalQuantityKg,
                        //Description = ,
                        //DeliveryNote = ,
                        Area = s.Area,
                        HasOutputDocument = s.HasOutputDocument,
                        DyeingPrintingAreaInputId = s.DyeingPrintingAreaInputId,
                        Qty = s.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(s.Balance), s.PackagingQty)
                    }).Distinct(new PackingComparer()).ToList(),
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy
                }).ToList()
            };

            return vm;
        }

        public async Task<InputWarehouseDetailViewModel> ReadById(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;
            
            InputWarehouseDetailViewModel vm = MapToViewModel(model);

            return vm;
        }

        //Create - Generate Bon No
        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        //Create
        public async Task<int> Create(InputWarehouseCreateViewModel viewModel)
        {
            int result = 0;

            var model = _inputRepository.GetDbSet().Include(s => s.DyeingPrintingAreaInputProductionOrders)
                                                   .FirstOrDefault(s => s.Area == GUDANGJADI &&
                                                                        s.Date.Date == viewModel.Date.Date &&
                                                                        s.Shift == viewModel.Shift);

            if (model != null)
            {
                result = await UpdateExistingWarehouse(viewModel, model.Id, model.BonNo);
            }
            else
            {
                result = await InsertNewWarehouse(viewModel);
            }

            return result;
        }

        //Create - Insert New Warehouse
        public async Task<int> InsertNewWarehouse(InputWarehouseCreateViewModel viewModel)
        {
            int result = 0;

            int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGJADI &&
                                                                                              s.CreatedUtc.Year == viewModel.Date.Year);

            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

            //Mapping ViewModel to DyeingPrintingAreaInputModel
            var model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                         viewModel.Area,
                                                         viewModel.Shift,
                                                         bonNo,
                                                         viewModel.Group,
                                                         viewModel.MappedWarehousesProductionOrders.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                                                                                                s.ProductionOrder.Id,
                                                                                                                                                                s.ProductionOrder.No,
                                                                                                                                                                s.ProductionOrder.Type,
                                                                                                                                                                s.PackingInstruction,
                                                                                                                                                                s.CartNo,
                                                                                                                                                                s.Buyer,
                                                                                                                                                                s.Construction,
                                                                                                                                                                s.Unit,
                                                                                                                                                                s.Color,
                                                                                                                                                                s.Motif,
                                                                                                                                                                s.UomUnit,
                                                                                                                                                                s.Balance,
                                                                                                                                                                false,
                                                                                                                                                                s.PackagingUnit,
                                                                                                                                                                s.PackagingType,
                                                                                                                                                                s.PackagingQty,
                                                                                                                                                                s.Grade,
                                                                                                                                                                s.ProductionOrder.OrderQuantity,
                                                                                                                                                                s.BuyerId,
                                                                                                                                                                s.Id))
                                                                                             .ToList());
            //Insert to Input Repository
            result = await _inputRepository.InsertAsync(model);

            foreach (var item in viewModel.MappedWarehousesProductionOrders)
            {
                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        model.Id,
                                                                        model.BonNo,
                                                                        item.ProductionOrder.Id,
                                                                        item.ProductionOrderNo,
                                                                        item.CartNo,
                                                                        item.Buyer,
                                                                        item.Construction,
                                                                        item.Unit,
                                                                        item.Color,
                                                                        item.Motif,
                                                                        item.UomUnit,
                                                                        item.Balance);

                //Find Previous Summary by DyeingPrintingAreaDocumentId & ProductionOrderId
                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId &&
                                                                                       s.ProductionOrderId == item.ProductionOrder.Id);

                //Mapping to DyeingPrintingAreaSummaryModel
                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                                                                      viewModel.Area,
                                                                      TYPE,
                                                                      model.Id,
                                                                      model.BonNo,
                                                                      item.ProductionOrder.Id,
                                                                      item.ProductionOrderNo,
                                                                      item.CartNo,
                                                                      item.Buyer,
                                                                      item.Construction,
                                                                      item.Unit,
                                                                      item.Color,
                                                                      item.Motif,
                                                                      item.UomUnit,
                                                                      item.Balance);

                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                if (previousSummary == null)
                {
                    //Update Previous Summary with Summary Model Created Before
                    result += await _summaryRepository.InsertAsync(summaryModel);
                }
                else
                {

                    //Update Previous Summary with Summary Model Created Before
                    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                }
            }

            //Update from Output Only (Parent) Flag for HasNextAreaDocument == True (Because Not All Production Order Checked from UI)
            List<int> listOfDyeingPrintingAreaIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.OutputId).Distinct().ToList();
            foreach (var areaId in listOfDyeingPrintingAreaIds)
            {
                result += await _outputRepository.UpdateFromInputNextAreaFlagParentOnlyAsync(areaId, true);
            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            List<int> listOfOutputProductionOrderIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.Id).Distinct().ToList();
            foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            {
                result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true);
            }

            return result;
        }

        //Create - Update Existing Warehouse
        public async Task<int> UpdateExistingWarehouse(InputWarehouseCreateViewModel viewModel, int dyeingPrintingAreaInputId, string bonNo)
        {
            int result = 0;

            foreach (var productionOrder in viewModel.MappedWarehousesProductionOrders)
            {
                //Mapping to DyeingPrintingAreaInputProductionOrderModel
                var productionOrderModel = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                           productionOrder.ProductionOrder.Id,
                                                                                           productionOrder.ProductionOrder.No,
                                                                                           productionOrder.ProductionOrder.Type,
                                                                                           productionOrder.PackingInstruction,
                                                                                           productionOrder.CartNo,
                                                                                           productionOrder.Buyer,
                                                                                           productionOrder.Construction,
                                                                                           productionOrder.Unit,
                                                                                           productionOrder.Color,
                                                                                           productionOrder.Motif,
                                                                                           productionOrder.UomUnit,
                                                                                           productionOrder.Balance,
                                                                                           false,
                                                                                           productionOrder.PackagingUnit,
                                                                                           productionOrder.PackagingType,
                                                                                           productionOrder.PackagingQty,
                                                                                           productionOrder.Grade,
                                                                                           productionOrder.ProductionOrder.OrderQuantity,
                                                                                           productionOrder.BuyerId,
                                                                                           productionOrder.Id)
                {
                    DyeingPrintingAreaInputId = dyeingPrintingAreaInputId,
                };

                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        productionOrderModel.Id,
                                                                        bonNo,
                                                                        productionOrderModel.ProductionOrderId,
                                                                        productionOrderModel.ProductionOrderNo,
                                                                        productionOrderModel.CartNo,
                                                                        productionOrderModel.Buyer,
                                                                        productionOrderModel.Construction,
                                                                        productionOrderModel.Unit,
                                                                        productionOrderModel.Color,
                                                                        productionOrderModel.Motif,
                                                                        productionOrderModel.UomUnit,
                                                                        productionOrderModel.Balance);

                //Find Previous Summary by DyeingPrintingAreaDocumentId & ProductionOrderId
                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == productionOrder.OutputId &&
                                                                                       s.ProductionOrderId == productionOrder.ProductionOrder.Id);

                //Mapping to DyeingPrintingAreaSummaryModel
                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                                                                      viewModel.Area,
                                                                      TYPE,
                                                                      productionOrderModel.Id,
                                                                      bonNo,
                                                                      productionOrderModel.ProductionOrderId,
                                                                      productionOrderModel.ProductionOrderNo,
                                                                      productionOrderModel.CartNo,
                                                                      productionOrderModel.Buyer,
                                                                      productionOrderModel.Construction,
                                                                      productionOrderModel.Unit,
                                                                      productionOrderModel.Color,
                                                                      productionOrderModel.Motif,
                                                                      productionOrderModel.UomUnit,
                                                                      productionOrderModel.Balance);

                //Insert to Input Production Order Repository
                result += await _inputProductionOrderRepository.InsertAsync(productionOrderModel);

                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                if(previousSummary == null)
                {
                    //Update Previous Summary with Summary Model Created Before
                    result += await _summaryRepository.InsertAsync(summaryModel);
                }
                else
                {

                    //Update Previous Summary with Summary Model Created Before
                    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                }



            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            List<int> listOfOutputProductionOrderIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.Id).ToList();
            foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            {
                result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true);
            }

            return result;
        }

        //Get Output Pre Warehouse Input
        public List<OutputPreWarehouseViewModel> GetOutputPreWarehouseProductionOrders()
        {
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.DestinationArea == GUDANGJADI &&
                                                                    !s.HasNextAreaDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new OutputPreWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p=>new OutputPreWarehouseItemListViewModel()
                {

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
                    OutputId = p.DyeingPrintingAreaOutputId,
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
                    Description = p.Description,
                    DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    DestinationArea = p.DestinationArea,
                    HasNextAreaDocument = p.HasNextAreaDocument,
                    DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(p.Balance), p.PackagingQty)
                }).ToList()

            });

            return data.ToList();
        }

        //Reject - Generate Bon No
        private string GenerateBonNoReject(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == PACKING)
            {

                return string.Format("{0}.{1}.{2}", PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == INSPECTIONMATERIAL)
            {

                return string.Format("{0}.{1}.{2}", IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}", TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        //Reject
        public async Task<int> Reject(RejectedInputWarehouseViewModel viewModel)
        {
            int result = 0;

            var groupedProductionOrders = viewModel.WarehousesProductionOrders.GroupBy(s => s.Area);
            foreach (var item in groupedProductionOrders)
            {
                var model = _inputRepository.GetDbSet().AsNoTracking()
                                .FirstOrDefault(s => s.Area == item.Key && 
                                                     s.Date.Date == viewModel.Date.Date && 
                                                     s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key && 
                                                                                                      s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNoReject(totalCurrentYearData + 1, viewModel.Date, item.Key);

                    model = new DyeingPrintingAreaInputModel(viewModel.Date, 
                                                             item.Key, 
                                                             viewModel.Shift, 
                                                             bonNo, 
                                                             viewModel.Group, 
                                                             viewModel.WarehousesProductionOrders.Select(s => 
                                                                new DyeingPrintingAreaInputProductionOrderModel(s.ProductionOrder.Id,
                                                                                                                s.ProductionOrder.No,
                                                                                                                s.CartNo,
                                                                                                                s.Buyer,
                                                                                                                s.Construction,
                                                                                                                s.Unit,
                                                                                                                s.Color,
                                                                                                                s.Motif,
                                                                                                                s.UomUnit,
                                                                                                                s.Balance,
                                                                                                                false,
                                                                                                                s.PackingInstruction,
                                                                                                                s.ProductionOrder.Type,
                                                                                                                s.ProductionOrder.OrderQuantity,
                                                                                                                s.Remark,
                                                                                                                s.Grade,
                                                                                                                s.Status,
                                                                                                                s.AvalALength,
                                                                                                                s.AvalBLength,
                                                                                                                s.AvalConnectionLength,
                                                                                                                s.AvalType,
                                                                                                                s.AvalCartNo,
                                                                                                                s.AvalQuantityKg,
                                                                                                                s.DeliveryOrderSalesId,
                                                                                                                s.DeliveryOrderSalesNo,
                                                                                                                s.PackagingUnit,
                                                                                                                s.PackagingType,
                                                                                                                s.PackagingQty,
                                                                                                                item.Key,
                                                                                                                s.Balance,
                                                                                                                s.InputId,
                                                                                                                s.BuyerId)).ToList());

                    result = await _inputRepository.InsertAsync(model);

                    //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                    result += await _outputProductionOrderRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);

                    foreach (var detail in item)
                    {
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, 
                                                                                item.Key, 
                                                                                TYPE, 
                                                                                model.Id, 
                                                                                model.BonNo, 
                                                                                detail.ProductionOrder.Id, 
                                                                                detail.ProductionOrder.No,
                                                                                detail.CartNo, 
                                                                                detail.Buyer, 
                                                                                detail.Construction, 
                                                                                detail.Unit, 
                                                                                detail.Color, 
                                                                                detail.Motif, 
                                                                                detail.UomUnit, 
                                                                                detail.Balance);

                        var previousSummary = 
                            _summaryRepository.ReadAll()
                                              .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.OutputId && 
                                                                   s.ProductionOrderId == detail.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, 
                                                                              item.Key, 
                                                                              TYPE, 
                                                                              model.Id, 
                                                                              model.BonNo, 
                                                                              detail.ProductionOrder.Id, 
                                                                              detail.ProductionOrder.No,
                                                                              detail.CartNo, 
                                                                              detail.Buyer, 
                                                                              detail.Construction, 
                                                                              detail.Unit, 
                                                                              detail.Color, 
                                                                              detail.Motif, 
                                                                              detail.UomUnit, 
                                                                              detail.Balance);

                        result += await _movementRepository.InsertAsync(movementModel);
                        if (previousSummary == null)
                        {

                            result += await _summaryRepository.InsertAsync(summaryModel);
                        }
                        else
                        {

                            result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                        }
                    }
                }
                else
                {
                    foreach (var detail in item)
                    {
                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(detail.ProductionOrder.Id,
                                                                                        detail.ProductionOrder.No,
                                                                                        detail.CartNo,
                                                                                        detail.Buyer,
                                                                                        detail.Construction,
                                                                                        detail.Unit,
                                                                                        detail.Color,
                                                                                        detail.Motif,
                                                                                        detail.UomUnit,
                                                                                        detail.Balance,
                                                                                        false,
                                                                                        detail.PackingInstruction,
                                                                                        detail.ProductionOrder.Type,
                                                                                        detail.ProductionOrder.OrderQuantity,
                                                                                        detail.Remark,
                                                                                        detail.Grade,
                                                                                        detail.Status,
                                                                                        detail.AvalALength,
                                                                                        detail.AvalBLength,
                                                                                        detail.AvalConnectionLength,
                                                                                        detail.AvalType,
                                                                                        detail.AvalCartNo,
                                                                                        detail.AvalQuantityKg,
                                                                                        detail.DeliveryOrderSalesId,
                                                                                        detail.DeliveryOrderSalesNo,
                                                                                        detail.PackagingUnit,
                                                                                        detail.PackagingType,
                                                                                        detail.PackagingQty,
                                                                                        item.Key,
                                                                                        detail.Balance,
                                                                                        detail.InputId,
                                                                                        detail.BuyerId);

                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, 
                                                                                item.Key, 
                                                                                TYPE, 
                                                                                model.Id, 
                                                                                model.BonNo, 
                                                                                detail.ProductionOrder.Id, 
                                                                                detail.ProductionOrder.No,
                                                                                detail.CartNo, 
                                                                                detail.Buyer, 
                                                                                detail.Construction, 
                                                                                detail.Unit, 
                                                                                detail.Color, 
                                                                                detail.Motif, 
                                                                                detail.UomUnit, 
                                                                                detail.Balance);

                        var previousSummary = 
                            _summaryRepository.ReadAll()
                                              .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.OutputId && 
                                                                   s.ProductionOrderId == detail.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, 
                                                                              item.Key, 
                                                                              TYPE, 
                                                                              model.Id, 
                                                                              model.BonNo, 
                                                                              detail.ProductionOrder.Id, 
                                                                              detail.ProductionOrder.No,
                                                                              detail.CartNo, 
                                                                              detail.Buyer, 
                                                                              detail.Construction, 
                                                                              detail.Unit, 
                                                                              detail.Color, 
                                                                              detail.Motif, 
                                                                              detail.UomUnit, 
                                                                              detail.Balance);

                        result += await _inputProductionOrderRepository.InsertAsync(modelItem);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);
                        result += await _movementRepository.InsertAsync(movementModel);

                        if (previousSummary == null)
                        {

                            result += await _summaryRepository.InsertAsync(summaryModel);
                        }
                        else
                        {

                            result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                        }

                    }
                    result += await _outputProductionOrderRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);
                }
            }

            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            //get bon data and check if it has document output
            var modelBon = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any()).FirstOrDefault();
            if (modelBon != null)
            {
                var hasSPPwithOutput = modelBon.DyeingPrintingAreaInputProductionOrders.Where(x => x.HasOutputDocument);
                if (hasSPPwithOutput.Count() > 0)
                {
                    throw new Exception("Bon Sudah Berada di Packing Keluar");
                }
                else
                {
                    //get prev bon id using first spp modelBon and search bonId
                    var firstSppBonModel = modelBon.DyeingPrintingAreaInputProductionOrders.FirstOrDefault();
                    int sppIdPrevOutput = firstSppBonModel == null ? 0 : firstSppBonModel.DyeingPrintingAreaOutputProductionOrderId;
                    var sppPrevOutput = _outputProductionOrderRepository.ReadAll().Where(s => s.Id == sppIdPrevOutput).FirstOrDefault();
                    int bonIdPrevOutput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaOutputId;
                    var bonPrevOutput = _outputRepository.ReadAll().Where(x =>
                                                                        x.DyeingPrintingAreaOutputProductionOrders.Any() &&
                                                                        x.Id == bonIdPrevOutput
                                                                        );
                    //get prev bon input using input spp id in prev bon out and search bonId
                    int sppIdPrevInput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaInputProductionOrderId;
                    var sppPrevInput = _inputProductionOrderRepository.ReadAll().FirstOrDefault(x => x.Id == sppIdPrevInput);
                    int bonIdPrevInput = sppPrevInput == null ? 0 : sppPrevInput.DyeingPrintingAreaInputId;
                    var bonPrevInput = _inputRepository.ReadAll().Where(x =>
                                                            x.DyeingPrintingAreaInputProductionOrders.Any() &&
                                                            x.Id == bonIdPrevInput
                                                            );


                    //delete entire packing bon and spp using model
                    result += await _inputRepository.DeleteAsync(bonId);

                    //activate bon prev hasNextAreaDocument == false;
                    foreach (var bon in bonPrevOutput)
                    {
                        bon.SetHasNextAreaDocument(false, "WAREHOUSESERVICE", "SERVICE");
                        //activate spp prev from bon
                        foreach (var spp in bon.DyeingPrintingAreaOutputProductionOrders)
                        {
                            spp.SetHasNextAreaDocument(false, "WAREHOUSESERVICE", "SERVICE");
                            //update balance input spp from prev spp
                            var inputSpp = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
                            foreach (var modifInputSpp in inputSpp)
                            {
                                var newBalance = modifInputSpp.Balance + spp.Balance;
                                modifInputSpp.SetBalanceRemains(newBalance, "WAREHOUSESERVICE", "SERVICE");
                                modifInputSpp.SetBalance(newBalance, "WAREHOUSESERVICE", "SERVICE");

                                modifInputSpp.SetHasOutputDocument(false, "WAREHOUSESERVICE", "SERVICE");
                                result += await _inputProductionOrderRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
                            }

                            //insert new movement spp
                            var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, bon.Area, "OUT", bon.Id, modelBon.BonNo, spp.Id, spp.ProductionOrderNo,
                                    spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance);
                            result += await _movementRepository.InsertAsync(movementModel);

                            //update summary spp if exist create new when it null
                            var summaryModel = new DyeingPrintingAreaSummaryModel(bon.Date, bon.Area, "OUT", bon.Id, bon.BonNo, spp.Id, spp.ProductionOrderNo,
                            spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance);

                            var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.ProductionOrderId == spp.Id);
                            if (previousSummary == null)
                            {
                                result += await _summaryRepository.InsertAsync(summaryModel);
                            }
                            else
                            {
                                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                            }
                        }
                        result += await _outputRepository.UpdateAsync(bon.Id, bon);
                        //result += await _outputRepository.DeleteAsync(bon.Id);
                    }
                }
            }

            return result;
        }

        public async Task<int> Update(int bonId, InputWarehouseCreateViewModel viewModel)
        {
            var result = 0;
            var bonInput = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any());
            foreach (var bon in bonInput)
            {
                var sppInput = bon.DyeingPrintingAreaInputProductionOrders;
                var sppDeleted = sppInput.Where(x => viewModel.MappedWarehousesProductionOrders.Any(s => x.Id != s.Id));
                foreach (var spp in sppDeleted)
                {
                    var prevOutput = _outputProductionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    foreach (var prevOut in prevOutput)
                    {
                        var prevInput = _inputProductionOrderRepository.ReadAll().Where(x => x.Id == prevOut.DyeingPrintingAreaInputProductionOrderId);
                        foreach (var prevIn in prevInput)
                        {
                            var newBalanceRemain = prevIn.BalanceRemains + prevOut.Balance;
                            var newBalance = prevIn.Balance + prevOut.Balance;
                            prevIn.SetBalanceRemains(newBalanceRemain, "UPDATEWAREHOUSE", "SERVICE");
                            prevIn.SetBalance(newBalance, "UPDATEWAREHOUSE", "SERVICE");
                        }
                        prevOut.SetHasNextAreaDocument(false, "UPDATEWAREHOUSE", "SERVICE");
                        result += await _outputProductionOrderRepository.UpdateAsync(prevOut.Id, prevOut);
                    }
                    result += await _inputProductionOrderRepository.DeleteAsync(spp.Id);
                }
            }
            return result;
        }

        public MemoryStream GenerateExcelAll()
        {
            //var model = await _repository.ReadByIdAsync(id);
            var modelWhere = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var modelAll = modelWhere.Select(s =>
                new
                {
                    SppList = s.DyeingPrintingAreaInputProductionOrders.Select(d => new
                    {
                        BonNo = s.BonNo,
                        NoSPP = d.ProductionOrderNo,
                        QtyOrder = d.ProductionOrderOrderQuantity,
                        Material = d.Construction,
                        Unit = d.Unit,
                        Buyer = d.Buyer,
                        Warna = d.Color,
                        Motif = d.Motif,
                        Jenis = d.PackagingType,
                        Grade = d.Grade,
                        QtyPack = d.PackagingQty,
                        Pack = d.PackagingUnit,
                        Qty = d.Balance,
                        SAT = d.UomUnit
                    })
                });

            //var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            var query = modelAll.SelectMany(s => s.SppList);

            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"NoSPP","NO SP" },
                {"QtyOrder","QTY ORDER" },
                {"Material","MATERIAL"},
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"Jenis","JENIS"},
                {"Grade","GRADE"},
                {"QtyPack","QTY Pack"},
                {"Pack","PACK"},
                {"Qty","QTY" },
                {"SAT","SAT" },
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
                        valueClass = searchValue == null ? "" : searchValue.ToString();
                    }
                    else
                    {
                        valueClass = "";
                    }
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("PENERIMAAN GUDANG JADI");

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

    }

    public class PackingComparer : IEqualityComparer<ProductionOrderItemListDetailViewModel>
    {
        public bool Equals(ProductionOrderItemListDetailViewModel x, ProductionOrderItemListDetailViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ProductionOrderItemListDetailViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
