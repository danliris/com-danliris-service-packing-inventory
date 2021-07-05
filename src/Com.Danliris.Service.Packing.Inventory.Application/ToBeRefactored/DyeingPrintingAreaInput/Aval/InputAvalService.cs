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
using System.IO;
using System.Data;
using System.Globalization;

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
                    Machine = s.Machine,
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
                    MaterialWidth = s.MaterialWidth,
                    FinishWidth = s.FinishWidth,
                    Material = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
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
                    InputPackagingQty = s.InputPackagingQty,
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackingLength = s.PackagingLength,
                    PackagingUnit = s.PackagingUnit,
                    DyeingPrintingAreaOutputProductionOrderId = s.DyeingPrintingAreaOutputProductionOrderId,
                    Status = s.Status,
                    InputQuantity = s.InputQuantity,
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
                    UomUnit = s.UomUnit,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    HasPrintingProductSKU = s.HasPrintingProductSKU,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    HasPrintingProductPacking = s.HasPrintingProductPacking,
                    DateIn=s.DateIn,
                    DateOut=s.DateOut
                }).ToList()
            };


            return vm;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
        }
        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string area)
        {
            if (area == DyeingPrintingArea.PACKING)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == DyeingPrintingArea.INSPECTIONMATERIAL)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == DyeingPrintingArea.TRANSIT)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == DyeingPrintingArea.GUDANGJADI)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == DyeingPrintingArea.GUDANGAVAL)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else if (area == DyeingPrintingArea.SHIPPING)
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            else
                return string.Format("{0}.{1}.{2}", DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        public async Task<int> Create(InputAvalViewModel viewModel)
        {
            int result = 0;

            //Count Existing Document in Aval Input by Year
            int totalCurrentYearData = _inputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                                                              s.CreatedUtc.Year == viewModel.Date.Year);

            //Generate Bon Number if bon with date and shift has no exist
            //search bon 
            var bonExist = _inputRepository.ReadAll().Where(s => s.Date.Date == viewModel.Date.Date &&
                                                               s.Shift == viewModel.Shift &&
                                                               s.Area == DyeingPrintingArea.GUDANGAVAL);
            string bonNo = string.Empty;
            int bonExistCount = bonExist.Count();
            if (bonExistCount == 0)
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
                                                                                                                                            s.InputQuantity,
                                                                                                                                            s.PackingInstruction,
                                                                                                                                            s.ProductionOrderType,
                                                                                                                                            s.ProductionOrderOrderQuantity,
                                                                                                                                            s.PackagingType,
                                                                                                                                            s.InputPackagingQty,
                                                                                                                                            s.PackagingUnit,
                                                                                                                                            s.Id,
                                                                                                                                            s.Machine,
                                                                                                                                            s.Material.Id,
                                                                                                                                            s.Material.Name,
                                                                                                                                            s.MaterialConstruction.Id,
                                                                                                                                            s.MaterialConstruction.Name,
                                                                                                                                            s.MaterialWidth,
                                                                                                                                            s.ProcessType.Id,
                                                                                                                                            s.ProcessType.Name,
                                                                                                                                            s.YarnMaterial.Id,
                                                                                                                                            s.YarnMaterial.Name,
                                                                                                                                            s.ProductSKUId,
                                                                                                                                            s.FabricSKUId,
                                                                                                                                            s.ProductSKUCode,
                                                                                                                                            s.HasPrintingProductSKU,
                                                                                                                                            s.ProductPackingId,
                                                                                                                                            s.FabricPackingId,
                                                                                                                                            s.ProductPackingCode,
                                                                                                                                            s.HasPrintingProductPacking,
                                                                                                                                            s.PackingLength,
                                                                                                                                            s.InputQuantity,
                                                                                                                                            s.InputPackagingQty,
                                                                                                                                            viewModel.Date,
                                                                                                                                            s.FinishWidth))

                                                                               .ToList());
                result = await _inputRepository.InsertAsync(model);
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
                                                                s.InputQuantity,
                                                                s.InputQuantity,
                                                                s.PackingInstruction,
                                                                s.ProductionOrderType,
                                                                s.ProductionOrderOrderQuantity,
                                                                s.PackagingType,
                                                                s.InputPackagingQty,
                                                                s.PackagingUnit,
                                                                s.Id,
                                                                bonExist.First().Id,
                                                                s.Machine,
                                                                s.Material.Id,
                                                                s.Material.Name,
                                                                s.MaterialConstruction.Id,
                                                                s.MaterialConstruction.Name,
                                                                s.MaterialWidth,
                                                                s.ProcessType.Id,
                                                                s.ProcessType.Name,
                                                                s.YarnMaterial.Id,
                                                                s.YarnMaterial.Name,
                                                                s.ProductSKUId,
                                                                s.FabricSKUId,
                                                                s.ProductSKUCode,
                                                                s.HasPrintingProductSKU,
                                                                s.ProductPackingId,
                                                                s.FabricPackingId,
                                                                s.ProductPackingCode,
                                                                s.HasPrintingProductPacking,
                                                                s.PackingLength,
                                                                s.InputQuantity,
                                                                s.InputPackagingQty,
                                                                s.FinishWidth,
                                                                viewModel.Date)).ToList());
            }

            //Create New Row in Input and ProductionOrdersInput in Each Repository 
            if (bonExistCount == 0)
            {
                foreach (var spp in model.DyeingPrintingAreaInputProductionOrders)
                {
                    //update balance
                    //var prevOutput = _outputSppRepository.ReadAll().FirstOrDefault(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    //var prevInput = _inputSppRepository.ReadAll().FirstOrDefault(x => x.Id == prevOutput.DyeingPrintingAreaInputProductionOrderId);
                    //var newBalance = prevInput.Balance - spp.Balance;

                    //prevInput.SetBalance(newBalance, "CREATEAVAL", "SERVICE");
                    //result += await _inputSppRepository.UpdateAsync(prevInput.Id, prevInput);
                    var itemVM = viewModel.AvalItems.FirstOrDefault(s => s.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    if (itemVM.Area == DyeingPrintingArea.PACKING)
                    {
                        //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                        var packingData = JsonConvert.DeserializeObject<List<PackingData>>(itemVM.PrevSppInJson);
                        result += await _inputSppRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                    }
                    else
                    {

                        result += await _inputSppRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, spp.InputQuantity, spp.InputPackagingQty);
                    }
                    //result += await _inputSppRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, spp.InputQuantity, spp.InputPackagingQty);
                    result += await _outputSppRepository.UpdateFromInputNextAreaFlagAsync(itemVM.Id, true, DyeingPrintingArea.TERIMA);
                }

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
                foreach (var spp in model.DyeingPrintingAreaInputProductionOrders)
                {
                    result += await _inputSppRepository.InsertAsync(spp);
                    //update balance
                    //var prevOutput = _outputSppRepository.ReadAll().FirstOrDefault(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    //var prevInput = _inputSppRepository.ReadAll().FirstOrDefault(x => x.Id == prevOutput.DyeingPrintingAreaInputProductionOrderId);
                    //var newBalance = prevInput.Balance - spp.Balance;

                    //prevInput.SetBalance(newBalance, "CREATEAVAL", "SERVICE");
                    //result += await _inputSppRepository.UpdateAsync(prevInput.Id, prevInput);

                    var itemVM = viewModel.AvalItems.FirstOrDefault(s => s.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    if (itemVM.Area == DyeingPrintingArea.PACKING)
                    {
                        //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                        var packingData = JsonConvert.DeserializeObject<List<PackingData>>(itemVM.PrevSppInJson);
                        result += await _inputSppRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                    }
                    else
                    {

                        result += await _inputSppRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, spp.InputQuantity, spp.InputPackagingQty);
                    }
                    //result += await _inputSppRepository.UpdateFromNextAreaInputAsync(itemVM.DyeingPrintingAreaInputProductionOrderId, spp.InputQuantity, spp.InputPackagingQty);
                    result += await _outputSppRepository.UpdateFromInputNextAreaFlagAsync(itemVM.Id, true, DyeingPrintingArea.TERIMA);
                }
            }

            //Movement from Previous Area to Aval Area
            foreach (var dyeingPrintingMovement in viewModel.DyeingPrintingMovementIds)
            {
                //Flag for already on Input DyeingPrintingAreaOutputMovement
                result += await _outputRepository.UpdateFromInputAsync(dyeingPrintingMovement.DyeingPrintingAreaMovementId, true, dyeingPrintingMovement.ProductionOrderIds);

                //foreach (var productionOrderId in dyeingPrintingMovement.ProductionOrderIds)
                //{
                //    //Get Previous Summary
                //    var previousSummary = _summaryRepository.ReadAll()
                //                                            .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == dyeingPrintingMovement.DyeingPrintingAreaMovementId &&
                //                                                                 s.ProductionOrderId == productionOrderId);
                //    if (previousSummary != null)
                //        //Update Previous Summary
                //        result += await _summaryRepository.UpdateToAvalAsync(previousSummary, viewModel.Date, viewModel.Area, TYPE);
                //}
            }

            //Summed Up Balance (or Quantity in Aval)
            var groupedProductionOrders = model.DyeingPrintingAreaInputProductionOrders.GroupBy(o => new
            {
                o.AvalType,
                o.AvalCartNo,
                o.UomUnit
            }).Select(i => new
            {
                i.Key.AvalType,
                i.Key.AvalCartNo,
                i.Key.UomUnit,
                Quantity = i.Sum(s => s.Balance)
            });

            foreach (var productionOrder in groupedProductionOrders)
            {
                //Instantiate Movement Model
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        DyeingPrintingArea.IN,
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
            //var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
            //                                                  !s.IsTransformedAval &&
            //                                                  s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _inputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                             !s.IsTransformedAval);
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
                                                               s.DestinationArea == DyeingPrintingArea.GUDANGAVAL &&
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
                    Machine = d.Machine,
                    MaterialWidth = d.MaterialWidth,
                    FinishWidth = d.FinishWidth,
                    Material = new Material()
                    {
                        Id = d.MaterialId,
                        Name = d.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = d.MaterialConstructionName,
                        Id = d.MaterialConstructionId
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
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
                    InputQuantity = d.Balance,
                    PackingInstruction = d.PackingInstruction,
                    AvalALength = d.AvalALength,
                    AvalBLength = d.AvalBLength,
                    AvalConnectionLength = d.AvalConnectionLength,
                    QtyOrder = d.ProductionOrderOrderQuantity,
                    DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                    PackagingUnit = d.PackagingUnit,
                    PackagingQty = d.PackagingQty,
                    InputPackagingQty = d.PackagingQty,
                    PackingLength = d.PackagingLength,
                    PackagingType = d.PackagingType,
                    AvalType = d.AvalType,
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    HasPrintingProductSKU = d.HasPrintingProductSKU,
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    HasPrintingProductPacking = d.HasPrintingProductPacking,
                    Area = d.Area,
                    PrevSppInJson = d.PrevSppInJson
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
                                                               s.DestinationArea == DyeingPrintingArea.GUDANGAVAL &&
                                                               s.DyeingPrintingAreaOutputProductionOrders.Any(t => !t.HasNextAreaDocument)
                                                               ).Select(s => new PreAvalIndexViewModel()
                                                               {
                                                                   Id = s.Id,
                                                                   Date = s.Date,
                                                                   Area = s.Area,
                                                                   Shift = s.Shift,
                                                                   Group = s.Group,
                                                                   BonNo = s.BonNo,
                                                                   HasNextAreaDocument = s.HasNextAreaDocument,
                                                                   DestinationArea = s.DestinationArea,
                                                                   PreAvalProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Where(x => !x.HasNextAreaDocument).Select(d => new OutputPreAvalProductionOrderViewModel()
                                                                   {
                                                                       Id = d.Id,
                                                                       ProductionOrder = new ProductionOrder()
                                                                       {
                                                                           Id = d.ProductionOrderId,
                                                                           No = d.ProductionOrderNo,
                                                                           Type = d.ProductionOrderType
                                                                       },
                                                                       MaterialWidth = d.MaterialWidth,
                                                                       FinishWidth = d.FinishWidth,
                                                                       Material = new Material()
                                                                       {
                                                                           Id = d.MaterialId,
                                                                           Name = d.MaterialName
                                                                       },
                                                                       MaterialConstruction = new MaterialConstruction()
                                                                       {
                                                                           Name = d.MaterialConstructionName,
                                                                           Id = d.MaterialConstructionId
                                                                       },
                                                                       ProcessType = new CommonViewModelObjectProperties.ProcessType()
                                                                       {
                                                                           Id = d.ProcessTypeId,
                                                                           Name = d.ProcessTypeName
                                                                       },
                                                                       YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                                                                       {
                                                                           Id = d.YarnMaterialId,
                                                                           Name = d.YarnMaterialName
                                                                       },
                                                                       Machine = d.Machine,
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
                                                                       InputQuantity = d.Balance,
                                                                       PackingInstruction = d.PackingInstruction,
                                                                       AvalALength = d.AvalALength,
                                                                       AvalBLength = d.AvalBLength,
                                                                       AvalConnectionLength = d.AvalConnectionLength,
                                                                       QtyOrder = d.ProductionOrderOrderQuantity,
                                                                       AvalType = d.AvalType,
                                                                       DyeingPrintingAreaInputProductionOrderId = d.DyeingPrintingAreaInputProductionOrderId,
                                                                       PackagingUnit = d.PackagingUnit,
                                                                       PackagingQty = d.PackagingQty,
                                                                       InputPackagingQty = d.PackagingQty,
                                                                       PackagingType = d.PackagingType,
                                                                       ProductSKUId = d.ProductSKUId,
                                                                       PackingLength = d.PackagingLength,
                                                                       FabricSKUId = d.FabricSKUId,
                                                                       ProductSKUCode = d.ProductSKUCode,
                                                                       HasPrintingProductSKU = d.HasPrintingProductSKU,
                                                                       ProductPackingId = d.ProductPackingId,
                                                                       FabricPackingId = d.FabricPackingId,
                                                                       ProductPackingCode = d.ProductPackingCode,
                                                                       HasPrintingProductPacking = d.HasPrintingProductPacking,
                                                                       Area = d.Area,
                                                                       PrevSppInJson = d.PrevSppInJson

                                                                   }).ToList()
                                                               });

            return new ListResult<PreAvalIndexViewModel>(query.ToList(), page, size, query.Count());
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
                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, item.Key);

                    model = new DyeingPrintingAreaInputModel(viewModel.Date, item.Key, viewModel.Shift, bonNo, viewModel.Group, viewModel.AvalItems.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(item.Key, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrderOrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                         s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, false, s.Remark, s.ProductionMachine, s.Grade, s.Status, s.InputQuantity, s.BuyerId, s.Id, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id,
                         s.MaterialConstruction.Name, s.MaterialWidth, s.InputPackagingQty, s.PackagingUnit, s.PackagingType, 0, "", s.AvalType, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name,
                         s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.HasPrintingProductSKU, s.ProductPackingId, s.FabricPackingId, s.ProductPackingCode, s.HasPrintingProductPacking, s.PackingLength, s.InputQuantity, s.InputPackagingQty, s.FinishWidth, s.DateIn, s.MaterialOrigin)).ToList());


                    result = await _inputRepository.InsertAsync(model);
                    result += await _outputSppRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);

                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _inputSppRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        }
                        //result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, itemModel.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
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
                            detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, false, detail.Remark, detail.ProductionMachine, detail.Grade, detail.Status, detail.InputQuantity, detail.BuyerId,
                            detail.Id, detail.Material.Id, detail.Material.Name, detail.MaterialConstruction.Id, detail.MaterialConstruction.Name, detail.MaterialWidth, detail.InputPackagingQty,
                            detail.PackagingUnit, detail.PackagingType, 0, "", detail.AvalType, detail.ProcessType.Id, detail.ProcessType.Name, detail.YarnMaterial.Id, detail.YarnMaterial.Name,
                            detail.ProductSKUId, detail.FabricSKUId, detail.ProductSKUCode, detail.HasPrintingProductSKU, detail.ProductPackingId, detail.FabricPackingId,
                            detail.ProductPackingCode, detail.HasPrintingProductPacking, detail.PackingLength, detail.InputQuantity, detail.InputPackagingQty, detail.FinishWidth, detail.DateIn, detail.MaterialOrigin);

                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _inputSppRepository.InsertAsync(modelItem);
                        if (detail.Area == DyeingPrintingArea.PACKING)
                        {
                            //var outputData = await _outputProductionOrderRepository.ReadByIdAsync(item.Id);
                            var packingData = JsonConvert.DeserializeObject<List<PackingData>>(detail.PrevSppInJson);
                            result += await _inputSppRepository.UpdateFromNextAreaInputPackingAsync(packingData);
                        }
                        else
                        {

                            result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        }
                        //result += await _inputSppRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.InputQuantity, detail.InputPackagingQty);
                        var movementModel = new DyeingPrintingAreaMovementModel();
                        if (item.Key == DyeingPrintingArea.INSPECTIONMATERIAL)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.TRANSIT)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type, null, detail.Remark);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }
                        else if (item.Key == DyeingPrintingArea.PACKING)
                        {
                            movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, detail.MaterialOrigin, item.Key, DyeingPrintingArea.IN, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                                detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.InputQuantity, modelItem.Id, detail.ProductionOrder.Type, detail.Grade);
                            result += await _movementRepository.InsertAsync(movementModel);
                        }

                        //if (previousSummary == null)
                        //{

                        //    result += await _summaryRepository.InsertAsync(summaryModel);
                        //}
                        //else
                        //{

                        //    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                        //}

                    }
                    result += await _outputSppRepository.UpdateFromInputAsync(item.Select(s => s.Id).ToList(), true, DyeingPrintingArea.TOLAK);
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

                    foreach (var item in modelBon.DyeingPrintingAreaInputProductionOrders)
                    {
                        var movementModel = new DyeingPrintingAreaMovementModel(modelBon.Date, item.MaterialOrigin, modelBon.Area, DyeingPrintingArea.IN, modelBon.Id, modelBon.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                                item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);
                        result += await _movementRepository.InsertAsync(movementModel);
                    }

                    //activate bon prev hasNextAreaDocument == false;
                    foreach (var bon in bonPrevOutput)
                    {
                        var deletedBon = modelBon.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == bon.Id);
                        if (deletedBon != null)
                        {
                            bon.SetHasNextAreaDocument(false, "AVALINSERVICE", "SERVICE");
                        }
                        //activate spp prev from bon
                        foreach (var spp in bon.DyeingPrintingAreaOutputProductionOrders)
                        {
                            spp.SetHasNextAreaDocument(false, "AVALINSERVICE", "SERVICE");
                            spp.SetNextAreaInputStatus(null, "AVALINSERVICE", "SERVICE");
                            //update balance input spp from prev spp
                            //var inputSpp = _inputSppRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
                            IQueryable<DyeingPrintingAreaInputProductionOrderModel> inputSpp;
                            if (spp.Area == DyeingPrintingArea.PACKING)
                            {
                                var packingData = JsonConvert.DeserializeObject<List<PackingData>>(spp.PrevSppInJson);
                                inputSpp = _inputSppRepository.ReadAll().Where(x => packingData.Any(e => e.Id == x.Id));
                            }
                            else
                            {

                                inputSpp = _inputSppRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
                            }
                            foreach (var modifInputSpp in inputSpp)
                            {
                                var newBalance = modifInputSpp.Balance + spp.Balance;
                                //modifInputSpp.SetBalanceRemains(newBalance, "AVALINSERVICE", "SERVICE");
                                modifInputSpp.SetBalance(newBalance, "AVALINSERVICE", "SERVICE");

                                modifInputSpp.SetHasOutputDocument(false, "AVALINSERVICE", "SERVICE");
                                result += await _inputSppRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
                            }

                            ////insert new movement spp
                            //var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, bon.Area, TYPE, bon.Id, modelBon.BonNo, spp.ProductionOrderId, spp.ProductionOrderNo,
                            //        spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance, spp.Id, spp.ProductionOrderType);
                            //result += await _movementRepository.InsertAsync(movementModel);

                            //update summary spp if exist create new when it null

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

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _inputRepository.ReadAll()
            //    .Where(s => s.Area == GUDANGAVAL && !s.IsTransformedAval && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _inputRepository.ReadAll()
               .Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL && !s.IsTransformedAval);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }


            query = query.OrderBy(s => s.BonNo);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Macam Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Terima", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("",  "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument).OrderBy(s => s.ProductionOrderNo))
                    foreach (var item in model.DyeingPrintingAreaInputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                    {
                        var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");
                       
                        dt.Rows.Add(model.BonNo, item.ProductionOrderNo, dateIn, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                            item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.AvalType, item.UomUnit, item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture));
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Gudang Aval") }, true);
        }
    }
}
