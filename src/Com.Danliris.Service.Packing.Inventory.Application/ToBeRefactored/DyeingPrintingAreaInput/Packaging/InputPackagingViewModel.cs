using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class InputPackagingViewModel :BaseViewModel
    {
        public InputPackagingViewModel()
        {
            PackagingProductionOrders = new HashSet<InputPackagingProductionOrdersViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public ICollection<InputPackagingProductionOrdersViewModel> PackagingProductionOrders { get; set; }
    }
}
