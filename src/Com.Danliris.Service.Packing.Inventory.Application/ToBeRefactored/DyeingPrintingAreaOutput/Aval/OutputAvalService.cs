using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Globalization;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalService : IOutputAvalService
    {
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        public OutputAvalService(IServiceProvider serviceProvider)
        {
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private async Task<OutputAvalViewModel> MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputAvalViewModel();
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                vm = new OutputAvalViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Type = DyeingPrintingArea.OUT,
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
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    DeliveryOrderAvalNo = model.DeliveryOrderAvalNo,
                    DeliveryOrderAvalId = model.DeliveryOrderAvalId,
                    AvalItems = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputAvalItemViewModel()
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
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Id = s.Id,
                        AvalType = s.AvalType,
                        AvalCartNo = s.AvalCartNo,
                        AvalUomUnit = s.UomUnit,
                        AvalQuantity = s.AvalALength,
                        AvalQuantityKg = s.AvalBLength,
                        DeliveryNote = s.DeliveryNote,

                        AvalOutSatuan = s.Balance,
                        AvalOutQuantity = s.AvalQuantityKg,
                        PrevAval = s.PrevSppInJson,
                    }).ToList()
                };

                foreach (var item in vm.AvalItems.GroupBy(s => s.AvalType))
                {
                    if (vm.DestinationArea == DyeingPrintingArea.BUYER)
                    {
                        foreach (var detail in item)
                        {

                            detail.AvalQuantity = detail.AvalOutSatuan;
                            detail.AvalQuantityKg = detail.AvalOutQuantity;
                        }
                    }
                    else
                    {
                        var inputData = _inputRepository.GetDbSet().Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL && s.AvalType == item.Key && s.IsTransformedAval);
                        var totalQuantity = inputData.Sum(s => s.TotalAvalQuantity);
                        var totalKg = inputData.Sum(s => s.TotalAvalWeight);
                        var sumSatuan = item.Sum(s => s.AvalOutSatuan);
                        var sumKG = item.Sum(s => s.AvalOutQuantity);

                        foreach (var detail in item)
                        {
                            detail.AvalQuantity = totalQuantity + sumSatuan;
                            detail.AvalQuantityKg = totalKg + sumKG;
                        }

                    }
                }
            }
            else
            {
                vm = new OutputAvalViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Type = DyeingPrintingArea.ADJ,
                    Area = model.Area,
                    AdjType = model.Type,
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
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    AvalItems = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputAvalItemViewModel()
                    {
                        AdjDocumentNo = s.AdjDocumentNo,
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
                        HasNextAreaDocument = s.HasNextAreaDocument,
                        Id = s.Id,
                        AvalType = s.AvalType,
                        AvalTransformationId = s.DyeingPrintingAreaInputProductionOrderId,
                        AvalUomUnit = s.UomUnit,
                        AvalCartNo = s.CartNo,
                        AvalQuantity = s.Balance,
                        AvalQuantityKg = s.AvalQuantityKg,
                        PrevAval = s.PrevSppInJson
                    }).ToList()
                };

                foreach (var item in vm.AvalItems)
                {
                    var avalTransform = await _inputRepository.ReadByIdAsync(item.AvalTransformationId);
                    if (avalTransform != null)
                    {
                        item.AvalQuantityBalance = avalTransform.TotalAvalQuantity - item.AvalQuantity;
                        item.AvalWeightBalance = avalTransform.TotalAvalWeight - item.AvalQuantityKg;
                    }
                }
            }


            return vm;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {

            if (destinationArea == DyeingPrintingArea.PENJUALAN)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GA, DyeingPrintingArea.PJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationArea == DyeingPrintingArea.BUYER)
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GA, DyeingPrintingArea.BY, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GA, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtySatuan, IEnumerable<double> qtyWeight)
        {
            if (qtySatuan.All(s => s > 0) && qtyWeight.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_IN, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.ADJ_OUT, DyeingPrintingArea.GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputAvalViewModel viewModel)
        {
            int result = 0;

            var model = _outputRepository.GetDbSet().AsNoTracking()
                .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGAVAL && s.DestinationArea == viewModel.DestinationArea
                && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == DyeingPrintingArea.OUT && s.DeliveryOrderAvalId == viewModel.DeliveryOrderAvalId);

            if (model == null)
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                                                               s.DestinationArea == viewModel.DestinationArea &&
                                                                                               s.CreatedUtc.Year == viewModel.Date.Year &&
                                                                                               s.Type == DyeingPrintingArea.OUT);
                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                List<DyeingPrintingAreaOutputProductionOrderModel> productionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();

                foreach (var item in viewModel.AvalItems)
                {
                    if (viewModel.DestinationArea == DyeingPrintingArea.PENJUALAN)
                    {
                        var transform = await _inputRepository.UpdateAvalTransformationFromOut(item.AvalType, item.AvalOutSatuan, item.AvalOutQuantity);
                        result += transform.Item1;
                        var prevAval = JsonConvert.SerializeObject(transform.Item2);
                        var productionOrder = new DyeingPrintingAreaOutputProductionOrderModel(item.AvalType, item.AvalCartNo, item.AvalUomUnit, item.AvalOutSatuan, item.AvalOutQuantity, item.AvalQuantity,
                            item.AvalQuantityKg, viewModel.Area, viewModel.DestinationArea, item.DeliveryNote, prevAval, 0);
                        productionOrders.Add(productionOrder);
                    }
                    else
                    {
                        result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                        var productionOrder = new DyeingPrintingAreaOutputProductionOrderModel(item.AvalType, item.AvalCartNo, item.AvalUomUnit, item.AvalOutSatuan, item.AvalOutQuantity, item.AvalQuantity,
                            item.AvalQuantityKg, viewModel.Area, viewModel.DestinationArea, item.DeliveryNote, "[]", item.Id);
                        productionOrders.Add(productionOrder);
                    }
                }

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, viewModel.DeliveryOrderAvalNo, viewModel.DeliveryOrderAvalId, false,
                    viewModel.DestinationArea, viewModel.Group, viewModel.Type, productionOrders);
                result += await _outputRepository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    if (viewModel.DestinationArea == DyeingPrintingArea.BUYER)
                    {
                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Balance, item.AvalQuantityKg, item.AvalType);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }

                }
            }
            else
            {
                foreach (var item in viewModel.AvalItems)
                {
                    DyeingPrintingAreaOutputProductionOrderModel modelItem = null;
                    if (viewModel.DestinationArea == DyeingPrintingArea.PENJUALAN)
                    {
                        var transform = await _inputRepository.UpdateAvalTransformationFromOut(item.AvalType, item.AvalOutSatuan, item.AvalOutQuantity);
                        result += transform.Item1;
                        var prevAval = JsonConvert.SerializeObject(transform.Item2);
                        modelItem = new DyeingPrintingAreaOutputProductionOrderModel(item.AvalType, item.AvalCartNo, item.AvalUomUnit, item.AvalOutSatuan, item.AvalOutQuantity, item.AvalQuantity,
                            item.AvalQuantityKg, viewModel.Area, viewModel.DestinationArea, item.DeliveryNote, prevAval, 0);
                    }
                    else
                    {
                        result += await _outputProductionOrderRepository.UpdateFromInputNextAreaFlagAsync(item.Id, true);
                        modelItem = new DyeingPrintingAreaOutputProductionOrderModel(item.AvalType, item.AvalCartNo, item.AvalUomUnit, item.AvalOutSatuan, item.AvalOutQuantity, item.AvalQuantity,
                            item.AvalQuantityKg, viewModel.Area, viewModel.DestinationArea, item.DeliveryNote, "[]", item.Id);

                        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, modelItem.ProductionOrderId, modelItem.ProductionOrderNo,
                          modelItem.CartNo, modelItem.Buyer, modelItem.Construction, modelItem.Unit, modelItem.Color, modelItem.Motif, modelItem.UomUnit, modelItem.Balance, modelItem.Id, modelItem.ProductionOrderType, modelItem.Balance, modelItem.AvalQuantityKg, modelItem.AvalType);

                        result += await _movementRepository.InsertAsync(movementModel);
                    }

                    modelItem.DyeingPrintingAreaOutputId = model.Id;
                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);
                }
            }

            return result;
        }

        private async Task<int> CreateAdj(OutputAvalViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.AvalItems.All(d => d.AvalQuantity > 0 && d.AvalQuantityKg > 0))
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING && s.Type == DyeingPrintingArea.ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.AvalItems.Select(d => d.AvalQuantity), viewModel.AvalItems.Select(d => d.AvalQuantityKg));
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == DyeingPrintingArea.SHIPPING && s.Type == DyeingPrintingArea.ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.AvalItems.Select(d => d.AvalQuantity), viewModel.AvalItems.Select(d => d.AvalQuantityKg));
                type = DyeingPrintingArea.ADJ_OUT;
            }

            DyeingPrintingAreaOutputModel model = _outputRepository.GetDbSet().AsNoTracking()
                  .FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGAVAL && s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.Type == type);

            if (model == null)
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group,
                       type, viewModel.AvalItems.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo, s.AvalTransformationId)).ToList());

                result = await _outputRepository.InsertAsync(model);

                foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    var avalTransform = await _inputRepository.ReadByIdAsync(item.DyeingPrintingAreaInputProductionOrderId);
                    if (avalTransform != null)
                    {

                        result += await _inputRepository.UpdateHeaderAvalTransform(avalTransform, item.Balance, item.AvalQuantityKg);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Balance, item.AvalQuantityKg, item.AvalType);
                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            else
            {
                foreach (var item in viewModel.AvalItems)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, true, item.AvalType, item.AvalQuantity, item.AvalQuantityKg, item.AdjDocumentNo, item.AvalTransformationId);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    var avalTransform = await _inputRepository.ReadByIdAsync(item.AvalTransformationId);
                    if (avalTransform != null)
                    {

                        result += await _inputRepository.UpdateHeaderAvalTransform(avalTransform, item.AvalQuantity, item.AvalQuantityKg);
                    }

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, modelItem.ProductionOrderId, modelItem.ProductionOrderNo,
                      modelItem.CartNo, modelItem.Buyer, modelItem.Construction, modelItem.Unit, modelItem.Color, modelItem.Motif, modelItem.UomUnit, modelItem.Balance, modelItem.Id, modelItem.ProductionOrderType, modelItem.Balance, modelItem.AvalQuantityKg, modelItem.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);



                }
            }

            return result;
        }

        public async Task<int> Create(OutputAvalViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == DyeingPrintingArea.OUT)
            {
                result = await CreateOut(viewModel);
            }
            else
            {
                result = await CreateAdj(viewModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page,
                                               int size,
                                               string filter,
                                               string order,
                                               string keyword)
        {
            //var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
            //((s.Type == OUT || s.Type == null) && !s.HasNextAreaDocument || (s.Type != OUT && s.Type != null)));
            var query = _outputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL);
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
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
                Type = s.Type == null || s.Type == DyeingPrintingArea.OUT ? DyeingPrintingArea.OUT : DyeingPrintingArea.ADJ,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadAvailableAval(DateTimeOffset searchDate,
                                                                         string searchShift,
                                                                         string searchGroup,
                                                                         int page,
                                                                         int size,
                                                                         string filter,
                                                                         string order,
                                                                         string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Date <= searchDate &&
                                                         s.Shift == searchShift &&
                                                         s.Group == searchGroup &&
                                                         s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query.Skip((page - 1) * size).Take(size))
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public async Task<OutputAvalViewModel> ReadById(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputAvalViewModel vm = await MapToViewModel(model);

            return vm;
        }

        private MemoryStream GenerateExcelOut(DyeingPrintingAreaOutputModel model)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders;

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NAMA BARANG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KET", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KG", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(indexNumber,
                                item.AvalType,
                                item.Balance,
                                "KRG",
                                item.AvalQuantityKg);
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");
            sheet.Cells[1, 1].Value = "DIVISI";
            sheet.Cells[1, 2].Value = "DYEING PRINTING PT DANLIRIS";

            sheet.Cells[2, 1].Value = "TANGGAL";
            sheet.Cells[2, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = model.Shift;

            sheet.Cells[4, 1].Value = "MUTASI";
            sheet.Cells[4, 2].Value = "KELUAR";

            sheet.Cells[5, 1].Value = "ZONA";
            sheet.Cells[5, 2].Value = model.DestinationArea;
            sheet.Cells[5, 2, 5, 3].Merge = true;

            sheet.Cells[7, 1].Value = "BON PENYERAHAN BARANG";
            sheet.Cells[7, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 1, 7, 5].Merge = true;

            sheet.Cells[8, 1].Value = "PT. DANLIRIS";
            sheet.Cells[8, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[8, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[8, 1, 8, 5].Merge = true;

            sheet.Cells[9, 1].Value = "SUKOHARJO";
            sheet.Cells[9, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[9, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[9, 1, 9, 3].Merge = true;

            sheet.Cells[10, 1].Value = "Dari Seksi/ Bagian :";
            sheet.Cells[10, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[10, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[10, 1, 10, 3].Merge = true;
            //sheet.Cells[10, 4].Value = model.OriginSection;

            sheet.Cells[11, 1].Value = "Untuk Seksi/ Bagian :";
            sheet.Cells[11, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[11, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[11, 1, 11, 3].Merge = true;
            //sheet.Cells[11, 4].Value = model.DestinationSection;

            sheet.Cells[12, 1].Value = "Yang Menerima,";
            sheet.Cells[12, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[12, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[12, 1, 12, 2].Merge = true;

            sheet.Cells[12, 4].Value = "Yang Menyerahkan,";
            sheet.Cells[12, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[12, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[12, 4, 12, 5].Merge = true;

            //sheet.Cells[15, 1].Value = "( " + model.ReceiveOperator + " )";
            sheet.Cells[15, 1].Value = "(  )";
            sheet.Cells[15, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[15, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[15, 1, 15, 2].Merge = true;

            //sheet.Cells[15, 1].Value = "( " + model.SubmitOperator + " )";
            sheet.Cells[15, 4].Value = "(  )";
            sheet.Cells[15, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[15, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[15, 4, 15, 5].Merge = true;

            sheet.Cells[16, 1].Value = "NO.";
            sheet.Cells[16, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 1].AutoFitColumns();
            sheet.Cells[16, 1, 17, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Merge = true;

            sheet.Cells[16, 2].Value = "NAMA BARANG";
            sheet.Cells[16, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 2].AutoFitColumns();
            sheet.Cells[16, 2, 17, 2].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Merge = true;

            sheet.Cells[16, 3].Value = "SAT";
            sheet.Cells[16, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 3].AutoFitColumns();
            sheet.Cells[16, 3, 16, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3, 16, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3, 16, 4].Merge = true;

            sheet.Cells[17, 3].Value = "QTY";
            sheet.Cells[17, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[17, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[17, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[17, 3].AutoFitColumns();

            sheet.Cells[17, 4].Value = "KET";
            sheet.Cells[17, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[17, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[17, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[17, 4].AutoFitColumns();

            sheet.Cells[16, 5].Value = "KG";
            sheet.Cells[16, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 5].AutoFitColumns();
            sheet.Cells[16, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Merge = true;
            #endregion

            int tableRowStart = 18;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();
            //sheet.Cells[tableRowStart, tableColStart].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Aval Area Dyeing Printing") }, true);
            return stream;
        }

        private MemoryStream GenerateExcelAdj(DyeingPrintingAreaOutputModel model)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.AvalType);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar Berat", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.AvalType, item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture),
                        item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Gudang Aval") }, true);
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return GenerateExcelOut(model);
            }
            else
            {
                return GenerateExcelAdj(model);
            }

        }

        public ListResult<AvailableAvalIndexViewModel> ReadAllAvailableAval(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query)
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadByBonAvailableAval(int bonId, int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.Id == bonId &&
                                                         s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query)
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadByTypeAvailableAval(string avalType, int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputRepository.ReadAll().Where(s =>
            //                                             s.AvalType == avalType &&
            //                                             s.Area == DyeingPrintingArea.GUDANGAVAL &&
            //                                             s.IsTransformedAval &&
            //                                             s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.AvalType == avalType &&
                                                         s.Area == DyeingPrintingArea.GUDANGAVAL &&
                                                         s.IsTransformedAval);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            var queryGroup = query.GroupBy(
                s => s.AvalType,
                s => s,
                (key, item) => new { AvalType = key, AvalItem = item }
                );
            foreach (var avalInput in queryGroup)
            {
                var avalItems = new AvailableAvalIndexViewModel()
                {

                    AvalType = avalInput.AvalType,
                    AvalUomUnit = avalInput.AvalItem.First().DyeingPrintingAreaInputProductionOrders.FirstOrDefault().UomUnit,
                    AvalQuantity = avalInput.AvalItem.Sum(s => s.TotalAvalQuantity),
                    AvalQuantityKg = avalInput.AvalItem.Sum(s => s.TotalAvalWeight),
                };

                data.Add(avalItems);
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AdjAvalItemViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            //var query = _inputRepository.ReadAll()
            //     .Where(s => s.Area == GUDANGAVAL && s.IsTransformedAval && s.TotalAvalQuantity != 0 && s.TotalAvalWeight != 0)
            //     .Select(d => new PlainAdjAvalItem()
            //     {
            //         AvalType = d.AvalType,
            //         AvalQuantity = d.TotalAvalQuantity,
            //         AvalQuantityKg = d.TotalAvalWeight
            //     })
            //     .Union(_outputProductionOrderRepository.ReadAll()
            //     .Where(s => s.Area == GUDANGAVAL && !s.HasNextAreaDocument)
            //     .Select(d => new PlainAdjAvalItem()
            //     {
            //         AvalType = d.AvalType,
            //         AvalQuantity = d.Balance,
            //         AvalQuantityKg = d.AvalQuantityKg
            //     }));

            var query = _inputRepository.ReadAll()
                 .Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL && s.IsTransformedAval && (s.TotalAvalQuantity != 0 || s.TotalAvalWeight != 0))
                 .Select(d => new PlainAdjAvalItem()
                 {
                     Id = d.Id,
                     AvalType = d.AvalType,
                     AvalQuantity = d.TotalAvalQuantity,
                     AvalQuantityKg = d.TotalAvalWeight
                 });
            List<string> SearchAttributes = new List<string>()
            {
                "AvalType"
            };

            query = QueryHelper<PlainAdjAvalItem>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjAvalItem>.Filter(query, FilterDictionary);

            var data = query.ToList()
                //.GroupBy(d => d.AvalType)
                //.Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.AvalType)
                .Select(s => new AdjAvalItemViewModel()
                {
                    AvalTransformationId = s.Id,
                    AvalType = s.AvalType,
                    AvalQuantity = s.AvalQuantity,
                    AvalQuantityKg = s.AvalQuantityKg
                });

            return new ListResult<AdjAvalItemViewModel>(data.ToList(), page, size, query.Count());
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            //var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
            //    (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));
            var query = _outputRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL);


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

            dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo Karung", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo KG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Keluar Karung", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Keluar KG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
                    {
                        //foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.AvalType))
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.AvalType))
                        {
                            dt.Rows.Add(model.BonNo, item.AvalType, item.AvalALength.ToString("N2", CultureInfo.InvariantCulture), item.AvalBLength.ToString("N2", CultureInfo.InvariantCulture),
                               item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture), DyeingPrintingArea.OUT);

                        }

                    }
                    else
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.AvalType))
                        {
                            dt.Rows.Add(model.BonNo, item.AvalType, item.AvalALength.ToString("N2", CultureInfo.InvariantCulture), item.AvalBLength.ToString("N2", CultureInfo.InvariantCulture),
                               item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture), DyeingPrintingArea.ADJ);

                        }
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Gudang Aval") }, true);
        }

        private async Task<int> UpdateOut(int id, OutputAvalViewModel viewModel)
        {
            int result = 0;
            var dbModel = await _outputRepository.ReadByIdAsync(id);


            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, viewModel.DeliveryOrderAvalNo, viewModel.DeliveryOrderAvalId, false,
                    viewModel.DestinationArea, viewModel.Group, viewModel.Type, viewModel.AvalItems.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType, s.AvalCartNo, s.AvalUomUnit, s.AvalOutSatuan, s.AvalOutQuantity, s.AvalQuantity,
                            s.AvalQuantityKg, viewModel.Area, viewModel.DestinationArea, s.DeliveryNote, s.PrevAval, 0)
                    {
                        Id = s.Id
                    }).ToList());

            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, double> dictWeight = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;
                    var diffWeight = lclModel.AvalQuantityKg - item.AvalQuantityKg;
                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictWeight.Add(lclModel.Id, diffWeight);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !s.HasNextAreaDocument && !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _outputRepository.UpdateAvalArea(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument && !d.IsDeleted))
            {
                double newBalance = 0;
                double newWeight = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }
                if (!dictWeight.TryGetValue(item.Id, out newWeight))
                {
                    newWeight = item.AvalQuantityKg;
                }
                if ((newBalance != 0 || newWeight != 0) && dbModel.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                           item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, newBalance, newWeight, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                if (dbModel.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Balance * -1, item.AvalQuantityKg * -1, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }

            }

            return result;
        }

        private async Task<int> UpdateAdj(int id, OutputAvalViewModel viewModel)
        {
            string type;
            int result = 0;
            var dbModel = await _outputRepository.ReadByIdAsync(id);
            if (viewModel.AvalItems.All(d => d.AvalQuantity > 0 && d.AvalQuantityKg > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNo, true, "", viewModel.Group,
                       type, viewModel.AvalItems.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo, s.AvalTransformationId)
                    {
                        Id = s.Id
                    }).ToList());
            Dictionary<int, double> dictBalance = new Dictionary<int, double>();
            Dictionary<int, double> dictWeight = new Dictionary<int, double>();
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders)
            {
                var lclModel = model.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault(s => s.Id == item.Id);
                if (lclModel != null)
                {
                    var diffBalance = lclModel.Balance - item.Balance;
                    var diffWeight = lclModel.AvalQuantityKg - item.AvalQuantityKg;

                    dictBalance.Add(lclModel.Id, diffBalance);
                    dictWeight.Add(lclModel.Id, diffWeight);
                }
            }

            var deletedData = dbModel.DyeingPrintingAreaOutputProductionOrders.Where(s => !model.DyeingPrintingAreaOutputProductionOrders.Any(d => d.Id == s.Id)).ToList();

            result = await _outputRepository.UpdateAdjustmentDataAval(id, model, dbModel);
            foreach (var item in dbModel.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.IsDeleted))
            {
                double newBalance = 0;
                double newWeight = 0;
                if (!dictBalance.TryGetValue(item.Id, out newBalance))
                {
                    newBalance = item.Balance;
                }

                if (!dictWeight.TryGetValue(item.Id, out newWeight))
                {
                    newWeight = item.AvalQuantityKg;
                }

                if (newBalance != 0 || newWeight != 0)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, newBalance, item.Id, item.ProductionOrderType, newBalance, newWeight, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);
                }


            }
            foreach (var item in deletedData)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Balance * -1, item.AvalQuantityKg * -1, item.AvalType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Update(int id, OutputAvalViewModel viewModel)
        {
            if (viewModel.Type == DyeingPrintingArea.OUT)
            {
                return await UpdateOut(id, viewModel);
            }
            else
            {
                return await UpdateAdj(id, viewModel);
            }
        }

        private async Task<int> DeleteOut(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                if (!item.HasNextAreaDocument && model.DestinationArea != DyeingPrintingArea.PENJUALAN)
                {
                    var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, DyeingPrintingArea.OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                            item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Balance * -1, item.AvalQuantityKg * -1, item.AvalType);

                    result += await _movementRepository.InsertAsync(movementModel);

                }
            }
            result += await _outputRepository.DeleteAvalArea(model);

            return result;
        }

        private async Task<int> DeleteAdj(DyeingPrintingAreaOutputModel model)
        {
            int result = 0;
            string type;
            if (model.DyeingPrintingAreaOutputProductionOrders.All(d => d.Balance > 0))
            {
                type = DyeingPrintingArea.ADJ_IN;
            }
            else
            {
                type = DyeingPrintingArea.ADJ_OUT;
            }
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(model.Date, model.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance * -1, item.Id, item.ProductionOrderType, item.Balance * -1, item.AvalQuantityKg * -1, item.AvalType);
                result += await _movementRepository.InsertAsync(movementModel);


            }
            result += await _outputRepository.DeleteAdjustmentAval(model);

            return result;
        }

        public async Task<int> Delete(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == DyeingPrintingArea.OUT)
            {
                return await DeleteOut(model);
            }
            else
            {
                return await DeleteAdj(model);
            }
        }
    }
}
