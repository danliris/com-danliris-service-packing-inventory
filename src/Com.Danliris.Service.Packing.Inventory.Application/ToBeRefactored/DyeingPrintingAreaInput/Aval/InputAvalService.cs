using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalService : IInputAvalService
    {
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;

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

        public InputAvalService(IServiceProvider serviceProvider)
        {
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
        }

        private InputAvalViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputAvalViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
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
                AvalItems = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputAvalItemViewModel()
                {
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
                    LastModifiedBy = s.LastModifiedBy,

                    Id = s.Id,
                    AvalType = s.AvalType,
                    AvalCartNo = s.AvalCartNo,
                    AvalUomUnit = s.UomUnit,
                    AvalQuantity = s.Balance,
                    AvalQuantityKg = s.AvalQuantityKg,
                    HasOutputDocument = s.HasOutputDocument,
                    IsChecked = s.IsChecked
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        public async Task<int> Create(InputAvalViewModel viewModel)
        {
            int result = 0;

            //Count Existing Document in Aval by Year
            int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL && s.CreatedUtc.Year == viewModel.Date.Year);

            //Generate Bon
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

            //Instantiate Input Model
            var model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                         viewModel.Area,
                                                         viewModel.Shift,
                                                         bonNo,
                                                         viewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                                                                                    s.AvalType,
                                                                                                                                                    s.AvalCartNo,
                                                                                                                                                    s.AvalUomUnit,
                                                                                                                                                    s.AvalQuantity,
                                                                                                                                                    s.AvalQuantityKg,
                                                                                                                                                    false))
                                                                                       .ToList());

            //Create New Row in Input and ProductionOrdersInput in Each Repository 
            result = await _inputRepository.InsertAsync(model);

            //Movement from Previous Area to Aval Area
            foreach (var dyeingPrintingMovement in viewModel.DyeingPrintingMovementIds)
            {
                //Flag for Input on DyeingPrintingAreaOutputMovement
                result += await _outputRepository.UpdateFromInputAsync(dyeingPrintingMovement.DyeingPrintingAreaMovementId, true);

                foreach (var productionOrderId in dyeingPrintingMovement.ProductionOrderIds)
                {
                    //Get Previous Summary
                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == dyeingPrintingMovement.DyeingPrintingAreaMovementId &&
                                                                                           s.ProductionOrderId == productionOrderId);

                    //Update Previous Summary
                    result += await _summaryRepository.UpdateToAvalAsync(previousSummary, viewModel.Date, viewModel.Area, TYPE);
                }
            }

            //Summed Up Balance (or Quantity in Aval)
            var groupedProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(o => new { o.AvalType, o.AvalCartNo, o.UomUnit })
                                                                             .Select(i => new { i.Key.AvalType, i.Key.AvalCartNo, i.Key.UomUnit, Quantity = i.Sum(s => s.Balance) });

            foreach (var productionOrder in groupedProductionOrders)
            {
                //Instantiate Movement Model
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        model.Id,
                                                                        productionOrder.AvalCartNo,
                                                                        productionOrder.UomUnit,
                                                                        productionOrder.Quantity);

                //Create New Row in Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                //Instantiate Summary Model
                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                                                                      viewModel.Area,
                                                                      TYPE,
                                                                      model.Id,
                                                                      productionOrder.AvalCartNo,
                                                                      productionOrder.UomUnit,
                                                                      productionOrder.Quantity);

                //Create New Row in Summary Repository
                result += await _summaryRepository.InsertAsync(summaryModel);
            }

            return result;
        }

        //Already INPUT AVAL (Repository INPUT) for List in Input Aval List
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                AvalProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputAvalItemViewModel()
                {
                    Id = d.Id,
                    AvalType = d.AvalType,
                    AvalCartNo = d.AvalCartNo,
                    AvalUomUnit = d.UomUnit,
                    AvalQuantity = d.Balance,
                    AvalQuantityKg = d.AvalQuantityKg,
                    HasOutputDocument = d.HasOutputDocument,
                    IsChecked = d.IsChecked
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputAvalViewModel> ReadById(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        //OUT from IM, Not INPUT to AVAL yet (Repository OUTPUT) => for Loader in Aval Input
        public ListResult<PreAvalIndexViewModel> ReadOutputPreAval(DateTimeOffset searchDate, 
                                                                   string shift, 
                                                                   int page, 
                                                                   int size, 
                                                                   string filter, 
                                                                   string order, 
                                                                   string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Date <= searchDate && s.Shift == shift && s.DestinationArea == GUDANGAVAL && !s.HasNextAreaDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new PreAvalIndexViewModel()
            {
                Id = s.Id,
                Date = s.Date,
                Area = s.Area,
                Shift = s.Shift,
                BonNo = s.BonNo,
                HasNextAreaDocument = s.HasNextAreaDocument,
                DestinationArea = s.DestinationArea,
                PreAvalProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPreAvalProductionOrderViewModel()
                {
                    Id = d.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    CartNo = d.CartNo,
                    Buyer = d.Buyer,
                    Construction = d.Construction,
                    Unit = d.Unit,
                    Color = d.Color,
                    Motif = d.Motif,
                    UomUnit = d.UomUnit,
                    Remark = d.Remark,
                    Grade = d.Grade,
                    Status = d.Status,
                    Balance = d.Balance,
                    PackingInstruction = d.PackingInstruction,
                    AvalALength = d.AvalALength,
                    AvalBLength = d.AvalBLength,
                    AvalConnectionLength = d.AvalConnectionLength
                }).ToList()
            });

            return new ListResult<PreAvalIndexViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
