using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport
{
    public interface IQcToWarehouseReportService
    {
        List<QcToWarehouseReportViewModel> GetReportData(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset);
        MemoryStream GenerateExcel(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset);
    }
}
