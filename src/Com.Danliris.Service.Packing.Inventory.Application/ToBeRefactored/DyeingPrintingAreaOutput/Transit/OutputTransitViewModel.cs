using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit
{
    public class OutputTransitViewModel : BaseViewModel
    {
        public OutputTransitViewModel()
        {
            TransitProductionOrders = new HashSet<OutputTransitProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputTransitId { get; set; }
        public string Group { get; set; }
        public ICollection<OutputTransitProductionOrderViewModel> TransitProductionOrders { get; set; }
    }
}
