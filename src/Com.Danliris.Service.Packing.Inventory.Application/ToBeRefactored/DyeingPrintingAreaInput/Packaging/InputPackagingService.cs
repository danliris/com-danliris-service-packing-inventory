using System;
using System.Collections.Generic;
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
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade,s.Balance)).ToList());

                result = await _repository.InsertAsync(model);
            }
            else
            {
                bonNo = prevBon.BonNo;
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade,prevBon.Id,s.Balance)).ToList());
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
                modelInput.SetBalance(modelInput.Balance - modelSpp.Balance,"REPOSITORY","");
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
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);
                var summaryModelRepo = _summaryRepository.ReadAll().Where(x => x.ProductionOrderNo == summaryModel.ProductionOrderNo).FirstOrDefault();
                summaryModelRepo.SetArea(PACKING, "REPOSITORY", "");
                summaryModelRepo.SetType(TYPE, "REPOSITORY", "");
                summaryModelRepo.SetDyeingPrintingAreaDocument(model.Id,bonNo, "REPOSITORY", "");

                if(prevBon!=null)
                    result += await _productionOrderRepository.InsertAsync(item);

                result += await _movementRepository.InsertAsync(movementModel);
                //result += await _summaryRepository.InsertAsync(summaryModel);
                result += await _summaryRepository.UpdateAsync(summaryModelRepo.Id, summaryModelRepo);

            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == PACKING &&s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument && d.BalanceRemains > 0));
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
                CartNo = s.CartNo,
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
                UomUnit = s.UomUnit,
                QtyOrder = s.ProductionOrderOrderQuantity
            });

            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }
        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {
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
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
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
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,

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
                                                                                (s, s2) => s).Where(x=>x.HasNextAreaDocument == false);
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
                CartNo = s.CartNo,
                Color = s.Color,
                Construction = s.Construction,
                //HasOutputDocument = s.HasOutputDocument,
                //IsChecked = s.IsChecked,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType
                },
                ProductionOrderNo = s.ProductionOrderNo,
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                QtyOrder = s.ProductionOrderOrderQuantity
            });


            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
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
