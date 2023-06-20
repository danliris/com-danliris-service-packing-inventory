using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehousePreInputModel : StandardEntity
    {
        public double Balance { get; set; }
        public double BalanceRemains { get; set; }
        public double BalanceReceipt { get; set; }
        public double BalanceReject { get; set; }

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
        public decimal PackagingQty { get; private set; }
        public decimal PackagingQtyRemains { get; set; }
        public decimal PackagingQtyReceipt { get; set; }
        public decimal PackagingQtyReject { get;  set; }
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
        public string Remark { get; set; }
        public string Description { get; set; }
        public string MaterialOrigin { get; set; }
        public string FinishWidth { get; set; }
        


        #region Product SKU Packing

        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        #endregion

        public DPWarehousePreInputModel(double balance, double balanceRemains, double balanceReceipt, double balanceReject, int buyerId, string buyer, string color, string construction, string grade,
            int materialConstructionId, string materialConstructionName, int materialId, string materialName, string materialWidth, string motif, string packingInstruction, decimal packagingQty,
            decimal packagingQtyRemains, decimal packagingQtyReceipt, decimal packagingQtyReject, double packagingLength, string packagingType, string packagingUnit, long productionOrderId, string productionOrderNo,
            string productionOrderType, double productionOrderOrderQuantity, DateTime createdUtcOrderNo, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, string unit, string uomUnit,
            /*int trackId, string trackType, string trackName, string trackBox,*/ string remark, string description, int productSKUId, int fabricSKUId, string productSKUCode, int productPackingId, int fabricPackingId,
            string productPackingCode, string materialOrigin, string finishWidth)  
        {
            Balance = balance;
            BalanceRemains = balanceRemains;
            BalanceReceipt = balanceReceipt;
            BalanceReject = balanceReject;
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
            PackagingQtyReceipt = packagingQtyReceipt;
            PackagingQtyReject = packagingQtyReject;
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
            Remark = remark;
            Description = description;
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            MaterialOrigin = materialOrigin;
            FinishWidth = finishWidth;

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
    }

    
}
