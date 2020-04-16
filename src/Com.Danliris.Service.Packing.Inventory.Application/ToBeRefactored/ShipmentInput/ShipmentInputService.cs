using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput
{
    public class ShipmentInputService : IShipmentInputService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        private readonly IDyeingPrintingAreaMovementHistoryRepository _historyRepository;

        public ShipmentInputService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _historyRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementHistoryRepository>();
        }

        private ShipmentInputViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new ShipmentInputViewModel()
            {
                Active = model.Active,
                Area = model.Area,
                BonNo = model.BonNo,
                BuyerName = model.Buyer,
                Color = model.Color,
                Construction = model.Construction,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                DeliveryOrderSales = new DeliveryOrderSales()
                {
                    Id = model.DeliveryOrderSalesId,
                    No = model.DeliveryOrderSalesNo
                },
                Grade = model.Grade,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Motif = model.Motif,
                PreShipmentAreaId = model.Id,
                Shift = model.Shift,
                ProductionOrderNo = model.ProductionOrderNo,
                UomUnit = model.UOMUnit
            };

            return vm;
        }


        public Task<int> Create(ShipmentInputViewModel viewModel)
        {
            DyeingPrintingAreaMovementHistoryModel history = new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, viewModel.Area, viewModel.Shift, AreaEnum.SHIP);

            return _repository.InsertFromShipmentAsync(viewModel.PreShipmentAreaId, viewModel.Area, DateTimeOffset.UtcNow, viewModel.DeliveryOrderSales.Id, viewModel.DeliveryOrderSales.No, history);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteFromShipmentAsync(id);
        }

        public ListResult<PreShipmentIndexViewModel> LoaderPreShipmentData(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => 
                s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.AVAL ||
                s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.GUDANGJADI);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaMovementModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new PreShipmentIndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                BuyerName = s.Buyer,
                Color = s.Color,
                Construction =s.Construction,
                Grade = s.Grade,
                Motif = s.Motif,
                ProductionOrderNo = s.ProductionOrderNo,
                Shift = s.Shift,
                Unit = s.UnitName,
                UomUnit = s.UOMUnit
            });

            return new ListResult<PreShipmentIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<ShipmentIndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => 
                s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.SHIP);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaMovementModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new ShipmentIndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                BuyerName = s.Buyer,
                Color = s.Color,
                Construction = s.Construction,
                Grade = s.Grade,
                Motif = s.Motif,
                ProductionOrderNo = s.ProductionOrderNo,
                Date = s.Date,
                MeterLength = s.MeterLength,
                DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                YardsLength = s.YardsLength,
                UomUnit = s.UOMUnit
            });

            return new ListResult<ShipmentIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<ShipmentInputViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            ShipmentInputViewModel vm = MapToViewModel(model);

            return vm;
        }

        public async Task<int> Update(int id, ShipmentInputViewModel viewModel)
        {
            int result = await _repository.UpdateFromShipmentAsync(id, viewModel.DeliveryOrderSales.Id, viewModel.DeliveryOrderSales.No);
            result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.SHIP, DateTimeOffset.UtcNow, viewModel.Shift);

            return result;
        }
    }
}
