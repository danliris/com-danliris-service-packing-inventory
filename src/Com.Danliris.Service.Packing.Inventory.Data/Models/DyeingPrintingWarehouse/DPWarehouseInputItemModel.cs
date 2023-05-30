using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseInputItemModel : StandardEntity
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }
        public string MaterialWidth { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public double Balance { get; set; }
        public string PackingInstruction { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }
        public DateTime CreatedUtcOrderNo { get; private set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public double PackagingLength { get; set; }
        public string Area { get; set; }
        public int DPWarehouseInputId { get; set; }

        #region Product SKU Packing
        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        #endregion

        public int ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }
        public int YarnMaterialId { get; set; }
        public string YarnMaterialName { get; set; }

        public decimal InputPackagingQty { get; set; }
        public double InputQuantity { get; set; }
        public long DeliveryOrderReturId { get; set; }

        public string DeliveryOrderReturNo { get; set; }
        public string FinishWidth { get; set; }
        public DateTimeOffset DateIn { get; set; }
        public string DestinationBuyerName { get; set; }
        public string MaterialOrigin { get; set; }
        public string DeliveryOrderSalesType { get; private set; }

        //public DPWarehouseInputModel DPWarehouseInput { get; set; }



    }
}
