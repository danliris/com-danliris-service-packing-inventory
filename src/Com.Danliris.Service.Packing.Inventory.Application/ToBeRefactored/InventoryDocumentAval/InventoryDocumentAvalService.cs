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

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval
{
    public class InventoryDocumentAvalService : IInventoryDocumentAvalService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        private readonly IDyeingPrintingAreaMovementHistoryRepository _historyRepository;

        public InventoryDocumentAvalService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _historyRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementHistoryRepository>();
        }

        private InventoryDocumentAvalViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new InventoryDocumentAvalViewModel()
            {
                Id = model.Id,
                //BonNo = model.BonNo,
                Shift = model.Shift,
                UOMUnit = model.UOMUnit,
                ProductionOrderQuantity = model.ProductionOrderQuantity,
                QtyKg = model.QtyKg
            };

            return vm;
        }

        public async Task<int> Create(int id, InventoryDocumentAvalViewModel viewModel)
        {

            var model = new DyeingPrintingAreaMovementModel(viewModel.Area, viewModel.Shift, viewModel.UOMUnit, viewModel.ProductionOrderQuantity, viewModel.QtyKg, new List<DyeingPrintingAreaMovementHistoryModel>());

            int result = await _repository.UpdateAsync(id, model);
            result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.AVAL, DateTimeOffset.UtcNow, viewModel.Shift);

            return result;
        }

        //public async Task<int> Update(int id, InventoryDocumentAvalViewModel viewModel)
        //{

        //    var model = new DyeingPrintingAreaMovementModel(viewModel.Area, viewModel.BonNo, viewModel.Shift, viewModel.UOMUnit, viewModel.ProductionOrderQuantity, viewModel.QtyKg, new List<DyeingPrintingAreaMovementHistoryModel>());

        //    int result = await _repository.UpdateAsync(id, model);
        //    //result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.AVAL, viewModel.Date, viewModel.Shift);

        //    return result;
        //}

        public async Task<InventoryDocumentAvalViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InventoryDocumentAvalViewModel vm = MapToViewModel(model);

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
