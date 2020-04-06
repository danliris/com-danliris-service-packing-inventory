using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public interface IInspectionDocumentReportService
    {
        List<IndexViewModel> GetReport(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan,int timeOffset);
        MemoryStream GenerateExcel(DateTimeOffset? dateReport, string group, string mutasi, string zona, string keterangan,int timeOffset);
    }
}
