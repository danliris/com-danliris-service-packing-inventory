using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class PlainAdjInspectionMaterialProductionOrder
    {
        public int Id { get; set; }
        public double BalanceRemains { get; set; }
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }

        public string MaterialWidth { get; set; }

        public int ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }

        public int YarnMaterialId { get; set; }
        public string YarnMaterialName { get; set; }

        public string CartNo { get; set; }

        public int BuyerId { get; set; }
        public string Buyer { get; set; }

        public string Construction { get; set; }

        public string Unit { get; set; }

        public string Color { get; set; }

        public string Motif { get; set; }

        public string UomUnit { get; set; }

        public string Area { get; set; }
    }
}
