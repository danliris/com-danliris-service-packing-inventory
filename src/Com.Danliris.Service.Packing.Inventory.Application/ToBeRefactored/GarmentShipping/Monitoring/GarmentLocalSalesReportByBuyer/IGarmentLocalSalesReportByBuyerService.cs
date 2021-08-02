using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesReportByBuyer
{
    public interface IGarmentLocalSalesReportByBuyerService
    {
        List<GarmentLocalSalesReportByBuyerViewModel> GetReportData(string buyer, string lstype, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string buyer, string lstype, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
