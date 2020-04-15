using Microsoft.Extensions.DependencyInjection;
using System;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput
{
    public class AvalInputService : IAvalInputService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        private readonly IDyeingPrintingAreaMovementHistoryRepository _historyRepository;

        public AvalInputService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _historyRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementHistoryRepository>();
        }

        private AvalInputViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new AvalInputViewModel()
            {
                Id = model.Id,
                BonNo = model.BonNo,
                CartNo = model.CartNo,
                Unit = model.UnitName,
                Area = model.Area,
                ProductionOrderType = model.ProductionOrderType,
                Shift = model.Shift,
                UOMUnit = model.UOMUnit,
                ProductionOrderQuantity = model.ProductionOrderQuantity,
                QtyKg = model.QtyKg
            };

            return vm;
        }

        public Task<int> Create(AvalInputViewModel viewModel)
        {
            DyeingPrintingAreaMovementHistoryModel history = new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, viewModel.Area, viewModel.Shift, AreaEnum.AVAL);

            return _repository.InsertFromAvalAsync(viewModel.Id, viewModel.Area, viewModel.Shift, viewModel.UOMUnit, viewModel.ProductionOrderQuantity, viewModel.QtyKg, history);
        }

        public async Task<int> Update(int id, AvalInputViewModel viewModel)
        {
            int result = await _repository.UpdateFromAvalAsync(id, viewModel.Area, viewModel.Shift, viewModel.UOMUnit, viewModel.ProductionOrderQuantity, viewModel.QtyKg);
            result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.AVAL, DateTimeOffset.UtcNow, viewModel.Shift);

            return result;
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteFromAvalAsync(id);
        }

        public async Task<AvalInputViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            AvalInputViewModel vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.AVAL);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaMovementModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(entity => new IndexViewModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                BonNo = entity.BonNo,
                Shift = entity.Shift,
                CartNo = entity.CartNo,
                UnitId = entity.UnitId,
                UnitCode = entity.UnitCode,
                UnitName = entity.UnitName,
                Area = entity.Area,
                ProductionOrderType = entity.ProductionOrderType,
                UOMUnit = entity.UOMUnit,
                ProductionOrderQuantity = entity.ProductionOrderQuantity,
                QtyKg = entity.QtyKg
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
