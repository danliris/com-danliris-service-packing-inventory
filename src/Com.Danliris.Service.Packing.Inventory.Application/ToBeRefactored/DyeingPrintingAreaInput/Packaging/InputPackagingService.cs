using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class InputPackagingService : IInputPackagingService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        private readonly IDyeingPrintingAreaOutputRepository _repositoryAreaOutput;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _repositoryAreaProductionOrderOutput;

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

        public InputPackagingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _repositoryAreaOutput = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _repositoryAreaProductionOrderOutput = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        public async Task<int> CreateAsync(InputPackagingViewModel viewModel)
        {
            int result = 0;
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == PACKING && s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
            var prevBon = _repository.ReadAll().Where(s => s.Area == PACKING && s.Shift == viewModel.Shift && s.Date.Date == viewModel.Date.Date).FirstOrDefault();
            DyeingPrintingAreaInputModel model = new DyeingPrintingAreaInputModel();
            if (prevBon == null)
            {
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, s.Balance, s.BuyerId, s.Id, s.Remark)).ToList());

                result = await _repository.InsertAsync(model);
            }
            else
            {
                bonNo = prevBon.BonNo;
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, prevBon.Id, s.Balance, s.BuyerId, s.Id, s.Remark)).ToList());
                model.Id = prevBon.Id;

            }

            //var modelOutput = _repositoryAreaOutput.ReadAll().Where(s => s.DestinationArea == PACKING && s.BonNo == viewModel.BonNo && s.DyeingPrintingAreaOutputProductionOrders.Any(d => d.DyeingPrintingAreaOutputId == s.Id)).FirstOrDefault();
            //modelOutput.SetHasNextAreaDocument(true, "REPOSITORY", "");
            //if (modelOutput != null)
            //{
            //    result += await _repositoryAreaOutput.UpdateAsync(modelOutput.Id, modelOutput);
            //};

            foreach (var modelSpp in viewModel.PackagingProductionOrders)
            {
                var modelOutputs = _repositoryAreaProductionOrderOutput.ReadAll().Where(s => s.DestinationArea == PACKING && s.Id == modelSpp.Id).FirstOrDefault();
                modelOutputs.SetHasNextAreaDocument(true, "REPOSITORY", "");
                if (modelOutputs != null)
                {
                    result += await _repositoryAreaProductionOrderOutput.UpdateAsync(modelOutputs.Id, modelOutputs);
                };
                //set saldo inputSPP from outputSPP id
                var modelInput = _productionOrderRepository.ReadAll().First(x => x.Id == modelOutputs.DyeingPrintingAreaInputProductionOrderId);
                modelInput.SetBalance(modelInput.Balance - modelSpp.Balance, "REPOSITORY", "");
                result += await _productionOrderRepository.UpdateAsync(modelInput.Id, modelInput);
            }
            //var modelOutput = _repositoryAreaProductionOrderOutput.ReadAll().Join(viewModel.PackagingProductionOrders,
            //                                                                        s => s.Id,
            //                                                                        s2 => s2.Id,
            //                                                                        (s, s2) => s);
            //foreach (var modelSpp in modelOutput)
            //{
            //    modelSpp.SetHasNextAreaDocument(true, "REPOSITORY", "");
            //    if (modelOutput != null)
            //    {
            //        result += await _repositoryAreaProductionOrderOutput.UpdateAsync(modelSpp.Id, modelSpp);
            //    };
            //}

            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Grade);

                if (prevBon != null)
                    result += await _productionOrderRepository.InsertAsync(item);

                result += await _movementRepository.InsertAsync(movementModel);

            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument && d.BalanceRemains > 0));
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
                Shift = s.Shift,
                Group = s.Group,
                PackagingProductionOrders = s.DyeingPrintingAreaInputProductionOrders.Select(d => new InputPackagingProductionOrdersViewModel()
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
                        Type = d.ProductionOrderType
                    },
                    Grade = d.Grade,
                    Id = d.Id,
                    Unit = d.Unit,
                    Remark = d.Remark,
                    IsChecked = d.IsChecked,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit,
                    Material = d.Construction,
                    QtyOrder = d.ProductionOrderOrderQuantity
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<InputPackagingViewModel> ReadByIdAsync(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            InputPackagingViewModel vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<InputPackagingProductionOrdersViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword)
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
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputPackagingProductionOrdersViewModel()
            {
                Id = s.Id,
                Balance = s.Balance,
                //Balance = s.Balance - s.BalanceRemains,
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                CartNo = s.CartNo,
                Color = s.Color,
                Remark = s.Remark,
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
                MaterialWidth = s.MaterialWidth,
                MaterialProduct = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = s.MaterialConstructionName,
                    Id = s.MaterialConstructionId
                },
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                QtyOrder = s.ProductionOrderOrderQuantity
            });

            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }
        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
            return string.Format("{0}.{1}.{2}", PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
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
                return string.Format("{0}.{1}.{2}", PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        private InputPackagingViewModel MapToViewModel(DyeingPrintingAreaInputModel model)
        {
            var vm = new InputPackagingViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
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
                Group = model.Group,
                PackagingProductionOrders = model.DyeingPrintingAreaInputProductionOrders.Select(s => new InputPackagingProductionOrdersViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Remark = s.Remark,
                    HasOutputDocument = s.HasOutputDocument,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Motif = s.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo
                    },
                    MaterialWidth = s.MaterialWidth,
                    MaterialProduct = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    QtyOrder = s.ProductionOrderOrderQuantity,
                }).ToList()
            };


            return vm;
        }

        public ListResult<IndexViewModel> ReadBonOutToPack(int page, int size, string filter, string order, string keyword)
        {
            var query = _repositoryAreaOutput.ReadAll().Where(s => s.DestinationArea == PACKING && s.HasNextAreaDocument == false && s.DyeingPrintingAreaOutputProductionOrders.Any(d => d.DyeingPrintingAreaOutputId == s.Id));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
                PackagingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new InputPackagingProductionOrdersViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    Remark = d.Remark,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    MaterialWidth = d.MaterialWidth,
                    MaterialProduct = new Material()
                    {
                        Id = d.MaterialId,
                        Name = d.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = d.MaterialConstructionName,
                        Id = d.MaterialConstructionId
                    },
                    Grade = d.Grade,
                    Id = d.Id,
                    Unit = d.Unit,
                    Material = d.Construction,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit,
                    ProductionOrderNo = d.ProductionOrderNo,
                    QtyOrder = d.ProductionOrderOrderQuantity,

                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<InputPackagingProductionOrdersViewModel> ReadInProducionOrders(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repositoryAreaProductionOrderOutput.ReadAll();
            var query2 = _repositoryAreaOutput.ReadAll().Where(s => s.DestinationArea == PACKING && s.HasNextAreaDocument == false && s.DyeingPrintingAreaOutputProductionOrders.Any(item => item.DyeingPrintingAreaOutputId == s.Id));
            var query = _repositoryAreaProductionOrderOutput.ReadAll().Join(query2,
                                                                                s => s.DyeingPrintingAreaOutputId,
                                                                                s2 => s2.Id,
                                                                                (s, s2) => s).Where(x => x.HasNextAreaDocument == false);
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new InputPackagingProductionOrdersViewModel
            {
                Id = s.Id,
                Balance = s.Balance,
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                CartNo = s.CartNo,
                Color = s.Color,
                Status = s.Status,
                Construction = s.Construction,
                //HasOutputDocument = s.HasOutputDocument,
                //IsChecked = s.IsChecked,
                Motif = s.Motif,
                Grade = s.Grade,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType,
                    OrderQuantity = s.ProductionOrderOrderQuantity

                },
                MaterialWidth = s.MaterialWidth,
                MaterialProduct = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = s.MaterialConstructionName,
                    Id = s.MaterialConstructionId
                },
                Remark = s.Remark,
                ProductionOrderNo = s.ProductionOrderNo,
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                QtyOrder = s.ProductionOrderOrderQuantity,
                Area = s.Area,
                DyeingPrintingAreaInputProductionOrderId = s.DyeingPrintingAreaInputProductionOrderId,
                OutputId = s.DyeingPrintingAreaOutputId
            });


            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Reject(InputPackagingViewModel viewModel)
        {
            int result = 0;

            var groupedProductionOrders = viewModel.PackagingProductionOrders.GroupBy(s => s.Area);
            foreach (var item in groupedProductionOrders)
            {
                var model = _repository.GetDbSet()
                                .FirstOrDefault(s => s.Area == item.Key && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

                if (model == null)
                {
                    int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == item.Key && s.CreatedUtc.Year == viewModel.Date.Year);
                    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, item.Key);
                    model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(item.Key, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.Remark, s.Grade, s.Status, s.Balance, s.BuyerId, s.Id, s.MaterialProduct.Id, s.MaterialProduct.Name, s.MaterialConstruction.Id,
                     s.MaterialConstruction.Name, s.MaterialWidth)).ToList());

                    result = await _repository.InsertAsync(model);
                    result += await _repositoryAreaProductionOrderOutput.UpdateFromInputAsync(item.Select(s => s.Id), true);
                    
                    foreach (var detail in item)
                    {
                        var itemModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.DyeingPrintingAreaOutputProductionOrderId == detail.Id && s.ProductionOrderId == detail.ProductionOrder.Id);
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                            detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, itemModel.Id, detail.ProductionOrder.Type, detail.Grade);

                        result += await _movementRepository.InsertAsync(movementModel);
                        
                    }
                }
                else
                {
                    foreach (var detail in item)
                    {
                        var modelItem = new DyeingPrintingAreaInputProductionOrderModel(item.Key, detail.ProductionOrder.Id, detail.ProductionOrder.No, detail.ProductionOrder.Type,
                            detail.ProductionOrder.OrderQuantity, detail.PackingInstruction, detail.CartNo, detail.Buyer, detail.Construction,
                            detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, false, detail.Remark, detail.Grade, detail.Status, detail.Balance, detail.BuyerId, detail.Id
                            , detail.MaterialProduct.Id, detail.MaterialProduct.Name, detail.MaterialConstruction.Id, detail.MaterialConstruction.Name, detail.MaterialWidth);
                        modelItem.DyeingPrintingAreaInputId = model.Id;

                        result += await _productionOrderRepository.InsertAsync(modelItem);
                        result += await _productionOrderRepository.UpdateFromNextAreaInputAsync(detail.DyeingPrintingAreaInputProductionOrderId, detail.Balance);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, item.Key, TYPE, model.Id, model.BonNo, detail.ProductionOrder.Id, detail.ProductionOrder.No,
                           detail.CartNo, detail.Buyer, detail.Construction, detail.Unit, detail.Color, detail.Motif, detail.UomUnit, detail.Balance, modelItem.Id, detail.ProductionOrder.Type, detail.Grade);

                        result += await _movementRepository.InsertAsync(movementModel);

                    }
                    result += await _repositoryAreaProductionOrderOutput.UpdateFromInputAsync(item.Select(s => s.Id), true);
                }
            }


            return result;
        }

        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            //get bon data and check if it has document output
            var modelBon = _repository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any()).FirstOrDefault();
            if (modelBon != null)
            {
                var hasSPPwithOutput = modelBon.DyeingPrintingAreaInputProductionOrders.Where(x => x.HasOutputDocument);
                if (hasSPPwithOutput.Count() > 0)
                {
                    throw new Exception("Bon Sudah Berada di Packing Keluar");
                }
                else
                {
                    //get prev bon id using first spp modelBon and search bonId
                    var firstSppBonModel = modelBon.DyeingPrintingAreaInputProductionOrders.FirstOrDefault();
                    int sppIdPrevOutput = firstSppBonModel == null ? 0 : firstSppBonModel.DyeingPrintingAreaOutputProductionOrderId;
                    var sppPrevOutput = _repositoryAreaProductionOrderOutput.ReadAll().Where(s => s.Id == sppIdPrevOutput).FirstOrDefault();
                    int bonIdPrevOutput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaOutputId;
                    var bonPrevOutput = _repositoryAreaOutput.ReadAll().Where(x =>
                                                                        x.DyeingPrintingAreaOutputProductionOrders.Any() &&
                                                                        x.Id == bonIdPrevOutput
                                                                        );
                    //get prev bon input using input spp id in prev bon out and search bonId
                    int sppIdPrevInput = sppPrevOutput == null ? 0 : sppPrevOutput.DyeingPrintingAreaInputProductionOrderId;
                    var sppPrevInput = _productionOrderRepository.ReadAll().FirstOrDefault(x => x.Id == sppIdPrevInput);
                    int bonIdPrevInput = sppPrevInput == null ? 0 : sppPrevInput.DyeingPrintingAreaInputId;
                    var bonPrevInput = _repository.ReadAll().Where(x =>
                                                            x.DyeingPrintingAreaInputProductionOrders.Any() &&
                                                            x.Id == bonIdPrevInput
                                                            );


                    //delete entire packing bon and spp using model
                    result += await _repository.DeleteAsync(bonId);

                    //activate bon prev hasNextAreaDocument == false;
                    foreach (var bon in bonPrevOutput)
                    {
                        bon.SetHasNextAreaDocument(false, "PACKINGSERVICE", "SERVICE");
                        //activate spp prev from bon
                        foreach (var spp in bon.DyeingPrintingAreaOutputProductionOrders)
                        {
                            spp.SetHasNextAreaDocument(false, "PACKINGSERVICE", "SERVICE");
                            //update balance input spp from prev spp
                            var inputSpp = _productionOrderRepository.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaInputProductionOrderId);
                            foreach (var modifInputSpp in inputSpp)
                            {
                                var newBalance = modifInputSpp.Balance + spp.Balance;
                                modifInputSpp.SetBalance(newBalance, "PACKINGSERVICE", "SERVICE");

                                modifInputSpp.SetHasOutputDocument(false, "PACKINGSERVICE", "SERVICE");
                                result += await _productionOrderRepository.UpdateAsync(modifInputSpp.Id, modifInputSpp);
                            }

                            //insert new movement spp
                            var movementModel = new DyeingPrintingAreaMovementModel(bon.Date, bon.Area, "OUT", bon.Id, modelBon.BonNo, spp.ProductionOrderId, spp.ProductionOrderNo,
                                    spp.CartNo, spp.Buyer, spp.Construction, spp.Unit, spp.Color, spp.Motif, spp.UomUnit, spp.Balance, spp.Id, spp.ProductionOrderType, spp.Grade);
                            
                            result += await _movementRepository.InsertAsync(movementModel);

                        }
                        result += await _repositoryAreaOutput.UpdateAsync(bon.Id, bon);
                        //result += await _repositoryAreaOutput.DeleteAsync(bon.Id);
                    }
                }
            }

            return result;

        }

        public async Task<int> Update(int bonId, InputPackagingViewModel viewModel)
        {
            var result = 0;
            var bonInput = _repository.ReadAll().Where(x => x.Id == bonId && x.DyeingPrintingAreaInputProductionOrders.Any());
            foreach (var bon in bonInput)
            {
                var sppInput = bon.DyeingPrintingAreaInputProductionOrders;
                var sppDeleted = sppInput.Where(x => viewModel.PackagingProductionOrders.Any(s => x.Id != s.Id));
                foreach (var spp in sppDeleted)
                {
                    var prevOutput = _repositoryAreaProductionOrderOutput.ReadAll().Where(x => x.Id == spp.DyeingPrintingAreaOutputProductionOrderId);
                    foreach (var prevOut in prevOutput)
                    {
                        var prevInput = _productionOrderRepository.ReadAll().Where(x => x.Id == prevOut.DyeingPrintingAreaInputProductionOrderId);
                        foreach (var prevIn in prevInput)
                        {
                            var newBalance = prevIn.Balance + prevOut.Balance;
                            prevIn.SetBalance(newBalance, "UPDATEPACKING", "SERVICE");
                        }
                        prevOut.SetHasNextAreaDocument(false, "UPDATEPACKING", "SERVICE");
                        result += await _repositoryAreaProductionOrderOutput.UpdateAsync(prevOut.Id, prevOut);
                    }
                    result += await _productionOrderRepository.DeleteAsync(spp.Id);
                }
            }
            return result;
        }
        public MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var model = await _repository.ReadByIdAsync(id);
            var packingData = _repository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                packingData = packingData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                packingData = packingData.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                packingData = packingData.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            packingData = packingData.OrderBy(s => s.BonNo);

            var modelAll = packingData.ToList().Select(s =>
                  new
                  {
                      SppList = s.DyeingPrintingAreaInputProductionOrders.Select(d => new
                      {
                          BonNo = s.BonNo,
                          NoSPP = d.ProductionOrderNo,
                          QtyOrder = d.ProductionOrderOrderQuantity,
                          NoKreta = d.CartNo,
                          Material = d.Construction,
                          Unit = d.Unit,
                          Buyer = d.Buyer,
                          Warna = d.Color,
                          Motif = d.Motif,
                          Grade = d.Grade,
                          QtyKeluar = d.Balance,
                          SAT = d.UomUnit
                      })
                  });

            var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            var query = modelAll.SelectMany(s => s.SppList);

            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"NoSPP","NO SP" },
                {"QtyOrder","QTY ORDER" },
                {"NoKreta","NO KRETA" },
                {"Material","MATERIAL"},
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"Grade","GRADE"},
                {"QtyKeluar","QTY KELUAR" },
                {"SAT","SAT" },
            };
            var listClass = query.ToList().FirstOrDefault().GetType().GetProperties();
            #endregion
            #region Assign Column
            foreach (var prop in mappedClass.Select((item, index) => new { Index = index, Items = item }))
            {
                string fieldName = prop.Items.Value;
                dt.Columns.Add(new DataColumn() { ColumnName = fieldName, DataType = typeof(string) });
            }
            #endregion
            #region Assign Data
            foreach (var item in query)
            {
                List<string> data = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName && column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima");
                    string valueClass = "";
                    if (searchMappedClass != null && searchMappedClass != null && searchMappedClass.FirstOrDefault().Key != null)
                    {
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        var searchValue = searchProperty.GetValue(item, null);
                        valueClass = searchValue == null ? "" : searchValue.ToString();
                    }
                    //else
                    //{
                    //    valueClass = "";
                    //}
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("PENERIMAAN PACKAGING");

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 1, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[1, startHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {

                sheet.Cells[1, startHeaderColumn].Value = column.ColumnName;
                sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                startHeaderColumn++;
            }
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 2;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        //public ListResult<InputPackagingProductionOrdersViewModel> ReadProductionOrderByBon(string bonNo)
        //{
        //    //var query = _repositoryAreaProductionOrderOutput.ReadAll();
        //    var query2 = _repositoryAreaOutput.ReadAll().Where(s => s.DestinationArea == PACKING && s.HasNextAreaDocument == false && s.DyeingPrintingAreaOutputProductionOrders.Any(item => item.DyeingPrintingAreaOutputId == s.Id));
        //    var query = _repositoryAreaProductionOrderOutput.ReadAll().Join(query2,
        //                                                                        s => s.Id,
        //                                                                        s2 => s2.Id,
        //                                                                        (s, s2) => s);
        //    List<string> SearchAttributes = new List<string>()
        //    {
        //        "BonNo"
        //    };

        //    query = QueryHelper<DyeingPrintingAreaOutputProductionOrderModel>.Search(query, SearchAttributes, bonNo);
        //    var data = query.Select(s => new InputPackagingProductionOrdersViewModel
        //    {
        //        Id = s.Id,
        //        Balance = s.Balance,
        //        Buyer = s.Buyer,
        //        CartNo = s.CartNo,
        //        Color = s.Color,
        //        Construction = s.Construction,
        //        //HasOutputDocument = s.HasOutputDocument,
        //        //IsChecked = s.IsChecked,
        //        Motif = s.Motif,
        //        PackingInstruction = s.PackingInstruction,
        //        ProductionOrder = new ProductionOrder()
        //        {
        //            Id = s.ProductionOrderId,
        //            No = s.ProductionOrderNo,
        //            Type = s.ProductionOrderType
        //        },
        //        Unit = s.Unit,
        //        UomUnit = s.UomUnit
        //    });


        //    return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), 0, data.Count(), query.Count());
        //}
    }
}
