using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel
{
    public class MonitoringPreInputWarehouseViewModel
    {
        public DateTimeOffset LastModifiedUtc { get; set; }
        public string ProductPackingCode { get; set; }
        public string ProductionOrderNo { get; set; }
        public double Balance { get; set; }
        public double BalanceRemains { get; set; }
        public double BalanceReceipt { get; set; }
        public double BalanceReject { get; set; }

        public double PackagingLength { get; set; }
        public decimal PackagingQty { get; set; }
        public decimal PackagingQtyRemains { get; set; }
        public decimal PackagingQtyReceipt { get; set; }
        public decimal PackagingQtyReject { get; set; }
        public string PackagingUnit { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }
        
        public string Grade { get; set; }
        public string UomUnit { get; set; }


    }
}
