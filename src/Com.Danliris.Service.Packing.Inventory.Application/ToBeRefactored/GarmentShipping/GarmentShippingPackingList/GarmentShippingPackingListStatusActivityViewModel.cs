using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListStatusActivityViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAgent { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
