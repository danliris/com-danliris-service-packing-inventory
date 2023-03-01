using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class ReportSOViewModel
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductPackingCode { get; set; }
        public string ProcessTypeName { get; set; }
        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public string Grade { get; set; }
        public string Color { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public double SaldoBegin { get; set; }
        public double InQty { get; set; }
        public double OutQty { get; set; }
        public double Total { get; set; }

        
    }
}
