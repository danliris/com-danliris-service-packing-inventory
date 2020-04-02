using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public interface IInspectionDocumentReportService
    {
        List<InspectionDocumentReportViewModel> GetAll();
    }
}
