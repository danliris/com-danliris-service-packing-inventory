using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            ShippingProductionOrders = new HashSet<InputShippingProductionOrderViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string ShippingType { get; set; }
        public ICollection<InputShippingProductionOrderViewModel> ShippingProductionOrders { get; set; }
    }
}
