using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial
{
    public class InspectionMaterialService : IInspectionMaterialService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        private readonly IDyeingPrintingAreaMovementHistoryRepository _historyRepository;
        public InspectionMaterialService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _historyRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementHistoryRepository>();
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("IM.{0}.{1}", date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private InspectionMaterialViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new InspectionMaterialViewModel()
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
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Material = new Material()
                {
                    Id = model.MaterialId,
                    Code = model.MaterialCode,
                    Name = model.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Code = model.MaterialConstructionCode,
                    Name = model.MaterialConstructionName,
                    Id = model.MaterialConstructionId,
                },
                MaterialWidth = model.MaterialWidth,
                Mutation = model.Mutation,
                Motif = model.Motif,
                ProductionOrder = new ProductionOrder()
                {
                    Id = model.ProductionOrderId,
                    Code = model.ProductionOrderCode,
                    No = model.ProductionOrderNo,
                    Type = model.ProductionOrderType
                },
                ProductionOrderQuantity = model.ProductionOrderQuantity,
                PackingInstruction = model.PackingInstruction,
                Buyer = model.Buyer,
                Shift = model.Shift,
                Status = model.Status,
                Unit = new Unit()
                {
                    Code = model.UnitCode,
                    Id = model.UnitId,
                    Name = model.UnitName
                },
                UOMUnit = model.UOMUnit,
                Grade = model.Grade,
                SourceArea = model.SourceArea
            };

            if (vm.UOMUnit == model.METER)
            {
                vm.Length = model.MeterLength;
            }
            else if (vm.UOMUnit == model.YARDS)
            {
                vm.Length = model.YardsLength;
            }

            return vm;
        }

        public Task<int> Create(InspectionMaterialViewModel viewModel)
        {
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.CreatedUtc.Year == viewModel.Date.Year);
            List<DyeingPrintingAreaMovementHistoryModel> histories = new List<DyeingPrintingAreaMovementHistoryModel>();
            DyeingPrintingAreaMovementHistoryModel historyModel = new DyeingPrintingAreaMovementHistoryModel(viewModel.Date, viewModel.Area, viewModel.Shift, AreaEnum.IM);
            histories.Add(historyModel);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
            var model = new DyeingPrintingAreaMovementModel(viewModel.Area, bonNo, viewModel.Date, viewModel.Shift, viewModel.ProductionOrder.Id,
                            viewModel.ProductionOrder.Code, viewModel.ProductionOrder.No, viewModel.ProductionOrderQuantity, viewModel.ProductionOrder.Type,
                            viewModel.Buyer, viewModel.PackingInstruction, viewModel.CartNo, viewModel.Material.Id, viewModel.Material.Code,
                            viewModel.Material.Name, viewModel.MaterialConstruction.Id, viewModel.MaterialConstruction.Code, viewModel.MaterialConstruction.Name,
                            viewModel.MaterialWidth, viewModel.Unit.Id, viewModel.Unit.Code, viewModel.Unit.Name, viewModel.Color, viewModel.Motif, viewModel.Mutation, viewModel.Length,
                            viewModel.UOMUnit, viewModel.Balance, histories);

            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return  _repository.DeleteAsync(id);
        }

        public async Task<InspectionMaterialViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InspectionMaterialViewModel vm = MapToViewModel(model);

            return vm;
        }


        public async Task<int> Update(int id, InspectionMaterialViewModel viewModel)
        {
            
            var model = new DyeingPrintingAreaMovementModel(viewModel.Area, viewModel.BonNo, viewModel.Date, viewModel.Shift, viewModel.ProductionOrder.Id,
                            viewModel.ProductionOrder.Code, viewModel.ProductionOrder.No, viewModel.ProductionOrderQuantity, viewModel.ProductionOrder.Type, 
                            viewModel.Buyer, viewModel.PackingInstruction, viewModel.CartNo, viewModel.Material.Id, viewModel.Material.Code,
                            viewModel.Material.Name, viewModel.MaterialConstruction.Id, viewModel.MaterialConstruction.Code, viewModel.MaterialConstruction.Name,
                            viewModel.MaterialWidth, viewModel.Unit.Id, viewModel.Unit.Code, viewModel.Unit.Name, viewModel.Color, viewModel.Motif, viewModel.Mutation, viewModel.Length,
                            viewModel.UOMUnit, viewModel.Balance, new List<DyeingPrintingAreaMovementHistoryModel>());
            int result = await _repository.UpdateAsync(id, model);
            result += await _historyRepository.UpdateAsyncFromParent(id, AreaEnum.IM, viewModel.Date, viewModel.Shift);
            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.IM);
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
                            Mutation = s.Mutation,
                            ProductionOrderNo = s.ProductionOrderNo,
                            ProductionOrderQuantity = s.ProductionOrderQuantity,
                            Shift = s.Shift,
                            Status = s.Status,
                            UnitName = s.UnitName,
                            Motif = s.Motif,
                            UomUnit = s.UOMUnit,
                            YardsLength = s.YardsLength,
                            ProductionOrderId = s.ProductionOrderId,
                            ProductionOrderType = s.ProductionOrderType,
                            Buyer = s.Buyer
                        });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
