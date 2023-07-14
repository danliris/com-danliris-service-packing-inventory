using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report
{
    public class GarmentFinanceExportSalesJournalViewModel
    {
        public string remark { get; set; }
        public string account { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
    }

    public class GarmentFinanceExportSalesJournalTempViewModel
    {
        public string CurrencyCode { get; set; }
        public string InvoiceType { get; set; }
        public DateTimeOffset PEBDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Rate { get; set; }
    }
}
