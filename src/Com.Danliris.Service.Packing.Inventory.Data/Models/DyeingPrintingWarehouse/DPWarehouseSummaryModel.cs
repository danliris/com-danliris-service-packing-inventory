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
        //public int DyeingPrintingStockOpnameId { get; set; }
        public string Grade { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionName { get; private set; }

        public int MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string MaterialWidth { get; private set; }
        public string Motif { get; private set; }

        public string PackingInstruction { get; private set; }
        public decimal PackagingQty { get; set; }
        public decimal PackagingQtyRemains { get; set; }
        public decimal PackagingQtyOut { get;  set; }
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
        public int TrackId { get;  set; }
        public string TrackType { get;  set; }
        public string TrackName { get;  set; }
        public string TrackBox { get;  set; }


        public double SplitQuantity { get; set; }
        public string Description { get; set; }
        public string MaterialOrigin { get; set; }
        public string Remark { get; set; }
        public string FinishWidth { get; set; }


        #region Product SKU Packing

        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        #endregion

        public DPWarehouseSummaryModel(double balance, double balanceRemains, double balanceOut, int buyerId, string buyer, string cartNo, string color, string grade, string construction,
         int materialConstructionId, string materialConstructionName,
         int materialId,
         string materialName,
         string materialWidth,
         string motif,
         string packingInstruction,
         decimal packagingQty,
         decimal packagingQtyRemains,
         decimal packagingQtyOut,
         double packagingLength,
         string packagingType,
         string packagingUnit,
          long productionOrderId,
         string productionOrderNo,
         string productionOrderType,
         double productionOrderOrderQuantity,
         DateTime createdUtcOrderNo,
         int processTypeId,
         string processTypeName,
         int yarnMaterialId,
         string yarnMaterialName,
         string unit,
         string uomUnit,
         //int trackId,
         //string trackType,
         //string trackName,
         //string trackBox,
         double splitQuantity,
         string description,
         int productSKUId,
         int fabricSKUId,
         string productSKUCode,
         int productPackingId,
         int fabricPackingId,
         string productPackingCode,
         string materialOrigin,
         string remark,
         string finishWidth
         ) 
        {
            Balance = balance;
            BalanceRemains = balanceRemains;
            BalanceOut =balanceOut;
            BuyerId = buyerId;
            Buyer = buyer;
            CartNo = cartNo;
            Color = color;
            Construction = construction;
            Grade = grade;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialWidth = materialWidth;
            Motif = motif;
            PackingInstruction = packingInstruction;
            PackagingQty = packagingQty;
            PackagingQtyRemains = packagingQtyRemains;
            PackagingQtyOut = packagingQtyOut;
            PackagingLength = packagingLength;
            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            CreatedUtcOrderNo = createdUtcOrderNo;
            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            Unit = unit;
            UomUnit = uomUnit;
            //TrackId = trackId;
            //TrackType = trackType;
            //TrackName = trackName;
            //TrackBox = trackBox;
            SplitQuantity = splitQuantity;
            Description = description;
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            MaterialOrigin = materialOrigin;
            Remark = remark;
            FinishWidth = finishWidth;
        }

        public DPWarehouseSummaryModel(double balance, double balanceRemains, double balanceOut, int buyerId, string buyer, string cartNo, string color, string grade, string construction,
         int materialConstructionId, string materialConstructionName,
         int materialId,
         string materialName,
         string materialWidth,
         string motif,
         string packingInstruction,
         decimal packagingQty,
         decimal packagingQtyRemains,
         decimal packagingQtyOut,
         double packagingLength,
         string packagingType,
         string packagingUnit,
          long productionOrderId,
         string productionOrderNo,
         string productionOrderType,
         double productionOrderOrderQuantity,
         DateTime createdUtcOrderNo,
         int processTypeId,
         string processTypeName,
         int yarnMaterialId,
         string yarnMaterialName,
         string unit,
         string uomUnit,
         int trackId,
         string trackType,
         string trackName,
         string trackBox,
         double splitQuantity,
         string description,
         int productSKUId,
         int fabricSKUId,
         string productSKUCode,
         int productPackingId,
         int fabricPackingId,
         string productPackingCode,
         string materialOrigin,
         string remark,
         string finishWidth
         )
        {
            Balance = balance;
            BalanceRemains = balanceRemains;
            BalanceOut = balanceOut;
            BuyerId = buyerId;
            Buyer = buyer;
            CartNo = cartNo;
            Color = color;
            Construction = construction;
            Grade = grade;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialWidth = materialWidth;
            Motif = motif;
            PackingInstruction = packingInstruction;
            PackagingQty = packagingQty;
            PackagingQtyRemains = packagingQtyRemains;
            PackagingQtyOut = packagingQtyOut;
            PackagingLength = packagingLength;
            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            CreatedUtcOrderNo = createdUtcOrderNo;
            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            Unit = unit;
            UomUnit = uomUnit;
            TrackId = trackId;
            TrackType = trackType;
            TrackName = trackName;
            TrackBox = trackBox;
            SplitQuantity = splitQuantity;
            Description = description;
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            MaterialOrigin = materialOrigin;
            Remark = remark;
            FinishWidth = finishWidth;
        }

        public void SetBalanceRemains(double newBalanceRemains, string user, string agent)
        {
            if (newBalanceRemains != BalanceRemains)
            {
                BalanceRemains = newBalanceRemains;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
