using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice
{
    public interface IGarmentInvoiceMonitoringService
    {
        List<GarmentInvoiceMonitoringViewModel> GetReportData(string buyerAgentCode, string invoiceType, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgentCode, string invoiceType, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
