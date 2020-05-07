using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialViewModel : BaseViewModel
    {
        public OutputInspectionMaterialViewModel()
        {
            InspectionMaterialProductionOrders = new HashSet<OutputInspectionMaterialProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputInspectionMaterialId { get; set; }
        public string Group { get; set; }
        public ICollection<OutputInspectionMaterialProductionOrderViewModel> InspectionMaterialProductionOrders { get; set; }
    }
}
