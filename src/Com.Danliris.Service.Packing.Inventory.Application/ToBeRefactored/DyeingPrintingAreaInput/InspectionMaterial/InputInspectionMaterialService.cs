﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Results;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial
{
    public class InputInspectionMaterialService : IInputInspectionMaterialService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;

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

        public InputInspectionMaterialService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
        }

        private InputInspectionMaterialViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputInspectionMaterialViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                Group = model.Group,
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
                InspectionMaterialProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputInspectionMaterialProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    BalanceRemains = s.BalanceRemains,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    AvalALength = s.AvalALength,
                    AvalBLength = s.AvalBLength,
                    AvalConnectionLength = s.AvalConnectionLength,
                    Grade = s.Grade,
                    InitLength = s.InitLength,
                    IsChecked = s.IsChecked,
                    PackingInstruction = s.PackingInstruction,
                    HasOutputDocument = s.HasOutputDocument,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Motif = s.Motif,
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
            return string.Format("{0}.{1}.{2}", IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }


        public async Task<int> Create(InputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == INSPECTIONMATERIAL && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.InspectionMaterialProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                     s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.Balance, false, s.BuyerId, 0)).ToList());

                result = await _repository.InsertAsync(model);
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(model.Date, model.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id);

                    result += await _movementRepository.InsertAsync(movementModel);
                    result += await _summaryRepository.InsertAsync(summaryModel);
                }
            }
            else
            {
                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                        item.Motif, item.UomUnit, item.Balance, item.Balance, false, item.BuyerId, 0);
                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    result += await _productionOrderRepository.InsertAsync(modelItem);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id);

                    result += await _movementRepository.InsertAsync(movementModel);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, modelItem.Id);

                    result += await _summaryRepository.InsertAsync(summaryModel);
                }

            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                InspectionMaterialProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputInspectionMaterialProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
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
                        OrderQuantity = d.ProductionOrderOrderQuantity
                    },
                    Grade = d.Grade,
                    AvalALength = d.AvalALength,
                    AvalBLength = d.AvalBLength,
                    AvalConnectionLength = d.AvalConnectionLength,
                    InitLength = d.InitLength,
                    Id = d.Id,
                    Unit = d.Unit,
                    IsChecked = d.IsChecked,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputInspectionMaterialViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InputInspectionMaterialViewModel vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<InputInspectionMaterialProductionOrderViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword)
        {
            var query = _productionOrderRepository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputInspectionMaterialProductionOrderViewModel()
            {
                Id = s.Id,
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                BuyerId = s.BuyerId,
                Color = s.Color,
                Construction = s.Construction,
                HasOutputDocument = s.HasOutputDocument,
                IsChecked = s.IsChecked,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType
                },
                Unit = s.Unit,
                UomUnit = s.UomUnit
            });

            return new ListResult<InputInspectionMaterialProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var model = await _repository.ReadByIdAsync(id);

            if (model.DyeingPrintingAreaInputProductionOrders.Any(item => !item.HasOutputDocument && item.BalanceRemains != item.Balance))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Inspection Material!");
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

            result += await _repository.DeleteIMArea(model);

            return result;
        }

        public async Task<int> Update(int id, InputInspectionMaterialViewModel viewModel)
        {

            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            var model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.Group, viewModel.InspectionMaterialProductionOrders.Select(s =>
                 new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                 s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, s.Balance, s.HasOutputDocument, s.BuyerId, 0)
                 { Id = s.Id }).ToList());
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;

                    dictBalance.Add(lclModel.Id, diffBalance);
                }
            }
            var deletedData = dbModel.DyeingPrintingAreaInputProductionOrders.Where(s => !s.HasOutputDocument && !viewModel.InspectionMaterialProductionOrders.Any(d => d.Id == s.Id)).ToList();

            if (deletedData.Any(item => !item.HasOutputDocument && item.BalanceRemains != item.Balance))
            {
                throw new Exception("Ada SPP yang Sudah Dibuat di Pengeluaran Inspection Material!");
            }

            result = await _repository.UpdateIMArea(id, model, dbModel);

            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }

                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, TYPE, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }
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
