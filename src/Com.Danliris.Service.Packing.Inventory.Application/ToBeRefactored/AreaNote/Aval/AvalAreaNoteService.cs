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

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Aval
{
    public class AvalAreaNoteService : IAvalAreaNoteService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;
        public AvalAreaNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        private IQueryable<IndexViewModel> GetQuery(DateTimeOffset? date, string group, string mutation, string zone, int offset)
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
                Id = s.Id,
                Area = s.Area,
                Date = s.Date,
                Group = s.Shift,
                BonNo = s.BonNo,
                Unit = s.UnitName,
                Mutation = s.Mutation,
                ProductionOrderNo = s.ProductionOrderNo,
                CartNo = s.CartNo,
                ProductionOrderType = s.ProductionOrderType,
                ProductionOrderQuantity = s.ProductionOrderQuantity,
                UomUnit = s.UOMUnit,
                //MassKg = s.MassKg

            });
            return result;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? date, string group, string mutation, string zone, int offSet)
        {
            var query = GetQuery(date, group, mutation, zone, offSet);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Group", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar Ke", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Kode Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Ket", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Kg", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Menyerahkan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Menerima", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    var stringDate = item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    dt.Rows.Add(stringDate, 
                                item.Group, 
                                item.Unit, 
                                item.Mutation, 
                                item.CartNo, 
                                item.BonNo, 
                                item.ProductionOrderType, 
                                item.ProductionOrderQuantity, 
                                item.UomUnit);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Aval Area Dyeing Printing") }, true);
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? date, string group, string mutation, string zone, int offSet)
        {
            var query = GetQuery(date, group, mutation, zone, offSet);

            return query.ToList();
        }
    }
}
