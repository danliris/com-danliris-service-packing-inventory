using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaOutputProductionOrderModel : StandardEntity
    {
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public int MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionName { get; private set; }
        public string MaterialWidth { get; private set; }
        public string FinishWidth { get; private set; }

        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }

        public int YarnMaterialId { get; private set; }
        public string YarnMaterialName { get; private set; }

        public string CartNo { get; private set; }
        public int BuyerId { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public string Remark { get; private set; }
        public string Grade { get; private set; }
        public string Status { get; private set; }
        public double Balance { get; private set; }
        public string PackingInstruction { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }

        public string PackagingType { get; private set; }
        public decimal PackagingQty { get; private set; }
        public string PackagingUnit { get; private set; }
        public double PackagingLength { get; private set; }

        public double AvalALength { get; private set; }
        public double AvalBLength { get; private set; }
        public double AvalConnectionLength { get; private set; }

        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }

        public string AvalType { get; private set; }
        public string AvalCartNo { get; private set; }
        public double AvalQuantityKg { get; private set; }
        public string Machine { get; private set; }

        public string ProductionMachine { get; set; }

        public bool HasNextAreaDocument { get; private set; }
        public string Area { get; private set; }
        public string DestinationArea { get; private set; }
        public string Description { get; set; }

        public string NextAreaInputStatus { get; private set; }


        public string DeliveryNote { get; private set; }

        public bool HasSalesInvoice { get; private set; }
        public string ShippingGrade { get; private set; }
        public string ShippingRemark { get; private set; }
        public double Weight { get; private set; }

        public string PrevSppInJson { get; set; }

        public string AdjDocumentNo { get; set; }
        public string DestinationBuyerName { get; set; }
        public string InventoryType { get; set; }
        public string MaterialOrigin { get; set;  }
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


        /// <summary>
        /// ID SPP Input
        /// </summary>
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }


        //public ICollection<DyeingPrintingAreaOutputAvalItemModel> DyeingPrintingAreaOutputAvalItems { get; private set; }


        public int DyeingPrintingAreaOutputId { get; set; }
        public DyeingPrintingAreaOutputModel DyeingPrintingAreaOutput { get; set; }
        public DateTimeOffset DateIn { get; private set; }
        public DateTimeOffset DateOut { get; private set; }

        public DyeingPrintingAreaOutputProductionOrderModel()
        {
            //DyeingPrintingAreaOutputAvalItems = new HashSet<DyeingPrintingAreaOutputAvalItemModel>();
        }

        /// <summary>
        /// Area IM
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
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
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="avalType"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="machine"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="materialOrigin"></param>

        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
                    string unit, string color, string motif, string uomUnit, string remark, string grade, string status, double balance, int dyeingPrintingAreaInputProductionOrderId, int buyerId, string avalType,
                    int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string machine, string productionMachine, string adjDocumentNo,
                    int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName, int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, string finishWidth,DateTimeOffset dateIn, DateTimeOffset dateOut, string materialOrigin) : this()
                   
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
            Remark = remark;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;

            AvalType = avalType;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;

            Machine = machine;
            ProductionMachine = productionMachine;

            AdjDocumentNo = adjDocumentNo;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            HasPrintingProductSKU = hasPrintingProductSKU;

            FinishWidth = finishWidth;
            DateIn = dateIn;
            DateOut = dateOut;
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Area Trasnit
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
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
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="dyeingPrintingAreaInputProductonOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="qtyPacking"></param>
        /// <param name="packingType"></param>
        /// <param name="packingUnit"></param>
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
        /// <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, string remark, string productionMachine, string grade, string status, double balance, int dyeingPrintingAreaInputProductonOrderId, int buyerId,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string adjDocumentNo, decimal qtyPacking, string packingType,
            string packingUnit, long deliveryOrderSalesId, string deliveryOrderSalesNo, string avalType, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, DateTimeOffset dateIn, DateTimeOffset dateOut, string materialOrigin) : this()
            
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
            Remark = remark;
            ProductionMachine = productionMachine;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;
            AdjDocumentNo = adjDocumentNo;

            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

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
            DateOut = dateOut;
            MaterialOrigin = materialOrigin;
        }



        /// <summary>
        /// Area Trasnit
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
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
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="dyeingPrintingAreaInputProductonOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="qtyPacking"></param>
        /// <param name="packingType"></param>
        /// <param name="packingUnit"></param>
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
        ///  <param name="nextAreaInputStatus"></param>
        ///  <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, string remark, string productionMachine, string grade, string status, double balance, int dyeingPrintingAreaInputProductonOrderId, int buyerId,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string adjDocumentNo, decimal qtyPacking, string packingType,
            string packingUnit, long deliveryOrderSalesId, string deliveryOrderSalesNo, string avalType, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, DateTimeOffset dateIn, DateTimeOffset dateOut, string nextAreaInputStatus, string materialOrigin) : this()

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
            Remark = remark;
            ProductionMachine = productionMachine;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;
            AdjDocumentNo = adjDocumentNo;

            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

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
            DateOut = dateOut;
            NextAreaInputStatus = nextAreaInputStatus;
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Area Packing
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
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
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="dyeingPrintingAreaInputProductonOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, string remark, string grade, string status, double balance, int dyeingPrintingAreaInputProductonOrderId, int buyerId,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string adjDocumentNo, string packagingType, decimal packagingQty,
            string packagingUnit, int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth,DateTimeOffset dateIn, DateTimeOffset dateOut, string materialOrigin) : this()
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
            Remark = remark;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;
            AdjDocumentNo = adjDocumentNo;
            PackagingQty = packagingQty;
            PackagingType = packagingType;
            PackagingUnit = packagingUnit;

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
            DateOut = dateOut;
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Area Shipping
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="deliveryOrderSalesId"></param>
        /// <param name="deliveryOrderSalesNo"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="grade"></param>
        /// <param name="uomUnit"></param>
        /// <param name="deliveryNote"></param>
        /// <param name="balance"></param>
        /// <param name="dyeingPrintingAreaInputProductonOrderId"></param>
        /// <param name="packingUnit"></param>
        /// <param name="packingType"></param>
        /// <param name="qtyPacking"></param>
        /// <param name="buyerId"></param>
        /// <param name="hasSalesInvoice"></param>
        /// <param name="shippingGrade"></param>
        /// <param name="shippingRemark"></param>
        /// <param name="weight"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="cartNo"></param>
        /// <param name="remark"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="detinationBuyerName"></param>
        /// <param name="InventoryType"></param>
        /// <param name="MaterialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long deliveryOrderSalesId, string deliveryOrderSalesNo, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string buyer, string construction,
            string unit, string color, string motif, string grade, string uomUnit, string deliveryNote, double balance, int dyeingPrintingAreaInputProductonOrderId, string packingUnit, string packingType, decimal qtyPacking, int buyerId, bool hasSalesInvoice, string shippingGrade, string shippingRemark, double weight,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string cartNo, string remark, string adjDocumentNo,
            int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, DateTimeOffset dateIn, DateTimeOffset dateOut, string destinationBuyerName, string inventoryType, string materialOrigin) : this()
           
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            Buyer = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            Balance = balance;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            DeliveryNote = deliveryNote;
            Unit = unit;
            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            HasSalesInvoice = hasSalesInvoice;

            ShippingGrade = shippingGrade;
            ShippingRemark = shippingRemark;
            Weight = weight;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            CartNo = cartNo;
            Remark = remark;
            AdjDocumentNo = adjDocumentNo;

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
            DateOut = dateOut;
            DestinationBuyerName = destinationBuyerName;
            InventoryType = inventoryType;
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Using For Packaging Area when you want to set with SPP input
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="description"></param>
        /// <param name="dyeingPrintintOutputId"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
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
        /// <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, double productionOrderOrderQuantity, string description, int dyeingPrintintOutputId,
            int dyeingPrintingAreaInputProductionOrderId, int buyerId, int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, int processTypeId, string processTypeName,
             int yarnMaterialId, string yarnMaterialName,
             int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, string materialOrigin) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Description = description;
            DyeingPrintingAreaOutputId = dyeingPrintintOutputId;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

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
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Using For Packaging Area when you want to set with SPP input and Prev Spp In that decrease
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="description"></param>
        /// <param name="dyeingPrintintOutputId"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="prevSppInJson"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string productionMachine, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, double productionOrderOrderQuantity, string description, int dyeingPrintintOutputId, int dyeingPrintingAreaInputProductionOrderId, int buyerId, string prevSppInJson,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, int processTypeId, string processTypeName,
            int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, DateTimeOffset dateIn, DateTimeOffset dateOut, string materialOrigin) : this()
             
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
            Remark = remark;
            ProductionMachine = productionMachine;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Description = description;
            DyeingPrintingAreaOutputId = dyeingPrintintOutputId;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;
            PrevSppInJson = prevSppInJson;

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
            DateIn = dateIn;
            DateOut = dateOut;
            MaterialOrigin = materialOrigin;
        }

        /// <summary>
        /// Aval Adj
        /// </summary>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="avalUomUnit"></param>
        /// <param name="avalQuantity"></param>
        /// <param name="avalQuantityKg"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="area"></param>
        /// <param name="hasNextAreaDocument"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, bool hasNextAreaDocument, string avalType, double avalQuantity, double avalQuantityKg, string adjDocumentNo, int avalTransformationId,DateTimeOffset dateOut) : this()
        {
            Area = area;
            HasNextAreaDocument = hasNextAreaDocument;
            AdjDocumentNo = adjDocumentNo;
            AvalType = avalType;
            Balance = avalQuantity;
            AvalQuantityKg = avalQuantityKg;
            DyeingPrintingAreaInputProductionOrderId = avalTransformationId;
           
            DateOut = dateOut;
        }

        /// <summary>
        /// insert aval using prev total weight
        /// </summary>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="avalUomUnit"></param>
        /// <param name="avalQuantityOut"></param>
        /// <param name="avalQuantityKgOut"></param>
        /// <param name="avalQuantity"></param>
        /// <param name="avalQuantityTotal"></param>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="deliveryNote"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantityOut,
                                                            double avalQuantityKgOut,
                                                            double avalQuantity,
                                                            double avalQuantityTotal,
                                                            string area,
                                                            string destinationArea,
                                                            string deliveryNote,
                                                            string prevAval,
                                                            int dyeingPrintingAreaInputProductionOrderId,
                                                            DateTimeOffset dateOut) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantityOut;
            AvalQuantityKg = avalQuantityKgOut;
            AvalALength = avalQuantity;
            AvalBLength = avalQuantityTotal;
            Area = area;
            DestinationArea = destinationArea;
            DeliveryNote = deliveryNote;
            PrevSppInJson = prevAval;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;
            DateOut = dateOut;
        }

        /// <summary>
        ///  Insert Aval with existing bon using prev total weight
        /// </summary>
        /// <param name="avalType"></param>
        /// <param name="avalCartNo"></param>
        /// <param name="avalUomUnit"></param>
        /// <param name="avalQuantityOut"></param>
        /// <param name="avalQuantityKgOut"></param>
        /// <param name="avalQuantity"></param>
        /// <param name="avalQuantityTotal"></param>
        /// <param name="dyeingPrintingOutputId"></param>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="deliveryNote"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantityOut,
                                                            double avalQuantityKgOut,
                                                            double avalQuantity,
                                                            double avalQuantityTotal,
                                                            int dyeingPrintingOutputId,
                                                            string area,
                                                            string destinationArea,
                                                            string deliveryNote) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantityOut;
            AvalQuantityKg = avalQuantityKgOut;
            AvalALength = avalQuantity;
            AvalBLength = avalQuantityTotal;
            DyeingPrintingAreaOutputId = dyeingPrintingOutputId;
            Area = area;
            DestinationArea = destinationArea;
            DeliveryNote = deliveryNote;
        }

        /// <summary>
        /// Area Gudang Jadi
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
        /// <param name="deliveryOrderSalesId"></param>
        /// <param name="deliveryOrderSalesNo"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
        /// <param name="buyerId"></param>
        /// <param name="materialId"></param>
        /// <param name="materialName"></param>
        /// <param name="materialConstructionId"></param>
        /// <param name="materialConstructionName"></param>
        /// <param name="materialWidth"></param>
        /// <param name="adjDocumentNo"></param>
        /// <param name="processTypeId"></param>
        /// <param name="processTypeName"></param>
        /// <param name="yarnMaterialId"></param>
        /// <param name="yarnMaterialName"></param>
        /// <param name="destinationBuyerName"></param>
        /// <param name="inventoryType"></param>
        /// <param name="materialOrigin"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit, string color,
            string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, long deliveryOrderSalesId, string deliveryOrderSalesNo, bool hasNextAreaDocument, string area,
            string destinationArea, int dyeingPrintingAreaInputProductionOrderId, int buyerId, int materialId, string materialName, int materialConstructionId, string materialConstructionName,
            string materialWidth, string adjDocumentNo, int processTypeId, string processTypeName,
            int yarnMaterialId, string yarnMaterialName, int productSKUId, int fabricSKUId, string productSKUCode, bool hasPrintingProductSKU, int productPackingId, int fabricPackingId, string productPackingCode, bool hasPrintingProductPacking, double packingLength, string finishWidth, DateTimeOffset dateIn, DateTimeOffset dateOut, string destinationBuyerName, string inventoryType, string materialOrigin) : this()
             
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            HasNextAreaDocument = hasNextAreaDocument;
            Area = area;
            DestinationArea = destinationArea;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

            AdjDocumentNo = adjDocumentNo;

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
            DateOut = dateOut;
            DestinationBuyerName = destinationBuyerName;
            InventoryType = inventoryType;
            MaterialOrigin = materialOrigin;
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

        public void SetAvalQuantityKg(double newAvalQuantityKg, string user, string agent)
        {
            if (newAvalQuantityKg != AvalQuantityKg)
            {
                AvalQuantityKg = newAvalQuantityKg;
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

        //public void SetAvalALength(double newAvalABalance, string user, string agent)
        //{
        //    if (newAvalABalance != AvalALength)
        //    {
        //        AvalALength = newAvalABalance;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetAvalBLength(double newAvalBBalance, string user, string agent)
        //{
        //    if (newAvalBBalance != AvalBLength)
        //    {
        //        AvalBLength = newAvalBBalance;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetAvalConnectionLength(double newAvalConnectionLength, string user, string agent)
        //{
        //    if (newAvalConnectionLength != AvalConnectionLength)
        //    {
        //        AvalConnectionLength = newAvalConnectionLength;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        public void SetArea(string newArea, string user, string agent)
        {
            if (newArea != Area)
            {
                Area = newArea;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasNextAreaDocument(bool newFlagNextAreaDocument, string user, string agent)
        {
            if (newFlagNextAreaDocument != HasNextAreaDocument)
            {
                HasNextAreaDocument = newFlagNextAreaDocument;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDestinationArea(string newDestinationArea, string user, string agent)
        {
            if (newDestinationArea != DestinationArea)
            {
                DestinationArea = newDestinationArea;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetDescription(string newDescription, string user, string agent)
        {
            if (newDescription != Description)
            {
                Description = newDescription;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasSalesInvoice(bool newFlagHasSalesInvoice, string user, string agent)
        {
            if (newFlagHasSalesInvoice != HasSalesInvoice)
            {
                HasSalesInvoice = newFlagHasSalesInvoice;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetShippingGrade(string newShippingGrade, string user, string agent)
        {
            if (newShippingGrade != ShippingGrade)
            {
                ShippingGrade = newShippingGrade;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetShippingRemark(string newShippingRemark, string user, string agent)
        {
            if (newShippingRemark != ShippingRemark)
            {
                ShippingRemark = newShippingRemark;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetWeight(double newWeight, string user, string agent)
        {
            if (newWeight != Weight)
            {
                Weight = newWeight;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDeliveryNote(string newDeliveryNote, string user, string agent)
        {
            if (newDeliveryNote != DeliveryNote)
            {
                DeliveryNote = newDeliveryNote;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalType(string newAvalType, string user, string agent)
        {
            if (newAvalType != AvalType)
            {
                AvalType = newAvalType;
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

        public void SetProductionMachine(string productionMachine, string user, string agent)
        {

            if (productionMachine != ProductionMachine)
            {
                ProductionMachine = productionMachine;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAdjDocumentNo(string newAdjDocumentNo, string user, string agent)
        {

            if (newAdjDocumentNo != AdjDocumentNo)
            {
                AdjDocumentNo = newAdjDocumentNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetProductSKUId(int newId, string user, string agent)
        {
            if(newId != ProductSKUId)
            {
                ProductSKUId = newId;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFabricSKUId(int newId, string user, string agent)
        {
            if (newId != FabricSKUId)
            {
                FabricSKUId = newId;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetProductSKUCode(string newProductSKUCode, string user, string agent)
        {

            if (newProductSKUCode != ProductSKUCode)
            {
                ProductSKUCode = newProductSKUCode;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasPrintingProductSKU(bool newHasPrintingProductSKU, string user, string agent)
        {

            if (newHasPrintingProductSKU != HasPrintingProductSKU)
            {
                HasPrintingProductSKU = newHasPrintingProductSKU;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetProductPackingId(int newId, string user, string agent)
        {
            if (newId != ProductPackingId)
            {
                ProductPackingId = newId;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFabricPackingId(int newId, string user, string agent)
        {
            if (newId != FabricPackingId)
            {
                FabricPackingId = newId;
                this.FlagForUpdate(user, agent);
            }
        }



        public void SetProductPackingCode(string newProductPackingCode, string user, string agent)
        {

            if (newProductPackingCode != ProductPackingCode)
            {
                ProductPackingCode = newProductPackingCode;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasPrintingProductPacking(bool newHasPrintingProductPacking, string user, string agent)
        {

            if (newHasPrintingProductPacking != HasPrintingProductPacking)
            {
                HasPrintingProductPacking = newHasPrintingProductPacking;
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

        public void SetNextAreaInputStatus(string newNextAreaInputStatus, string user, string agent)
        {

            if (newNextAreaInputStatus != NextAreaInputStatus)
            {
                NextAreaInputStatus = newNextAreaInputStatus;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFinishWidth(string newFinishWidth, string user, string agent)
        {

            if (newFinishWidth != FinishWidth)
            {
                FinishWidth = newFinishWidth;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDateIn(DateTimeOffset NewDateIn, string user, string agent)
        {
            if (NewDateIn != DateIn)
            {
                DateIn = NewDateIn;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
