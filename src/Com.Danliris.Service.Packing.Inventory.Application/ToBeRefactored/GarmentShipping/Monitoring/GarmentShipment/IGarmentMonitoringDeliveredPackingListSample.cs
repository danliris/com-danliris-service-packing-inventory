using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment
{
    public interface IGarmentMonitoringDeliveredPackingListSample
    {
        ListResult<GarmentMonitoringDeliveredPackingListSampleViewModel> GetReportData(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo,int offset);
        MemoryStream GenerateExcel(string invoiceNo, string paymentTerm, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offset);
    }
}
