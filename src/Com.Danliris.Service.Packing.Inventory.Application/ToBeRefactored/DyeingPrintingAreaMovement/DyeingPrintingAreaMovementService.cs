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

namespace Com.Danliris.Service.Packing.Inventory.Application.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementService : IDyeingPrintingAreaMovementService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public DyeingPrintingAreaMovementService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private string GenerateBonNo(int totalPreviousData)
        {
            return string.Format("IM.{0}.{1}", DateTime.UtcNow.Year.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private DyeingPrintingAreaMovementViewModel MapToViewModel(DyeingPrintingAreaMovementModel model)
        {
            var vm = new DyeingPrintingAreaMovementViewModel()
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
                ProductionOrder = new ProductionOrder()
                {
                    Id = model.ProductionOrderId,
                    Code = model.ProductionOrderCode,
                    No = model.ProductionOrderNo
                },
                Shift = model.Shift,
                Status = model.Status,
                Unit = new Unit()
                {
                    Code = model.UnitCode,
                    Id = model.UnitId,
                    Name = model.UnitName
                },
                UOMUnit = model.UOMUnit
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

        public Task<int> Create(DyeingPrintingAreaMovementViewModel viewModel)
        {
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.CreatedUtc.Year == DateTime.UtcNow.Year);

            string bonNo = GenerateBonNo(totalCurrentYearData++);
            var model = new DyeingPrintingAreaMovementModel(viewModel.Area, bonNo, viewModel.Date, viewModel.Shift, viewModel.ProductionOrder.Id,
                            viewModel.ProductionOrder.Code, viewModel.ProductionOrder.No, viewModel.CartNo, viewModel.Material.Id, viewModel.Material.Code,
                            viewModel.Material.Name, viewModel.MaterialConstruction.Id, viewModel.MaterialConstruction.Code, viewModel.MaterialConstruction.Name,
                            viewModel.MaterialWidth, viewModel.Unit.Id, viewModel.Unit.Code, viewModel.Unit.Name, viewModel.Color, viewModel.Mutation, viewModel.Length,
                            viewModel.UOMUnit, viewModel.Balance);

            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return  _repository.DeleteAsync(id);
        }

        public async Task<DyeingPrintingAreaMovementViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;
            
            DyeingPrintingAreaMovementViewModel vm = MapToViewModel(model);

            return vm;
        }


        public Task<int> Update(int id, DyeingPrintingAreaMovementViewModel viewModel)
        {
            var model = new DyeingPrintingAreaMovementModel(viewModel.Area, viewModel.BonNo, viewModel.Date, viewModel.Shift, viewModel.ProductionOrder.Id,
                            viewModel.ProductionOrder.Code, viewModel.ProductionOrder.No, viewModel.CartNo, viewModel.Material.Id, viewModel.Material.Code,
                            viewModel.Material.Name, viewModel.MaterialConstruction.Id, viewModel.MaterialConstruction.Code, viewModel.MaterialConstruction.Name,
                            viewModel.MaterialWidth, viewModel.Unit.Id, viewModel.Unit.Code, viewModel.Unit.Name, viewModel.Color, viewModel.Mutation, viewModel.Length,
                            viewModel.UOMUnit, viewModel.Balance);
            return _repository.UpdateAsync(id, model);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaMovementModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaMovementModel>.Order(query, OrderDictionary);
            var data = query.Select(s => new IndexViewModel()
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
                            Shift = s.Shift,
                            Status = s.Status,
                            UnitName = s.UnitName,
                            YardsLength = s.YardsLength
                        }).Skip((page - 1) * size).Take(size);

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
