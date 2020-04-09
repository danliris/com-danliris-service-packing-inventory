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
using OfficeOpenXml;

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
            //dt.Columns.Add(new DataColumn() { ColumnName = "Menyerahkan", DataType = typeof(string) });
            //dt.Columns.Add(new DataColumn() { ColumnName = "Menerima", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
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
                                item.UomUnit,
                                0);
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");
            sheet.Cells[1, 1].Value = "DIVISI";
            sheet.Cells[1, 2].Value = "DYEING PRINTING PT DANLIRIS";

            sheet.Cells[2, 1].Value = "TANGGAL";
            sheet.Cells[2, 2].Value = date.HasValue ? date.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID")) : "";

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = group;

            sheet.Cells[4, 1].Value = "MUTASI";
            sheet.Cells[4, 2].Value = mutation;

            sheet.Cells[5, 1].Value = "ZONA";
            sheet.Cells[5, 2].Value = zone;

            sheet.Cells[6, 1].Value = "TANGGAL";
            sheet.Cells[6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 1].AutoFitColumns();
            sheet.Cells[6, 1, 7, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 1, 7, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 1, 7, 1].Merge = true;

            sheet.Cells[6, 2].Value = "GROUP";
            sheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 2].AutoFitColumns();
            sheet.Cells[6, 2, 7, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 2, 7, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 2, 7, 2].Merge = true;

            sheet.Cells[6, 3].Value = "UNIT";
            sheet.Cells[6, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 3].AutoFitColumns();
            sheet.Cells[6, 3, 7, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 3, 7, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 3, 7, 3].Merge = true;

            sheet.Cells[6, 4].Value = "KELUAR KE";
            sheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 4].AutoFitColumns();
            sheet.Cells[6, 4, 7, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 4, 7, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 4, 7, 4].Merge = true;

            sheet.Cells[6, 5].Value = "NO. KERETA";
            sheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 5].AutoFitColumns();
            sheet.Cells[6, 5, 7, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 5, 7, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 5, 7, 5].Merge = true;

            sheet.Cells[6, 6].Value = "KODE BON";
            sheet.Cells[6, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 6].AutoFitColumns();
            sheet.Cells[6, 6, 7, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 6, 7, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 6, 7, 6].Merge = true;

            sheet.Cells[6, 7].Value = "JENIS";
            sheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 7].AutoFitColumns();
            sheet.Cells[6, 7, 7, 7].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 7, 7, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 7, 7, 7].Merge = true;

            sheet.Cells[6, 8].Value = "SAT";
            sheet.Cells[6, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 8].AutoFitColumns();
            sheet.Cells[6, 8, 7, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 8, 7, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 8, 6, 9].Merge = true;

            sheet.Cells[7, 8].Value = "QTY";
            sheet.Cells[7, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 8].AutoFitColumns();
            sheet.Cells[7, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[7, 9].Value = "KET";
            sheet.Cells[7, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 9].AutoFitColumns();
            sheet.Cells[7, 9].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 9].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[6, 10].Value = "KG";
            sheet.Cells[6, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 1].AutoFitColumns();
            sheet.Cells[6, 10, 6, 11].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 10, 6, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[6, 11].Value = "NAMA & PARAF";
            sheet.Cells[6, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[6, 11].AutoFitColumns();
            sheet.Cells[6, 11, 7, 11].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 11, 7, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 11, 6, 12].Merge = true;

            sheet.Cells[7, 11].Value = "MENYERAHKAN";
            sheet.Cells[7, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 11].AutoFitColumns();
            sheet.Cells[7, 11].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[7, 12].Value = "MENERIMA";
            sheet.Cells[7, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 12].AutoFitColumns();
            sheet.Cells[7, 12].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[7, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            #endregion

            int tableRowStart = 8;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Aval Area Dyeing Printing") }, true);
            return stream;
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? date, string group, string mutation, string zone, int offSet)
        {
            var query = GetQuery(date, group, mutation, zone, offSet);

            return query.ToList();
        }
    }
}
