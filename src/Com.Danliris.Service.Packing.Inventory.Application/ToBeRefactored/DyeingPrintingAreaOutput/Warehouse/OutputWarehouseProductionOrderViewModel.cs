﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseProductionOrderViewModel: BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public Material MaterialProduct { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public string MaterialWidth { get; set; }
        public string FinishWidth { get; set; }
        public string CartNo { get; set; }
        public string PackingInstruction { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public double PreviousBalance { get; set; }
        public decimal PreviousQtyPacking { get; set; }

        public int InputId { get; set; }
        public string ProductionOrderNo { get; set; }
        public bool HasNextAreaDocument { get; set; }
        //public bool IsChecked { get; set; }
        public string Material { get; set; }
        public decimal MtrLength { get; set; }
        public decimal YdsLength { get; set; }
        public double Quantity { get; set; }
        public string PackagingType { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQty { get; set; }
        public double QtyOrder { get; set; }
        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string AdjDocumentNo { get; set; }

        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        public double BalanceRemains { get; set; }

        public bool IsSave { get; set; }

        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public bool HasPrintingProductPacking { get; set; }
        public DateTimeOffset DateIn { get; set; }
        public DateTimeOffset DateOut { get; set; }

        public string DestinationBuyerName { get; set; }
    }
}
