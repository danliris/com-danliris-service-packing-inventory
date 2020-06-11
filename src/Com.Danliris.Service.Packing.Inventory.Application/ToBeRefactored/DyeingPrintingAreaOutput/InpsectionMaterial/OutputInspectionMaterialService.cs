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

        private async Task<OutputInspectionMaterialViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
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
                    Status = sppData.Status,
                    Unit = sppData.Unit,
                    UomUnit = sppData.UomUnit,
                    Balance = inputData.Balance,
                    BalanceRemains = inputData.BalanceRemains + item.Sum(d => d.Balance),
                    ProductionOrderDetails = item.Select(d => new OutputInspectionMaterialProductionOrderDetailViewModel()
                    {
                        Active = d.Active,
                        //AvalItems = d.DyeingPrintingAreaOutputAvalItems.Select(e => new AvalItem()
                        //{
                        //    Active = e.Active,
                        //    CreatedAgent = e.CreatedAgent,
                        //    CreatedBy = e.CreatedBy,
                        //    CreatedUtc = e.CreatedUtc,
                        //    DeletedAgent = e.DeletedAgent,
                        //    DeletedBy = e.DeletedBy,
                        //    DeletedUtc = e.DeletedUtc,
                        //    Id = e.Id,
                        //    IsDeleted = e.IsDeleted,
                        //    LastModifiedAgent = e.LastModifiedAgent,
                        //    LastModifiedBy = e.LastModifiedBy,
                        //    LastModifiedUtc = e.LastModifiedUtc,
                        //    Length = e.Length,
                        //    Type = e.Type
                        //}).ToList(),
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

            viewModel.InspectionMaterialProductionOrders = viewModel.InspectionMaterialProductionOrders.Where(s => s.IsSave).ToList();
            if (model == null)
            {
                int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
                List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

                foreach (var item in viewModel.InspectionMaterialProductionOrders)
                {
                    foreach (var detail in item.ProductionOrderDetails)
                    {
                        var outputProductionOrder = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id,
                            item.ProductionOrder.No, item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                            item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId, detail.AvalType);
                        productionOrders.Add(outputProductionOrder);
                    }
                }

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, productionOrders);

                result = await _repository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    var itemVM = viewModel.InspectionMaterialProductionOrders.FirstOrDefault(s => s.Id == item.DyeingPrintingAreaInputProductionOrderId);
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.DyeingPrintingAreaInputProductionOrderId, item.Balance);
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == itemVM.InputId && s.ProductionOrderId == item.ProductionOrderId);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id);

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
                    foreach (var detail in item.ProductionOrderDetails)
                    {
                        var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, item.ProductionOrder.Id, item.ProductionOrder.No,
                        item.ProductionOrder.Type, item.ProductionOrder.OrderQuantity, item.PackingInstruction, item.CartNo, item.Buyer, item.Construction,
                         item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance,
                         item.Id, item.BuyerId, detail.AvalType);
                        modelItem.DyeingPrintingAreaOutputId = model.Id;

                        result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, detail.Balance);
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, detail.Balance, modelItem.Id);

                        result += await _movementRepository.InsertAsync(movementModel);

                        var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId && s.ProductionOrderId == item.ProductionOrder.Id);

                        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrder.Id, item.ProductionOrder.No,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, detail.Balance, modelItem.Id);


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
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
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
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                Id = s.Id,
                InputId = s.DyeingPrintingAreaInputId

            });

            return data.ToList();
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var model = await _repository.ReadByIdAsync(id);

            //if (model.DyeingPrintingAreaOutputProductionOrders.Any(s => s.HasNextAreaDocument))
            //{
            //    throw new Exception(string.Format("Ada SPP yang Sudah Dibuat di Penerimaan Area {0}!", model.DestinationArea));
            //}

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _repository.DeleteIMArea(model);

            return result;
        }

        public async Task<int> Update(int id, OutputInspectionMaterialViewModel viewModel)
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
                        item.Unit, item.Color, item.Motif, item.UomUnit, detail.Remark, detail.Grade, item.Status, detail.Balance, item.Id, item.BuyerId, detail.AvalType)
                    {
                        Id = detail.Id
                    };
                    productionOrders.Add(outputProductionOrder);
                }
            }

            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.HasNextAreaDocument, viewModel.DestinationArea, viewModel.Group, productionOrders);
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

            //if (deletedData.Any(item => item.HasNextAreaDocument))
            //{
            //    throw new Exception(string.Format("Ada SPP yang Sudah Dibuat di Penerimaan Area {0}!", dbModel.DestinationArea));
            //}
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
