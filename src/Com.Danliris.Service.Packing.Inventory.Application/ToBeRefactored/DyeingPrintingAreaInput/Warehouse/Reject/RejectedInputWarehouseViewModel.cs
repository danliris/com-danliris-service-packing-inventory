using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Reject
{
    public class RejectedInputWarehouseViewModel : BaseViewModel
    {
        public RejectedInputWarehouseViewModel()
        {
            WarehousesProductionOrders = new HashSet<RejectedInputWarehouseProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public int OutputId { get; set; }
        public string Group { get; set; }
        public ICollection<RejectedInputWarehouseProductionOrderViewModel> WarehousesProductionOrders { get; set; }
    }
}
