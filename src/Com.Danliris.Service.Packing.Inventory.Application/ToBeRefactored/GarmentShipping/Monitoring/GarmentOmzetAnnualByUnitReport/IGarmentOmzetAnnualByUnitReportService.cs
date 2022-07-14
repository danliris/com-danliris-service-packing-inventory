using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetAnnualByUnitReport
{
    public interface IGarmentOmzetAnnualByUnitReportService
    {
        ListResult<AnnualOmzetByUnitViewModel> GetReportData(int year, int offset);
        MemoryStream GenerateExcel(int year, int offset);
    }
}
