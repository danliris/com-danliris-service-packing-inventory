using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseViewModel : BaseViewModel
    {
        public OutputWarehouseViewModel()
        {
            WarehousesProductionOrders = new HashSet<OutputWarehouseProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputWarehouseId { get; set; }
        public string Group { get; set; }
        public ICollection<OutputWarehouseProductionOrderViewModel> WarehousesProductionOrders { get; set; }
    }
}
