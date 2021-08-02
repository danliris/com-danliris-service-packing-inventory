using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyBySection
{
    public interface IGarmentOmzetMonthlyBySectionService
    {
        List<GarmentOmzetMonthlyBySectionViewModel> GetReportData(string section, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string section, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
