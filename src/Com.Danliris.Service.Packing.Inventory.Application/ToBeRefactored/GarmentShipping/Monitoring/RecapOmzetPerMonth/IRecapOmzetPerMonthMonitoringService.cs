using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public interface IRecapOmzetPerMonthMonitoringService
    {
        ListResult<RecapOmzetPerMonthMonitoringViewModel> GetReportData(int month, int year);
        FileResult GenerateExcel(int month, int year);
    }
}
