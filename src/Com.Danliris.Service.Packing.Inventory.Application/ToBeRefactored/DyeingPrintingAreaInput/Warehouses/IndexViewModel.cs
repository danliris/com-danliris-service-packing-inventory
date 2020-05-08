using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouses
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            WarehousesProductionOrders = new HashSet<InputWarehousesProductionOrdersViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public ICollection<InputWarehousesProductionOrdersViewModel> WarehousesProductionOrders { get; set; }
    }
}
