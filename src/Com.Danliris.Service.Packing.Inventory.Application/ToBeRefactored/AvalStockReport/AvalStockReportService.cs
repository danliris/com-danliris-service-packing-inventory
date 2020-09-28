using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport
{
    public class AvalStockReportService : IAvalStockReportService
    {
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;

        public AvalStockReportService(IServiceProvider serviceProvider)
        {
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private IEnumerable<SimpleAvalViewModel> GetAwalData(DateTime startDate, IEnumerable<string> avalTypes, int offset)
        {
            var queryTransform = _movementRepository.ReadAll()
                .Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL && 
                        s.Type != DyeingPrintingArea.IN && 
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < startDate.Date && 
                        avalTypes.Contains(s.AvalType))
                .Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.AvalType, s.AvalQuantity, s.AvalWeightQuantity)).ToList();

            var result = queryTransform.GroupBy(s => s.AvalType).Select(d => new SimpleAvalViewModel()
            {
                AvalType = d.Key,
                AvalQuantity = d.Where(e => e.Type == DyeingPrintingArea.TRANSFORM).Sum(e => e.AvalQuantity) - d.Where(e => e.Type == DyeingPrintingArea.OUT).Sum(e => e.AvalQuantity),
                Type = DyeingPrintingArea.AWAL,
                AvalQuantityWeight = d.Where(e => e.Type == DyeingPrintingArea.TRANSFORM).Sum(e => e.AvalWeightQuantity) - d.Where(e => e.Type == DyeingPrintingArea.OUT).Sum(e => e.AvalWeightQuantity)
                        + d.Where(e => e.Type == DyeingPrintingArea.ADJ_IN || e.Type == DyeingPrintingArea.ADJ_OUT).Sum(e => e.Balance)
            });

            return result;
        }

        //private IQueryable<SimpleAvalViewModel> GetDataTransform(DateTimeOffset searchDate)
        //{
        //    var queryTransform = _movementRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.Type == TRANSFORM && s.Date.Date == searchDate.Date)
        //        .GroupBy(s => s.AvalType).Select(d => new SimpleAvalViewModel()
        //        {
        //            AvalType = d.Key,
        //            Type = TRANSFORM,
        //            AvalQuantity = d.Sum(e => e.AvalQuantity),
        //            AvalQuantityWeight = d.Sum(e => e.AvalWeightQuantity)
        //        });

        //    return queryTransform;
        //}

        //private IQueryable<SimpleAvalViewModel> GetDataOutput(DateTimeOffset searchDate)
        //{
        //    var queryTransform = _movementRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.Type == OUT && s.Date.Date == searchDate.Date)
        //        .GroupBy(s => s.AvalType).Select(d => new SimpleAvalViewModel()
        //        {
        //            Type = OUT,
        //            AvalType = d.Key,
        //            AvalQuantity = d.Sum(e => e.AvalQuantity),
        //            AvalQuantityWeight = d.Sum(e => e.AvalWeightQuantity)
        //        });

        //    return queryTransform;
        //}

        private IEnumerable<SimpleAvalViewModel> GetDataByDate(DateTime startDate, DateTimeOffset searchDate, int offset)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == DyeingPrintingArea.GUDANGAVAL && 
                        s.Type != DyeingPrintingArea.IN &&
                        startDate.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= searchDate.Date)
                   .Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.AvalType, s.AvalQuantity, s.AvalWeightQuantity)).ToList();

            var result = queryTransform.GroupBy(s => new { s.AvalType, s.Type }).Select(d => new SimpleAvalViewModel()
            {
                Type = d.Key.Type,
                AvalType = d.Key.AvalType,
                AvalQuantity = d.Sum(e => e.AvalQuantity),
                AvalQuantityWeight = d.Sum(e => e.AvalWeightQuantity)
            });

            return result;
        }

        private IEnumerable<AvalStockReportViewModel> GetQuery(DateTimeOffset searchDate, int offset)
        {
            //var dataTransformFunc = GetDataTransform(searchDate).ToListAsync();
            //var dataOutpuFunc = GetDataOutput(searchDate).ToListAsync();

            //var dataTransform = await dataTransformFunc;
            //var dataOutput = await dataOutpuFunc;
            //var joinData1 = dataTransform.Concat(dataOutput);

            var startDate = new DateTime(searchDate.Year, searchDate.Month, 1);
            var dataSearchDate = GetDataByDate(startDate, searchDate, offset);
            var listAvalType = dataSearchDate.Select(d => d.AvalType).Distinct();
            var dataAwal = GetAwalData(startDate, listAvalType, offset);
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => d.AvalType).Select(e => new AvalStockReportViewModel()
            {
                AvalType = e.Key,
                StartAvalQuantity = e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).AvalQuantity : 0,
                StartAvalWeightQuantity = e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).AvalQuantityWeight : 0,
                InAvalQuantity = e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM).AvalQuantity : 0,
                InAvalWeightQuantity = e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM).AvalQuantityWeight : 0,
                OutAvalQuantity = (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).AvalQuantity : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).AvalQuantity : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).AvalQuantity : 0),
                OutAvalWeightQuantity = (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).AvalQuantityWeight : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).AvalQuantityWeight : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).AvalQuantityWeight : 0),
                EndAvalQuantity = (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).AvalQuantity : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM).AvalQuantity : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).AvalQuantity : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).AvalQuantity : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).AvalQuantity : 0),
                EndAvalWeightQuantity = (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.AWAL).AvalQuantityWeight : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.TRANSFORM).AvalQuantityWeight : 0)
                    - (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.OUT).AvalQuantityWeight : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_IN).AvalQuantityWeight : 0)
                    + (e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == DyeingPrintingArea.ADJ_OUT).AvalQuantityWeight : 0)
            }); 

            return result.Where(s => s.StartAvalQuantity != 0 || s.InAvalQuantity != 0 || s.OutAvalQuantity != 0 || s.EndAvalQuantity != 0 ||
                    s.StartAvalWeightQuantity != 0 || s.InAvalWeightQuantity != 0 || s.OutAvalWeightQuantity != 0 || s.EndAvalWeightQuantity != 0);
        }

        public MemoryStream GenerateExcel(DateTimeOffset searchDate, int offset)
        {
            var data = GetQuery(searchDate, offset);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Awal QTY Satuan", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Awal QTY Berat", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk QTY Satuan", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk QTY Berat", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar QTY Satuan", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar QTY Berat", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Akhir QTY Satuan", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Akhir QTY Berat", DataType = typeof(double) });

            if (data.Count() == 0)
            {
                dt.Rows.Add("", 0, 0, 0, 0, 0, 0, 0, 0);
            }
            else
            {
                foreach (var item in data)
                {
                    dt.Rows.Add(item.AvalType, item.StartAvalQuantity, item.StartAvalWeightQuantity, item.InAvalQuantity, item.InAvalWeightQuantity, item.OutAvalQuantity, item.OutAvalWeightQuantity,
                        item.EndAvalQuantity, item.EndAvalWeightQuantity);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Laporan Stock Gudang Aval") }, true);
        }

        public ListResult<AvalStockReportViewModel> GetReportData(DateTimeOffset searchDate, int offset)
        {
            var data = GetQuery(searchDate, offset);
            return new ListResult<AvalStockReportViewModel>(data.ToList(), 1, data.Count(), data.Count());
        }
    }
}
