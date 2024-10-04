using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport
{
    public interface IGarmentRecapOmzetReportService
    {
        ListResult<GarmentRecapOmzetReportViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<int> PushToBeginingBalance(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
