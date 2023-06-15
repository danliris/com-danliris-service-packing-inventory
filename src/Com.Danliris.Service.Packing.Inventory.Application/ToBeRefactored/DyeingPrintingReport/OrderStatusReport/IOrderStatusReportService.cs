using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public interface IOrderStatusReportService
    {
        List<OrderStatusReportViewModel> GetReportData(string orderType, string year);
        MemoryStream GenerateExcel(string orderType, string year);
    }
}
