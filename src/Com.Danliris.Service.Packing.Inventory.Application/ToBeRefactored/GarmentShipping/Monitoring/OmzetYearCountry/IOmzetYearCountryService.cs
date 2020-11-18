using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearCountry
{
    public interface IOmzetYearCountryService
    {
        OmzetYearCountryViewModel GetReportData(int year);
        MemoryStreamResult GenerateExcel(int year);
    }
}
