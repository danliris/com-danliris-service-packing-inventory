using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyerComodity
{
    public interface IOmzetYearBuyerComodityService
    {
        List<OmzetYearBuyerComodityViewModel> GetReportData(int year);
        MemoryStream GenerateExcel(int year);
    }
}
