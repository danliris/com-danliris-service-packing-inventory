using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.TransitInput
{
    public class TransitInputService : ITransitInputService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        private readonly IDyeingPrintingAreaMovementHistoryRepository _historyRepository;

        public TransitInputService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _historyRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementHistoryRepository>();

        }

        private TransitInputViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new TransitInputViewModel()
            {
                Active = model.Active,
                Area = model.Area,
                Balance = model.Balance,
                BonNo = model.BonNo,
                CartNo = model.CartNo,
                Color = model.Color,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Material = model.MaterialName,
                MaterialConstruction = model.MaterialConstructionName,
                MaterialWidth = model.MaterialWidth,
                Motif = model.Motif,
                ProductionOrderNo = model.ProductionOrderNo,
                Shift = model.Shift,
                Unit = model.UnitName,
                UOMUnit = model.UOMUnit,
                Remark = model.Remark,
                InspectionAreaId = model.Id
            };

            return vm;
        }

        public Task<int> Create(TransitInputViewModel viewModel)
        {
            DyeingPrintingAreaMovementHistoryModel history = new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, viewModel.Area, viewModel.Shift, AreaEnum.TRANSIT);

            return _repository.InsertFromTransitAsync(viewModel.InspectionAreaId, viewModel.Shift, DateTimeOffset.UtcNow, viewModel.Area, viewModel.Remark, history);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteFromTransitAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.TRANSIT);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaMovementModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                Balance = s.Balance,
                BonNo = s.BonNo,
                CartNo = s.CartNo,
                Color = s.Color,
                Date = s.Date,
                Id = s.Id,
                MaterialConstructionName = s.MaterialConstructionName,
                MaterialName = s.MaterialName,
                MaterialWidth = s.MaterialWidth,
                MeterLength = s.MeterLength,
                ProductionOrderNo = s.ProductionOrderNo,
                Shift = s.Shift,
                UnitName = s.UnitName,
                Motif = s.Motif,
                YardsLength = s.YardsLength,
                Remark = s.Remark,
                ProductionOrderId = s.ProductionOrderId,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<TransitInputViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            TransitInputViewModel vm = MapToViewModel(model);

            return vm;
        }

        public async Task<int> Update(int id, TransitInputViewModel viewModel)
        {
            int result = await _repository.UpdateFromTransitAsync(id, viewModel.Shift, viewModel.Remark);
            result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.TRANSIT, DateTimeOffset.UtcNow, viewModel.Shift);

            return result;
        }
    }
}
