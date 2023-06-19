using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public class OrderStatusReportViewModel
    {
        public DateTimeOffset date { get; set; }
        public string bonNo { get; set; }
        public long productionOrderId { get; set; }
        public string productionOrderNo { get; set; }
        public decimal targetQty { get; set; }
        public decimal remainingQty { get; set; }
        public decimal preProductionQty { get; set; }
        public decimal inProductionQty { get; set; }
        public decimal qcQty { get; set; }
        public decimal producedQty { get; set; }
        public decimal sentGJQty { get; set; }
        public decimal sentBuyerQty { get; set; }
        public decimal remainingSentQty { get; set; }
    }
}
