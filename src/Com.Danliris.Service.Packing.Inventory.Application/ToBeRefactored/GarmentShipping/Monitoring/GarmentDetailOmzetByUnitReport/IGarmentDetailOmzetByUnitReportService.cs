using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport
{
    public interface IGarmentDetailOmzetByUnitReportService
    {
        Task<ListResult<GarmentDetailOmzetByUnitReportViewModel>> GetReportData(string unit,  DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<MemoryStream> GenerateExcel(string unit, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
