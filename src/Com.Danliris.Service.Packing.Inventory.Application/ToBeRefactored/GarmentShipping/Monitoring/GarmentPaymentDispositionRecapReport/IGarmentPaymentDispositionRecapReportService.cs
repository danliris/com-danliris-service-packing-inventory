using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionRecapReport
{
    public interface IGarmentPaymentDispositionRecapReportService
    {
        List<GarmentPaymentDispositionRecapReportViewModel> GetReportData(string emkl, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string emkl, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
