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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialService : IOutputInspectionMaterialService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string TYPE = "OUT";

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

        private const string AVALA = "Aval A";
        private const string AVALB = "Aval B";
        private const string AVALCONNECTION = "Aval Sambungan";

        public OutputInspectionMaterialService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private OutputInspectionMaterialViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputInspectionMaterialViewModel()
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
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                DestinationArea = model.DestinationArea,
                HasNextAreaDocument = model.HasNextAreaDocument,
                InspectionMaterialProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputInspectionMaterialProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    CartNo = s.CartNo,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    AvalALength = s.AvalALength,
                    AvalBLength = s.AvalBLength,
                    AvalConnectionLength = s.AvalConnectionLength,

                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Grade = s.Grade,
                    PackingInstruction = s.PackingInstruction,
                    Remark = s.Remark,
                    Status = s.Status,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    Motif = s.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        Type = s.ProductionOrderType,
                        OrderQuantity = s.ProductionOrderOrderQuantity
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit
                }).ToList()
            };


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
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", IM, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

            }

        }

        public async Task<int> Create(OutputInspectionMaterialViewModel viewModel)
        {
            int result = 0;
            var model = _repository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == INSPECTIONMATERIAL && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, viewModel.InspectionMaterialProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                     s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance,
                     s.AvalItems.FirstOrDefault(e => e.Type == AVALA) == null ? 0 : s.AvalItems.FirstOrDefault(e => e.Type == AVALA).Length,
                     s.AvalItems.FirstOrDefault(e => e.Type == AVALB) == null ? 0 : s.AvalItems.FirstOrDefault(e => e.Type == AVALB).Length,
                     s.AvalItems.FirstOrDefault(e => e.Type == AVALCONNECTION) == null ? 0 : s.AvalItems.FirstOrDefault(e => e.Type == AVALCONNECTION).Length, 
                     s.Id)).ToList());

                result = await _repository.InsertAsync(model);
                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {

                    //if (viewModel.DestinationArea == GUDANGAVAL)
                    //{
                    //    var avalA = item.AvalItems.FirstOrDefault(s => s.Type == AVALA)?.Length;
                    //    var avalB = item.AvalItems.FirstOrDefault(s => s.Type == AVALB)?.Length;
                    //    var avalConnection = item.AvalItems.FirstOrDefault(s => s.Type == AVALCONNECTION)?.Length;
                    //    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.Id, item.Balance, avalA.GetValueOrDefault(), avalB.GetValueOrDefault(),
                    //        avalConnection.GetValueOrDefault());

                    //}
                    //else
                    //{

                    //    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.Id, item.Balance, 0, 0, 0);

                    //}
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    result += await _movementRepository.InsertAsync(movementModel);

                    if (previousSummary == null)
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                }
            }
            else
            {
                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                         item.Unit, item.Color, item.Motif, item.UomUnit, item.Remark, item.Grade, item.Status, item.Balance,
                         item.AvalItems.FirstOrDefault(e => e.Type == AVALA) == null ? 0 : item.AvalItems.FirstOrDefault(e => e.Type == AVALA).Length,
                         item.AvalItems.FirstOrDefault(e => e.Type == AVALB) == null ? 0 : item.AvalItems.FirstOrDefault(e => e.Type == AVALB).Length,
                         item.AvalItems.FirstOrDefault(e => e.Type == AVALCONNECTION) == null ? 0 : item.AvalItems.FirstOrDefault(e => e.Type == AVALCONNECTION).Length,
                         item.Id);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    //if (viewModel.DestinationArea == GUDANGAVAL)
                    //{

                    //    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.Id, item.Balance, item.AvalALength, item.AvalBLength, item.AvalConnectionLength);

                    //}
                    //else
                    //{

                    //    result += await _inputProductionOrderRepository.UpdateFromOutputIMAsync(item.Id, item.Balance, 0, 0, 0);

                    //}
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId && s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);
                    result += await _movementRepository.InsertAsync(movementModel);

                    if (previousSummary == null)
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }

                }
            }


            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument));
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
                DestinationArea = s.DestinationArea,
                Group = s.Group,
                HasNextAreaDocument = s.HasNextAreaDocument,
                InspectionMaterialProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputInspectionMaterialProductionOrderViewModel()
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
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    Status = d.Status,
                    PackingInstruction = d.PackingInstruction,
                    UomUnit = d.UomUnit
                }).ToList()
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputInspectionMaterialViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputInspectionMaterialViewModel vm = MapToViewModel(model);

            return vm;
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            var query = model.DyeingPrintingAreaOutputProductionOrders;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", 0, "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.ProductionOrderNo, item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Remark, item.Grade, item.UomUnit, item.Balance,
                        "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon IM Area Dyeing Printing") }, true);
        }

        public List<InputInspectionMaterialProductionOrderViewModel> GetInputInspectionMaterialProductionOrders()
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll()
                .Where(s => s.Area == INSPECTIONMATERIAL && !s.HasOutputDocument);
            var data = productionOrders.Select(s => new InputInspectionMaterialProductionOrderViewModel()
            {
                AvalALength = s.AvalALength,
                AvalBLength = s.AvalBLength,
                AvalConnectionLength = s.AvalConnectionLength,
                Balance = s.Balance,
                Buyer = s.Buyer,
                CartNo = s.CartNo,
                Color = s.Color,
                Construction = s.Construction,
                Grade = s.Grade,
                HasOutputDocument = s.HasOutputDocument,
                InitLength = s.InitLength,
                Motif = s.Motif,
                PackingInstruction = s.PackingInstruction,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = s.ProductionOrderOrderQuantity,
                    Type = s.ProductionOrderType
                },
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                Id = s.Id,
                InputId = s.DyeingPrintingAreaInputId

            });

            return data.ToList();
        }
    }
}
