using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname
{
    public class DyeingPrintingStockOpnameSummaryModel : StandardEntity
    {
        public double Balance { get;  set; }
        public double BalanceRemains { get;  set; }
        public double BalanceEnd { get;  set; }

        public int BuyerId { get;  set; }
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
        public decimal PackagingQtyEnd { get; private set; }
        public double PackagingLength { get; private set; }
        public string PackagingType { get; private set; }
        public string PackagingUnit { get; private set; }

        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }
        public DateTime CreatedUtcOrderNo { get; private set; }
        public string Remark { get; private set; }
        public string Status { get; private set; }
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


        public DyeingPrintingStockOpnameSummaryModel(double balance, double balanceRemains, int buyerId, string buyer, string color, string construction, string grade, int materialConstructionId, string materialConstructionName, int materialId,
            string materialName, string materialWidth, string motif, string packingInstruction, decimal packagingQty, decimal packagingQtyRemains, double packagingLength, string packagingType, string packagingUnit,
            long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderOrderQuantity, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
             string remark, string status, string unit, string uomUnit, int fabricSKUId, int productSKUId, string productSKUCode, int productPackingId, string productPackingCode, int trackId, string trackType, string trackName, string trackBox, DateTime createdUtcOrderNo)
        {
            Balance = balance;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            Buyer = buyer;
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
            PackagingLength = packagingLength;

            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            ProcessTypeId = processTypeId;
            Remark = remark;
            Status = status;
            Unit = unit;
            UomUnit = uomUnit;
            FabricSKUId = fabricSKUId;
            ProductSKUId = productSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            ProductPackingCode = productPackingCode;
            TrackId = trackId;
            TrackType = trackType;
            TrackName = trackName;
            TrackBox = trackBox;
            CreatedUtcOrderNo = createdUtcOrderNo;
        }


        // for update track
        public DyeingPrintingStockOpnameSummaryModel(double balance, double balanceRemains, int buyerId, string buyer, string color, string construction, string grade, int materialConstructionId, string materialConstructionName, int materialId,
            string materialName, string materialWidth, string motif, string packingInstruction, decimal packagingQty, decimal packagingQtyRemains, double packagingLength, string packagingType, string packagingUnit,
            long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderOrderQuantity, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
             string remark, string status, string unit, string uomUnit, int fabricSKUId, int productSKUId, string productSKUCode, int productPackingId, string productPackingCode, int trackId, string trackType, string trackName, string trackBox, DateTime createdUtcOrderNo, double splitQuantity)
        {
            Balance = balance;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            Buyer = buyer;
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
            PackagingLength = packagingLength;

            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            ProcessTypeId = processTypeId;
            Remark = remark;
            Status = status;
            Unit = unit;
            UomUnit = uomUnit;
            FabricSKUId = fabricSKUId;
            ProductSKUId = productSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            ProductPackingCode = productPackingCode;
            TrackId = trackId;
            TrackType = trackType;
            TrackName = trackName;
            TrackBox = trackBox;
            CreatedUtcOrderNo = createdUtcOrderNo;
            SplitQuantity = splitQuantity;
        }


        public void SetBalance(double newBalance, string user, string agent)
        {
            if (newBalance != Balance)
            {
                Balance = newBalance;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetBalanceRemains(double newBalanceRemains, string user, string agent)
        {
            if (newBalanceRemains != BalanceRemains)
            {
                BalanceRemains = newBalanceRemains;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetBalanceEnd(double newBalanceEnd, string user, string agent)
        {
            if (newBalanceEnd != BalanceEnd)
            {
                BalanceEnd = newBalanceEnd;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQty(decimal newPackagingQty, string user, string agent)
        {
            if (newPackagingQty != PackagingQty)
            {
                PackagingQty = newPackagingQty;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQtyRemains(decimal newPackagingQtyRemains, string user, string agent)
        {
            if (newPackagingQtyRemains != PackagingQtyRemains)
            {
                PackagingQtyRemains = newPackagingQtyRemains;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQtyEnd(decimal newPackagingQtyEnd, string user, string agent)
        {
            if (newPackagingQtyEnd != PackagingQtyEnd)
            {
                PackagingQtyEnd = newPackagingQtyEnd;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetSplitQuantity(double newSplitQuantity, string user, string agent)
        {
            if (newSplitQuantity != SplitQuantity)
            {
                SplitQuantity = newSplitQuantity;
                this.FlagForUpdate(user, agent);
            }
        }

    }
}
