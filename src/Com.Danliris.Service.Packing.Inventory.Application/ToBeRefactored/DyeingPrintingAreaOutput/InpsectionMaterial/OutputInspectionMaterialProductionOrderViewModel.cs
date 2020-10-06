using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialProductionOrderViewModel : BaseViewModel
    {
        public OutputInspectionMaterialProductionOrderViewModel()
        {
            ProductionOrderDetails = new HashSet<OutputInspectionMaterialProductionOrderDetailViewModel>();
        }
        public ProductionOrder ProductionOrder { get; set; }
        public Material Material { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
        public string FinishWidth { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public string CartNo { get; set; }
        public string PackingInstruction { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Status { get; set; }
        public double PreviousBalance { get; set; }
        public double InitLength { get; set; }
        public string Machine { get; set; }

        public string ProductionMachine { get; set; }

        public double BalanceRemains { get; set; }
        public double Balance { get; set; }

        public int InputId { get; set; }

        public bool IsSave { get; set; }

        public string AdjDocumentNo { get; set; }

        public bool HasNextAreaDocument { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }

        public ICollection<OutputInspectionMaterialProductionOrderDetailViewModel> ProductionOrderDetails { get; set; }
    }

    public class OutputInspectionMaterialProductionOrderDetailViewModel : BaseViewModel
    {
        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public string AvalType { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public double Balance { get; set; }
        public bool HasNextAreaDocument { get; set; }

    }

    //public class AvalItem : BaseViewModel
    //{
    //    public string Type { get; set; }
    //    public double Length { get; set; }
    //}
}
