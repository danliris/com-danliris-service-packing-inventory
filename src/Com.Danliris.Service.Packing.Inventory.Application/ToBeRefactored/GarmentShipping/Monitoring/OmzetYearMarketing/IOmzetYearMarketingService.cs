using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearMarketing
{
    public interface IOmzetYearMarketingService
    {
        List<OmzetYearMarketingViewModel> GetReportData(int year);
        MemoryStream GenerateExcel(int year);
    }
}
