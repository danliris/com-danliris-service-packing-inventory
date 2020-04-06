using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.InspectionDocumentReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Data;
using System.Globalization;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public class InspectionDocumentReportService : IInspectionDocumentReportService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public InspectionDocumentReportService(IDyeingPrintingAreaMovementRepository repository)
        {
            _repository = repository;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan,int timeOffset)
        {
            var query = GetQuery(dateReport,group,mutasi,zona,keterangan, timeOffset);

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
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0,"","");
            }
            else
            {
                foreach (var item in query)
                {
                    var stringDate = item.DateReport.ToOffset(new TimeSpan(1, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    dt.Rows.Add(item.Index, stringDate, item.GroupText, item.UnitText, item.KeluarKe, item.NoSpp, item.NoKereta, item.Material,
                        item.Keterangan, item.Status, item.Lebar, item.Motif, item.Warna, item.Mtr, item.Yds,"","");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon IM") }, true);
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan,int timeOffset)
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
            //var test = query.OrderByDescending(s => s.LastModifiedUtc).ToList();
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
                Mtr = s.MeterLength.ToString("{0:N2}%"),
                //Mtr = s.MeterLength.ToString(),
                NoKereta = s.CartNo,
                UnitText = s.UnitName,
                Warna = s.Color,
                Yds = s.YardsLength.ToString("{0:N2}%")
                //Yds = s.YardsLength.ToString()
            });

            return result;
        }
    }
}
