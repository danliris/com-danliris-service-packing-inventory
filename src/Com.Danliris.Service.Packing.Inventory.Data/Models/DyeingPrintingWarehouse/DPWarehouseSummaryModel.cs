using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseSummaryModel : StandardEntity
    {
        public double Balance { get; set; }
        public double BalanceRemains { get; set; }
        public double BalanceOut { get; set; }

        public int BuyerId { get; set; }
        public string Buyer { get; private set; }
        public string CartNo { get; private set; }
        public string Color { get; private set; }

        public string Construction { get; private set; }
        public int DyeingPrintingStockOpnameId { get; set; }
        public string Grade { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionName { get; private set; }

        public int MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string MaterialWidth { get; private set; }
        public string Motif { get; private set; }

        public string PackingInstruction { get; private set; }
        public decimal PackagingQty { get; private set; }
        public decimal PackagingQtyRemains { get; private set; }
        public decimal PackagingQtyOut { get; private set; }
        public double PackagingLength { get; private set; }
        public string PackagingType { get; private set; }
        public string PackagingUnit { get; private set; }

        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }
        public DateTime CreatedUtcOrderNo { get; private set; }
     
        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }

        public int YarnMaterialId { get; private set; }
        public string YarnMaterialName { get; private set; }
        public string Unit { get; private set; }
        public string UomUnit { get; private set; }
        public int TrackId { get; private set; }
        public string TrackType { get; private set; }
        public string TrackName { get; private set; }
        public string TrackBox { get; private set; }


        public double SplitQuantity { get; set; }


        #region Product SKU Packing

        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        #endregion
    }
}
