using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public interface IGarmentPackingListMonitoringService
    {
        ListResult<GarmentPackingListMonitoringViewModel> GetReportData(int buyerAgentId, string invoiceType, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
        FileResult GenerateExcel(int buyerAgentId, string buyerAgent, string invoiceType, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
    }
}
