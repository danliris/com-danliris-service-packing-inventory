using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            AvalProductionOrders = new HashSet<OutputAvalItemViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BonNo { get; set; }
        public string Shift { get; set; }
        public string AvalType { get; set; }
        public string CartNo { get; set; }
        public string UomUnit { get; set; }
        public string Qty { get; set; }
        public string QtyKg { get; set; }
        public ICollection<OutputAvalItemViewModel> AvalProductionOrders { get; set; }
    }
}
