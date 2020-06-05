using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListItemViewModel : BaseViewModel
    {
        public string RONo { get; set; }
        public string SCNo { get; set; }
        public Buyer Buyer { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasurement Uom { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Valas { get; set; }
        public Unit Unit { get; set; }
        public string OTL { get; set; }
        public string Article { get; set; }
        public string OrderNo { get; set; }
        public string Description { get; set; }

        public List<GarmentPackingListDetailViewModel> Details { get; set; }
    }
}
