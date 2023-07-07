using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT.ViewModel
{
    public class DPOutputWarehouseMonitoringViewModel
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductPackingCode { get; set; }
        public string ProcessTypeName { get; set; }
        public string Grade { get; set; }
        public string Color { get; set; }
        public string Construction { get; set; }
        public string Motif { get; set; }
        public string Description { get; set; }
        public decimal PackagingQty { get; set; }
        public double PackingLength { get; set; }
        public double Balance { get; set; }
        public string PackagingUnit { get; set; }
        public DateTime DateIn { get; set; }
        public string TrackName { get; set; }
        public string UomUnit { get; set; }
    }
}
