using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport
{
    public interface IGarmentCMTSalesService
    {
        ListResult<GarmentCMTSalesViewModel> GetReportData(string buyerAgent,  DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyerAgent,  DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
