using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using OfficeOpenXml;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class StockWarehouseService : IStockWarehouseService
    {
        private readonly IDyeingPrintingAreaOutputRepository _outputBonRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputSppRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputBonRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSppRepository;
        private readonly IDyeingPrintingStockOpnameProductionOrderRepository _stockOpnameItemRepository;
        private readonly IDyeingPrintingStockOpnameRepository _stockOpnameRepository;

        public StockWarehouseService(IServiceProvider serviceProvider)
        {
            _outputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _inputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _outputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _stockOpnameItemRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameProductionOrderRepository>();
            _stockOpnameRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameRepository>();
        }

        private IEnumerable<SimpleReportViewModel> GetAwalData(DateTimeOffset dateFrom, string area, IEnumerable<long> productionOrderIds, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType)
        {
            //var queryTransform = _movementRepository.ReadAll()
            //    .Where(s => s.Area == area && s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date && productionOrderIds.Contains(s.ProductionOrderId));
            // var invType = inventoryType == "BARU" ? null : inventoryType;
            var queryTransform = _movementRepository.ReadAll()
                .Where(s => s.Area == area && s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date);

            if (!string.IsNullOrEmpty(unit))
            {
                queryTransform = queryTransform.Where(s => s.Unit == unit);
            }
            if (!string.IsNullOrEmpty(packingType))
            {
                queryTransform = queryTransform.Where(s => s.PackingType == packingType);
            }
            if (!string.IsNullOrEmpty(construction))
            {
                queryTransform = queryTransform.Where(s => s.Construction == construction);
            }
            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }
            if (productionOrderId != 0)
            {
                queryTransform = queryTransform.Where(s => s.ProductionOrderId == productionOrderId);
            }
            if (inventoryType == "BARU")
            {
                queryTransform = queryTransform.Where(s => s.InventoryType == null);
            }
            else
            {
                if (!string.IsNullOrEmpty(inventoryType))
                {
                    queryTransform = queryTransform.Where(s => s.InventoryType == inventoryType);
                }

            }

            //var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
            //       s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance, s.PackingType, s.Buyer)).ToList();

            var result = queryTransform.GroupBy(s => new { s.ProductionOrderId, s.Grade, s.Remark, s.PackingType, s.InventoryType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = DyeingPrintingArea.AWAL,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                Buyer = d.First().Buyer,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                InventoryType = d.Key.InventoryType,
                Quantity = d.Where(e => e.Type == DyeingPrintingArea.IN).Sum(e => e.Balance) - d.Where(e => e.Type == DyeingPrintingArea.OUT).Sum(e => e.Balance)
                    + d.Where(e => e.Type == DyeingPrintingArea.ADJ_IN || e.Type == DyeingPrintingArea.ADJ_OUT).Sum(e => e.Balance)

            });

            return result;
        }

        private IEnumerable<SimpleReportViewModel> GetDataByDate(DateTime startDate, DateTimeOffset dateReport, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType)
        {

            //var invType = inventoryType == "BARU" ? String.Empty : inventoryType;
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        startDate.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= dateReport.Date);

            if (!string.IsNullOrEmpty(unit))
            {
                queryTransform = queryTransform.Where(s => s.Unit == unit);
            }
            if (!string.IsNullOrEmpty(packingType))
            {
                queryTransform = queryTransform.Where(s => s.PackingType == packingType);
            }
            if (!string.IsNullOrEmpty(construction))
            {
                queryTransform = queryTransform.Where(s => s.Construction == construction);
            }
            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }
            if (productionOrderId != 0)
            {
                queryTransform = queryTransform.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (inventoryType == "BARU")
            {
                queryTransform = queryTransform.Where(s => s.InventoryType == null);
            }
            else
            {
                if (!string.IsNullOrEmpty(inventoryType))
                {
                    queryTransform = queryTransform.Where(s => s.InventoryType == inventoryType);
                }

            }


            //var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
            //            s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance, s.PackingType, s.Buyer)).ToList();

            var result = queryTransform.GroupBy(s => new { s.ProductionOrderId, s.Type, s.Grade, s.Remark, s.PackingType, s.InventoryType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = d.Key.Type,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                Buyer = d.First().Buyer,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                InventoryType = d.Key.InventoryType,
                Quantity = d.Sum(e => e.Balance)
            });

            return result;
        }

        public List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType)
        {
            var result = new List<ReportStockWarehouseViewModel>();

            if (inventoryType == "STOCK OPNAME")
            {
                var startDate = new DateTime(dateReport.Year, dateReport.Month, 1);
                var dataSearchDate = GetDataByDate(startDate, dateReport, "GUDANG JADI", offset, unit, packingType, construction, buyer, productionOrderId, inventoryType);
                var productionOrderIds = dataSearchDate.Select(e => e.ProductionOrderId);
                var dataAwal = GetAwalData(startDate, zona, productionOrderIds, offset, unit, packingType, construction, buyer, productionOrderId, inventoryType);

                var joinData2 = dataSearchDate.Concat(dataAwal);
                var tempResult = joinData2.GroupBy(d => new { d.ProductionOrderId, d.Grade, d.Jenis, d.Ket, d.InventoryType }).Select(e => new ReportStockWarehouseViewModel()
                {
                    ProductionOrderId = e.Key.ProductionOrderId,
                    NoSpp = e.First().NoSpp,
                    Color = e.First().Color,
                    Construction = e.First().Construction,
                    Grade = e.Key.Grade,
                    Jenis = e.Key.Jenis,
                    Ket = e.Key.Ket,
                    Motif = e.First().Motif,
                    Buyer = e.First().Buyer,
                    Satuan = e.First().Satuan,
                    Unit = e.First().Unit,
                    InventoryType = e.First().InventoryType == null ? "BARU" : e.First().InventoryType,
                    Awal = decimal.Round(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).Quantity) : 0, 4),
                    Masuk = decimal.Round(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).Quantity) : 0, 4),
                    Keluar = decimal.Round((e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).Quantity) : 0), 4),
                    Akhir = decimal.Round((e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).Quantity) : 0), 4)
                });

                var dpWarehouseResult = tempResult.Where(s => s.Awal != 0 || s.Masuk != 0 || s.Keluar != 0 || s.Akhir != 0).OrderBy(s => s.NoSpp).ThenBy(s => s.Construction).ToList();

                var stockOpnameIds = _stockOpnameRepository.ReadAll().Where(entity => entity.Date < dateReport.AddDays(1)).Select(entity => entity.Id).ToList();
                var stockOpnames = _stockOpnameItemRepository.ReadAll().Where(entity => entity.IsStockOpname && stockOpnameIds.Contains(entity.DyeingPrintingStockOpnameId)).ToList();

                var stockOpnameTempResult = stockOpnames.GroupBy(s => new { s.ProductionOrderId, s.Grade })
                    .Select(d => new
                    {
                        ProductionOrderId = d.Key.ProductionOrderId,
                        Color = d.First().Color,
                        Construction = d.First().Construction,
                        Grade = d.Key.Grade,
                        Jenis = d.First().PackagingType,
                        Motif = d.First().Motif,
                        Buyer = d.First().Buyer,
                        NoSpp = d.First().ProductionOrderNo,
                        Satuan = d.First().UomUnit,
                        Unit = d.First().Unit,
                        Quantity = d.Sum(e => e.PackagingQty * (decimal)e.PackagingLength)
                    }).ToList();

                foreach (var stockOpname in stockOpnameTempResult)
                {
                    var dpWarehouse = dpWarehouseResult.FirstOrDefault(element => element.ProductionOrderId == stockOpname.ProductionOrderId && element.Grade == stockOpname.Grade);

                    var gudangJadiBalance = (decimal)0;
                    if (dpWarehouse != null)
                    {
                        gudangJadiBalance = dpWarehouse.Akhir;
                    }

                    result.Add(new ReportStockWarehouseViewModel()
                    {
                        NoSpp = stockOpname.NoSpp,
                        Construction = stockOpname.Construction,
                        Unit = stockOpname.Unit,
                        Motif = stockOpname.Motif,
                        Buyer = stockOpname.Buyer,
                        Color = stockOpname.Color,
                        Grade = stockOpname.Grade,
                        Jenis = stockOpname.Jenis,
                        StockOpname = stockOpname.Quantity,
                        StorageBalance = gudangJadiBalance,
                        Difference = gudangJadiBalance - stockOpname.Quantity
                    });

                }

                result = result.OrderBy(element => element.NoSpp).ThenBy(element => element.Construction).ToList();
            }
            else
            {
                var startDate = new DateTime(dateReport.Year, dateReport.Month, 1);
                var dataSearchDate = GetDataByDate(startDate, dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId, inventoryType);
                var productionOrderIds = dataSearchDate.Select(e => e.ProductionOrderId);
                var dataAwal = GetAwalData(startDate, zona, productionOrderIds, offset, unit, packingType, construction, buyer, productionOrderId, inventoryType);

                var joinData2 = dataSearchDate.Concat(dataAwal);
                var tempResult = joinData2.GroupBy(d => new { d.ProductionOrderId, d.Grade, d.Jenis, d.Ket, d.InventoryType }).Select(e => new ReportStockWarehouseViewModel()
                {
                    ProductionOrderId = e.Key.ProductionOrderId,
                    NoSpp = e.First().NoSpp,
                    Color = e.First().Color,
                    Construction = e.First().Construction,
                    Grade = e.Key.Grade,
                    Jenis = e.Key.Jenis,
                    Ket = e.Key.Ket,
                    Motif = e.First().Motif,
                    Buyer = e.First().Buyer,
                    Satuan = e.First().Satuan,
                    Unit = e.First().Unit,
                    InventoryType = e.First().InventoryType == null ? "BARU" : e.First().InventoryType,
                    Awal = decimal.Round(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).Quantity) : 0, 4),
                    Masuk = decimal.Round(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).Quantity) : 0, 4),
                    Keluar = decimal.Round((e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).Quantity) : 0), 4),
                    Akhir = decimal.Round((e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).Quantity) : 0)
                         - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).Quantity) : 0)
                         + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? Convert.ToDecimal(e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).Quantity) : 0), 4)
                });

                result = tempResult.Where(s => s.Awal != 0 || s.Masuk != 0 || s.Keluar != 0 || s.Akhir != 0).OrderBy(s => s.NoSpp).ThenBy(s => s.Construction).ToList();
            }


            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string inventoryType)
        {
            var data = GetReportData(dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId, inventoryType);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Ket", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Awal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Akhir", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Gudang", DataType = typeof(string) });

            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", 0, 0, 0, 0, "");
            }
            else
            {
                foreach (var item in data)
                {
                    dt.Rows.Add(item.NoSpp, item.Construction, item.Unit, item.Motif, item.Buyer, item.Color, item.Grade, item.Jenis,
                        item.Ket, item.Awal.ToString("N2", CultureInfo.InvariantCulture), item.Masuk.ToString("N2", CultureInfo.InvariantCulture), item.Keluar.ToString("N2", CultureInfo.InvariantCulture),
                        item.Akhir.ToString("N2", CultureInfo.InvariantCulture), item.Satuan, item.InventoryType);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", zona)) }, true);
        }

        private IEnumerable<PackingDataViewModel> GetAwalPackingData(DateTimeOffset dateFrom, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        s.Unit == unit &&
                        s.PackingType == packingType &&
                        s.Construction == construction &&
                        s.Grade == grade &&
                        s.ProductionOrderId == productionOrderId &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date);


            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.PackagingQty, s.PackagingUnit, s.PackagingLength, s.Balance)).ToList();

            var result = data.GroupBy(s => new { s.PackagingUnit, s.PackagingLength }).Select(d => new PackingDataViewModel()
            {
                Type = DyeingPrintingArea.AWAL,
                PackagingLength = d.Key.PackagingLength,
                PackagingUnit = d.Key.PackagingUnit,
                PackagingQty = d.Where(e => e.Type == DyeingPrintingArea.IN).Sum(e => e.PackagingQty) - d.Where(e => e.Type == DyeingPrintingArea.OUT).Sum(e => e.PackagingQty)
                    + d.Where(e => e.Type == DyeingPrintingArea.ADJ_IN || e.Type == DyeingPrintingArea.ADJ_OUT).Sum(e => e.PackagingQty),
                Balance = Convert.ToDecimal(d.Where(e => e.Type == DyeingPrintingArea.IN).Sum(e => e.Balance) - d.Where(e => e.Type == DyeingPrintingArea.OUT).Sum(e => e.Balance)
                    + d.Where(e => e.Type == DyeingPrintingArea.ADJ_IN || e.Type == DyeingPrintingArea.ADJ_OUT).Sum(e => e.Balance))

            });

            return result;
        }

        private IEnumerable<PackingDataViewModel> GetPackingDataByDate(DateTime startDate, DateTimeOffset dateReport, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        s.Unit == unit &&
                        s.PackingType == packingType &&
                        s.Construction == construction &&
                        s.Grade == grade &&
                        s.ProductionOrderId == productionOrderId &&
                        startDate.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= dateReport.Date);

            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.PackagingQty, s.PackagingUnit, s.PackagingLength, s.Balance)).ToList();

            var result = data.GroupBy(s => new { s.Type, s.PackagingUnit, s.PackagingLength }).Select(d => new PackingDataViewModel()
            {
                Type = d.Key.Type,
                PackagingLength = d.Key.PackagingLength,
                PackagingUnit = d.Key.PackagingUnit,
                PackagingQty = d.Sum(e => e.PackagingQty),
                Balance = Convert.ToDecimal(d.Sum(e => e.Balance))
            });

            return result;
        }

        public List<PackingDataViewModel> GetPackingData(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var startDate = new DateTime(dateReport.Year, dateReport.Month, 1);
            var dataSearchDate = GetPackingDataByDate(startDate, dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId, grade);
            var dataAwal = GetAwalPackingData(startDate, zona, offset, unit, packingType, construction, buyer, productionOrderId, grade);
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => new { d.PackagingUnit, d.PackagingLength }).Select(e => new PackingDataViewModel()
            {
                PackagingLength = e.Key.PackagingLength,
                PackagingUnit = e.Key.PackagingUnit,
                Balance = decimal.Round((e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).Balance : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).Balance : 0), 4),
                PackagingQty = (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.IN).PackagingQty : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).PackagingQty : 0)
            });

            return result.Where(s => s.Balance != 0).ToList();
        }
    }
}
