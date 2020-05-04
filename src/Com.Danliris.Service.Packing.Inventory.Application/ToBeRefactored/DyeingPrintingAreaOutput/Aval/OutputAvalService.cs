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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    //public class OutputAvalService : IOutputAvalService
    //{
        //private readonly IDyeingPrintingAreaOutputRepository _repository;
        //private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        //private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        //private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;

        //private const string TYPE = "OUT";

        //private const string IM = "IM";
        //private const string TR = "TR";
        //private const string PC = "PC";
        //private const string GJ = "GJ";
        //private const string GA = "GA";
        //private const string SP = "SP";

        //private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        //private const string TRANSIT = "TRANSIT";
        //private const string PACKING = "PACKING";
        //private const string GUDANGJADI = "GUDANG JADI";
        //private const string GUDANGAVAL = "GUDANG AVAL";
        //private const string SHIPPING = "SHIPPING";

        //public OutputAvalService(IServiceProvider serviceProvider)
        //{
        //    _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
        //    _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        //    _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
        //    _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        //}

        //private OutputAvalViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        //{
        //    var vm = new OutputAvalViewModel()
        //    {
        //        Active = model.Active,
        //        Id = model.Id,
        //        Area = model.Area,
        //        BonNo = model.BonNo,
        //        CreatedAgent = model.CreatedAgent,
        //        CreatedBy = model.CreatedBy,
        //        CreatedUtc = model.CreatedUtc,
        //        Date = model.Date,
        //        DeletedAgent = model.DeletedAgent,
        //        DeletedBy = model.DeletedBy,
        //        DeletedUtc = model.DeletedUtc,
        //        IsDeleted = model.IsDeleted,
        //        LastModifiedAgent = model.LastModifiedAgent,
        //        LastModifiedBy = model.LastModifiedBy,
        //        LastModifiedUtc = model.LastModifiedUtc,
        //        //Shift = model.Shift,
        //        AvalItems = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputAvalItemViewModel()
        //        {
        //            Active = s.Active,
        //            LastModifiedUtc = s.LastModifiedUtc,
        //            CreatedAgent = s.CreatedAgent,
        //            CreatedBy = s.CreatedBy,
        //            CreatedUtc = s.CreatedUtc,
        //            DeletedAgent = s.DeletedAgent,
        //            DeletedBy = s.DeletedBy,
        //            DeletedUtc = s.DeletedUtc,
        //            IsDeleted = s.IsDeleted,
        //            LastModifiedAgent = s.LastModifiedAgent,
        //            LastModifiedBy = s.LastModifiedBy,

        //            Id = s.Id,
        //            AvalType = s.AvalType,
        //            AvalCartNo = s.AvalCartNo,
        //            UomUnit = s.UomUnit,
        //            Quantity = s.Balance,
        //            QuantityKg = s.AvalQuantityKg,
        //            //HasOutputDocument = s.HasOutputDocument,
        //            //IsChecked = s.IsChecked
        //        }).ToList()
        //    };


        //    return vm;
        //}

        //private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        //{
        //    return string.Format("{0}.{1}.{2}.{3}", GA, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        //}

        //public async Task<int> Create(OutputAvalViewModel viewModel)
        //{
        //    int result = 0;
        //    int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == TRANSIT && 
        //                                                                                 s.DestinationArea == viewModel.DestinationArea && 
        //                                                                                 s.CreatedUtc.Year == viewModel.Date.Year);
        //    string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
        //    viewModel.AvalItems = viewModel.AvalItems.Where(s => s.Quantity > 0).ToList();

        //    var model = new DyeingPrintingAreaOutputModel(viewModel.Date, 
        //                                                  viewModel.Area, 
        //                                                  //viewModel.Shift, 
        //                                                  bonNo, 
        //                                                  false, 
        //                                                  viewModel.DestinationArea, 
        //                                                  viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(
        //                                                                                             s.AvalType,
        //                                                                                             s.AvalCartNo,
        //                                                                                             s.UomUnit,
        //                                                                                             s.Quantity,
        //                                                                                             s.QuantityKg))
        //                                                                                .ToList());

        //    result = await _repository.InsertAsync(model);
        //    foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
        //    {
        //        var vmItem = viewModel.AvalItems.FirstOrDefault(s => s.AvalCartNo == item.AvalCartNo);

        //        result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);

        //        var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, 
        //                                                                viewModel.Area, 
        //                                                                TYPE, 
        //                                                                model.Id, 
        //                                                                model.BonNo, 
        //                                                                item.ProductionOrderId, 
        //                                                                item.ProductionOrderNo,
        //                                                                item.CartNo, 
        //                                                                item.Buyer, 
        //                                                                item.Construction, 
        //                                                                item.Unit, 
        //                                                                item.Color, 
        //                                                                item.Motif, 
        //                                                                item.UomUnit, 
        //                                                                item.Balance);

        //        var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == viewModel.InputAvalId && 
        //                                                                               s.DyeingPrintingAreaDocumentBonNo == viewModel.BonNo);

        //        var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, 
        //                                                              viewModel.Area, 
        //                                                              TYPE, 
        //                                                              model.Id, 
        //                                                              model.BonNo, 
        //                                                              item.ProductionOrderId, 
        //                                                              item.ProductionOrderNo,
        //                                                              item.CartNo, 
        //                                                              item.Buyer, 
        //                                                              item.Construction, 
        //                                                              item.Unit, 
        //                                                              item.Color, 
        //                                                              item.Motif, 
        //                                                              item.UomUnit, 
        //                                                              item.Balance);

        //        result += await _movementRepository.InsertAsync(movementModel);
        //        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
        //    }

        //    return result;
        //}

        //public ListResult<IndexViewModel> Read(int page, 
        //                                       int size, 
        //                                       string filter, 
        //                                       string order, 
        //                                       string keyword)
        //{
        //    var query = _repository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
        //                                                 s.HasNextAreaDocument);
        //    List<string> SearchAttributes = new List<string>()
        //    {
        //        "BonNo"
        //    };

        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

        //    Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

        //    Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
        //    var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
        //    {
        //        Area = s.Area,
        //        BonNo = s.BonNo,
        //        Date = s.Date,
        //        Id = s.Id,
        //        Shift = s.Shift,
        //        AvalProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputAvalItemViewModel()
        //        {
        //            Id = d.Id,
        //            AvalType = d.AvalType,
        //            AvalCartNo = d.AvalCartNo,
        //            UomUnit = d.UomUnit,
        //            Quantity = d.Balance,
        //            QuantityKg = d.AvalQuantityKg,
        //            //HasOutputDocument = d.HasOutputDocument,
        //            //IsChecked = d.IsChecked
        //        }).ToList()
        //    });

        //    return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        //}

        //public ListResult<IndexViewModel> ReadAvailableAval(DateTimeOffset searchDate, 
        //                                                    string searchShift, 
        //                                                    int page, 
        //                                                    int size, 
        //                                                    string filter, 
        //                                                    string order, 
        //                                                    string keyword)
        //{
        //    var query = _repository.ReadAll().Where(s => s.Date <= searchDate &&
        //                                                 s.Shift == searchShift &&
        //                                                 s.Area == GUDANGAVAL && 
        //                                                 !s.HasNextAreaDocument);
        //    List<string> SearchAttributes = new List<string>()
        //    {
        //        "BonNo"
        //    };

        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

        //    Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

        //    Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
        //    query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
        //    var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
        //    {
        //        Area = s.Area,
        //        BonNo = s.BonNo,
        //        Date = s.Date,
        //        Id = s.Id,
        //        Shift = s.Shift,
        //        AvalProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputAvalItemViewModel()
        //        {
        //            Id = d.Id,
        //            AvalType = d.AvalType,
        //            AvalCartNo = d.AvalCartNo,
        //            UomUnit = d.UomUnit,
        //            Quantity = d.Balance,
        //            QuantityKg = d.AvalQuantityKg,
        //            //HasOutputDocument = d.HasOutputDocument,
        //            //IsChecked = d.IsChecked
        //        }).ToList()
        //    });

        //    return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        //}

        //public async Task<OutputAvalViewModel> ReadById(int id)
        //{
        //    var model = await _repository.ReadByIdAsync(id);
        //    if (model == null)
        //        return null;

        //    OutputAvalViewModel vm = MapToViewModel(model);

        //    return vm;
        //}

        //public async Task<MemoryStream> GenerateExcel(int id)
        //{
        //    var model = await _repository.ReadByIdAsync(id);
        //    var query = model.DyeingPrintingAreaOutputProductionOrders;
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Saldo", DataType = typeof(double) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

        //    if (query.Count() == 0)
        //    {
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", 0, "");
        //    }
        //    else
        //    {
        //        foreach (var item in query)
        //        {
        //            dt.Rows.Add(item.ProductionOrderNo, item.CartNo, item.Construction, item.Unit, item.Buyer, item.Color, item.Motif, item.Remark, item.Grade, item.UomUnit, item.Balance,
        //                "");
        //        }
        //    }

        //    return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Transit Area Dyeing Printing") }, true);
        //}
    //}
}
