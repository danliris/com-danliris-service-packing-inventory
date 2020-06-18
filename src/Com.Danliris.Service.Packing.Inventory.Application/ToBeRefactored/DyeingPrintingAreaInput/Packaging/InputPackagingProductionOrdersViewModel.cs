using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class InputPackagingProductionOrdersViewModel :BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public Material MaterialProduct { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
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
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
        public string Grade { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string Material { get; set; }
        public decimal MtrLength { get; set; }
        public decimal YdsLength { get; set; }
        public string ProductionOrderNo { get; set; }
        public double QtyOrder { get; set; }
        public string Area { get; set; }

        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        public int OutputId { get; set; }

    }
}
