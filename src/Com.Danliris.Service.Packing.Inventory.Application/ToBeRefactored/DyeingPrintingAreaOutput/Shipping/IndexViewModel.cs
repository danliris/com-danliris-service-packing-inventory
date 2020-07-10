using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            ShippingProductionOrders = new HashSet<OutputShippingProductionOrderViewModel>();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string Type { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Group { get; set; }
        public string Shift { get; set; }
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public bool HasSalesInvoice { get; set; }
        public ICollection<OutputShippingProductionOrderViewModel> ShippingProductionOrders { get; set; }
    }
}
