using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class PlainAdjWarehouseProductionOrder
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }

        public string MaterialWidth { get; set; }

        public string Grade { get; set; }

        public string Packing { get; set; }

        public int BuyerId { get; set; }
        public string Buyer { get; set; }

        public string Construction { get; set; }

        public string Unit { get; set; }

        public string Color { get; set; }

        public string Motif { get; set; }

        public string UomUnit { get; set; }

        public string Area { get; set; }

        public decimal PackagingQty { get; set; }
        public double Balance { get; set; }
        public string PackagingType { get; set; }
    }
}
