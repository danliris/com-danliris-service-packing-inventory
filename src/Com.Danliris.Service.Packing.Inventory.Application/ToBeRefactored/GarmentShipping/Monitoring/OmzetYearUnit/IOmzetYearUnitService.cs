using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearUnit
{
    public interface IOmzetYearUnitService
    {
        OmzetYearUnitViewModel GetReportData(int year);
        FileResult GenerateExcel(int year);
    }
}
