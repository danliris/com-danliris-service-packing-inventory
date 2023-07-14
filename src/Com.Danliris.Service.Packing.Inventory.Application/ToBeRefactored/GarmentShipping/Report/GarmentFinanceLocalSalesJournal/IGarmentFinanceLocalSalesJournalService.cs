using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal
{
    public interface IGarmentFinanceLocalSalesJournalService
    {
        //List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(int month, int year, int offset);
        //MemoryStream GenerateExcel(int month, int year, int offset);
        List<GarmentFinanceLocalSalesJournalViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
