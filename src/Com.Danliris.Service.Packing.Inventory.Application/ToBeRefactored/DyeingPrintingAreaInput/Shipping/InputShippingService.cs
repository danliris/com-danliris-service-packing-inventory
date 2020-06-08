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
                    PackingType = s.PackagingType,
                    Qty = s.Balance,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == PACKING)
            {

                return string.Format("{0}.{1}.{2}", PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == GUDANGAVAL)
            {

                return string.Format("{0}.{1}.{2}", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (area == GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}", GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {

                return string.Format("{0}.{1}.{2}", SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        public async Task<int> Create(InputShippingViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == SHIPPING && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.Area);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.ShippingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.DeliveryOrder.Id, s.DeliveryOrder.No, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type,
                     s.ProductionOrder.OrderQuantity, s.Buyer, s.Construction, s.PackingType, s.Color, s.Motif, s.Grade, s.QtyPacking, s.Packing, s.Qty, s.UomUnit, false, s.Qty, s.Unit, s.BuyerId)).ToList());

                result = await _repository.InsertAsync(model);

                //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);
                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.ShippingProductionOrders.Select(s => s.Id), true);
                foreach (var item in viewModel.ShippingProductionOrders)
                {
                    result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Qty);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

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
                foreach (var item in viewModel.ShippingProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.DeliveryOrder.Id, item.DeliveryOrder.No, item.ProductionOrder.Id,
                        item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.Buyer, item.Construction, item.PackingType, item.Color, item.Motif,
                        item.Grade, item.QtyPacking, item.Packing, item.Qty, item.UomUnit, false, item.Qty, item.Unit, item.BuyerId);
                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty);

                    result += await _productionOrderRepository.InsertAsync(modelItem);
                    result += await _movementRepository.InsertAsync(movementModel);
                    result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Qty);
                    if (previousSummary == null)
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                }
                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.ShippingProductionOrders.Select(s => s.Id), true);
            }



            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == SHIPPING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                    Qty = d.Balance,
                    QtyPacking = d.PackagingQty,
                    Packing = d.PackagingUnit,
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
                    Unit = d.Unit,
                    UomUnit = d.UomUnit
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
            var query = _outputRepository.ReadAll().Where(s => s.DestinationArea == SHIPPING && !s.HasNextAreaDocument);
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
                HasNextAreaDocument = s.HasNextAreaDocument,
                //PreShippingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPreShippingProductionOrderViewModel()
                //{
                //    Buyer = d.Buyer,
                //    CartNo = d.CartNo,
                //    Color = d.Color,
                //    Construction = d.Construction,
                //    Id = d.Id,
                //    Motif = d.Motif,
                //    Grade = d.Grade,
                //    ProductionOrder = new ProductionOrder()
                //    {
                //        Id = d.ProductionOrderId,
                //        No = d.ProductionOrderNo
                //    },
                //    Unit = d.Unit,
                //    UomUnit = d.UomUnit
                //}).ToList()
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
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                PackingType = s.PackagingType,
                Grade = s.Grade,
                QtyPacking = s.PackagingQty,
                Packing = s.PackagingUnit,
                Qty = s.Balance,
                UomUnit = s.UomUnit
            });

            return new ListResult<InputShippingProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public List<OutputPreShippingProductionOrderViewModel> GetOutputPreShippingProductionOrders()
        {
            var productionOrders = _outputSPPRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == SHIPPING && !s.HasNextAreaDocument);
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
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                Unit = s.Unit,
                CartNo = s.CartNo,
                Area = s.Area,
                PackingType = s.PackagingType,
                Grade = s.Grade,
                QtyPacking = s.PackagingQty,
                Packing = s.PackagingUnit,
                PackingInstruction = s.PackingInstruction,
                Qty = s.Balance,
                UomUnit = s.UomUnit,
                OutputId = s.DyeingPrintingAreaOutputId,
                DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId
            });

            return data.ToList();
        }

        public async Task<int> Reject(InputShippingViewModel viewModel)
        {
            int result = 0;
            var groupedProductionOrders = viewModel.ShippingProductionOrders.GroupBy(s => s.Area);

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
                                        s.Qty, false, s.Packing, s.PackingType, s.QtyPacking, s.Grade, s.ProductionOrder.OrderQuantity, s.BuyerId)).ToList());

                    result = await _repository.InsertAsync(model);
                    result += await _outputSPPRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);
                    foreach (var detail in item)
                    {
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Qty);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Qty);

                        var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.OutputId && s.ProductionOrderId == detail.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Qty);

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

                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(item.Key, detail.ProductionOrder.Id, detail.ProductionOrder.No, detail.ProductionOrder.Type,
                                        detail.PackingInstruction, detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit,
                                        detail.Qty, false, detail.Packing, detail.PackingType, detail.QtyPacking, detail.Grade, detail.ProductionOrder.OrderQuantity, detail.BuyerId);
                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Qty);

                        var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.OutputId && s.ProductionOrderId == detail.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Qty);

                        result += await _productionOrderRepository.InsertAsync(modelItem);
                        result += await _movementRepository.InsertAsync(movementModel);
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Qty);
                        if (previousSummary == null)
                        {

                            result += await _summaryRepository.InsertAsync(summaryModel);
                        }
                        else
                        {

                            result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                        }
                    }
                    result += await _outputSPPRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);
                }
            }

            return result;
        }
    }
}
