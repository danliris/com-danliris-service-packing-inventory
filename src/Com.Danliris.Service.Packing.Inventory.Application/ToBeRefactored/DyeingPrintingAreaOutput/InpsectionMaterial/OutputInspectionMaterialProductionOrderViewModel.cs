using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
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
            AvalItems = new HashSet<AvalItem>();
        }
        public ProductionOrder ProductionOrder { get; set; }
        public string CartNo { get; set; }
        public string PackingInstruction { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public double PreviousBalance { get; set; }
        public double InitLength { get; set; }
        public double AvalALength { get; set; }
        public double AvalBLength { get; set; }
        public double AvalConnectionLength { get; set; }
        public bool HasNextAreaDocument { get; set; }

        public ICollection<AvalItem> AvalItems { get; set; }

        public int InputId { get; set; }
    }

    public class AvalItem
    {
        public string Type { get; set; }
        public double Length { get; set; }
    }
}
