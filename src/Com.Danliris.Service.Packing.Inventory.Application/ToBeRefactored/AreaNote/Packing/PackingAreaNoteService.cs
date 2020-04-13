using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Packing
{
    public class PackingAreaNoteService : IPackingAreaNoteService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        public PackingAreaNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private IQueryable<IndexViewModel> GetQuery(DateTimeOffset? date, string zone, string group, int offset)
        {
            var query = _repository.ReadAll();

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

            var result = query.OrderByDescending(s => s.LastModifiedUtc).Select(s => new IndexViewModel()
            {
                Area = s.Area,
                CartNo = s.CartNo,
                Color = s.Color,
                Date = s.Date,
                Grade = s.Grade,
                Group = s.Shift,
                Id = s.Id,
                Motif = s.Motif,
                ProductionOrderNo = s.ProductionOrderNo,
                Balance = s.Balance,
                Construction = s.Construction,
                Mutation = s.Mutation,
                ProductionOrderQuantity = s.ProductionOrderQuantity,
                UomUnit = s.UOMUnit

            });
            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? date, string zone, string group, int offSet)
        {
            var query = GetQuery(date, zone, group, offSet);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Group", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Aktivitas", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mutasi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packaging", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packaging", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo", DataType = typeof(decimal) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", 0, "", 0, "", 0, "");
            }
            else
            {
                foreach (var item in query)
                {
                    var stringDate = item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    dt.Rows.Add(stringDate, item.Grade, item.Activity, item.Mutation, item.ProductionOrderNo, item.CartNo, item.Construction, item.Motif, item.Color,
                        item.Grade, item.QtyPackaging, item.Packaging, item.ProductionOrderQuantity, item.UomUnit, item.Balance, "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Packing Area Dyeing Printing") }, true);
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? date, string zone, string group, int offSet)
        {
            var query = GetQuery(date, zone, group, offSet);

            return query.ToList();
        }
    }
}
