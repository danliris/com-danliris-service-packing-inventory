using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialService : IOutputInspectionMaterialService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string OUT = "OUT";
        private const string ADJ = "ADJ";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";
        private const string PR = "PR";
        private const string ADJ_IN = "ADJ IN";
        private const string ADJ_OUT = "ADJ OUT";

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string PRODUKSI = "PRODUKSI";


        public OutputInspectionMaterialService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private async Task<OutputInspectionMaterialViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputInspectionMaterialViewModel();
            if (model.Type == null || model.Type == OUT)
            {
                vm = new OutputInspectionMaterialViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    Group = model.Group,
                    Type = OUT,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument
                };
                var groupedProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.GroupBy(s => s.DyeingPrintingAreaInputProductionOrderId);
                foreach (var item in groupedProductionOrders)
                {
                    var sppData = item.FirstOrDefault();
                    var inputData = await _inputProductionOrderRepository.ReadByIdAsync(sppData.DyeingPrintingAreaInputProductionOrderId);
                    var imProductionOrder = new OutputInspectionMaterialProductionOrderViewModel()
                    {
                        Id = sppData.DyeingPrintingAreaInputProductionOrderId,
                        Buyer = sppData.Buyer,
                        BuyerId = sppData.BuyerId,
                        CartNo = sppData.CartNo,
                        Machine = sppData.Machine,
                        Color = sppData.Color,
                        Construction = sppData.Construction,
                        Motif = sppData.Motif,
                        PackingInstruction = sppData.PackingInstruction,
                        ProductionOrder = new ProductionOrder()
                        {
                            Id = sppData.ProductionOrderId,
                            No = sppData.ProductionOrderNo,
                            OrderQuantity = sppData.ProductionOrderOrderQuantity,
                            Type = sppData.ProductionOrderType
                        },
                        MaterialWidth = sppData.MaterialWidth,
                        Material = new Material()
                        {
                            Id = sppData.MaterialId,
                            Name = sppData.MaterialName
                        },
                        MaterialConstruction = new MaterialConstruction()
                        {
                            Name = sppData.MaterialConstructionName,
                            Id = sppData.MaterialConstructionId
                        },
                        Status = sppData.Status,
                        Unit = sppData.Unit,
                        UomUnit = sppData.UomUnit,
                        Balance = inputData.Balance,
                        BalanceRemains = inputData.BalanceRemains + item.Sum(d => d.Balance),
                        ProductionOrderDetails = item.Select(d => new OutputInspectionMaterialProductionOrderDetailViewModel()
                        {
                            Active = d.Active,
                            AvalType = d.AvalType,
                            LastModifiedUtc = d.LastModifiedUtc,
                            LastModifiedBy = d.LastModifiedBy,
                            LastModifiedAgent = d.LastModifiedAgent,
                            IsDeleted = d.IsDeleted,
                            Id = d.Id,
                            Balance = d.Balance,
                            CreatedAgent = d.CreatedAgent,
                            CreatedBy = d.CreatedBy,
                            CreatedUtc = d.CreatedUtc,
                            DeletedAgent = d.DeletedAgent,
                            DeletedBy = d.DeletedBy,
                            DeletedUtc = d.DeletedUtc,
                            Grade = d.Grade,
                            HasNextAreaDocument = d.HasNextAreaDocument,
                            Remark = d.Remark
                        }).ToList()
                    };
                    vm.InspectionMaterialProductionOrders.Add(imProductionOrder);
                }
            }
            else
            {
                vm = new OutputInspectionMaterialViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Area = model.Area,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Type = ADJ,
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
                    InspectionMaterialProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputInspectionMaterialProductionOrderViewModel()
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
                        MaterialWidth = s.MaterialWidth,
                        AdjDocumentNo = s.AdjDocumentNo,
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
                        HasNextAreaDocument = s.HasNextAreaDocument,
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
            }


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            if (destinationArea == TRANSIT)
            {
                return string.Format("{0}.{1}.{2}.{3}", IM, TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == PACKING)
            {
                return string.Format("{0}.{1}.{2}.{3}", IM, PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == GUDANGAVAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", IM, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", IM, PR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtys)
        {
            if (qtys.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_IN, IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_OUT, IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == INSPECTIONMATERIAL && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == OUT);

            viewModel.InspectionMaterialProductionOrders = viewModel.InspectionMaterialProductionOrders.Where(s => s.IsSave).ToList();
            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.Type == OUT && s.DestinationArea == viewModel.DestinationArea
                    && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
                List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    foreach (var detail in item.ProductionOrderDetails)
                    {
                        var outputProductionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id,
                            item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                            item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId, detail.AvalType,
                            item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.Machine, "");
                        productionOrders.Add(outputProductionOrder);
                    }
                }

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, viewModel.Type,
                    productionOrders);

                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    var itemVM = viewModel.InspectionMaterialProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaInputProductionOrderId);

                    if(viewModel.DestinationArea == PRODUKSI)
                    {
                        result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                    }
                    else
                    {
                        result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                    }
                    
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);
                    
                }
            }
            else
            {
                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    foreach (var detail in item.ProductionOrderDetails)
                    {
                        var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                         item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId, detail.AvalType,
                         item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.Machine, "");
                        modelItem.DyeingPrintingAreaOutputId = model.Id;

                        result += await _outputProductionOrderRepository.InsertAsync(modelItem);
                        if (viewModel.DestinationArea == PRODUKSI)
                        {
                            result += await _inputProductionOrderRepository.UpdateBalanceAndRemainsAsync(item.Id, detail.Balance);
                        }
                        else
                        {
                            result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, detail.Balance);
                        }
                        
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, detail.Balance, modelItem.Id, item.ProductionOrder.Type);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }
                }
            }

            return result;
        }

        private async Task<int> CreateAdj(OutputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.InspectionMaterialProductionOrders.All(d => d.Balance > 0))
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.Type == ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.InspectionMaterialProductionOrders.Select(d => d.Balance));
                type = ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.Type == ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.InspectionMaterialProductionOrders.Select(d => d.Balance));
                type = ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group, type,
                    viewModel.InspectionMaterialProductionOrders.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id,
                    item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                    item.Motif, item.UomUnit, "", "", "", item.Balance, 0, item.BuyerId, "", item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth,
                    "", item.AdjDocumentNo)).ToList());

            result = await _repository.InsertAsync(model);

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Create(OutputInspectionMaterialViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == OUT)
            {
                result = await CreateOut(viewModel);
            }
            else
            {
                result = await CreateAdj(viewModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL &&
            (((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
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
                Type = s.Type == null || s.Type == OUT ? OUT : ADJ,
                DestinationArea = s.DestinationArea,
                Group = s.Group,
                HasNextAreaDocument = s.HasNextAreaDocument
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputInspectionMaterialViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputInspectionMaterialViewModel vm = await MapToViewModel(model);

            return vm;
        }

        public List<InputInspectionMaterialProductionOrderViewModel> GetInputInspectionMaterialProductionOrders(long productionOrderId)
        {
            IQueryable<DyeingPrintingAreaInputProductionOrderModel> productionOrders;

            if (productionOrderId == 0)
            {
                productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                                    .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasOutputDocument);
            }
            else
            {
                productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                                    .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasOutputDocument && s.ProductionOrderId == productionOrderId);

            }


            var data = productionOrders.Select(s => new InputInspectionMaterialProductionOrderViewModel()
            {
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                Color = s.Color,
                Construction = s.Construction,
                Grade = s.Grade,
                BuyerId = s.BuyerId,
                HasOutputDocument = s.HasOutputDocument,
                InitLength = s.InitLength,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                BalanceRemains = s.BalanceRemains,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                MaterialWidth = s.MaterialWidth,
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
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                Id = s.Id,
                InputId = s.DyeingPrintingAreaInputId

            });

            return data.ToList();
        }

        public ListResult<InputInspectionMaterialProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
                .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasOutputDocument);
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Order(query, OrderDictionary);
            var data = query
                .GroupBy(d => d.ProductionOrderId)
                .Select(s => s.First())
                .Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.ProductionOrderNo)
                .Select(s => new InputInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    }
                });

            return new ListResult<InputInspectionMaterialProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }

        private async Task<int> DeleteOut(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteIMArea(model);

            return result;
        }

        private async Task<int> DeleteAdj(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            string type;
            if (model.DyeingPrintingAreaOutputProductionOrders.All(d => d.Balance > 0))
            {
                type = ADJ_IN;
            }
            else
            {
                type = ADJ_OUT;
            }
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                   item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _repository.DeleteAsync(model.Id);

            return result;
        }

        public async Task<int> Delete(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == OUT)
            {
                return await DeleteOut(model);
            }
            else
            {
                return await DeleteAdj(model);
            }
        }

        private async Task<int> UpdateOut(int id, OutputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

            foreach (var item in viewModel.InspectionMaterialProductionOrders)
            {
                foreach (var detail in item.ProductionOrderDetails)
                {
                    var outputProductionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, detail.HasNextAreaDocument, item.ProductionOrder.Id,
                        item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                        item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId, detail.AvalType,
                        item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.Machine, "")
                    {
                        Id = detail.Id
                    };
                    productionOrders.Add(outputProductionOrder);
                }
            }

            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea, viewModel.Group, viewModel.Type,
                productionOrders);
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;

                    dictBalance.Add(lclModel.Id, diffBalance);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument && !productionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdateIMArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, OUT, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputInspectionMaterialViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _repository.ReadByIdAsync(id);
            if (viewModel.InspectionMaterialProductionOrders.All(d => d.Balance > 0))
            {
                type = ADJ_IN;
            }
            else
            {
                type = ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group, type,
                    viewModel.InspectionMaterialProductionOrders.Select(item => new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, "", true, item.ProductionOrder.Id,
                    item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                    item.Motif, item.UomUnit, "", "", "", item.Balance, 0, item.BuyerId, "", item.Material.Id, item.Material.Name, item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth,
                    "", item.AdjDocumentNo)
                    {
                        Id = item.Id
                    }).ToList());
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;

                    dictBalance.Add(lclModel.Id, diffBalance);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _repository.UpdateAdjustmentData(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.IsDeleted))
            {
                double newBalance = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, type, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputInspectionMaterialViewModel viewModel)
        {
            if (viewModel.Type == OUT)
            {
                return await UpdateOut(id, viewModel);
            }
            else
            {
                return await UpdateAdj(id, viewModel);
            }
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL &&
                (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));

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
            

            query = query.OrderBy(s => s.Type).ThenBy(s => s.DestinationArea).ThenBy(d => d.BonNo);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan Transit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Zona Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    if (model.Type == null || model.Type == OUT)
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.ProductionOrderNo, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Status, item.Remark, item.Machine, item.Grade, item.UomUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), model.DestinationArea, OUT);

                        }
                    }
                    else
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.ProductionOrderNo))
                        {
                            dt.Rows.Add(model.BonNo, item.ProductionOrderNo, item.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Status, item.Remark, item.Machine, item.Grade, item.UomUnit,
                                item.Balance.ToString("N2", CultureInfo.InvariantCulture), model.DestinationArea, ADJ);

                        }
                    }

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Inspection Material") }, true);
        }

        private MemoryStream GenerateExcelOut(OutputInspectionMaterialViewModel viewModel)
        {
            var query = viewModel.InspectionMaterialProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan Transit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Aval", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    foreach (var detail in item.ProductionOrderDetails.Where(s => !s.HasNextAreaDocument))
                    {
                        dt.Rows.Add(item.ProductionOrder.No, item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.CartNo, item.Construction, item.Unit,
                            item.Buyer, item.Color, item.Motif, item.Status, item.Machine, item.UomUnit, detail.Remark, detail.Grade, detail.AvalType,
                            detail.Balance.ToString("N2", CultureInfo.InvariantCulture), "");
                    }

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Inspection Material") }, true);
        }

        private MemoryStream GenerateExcelAdj(OutputInspectionMaterialViewModel viewModel)
        {
            var query = viewModel.InspectionMaterialProductionOrders.OrderBy(s => s.ProductionOrder.No);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrder.No, item.ProductionOrder.OrderQuantity.ToString("N2", CultureInfo.InvariantCulture), item.CartNo, item.Construction, item.Unit,
                               item.Buyer, item.Color, item.Motif, item.UomUnit, item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Inspection Material") }, true);
        }

        public MemoryStream GenerateExcel(OutputInspectionMaterialViewModel viewModel)
        {
            if (viewModel.Type == null || viewModel.Type == OUT)
            {
                return GenerateExcelOut(viewModel);
            }
            else
            {
                return GenerateExcelAdj(viewModel);
            }
        }

        public ListResult<AdjInspectionMaterialProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputProductionOrderRepository.ReadAll()
                .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasOutputDocument)
                .Select(d => new PlainAdjInspectionMaterialProductionOrder()
                {
                    Area = d.Area,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    MaterialConstructionId = d.MaterialConstructionId,
                    MaterialConstructionName = d.MaterialConstructionName,
                    MaterialId = d.MaterialId,
                    MaterialName = d.MaterialName,
                    MaterialWidth = d.MaterialWidth,
                    Motif = d.Motif,
                    ProductionOrderId = d.ProductionOrderId,
                    ProductionOrderNo = d.ProductionOrderNo,
                    ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                    ProductionOrderType = d.ProductionOrderType,
                    Unit = d.Unit,
                    UomUnit = d.UomUnit
                })
                .Union(_outputProductionOrderRepository.ReadAll()
                .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasNextAreaDocument)
                .Select(d => new PlainAdjInspectionMaterialProductionOrder()
                {
                    Area = d.Area,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    MaterialConstructionId = d.MaterialConstructionId,
                    MaterialConstructionName = d.MaterialConstructionName,
                    MaterialId = d.MaterialId,
                    MaterialName = d.MaterialName,
                    MaterialWidth = d.MaterialWidth,
                    Motif = d.Motif,
                    ProductionOrderId = d.ProductionOrderId,
                    ProductionOrderNo = d.ProductionOrderNo,
                    ProductionOrderOrderQuantity = d.ProductionOrderOrderQuantity,
                    ProductionOrderType = d.ProductionOrderType,
                    Unit = d.Unit,
                    UomUnit = d.UomUnit
                }));
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<PlainAdjInspectionMaterialProductionOrder>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjInspectionMaterialProductionOrder>.Filter(query, FilterDictionary);

            var data = query.ToList()
                .GroupBy(d => d.ProductionOrderId)
                .Select(s => s.First())
                .Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.ProductionOrderNo)
                .Select(s => new AdjInspectionMaterialProductionOrderViewModel()
                {
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType

                    },
                    Buyer = s.Buyer,
                    BuyerId = s.BuyerId,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    Material = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = s.MaterialConstructionId,
                        Name = s.MaterialConstructionName
                    },
                    MaterialWidth = s.MaterialWidth,
                    Motif = s.Motif,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit
                });

            return new ListResult<AdjInspectionMaterialProductionOrderViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
