using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseOutputItemModel : StandardEntity
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; private set; }
        public int MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionName { get; private set; }
        public string MaterialWidth { get; private set; }
        public int BuyerId { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public string Remark { get; private set; }
        public string Grade { get; private set; }
        public double Balance { get; private set; }
        public string PackingInstruction { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }
        public string PackagingType { get; private set; }
        public decimal PackagingQty { get; private set; }
        public double PackagingLength { get; private set; }
        public string PackagingUnit { get; private set; }
        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }
        public string ProductionMachine { get; set; }
        public string Area { get; private set; }
        public string DestinationArea { get; private set; }
        public string Description { get; set; }
        public string ShippingGrade { get; private set; }
        public string ShippingRemark { get; private set; }
        public int DPWarehouseOutputId { get; set; }
        public int DPWarehouseSummaryId { get; set; }
        #region Product SKU Packing
        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        #endregion
        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }
        public int YarnMaterialId { get; private set; }
        public string YarnMaterialName { get; private set; }
        public string NextAreaInputStatus { get;  set; }
        public bool HasNextAreaDocument { get;  set; }
        public string FinishWidth { get; private set; }
        public DateTimeOffset DateOut { get; private set; }
        public string DestinationBuyerName { get; set; }
        public string MaterialOrigin { get; set; }
        public string DeliveryOrderSalesType { get; private set; }
        public string PackingListBaleNo { get; private set; }
        public decimal PackingListNet { get; private set; }
        public decimal PackingListGross { get; private set; }

        public int? ProductTextileId { get; set; }
        public string ProductTextileCode { get; set; }
        public string ProductTextileName { get; set; }
        #region Track
        public int TrackFromId { get; private set; }
        public string TrackFromType { get; private set; }
        public string TrackFromName { get; private set; }
        public string TrackFromBox { get; private set; }
        #endregion
        public DPWarehouseOutputModel DPWarehouseOutput { get; set; }

        public DPWarehouseOutputItemModel()
        {
        }


        public DPWarehouseOutputItemModel(long productionOrderId, string productionOrderNo, int materialId, string materalName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int buyerId, string buyer, string construction, string unit, string color, string motif, string uomUnit, string remark, string grade, double balance, 
            string packingInstruction, string productionOrderType, double productionOrderOrderQuantity, string packagingType, decimal packagingQty, double packagingLength, string packagingUnit,
            long deliveryOrderSalesId, string deliveryOrderSalesNo, string deliveryOrderSalesType, string productionMachine, string area, string description, int dPWarehouseOutputId, int productSKUId, int fabricSKUId, string productSKUCode,
            int productPackingId, int fabricPackingId, string productPackingCode, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, 
            string finishWidth, string materialOrigin,  int dPWarehouseSummaryId, int trackFromId, string trackFromType, string trackFromName, string trackFromBox, string destinationArea,
            string destinationBuyerName
            ) : this()
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            MaterialId = materialId;
            MaterialName = materalName;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialWidth = materialWidth;
            BuyerId = buyerId;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Remark = remark;
            Grade = grade;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingLength = packagingLength;
            PackagingUnit = packagingUnit;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            DeliveryOrderSalesType = deliveryOrderSalesType;
            ProductionMachine = productionMachine;
            Area = area;
            Description = description;
            DPWarehouseOutputId = dPWarehouseOutputId;
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            FinishWidth = finishWidth;
            MaterialOrigin = materialOrigin;
            
            DPWarehouseSummaryId = dPWarehouseSummaryId;
            TrackFromId = trackFromId;
            TrackFromType = trackFromType;
            TrackFromName = trackFromName;
            TrackFromBox = trackFromBox;
            DestinationArea = destinationArea;
            DestinationBuyerName = destinationBuyerName;

        }

        
    }
}
