using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearSection
{
    public interface IOmzetYearSectionService
    {
        OmzetYearSectionViewModel GetReportData(int year);
        FileResult GenerateExcel(int year);
    }
}
