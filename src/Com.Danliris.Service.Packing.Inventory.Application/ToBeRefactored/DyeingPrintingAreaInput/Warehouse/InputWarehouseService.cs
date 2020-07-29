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
                WarehousesProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(item => item.ProductionOrderId).Select(item => new InputWarehouseProductionOrderDetailViewModel()
                {
                    ProductionOrderId = item.Key,
                    ProductionOrderNo = item.First().ProductionOrderNo,
                    ProductionOrderType = item.First().ProductionOrderType,
                    ProductionOrderOrderQuantity = item.First().ProductionOrderOrderQuantity,
                    ProductionOrderItems = item.Select(s => new ProductionOrderItemListDetailViewModel()
                    {
                        Active = s.Active,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        Id = s.Id,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedUtc = s.LastModifiedUtc,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = s.ProductionOrderId,
                            No = s.ProductionOrderNo,
                            OrderQuantity = s.ProductionOrderOrderQuantity,
                            Type = s.ProductionOrderType
                        },
                        MaterialWidth = s.MaterialWidth,
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
                        Area = s.Area,
                        HasOutputDocument = s.HasOutputDocument,
                        DyeingPrintingAreaInputId = s.DyeingPrintingAreaInputId,
                        Qty = s.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(s.Balance), s.PackagingQty)
                    }).Distinct(new PackingComparer()).ToList()
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
                                                                                                                                                                s.Id,
                                                                                                                                                                s.Remark,
                                                                                                                                                                s.Balance,
                                                                                                                                                                s.MaterialProduct.Id,
                                                                                                                                                                s.MaterialProduct.Name,
                                                                                                                                                                s.MaterialConstruction.Id,
                                                                                                                                                                s.MaterialConstruction.Name,
                                                                                                                                                                s.MaterialWidth,
                                                                                                                                                                s.ProcessType.Id,
                                                                                                                                                                s.ProcessType.Name,
                                                                                                                                                                s.YarnMaterial.Id,
                                                                                                                                                                s.YarnMaterial.Name))
                                                                                                                                                                .ToList());
            //Insert to Input Repository
            result = await _inputRepository.InsertAsync(model);

            foreach (var item in viewModel.MappedWarehousesProductionOrders)
            {
                var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == item.Id);
                result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance, item.PackagingQty);
                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No, item.CartNo,
                    item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, itemModel.Id, item.ProductionOrder.Type, item.Grade, null, item.PackagingType);


                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);
            }

            //Update from Output Only (Parent) Flag for HasNextAreaDocument == True (Because Not All Production Order Checked from UI)
            //List<int> listOfDyeingPrintingAreaIds = viewModel.MappedWarehousesProductionOrders.Select(o => o.OutputId).Distinct().ToList();
            //foreach (var areaId in listOfDyeingPrintingAreaIds)
            //{
            //    result += await _outputRepository.UpdateFromInputNextAreaFlagParentOnlyAsync(areaId, true);
            //}

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
                                                                                           productionOrder.Id,
                                                                                           productionOrder.Remark,
                                                                                           productionOrder.Balance,
                                                                                           productionOrder.MaterialProduct.Id,
                                                                                           productionOrder.MaterialProduct.Name, 
                                                                                           productionOrder.MaterialConstruction.Id,
                                                                                           productionOrder.MaterialConstruction.Name,
                                                                                           productionOrder.MaterialWidth,
                                                                                           productionOrder.ProcessType.Id,
                                                                                           productionOrder.ProcessType.Name,
                                                                                           productionOrder.YarnMaterial.Id,
                                                                                           productionOrder.YarnMaterial.Name)
                {
                    DyeingPrintingAreaInputId = dyeingPrintingAreaInputId,
                };

                //Insert to Input Production Order Repository
                result += await _inputProductionOrderRepository.InsertAsync(productionOrderModel);

                //Mapping to DyeingPrintingAreaMovementModel
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, productionOrder.Id, bonNo, productionOrder.ProductionOrder.Id,
                    productionOrder.ProductionOrder.No, productionOrder.CartNo, productionOrder.Buyer, productionOrder.Construction, productionOrder.Unit, productionOrder.Color,
                    productionOrder.Motif, productionOrder.UomUnit, productionOrder.Balance, productionOrderModel.Id, productionOrder.ProductionOrder.Type, productionOrder.Grade, null, productionOrder.PackagingType);

                //Insert to Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(productionOrder.DyeingPrintingAreaInputProductionOrderId, productionOrder.Balance, productionOrder.PackagingQty);
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
                ProductionOrderItems = s.Select(p => new OutputPreWarehouseItemListViewModel()
                {

                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    MaterialWidth = p.MaterialWidth,
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
            else if (area == SHIPPING)
            {
                return string.Format("{0}.{1}.{2}", SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
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

            var groupedProductionOrders = viewModel.MappedWarehousesProductionOrders.GroupBy(s => s.Area);
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
                                                             viewModel.MappedWarehousesProductionOrders.Select(s =>
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
                                                                                                                s.Id,
                                                                                                                s.BuyerId,
                                                                                                                s.MaterialProduct.Id,
                                                                                                                s.MaterialProduct.Name,
                                                                                                                s.MaterialConstruction.Id,
                                                                                                                s.MaterialConstruction.Name,
                                                                                                                s.MaterialWidth,
                                                                                                                s.ProcessType.Id,
                                                                                                                s.ProcessType.Name,
                                                                                                                s.YarnMaterial.Id,
                                                                                                                s.YarnMaterial.Name)).ToList());

                    result = await _inputRepository.InsertAsync(model);

                    //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                    result += await _outputProductionOrderRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);

                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id);
                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance, detail.PackagingQty);

                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, itemModel.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, itemModel.Id, detail.ProductionOrder.Type, detail.Grade, null, detail.PackagingType);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, itemModel.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
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
                                                                                        detail.Id,
                                                                                        detail.BuyerId,
                                                                                        detail.MaterialProduct.Id,
                                                                                        detail.MaterialProduct.Name,
                                                                                        detail.MaterialConstruction.Id,
                                                                                        detail.MaterialConstruction.Name,
                                                                                        detail.MaterialWidth,
                                                                                        detail.ProcessType.Id,
                                                                                        detail.ProcessType.Name,
                                                                                        detail.YarnMaterial.Id,
                                                                                        detail.YarnMaterial.Name);

                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _inputProductionOrderRepository.InsertAsync(modelItem);

                        result += await _inputProductionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance, detail.PackagingQty);
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, modelItem.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == SHIPPING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, modelItem.Id, detail.ProductionOrder.Type, detail.Grade, null, detail.PackagingType);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, modelItem.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
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

                    foreach (var item in modelBon.DyeingPrintingAreaInputProductionOrders)
                    {
                        var movementModel = new DyeingPrintingAreaMovementModel(modelBon.Date, modelBon.Area, TYPE, modelBon.Id, modelBon.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                                item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Grade,
                                null, item.PackagingType);
                        result += await _movementRepository.InsertAsync(movementModel);
                    }

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
                                modifInputSpp.SetBalance(newBalance, "WAREHOUSESERVICE", "SERVICE");

                                modifInputSpp.SetHasOutputDocument(false, "WAREHOUSESERVICE", "SERVICE");
                                result += await _inputProductionOrderRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
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
                            var newBalance = prevIn.Balance + prevOut.Balance;
                            prevIn.SetBalance(newBalance, "UPDATEWAREHOUSE", "SERVICE");
                        }
                        prevOut.SetHasNextAreaDocument(false, "UPDATEWAREHOUSE", "SERVICE");
                        result += await _outputProductionOrderRepository.UpdateAsync(prevOut.Id, prevOut);
                    }
                    result += await _inputProductionOrderRepository.DeleteAsync(spp.Id);

                    var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, bon.Area, TYPE, bon.Id, bon.BonNo, spp.ProductionOrderId, spp.ProductionOrderNo,
                               spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance * -1, spp.Id, spp.ProductionOrderType, spp.Grade,
                               null, spp.PackagingType);
                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            return result;
        }

        public MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var warehouseData = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));

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
            //var model = await _repository.ReadByIdAsync(id);
            //var modelWhere = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var modelAll = warehouseData.Select(s =>
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
            //var query = modelAll.SelectMany(s => s.SppList);
            var query = modelAll.Select(s => s.SppList);


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
            foreach (var items in query)
            {
                foreach (var item in items)
                {
                    List<string> data = new List<string>();
                    foreach (DataColumn column in dt.Columns)
                    {
                        var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName && column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima");
                        string valueClass = "";
                        //if (searchMappedClass != null && searchMappedClass != null && searchMappedClass.FirstOrDefault().Key != null)
                        //{
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        var searchValue = searchProperty.GetValue(item, null);
                        valueClass = searchValue == null ? "" : searchValue.ToString();
                        //}
                        //else
                        //{
                        //    valueClass = "";
                        //}
                        data.Add(valueClass);
                    }
                    dt.Rows.Add(data.ToArray());
                }
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
