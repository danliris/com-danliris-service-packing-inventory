using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLetterOfCredit
{
    public interface IGarmentLetterOfCreditMonitoringService
    {
        List<GarmentLetterOfCreditMonitoringViewModel> GetReportData(string buyerAgent, string lcNo, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, string lcNo, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
