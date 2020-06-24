using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
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
                Bon = new IndexViewModel
                {
                    Area = model.Area,
                    BonNo = model.BonNo,
                    DestinationArea = model.DestinationArea,
                    Shift = model.Shift,
                    Group = model.Group,
                    Date = model.Date,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    Id = model.Id
                },
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

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
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
                case PACKING:
                    bonNo = string.Format("{0}.{1}.{2}.{3}", GJ, PC, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                    break;
                case TRANSIT:
                    bonNo = string.Format("{0}.{1}.{2}.{3}", GJ, TR, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
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
                                                                                                             s.ProductionOrder.No,
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
                                                                                        item.ProductionOrder.No,
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

        public ListResult<IndexViewModel> Read(string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && s.DyeingPrintingAreaInputProductionOrders.Any(d => !d.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            var data = query.Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                BonNo = s.BonNo,
                Date = s.Date,
                //DestinationArea = s.DestinationArea,
                //HasNextAreaDocument = s.HasOutputDocument,
                Shift = s.Shift,
                Group = s.Group
            });

            return new ListResult<IndexViewModel>(data.ToList(), 0, data.Count(), query.Count());
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

        public List<InputWarehouseProductionOrderCreateViewModel> GetInputWarehouseProductionOrders()
        {
            var productionOrders = _inputProductionOrderRepository.ReadAll().OrderByDescending(s => s.LastModifiedUtc)
               .Where(s => s.Area == GUDANGJADI && !s.HasOutputDocument);
            var data = productionOrders.Select(s => new InputWarehouseProductionOrderCreateViewModel()
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

        public List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList()
        {
            var query = _inputProductionOrderRepository.ReadAll()
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == GUDANGJADI &&
                                                                    !s.HasOutputDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {

                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    InputId = p.DyeingPrintingAreaInputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasOutputDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(p.Balance), p.PackagingQty)
                }).ToList()

            });

            return data.ToList();
        }
        public List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList(int bonId)
        {
            var query = _inputProductionOrderRepository.ReadAll()
                                                        .Join(_inputRepository.ReadAll().Where(x => x.Id == bonId),
                                                        spp => spp.DyeingPrintingAreaInputId,
                                                        bon => bon.Id,
                                                        (spp,bon)=> spp)
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == GUDANGJADI &&
                                                                    !s.HasOutputDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {

                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    InputId = p.DyeingPrintingAreaInputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasOutputDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(p.Balance), p.PackagingQty)
                }).ToList()

            });

            return data.ToList();
        }

        public List<InputSppWarehouseViewModel> GetOutputSppWarehouseItemList(int bonId)
        {
            var query = _outputProductionOrderRepository.ReadAll()
                                                        .Join(_outputRepository.ReadAll().Where(x => x.Id == bonId),
                                                        spp => spp.DyeingPrintingAreaOutputId,
                                                        bon => bon.Id,
                                                        (spp, bon) => spp)
                                                        .OrderByDescending(s => s.LastModifiedUtc)
                                                        .Where(s => s.Area == GUDANGJADI &&
                                                                    !s.HasNextAreaDocument);

            //var groupedProductionOrders = query.GroupBy(s => s.ProductionOrderId);

            var data = query.GroupBy(o => new { o.ProductionOrderId, o.ProductionOrderNo, o.ProductionOrderOrderQuantity, o.ProductionOrderType }).Select(s => new InputSppWarehouseViewModel()
            {
                ProductionOrderId = s.Key.ProductionOrderId,
                ProductionOrderNo = s.Key.ProductionOrderNo,
                ProductionOrderOrderQuantity = s.Key.ProductionOrderOrderQuantity,
                ProductionOrderType = s.Key.ProductionOrderType,
                ProductionOrderItems = s.Select(p => new InputSppWarehouseItemListViewModel()
                {

                    Id = p.Id,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.Key.ProductionOrderId,
                        No = s.Key.ProductionOrderNo,
                        Type = s.Key.ProductionOrderType,
                        OrderQuantity = s.Key.ProductionOrderOrderQuantity
                    },
                    CartNo = p.CartNo,
                    Buyer = p.Buyer,
                    BuyerId = p.BuyerId,
                    Construction = p.Construction,
                    Unit = p.Unit,
                    Color = p.Color,
                    Motif = p.Motif,
                    UomUnit = p.UomUnit,
                    Remark = p.Remark,
                    //InputId = p.DyeingPrintingAreaOutputId,
                    Grade = p.Grade,
                    Status = p.Status,
                    Balance = p.Balance,
                    PackingInstruction = p.PackingInstruction,
                    PackagingType = p.PackagingType,
                    PackagingQty = p.PackagingQty,
                    PackagingUnit = p.PackagingUnit,
                    AvalALength = p.AvalALength,
                    AvalBLength = p.AvalBLength,
                    AvalConnectionLength = p.AvalConnectionLength,
                    DeliveryOrderSalesId = p.DeliveryOrderSalesId,
                    DeliveryOrderSalesNo = p.DeliveryOrderSalesNo,
                    AvalType = p.AvalType,
                    AvalCartNo = p.AvalCartNo,
                    AvalQuantityKg = p.AvalQuantityKg,
                    //Description = p.Description,
                    //DeliveryNote = p.DeliveryNote,
                    Area = p.Area,
                    //DestinationArea = p.DestinationArea,
                    HasOutputDocument = p.HasNextAreaDocument,
                    //DyeingPrintingAreaInputProductionOrderId = p.DyeingPrintingAreaInputProductionOrderId,
                    Qty = p.PackagingQty.Equals(0) ? 0 : Decimal.Divide(Convert.ToDecimal(p.Balance), p.PackagingQty)
                }).ToList()

            });

            return data.ToList();
        }

        public async Task<int> Delete(int bonId)
        {
            var result = 0;
            var bonOutput = _outputRepository.ReadAll().FirstOrDefault(x => x.Id == bonId && x.DyeingPrintingAreaOutputProductionOrders.Any(s => !s.HasNextAreaDocument));
            if (bonOutput != null)
            {
                var listBonInputByBonOutput = _inputProductionOrderRepository.ReadAll().Join(bonOutput.DyeingPrintingAreaOutputProductionOrders,
                                                                              sppInput => sppInput.Id,
                                                                              sppOutput => sppOutput.DyeingPrintingAreaInputProductionOrderId,
                                                                              (sppInput, sppOutput) => new { Input = sppInput, Output = sppOutput });
                foreach (var spp in listBonInputByBonOutput)
                {
                    spp.Input.SetHasOutputDocument(false, "OUTWAREHOUSESERVICE", "SERVICE");

                    //update balance remains
                    var newBalance = spp.Input.BalanceRemains + spp.Output.Balance;

                    spp.Input.SetBalanceRemains(newBalance, "OUTWAREHOUSESERVICE", "SERVICE");
                    result += await _inputProductionOrderRepository.UpdateAsync(spp.Input.Id, spp.Input);
                }
            }
            result += await _outputRepository.DeleteAsync(bonId);

            return result;
        }

        public MemoryStream GenerateExcelAll()
        {
            //var model = await _repository.ReadByIdAsync(id);
            var modelAll = _outputRepository.ReadAll().Where(s => s.Area == GUDANGJADI && !s.HasNextAreaDocument).ToList().Select(s =>
                 new
                 {
                     SppList = s.DyeingPrintingAreaOutputProductionOrders.Select(d => new
                     {
                         BonNo = s.BonNo,
                         DoNo = s.DeliveryOrderSalesNo,
                         NoSPP = d.ProductionOrderNo,
                         QtyOrder = d.ProductionOrderOrderQuantity,
                         Material = d.Construction,
                         Unit = d.Unit,
                         Buyer = d.Buyer,
                         Warna = d.Color,
                         Motif = d.Motif,
                         Jenis = d.PackagingType,
                         Grade = d.Grade,
                         Ket = d.Description,
                         QtyPack = d.PackagingQty,
                         Pack = d.PackagingUnit,
                         Qty = d.Balance,
                         SAT = d.UomUnit
                     })
                 });

            //var model = modelAll.First();
            //var query = model.DyeingPrintingAreaOutputProductionOrders;
            var query = modelAll.SelectMany(s => s.SppList);

            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"BonNo","NO BON" },
                {"DoNo","NO DO" },
                {"NoSPP","NO SP" },
                {"QtyOrder","QTY ORDER" },
                {"Material","MATERIAL"},
                {"Unit","UNIT"},
                {"Buyer","BUYER"},
                {"Warna","WARNA"},
                {"Motif","MOTIF"},
                {"Jenis","JENIS"},
                {"Grade","GRADE"},
                {"Ket","KET" },
                {"QtyPack","QTY Pack"},
                {"Pack","PACK"},
                {"Qty","QTY" },
                {"SAT","SAT" },
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
                    //else
                    //{
                    //    valueClass = "";
                    //}
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("PENCATATAN KELUAR GUDANG JADI");

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 1, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[1, startHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[1, startHeaderColumn, 1, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);

            foreach (DataColumn column in dt.Columns)
            {

                sheet.Cells[1, startHeaderColumn].Value = column.ColumnName;
                sheet.Cells[1, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                startHeaderColumn++;
            }
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 2;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }


    }
}
