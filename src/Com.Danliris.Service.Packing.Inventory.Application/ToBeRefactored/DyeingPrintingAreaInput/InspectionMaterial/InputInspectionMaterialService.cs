using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
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
using System.IO;
using System.Data;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial
{
    public class InputInspectionMaterialService : IInputInspectionMaterialService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;

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
                    Grade = s.Grade,
                    InitLength = s.InitLength,
                    InputQuantity = s.InputQuantity,
                    MaterialWidth = s.MaterialWidth,
                    FinishWidth = s.FinishWidth,
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
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
            return string.Format("{0}.{1}.{2}", DyeingPrintingArea.IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }


        public async Task<int> Create(InputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.INSPECTIONMATERIAL && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.INSPECTIONMATERIAL && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);
                model = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.Group, viewModel.InspectionMaterialProductionOrders.Select(s =>
                     new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity,
                     s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, s.InputQuantity, false, s.BuyerId, 0, s.Material.Id, s.Material.Name,
                     s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.InputQuantity, s.FinishWidth, viewModel.Date)).ToList());
                   

                result = await _repository.InsertAsync(model);
                foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }
            }
            else
            {
                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color,
                        item.Motif, item.UomUnit, item.InputQuantity, item.InputQuantity, false, item.BuyerId, 0, item.Material.Id, item.Material.Name,
                        item.MaterialConstruction.Id, item.MaterialConstruction.Name, item.MaterialWidth, item.ProcessType.Id, item.ProcessType.Name, item.YarnMaterial.Id, item.YarnMaterial.Name,
                        item.InputQuantity, item.FinishWidth,viewModel.Date);
                       
                    modelItem.DyeingPrintingAreaInputId = model.Id;

                    result += await _productionOrderRepository.InsertAsync(modelItem);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.InputQuantity, modelItem.Id, item.ProductionOrder.Type);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.INSPECTIONMATERIAL);
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
                    BalanceRemains = d.BalanceRemains,
                    InputQuantity = d.InputQuantity,
                    BuyerId = d.BuyerId,
                    CartNo = d.CartNo,
                    Color = d.Color,
                    Construction = d.Construction,
                    HasOutputDocument = d.HasOutputDocument,
                    Motif = d.Motif,
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType,
                        OrderQuantity = d.ProductionOrderOrderQuantity
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
                    Grade = d.Grade,
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
                BalanceRemains = s.BalanceRemains,
                InputQuantity = s.InputQuantity,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                BuyerId = s.BuyerId,
                Color = s.Color,
                Construction = s.Construction,
                HasOutputDocument = s.HasOutputDocument,
                IsChecked = s.IsChecked,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                MaterialWidth = s.MaterialWidth,
                FinishWidth = s.FinishWidth,
                Material = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.ProcessTypeId,
                    Name = s.ProcessTypeName
                },
                Grade = s.Grade,
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = s.MaterialConstructionName,
                    Id = s.MaterialConstructionId
                },
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType
                },
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                DateIn=s.DateIn,
                DateOut=s.DateOut
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
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, DyeingPrintingArea.IN, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

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
                 s.PackingInstruction, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.UomUnit, s.InputQuantity, s.InputQuantity, s.HasOutputDocument, s.BuyerId, 0, s.Material.Id,
                 s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.InputQuantity, s.FinishWidth,viewModel.Date)
               //  s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.ProcessType.Id, s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.InputQuantity, viewModel.Date)
                 { Id = s.Id }).ToList());
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaInputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaInputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.InputQuantity - item.InputQuantity;

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
                    newBalance = item.InputQuantity;
                }

                if (newBalance != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, DyeingPrintingArea.IN, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(dbModel.Date, dbModel.Area, DyeingPrintingArea.IN, dbModel.Id, dbModel.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _repository.ReadAll()
            //    .Where(s => s.Area == INSPECTIONMATERIAL && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            var query = _repository.ReadAll().Where(s => s.Area == DyeingPrintingArea.INSPECTIONMATERIAL);
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
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Terima", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "","", "", "", "", "", "", "", "");
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
                            item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.UomUnit, item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture));
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Inspection Material") }, true);
        }
    }
}
