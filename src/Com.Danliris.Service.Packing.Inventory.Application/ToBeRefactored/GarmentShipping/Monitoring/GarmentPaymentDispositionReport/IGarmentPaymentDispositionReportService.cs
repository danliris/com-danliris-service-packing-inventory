using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport
{
    public interface IGarmentPaymentDispositionReportService
    {
        List<GarmentPaymentDispositionReportViewModel> GetReportData(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string paymentType, string unit, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
