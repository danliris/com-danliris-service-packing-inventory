using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses
{
    public class InputWarehousesService : IInputWarehousesService
    {
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
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

        public InputWarehousesService(IServiceProvider serviceProvider)
        {
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private InputWarehousesViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputWarehousesViewModel()
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
                WarehousesProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputWarehousesProductionOrdersViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
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
                    Motif = s.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    ProductionOrderNo = s.ProductionOrderNo,
                    QtyOrder = s.ProductionOrderOrderQuantity
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        public async Task<int> Create(InputWarehousesViewModel viewModel)
        {
            int result = 0;

            //bool hasBonNo = _inputRepository.ReadAll().Any(o => o.Date == viewModel.Date &&
            //                                                    o.Shift == viewModel.Shift &&
            //                                                    o.Group == viewModel.Group &&
            //                                                    o.Area == viewModel.Area);

            var model = _inputRepository.GetDbSet().Include(s => s.DyeingPrintingAreaInputProductionOrders)
                                                   .FirstOrDefault(s => s.Area == GUDANGJADI && 
                                                                        s.Date.Date == viewModel.Date.Date && 
                                                                        s.Shift == viewModel.Shift);

            if (model != null)
            {
                //var modelToUpdate = _inputRepository.ReadAll().Where(o => o.Date == viewModel.Date &&
                //                                                          o.Shift == viewModel.Shift &&
                //                                                          o.Group == viewModel.Group)
                //                                              .FirstOrDefault();

                result = await UpdateExistingWarehouse(viewModel, model.Id, model.BonNo);
            }
            else
            {
                result = await InsertNewWarehouse(viewModel);
            }

            return result;
        }

        public async Task<int> InsertNewWarehouse(InputWarehousesViewModel viewModel)
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
                                                         viewModel.WarehousesProductionOrders.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
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
                                                                                                                                                          s.QtyOrder))
                                                                                             .ToList());
            //Insert to Input Repository
            result = await _inputRepository.InsertAsync(model);

            foreach (var item in viewModel.WarehousesProductionOrders)
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

                //Update Previous Summary with Summary Model Created Before
                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
            }

            //Update from Output Only (Parent) Flag for HasNextAreaDocument == True (Because Not All Production Order Checked from UI)
            List<int> listOfDyeingPrintingAreaIds = viewModel.WarehousesProductionOrders.Select(o => o.OutputId).ToList();
            foreach (var areaId in listOfDyeingPrintingAreaIds)
            {
                result += await _outputRepository.UpdateFromInputNextAreaFlagParentOnlyAsync(areaId, true);
            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            List<int> listOfOutputProductionOrderIds = viewModel.WarehousesProductionOrders.Select(o => o.Id).ToList();
            foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            {
                result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true);
            }

            return result;
        }

        public async Task<int> UpdateExistingWarehouse(InputWarehousesViewModel viewModel, int dyeingPrintingAreaInputId, string bonNo)
        {
            int result = 0;

            foreach (var productionOrder in viewModel.WarehousesProductionOrders)
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
                                                                                           productionOrder.QtyOrder)
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

                //Update Previous Summary with Summary Model Created Before
                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
            }

            //Update from Output Production Order (Child) Flag for HasNextAreaDocument == True
            List<int> listOfOutputProductionOrderIds = viewModel.WarehousesProductionOrders.Select(o => o.Id).ToList();
            foreach (var outputProductionOrderId in listOfOutputProductionOrderIds)
            {
                result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(outputProductionOrderId, true);
            }

            return result;
        }

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
                //WarehousesProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputWarehousesProductionOrdersViewModel()
                //{
                //    Balance = d.Balance,
                //    Buyer = d.Buyer,
                //    CartNo = d.CartNo,
                //    Color = d.Color,
                //    Construction = d.Construction,
                //    HasOutputDocument = d.HasOutputDocument,
                //    Motif = d.Motif,
                //    ProductionOrder = new ProductionOrder()
                //    {
                //        Id = d.ProductionOrderId,
                //        No = d.ProductionOrderNo,
                //        Type = d.ProductionOrderType
                //    },
                //    Grade = d.Grade,
                //    Id = d.Id,
                //    Unit = d.Unit,
                //    //IsChecked = d.IsChecked,
                //    PackingInstruction = d.PackingInstruction,
                //    UomUnit = d.UomUnit,
                //    Material = d.Construction,
                //    PackagingQty = d.PackagingQty,
                //    PackagingType = d.PackagingType,
                //    PackagingUnit = d.PackagingUnit,
                //    ProductionOrderNo = d.ProductionOrderNo,
                //    QtyOrder = d.ProductionOrderOrderQuantity
                //}).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputWarehousesViewModel> ReadById(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InputWarehousesViewModel vm = MapToViewModel(model);

            return vm;
        }

        public List<OutputPreWarehouseIndexViewModel> GetOutputPreWarehouseProductionOrders()
        {
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.DestinationArea == GUDANGJADI &&
                                                                    !s.HasNextAreaDocument);

            var data = query.Select(s => new OutputPreWarehouseIndexViewModel()
            {
                Id = s.Id,
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                Color = s.Color,
                Construction = s.Construction,
                //HasNextAreaDocument = s.HasNextAreaDocument,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType
                },
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                PackagingUnit = s.PackagingUnit,
                PackagingType = s.PackagingType,
                PackagingQty = s.PackagingQty,
                ProductionOrderNo = s.ProductionOrderNo,
                QtyOrder = s.ProductionOrderOrderQuantity,
                OutputId = s.DyeingPrintingAreaOutputId
            });

            return data.ToList();
        }
    }
}
