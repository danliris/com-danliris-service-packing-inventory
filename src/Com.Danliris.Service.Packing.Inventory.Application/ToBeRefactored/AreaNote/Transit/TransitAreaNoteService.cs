using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Transit
{
    public class TransitAreaNoteService : ITransitAreaNoteService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public TransitAreaNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private IQueryable<IndexViewModel> GetQuery(DateTimeOffset? date, string zone, string group, string mutation, int offset)
        {
            var query = _repository.ReadAll().Where(s => s.DyeingPrintingAreaMovementHistories.OrderByDescending(d => d.Index).FirstOrDefault().Index == AreaEnum.TRANSIT);

            if (date.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date == date.GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(zone))
            {
                query = query.Where(s => s.Area == zone);
            }

            if (!string.IsNullOrEmpty(group))
            {
                query = query.Where(s => s.Shift == group);
            }

            if (!string.IsNullOrEmpty(mutation))
            {
                query = query.Where(s => s.Mutation == mutation);
            }

            var result = query.OrderByDescending(s => s.LastModifiedUtc).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                CartNo = s.CartNo,
                Color = s.Color,
                Date = s.Date,
                Grade = s.Grade,
                Group = s.Shift,
                Id = s.Id,
                MaterialConstructionName = s.MaterialConstructionName,
                MaterialName = s.MaterialName,
                MaterialWidth = s.MaterialWidth,
                MeterLength = s.MeterLength,
                Motif = s.Motif,
                ProductionOrderNo = s.ProductionOrderNo,
                SourceArea = s.SourceArea,
                UnitName = s.UnitName,
                YardsLength = s.YardsLength,
                Remark = s.Remark
            });
            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? date, string zone, string group, string mutation, int offSet)
        {
            var query = GetQuery(date, zone, group, mutation, offSet);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Group", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk Dari", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mtr", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Yds", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "");
            }
            else
            {
                foreach (var item in query)
                {
                    var stringDate = item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    dt.Rows.Add(stringDate, item.Group, item.UnitName, item.SourceArea, item.ProductionOrderNo, item.CartNo, item.MaterialName, item.MaterialConstructionName,
                        item.MaterialWidth, item.Remark, item.Grade, item.Motif, item.Color, item.MeterLength, item.YardsLength, "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Transit Area Dyeing Printing") }, true);
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? date, string zone, string group, string mutation, int offSet)
        {
            var query = GetQuery(date, zone, group, mutation, offSet);

            return query.ToList();
        }
    }
}
