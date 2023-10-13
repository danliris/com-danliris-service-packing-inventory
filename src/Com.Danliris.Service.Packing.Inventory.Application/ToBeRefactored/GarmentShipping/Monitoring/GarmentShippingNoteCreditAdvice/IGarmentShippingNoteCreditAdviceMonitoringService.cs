using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice
{
    public interface IGarmentShippingNoteCreditAdviceMonitoringService
    {
        List<GarmentShippingNoteCreditAdviceMonitoringViewModel> GetReportData(string buyerAgent, string noteType, string noteNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, string noteType, string noteNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
