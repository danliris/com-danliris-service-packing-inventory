using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            PackagingProductionOrders = new HashSet<InputPackagingProductionOrdersViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public ICollection<InputPackagingProductionOrdersViewModel> PackagingProductionOrders { get; set; }
    }
}
