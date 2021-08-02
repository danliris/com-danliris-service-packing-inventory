using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            TransitProductionOrders = new HashSet<InputTransitProductionOrderViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public ICollection<InputTransitProductionOrderViewModel> TransitProductionOrders { get; set; }
    }
}
