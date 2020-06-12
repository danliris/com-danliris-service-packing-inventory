﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport
{
    public class AvalStockReportService : IAvalStockReportService
    {
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;

        private const string OUT = "OUT";
        private const string IN = "IN";
        private const string AWAL = "AWAL";
        private const string TRANSFORM = "TRANSFORM";

        private const string GUDANGAVAL = "GUDANG AVAL";

        public AvalStockReportService(IServiceProvider serviceProvider)
        {
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private IQueryable<SimpleAvalViewModel> GetAwalData(DateTimeOffset searchDate, IEnumerable<string> avalTypes)
        {
            var queryTransform = _movementRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.Type != IN && s.Date.Date < searchDate.Date && avalTypes.Contains(s.AvalType))
                .GroupBy(s => s.AvalType).Select(d => new SimpleAvalViewModel()
                {
                    AvalType = d.Key,
                    AvalQuantity = d.Where(e => e.Type == TRANSFORM).Sum(e => e.AvalQuantity) - d.Where(e => e.Type == OUT).Sum(e => e.AvalQuantity),
                    Type = AWAL,
                    AvalQuantityWeight = d.Where(e => e.Type == TRANSFORM).Sum(e => e.AvalWeightQuantity) - d.Where(e => e.Type == OUT).Sum(e => e.AvalWeightQuantity)
                });

            return queryTransform;
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

        private IQueryable<SimpleAvalViewModel> GetDataByDate(DateTimeOffset searchDate)
        {
            var queryTransform = _movementRepository.ReadAll().Where(s => s.Area == GUDANGAVAL && s.Type != IN && s.Date.Date == searchDate.Date)
                .GroupBy(s => new { s.AvalType, s.Type }).Select(d => new SimpleAvalViewModel()
                {
                    Type = d.Key.Type,
                    AvalType = d.Key.AvalType,
                    AvalQuantity = d.Sum(e => e.AvalQuantity),
                    AvalQuantityWeight = d.Sum(e => e.AvalWeightQuantity)
                });

            return queryTransform;
        }

        private IEnumerable<AvalStockReportViewModel> GetQuery(DateTimeOffset searchDate)
        {
            //var dataTransformFunc = GetDataTransform(searchDate).ToListAsync();
            //var dataOutpuFunc = GetDataOutput(searchDate).ToListAsync();

            //var dataTransform = await dataTransformFunc;
            //var dataOutput = await dataOutpuFunc;
            //var joinData1 = dataTransform.Concat(dataOutput);

            var dataSearchDate = GetDataByDate(searchDate).ToList();
            var listAvalType = dataSearchDate.Select(d => d.AvalType).Distinct();
            var dataAwal = GetAwalData(searchDate, listAvalType).ToList();
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => d.AvalType).Select(e => new AvalStockReportViewModel()
            {
                AvalType = e.Key,
                StartAvalQuantity = e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).AvalQuantity : 0,
                StartAvalWeightQuantity = e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).AvalQuantityWeight : 0,
                InAvalQuantity = e.FirstOrDefault(d => d.Type == TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == TRANSFORM).AvalQuantity : 0,
                InAvalWeightQuantity = e.FirstOrDefault(d => d.Type == TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == TRANSFORM).AvalQuantityWeight : 0,
                OutAvalQuantity = e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).AvalQuantity : 0,
                OutAvalWeightQuantity = e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).AvalQuantityWeight : 0,
                EndAvalQuantity = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).AvalQuantity : 0)
                    + (e.FirstOrDefault(d => d.Type == TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == TRANSFORM).AvalQuantity : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).AvalQuantity : 0),
                EndAvalWeightQuantity = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).AvalQuantityWeight : 0)
                    + (e.FirstOrDefault(d => d.Type == TRANSFORM) != null ? e.FirstOrDefault(d => d.Type == TRANSFORM).AvalQuantityWeight : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).AvalQuantityWeight : 0)
            });

            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset searchDate)
        {
            var data = GetQuery(searchDate);

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
                dt.Rows.Add("", 0, 0, 0, 0, 0, 0);
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

        public ListResult<AvalStockReportViewModel> GetReportData(DateTimeOffset searchDate)
        {
            var data = GetQuery(searchDate);
            return new ListResult<AvalStockReportViewModel>(data.ToList(), 1, data.Count(), data.Count());
        }
    }
}
