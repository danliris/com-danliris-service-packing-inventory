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
using OfficeOpenXml;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingService : IOutputPackagingService
    {
        private readonly IDyeingPrintingAreaOutputRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
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
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
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
                Group = model.Group,
                PackagingProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputPackagingProductionOrderViewModel()
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
                    PackagingUnit = s.PackagingUnit,
                    QtyOrder = s.ProductionOrderOrderQuantity,
                    QtyOut = s.Balance,
                    ProductionOrderNo = s.ProductionOrderNo,
                    Keterangan = s.Description
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
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == PACKING && s.DestinationArea == viewModel.DestinationArea
                && s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            viewModel.PackagingProductionOrders = viewModel.PackagingProductionOrders.Where(s => s.Balance > 0).ToList();

            //get BonNo with shift
            var hasBonNoWithShift = _repository.ReadAll().Where(x => x.Shift == viewModel.Shift && x.Area == PACKING && x.Date == viewModel.Date).FirstOrDefault();
            DyeingPrintingAreaOutputModel model = new DyeingPrintingAreaOutputModel();
            if (hasBonNoWithShift == null)
            {

                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, false, viewModel.DestinationArea, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                     s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, 0, s.Id, s.BuyerId)).ToList());
                result += await _repository.InsertAsync(model);
            }
            else
            {
                model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, hasBonNoWithShift.BonNo, false, viewModel.DestinationArea, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, viewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.CartNo, s.Buyer, s.Construction, s.Unit, s.Color,
                     s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.PackingInstruction, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackagingType, s.PackagingQTY, s.PackagingUnit, s.QtyOrder, s.Keterangan, hasBonNoWithShift.Id, s.Id, s.BuyerId)).ToList());
                model.Id = hasBonNoWithShift.Id;
                bonNo = model.BonNo;
            }
            var modelInput = _inputRepository.ReadAll().Where(x => x.BonNo == viewModel.BonNoInput && x.Area == PACKING);

            var modelInputProductionOrder = _inputProductionOrderRepository.ReadAll().Join(modelInput,
                                                                                s => s.DyeingPrintingAreaInputId,
                                                                                s2 => s2.Id,
                                                                                (s, s2) => s);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var vmItem = viewModel.PackagingProductionOrders.FirstOrDefault(s => s.ProductionOrder.Id == item.ProductionOrderId);
                //update balance SPP Input
                var inputSPP = _inputProductionOrderRepository.ReadAll().FirstOrDefault(x => x.Id == item.DyeingPrintingAreaInputProductionOrderId);
                inputSPP.SetBalanceRemains(inputSPP.BalanceRemains - item.Balance, "REPOSITORY", "");
                result += await _inputProductionOrderRepository.UpdateAsync(inputSPP.Id, inputSPP);

                //var previousProductionOrder = modelInputProductionOrder.Where(x => x.ProductionOrderNo == vmItem.ProductionOrder.No).FirstOrDefault();
                //var lastBalance = previousProductionOrder.Balance - vmItem.QtyOut;
                //previousProductionOrder.SetBalance(lastBalance,"REPOSITORY","");

                //result += await _inputProductionOrderRepository.UpdateAsync(previousProductionOrder.Id,previousProductionOrder);
                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);


                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, TYPE, model.Id, bonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == viewModel.InputPackagingId && s.ProductionOrderId == item.ProductionOrderId);

                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, viewModel.Area, TYPE, model.Id, bonNo, item.ProductionOrderId, item.ProductionOrderNo,
                    item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance);

                //var updateBalance = new DyeingPrintingAreaInputModel(viewModel.Date, viewModel.Area, viewModel.Shift, viewModel.BonNoInput, viewModel.Group, viewModel.PackagingProductionOrders.Select(s =>
                // new DyeingPrintingAreaInputProductionOrderModel(viewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                // s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance-vmItem.QtyOut, false, s.QtyOrder, s.Grade)).ToList());

                if (hasBonNoWithShift != null)
                    result += await _outputProductionOrderRepository.InsertAsync(item);

                result += await _movementRepository.InsertAsync(movementModel);
                if (previousSummary != null)
                    result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);

            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == PACKING && !s.HasNextAreaDocument);
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
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument,
                PackagingProductionOrders = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new OutputPackagingProductionOrderViewModel()
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
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
                    UomUnit = d.UomUnit,
                    PackagingQTY = d.PackagingQty,
                    PackagingType = d.PackagingType,
                    PackagingUnit = d.PackagingUnit,
                    Material = d.Construction,
                    QtyOrder = d.ProductionOrderOrderQuantity,
                    ProductionOrderNo = d.ProductionOrderNo,
                    Keterangan = d.Description
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
            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"ProductionOrderNo","No SPP" },
                {"ProductionOrderOrderQuantity","Qty Order" },
                {"Buyer","Buyer" },
                {"Unit","Unit"},
                {"Construction","Material "},
                {"Color","Warna"},
                {"Motif","Motif"},
                {"PackagingType","Jenis"},
                {"Grade","Grade"},
                {"PackagingQty","Qty Packaging"},
                {"PackagingUnit","Packaging"},
                {"UomUnit","Satuan"},
                {"Balance","Saldo"},
                //{"Balance","Qty Keluar" },
                {"Description","Keterangan" },
                {"Menyerahkan","Menyerahkan" },
                {"Menerima","Menerima" },
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
                    else
                    {
                        valueClass = "";
                    }
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("BON PACKAGING");
            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = model.Date.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "SHIFT";
            sheet.Cells[2, 2].Value = model.Shift;

            sheet.Cells[3, 1].Value = "ZONA";
            sheet.Cells[3, 2].Value = model.DestinationArea;

            sheet.Cells[4, 1].Value = "NOMOR BON";
            sheet.Cells[4, 2].Value = model.BonNo;

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 6, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[6, startHeaderColumn, 7, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima")
                {
                    sheet.Cells[6, startHeaderColumn].Value = column.ColumnName;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6, startHeaderColumn, 7, startHeaderColumn].Merge = true;
                    startHeaderColumn++;
                }
            }

            sheet.Cells[6, startHeaderColumn].Value = "Paraf";
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn, 6, startHeaderColumn + 1].Merge = true;

            sheet.Cells[7, startHeaderColumn].Value = "Menyerahkan";
            sheet.Cells[7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            startHeaderColumn++;

            sheet.Cells[7, startHeaderColumn].Value = "Menerima";
            sheet.Cells[7, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 8;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public ListResult<IndexViewModel> ReadBonOutFromPack(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => Convert.ToInt32(d.BalanceRemains) > 0 && d.DyeingPrintingAreaInputId == s.Id && d.HasOutputDocument == false));
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
                PackagingProductionOrders = MapModeltoModelView(s.DyeingPrintingAreaInputProductionOrders.ToList())
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }
        public ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPack(int page, int size, string filter, string order, string keyword)
        {
            var query2 = _inputRepository.ReadAll().Where(s => s.Area == PACKING && s.DyeingPrintingAreaInputProductionOrders.Any(d => d.DyeingPrintingAreaInputId == s.Id)); ;
            var query = _inputProductionOrderRepository.ReadAll().Join(query2,
                                                                        s => s.DyeingPrintingAreaInputId,
                                                                        s2 => s2.Id,
                                                                        (s, s2) => s);
            //var query = query3.GroupBy(s => s.ProductionOrderNo).Select(s => new DyeingPrintingAreaInputProductionOrderModel
            // (s.First().Area,
            // s.First().ProductionOrderId,
            // s.First().ProductionOrderNo,
            // s.First().ProductionOrderType,
            // s.First().ProductionOrderOrderQuantity,
            // s.First().PackingInstruction,
            // s.First().CartNo,
            // s.First().Buyer,
            // s.First().Construction,
            // s.First().Unit,
            // s.First().Color,
            // s.First().Motif,
            // s.First().UomUnit,
            // Convert.ToDouble(s.Sum(d => d.Balance).ToString()),
            // s.First().HasOutputDocument
            // ));

            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Filter(query, FilterDictionary);

            //Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            //query = QueryHelper<DyeingPrintingAreaInputProductionOrderModel>.Order(query, OrderDictionary);
            //var datas = query.Skip((page - 1) * size).Take(size);
            var data = query.ToList().Select(s => new InputPackagingProductionOrdersViewModel()
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
                Unit = s.Unit,
                UomUnit = s.UomUnit,
                ProductionOrderNo = s.ProductionOrderNo,
                Area = s.Area,
                BuyerId = s.BuyerId,
                Grade = s.Grade,
                Status = s.Status,
                HasOutputDocument= s.HasOutputDocument,
                QtyOrder = s.ProductionOrderOrderQuantity,
                Remark = s.Remark
            });

            return new ListResult<InputPackagingProductionOrdersViewModel>(data.ToList(), page, size, query.Count());
        }
        public ICollection<OutputPackagingProductionOrderViewModel> MapModeltoModelView(List<DyeingPrintingAreaInputProductionOrderModel> source)
        {
            List<OutputPackagingProductionOrderViewModel> result = new List<OutputPackagingProductionOrderViewModel>();
            foreach (var d in source)
            {
                result.Add(new OutputPackagingProductionOrderViewModel
                {
                    Balance = d.Balance,
                    Buyer = d.Buyer,
                    BuyerId = d.BuyerId,
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
                    QtyOrder = d.ProductionOrderOrderQuantity
                });
            }
            return result;
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
