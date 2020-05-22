using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create
{
    public class InputWarehouseCreateViewModel : BaseViewModel
    {
        public InputWarehouseCreateViewModel()
        {
            MappedWarehousesProductionOrders = new HashSet<InputWarehouseProductionOrderCreateViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public int OutputId { get; set; }
        public string Group { get; set; }
        public ICollection<InputWarehouseProductionOrderCreateViewModel> MappedWarehousesProductionOrders { get; set; }
    }
}
