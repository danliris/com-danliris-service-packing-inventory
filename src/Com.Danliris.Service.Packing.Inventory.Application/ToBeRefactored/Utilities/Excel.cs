using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities
{
    public static class Excel
    {
        /// <summary>
        /// Create an excel file using MemoryStream.
        /// File name is assigned later in Response.AddHeader() when you want to download.
        /// Each DataTable will be rendered in its own sheet, so you need to supply its sheet name as well.
        /// </summary>
        /// <param name="dtSourceList">A List of KeyValuePair of DataTable and its sheet name</param>
        /// <param name="styling">Default style is set to False</param>
        /// <returns>MemoryStream object to be written into Response.OutputStream</returns>
        public static MemoryStream CreateExcel(List<KeyValuePair<DataTable, string>> dtSourceList, bool styling = false)
        {
            ExcelPackage package = new ExcelPackage();
            foreach (KeyValuePair<DataTable, string> item in dtSourceList)
            {
                var sheet = package.Workbook.Worksheets.Add(item.Value);
                sheet.Cells["A1"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            }
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }
        /// <summary>
        /// Create an excel file using MemoryStream.
        /// File name is assigned later in Response.AddHeader() when you want to download.
        /// Each DataTable will be rendered in its own sheet, so you need to supply its sheet name as well.
        /// </summary>
        /// <param name="dtSourceList">A List of KeyValuePair of DataTable and its sheet name</param>
        /// <param name="tableColStart">which column pos you want to start insert table</param>
        /// <param name="tableRowStart">which row pos you want to start insert table</param>
        /// <param name="styling">Default style is set to False<</param>
        /// <returns></returns>
        public static MemoryStream CreateExcel(List<KeyValuePair<DataTable, string>> dtSourceList,int tableColStart,int tableRowStart, bool styling = false)
        {
            ExcelPackage package = new ExcelPackage();
            foreach (KeyValuePair<DataTable, string> item in dtSourceList)
            {
                var sheet = package.Workbook.Worksheets.Add(item.Value);
                sheet.Cells[tableRowStart,tableColStart].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            }
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }

        /// <summary>
        /// Create an excel file using MemoryStream.
        /// if you want defined Custom Header Sheet Layout You can use packageExist(must be defined before execute this),
        /// </summary>
        /// <param name="dtSourceList">DataTable With name Sheet</param>
        /// <param name="packageExist">Custom Header Layout Sheet</param>
        /// <param name="tableColStart">Table Column Start to insert table</param>
        /// <param name="tableRowStart">Table Row Start to insert table</param>
        /// <param name="withHeader">if you want to insert table using header</param>
        /// <param name="styling">for style </param>
        /// <returns></returns>
        public static MemoryStream CreateExcel(List<KeyValuePair<DataTable, string>> dtSourceList, ExcelPackage packageExist, int tableColStart, int tableRowStart, bool withHeader, OfficeOpenXml.Table.TableStyles styles)
        {
            foreach (KeyValuePair<DataTable, string> item in dtSourceList)
            {
                var sheet = packageExist.Workbook.Worksheets.Add(item.Value);
                sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(item.Key, true, styles);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            }
            MemoryStream stream = new MemoryStream();
            packageExist.SaveAs(stream);
            return stream;
        }
    }
}
