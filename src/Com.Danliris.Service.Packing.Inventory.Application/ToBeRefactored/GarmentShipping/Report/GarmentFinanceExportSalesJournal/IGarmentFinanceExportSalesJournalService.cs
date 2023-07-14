using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public interface IGarmentFinanceExportSalesJournalService
    {
        //List<GarmentFinanceExportSalesJournalViewModel> GetReportData(int month, int year, int offset);
        //MemoryStream GenerateExcel(int month, int year, int offset);
        List<GarmentFinanceExportSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
