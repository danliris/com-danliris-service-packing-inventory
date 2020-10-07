using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial
{
    public class InputInspectionMaterialProductionOrderViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public Material Material { get; set; }
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
        public double Balance { get; set; }
        public double InputQuantity { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
        public string Grade { get; set; }
        public double InitLength { get; set; }
        //public double AvalALength { get; set; }
        //public double AvalBLength { get; set; }
        //public double AvalConnectionLength { get; set; }

        public double BalanceRemains { get; set; }

        public int InputId { get; set; }

        public DateTimeOffset DateIn { get;  set; }

    }
}
