using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentReceiptSubconOmzetByUnitReport
{
    public interface IGarmentReceiptSubconOmzetByUnitReportService
    {
        ListResult<GarmentReceiptSubconOmzetByUnitReportViewModel> GetReportData(string unit,  DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string unit, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
