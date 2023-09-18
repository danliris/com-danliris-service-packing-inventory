using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditNote
{
    public interface IGarmentCreditNoteMonitoringService
    {
        List<GarmentCreditNoteMonitoringViewModel> GetReportData(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, DateTime? dateFrom, DateTime? dateTo, int offset);
        ListResult<GarmentCreditNoteMIIMonitoringViewModel> GetReportDataMII(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcelMII(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
