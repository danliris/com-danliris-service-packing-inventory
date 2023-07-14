using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public interface IGarmentFinanceDetailExportSalesJournalService
    {
        //List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
