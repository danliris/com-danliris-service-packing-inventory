using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport
{
    public interface IGarmentInsuranceDispositionReportService
    {
        List<GarmentInsuranceDispositionReportViewModel> GetReportData(string policytype, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string policytype, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
