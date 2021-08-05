using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice
{
    public interface IGarmentCreditAdviceMonitoringService
    {
        List<GarmentCreditAdviceMonitoringViewModel> GetReportData(string buyerAgent, string invoiceNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent, string invoiceNo, string paymentTerm, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
