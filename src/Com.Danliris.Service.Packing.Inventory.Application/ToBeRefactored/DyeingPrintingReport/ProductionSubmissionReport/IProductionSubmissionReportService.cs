using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.ProductionSubmissionReport
{
    public interface IProductionSubmissionReportService
    {
        List<ProductionSubmissionReportViewModel> GetReportData(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset);
        MemoryStream GenerateExcel(string bonNo, string orderNo, DateTime startdate, DateTime finishdate, int offset);
    }
}
