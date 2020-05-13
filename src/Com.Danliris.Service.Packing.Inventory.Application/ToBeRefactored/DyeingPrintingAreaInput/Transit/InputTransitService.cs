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
                    Grade = s.Grade,
                    IsChecked = s.IsChecked,
                    PackingInstruction = s.PackingInstruction,
                    Remark = s.Remark,
                    Status = s.Status,
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

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        public async Task<int> Create(InputTransitViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == TRANSIT && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if(model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == TRANSIT && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.TransitProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.Remark, s.Grade, s.Status)).ToList());

                result = await _repository.InsertAsync(model);

                //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);

                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.TransitProductionOrders.Select(s => s.Id), true);
                foreach (var item in viewModel.TransitProductionOrders)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

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
                foreach(var item in viewModel.TransitProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                     item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, false, item.Remark, item.Grade, item.Status);
                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    result += await _SPPRepository.InsertAsync(modelItem);
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
                result += await _outputSPPRepository.UpdateFromInputAsync(viewModel.TransitProductionOrders.Select(s => s.Id), true);
            }

            

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == TRANSIT && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                    Grade = d.Grade,
                    Id = d.Id,
                    Unit = d.Unit,
                    Remark = d.Remark,
                    Status = d.Status,
                    IsChecked = d.IsChecked,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit
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
            var query = _outputRepository.ReadAll().Where(s => s.DestinationArea == TRANSIT && !s.HasNextAreaDocument);
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
                    Buyer = d.Buyer,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Id = d.Id,
                    Motif = d.Motif,
                    Balance = d.Balance,
                    PackingInstruction = d.PackingInstruction,
                    Remark = d.Remark,
                    Status = d.Status,
                    Grade = d.Grade,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType,
                        OrderQuantity = d.ProductionOrderOrderQuantity
                    },
                    Unit = d.Unit,
                    UomUnit = d.UomUnit
                }).ToList()
            });

            return new ListResult<PreTransitIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public List<OutputPreTransitProductionOrderViewModel> GetOutputPreTransitProductionOrders()
        {
            var productionOrders = _outputSPPRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.DestinationArea == TRANSIT && !s.HasNextAreaDocument);
            var data = productionOrders.Select(d => new OutputPreTransitProductionOrderViewModel()
            {
                Buyer = d.Buyer,
                CartNo = d.CartNo,
                Color = d.Color,
                Construction = d.Construction,
                Id = d.Id,
                Motif = d.Motif,
                Balance = d.Balance,
                PackingInstruction = d.PackingInstruction,
                Remark = d.Remark,
                Status = d.Status,
                Grade = d.Grade,
                ProductionOrder = new ProductionOrder()
                {
                    Id = d.ProductionOrderId,
                    No = d.ProductionOrderNo,
                    Type = d.ProductionOrderType,
                    OrderQuantity = d.ProductionOrderOrderQuantity
                },
                Unit = d.Unit,
                UomUnit = d.UomUnit,
                OutputId = d.DyeingPrintingAreaOutputId
            });

            return data.ToList();
        }
    }
}
