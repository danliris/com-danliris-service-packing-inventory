using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Data;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using OfficeOpenXml;
using System.Drawing;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public class InspectionDocumentReportService : IInspectionDocumentReportService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public InspectionDocumentReportService(IDyeingPrintingAreaMovementRepository repository)
        {
            _repository = repository;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan, int timeOffset)
        {
            var query = GetQuery(dateReport, group, mutasi, zona, keterangan, timeOffset);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "TGL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "GROUP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "UNIT", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KELUAR KE", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NO KRETA", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KET", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "STATUS", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "LEBAR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MOTIF", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "WARNA", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MTR", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "YDS", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENYERAHKAN", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "MENERIMA", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    var stringDate = item.DateReport.ToOffset(new TimeSpan(1, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    dt.Rows.Add(item.Index, stringDate, item.GroupText, item.UnitText, item.KeluarKe, item.NoSpp, item.NoKereta, item.Material,
                        item.Keterangan, item.Status, item.Lebar, item.Motif, item.Warna, item.Mtr, item.Yds, "", "");
                }
            }

            //build Excel Header
            ExcelPackage package = new ExcelPackage();
            #region headerExcel
            var sheet = package.Workbook.Worksheets.Add("Bon IM");
            sheet.Cells[1, 1].Value = "Divisi";
            sheet.Cells[1, 2].Value = "DYEING PRINTING PT DANLIRIS";

            sheet.Cells[2, 1].Value = "TGL";
            sheet.Cells[2, 2].Value = query.FirstOrDefault().DateReport.ToOffset(new TimeSpan(1, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = group;

            sheet.Cells[4, 1].Value = "MUTASI";
            sheet.Cells[4, 2].Value = mutasi;

            sheet.Cells[5, 1].Value = "ZONA";
            sheet.Cells[5, 2].Value = zona;

            sheet.Cells[6, 1].Value = "KET";
            sheet.Cells[6, 2].Value = keterangan;

            sheet.Cells[2, 14].Value = "NO. BON :";
            string dateBon = dateReport.HasValue ? dateReport.Value.Date.ToString("M.dd") : "";
            sheet.Cells[2, 16].Value = $"IM.{group}-{mutasi}-{zona}-{dateBon}";
            #endregion
            sheet.Cells[1, 1, 9, 17].Style.Font.Bold = true;

            sheet.Cells[8, 1, 9, 17].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 17].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 17].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 17].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 17].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[8, 1, 9, 17].Style.Fill.BackgroundColor.SetColor(Color.Aqua);

            sheet.Cells[8, 1].Value = "NO";
            sheet.Cells[8, 1, 9, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 1, 9, 1].Merge = true;


            sheet.Cells[8, 2].Value = "TGL";
            sheet.Cells[8, 2, 9, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 2, 9, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 2, 9, 2].Merge = true;

            sheet.Cells[8, 3].Value = "GROUP";
            sheet.Cells[8, 3, 9, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 3, 9, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 3, 9, 3].Merge = true;

            sheet.Cells[8, 4].Value = "UNIT";
            sheet.Cells[8, 4, 9, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 4, 9, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 4, 9, 4].Merge = true;

            sheet.Cells[8, 5].Value = "KELUAR KE";
            sheet.Cells[8, 5, 9, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 5, 9, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 5, 9, 5].Merge = true;

            sheet.Cells[8, 6].Value = "SP";
            sheet.Cells[8, 6, 9, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 6, 9, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 6, 9, 6].Merge = true;

            sheet.Cells[8, 7].Value = "NO KRETA";
            sheet.Cells[8, 7, 9, 7].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 7, 9, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 7, 9, 7].Merge = true;

            sheet.Cells[8, 8].Value = "MATERIAL";
            sheet.Cells[8, 8, 9, 8].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 8, 9, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 8, 9, 8].Merge = true;

            sheet.Cells[8, 9].Value = "KET";
            sheet.Cells[8, 9, 9, 9].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 9, 9, 9].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 9, 9, 9].Merge = true;

            sheet.Cells[8, 10].Value = "STATUS";
            sheet.Cells[8, 10, 9, 10].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 10, 9, 10].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 10, 9, 10].Merge = true;

            sheet.Cells[8, 11].Value = "LEBAR";
            sheet.Cells[8, 11, 9, 11].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 11, 9, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 11, 9, 11].Merge = true;

            sheet.Cells[8, 12].Value = "MOTIF";
            sheet.Cells[8, 12, 9, 12].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 12, 9, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 12, 9, 12].Merge = true;

            sheet.Cells[8, 13].Value = "WARNA";
            sheet.Cells[8, 13, 9, 13].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 13, 9, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 13, 9, 13].Merge = true;

            sheet.Cells[8, 14].Value = "PANJANG";
            sheet.Cells[8, 14, 8, 15].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 14, 8, 15].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 14, 8, 15].Merge = true;

            sheet.Cells[8, 16].Value = "NAMA & PARAF";
            sheet.Cells[8, 16, 8, 17].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 16, 8, 17].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[8, 16, 8, 17].Merge = true;

            sheet.Cells[9, 14].Value = "MTR";
            sheet.Cells[9, 14].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[9, 14].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[9, 15].Value = "YDS";
            sheet.Cells[9, 15].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[9, 15].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[9, 16].Value = "MENYERAHKAN";
            sheet.Cells[9, 16].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[9, 16].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[9, 17].Value = "MENERIMA";
            sheet.Cells[9, 17].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[9, 17].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


            int tableRowStart = 10;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon IM") }, true);
            return stream;
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan, int timeOffset)
        {
            var query = GetQuery(dateReport, group, mutasi, zona, keterangan, timeOffset);
            return query.ToList();
        }

        /// <summary>
        /// Get Data Inspection Document Report :
        /// Query : SELECT FROM DyeyingPrintingAreaMovement where 
        ///         Date = @dateReport 
        ///         Shift = @group
        ///         Mutation = @mutasi
        ///         SourceArea= @zona,
        ///         Status = keterangan
        /// </summary>
        /// <param name="dateReport">Fieldname db : Date</param>
        /// <param name="group">Fieldname db : Shift</param>
        /// <param name="mutasi">Fieldname db : Mutation</param>
        /// <param name="zona">Fieldname db : SourceApps</param>
        /// <param name="keterangan">Fieldname db : Status</param>
        /// <returns></returns>
        private IQueryable<IndexViewModel> GetQuery(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan, int timeOffset)
        {
            var query = _repository.ReadAll();

            if (dateReport.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(timeOffset, 0, 0)).Date == dateReport.GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(zona))
            {
                query = query.Where(s => s.Area == zona);
            }

            if (!string.IsNullOrEmpty(group))
            {
                query = query.Where(s => s.Shift == group);
            }

            if (!string.IsNullOrEmpty(mutasi))
            {
                query = query.Where(s => s.Mutation == mutasi);
            }

            var result = query.OrderByDescending(s => s.LastModifiedUtc).Select(s => new IndexViewModel()
            {
                DateReport = s.Date,
                GroupText = s.Shift,
                Status = s.Status,
                NoSpp = s.ProductionOrderNo,
                KeluarKe = s.Area,
                Keterangan = s.Status,
                Lebar = s.MaterialWidth,
                Material = s.MaterialName,
                Motif = s.Motif,
                Mtr = s.MeterLength.ToString(),
                NoKereta = s.CartNo,
                UnitText = s.UnitName,
                Warna = s.Color,
                Yds = s.YardsLength.ToString()
            });

            return result;
        }
    }
}
