using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            InspectionMaterialProductionOrders = new HashSet<OutputInspectionMaterialProductionOrderViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public ICollection<OutputInspectionMaterialProductionOrderViewModel> InspectionMaterialProductionOrders { get; set; }
    }
}
