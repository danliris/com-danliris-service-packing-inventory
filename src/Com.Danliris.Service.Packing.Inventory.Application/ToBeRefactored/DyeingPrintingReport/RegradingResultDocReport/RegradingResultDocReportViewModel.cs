using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.RegradingResultDocReport
{
    public class RegradingResultDocReportViewModel
    {
        public DateTimeOffset dateIn { get; set; }
        public DateTimeOffset dateOut { get; set; }
        public string bonNo { get; set; }
        public string productionOrderNo { get; set; }
        public string orderType { get; set; }
        public string buyer { get; set; }
        public string motif { get; set; }
        public string color { get; set; }
        public string grade { get; set; }
        public double balance { get; set; }
    }
}
