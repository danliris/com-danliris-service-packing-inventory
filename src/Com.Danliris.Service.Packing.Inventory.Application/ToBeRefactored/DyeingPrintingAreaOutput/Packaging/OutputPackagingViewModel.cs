using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingViewModel : BaseViewModel
    {
        public OutputPackagingViewModel()
        {
            PackagingProductionOrders = new HashSet<OutputPackagingProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputPackagingId { get; set; }
        public ICollection<OutputPackagingProductionOrderViewModel> PackagingProductionOrders { get; set; }
    }
}
