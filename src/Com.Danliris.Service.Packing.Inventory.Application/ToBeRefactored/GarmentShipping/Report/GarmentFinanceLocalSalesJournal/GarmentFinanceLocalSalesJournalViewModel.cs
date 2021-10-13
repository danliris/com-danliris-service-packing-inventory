using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal
{
    public class GarmentFinanceLocalSalesJournalViewModel
    {
        public string type { get; set; }
        public string remark { get; set; }
        public string account { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }
        public bool useVat { get; set; }
    }
}
