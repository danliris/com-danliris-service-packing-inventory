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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingService : IOutputPackagingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;

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

        public enum AreaAbbr
        {
            [Description("IM")]
            INSPECTIONMATERIAL = 0,
            [Description("TR")]
            TRANSIT,
            [Description("PC")]
            PACKING,
            [Description("GJ")]
            GUDANGJADI,
            [Description("GA")]
            GUDANGAVAL,
            [Description("SP")]
            SHIPPING,
        }

        //public enum AreaName
        //{
        //    [Description("INSPECTION MATERIAL")]
        //    INSPECTIONMATERIAL = 0,
        //    [Description("TRANSIT")]
        //    TRANSIT,
        //    [Description("PACKING")]
        //    PACKING,
        //    [Description("GUDANG JADI")]
        //    GUDANGJADI,
        //    [Description("GUDANG AVAL")]
        //    GUDANGAVAL,
        //    [Description("SHIPPING")]
        //    SHIPPING,
        //}
        //public enum UnitPackaging
        //{
        //    DEFAULT = 0,

        //    [Description("ROLLS")]
        //    ROLLS ,

        //    [Description("PIECE")]
        //    PIECE,

        //    [Description("PACK")]
        //    PACK
        //}
        //public enum TypePackaging
        //{
        //    DEFAULT = 0,

        //    [Description("WHITE")]
        //    WHITE = 0,

        //    [Description("DYEING")]
        //    DYEING,

        //    [Description("BATIK")]
        //    BATIK,

        //    [Description("TEXTILE")]
        //    TEXTILE,

        //    [Description("DIGITAL PRINTING")]
        //    DIGITALPRINTING,

        //    [Description("TRANSFER PRINT")]
        //    TRANSFERPRINT
        //}

        public OutputPackagingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        }

        private OutputPackagingViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputPackagingViewModel()
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
                DestinationArea = model.DestinationArea,
                HasNextAreaDocument = model.HasNextAreaDocument,
                PackagingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputPackagingProductionOrderViewModel()
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
                        Type = s.ProductionOrderType
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    PackagingQTY = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit
                    
                }).ToList()
            };
            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationAreaName)
        {
            string sourceArea = AreaAbbr.PACKING.ToDescription();
            if (destinationAreaName == TRANSIT)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == INSPECTIONMATERIAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == GUDANGJADI)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, GJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == GUDANGAVAL)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else if (destinationAreaName == SHIPPING)
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", sourceArea, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        public async Task<int> Create(OutputPackagingViewModel viewModel)
        {
            int result = 0;
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == INSPECTIONMATERIAL && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            viewModel.PackagingProductionOrders = viewModel.PackagingProductionOrders.Where(s => s.Balance > 0).ToList();
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.PackagingProductionOrders.Select(s =>
                 new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                 s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type,s.PackagingType,s.PackagingQTY,s.PackagingUnit)).ToList());

            result = await _repository.InsertAsync(model);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var vmItem = viewModel.PackagingProductionOrders.FirstOrDefault(s => s.ProductionOrder.Id == item.ProductionOrderId);

                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);

                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == viewModel.InputPackagingId && s.ProductionOrderId == item.ProductionOrderId);

                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                result += await _movementRepository.InsertAsync(movementModel);
                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == INSPECTIONMATERIAL && !s.HasNextAreaDocument);
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
                HasNextAreaDocument = s.HasNextAreaDocument,
                PackagingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPackagingProductionOrderViewModel()
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

        public async Task<OutputPackagingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputPackagingViewModel vm = MapToViewModel(model);

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
    }
    internal static class Extensions
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        //public static Dictionary<int, string> ToDictionaries(Enum myEnum)
        //{
        //    var myEnumType = myEnum.GetType();
        //    var names = myEnumType.GetFields()
        //        .Where(m => m.GetCustomAttribute<DisplayAttribute>() != null)
        //        .Select(e => e.GetCustomAttribute<DisplayAttribute>().Name);
        //    var values = Enum.GetValues(myEnumType).Cast<int>();
        //    return names.Zip(values, (n, v) => new KeyValuePair<int, string>(v, n))
        //        .ToDictionary(kv => kv.Key, kv => kv.Value);
        //}
        //public static Enum ToObject(this Enum value)
        //{
        //    FieldInfo field = value.GetType().GetField(value.ToString());
        //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        //    return attribute == null ? value.ToString() : attribute.Description;
        //}
    }
}
