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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalService : IOutputAvalService
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

        public OutputAvalService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        }

        private OutputAvalViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputAvalViewModel()
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
                    UomUnit = s.UomUnit,
                    Quantity = s.Balance,
                    QuantityKg = s.AvalQuantityKg,
                    //HasOutputDocument = s.HasOutputDocument,
                    //IsChecked = s.IsChecked
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
            int totalCurrentYearData = _repository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL &&
                                                                                         s.DestinationArea == viewModel.DestinationArea &&
                                                                                         s.CreatedUtc.Year == viewModel.Date.Year);
            string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            viewModel.AvalItems = viewModel.AvalItems.Where(s => s.Quantity > 0).ToList();

            var model = new DyeingPrintingAreaOutputModel(viewModel.Date,
                                                          viewModel.Area,
                                                          viewModel.Shift,
                                                          bonNo,
                                                          false,
                                                          viewModel.DestinationArea,
                                                          viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(
                                                                                                     s.AvalType,
                                                                                                     s.AvalCartNo,
                                                                                                     s.UomUnit,
                                                                                                     s.Quantity,
                                                                                                     s.QuantityKg))
                                                                             .ToList());

            result = await _repository.InsertAsync(model);
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var vmItem = viewModel.AvalItems.FirstOrDefault(s => s.AvalCartNo == item.AvalCartNo);

                result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);

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

                var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == viewModel.InputAvalId &&
                                                                                       s.DyeingPrintingAreaDocumentBonNo == viewModel.BonNo);

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

                result += await _movementRepository.InsertAsync(movementModel);
                result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page,
                                               int size,
                                               string filter,
                                               string order,
                                               string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                                                         s.HasNextAreaDocument);
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

        public ListResult<IndexViewModel> ReadAvailableAval(DateTimeOffset searchDate,
                                                            string searchShift,
                                                            int page,
                                                            int size,
                                                            string filter,
                                                            string order,
                                                            string keyword)
        {
            var query = _repository.ReadAll().Where(s => s.Date <= searchDate &&
                                                         s.Shift == searchShift &&
                                                         s.Area == GUDANGAVAL &&
                                                         !s.HasNextAreaDocument);
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

        public async Task<OutputAvalViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputAvalViewModel vm = MapToViewModel(model);

            return vm;
        }

        //public async Task<MemoryStream> GenerateExcel(int id)
        //{
        //    var model = await _repository.ReadByIdAsync(id);
        //    var query = model.DyeingPrintingAreaOutputProductionOrders;

        //    var indexNumber = 1;
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Group", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Keluar Ke", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Kode Bon", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Ket", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Kg", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Menyerahkan", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Menerima", DataType = typeof(string) });

        //    if (query.Count() == 0)
        //    {
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "");
        //    }
        //    else
        //    {
        //        foreach (var item in query)
        //        {
        //            //var stringDate = item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
        //            dt.Rows.Add(indexNumber,
        //                        item.,
        //                        item.Group,
        //                        item.Unit,
        //                        item.Mutation,
        //                        item.CartNo,
        //                        item.BonNo,
        //                        item.ProductionOrderType,
        //                        item.ProductionOrderQuantity,
        //                        item.UomUnit,
        //                        0,
        //                        "",
        //                        "");
        //            indexNumber++;
        //        }
        //    }

        //    ExcelPackage package = new ExcelPackage();
        //    #region Header
        //    var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");
        //    sheet.Cells[1, 1].Value = "DIVISI";
        //    sheet.Cells[1, 2].Value = "DYEING PRINTING PT DANLIRIS";

        //    sheet.Cells[2, 1].Value = "TANGGAL";
        //    sheet.Cells[2, 2].Value = date.HasValue ? date.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID")) : "";

        //    sheet.Cells[3, 1].Value = "GROUP";
        //    sheet.Cells[3, 2].Value = group;

        //    sheet.Cells[4, 1].Value = "MUTASI";
        //    sheet.Cells[4, 2].Value = mutation;

        //    sheet.Cells[5, 1].Value = "ZONA";
        //    sheet.Cells[5, 2].Value = zone;

        //    sheet.Cells[6, 1].Value = "NO.";
        //    sheet.Cells[6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 1].AutoFitColumns();
        //    sheet.Cells[6, 1, 7, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 1, 7, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 1, 7, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 1, 7, 1].Merge = true;

        //    sheet.Cells[6, 2].Value = "TANGGAL";
        //    sheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 2].AutoFitColumns();
        //    sheet.Cells[6, 2, 7, 2].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 2, 7, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 2, 7, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 2, 7, 2].Merge = true;

        //    sheet.Cells[6, 3].Value = "GROUP";
        //    sheet.Cells[6, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 3].AutoFitColumns();
        //    sheet.Cells[6, 3, 7, 3].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 3, 7, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 3, 7, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 3, 7, 3].Merge = true;

        //    sheet.Cells[6, 4].Value = "UNIT";
        //    sheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 4].AutoFitColumns();
        //    sheet.Cells[6, 4, 7, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 4, 7, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 4, 7, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 4, 7, 4].Merge = true;

        //    sheet.Cells[6, 5].Value = "KELUAR KE";
        //    sheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 5].AutoFitColumns();
        //    sheet.Cells[6, 5, 7, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 5, 7, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 5, 7, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 5, 7, 5].Merge = true;

        //    sheet.Cells[6, 6].Value = "NO. KERETA";
        //    sheet.Cells[6, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 6].AutoFitColumns();
        //    sheet.Cells[6, 6, 7, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 6, 7, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 6, 7, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 6, 7, 6].Merge = true;

        //    sheet.Cells[6, 7].Value = "KODE BON";
        //    sheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 7].AutoFitColumns();
        //    sheet.Cells[6, 7, 7, 7].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 7, 7, 7].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 7, 7, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 7, 7, 7].Merge = true;

        //    sheet.Cells[6, 8].Value = "JENIS";
        //    sheet.Cells[6, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 8].AutoFitColumns();
        //    sheet.Cells[6, 8, 7, 8].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 8, 7, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 8, 7, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 8, 7, 8].Merge = true;

        //    sheet.Cells[6, 9].Value = "SAT";
        //    sheet.Cells[6, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 9].AutoFitColumns();
        //    sheet.Cells[6, 9, 6, 10].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 9, 7, 9].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 9, 7, 9].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 9, 6, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 9, 6, 10].Merge = true;

        //    sheet.Cells[7, 9].Value = "QTY";
        //    sheet.Cells[7, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[7, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[7, 9].AutoFitColumns();

        //    sheet.Cells[7, 10].Value = "KET";
        //    sheet.Cells[7, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[7, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[7, 10].AutoFitColumns();

        //    sheet.Cells[6, 11].Value = "KG";
        //    sheet.Cells[6, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 11].AutoFitColumns();
        //    sheet.Cells[6, 11].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 11, 7, 11].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 11, 7, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 11, 7, 11].Merge = true;

        //    sheet.Cells[6, 12].Value = "NAMA & PARAF";
        //    sheet.Cells[6, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[6, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[6, 12].AutoFitColumns();
        //    sheet.Cells[6, 12, 6, 13].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 12, 6, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 12, 6, 12].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 12, 6, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[6, 12, 6, 13].Merge = true;

        //    sheet.Cells[7, 12].Value = "MENYERAHKAN";
        //    sheet.Cells[7, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[7, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[7, 12].AutoFitColumns();
        //    sheet.Cells[7, 12].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 12].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

        //    sheet.Cells[7, 13].Value = "MENERIMA";
        //    sheet.Cells[7, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    sheet.Cells[7, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //    sheet.Cells[7, 13].AutoFitColumns();
        //    sheet.Cells[7, 13].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 13].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[7, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    #endregion

        //    int tableRowStart = 8;
        //    int tableColStart = 1;

        //    sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
        //    sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();
        //    sheet.Cells[tableRowStart, tableColStart].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[tableRowStart, tableColStart].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[tableRowStart, tableColStart].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //    sheet.Cells[tableRowStart, tableColStart].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

        //    MemoryStream stream = new MemoryStream();
        //    package.SaveAs(stream);

        //    //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Aval Area Dyeing Printing") }, true);
        //    return stream;
        //}
    }
}
