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

        public OutputAvalService(IServiceProvider serviceProvider)
        {
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private OutputAvalViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputAvalViewModel()
            {
                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                //BonNo = model.BonNo,
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

                    Id = s.Id,
                    AvalType = s.AvalType,
                    AvalCartNo = s.AvalCartNo,
                    AvalUomUnit = s.UomUnit,
                    AvalQuantity = s.Balance,
                    AvalQuantityKg = s.AvalQuantityKg
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            return string.Format("{0}.{1}.{2}.{3}", GA, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        public async Task<int> Create(OutputAvalViewModel viewModel)
        {
            int result = 0;

            //Count Existing Document in Aval Output (and Destination Area) by Year
            int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL &&
                                                                                               s.DestinationArea == viewModel.DestinationArea &&
                                                                                               s.CreatedUtc.Year == viewModel.Date.Year);
            //Generate Bon Number
            string bonNo = string.Empty;
            var bonExist = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                                                                s.Date == viewModel.Date &&
                                                                s.Shift == viewModel.Shift);
            int bonExistCount = bonExist.Count();
            if (bonExistCount == 0)
                bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            else
                bonNo = bonExist.FirstOrDefault().BonNo;

            //Filter only Item Has Quantity and Quantity KG can be Inserted
            //viewModel.AvalItems = viewModel.AvalItems.Where(s => s.AvalQuantity > 0 && s.AvalQuantityKg > 0).ToList();
            DyeingPrintingAreaOutputModel model = null;
            if (bonExistCount == 0)
            {
                //Instantiate Output Model
                model = new DyeingPrintingAreaOutputModel(viewModel.Date,
                                                              viewModel.Area,
                                                              viewModel.Shift,
                                                              bonNo,
                                                              false,
                                                              viewModel.DestinationArea,
                                                              viewModel.Group,
                                                              viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType,
                                                                                                                                               s.AvalCartNo,
                                                                                                                                               s.AvalOutSatuan,
                                                                                                                                               s.AvalOutQuantity,
                                                                                                                                               s.AvalOutQuantity))
                                                                                 .ToList());

                //Create New Row in Output and ProductionOrdersOutput in Each Repository 
                result = await _outputRepository.InsertAsync(model);
            }
            else
            {
                model = new DyeingPrintingAreaOutputModel(bonExist.FirstOrDefault().Date,
                                                              bonExist.FirstOrDefault().Area,
                                                              bonExist.FirstOrDefault().Shift,
                                                              bonNo,
                                                              false,
                                                              bonExist.FirstOrDefault().DestinationArea,
                                                              bonExist.FirstOrDefault().Group,
                                                              viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType,
                                                                                                                                               s.AvalCartNo,
                                                                                                                                               s.AvalOutSatuan,
                                                                                                                                               s.AvalOutQuantity,
                                                                                                                                               s.AvalOutQuantity,
                                                                                                                                               bonExist.FirstOrDefault().Id))
                                                                                 .ToList());
                foreach (var avalitem in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    result += await _outputProductionOrderRepository.InsertAsync(avalitem);
                }
            }

            //var productionOrderIds = _outputProductionOrderRepository.ReadAll().Where(o => o.DyeingPrintingAreaOutputId == model.Id).ToList();

            ////Movement from Aval Input Area to Aval Output Area
            //foreach (var DyeingPrintingMovement in viewModel.DyeingPrintingMovementIds)
            //{
            //    //Get Previous Summary
            //    var previousSummary = _summaryRepository.ReadAll()
            //                                            .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == DyeingPrintingMovement.DyeingPrintingAreaMovementId);
            //                                                                 //&& s.ProductionOrderId == DyeingPrintingMovement.AvalItemId);

            //    //Update Previous Summary
            //    result += await _summaryRepository.UpdateToAvalAsync(previousSummary, viewModel.Date, viewModel.Area, TYPE);
            //}

            //foreach (var item in viewModel.DyeingPrintingMovementIds)
            //{
            //    var vmItem = _inputProductionOrderRepository.GetInputProductionOrder(item.AvalItemId);

            //    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);
            //}

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                //var vmItem = viewModel.AvalItems.FirstOrDefault(s => s.AvalCartNo == item.AvalCartNo);

                //result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);

                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date,
                                                                        viewModel.Area,
                                                                        TYPE,
                                                                        model.Id,
                                                                        model.BonNo,
                                                                        item.ProductionOrderId,
                                                                        item.ProductionOrderNo,
                                                                        item.CartNo,
                                                                        item.Buyer,
                                                                        item.Construction,
                                                                        item.Unit,
                                                                        item.Color,
                                                                        item.Motif,
                                                                        item.UomUnit,
                                                                        item.Balance);

                result += await _movementRepository.InsertAsync(movementModel);

                var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date,
                                                                      viewModel.Area,
                                                                      TYPE,
                                                                      model.Id,
                                                                      model.BonNo,
                                                                      item.ProductionOrderId,
                                                                      item.ProductionOrderNo,
                                                                      item.CartNo,
                                                                      item.Buyer,
                                                                      item.Construction,
                                                                      item.Unit,
                                                                      item.Color,
                                                                      item.Motif,
                                                                      item.UomUnit,
                                                                      item.Balance);

                result += await _summaryRepository.InsertAsync(summaryModel);
            }

            var groupedType = model.DyeingPrintingAreaOutputProductionOrders.GroupBy(s => s.AvalType,
                                                                                     s => s,
                                                                                     (key, item) => new { Key = key, Items = item });
            foreach (var type in groupedType)
            {
                //update bon Aval Sum
                var bonLastAval = _inputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                                                                        s.IsTransformedAval &&
                                                                        s.AvalType == type.Key).OrderByDescending(s => s.Date).FirstOrDefault();
                if (bonLastAval != null)
                {
                    var sumType = type.Items.Sum(s => s.AvalQuantityKg);
                    var substractSum = bonLastAval.TotalAvalWeight - sumType;
                    bonLastAval.SetTotalAvalWeight(substractSum, "OUTPUTAVALSERVICE", "SERVICES");
                    result += await _inputRepository.UpdateAsync(bonLastAval.Id, bonLastAval);
                }
            }


            return result;
        }

        public ListResult<IndexViewModel> Read(int page,
                                               int size,
                                               string filter,
                                               string order,
                                               string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && !s.HasNextAreaDocument);
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
                Shift = s.Shift
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
                                                         s.Area == GUDANGAVAL &&
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

            OutputAvalViewModel vm = MapToViewModel(model);

            return vm;
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
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
                                item.UomUnit,
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

        public ListResult<AvailableAvalIndexViewModel> ReadAllAvailableAval(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.Area == GUDANGAVAL &&
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
                                                         s.Area == GUDANGAVAL &&
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
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.AvalType == avalType &&
                                                         s.Area == GUDANGAVAL &&
                                                         s.IsTransformedAval &&
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
                    AvalQuantityKg = avalInput.AvalItem.Sum(s => s.TotalAvalWeight)
                };

                data.Add(avalItems);
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }
    }
}
