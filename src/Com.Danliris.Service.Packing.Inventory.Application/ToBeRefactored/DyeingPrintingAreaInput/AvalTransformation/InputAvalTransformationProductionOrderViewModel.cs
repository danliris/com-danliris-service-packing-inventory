using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation
{
    public class InputAvalTransformationProductionOrderViewModel : BaseViewModel
    {
        public bool IsSave { get; set; }
        public string BonNo { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public Material Material { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public string MaterialWidth { get; set; }
        public string CartNo { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Buyer { get; set; }
        public int BuyerId { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string AvalType { get; set; }
        public string UomUnit { get; set; }
        public double Quantity { get; set; }
        public double InputQuantity { get; set; }
        public bool HasOutputDocument { get; set; }
        public string Machine { get; set; }

        public int InputId { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }

        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public bool HasPrintingProductPacking { get; set; }
    }
}
