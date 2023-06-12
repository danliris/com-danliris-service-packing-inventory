using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote
{
    public interface IGarmentDebitNoteMonitoringService
    {
        List<GarmentDebitNoteMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset);

        ListResult<GarmentDebitNoteMIIMonitoringViewModel> GetReportDataMII(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcelMII(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
