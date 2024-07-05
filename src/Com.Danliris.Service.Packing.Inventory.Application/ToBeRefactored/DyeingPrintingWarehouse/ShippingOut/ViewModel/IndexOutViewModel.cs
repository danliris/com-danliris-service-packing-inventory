using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut.ViewModel
{
    public class IndexOutViewModel
    {

        public IndexOutViewModel()
        {
            ShippingItems = new HashSet<OutputShippingItemViewModel>();
        }
        public int Id { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string Type { get; set; }
        public string ShippingCode { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Group { get; set; }
        public string Shift { get; set; }
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public bool UpdateBySales { get; set; }
        public ICollection<OutputShippingItemViewModel> ShippingItems { get; set; }

    }
}
