﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class InputPackagingProductionOrdersViewModel : BaseViewModel
    {
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public Material MaterialProduct { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public ProductTextile ProductTextile { get; set; }
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
        public double Balance { get; set; }
        public double InputQuantity { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
        public string Grade { get; set; }
        public string Remark { get; set; }

        public string ProductionMachine { get; set; }
        public string Status { get; set; }
        public string Material { get; set; }
        public string ProductionOrderNo { get; set; }
        public double QtyOrder { get; set; }
        public string Area { get; set; }
        public double BalanceRemains { get; set; }
        public double PreviousBalance { get; set; }

        public string PackingType { get; set; }
        public decimal QtyPacking { get; set; }
        public decimal InputQtyPacking { get; set; }
        public string PackingUnit { get; set; }
        public double PackingLength { get; set; }
        public double QtyOut { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        public int OutputId { get; set; }

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
        public DateTime CreatedUtcOrderNo { get; set; }
        public string MaterialOrigin { get; set; }

    }
}
