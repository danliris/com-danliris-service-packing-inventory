using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCoverLetter
{
    public interface IGarmentCoverLetterMonitoringService
    {
        List<GarmentCoverLetterMonitoringViewModel> GetReportData(string buyerAgent, string emkl, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, string emkl, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
