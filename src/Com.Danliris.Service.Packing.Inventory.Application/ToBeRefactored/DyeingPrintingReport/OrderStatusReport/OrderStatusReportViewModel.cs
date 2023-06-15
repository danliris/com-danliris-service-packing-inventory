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
        public double targetQty { get; set; }
        public double remainingQty { get; set; }
        public double preProductionQty { get; set; }
        public double inProductionQty { get; set; }
        public double qcQty { get; set; }
        public double producedQty { get; set; }
        public double sentGJQty { get; set; }
        public double sentBuyerQty { get; set; }
        public double remainingSentQty { get; set; }
    }
}
