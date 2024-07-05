using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping
{
    public class DPShippingOutputItemModel : StandardEntity
    {
        public DateTimeOffset Date { get; set; }
        public int ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }
        public string MaterialWidth { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public double Balance { get; set; }
        public string PackingInstruction { get; set; }
        public string ProductionOrderType { get; set; }
        public string ProductionOrderOrderQuantity { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public int DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Area { get; set; }
        public string DestinationArea { get; set; }
        public string Description { get; set; }
        public string ShippingGrade { get; set; }
        public string ShippingRemark { get; set; }
        public int DPShippingOutputId { get; set; }
        public string ProductPackingCode { get; set; }
        public int ProductPackingId { get; set; }
        public string ProductSKUCode { get; set; }
        public int ProductSKUId { get; set; }
        public int ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }
        public int YarnMaterialId { get; set; }
        public string YarnMaterialName { get; set; }
        public int FabricPackingId { get; set; }
        public int FabricSKUId { get; set; }
        public double PackagingLength { get; set; }
        public string FinishWidth { get; set; }
        public string DestinationBuyerName { get; set; }
        public string MaterialOrigin { get; set; }
        public string PackingListBaleNo { get; set; }
        public decimal PackingListGross { get; set; }
        public decimal PackingListNet { get; set; }
        public string DeliveryOrderSalesType { get; set; }
        public DateTimeOffset CreatedUtcOrderNo { get; set; }
        public int DPShippingInputItemId { get; set; }
        public string DeliveryNote { get; set; }
        public DPShippingOutputModel DPShippingOutput { get; set; }

        public DPShippingOutputItemModel()
        {
        }

        public DPShippingOutputItemModel( string area,  string destinationArea, bool hasNextAreaDocument, int deliveryOrderSalesId, string deliveryOrderSalesNo, int productionOrderId, string productionOrderNo, string productionOrderType, string productionOrderQuantity, int buyerId, string buyer, string construction,
            string unit, string color, string motif, string grade, string uomUnit,  double balance, int dyeingPrintingAreaInputProductonOrderId, string packingUnit, string packingType, decimal qtyPacking,  string shippingGrade, string shippingRemark, double weight,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string remark, 
            int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode,  int productPackingId, int fabricPackingId, string productPackingCode, double packingLength, string finishWidth, string destinationBuyerName, string materialOrigin, string deliveryOrderSalesType, string packingInstruction, int dPShippingInputItemId
            ) : this()

        {
            Area = area;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            BuyerName = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            Balance = balance;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            
            Unit = unit;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

            DPShippingOutputId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

           

            ShippingGrade = shippingGrade;
            ShippingRemark = shippingRemark;
           

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;

           
            Remark = remark;
            
            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;

            PackagingLength = packingLength;
            
            DestinationBuyerName = destinationBuyerName;
            
            MaterialOrigin = materialOrigin;
            DeliveryOrderSalesType = deliveryOrderSalesType;
            PackingInstruction = packingInstruction;
            DPShippingInputItemId = dPShippingInputItemId;
        }

        //Output Buyer
        public DPShippingOutputItemModel(string area, string destinationArea, bool hasNextAreaDocument, int deliveryOrderSalesId, string deliveryOrderSalesNo, int productionOrderId, string productionOrderNo, string productionOrderType, string productionOrderQuantity, int buyerId, string buyer, string construction,
            string unit, string color, string motif, string grade, string uomUnit, double balance, int dyeingPrintingAreaInputProductonOrderId, string packingUnit, string packingType, decimal qtyPacking, string shippingGrade, string shippingRemark, double weight,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string remark,
            int processTypeId, string processTypeName, int yarnMaterialId, string yarnMaterialName,
            int productSKUId, int fabricSKUId, string productSKUCode, int productPackingId, int fabricPackingId, string productPackingCode, double packingLength, string finishWidth, string destinationBuyerName, string materialOrigin, string deliveryOrderSalesType, string packingInstruction, int dPShippingInputItemId,
            decimal packingListNet, decimal packingListGross, string packingListBaleNo, string deliveryNote
            ) : this()

        {
            Area = area;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            BuyerName = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            Balance = balance;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;

            Unit = unit;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

            DPShippingOutputId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;



            ShippingGrade = shippingGrade;
            ShippingRemark = shippingRemark;


            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
            FinishWidth = finishWidth;


            Remark = remark;

            ProcessTypeId = processTypeId;
            ProcessTypeName = processTypeName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;

            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;

            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;

            PackagingLength = packingLength;

            DestinationBuyerName = destinationBuyerName;

            MaterialOrigin = materialOrigin;
            DeliveryOrderSalesType = deliveryOrderSalesType;
            PackingInstruction = packingInstruction;
            DPShippingInputItemId = dPShippingInputItemId;
            PackingListNet = packingListNet;
            PackingListGross = packingListGross;
            PackingListBaleNo = packingListBaleNo;
            DeliveryNote = deliveryNote;
        }

    }

    
}
