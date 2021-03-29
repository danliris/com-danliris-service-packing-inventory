using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaInputProductionOrderModel : StandardEntity
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }
        public string MaterialWidth { get; set; }
        public string FinishWidth { get; set; }

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
        public double Balance { get; set; }
        public double InputQuantity { get; private set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
        public string PackingInstruction { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }

        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double InitLength { get; set; }
        public double AvalALength { get; set; }
        public double AvalBLength { get; set; }
        public double AvalConnectionLength { get; set; }

        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string Machine { get; set; }

        public string ProductionMachine { get; set; }

        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }

        public long DeliveryOrderReturId { get; set; }

        public string DeliveryOrderReturNo { get; set; }

        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public decimal InputPackagingQty { get; private set; }
        public double PackagingLength { get; private set; }

        public string Area { get; set; }

        public double BalanceRemains { get; set; }
        public string DestinationBuyerName { get; set; }
        public string InventoryType { get; set; }

        #region aval transformasi
        public string InputAvalBonNo { get; private set; }
        public double AvalQuantityKg { get; private set; }
        public double AvalQuantity { get; private set; }
        #endregion


        #region Product SKU Packing

        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }

        public bool HasPrintingProductSKU { get; private set; }

        public bool HasPrintingProductPacking { get; private set; }


        #endregion

        public int DyeingPrintingAreaInputId { get; set; }
        public int DyeingPrintingAreaOutputProductionOrderId { get; set; }
        public DyeingPrintingAreaInputModel DyeingPrintingAreaInput { get; set; }
        public DateTimeOffset DateIn { get; private  set; }
        public DateTimeOffset DateOut { get; private set; }

        public DyeingPrintingAreaInputProductionOrderModel()
        {

        }

        /// <summary>
        /// Area IM
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="buyerId"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity,
            string packingInstruction, string cartNo, string buyer, string construction, string unit, string color, string motif, string uomUnit, double balance, double balanceRemains,
            bool hasOutputDocument, int buyerId, int dyeingPrintingAreaOutputProductionOrderId, int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, double inputQuantity, string finishWidth, DateTimeOffset dateIn)
           
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;

            BalanceRemains = balanceRemains;

            Area = area;

            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            InputQuantity = inputQuantity;

            DateIn = dateIn;
        }

        /// <summary>
        /// FQC
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, int buyerId)
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;

            Area = area;

            BuyerId = buyerId;
        }

        /// <summary>
        /// Constructor using by Packaging Area With Balance Remains and Output Prev SppID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        /// <param name="prevSppOutputID"></param>
        /// <param name="remark"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, double balanceRemains, int buyerId, int prevSppOutputID, string remark,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode,
            bool hasPrintingProductPacking, double inputQuantity, string finishWidth, DateTimeOffset dateIn)

        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = prevSppOutputID;
            Remark = remark;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            InputQuantity = inputQuantity;
            DateIn = dateIn;
        }

        /// <summary>
        /// Constructor Using By Packaging Area WIth BalanceRemain and Bon ID and OutputSPP ID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="dyeingPrintingAreaInputId"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="remark"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, int dyeingPrintingAreaInputId, double balanceRemains, int buyerId, int dyeingPrintingAreaOutputProductionOrderId,
            string remark, string productionMachine, int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode,
            bool hasPrintingProductPacking, double inputQuantity, string finishWidth, DateTimeOffset dateIn)

        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputId;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;
            Remark = remark;
            ProductionMachine = productionMachine;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            InputQuantity = inputQuantity;
            DateIn = dateIn;
        }

        /// <summary>
        ///  construtor for Gudang barang jadi With OutputSPP ID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="grade"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="buyerId"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="remark"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="inventoryType"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, string packagingUnit, string packagingType, decimal packagingQty, string grade,
            double productionOrderOrderQuantity, int buyerId, int dyeingPrintingAreaOutputProductionOrderId, string remark, double balanceRemains,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode,
            bool hasPrintingProductPacking, double packingLength, double inputQuantity, decimal inputPackagingQty, string finishWidth, DateTimeOffset dataIn, string inventoryType)

        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            PackagingUnit = packagingUnit;
            PackagingQty = packagingQty;
            PackagingType = packagingType;
            Grade = grade;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;

            Area = area;

            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;
            BalanceRemains = balanceRemains;
            Remark = remark;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;

            InputQuantity = inputQuantity;
            InputPackagingQty = inputPackagingQty;
            DateIn = dataIn;
            InventoryType = inventoryType;
        }

        /// <summary>
        /// Area Transit
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="qtyPacking"></param>
        /// <param name="packingUnit"></param>
        /// <param name="packingType"></param>
        /// <param name="deliveryOrderSalesId"></param>
        /// <param name="deliveryOrderSalesNo"></param>
        /// <param name="avalType"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="productSKUId"></param>
        /// <param name="fabricSKUId"></param>
        /// <param name="productSKUCode"></param>
        /// <param name="hasPrintingProductSKU"></param>
        /// <param name="productPackingId"></param>
        /// <param name="fabricPackingId"></param>
        /// <param name="productPackingCode"></param>
        /// <param name="hasPrintingProductPacking"></param>
        /// 
        
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity,
            string packingInstruction, string cartNo, string buyer, string construction, string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument,
            string remark, string productionMachine, string grade, string status, double balanceRemains, int buyerId, int dyeingPrintingAreaOutputProductionOrderId, int materialId, string materialName,
            int materialConstructionId, string materialConstructionName, string materialWidth, decimal qtyPacking, string packingUnit, string packingType, long deliveryOrderSalesId,
            string deliveryOrderSalesNo, string avalType, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking,
            double packingLength, double inputQuantity, decimal inputQtyPacking, string finishWidth, DateTimeOffset dateIn)

        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            Remark = remark;
            ProductionMachine = productionMachine;
            Grade = grade;
            Status = status;
           
            Area = area;

            BalanceRemains = balanceRemains;

            BuyerId = buyerId;

            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            PackagingQty = qtyPacking;
            PackagingUnit = packingUnit;
            PackagingType = packingType;

            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;

            AvalType = avalType;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;

            DateIn = dateIn;

            InputQuantity = inputQuantity;
            InputPackagingQty = inputQtyPacking;
        }


        /// <summary>
        /// Aval Insert Data Using SPP Entity
        /// </summary>
        /// <param name="area"></param>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="uomUnit"></param>
        /// <param name="quantity"></param>
        /// <param name="avalQuantityKg"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyerId"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="machine"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area,
                                                           string avalType,
                                                           string avalCartNo,
                                                           string uomUnit,
                                                           double quantity,
                                                           double avalQuantityKg,
                                                           bool hasOutputDocument,
                                                           long productionOrderId,
                                                           string productionOrderNo,
                                                           string cartNo,
                                                           int buyerId,
                                                           string buyer,
                                                           string construction,
                                                           string unit,
                                                           string color,
                                                           string motif,
                                                           string remark,
                                                           string grade,
                                                           string status,
                                                           double balance,
                                                           string packingInstruction,
                                                           string productionOrderType,
                                                           double productionOrderOrderQuantity,
                                                           string packagingType,
                                                           decimal packagingQty,
                                                           string packagingUnit,
                                                           int dyeingPrintingAreaOutputProductionOrderId,
                                                           string machine,
                                                           int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode,
            bool hasPrintingProductPacking, double packingLength, double inputQuantity, decimal inputQtyPacking, DateTimeOffset dateIn, string finishWidth)
            
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = uomUnit;
            Balance = quantity;
            AvalQuantityKg = avalQuantityKg;
            HasOutputDocument = hasOutputDocument;

            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            BuyerId = buyerId;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            BalanceRemains = inputQuantity;
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = PackingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            PackingInstruction = packingInstruction;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;

            Area = area;

            Machine = machine;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;
            InputQuantity = inputQuantity;
            InputPackagingQty = inputQtyPacking;
            DateIn = dateIn;
        }

        /// <summary>
        /// Aval Insert Data Using SPP Entity and ID Bon existing
        /// </summary>
        /// <param name="area"></param>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="uomUnit"></param>
        /// <param name="quantity"></param>
        /// <param name="avalQuantityKg"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyerId"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="dyeingPrintingAreaInputsId"></param>
        /// <param name="machine"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area,
            string avalType,
            string avalCartNo,
            string uomUnit,
            double quantity,
            double avalQuantityKg,
            bool hasOutputDocument,
            int productionOrderId,
            string productionOrderNo,
            string cartNo,
            int buyerId,
            string buyer,
            string construction,
            string unit,
            string color,
            string motif,
            string remark,
            string grade,
            string status,
            double balance,
            double balanceRemains,
            string packingInstruction,
            string productionOrderType,
            double productionOrderOrderQuantity,
            string packagingType,
            decimal packagingQty,
            string packagingUnit,
            int dyeingPrintingAreaOutputProductionOrderId,
            int dyeingPrintingAreaInputsId,
            string machine,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode,
            bool hasPrintingProductPacking, double packingLength, double inputQuantity, decimal inputQtyPacking, string finishWidth, DateTimeOffset dateIn)

        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = uomUnit;
            Balance = quantity;
            AvalQuantityKg = avalQuantityKg;
            HasOutputDocument = hasOutputDocument;
            BalanceRemains = balanceRemains;
            PackingInstruction = packingInstruction;

            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            BuyerId = buyerId;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = PackingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputsId;

            Area = area;

            Machine = machine;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;

            InputQuantity = inputQuantity;
            InputPackagingQty = inputQtyPacking;
            DateIn = dateIn;
        }

        /// <summary>
        /// Area Shipping
        /// </summary>
        /// <param name="area"></param>
        /// <param name="deliveryOrderSalesId"></param>
        /// <param name="deliveryOrderSalesNo"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="packingType"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="grade"></param>
        /// <param name="qtyPacking"></param>
        /// <param name="packingUnit"></param>
        /// <param name="qty"></param>
        /// <param name="uomUnit"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="unit"></param>
        /// <param name="buyerId"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="cartNo"></param>
        /// <param name="remark"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="destinationBuyerName"></param>
        /// <param name="inventoryType"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long deliveryOrderSalesId, string deliveryOrderSalesNo, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string buyer, string construction,
            string packingType, string color, string motif, string grade, decimal qtyPacking, string packingUnit, double qty, string uomUnit, bool hasOutputDocument, double balanceRemains,
            string unit, int buyerId, int dyeingPrintingAreaOutputProductionOrderId, int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth,
            string cartNo, string remark, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, int productSKUId, int fabricSKUId, string productSKUCode,
            bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, double inputQuantity, decimal inputPackagingQty,
            long deliveryOrderReturId, string deliveryOrderReturNo, string finishWidth, DateTimeOffset dateIn, string destinationBuyerName, string inventoryType)

        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            Buyer = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            HasOutputDocument = hasOutputDocument;
            ProductionOrderType = productionOrderType;
            Unit = unit;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            Grade = grade;

            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packingType;
            PackagingQty = qtyPacking;
            PackagingUnit = packingUnit;
            Balance = qty;
            Area = area;

            BalanceRemains = balanceRemains;

            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            CartNo = cartNo;
            Remark = remark;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;

            InputQuantity = inputQuantity;
            InputPackagingQty = inputPackagingQty;

            DeliveryOrderReturId = deliveryOrderReturId;
            DeliveryOrderReturNo = deliveryOrderReturNo;
            DateIn = dateIn;
            DestinationBuyerName = destinationBuyerName;
            InventoryType = inventoryType;
        }

        /// <summary>
        /// Reject Gudang Jadi
        /// </summary>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="avalALength"></param>
        /// <param name="avalBLength"></param>
        /// <param name="avalConnectionLength"></param>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="avalQuantityKg"></param>
        /// <param name="deliveryOrderSalesId"></param>
        /// <param name="deliveryOrderSalesNo"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="area"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(long productionOrderId,
                                                           string productionOrderNo,
                                                           string cartNo,
                                                           string buyer,
                                                           string construction,
                                                           string unit,
                                                           string color,
                                                           string motif,
                                                           string uomUnit,
                                                           double balance,
                                                           bool hasOutputDocument,
                                                           //bool isChecked, 
                                                           string packingInstruction,
                                                           string productionOrderType,
                                                           double productionOrderOrderQuantity,
                                                           string remark,
                                                           string grade,
                                                           string status,
                                                           //double initLength, 
                                                           double avalALength,
                                                           double avalBLength,
                                                           double avalConnectionLength,
                                                           string avalType,
                                                           string avalCartNo,
                                                           double avalQuantityKg,
                                                           long deliveryOrderSalesId,
                                                           string deliveryOrderSalesNo,
                                                           string packagingUnit,
                                                           string packagingType,
                                                           decimal packagingQty,
                                                           string area,
                                                           double balanceRemains,
                                                           int dyeingPrintingAreaOutputProductionOrderId,
                                                           int buyerId,
                                                           int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, int productSKUId, int fabricSKUId, string productSKUCode,
            bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, double inputQuantity, decimal inputPackagingQty, string finishWidth)
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            //IsChecked = isChecked;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Remark = remark;
            Grade = grade;
            Status = status;
            //InitLength = initLength;
            AvalALength = avalALength;
            AvalBLength = avalBLength;
            AvalConnectionLength = avalConnectionLength;
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            AvalQuantityKg = avalQuantityKg;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            PackagingUnit = packagingUnit;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            Area = area;
            BalanceRemains = balanceRemains;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            PackagingLength = packingLength;

            InputQuantity = inputQuantity;
            InputPackagingQty = inputPackagingQty;
        }

        /// <summary>
        /// Area Aval Transformasi
        /// </summary>
        /// <param name="area"></param>
        /// <param name="inputAvalBonNo"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="cartNo"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="buyer"></param>
        /// <param name="buyerId"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="avalType"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="machine"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, string inputAvalBonNo, long productionOrderId, string productionOrderNo, string productionOrderType,
            double productionOrderQuantity, string cartNo, string construction, string unit, string buyer, int buyerId, string color, string motif, string avalType, string uomUnit,
            double balance, bool hasOutputDocument, int dyeingPrintingAreaInputProductionOrderId, int materialId, string materialName,
            int materialConstructionId, string materialConstructionName, string materialWidth, string machine, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double inputQuantity, string finishWidth) : this()
        {
            Area = area;
            InputAvalBonNo = inputAvalBonNo;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            CartNo = cartNo;
            Construction = construction;
            Unit = unit;
            Buyer = buyer;
            BuyerId = buyerId;
            Color = color;
            Motif = motif;
            AvalType = avalType;
            UomUnit = uomUnit;
            Balance = balance;

            Machine = machine;

            HasOutputDocument = hasOutputDocument;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            HasPrintingProductPacking = hasPrintingProductPacking;

            InputQuantity = inputQuantity;
        }


        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderNo, string newProductionOrderType, double newProductionOrderOrderQuantity, string user, string agent)
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

            if (newProductionOrderOrderQuantity != ProductionOrderOrderQuantity)
            {
                ProductionOrderOrderQuantity = newProductionOrderOrderQuantity;
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

        public void SetHasOutputDocument(bool newFlagHasOutputDocument, string user, string agent)
        {
            if (newFlagHasOutputDocument != HasOutputDocument)
            {
                HasOutputDocument = newFlagHasOutputDocument;
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

        public void SetIsChecked(bool newFlagChecked, string user, string agent)
        {
            if (newFlagChecked != IsChecked)
            {
                IsChecked = newFlagChecked;
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

        public void SetDeliveryOrderSales(long deliveryOrderSalesId, string deliveryOrderSalesNo, string user, string agent)
        {
            if (deliveryOrderSalesId != DeliveryOrderSalesId)
            {
                DeliveryOrderSalesId = deliveryOrderSalesId;
                this.FlagForUpdate(user, agent);
            }

            if (deliveryOrderSalesNo != DeliveryOrderSalesNo)
            {
                DeliveryOrderSalesNo = deliveryOrderSalesNo;
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

        public void SetPackagingUnit(string newPackagingUnit, string user, string agent)
        {
            if (newPackagingUnit != PackagingUnit)
            {
                PackagingUnit = newPackagingUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalALength(double newAvalABalance, string user, string agent)
        {
            if (newAvalABalance != AvalALength)
            {
                AvalALength = newAvalABalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalBLength(double newAvalBBalance, string user, string agent)
        {
            if (newAvalBBalance != AvalBLength)
            {
                AvalBLength = newAvalBBalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalConnectionLength(double newAvalConnectionLength, string user, string agent)
        {
            if (newAvalConnectionLength != AvalConnectionLength)
            {
                AvalConnectionLength = newAvalConnectionLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetInitLength(double newInitLength, string user, string agent)
        {
            if (newInitLength != InitLength)
            {
                InitLength = newInitLength;
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
        public void SetArea(string newArea, string user, string agent)
        {
            if (newArea != Area)
            {
                Area = newArea;
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

        public void SetMachine(string newMachine, string user, string agent)
        {
            if (newMachine != Machine)
            {
                Machine = newMachine;

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

        public void SetInputQuantity(double newInputQuantity, string user, string agent)
        {
            if (newInputQuantity != InputQuantity)
            {
                InputQuantity = newInputQuantity;

                this.FlagForUpdate(user, agent);
            }
        }

        public void SetInputPackagingQty(decimal newInputPackagingQty, string user, string agent)
        {
            if (newInputPackagingQty != InputPackagingQty)
            {
                InputPackagingQty = newInputPackagingQty;

                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDateOut(DateTimeOffset NewDateOut, string user, string agent)
        {
            if (NewDateOut != DateOut)
            {
                DateOut = NewDateOut;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
