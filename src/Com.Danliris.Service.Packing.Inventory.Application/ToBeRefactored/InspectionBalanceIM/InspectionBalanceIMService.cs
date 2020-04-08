using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionBalanceIM
{
    public class InspectionBalanceIMService : IInspectionBalanceIMService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public InspectionBalanceIMService(IDyeingPrintingAreaMovementRepository repository)
        {
            _repository = repository;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateReport, string shift, string unit, int timeOffset)
        {
            var query = GetQuery(dateReport, shift, unit,timeOffset);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "MATERIAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NOSP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SUM OF AWAL", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SUM OF MASUK", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SUM OF KELUAR", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "SUM OF AKHIR", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.Material,item.NoOrder,item.SumOfAwal,item.SumOfMasuk,item.SumOfKeluar,item.SumOfAkhir);
                }
            }

            //build Excel Header
            ExcelPackage package = new ExcelPackage();
            #region headerExcel
            var sheet = package.Workbook.Worksheets.Add("Saldo IM");
            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = dateReport.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "SHIFT";
            sheet.Cells[2, 2].Value = shift;

            sheet.Cells[3, 1].Value = "UNIT";
            sheet.Cells[3, 2].Value = unit;

            sheet.Cells[1, 1, 6, 6].Style.Font.Bold = true;

            
            sheet.Cells[5, 1, 6, 6].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[5, 1, 6, 6].Style.Fill.BackgroundColor.SetColor(Color.Aqua);

            sheet.Cells[5, 1].Value = "MATERIAL";
            sheet.Cells[5, 1, 6, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 1, 6, 1].Merge = true;


            sheet.Cells[5, 2].Value = "NOSP";
            sheet.Cells[5, 2, 6, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 2, 6, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 2, 6, 2].Merge = true;

            sheet.Cells[5, 3].Value = "Value";
            sheet.Cells[5, 3, 5, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 3, 5, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 3, 5, 6].Merge = true;

            sheet.Cells[6, 3].Value = "Sum of Awal";
            sheet.Cells[6, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[6, 4].Value = "Sum of Masuk";
            sheet.Cells[6, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[6, 5].Value = "Sum of Keluar";
            sheet.Cells[6, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            sheet.Cells[6, 6].Value = "Sum of Akhir";
            sheet.Cells[6, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            #endregion

            int tableRowStart = 7;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon IM") }, true);
            return stream;
        }

        public List<IndexViewModel> GetReport(DateTimeOffset? dateReport, string shift, string unit, int timeOffset)
        {
            var query = GetQuery(dateReport, shift, unit, timeOffset);
            return query.ToList();
        }

        /// <summary>
        /// Get Data Balance : 
        /// Query : SELECT MaterialName,ProductionOrderNo, SUM(Balance where 'Awal') as SumOfAwal
        ///         SUM(Balance where 'Akhir') as SumOfAkhir
        ///         SUM(Balance where 'Masuk') as SumOfMasuk
        ///         SUM(Balance where 'Keluar') as SumOfKeluar
        ///         FROM DyeyingPrintingAreaMovement
        ///         where 
        ///         Date = @dateReport 
        ///         Shift = @shift
        ///         UnitName = @unit
        ///         Group by MaterialId, ProductionOrderId
        /// </summary>
        /// <param name="dateReport"></param>
        /// <param name="shift"></param>
        /// <param name="unit"></param>
        /// <param name="timeOffset"></param>
        /// <returns></returns>
        private IQueryable<IndexViewModel> GetQuery(DateTimeOffset? dateReport, string shift, string unit,int timeOffset)
        {
            var query = _repository.ReadAll();

            if (dateReport.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(timeOffset, 0, 0)).Date == dateReport.GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(shift))
            {
                query = query.Where(s => s.Shift == shift);
            }

            if (!string.IsNullOrEmpty(unit))
            {
                query = query.Where(s => s.UnitName == unit);
            }

            var result = query.GroupBy(x => new { x.MaterialId, x.ProductionOrderId }).Select(s => new IndexViewModel()
            {
                Material = s.First().MaterialName,
                NoOrder = s.First().ProductionOrderNo,
                SumOfAkhir = s.Where(sw => sw.Mutation.ToLower() == "akhir").Sum(ssum=> ssum.Balance),
                SumOfAwal = s.Where(sw => sw.Mutation.ToLower() == "awal").Sum(ssum => ssum.Balance),
                SumOfKeluar = s.Where(sw => sw.Mutation.ToLower() == "keluar").Sum(ssum => ssum.Balance),
                SumOfMasuk = s.Where(sw => sw.Mutation.ToLower() == "masuk").Sum(ssum => ssum.Balance),
            });

            return result;
        }
    }
}
