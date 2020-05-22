using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseService : IOutputWarehouseService
    {
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;

        private const string TYPE = "IN";

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

        public OutputWarehouseService(IServiceProvider serviceProvider)
        {
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
        }

        private OutputWarehouseViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputWarehouseViewModel()
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
                Group = model.Group,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                Shift = model.Shift,
                DestinationArea = model.DestinationArea,
                HasNextAreaDocument = model.HasNextAreaDocument,
                WarehousesProductionOrders = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputWarehouseProductionOrderViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Balance = s.Balance,
                    Buyer = s.Buyer,
                    CartNo = s.CartNo,
                    BuyerId = s.BuyerId,
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
                        OrderQuantity = s.ProductionOrderOrderQuantity,
                        Type = s.ProductionOrderType
                    },
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    HasNextAreaDocument = s.HasNextAreaDocument,
                    PackagingQty = s.PackagingQty,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    ProductionOrderNo = s.ProductionOrderNo,
                    QtyOrder = s.ProductionOrderOrderQuantity,
                    DeliveryOrderSalesId = s.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = s.DeliveryOrderSalesNo
                }).ToList()
            };


            return vm;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            var bonNo = "";
            switch (destinationArea)
            {
                case SHIPPING:
                    bonNo = string.Format("{0}.{1}.{2}.{3}", GJ, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                    break;
                case INSPECTIONMATERIAL:
                    bonNo = string.Format("{0}.{1}.{2}.{3}", GJ, IM, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                    break;
                default:
                    break;
            }
            return bonNo;
        }

        public async Task<int> Create(OutputWarehouseViewModel viewModel)
        {
            int result = 0;
            var model = _outputRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == GUDANGJADI && 
                                                                                        s.DestinationArea == viewModel.DestinationArea && 
                                                                                        s.Date.Date == viewModel.Date.Date && 
                                                                                        s.Shift == viewModel.Shift);

            if (model == null)
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter()
                                                            .Count(s => s.Area == GUDANGJADI && 
                                                                        s.DestinationArea == viewModel.DestinationArea && 
                                                                        s.CreatedUtc.Year == viewModel.Date.Year);

                string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);

                model = new DyeingPrintingAreaOutputModel(viewModel.Date,
                                                          viewModel.Area,
                                                          viewModel.Shift,
                                                          bonNo,
                                                          false,
                                                          viewModel.DestinationArea,
                                                          viewModel.Group,
                                                          viewModel.WarehousesProductionOrders.Select(s =>
                                                            new DyeingPrintingAreaOutputProductionOrderModel(s.ProductionOrder.Id,
                                                                                                             s.ProductionOrderNo,
                                                                                                             s.CartNo,
                                                                                                             s.Buyer,
                                                                                                             s.Construction,
                                                                                                             s.Unit,
                                                                                                             s.Color,
                                                                                                             s.Motif,
                                                                                                             s.UomUnit,
                                                                                                             s.Remark,
                                                                                                             s.Grade,
                                                                                                             s.Status,
                                                                                                             s.Balance,
                                                                                                             s.PackingInstruction,
                                                                                                             s.ProductionOrder.Type,
                                                                                                             s.ProductionOrder.OrderQuantity,
                                                                                                             s.PackagingType,
                                                                                                             s.PackagingQty,
                                                                                                             s.PackagingUnit,
                                                                                                             s.DeliveryOrderSalesId,
                                                                                                             s.DeliveryOrderSalesNo, 
                                                                                                             false, 
                                                                                                             viewModel.Area, 
                                                                                                             viewModel.DestinationArea, 
                                                                                                             s.Id,
                                                                                                             s.BuyerId)).ToList());

                result = await _outputRepository.InsertAsync(model);

                foreach (var item in viewModel.WarehousesProductionOrders)
                {
                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, 
                                                                            viewModel.Area, 
                                                                            TYPE, 
                                                                            model.Id, 
                                                                            model.BonNo, 
                                                                            item.ProductionOrder.Id, 
                                                                            item.ProductionOrder.No,
                                                                            item.CartNo, 
                                                                            item.Buyer, 
                                                                            item.Construction, 
                                                                            item.Unit, 
                                                                            item.Color, 
                                                                            item.Motif, 
                                                                            item.UomUnit, 
                                                                            item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId &&
                                                                                           s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, 
                                                                          viewModel.Area, 
                                                                          TYPE, 
                                                                          model.Id, 
                                                                          model.BonNo, 
                                                                          item.ProductionOrder.Id, 
                                                                          item.ProductionOrder.No,
                                                                          item.CartNo, 
                                                                          item.Buyer, 
                                                                          item.Construction, 
                                                                          item.Unit, 
                                                                          item.Color, 
                                                                          item.Motif, 
                                                                          item.UomUnit, 
                                                                          item.Balance);

                    result += await _movementRepository.InsertAsync(movementModel);
                    if (previousSummary != null)
                    {

                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                    else
                    {

                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                }
            }
            else
            {
                foreach (var item in viewModel.WarehousesProductionOrders)
                {
                    var modelItem = new DyeingPrintingAreaOutputProductionOrderModel(item.ProductionOrder.Id,
                                                                                        item.ProductionOrderNo,
                                                                                        item.CartNo,
                                                                                        item.Buyer,
                                                                                        item.Construction,
                                                                                        item.Unit,
                                                                                        item.Color,
                                                                                        item.Motif,
                                                                                        item.UomUnit,
                                                                                        item.Remark,
                                                                                        item.Grade,
                                                                                        item.Status,
                                                                                        item.Balance,
                                                                                        item.PackingInstruction,
                                                                                        item.ProductionOrder.Type,
                                                                                        item.ProductionOrder.OrderQuantity,
                                                                                        item.PackagingType,
                                                                                        item.PackagingQty,
                                                                                        item.PackagingUnit,
                                                                                        item.DeliveryOrderSalesId,
                                                                                        item.DeliveryOrderSalesNo,
                                                                                        false,
                                                                                        viewModel.Area,
                                                                                        viewModel.DestinationArea,
                                                                                        item.Id,
                                                                                        item.BuyerId);
                    modelItem.DyeingPrintingAreaOutputId = model.Id;

                    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(item.Id, item.Balance);

                    var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, 
                                                                            viewModel.Area, 
                                                                            TYPE, 
                                                                            model.Id, 
                                                                            model.BonNo, 
                                                                            item.ProductionOrder.Id, 
                                                                            item.ProductionOrder.No,
                                                                            item.CartNo, 
                                                                            item.Buyer, 
                                                                            item.Construction, 
                                                                            item.Unit, 
                                                                            item.Color, 
                                                                            item.Motif, 
                                                                            item.UomUnit, 
                                                                            item.Balance);

                    var previousSummary = _summaryRepository.ReadAll().FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == item.InputId &&
                                                                                           s.ProductionOrderId == item.ProductionOrder.Id);

                    var summaryModel = new DyeingPrintingAreaSummaryModel(viewModel.Date, 
                                                                          viewModel.Area, 
                                                                          TYPE, 
                                                                          model.Id, 
                                                                          model.BonNo, 
                                                                          item.ProductionOrder.Id, 
                                                                          item.ProductionOrder.No,
                                                                          item.CartNo, 
                                                                          item.Buyer, 
                                                                          item.Construction, 
                                                                          item.Unit, 
                                                                          item.Color, 
                                                                          item.Motif, 
                                                                          item.UomUnit, 
                                                                          item.Balance);

                    result += await _outputProductionOrderRepository.InsertAsync(modelItem);

                    result += await _movementRepository.InsertAsync(movementModel);

                    if (previousSummary != null)
                    {
                        result += await _summaryRepository.UpdateAsync(previousSummary.Id, summaryModel);
                    }
                    else
                    {
                        result += await _summaryRepository.InsertAsync(summaryModel);
                    }
                }

            }

            return result;
        }

        public async Task<OutputWarehouseViewModel> ReadById(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputWarehouseViewModel vm = MapToViewModel(model);

            return vm;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument));
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
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                DestinationArea = s.DestinationArea,
                HasNextAreaDocument = s.HasNextAreaDocument,
                Shift = s.Shift,
                Group = s.Group
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            var query = model.DyeingPrintingAreaOutputProductionOrders;

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO.", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. DO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY ORDER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "UNIT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BUYER", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "WARNA", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MOTIF", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "PACKAGING", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY PACKAGING", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "JENIS", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "GRADE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SATUAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SALDO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KETERANGAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENYERAHKAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENERIMA", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(indexNumber,
                                item.DeliveryOrderSalesNo,
                                item.ProductionOrderNo,
                                item.ProductionOrderOrderQuantity,
                                item.Construction,
                                item.Unit,
                                item.Buyer,
                                item.Color,
                                item.Motif,
                                item.PackagingType,
                                item.PackagingQty,
                                item.ProductionOrderType,
                                item.Grade,
                                item.UomUnit,
                                item.Balance,
                                "",
                                "",
                                "");
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");

            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "SHIFT";
            sheet.Cells[2, 2].Value = model.Shift;

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = model.Shift;

            sheet.Cells[4, 1].Value = "KELUAR KE";
            sheet.Cells[4, 2].Value = model.DestinationArea;

            sheet.Cells[5, 1].Value = "NO. BON";
            sheet.Cells[5, 2].Value = model.BonNo;
            sheet.Cells[5, 2, 5, 3].Merge = true;

            sheet.Cells[6, 1].Value = "NO.";
            sheet.Cells[6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 1, 7, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 1, 7, 1].Merge = true;

            sheet.Cells[6, 2].Value = "NO. DO";
            sheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 2, 7, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 2, 7, 2].Merge = true;

            sheet.Cells[6, 3].Value = "NO. SPP";
            sheet.Cells[6, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 3, 7, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 3, 7, 3].Merge = true;

            sheet.Cells[6, 4].Value = "QTY ORDER";
            sheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 4, 7, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 4, 7, 4].Merge = true;

            sheet.Cells[6, 5].Value = "MATERIAL";
            sheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 5, 7, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 5, 7, 5].Merge = true;

            sheet.Cells[6, 6].Value = "UNIT";
            sheet.Cells[6, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 6, 7, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 6, 7, 6].Merge = true;

            sheet.Cells[6, 7].Value = "BUYER";
            sheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 7, 7, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 7, 7, 7].Merge = true;

            sheet.Cells[6, 8].Value = "WARNA";
            sheet.Cells[6, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 8, 7, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 8, 7, 8].Merge = true;

            sheet.Cells[6, 9].Value = "MOTIF";
            sheet.Cells[6, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 9, 7, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 9, 7, 9].Merge = true;

            sheet.Cells[6, 10].Value = "PACKAGING";
            sheet.Cells[6, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 10, 7, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 10, 7, 10].Merge = true;

            sheet.Cells[6, 11].Value = "QTY PACKAGING";
            sheet.Cells[6, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 11, 7, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 11, 7, 11].Merge = true;

            sheet.Cells[6, 12].Value = "JENIS";
            sheet.Cells[6, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 12, 7, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 12, 7, 12].Merge = true;

            sheet.Cells[6, 13].Value = "GRADE";
            sheet.Cells[6, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 13, 7, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 13, 7, 13].Merge = true;

            sheet.Cells[6, 14].Value = "SATUAN";
            sheet.Cells[6, 14].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 14].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 14, 7, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 14, 7, 14].Merge = true;

            sheet.Cells[6, 15].Value = "SALDO";
            sheet.Cells[6, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 15].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 15, 7, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 15, 7, 15].Merge = true;

            sheet.Cells[6, 16].Value = "KETERANGAN";
            sheet.Cells[6, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 16].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 16, 7, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            sheet.Cells[6, 16, 7, 16].Merge = true;

            sheet.Cells[6, 17].Value = "PARAF";
            sheet.Cells[6, 17].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 17].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[6, 17].AutoFitColumns();
            sheet.Cells[6, 17, 6, 18].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 18].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17, 6, 18].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 17, 6, 18].Merge = true;

            sheet.Cells[7, 17].Value = "MENERIMA";
            sheet.Cells[7, 17].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 17].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 17].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 17].AutoFitColumns();

            sheet.Cells[7, 18].Value = "MENYERAHKAN";
            sheet.Cells[7, 18].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 18].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 18].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 18].AutoFitColumns();
            #endregion

            int tableRowStart = 8;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            
            return stream;
        }

        public List<InputWarehouseProductionOrderViewModel> GetInputWarehouseProductionOrders()
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
               .Where(s => s.Area == GUDANGJADI && !s.HasOutputDocument);
            var data = productionOrders.Select(s => new InputWarehouseProductionOrderViewModel()
            {
                Id = s.Id,
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    Type = s.ProductionOrderType,
                    OrderQuantity = s.ProductionOrderOrderQuantity
                },
                ProductionOrderNo = s.ProductionOrderNo,
                CartNo = s.CartNo,
                PackingInstruction = s.PackingInstruction,
                Construction = s.Construction,
                Unit = s.Unit,
                Buyer = s.Buyer,
                BuyerId = s.BuyerId,
                Color = s.Color,
                Motif = s.Motif,
                UomUnit = s.UomUnit,
                Balance = s.Balance,
                HasOutputDocument = s.HasOutputDocument,
                IsChecked = s.IsChecked,
                Grade = s.Grade,
                Remark = s.Remark,
                Status = s.Status,
                //MtrLength = ,
                //YdsLength = ,
                //Quantity = ,
                PackagingType = s.PackagingType,
                PackagingUnit = s.PackagingUnit,
                PackagingQty = s.PackagingQty,
                QtyOrder = s.ProductionOrderOrderQuantity,
                InputId = s.DyeingPrintingAreaInputId
                //DeliveryOrderSalesNo = s.DeliveryOrderSalesNo
            });

            return data.ToList();
        }
    }
}
