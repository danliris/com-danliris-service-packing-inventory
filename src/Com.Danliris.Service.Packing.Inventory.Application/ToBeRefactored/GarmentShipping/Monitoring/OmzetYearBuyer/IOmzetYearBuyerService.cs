using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyer
{
    public interface IOmzetYearBuyerService
    {
        OmzetYearBuyerViewModel GetReportData(int year);
        ExcelResult GenerateExcel(int year);
    }
}
