using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingProductionOrderGroupedViewModel
    {
        public string ProductionOrder { get; set; }
        public List<InputPackagingProductionOrdersViewModel> ProductionOrderList { get; set; }
    }
}
