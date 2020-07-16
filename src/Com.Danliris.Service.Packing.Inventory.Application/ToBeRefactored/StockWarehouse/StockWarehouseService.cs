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

        private const string OUT = "OUT";
        private const string IN = "IN";
        private const string AWAL = "AWAL";
        private const string TRANSFORM = "TRANSFORM";
        private const string ADJ_IN = "ADJ IN";
        private const string ADJ_OUT = "ADJ OUT";

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

        public StockWarehouseService(IServiceProvider serviceProvider)
        {
            _outputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _inputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _outputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private IEnumerable<SimpleReportViewModel> GetAwalData(DateTimeOffset dateFrom, string area, IEnumerable<long> productionOrderIds, int offset)
        {
            var queryTransform = _movementRepository.ReadAll()
                .Where(s => s.Area == area && s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date && productionOrderIds.Contains(s.ProductionOrderId))
                .Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
                    s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance)).ToList();

            var result = queryTransform.GroupBy(s => new { s.ProductionOrderId, s.Grade, s.Remark, s.PackingType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = AWAL,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                Quantity = d.Where(e => e.Type == IN).Sum(e => e.Balance) - d.Where(e => e.Type == OUT).Sum(e => e.Balance)
                    + d.Where(e => e.Type == ADJ_IN || e.Type == ADJ_OUT).Sum(e => e.Balance)

            });

            return result;
        }

        private IEnumerable<SimpleReportViewModel> GetDataByDate(DateTimeOffset dateFrom, DateTimeOffset dateTo, string area, int offset)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        dateFrom.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= dateTo.Date)
                    .Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
                        s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance)).ToList();

            var result = queryTransform.GroupBy(s => new { s.ProductionOrderId, s.Type, s.Grade, s.Remark, s.PackingType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = d.Key.Type,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                Quantity = d.Sum(e => e.Balance)
            });

            return result;
        }

        public List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateFrom, DateTimeOffset dateTo, string zona, int offset)
        {
            var dataSearchDate = GetDataByDate(dateFrom, dateTo, zona, offset);
            var productionOrderIds = dataSearchDate.Select(e => e.ProductionOrderId);
            var dataAwal = GetAwalData(dateFrom, zona, productionOrderIds, offset);
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => new { d.NoSpp, d.Grade, d.Jenis, d.Ket }).Select(e => new ReportStockWarehouseViewModel()
            {
                NoSpp = e.Key.NoSpp,
                Color = e.First().Color,
                Construction = e.First().Construction,
                Grade = e.Key.Grade,
                Jenis = e.Key.Jenis,
                Ket = e.Key.Ket,
                Motif = e.First().Motif,
                Satuan = e.First().Satuan,
                Unit = e.First().Unit,
                Awal = e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).Quantity : 0,
                Masuk = e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).Quantity : 0,
                Keluar = (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).Quantity : 0),
                Akhir = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).Quantity : 0)
            });

            return result.Where(s => s.Awal != 0 || s.Masuk != 0 || s.Keluar != 0 || s.Akhir != 0).ToList();
        }


        public MemoryStream GenerateExcel(DateTimeOffset dateFrom, DateTimeOffset dateTo, string zona, int offset)
        {
            //var model = await _repository.ReadByIdAsync(id);
            var query = GetReportData(dateFrom, dateTo, zona, offset);
            //var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"NoSpp","No SPP" },
                {"Grade","Grade"},
                {"Awal","Awal"},
                {"Masuk","Masuk"},
                {"Keluar","Keluar"},
                {"Akhir","Akhir"}
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
            var sheet = package.Workbook.Worksheets.Add("STOCK " + dateFrom.ToString("ddMMMyyyy") + " - " + dateTo.ToString("ddMMMyyyy"));

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 1, endHeaderColumn].Style.Font.Bold = true;

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
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        //public List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateFrom, DateTimeOffset dateTo, string zona, int offset)
        //{


        //    var outputGroupByNoSPP = _outputSppRepository.ReadAll()
        //                                .Join(_outputBonRepository.ReadAll().Where(x => x.Date.Date == dateReport.Date && x.Area == zona),
        //                                    spp => spp.DyeingPrintingAreaOutputId,
        //                                    bon => bon.Id,
        //                                    (spp, bon) => spp
        //                                )
        //                                .Where(x => !x.HasNextAreaDocument)
        //                                .GroupBy(x => x.ProductionOrderId)
        //                                .Select(x =>
        //                                    new ReportStockWarehouseViewModel
        //                                    {
        //                                        NoSpp = x.First().ProductionOrderNo,
        //                                        Color = x.First().Color,
        //                                        Satuan = x.First().UomUnit,
        //                                        Unit = x.First().Unit,
        //                                        Motif = x.First().Motif,
        //                                        Grade = x.First().Grade,
        //                                        Contruction = x.First().Construction,
        //                                        Jenis = x.First().ProductionOrderType,
        //                                        Ket = x.First().Description,
        //                                        Awal = x.First().ProductionOrderOrderQuantity,
        //                                        Keluar = x.Sum(t => t.Balance),
        //                                        Masuk = 0, //will be provide later quering from input area
        //                                        Akhir = 0 //will be provide after masuk has assign and will be recalculate
        //                                    }
        //                                );

        //    var inputGroupByNoSPP = _inputSppRepository.ReadAll()
        //                                .Join(_inputBonRepository.ReadAll().Where(x => x.Date.Date == dateReport.Date && x.Area == zona),
        //                                    spp => spp.DyeingPrintingAreaInputId,
        //                                    bon => bon.Id,
        //                                    (spp, bon) => spp
        //                                )
        //                                .Where(x => !x.HasOutputDocument)
        //                                .GroupBy(x => x.ProductionOrderId)
        //                                .Select(x =>
        //                                    new ReportStockWarehouseViewModel
        //                                    {
        //                                        NoSpp = x.First().ProductionOrderNo,
        //                                        Color = x.First().Color,
        //                                        Satuan = x.First().UomUnit,
        //                                        Unit = x.First().Unit,
        //                                        Motif = x.First().Motif,
        //                                        Grade = x.First().Grade,
        //                                        Contruction = x.First().Construction,
        //                                        Jenis = x.First().ProductionOrderType,
        //                                        Ket = string.Empty,
        //                                        Awal = x.First().ProductionOrderOrderQuantity,
        //                                        Keluar = 0, //will be provide later quering from output area
        //                                        Masuk = x.Sum(t => t.Balance),
        //                                        Akhir = 0 //will be provide after masuk has assign and will be recalculate
        //                                    }

        //                                );
        //    // recalculate for value akhir and join using no SPP
        //    var recalculateReport = from x in outputGroupByNoSPP
        //                            from y in inputGroupByNoSPP //on x.NoSpp equals y.NoSpp
        //                            select new ReportStockWarehouseViewModel
        //                            {
        //                                NoSpp = string.IsNullOrEmpty(x.NoSpp) ? y.NoSpp : x.NoSpp,
        //                                Color = string.IsNullOrEmpty(x.Color) ? y.Color : x.Color,
        //                                Grade = string.IsNullOrEmpty(x.Grade) ? y.Grade : x.Grade,
        //                                Contruction = string.IsNullOrWhiteSpace(x.Contruction) ? y.Contruction : x.Contruction,
        //                                Satuan = string.IsNullOrWhiteSpace(x.Satuan) ? y.Satuan : x.Satuan,
        //                                Jenis = string.IsNullOrWhiteSpace(x.Jenis) ? y.Jenis : x.Jenis,
        //                                Motif = string.IsNullOrWhiteSpace(x.Motif) ? y.Motif : x.Motif,
        //                                Unit = string.IsNullOrWhiteSpace(x.Unit) ? y.Unit : x.Unit,
        //                                Ket = string.IsNullOrWhiteSpace(x.Ket) ? y.Ket : x.Ket,
        //                                Awal = y.Awal,
        //                                Masuk = y.Masuk,
        //                                Keluar = x.Keluar,
        //                                Akhir = Math.Abs(x.Keluar - y.Awal)
        //                            };

        //    var result = recalculateReport
        //                    .GroupBy(x => x.NoSpp)
        //                    .Select(x =>
        //                                   new ReportStockWarehouseViewModel
        //                                   {
        //                                       NoSpp = x.First().NoSpp,
        //                                       Color = x.First().Color,
        //                                       Grade = x.First().Grade,
        //                                       Contruction = x.First().Contruction,
        //                                       Satuan = x.First().Satuan,
        //                                       Jenis = x.First().Jenis,
        //                                       Motif = x.First().Motif,
        //                                       Unit = x.First().Unit,
        //                                       Ket = x.First().Ket,
        //                                       Awal = x.Sum(y=> y.Awal),
        //                                       Masuk = x.Sum(y=>y.Masuk),
        //                                       Keluar = x.Sum(y=>y.Keluar),
        //                                       Akhir = Math.Abs(x.Sum(y=>y.Keluar) - x.Sum(y=> y.Awal))
        //                                   }
        //                    );

        //    return result.ToList();

        //}

    }
}
