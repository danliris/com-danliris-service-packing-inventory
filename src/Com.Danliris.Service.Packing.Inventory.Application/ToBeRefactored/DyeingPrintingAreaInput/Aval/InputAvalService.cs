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
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputSppRepository;

        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSppRepository;

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
            _inputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
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
                Group = model.Group,
                BonNo = model.BonNo,
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
                    ProductionOrder = new ProductionOrder
                    {
                        No = s.ProductionOrderNo,
                        Id = s.ProductionOrderId,
                        Type = s.ProductionOrderType
                    },
                    Id = s.Id,
                    AvalType = s.AvalType,
                    AvalCartNo = s.AvalCartNo,
                    AvalUomUnit = s.UomUnit,
                    AvalQuantity = s.Balance,
                    AvalQuantityKg = s.AvalQuantityKg,
                    HasOutputDocument = s.HasOutputDocument,
                    ProductionOrderId = Convert.ToInt32(s.ProductionOrderId),
                    ProductionOrderNo = s.ProductionOrderNo,
                    ProductionOrderOrderQuantity = s.ProductionOrderOrderQuantity,
                    ProductionOrderType = s.ProductionOrderType,
                    PackingInstruction = s.PackingInstruction,
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    DyeingPrintingAreaOutputProductionOrderId = s.DyeingPrintingAreaOutputProductionOrderId,
                    Status = s.Status,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    Grade = s.Grade,
                    Motif = s.Motif,
                    Remark = s.Remark,
                    Unit = s.Unit,
                    IsChecked = s.IsChecked,
                    Area = s.Area,
                    QtyOrder = s.ProductionOrderOrderQuantity,
                    UomUnit = s.UomUnit
                }).ToList()
            };


            return vm;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }
        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == PACKING)
                return string.Format("{0}.{1}.{2}", PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == INSPECTIONMATERIAL)
                return string.Format("{0}.{1}.{2}", IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == TRANSIT)
                return string.Format("{0}.{1}.{2}", TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == GUDANGJADI)
                return string.Format("{0}.{1}.{2}", GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == GUDANGAVAL)
                return string.Format("{0}.{1}.{2}", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == SHIPPING)
                return string.Format("{0}.{1}.{2}", SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else
                return string.Format("{0}.{1}.{2}", GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        public async Task<int> Create(InputAvalViewModel viewModel)
        {
            int result = 0;

            //Count Existing Document in Aval Input by Year
            int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL && 
                                                                                              s.CreatedUtc.Year == viewModel.Date.Year);

            //Generate Bon Number if bon with date and shift has no exist
            //search bon 
            var bonExist = _inputRepository.ReadAll().Where(s => s.Date.Date == viewModel.Date.Date &&
                                                               s.Shift == viewModel.Shift &&
                                                               s.Area == GUDANGAVAL);
            string bonNo = string.Empty;
            int bonExistCount = bonExist.Count();
            if(bonExistCount == 0)
            {
                bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
            }
            else
            {
                bonNo = bonExist.FirstOrDefault().BonNo;
            }

            //Filter only Item Has Quantity and Quantity KG can be Inserted
            //viewModel.AvalItems = viewModel.AvalItems.Where(s => s.AvalQuantity > 0 && s.AvalQuantityKg > 0).ToList();

            //Instantiate Input Model
            DyeingPrintingAreaInputModel model = null;
            if (bonExistCount == 0)
            {
                model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                            viewModel.Area,
                                                            viewModel.Shift,
                                                            bonNo,
                                                            viewModel.Group,
                                                            viewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                                                                            s.AvalType,
                                                                                                                                            s.AvalCartNo,
                                                                                                                                            s.AvalUomUnit,
                                                                                                                                            s.AvalQuantity,
                                                                                                                                            s.AvalQuantityKg,
                                                                                                                                            false,
                                                                                                                                            s.ProductionOrderId,
                                                                                                                                            s.ProductionOrderNo,
                                                                                                                                            s.CartNo,
                                                                                                                                            s.BuyerId,
                                                                                                                                            s.Buyer,
                                                                                                                                            s.Construction,
                                                                                                                                            s.Unit,
                                                                                                                                            s.Color,
                                                                                                                                            s.Motif,
                                                                                                                                            s.Remark,
                                                                                                                                            s.Grade,
                                                                                                                                            s.Status,
                                                                                                                                            s.Balance,
                                                                                                                                            s.PackingInstruction,
                                                                                                                                            s.ProductionOrderType,
                                                                                                                                            s.ProductionOrderOrderQuantity,
                                                                                                                                            s.PackagingType,
                                                                                                                                            s.PackagingQty,
                                                                                                                                            s.PackagingUnit,
                                                                                                                                            s.DyeingPrintingAreaOutputProductionOrderId))
                                                                               .ToList());
            }
            else
            {
                model = new DyeingPrintingAreaInputModel(viewModel.Date,
                                                            viewModel.Area,
                                                            viewModel.Shift,
                                                            bonNo,
                                                            viewModel.Group,
                                                            viewModel.AvalItems.Select(s => new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area,
                                                                                                                                            s.AvalType,
                                                                                                                                            s.AvalCartNo,
                                                                                                                                            s.AvalUomUnit,
                                                                                                                                            s.AvalQuantity,
                                                                                                                                            s.AvalQuantityKg,
                                                                                                                                            false,
                                                                                                                                            s.ProductionOrderId,
                                                                                                                                            s.ProductionOrderNo,
                                                                                                                                            s.CartNo,
                                                                                                                                            s.BuyerId,
                                                                                                                                            s.Buyer,
                                                                                                                                            s.Construction,
                                                                                                                                            s.Unit,
                                                                                                                                            s.Color,
                                                                                                                                            s.Motif,
                                                                                                                                            s.Remark,
                                                                                                                                            s.Grade,
                                                                                                                                            s.Status,
                                                                                                                                            s.Balance,
                                                                                                                                            s.Balance,
                                                                                                                                            s.PackingInstruction,
                                                                                                                                            s.ProductionOrderType,
                                                                                                                                            s.ProductionOrderOrderQuantity,
                                                                                                                                            s.PackagingType,
                                                                                                                                            s.PackagingQty,
                                                                                                                                            s.PackagingUnit,
                                                                                                                                            s.DyeingPrintingAreaOutputProductionOrderId,
                                                                                                                                            bonExist.First().Id
                                                                                                                                            ))
                                                                               .ToList());
            }

            //Create New Row in Input and ProductionOrdersInput in Each Repository 
            if (bonExistCount == 0)
            {
                result = await _inputRepository.InsertAsync(model);

                //foreach (var spp in model.DyeingPrintingAreaInputProductionOrders)
                //{ 
                //    //update balance
                //    var prevOutput = _outputSppRepository.ReadAll().FirstOrDefault(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                //    var prevInput = _inputSppRepository.ReadAll().FirstOrDefault(x => x.Id == prevOutput.DyeingPrintingAreaInputProductionOrderId);
                //    var newBalance = prevInput.Balance - spp.Balance;

                //    prevInput.SetBalance(newBalance, "CREATEAVAL", "SERVICE");
                //    result += await _inputSppRepository.UpdateAsync(prevInput.Id, prevInput);
                //}
            }
            else
            {
                foreach(var spp in model.DyeingPrintingAreaInputProductionOrders)
                {
                    result += await _inputSppRepository.InsertAsync(spp);
                    //update balance
                    var prevOutput = _outputSppRepository.ReadAll().FirstOrDefault(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    var prevInput = _inputSppRepository.ReadAll().FirstOrDefault(x => x.Id == prevOutput.DyeingPrintingAreaInputProductionOrderId);
                    var newBalance = prevInput.Balance - spp.Balance;

                    prevInput.SetBalance(newBalance, "CREATEAVAL", "SERVICE");
                    result += await _inputSppRepository.UpdateAsync(prevInput.Id, prevInput);
                }
            }

            ////Movement from Previous Area to Aval Area
            //foreach (var dyeingPrintingMovement in viewModel.DyeingPrintingMovementIds)
            //{
            //    //Flag for already on Input DyeingPrintingAreaOutputMovement
            //    result += await _outputRepository.UpdateFromInputAsync(dyeingPrintingMovement.DyeingPrintingAreaMovementId, true, dyeingPrintingMovement.ProductionOrderIds);

            //    foreach (var productionOrderId in dyeingPrintingMovement.ProductionOrderIds)
            //    {
            //        //Get Previous Summary
            //        var previousSummary = _summaryRepository.ReadAll()
            //                                                .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == dyeingPrintingMovement.DyeingPrintingAreaMovementId &&
            //                                                                     s.ProductionOrderId == productionOrderId);
            //        if (previousSummary != null)
            //            //Update Previous Summary
            //            result += await _summaryRepository.UpdateToAvalAsync(previousSummary, viewModel.Date, viewModel.Area, TYPE);
            //    }
            //}

            //Summed Up Balance (or Quantity in Aval)
            var groupedProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(o => new { o.AvalType,
                                                                                                           o.AvalCartNo,
                                                                                                           o.UomUnit })
                                                                                       .Select(i => new { i.Key.AvalType,
                                                                                                          i.Key.AvalCartNo,
                                                                                                          i.Key.UomUnit,
                                                                                                          Quantity = i.Sum(s => s.Balance) });

            foreach (var productionOrder in groupedProductionOrders)
             {
                //Instantiate Movement Model
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        model.Id,
                                                                        bonNo,
                                                                        productionOrder.AvalCartNo,
                                                                        productionOrder.UomUnit,
                                                                        productionOrder.Quantity);

                //Create New Row in Movement Repository
                result += await _movementRepository.InsertAsync(movementModel);

                ////Instantiate Summary Model
                //var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                //                                                      viewModel.Area,
                //                                                      TYPE,
                //                                                      model.Id,
                //                                                      bonNo,
                //                                                      productionOrder.AvalCartNo,
                //                                                      productionOrder.UomUnit,
                //                                                      productionOrder.Quantity);

                //Create New Row in Summary Repository
                //result += await _summaryRepository.InsertAsync(summaryModel);
            }

            return result;
        }

        //Already INPUT AVAL (Repository INPUT) for List in Input Aval List
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && 
                                                              s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
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
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputAvalViewModel> ReadById(int id)
        {
            var model = await _inputRepository.ReadByIdAsync(id);
            if (model == null)
            {
                return null;
            }

            var vm = MapToViewModel(model);

            return vm;
        }

        //OUT from IM, Not INPUT to AVAL yet (Repository OUTPUT) => for Loader in Aval Input
        public ListResult<PreAvalIndexViewModel> ReadOutputPreAval(DateTimeOffset searchDate, 
                                                                   string searchShift,
                                                                   string searchGroup,
                                                                   int page, 
                                                                   int size, 
                                                                   string filter, 
                                                                   string order, 
                                                                   string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Date <= searchDate && 
                                                               s.Shift == searchShift &&
                                                               s.Group == searchGroup &&
                                                               s.DestinationArea == GUDANGAVAL && 
                                                               !s.HasNextAreaDocument);
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
                Group = s.Group,
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
                    BuyerId = d.BuyerId,
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


        public ListResult<PreAvalIndexViewModel> ReadAllOutputPreAval(
                                                                   int page,
                                                                   int size,
                                                                   string filter,
                                                                   string order,
                                                                   string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => 
                                                               s.DestinationArea == GUDANGAVAL &&
                                                               s.DyeingPrintingAreaOutputProductionOrders.Any(t=>!t.HasNextAreaDocument)
                                                               );
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Select(s => new PreAvalIndexViewModel()
            {
                Id = s.Id,
                Date = s.Date,
                Area = s.Area,
                Shift = s.Shift,
                Group = s.Group,
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
                    BuyerId = d.BuyerId,
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
                    AvalConnectionLength = d.AvalConnectionLength,
                    QtyOrder = d.ProductionOrderOrderQuantity,
                    AvalType = d.AvalType,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId

                }).ToList()
            });

            return new ListResult<PreAvalIndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Reject(InputAvalViewModel viewModel)
        {
            int result = 0;

            var groupedProductionOrders = viewModel.AvalItems.GroupBy(s => s.Area);
            foreach (var item in groupedProductionOrders)
            {
                var model = _inputRepository.GetDbSet()
                                .FirstOrDefault(s => s.Area == item.Key && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key && s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date,item.Key);

                    model = new DyeingPrintingAreaInputModel(viewModel.Date, item.Key, viewModel.Shift, bonNo, viewModel.Group, viewModel.AvalItems.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(item.Key, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrderOrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id)).ToList());

                    result = await _inputRepository.InsertAsync(model);
                    result += await _outputSppRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);

                    foreach (var detail in item)
                    {
                        result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance);

                        //var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.Id && s.ProductionOrderId == detail.ProductionOrder.Id);

                        //var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            //detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance);

                        result += await _movementRepository.InsertAsync(movementModel);
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
                    foreach (var detail in item)
                    {
                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(item.Key, detail.ProductionOrder.Id, detail.ProductionOrder.No, detail.ProductionOrder.Type,
                            detail.ProductionOrder.OrderQuantity, detail.PackingInstruction, detail.CartNo, detail.Buyer, detail.Construction,
                            detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, false, detail.Remark, detail.Grade, detail.Status, detail.Balance, detail.BuyerId, detail.Id);
                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                           detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance);

                        //var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == detail.Id && s.ProductionOrderId == detail.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance);

                        result += await _inputSppRepository.InsertAsync(modelItem);
                        result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);
                        result += await _movementRepository.InsertAsync(movementModel);

                        //if (previousSummary == null)
                        //{

                        //    result += await _summaryRepository.InsertAsync(summaryModel);
                        //}
                        //else
                        //{

                        //    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                        //}

                    }
                    result += await _outputSppRepository.UpdateFromInputAsync(item.Select(s => s.Id), true);
                }
            }


            return result;
        }
        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            //get bon data and check if it has document output
            var modelBon = _inputRepository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any()).FirstOrDefault();
            if (modelBon != null)
            {
                var hasSPPwithOutput = modelBon.DyeingPrintingAreaInputProductionOrders.Where(x => x.HasOutputDocument);
                if (hasSPPwithOutput.Count() > 0)
                {
                    throw new Exception("Bon Sudah Berada di AVAL Keluar");
                }
                else
                {
                    //get prev bon id using first spp modelBon and search bonId
                    var firstSppBonModel = modelBon.DyeingPrintingAreaInputProductionOrders.FirstOrDefault();
                    int sppIdPrevOutput = firstSppBonModel == null ? 0 : firstSppBonModel.DyeingPrintingAreaOutputProductionOrderId;
                    var sppPrevOutput = _outputSppRepository.ReadAll().Where(s => s.Id == sppIdPrevOutput).FirstOrDefault();
                    int bonIdPrevOutput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaOutputId;
                    var bonPrevOutput = _outputRepository.ReadAll().Where(x =>
                                                                        x.DyeingPrintingAreaOutputProductionOrders.Any() &&
                                                                        x.Id == bonIdPrevOutput
                                                                        );
                    //get prev bon input using input spp id in prev bon out and search bonId
                    int sppIdPrevInput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaInputProductionOrderId;
                    var sppPrevInput = _inputSppRepository.ReadAll().FirstOrDefault(x => x.Id == sppIdPrevInput);
                    int bonIdPrevInput = sppPrevInput == null ? 0 : sppPrevInput.DyeingPrintingAreaInputId;
                    var bonPrevInput = _inputRepository.ReadAll().Where(x =>
                                                            x.DyeingPrintingAreaInputProductionOrders.Any() &&
                                                            x.Id == bonIdPrevInput
                                                            );


                    //delete entire packing bon and spp using model
                    result += await _inputRepository.DeleteAsync(bonId);

                    //activate bon prev hasNextAreaDocument == false;
                    foreach (var bon in bonPrevOutput)
                    {
                        bon.SetHasNextAreaDocument(false, "AVALINSERVICE", "SERVICE");
                        //activate spp prev from bon
                        foreach (var spp in bon.DyeingPrintingAreaOutputProductionOrders)
                        {
                            spp.SetHasNextAreaDocument(false, "AVALINSERVICE", "SERVICE");
                            //update balance input spp from prev spp
                            var inputSpp = _inputSppRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
                            foreach (var modifInputSpp in inputSpp)
                            {
                                var newBalance = modifInputSpp.Balance + spp.Balance;
                                modifInputSpp.SetBalanceRemains(newBalance, "AVALINSERVICE", "SERVICE");
                                modifInputSpp.SetBalance(newBalance, "AVALINSERVICE", "SERVICE");

                                modifInputSpp.SetHasOutputDocument(false, "AVALINSERVICE", "SERVICE");
                                result += await _inputSppRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
                            }

                            //insert new movement spp
                            var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, bon.Area, "OUT", bon.Id, modelBon.BonNo, spp.Id, spp.ProductionOrderNo,
                                    spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance);
                            result += await _movementRepository.InsertAsync(movementModel);

                            //update summary spp if exist create new when it null
                            //var summaryModel = new DyeingPrintingAreaSummaryModel(bon.Date, bon.Area, "OUT", bon.Id, bon.BonNo, spp.Id, spp.ProductionOrderNo,
                            //spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance);

                            //var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.ProductionOrderId == spp.Id);
                            //if (previousSummary == null)
                            //{
                            //    result += await _summaryRepository.InsertAsync(summaryModel);
                            //}
                            //else
                            //{
                            //    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                            //}
                        }
                        result += await _outputRepository.UpdateAsync(bon.Id, bon);
                    }
                }
            }

            return result;

        }
    }
}
