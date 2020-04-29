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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalService : IInputAvalService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
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
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
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
                BonNo = model.BonNo,
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
                AvalProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputAvalProductionOrderViewModel()
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
                    UomUnit = s.UomUnit,
                    Quantity = s.Balance,
                    QuantityKg = s.QuantityKg,
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
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL && s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
            var model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                         viewModel.Area,
                                                         viewModel.Shift,
                                                         bonNo,
                                                         viewModel.AvalProductionOrders.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                                                                                    s.AvalType,
                                                                                                                                                    s.AvalCartNo,
                                                                                                                                                    s.UomUnit,
                                                                                                                                                    s.Quantity,
                                                                                                                                                    s.QuantityKg,
                                                                                                                                                    false)).ToList());

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {

                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        model.Id,
                                                                        model.BonNo,
                                                                        item.ProductionOrderId,
                                                                        item.ProductionOrderNo,
                                                                        item.CartNo,
                                                                        item.Buyer,
                                                                        item.Construction,
                                                                        item.Unit,
                                                                        item.Color,
                                                                        item.Motif,
                                                                        item.UomUnit,
                                                                        item.Balance);

                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == viewModel.OutputInspectionMaterialId &&
                                                                                       s.DyeingPrintingAreaDocumentBonNo == viewModel.BonNo);

                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                                                                      viewModel.Area,
                                                                      TYPE,
                                                                      model.Id,
                                                                      model.BonNo,
                                                                      item.ProductionOrderId,
                                                                      item.ProductionOrderNo,
                                                                      item.CartNo,
                                                                      item.Buyer,
                                                                      item.Construction,
                                                                      item.Unit,
                                                                      item.Color,
                                                                      item.Motif,
                                                                      item.UomUnit,
                                                                      item.Balance);


                result = await _repository.InsertAsync(model);
                result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputInspectionMaterialId, true);
                result += await _movementRepository.InsertAsync(movementModel);
                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                AvalProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputAvalProductionOrderViewModel()
                {
                    Id = d.Id,
                    AvalType = d.AvalType,
                    AvalCartNo = d.AvalCartNo,
                    UomUnit = d.UomUnit,
                    Quantity = d.Balance,
                    QuantityKg = d.QuantityKg,
                    HasOutputDocument = d.HasOutputDocument,
                    IsChecked = d.IsChecked
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputAvalViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }
    }
}
