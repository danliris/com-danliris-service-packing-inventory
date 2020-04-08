using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse
{
    public class GoodsWarehouseDocumentsService : IGoodsWarehouseDocumentsService
    {
        public MemoryStream GetExcel(DateTimeOffset? date, string group, string zona, int timeOffSet)
        {
            var query = GetQuery(date, group, zona, timeOffSet);
            DataTable dt = new DataTable();

            #region Mapping Properties Class to Head excel
            Dictionary<string, string> mappedClass = new Dictionary<string, string>
            {
                {"Group","Group" },
                {"Activities","Aktivitas" },
                {"Mutation","Mutasi"},
                {"NoOrder","No. SPP"},
                {"Construction","Konstruksi"},
                {"Motif","Motif"},
                {"Color","Warna"},
                {"Grade","Grade"},
                {"QtyPacking","Qty Packaging"},
                {"Packaging","Packaging"},
                {"Qty","Qty"},
                {"Satuan","Satuan"},
                {"Balance","Balance"},
                {"Menyerahkan","Menyerahkan" },
                {"Menerima","Menerima" },
            };
            var listClass = query.ToList().FirstOrDefault().GetType().GetProperties();
            #endregion
            #region Assign Column
            foreach(var prop in mappedClass.Select((item,index)=> new { Index = index, Items = item}))
            {
                string fieldName = prop.Items.Value;
                dt.Columns.Add(new DataColumn() { ColumnName = fieldName, DataType = typeof(string) });
            }
            #endregion
            #region Assign Data
            foreach(var item in query)
            {
                List<string> data = new List<string>();
                foreach(DataColumn column in dt.Columns)
                {
                    var searchMappedClass = mappedClass.Where(x => x.Value == column.ColumnName);
                    string valueClass = "";
                    if (searchMappedClass != null)
                    {
                        var searchProperty = item.GetType().GetProperty(searchMappedClass.FirstOrDefault().Key);
                        valueClass = searchProperty == null ? "" : searchProperty.GetValue(item).ToString(); 
                    }
                    data.Add(valueClass);
                }
                dt.Rows.Add(data.ToArray());
            }
            #endregion

            #region Render Excel Header
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("BON GUDANG BARANG JADI");
            sheet.Cells[1, 1].Value = "TANGGAL";
            sheet.Cells[1, 2].Value = date.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[2, 1].Value = "GROUP";
            sheet.Cells[2, 2].Value = group;

            sheet.Cells[3, 1].Value = "ZONA";
            sheet.Cells[3, 2].Value = zona;

            int startHeaderColumn = 1;
            int endHeaderColumn = mappedClass.Count;

            sheet.Cells[1, 1, 6, endHeaderColumn].Style.Font.Bold = true;


            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells[5, startHeaderColumn, 6, endHeaderColumn].Style.Fill.BackgroundColor.SetColor(Color.Aqua);

            foreach(DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "Menyerahkan" && column.ColumnName != "Menerima")
                {
                    sheet.Cells[5, startHeaderColumn].Value = column.ColumnName;
                    sheet.Cells[5, startHeaderColumn, 6, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[5, startHeaderColumn, 6, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[5, startHeaderColumn, 6, startHeaderColumn].Merge = true;
                    startHeaderColumn++;
                }
            }

            sheet.Cells[5, startHeaderColumn].Value = "Paraf";
            sheet.Cells[5, startHeaderColumn, 5, startHeaderColumn+1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 5, startHeaderColumn+1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, startHeaderColumn, 5, startHeaderColumn+1].Merge = true;

            sheet.Cells[6, startHeaderColumn].Value = "Menyerahkan";
            sheet.Cells[6, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            startHeaderColumn++;

            sheet.Cells[6, startHeaderColumn].Value = "Menerima";
            sheet.Cells[6, startHeaderColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[6, startHeaderColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            #endregion

            #region Insert Data To Excel
            int tableRowStart = 7;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            #endregion

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return stream;
        }

        public List<IndexViewModel> GetList(DateTimeOffset? date, string group, string zona, int timeOffSet)
        {
            var result = GetQuery(date, group, zona, timeOffSet);
            return result.ToList();
        }

        public IQueryable<IndexViewModel> GetQuery(DateTimeOffset? date, string group, string zona, int timeOffSet)
        {
            var query = GetDummy();

            if (date.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(timeOffSet, 0, 0)).Date == date.GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(group))
            {
                query = query.Where(s => s.Group == group);
            }

            if (!string.IsNullOrEmpty(zona))
            {
                query = query.Where(s => s.Zona == zona);
            }

            //var result = query
            return query;

        }

        public IQueryable<IndexViewModel> GetDummy()
        {
            var result = new List<IndexViewModel>()
            {
                new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020,04,04)),
                    Group = "PAGI",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "Order001",
                    Construction = "PC 001 010",
                    Motif = "Batik",
                    Color = "Biru",
                    Grade = "A",
                    QtyPacking = "1 Rolls",
                    Qty = "1",
                    Packaging = "Rolls",
                    Satuan = "Meter",
                    Balance = "10.000",
                    Zona = "PROD"
                },
                new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020,04,04)),
                    Group = "PAGI",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "TS-11",
                    Construction = "Agregat 10",
                    Motif = "Polos",
                    Color = "Kuning",
                    Grade = "B",
                    QtyPacking = "10 Packs",
                    Qty = "1",
                    Packaging = "Packs",
                    Satuan = "Meter",
                    Balance = "1000",
                    Zona = "PROD"
                },
                new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020,04,04)),
                    Group = "SIANG",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "Order001",
                    Construction = "PC 001 010",
                    Motif = "Batik",
                    Color = "Biru",
                    Grade = "A",
                    QtyPacking = "1 Rolls",
                    Qty = "1",
                    Packaging = "Rolls",
                    Satuan = "Satu",
                    Balance = "10.000",
                    Zona = "PROD"
                },
                new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020,04,04)),
                    Group = "SIANG",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "Order001",
                    Construction = "PC 001 010",
                    Motif = "Batik",
                    Color = "Biru",
                    Grade = "A",
                    QtyPacking = "1 Rolls",
                    Qty = "1",
                    Packaging = "Rolls",
                    Satuan = "Satu",
                    Balance = "10.000",
                    Zona = "PROD"
                }
            };
            return result.AsQueryable();
        }
    }
}
