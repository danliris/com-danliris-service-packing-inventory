using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.ProductionSubmissionReport
{
    public class ProductionSubmissionReportViewModel
    {
        public DateTimeOffset date { get; set; }
        public string bonNo { get; set; }
        public long productionOrderId { get; set; }
        public string productionOrderNo { get; set; }
        public string orderType { get; set; }
        public string buyer { get; set; }
        public string construction { get; set; }
        public string motif { get; set; }
        public string color { get; set; }
        public string cartNo { get; set; }
        public double inputQuantity { get; set; }
    }
}
