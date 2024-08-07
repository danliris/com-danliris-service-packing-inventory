﻿using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping
{
    public class DPShippingMovementModel : StandardEntity
    {
        public DateTimeOffset Date { get; set; }
        public string Area { get; set; }
        public string DestinationArea { get; set; }
        public string Type { get; set; }
        public int  DPShippingInputItemId { get; set; }
        public int  DPShippingOutputItemId { get; set; }
        public int  DPShippingDocumentId { get; set; }
        public int  ProductionOrderId { get; set; }
        public string  ProductionOrderNo { get; set; }
        public int  BuyerId { get; set; }
        public string  BuyerName { get; set; }
        public string  Construction { get; set; }
        public string  Unit { get; set; }
        public string  Color { get; set; }
        public string  Motif { get; set; }
        public string  UomUnit { get; set; }
        public double  Balance { get; set; }
        public string  Grade { get; set; }
        public string  ProductionOrderType { get; set; }
        public string  Remark { get; set; }
        public string  Description { get; set; }
        public string  PackingType { get; set; }
        public double  PackagingLength { get; set; }
        public decimal  PackagingQty { get; set; }
        public string  PackagingUnit { get; set; }
        public string  MaterialOrigin { get; set; }
        public string  ProductPackingCode { get; set; }
        public int  ProductPackingId { get; set; }
        public int  ProductTextileId { get; set; }
        public string  ProductTextileCode { get; set; }
        public string  ProductTextileName { get; set; }

        public DPShippingMovementModel(
         DateTimeOffset date,
         string area,
         
         string type,
         int dPShippingInputItemId,
         int dPShippingOutputItemId,
         int dPShippingDocumentId,
         int productionOrderId,
         string productionOrderNo,
         int buyerId,
         string buyerName,
         string construction,
         string unit,
         string color,
         string motif,
         string uomUnit,
         double balance,
         string grade,
         string productionOrderType,
         string remark,
         string description,
         string packingType,
         double packagingLength,
         decimal packagingQty,
         string packagingUnit,
         string materialOrigin,
         string productPackingCode,
         int productPackingId,
         int productTextileId,
         string productTextileCode,
         string productTextileName
        )
        {
            Date = date;
            Area = area;
            
            Type = type;
            DPShippingInputItemId = dPShippingInputItemId;
            DPShippingOutputItemId = dPShippingOutputItemId;
            DPShippingDocumentId = dPShippingDocumentId;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            BuyerId = buyerId;
            BuyerName = buyerName;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            Remark = remark;
            Description = description;
            PackingType = packingType;
            PackagingLength = packagingLength;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            MaterialOrigin = materialOrigin;
            ProductPackingCode = productPackingCode;
            ProductPackingId = productPackingId;
            ProductTextileId = productTextileId;
            ProductTextileCode = productTextileCode;
            ProductTextileName = productTextileName;
        }

        //OutSHipping
        public DPShippingMovementModel(
         DateTimeOffset date,
         string area,
         string destinationArea,
         string type,
         int dPShippingInputItemId,
         int dPShippingOutputItemId,
         int dPShippingDocumentId,
         int productionOrderId,
         string productionOrderNo,
         int buyerId,
         string buyerName,
         string construction,
         string unit,
         string color,
         string motif,
         string uomUnit,
         double balance,
         string grade,
         string productionOrderType,
         string remark,
         string description,
         string packingType,
         double packagingLength,
         decimal packagingQty,
         string packagingUnit,
         string materialOrigin,
         string productPackingCode,
         int productPackingId,
         int productTextileId,
         string productTextileCode,
         string productTextileName
        )
        {
            Date = date;
            Area = area;
            DestinationArea = destinationArea;
            Type = type;
            DPShippingInputItemId = dPShippingInputItemId;
            DPShippingOutputItemId = dPShippingOutputItemId;
            DPShippingDocumentId = dPShippingDocumentId;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            BuyerId = buyerId;
            BuyerName = buyerName;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            Remark = remark;
            Description = description;
            PackingType = packingType;
            PackagingLength = packagingLength;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            MaterialOrigin = materialOrigin;
            ProductPackingCode = productPackingCode;
            ProductPackingId = productPackingId;
            ProductTextileId = productTextileId;
            ProductTextileCode = productTextileCode;
            ProductTextileName = productTextileName;
        }

    }

    
}
