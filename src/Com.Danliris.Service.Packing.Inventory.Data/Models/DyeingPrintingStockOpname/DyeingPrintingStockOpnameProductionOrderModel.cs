using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname
{
    public class DyeingPrintingStockOpnameProductionOrderModel : StandardEntity
    {
        public double Balance { get; private set; }
        public int BuyerId { get; private set; }
        public string Buyer { get; private set; }
        public string CartNo { get; private set; }
        public string Color { get; private set; }

        public string Construction { get; private set; }
        public string DocumentNo { get; set; }
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
        public double PackagingLength { get; private set; }
        public string PackagingType { get; private set; }
        public string PackagingUnit { get; private set; }

        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }
        public string Remark { get; private set; }
        public string Status { get; private set; }
        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }

        public int YarnMaterialId { get; private set; }
        public string YarnMaterialName { get; private set; }
        public string Unit { get; private set; }
        public string UomUnit { get; private set; }

        #region Product SKU Packing

        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }

        public bool HasPrintingProductSKU { get; private set; }

        public bool HasPrintingProductPacking { get; private set; }
        public bool IsStockOpname { get; private set; }

        #endregion

        public DyeingPrintingStockOpnameModel DyeingPrintingStockOpname { get; set; }

        public void SetPackingCode(string packingCode)
        {
            ProductPackingCode = packingCode;
        }

        public DyeingPrintingStockOpnameProductionOrderModel(double balance, int buyerId, string buyer, string color, string construction, string documentNo, string grade, int materialConstructionId, string materialConstructionName, int materialId,
            string materialName, string materialWidth, string motif, string packingInstruction, decimal packagingQty, double packagingLength, string packagingType, string packagingUnit,
            long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderOrderQuantity, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
             string remark, string status, string unit, string uomUnit, bool isStockOpname, string packingCodes)
        {
            Balance = balance;
            BuyerId = buyerId;
            Buyer = buyer;
            Color = color;
            Construction = construction;
            DocumentNo = documentNo;
            Grade = grade;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialWidth = materialWidth;
            Motif = motif;
            PackingInstruction = packingInstruction;
            PackagingQty = packagingQty;
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
            IsStockOpname = isStockOpname;
            ProductPackingCode = packingCodes;
        }

        public DyeingPrintingStockOpnameProductionOrderModel()
        {

        }

        public void SetPackingCode(int productSKUId, int fabricSKUId, string productSKUCode, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductSKU, string user, string agent)
        {
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductSKU = hasPrintingProductSKU;
            this.FlagForUpdate(user, agent);
        }

        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderNo, string newProductionOrderType, double newProductionOrderQuantity, string user, string agent)
        {
            if (newProductionOrderId != ProductionOrderId)
            {
                ProductionOrderId = newProductionOrderId;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderNo != ProductionOrderNo)
            {
                ProductionOrderNo = newProductionOrderNo;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderType != ProductionOrderType)
            {
                ProductionOrderType = newProductionOrderType;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderQuantity != ProductionOrderOrderQuantity)
            {
                ProductionOrderOrderQuantity = newProductionOrderQuantity;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackingInstruction(string newPackingInstruction, string user, string agent)
        {
            if (newPackingInstruction != PackingInstruction)
            {
                PackingInstruction = newPackingInstruction;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingUnit(string newPackagingUnit, string user, string agent)
        {
            if (newPackagingUnit != PackingInstruction)
            {
                PackagingUnit = newPackagingUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetBuyer(int newBuyerId, string newBuyer, string user, string agent)
        {
            if (newBuyerId != BuyerId)
            {
                BuyerId = newBuyerId;
                this.FlagForUpdate(user, agent);
            }

            if (newBuyer != Buyer)
            {
                Buyer = newBuyer;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetCartNo(string newCartNo, string user, string agent)
        {
            if (newCartNo != CartNo)
            {
                CartNo = newCartNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetConstruction(string newConstruction, string user, string agent)
        {
            if (newConstruction != Construction)
            {
                Construction = newConstruction;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetUnit(string newUnit, string user, string agent)
        {

            if (newUnit != Unit)
            {
                Unit = newUnit;
                this.FlagForUpdate(user, agent);
            }

        }

        public void SetColor(string newColor, string user, string agent)
        {
            if (newColor != Color)
            {
                Color = newColor;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMotif(string newMotif, string user, string agent)
        {
            if (newMotif != Motif)
            {
                Motif = newMotif;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetUomUnit(string newUomUnit, string user, string agent)
        {
            if (newUomUnit != UomUnit)
            {
                UomUnit = newUomUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetBalance(double newBalance, string user, string agent)
        {
            if (newBalance != Balance)
            {
                Balance = newBalance;
                this.FlagForUpdate(user, agent);
            }
        }



        public void SetRemark(string newRemark, string user, string agent)
        {
            if (newRemark != Remark)
            {
                Remark = newRemark;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetGrade(string newGrade, string user, string agent)
        {
            if (newGrade != Grade)
            {
                Grade = newGrade;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetStatus(string newStatus, string user, string agent)
        {
            if (newStatus != Status)
            {
                Status = newStatus;
                this.FlagForUpdate(user, agent);
            }
        }



        public void SetPackagingType(string newPackagingType, string user, string agent)
        {
            if (newPackagingType != PackagingType)
            {
                PackagingType = newPackagingType;
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






        public void SetMaterial(int newId, string newName, string user, string agent)
        {
            if (newId != MaterialId)
            {
                MaterialId = newId;
                this.FlagForUpdate(user, agent);
            }

            if (newName != MaterialName)
            {
                MaterialName = newName;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMaterialConstruction(int newId, string newName, string user, string agent)
        {
            if (newId != MaterialConstructionId)
            {
                MaterialConstructionId = newId;
                this.FlagForUpdate(user, agent);
            }

            if (newName != MaterialConstructionName)
            {
                MaterialConstructionName = newName;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMaterialWidth(string newMaterialWidth, string user, string agent)
        {

            if (newMaterialWidth != MaterialWidth)
            {
                MaterialWidth = newMaterialWidth;
                this.FlagForUpdate(user, agent);
            }
        }




        public void SetDocumentNo(string documentNo, string user, string agent)
        {

            if (documentNo != DocumentNo)
            {
                DocumentNo = documentNo;
                this.FlagForUpdate(user, agent);
            }
        }







        public void SetPackagingLength(double newPackagingLength, string user, string agent)
        {
            if (newPackagingLength != PackagingLength)
            {
                PackagingLength = newPackagingLength;

                this.FlagForUpdate(user, agent);
            }
        }


    }
}