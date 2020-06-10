using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation
{
    public class InputAvalTransformationService : IInputAvalTransformationService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string TYPE = "TRANSFORM";

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

        public InputAvalTransformationService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}Y", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }

        private InputAvalTransformationViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputAvalTransformationViewModel()
            {
                Active = model.Active,
                TotalWeight = model.TotalAvalWeight,
                TotalQuantity = model.TotalAvalQuantity,
                AvalType = model.AvalType,
                Area = model.Area,
                BonNo = model.BonNo,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy=model.DeletedBy,
                DeletedUtc= model.DeletedUtc,
                Group = model.Group,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                AvalTransformationProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(d => new InputAvalTransformationProductionOrderViewModel()
                {
                    Active = d.Active,
                    AvalQuantity = d.AvalQuantity,
                    AvalType = d.AvalType,
                    BonNo = d.InputAvalBonNo,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    CreatedAgent = d.CreatedAgent,
                    CreatedBy = d.CreatedBy,
                    CreatedUtc = d.CreatedUtc,
                    DeletedAgent = d.DeletedAgent,
                    DeletedBy = d.DeletedBy,
                    DeletedUtc = d.DeletedUtc,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaOutputProductionOrderId,
                    Id = d.Id,
                    IsDeleted =d.IsDeleted,
                    LastModifiedAgent = d.LastModifiedAgent,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedUtc = d.LastModifiedUtc,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        OrderQuantity = d.ProductionOrderOrderQuantity,
                        Type = d.ProductionOrderType
                    },
                    Quantity = d.Balance,
                    Unit = d.Unit,
                    UomUnit = d.UomUnit,
                    WeightQuantity = d.AvalQuantityKg,
                    HasOutputDocument = d.HasOutputDocument
                }).ToList()
            };

            return vm;
        }

        public async Task<int> Create(InputAvalTransformationViewModel viewModel)
        {
            int result = 0;

            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == GUDANGAVAL && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.AvalType == viewModel.AvalType && s.IsTransformedAval);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.AvalType, true,
                    viewModel.AvalTransformationProductionOrders.Sum(d => d.AvalQuantity), viewModel.AvalTransformationProductionOrders.Sum(d => d.WeightQuantity),
                    viewModel.AvalTransformationProductionOrders.Select(d => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, d.BonNo, d.ProductionOrder.Id, d.ProductionOrder.No,
                    d.ProductionOrder.Type, d.ProductionOrder.OrderQuantity, d.CartNo, d.Construction, d.Unit, d.Buyer, d.BuyerId, d.Color, d.Motif, d.AvalType, d.UomUnit, d.Quantity, d.AvalQuantity,
                    d.WeightQuantity, false, d.Id)).ToList());

                result = await _repository.InsertAsync(model);

                //result += await _outputRepository.UpdateFromInputAsync(viewModel.OutputId, true);
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {
                    var itemVM = viewModel.AvalTransformationProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaOutputProductionOrderId);
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaOutputProductionOrderId, true);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.AvalQuantity, item.AvalQuantityKg, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);

                    //var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == itemVM.InputId && s.ProductionOrderId == item.ProductionOrderId);

                    //var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    //    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id);

                    //if (previousSummary == null)
                    //{

                    //    result += await _summaryRepository.InsertAsync(summaryModel);
                    //}
                    //else
                    //{

                    //    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    //}
                }
            }
            else
            {
                foreach (var item in viewModel.AvalTransformationProductionOrders)
                {

                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No, item.ProductionOrder.Type,
                        item.ProductionOrder.OrderQuantity, item.CartNo, item.Construction, item.Unit, item.Buyer, item.BuyerId, item.Color, item.Motif, item.AvalType, item.UomUnit,
                        item.Quantity, item.AvalQuantity, item.WeightQuantity, false, item.Id);

                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    result += await _inputProductionOrderRepository.InsertAsync(modelItem);
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, true);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Quantity, modelItem.Id, item.AvalQuantity, item.WeightQuantity, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);
                    //var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.OutputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    //var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                    //    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Qty, modelItem.Id);

                    //if (previousSummary == null)
                    //{

                    //    result += await _summaryRepository.InsertAsync(summaryModel);
                    //}
                    //else
                    //{

                    //    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    //}
                }
            }



            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var model = await _repository.ReadByIdAsync(id);

            if (model.TotalAvalQuantity == 0 && model.TotalAvalWeight == 0)
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Aval!");
            }

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                if (!item.HasOutputDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteAvalTransformationArea(model);

            return result;
        }

        public List<InputAvalTransformationProductionOrderViewModel> GetInputAvalProductionOrders(string avalType)
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                 .Where(s => s.Area == GUDANGAVAL && !s.HasOutputDocument && !s.DyeingPrintingAreaInput.IsTransformedAval && s.AvalType == avalType);
            var data = productionOrders.Select(s => new InputAvalTransformationProductionOrderViewModel()
            {
                Id = s.Id,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Construction = s.Construction,
                Color = s.Color,
                Motif = s.Motif,
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                InputId = s.DyeingPrintingAreaInputId,
                AvalType = s.AvalType,
                BonNo = s.DyeingPrintingAreaInput.BonNo,
                CartNo = s.CartNo,
                Quantity = s.Balance,
                DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaOutputProductionOrderId
            });

            return data.ToList();
        }

        public ListResult<InputAvalTransformationViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.IsTransformedAval&& s.TotalAvalQuantity != 0 && s.TotalAvalWeight != 0);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputAvalTransformationViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Group = s.Group,
                Shift = s.Shift,
                AvalType = s.AvalType,
                TotalQuantity = s.TotalAvalQuantity,
                TotalWeight = s.TotalAvalWeight
            });

            return new ListResult<InputAvalTransformationViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputAvalTransformationViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public async Task<int> Update(int id, InputAvalTransformationViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.Group, viewModel.AvalType, viewModel.IsTransformedAval,
                    viewModel.AvalTransformationProductionOrders.Sum(d => d.AvalQuantity), viewModel.AvalTransformationProductionOrders.Sum(d => d.WeightQuantity),
                    viewModel.AvalTransformationProductionOrders.Select(d => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, d.BonNo, d.ProductionOrder.Id, d.ProductionOrder.No,
                    d.ProductionOrder.Type, d.ProductionOrder.OrderQuantity, d.CartNo, d.Construction, d.Unit, d.Buyer, d.BuyerId, d.Color, d.Motif, d.AvalType, d.UomUnit, d.Quantity, d.AvalQuantity,
                    d.WeightQuantity, d.HasOutputDocument, d.DyeingPrintingAreaInputProductionOrderId)
                    {
                        Id = d.Id
                    }).ToList());

            var deletedData = dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument && !viewModel.AvalTransformationProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdateAvalTransformationArea(id, model, dbModel);

            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, TYPE, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }
    }
}
