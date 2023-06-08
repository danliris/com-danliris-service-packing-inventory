using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.RegradingResultDocReport
{
    public interface IRegradingResultDocReportService
    {
        List<RegradingResultDocReportViewModel> GetReportData(string orderNo, DateTime startdate, DateTime finishdate, int offset);
        MemoryStream GenerateExcel(string orderNo, DateTime startdate, DateTime finishdate, int offset);
    }
}
