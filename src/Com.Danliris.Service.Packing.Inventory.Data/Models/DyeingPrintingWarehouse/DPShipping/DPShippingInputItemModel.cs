using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping
{
    public class DPShippingInputItemModel : StandardEntity
    {
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
        public double Balance { get; set; }
        public double BalanceRemains { get; set; }
        public string PackingInstruction { get; set; }
        public string ProductionOrderType { get; set; }
        public string ProductionOrderOrderQuantity { get; set; }
        public DateTimeOffset CreatedUtcOrderNo { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public int DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public double PackagingLength { get; set; }
        public string AreaOrigin { get; set; }
        public int DPShippingInputId { get; set; }
        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get;set; }
        public int ProductPackingId { get;set; }
        public int FabricPackingId { get;set; }
        public string ProductPackingCode { get;set; }
        public int ProcessTypeId { get;set; }
        public string ProcessTypeName { get;set; }
        public int YarnMaterialId { get;set; }
        public string YarnMaterialName { get;set; }
        public double InputPackagingQty { get;set; }
        public double InputQuantity { get;set; }
        public int DeliveryOrderReturId { get;set; }
        public string DeliveryOrderReturNo { get;set; }
        public string FinishWidth { get;set; }
        public string DestinationBuyerName { get;set; }
        public string MaterialOrigin { get; set; }
        public string DeliveryOrderSalesType { get; set; }
        public DPShippingInputModel DPShippingInput { get; set; }
       

        public DPShippingInputItemModel(
            int productionOrderId,
         string productionOrderNo,
         int materialId,
         string materialName,
         int materialConstructionId,
         string materialConstructionName,
         string materialWidth,
         int buyerId,
         string buyerName,
         string construction,
         string unit,
         string color,
         string motif,
         string uomUnit,
         double balance,
         double balanceRemains,
         string packingInstruction,
         string productionOrderType,
         string productionOrderOrderQuantity,
         DateTimeOffset createdUtcOrderNo,
         string remark,
         string grade,
         string description,
         int deliveryOrderSalesId,
         string deliveryOrderSalesNo,
         string packagingUnit,
         string packagingType,
         decimal packagingQty,
         double packagingLength,
         string areaOrigin,
         int dPShippingInputId,
         int productSKUId,
         int fabricSKUId,
         string productSKUCode,
         int productPackingId,
         int fabricPackingId,
         string productPackingCode,
         int processTypeId,
         string processTypeName,
         int yarnMaterialId,
         string yarnMaterialName,
         double inputPackagingQty,
         double inputQuantity,
         string finishWidth,
         string destinationBuyerName,
         string materialOrigin,
         string deliveryOrderSalesType

            ) 
        
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialWidth = materialWidth;
            BuyerId = buyerId;
            BuyerName = buyerName;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            BalanceRemains = balanceRemains;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            CreatedUtcOrderNo = createdUtcOrderNo;
            Remark = remark;
            Grade = grade;
            Description = description;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            PackagingUnit = packagingUnit;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingLength = packagingLength;
            AreaOrigin = areaOrigin;
            DPShippingInputId = dPShippingInputId;
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
            InputPackagingQty = inputPackagingQty;
            InputQuantity = inputQuantity;

            FinishWidth = finishWidth;
            DestinationBuyerName = destinationBuyerName;
            MaterialOrigin = materialOrigin;
            DeliveryOrderSalesType = deliveryOrderSalesType;
            
        }


    }
}
