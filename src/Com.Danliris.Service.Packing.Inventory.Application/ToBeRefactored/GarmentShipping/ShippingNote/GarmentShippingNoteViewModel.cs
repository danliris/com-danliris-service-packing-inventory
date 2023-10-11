using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public class GarmentShippingNoteViewModel : BaseViewModel
    {
        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyer { get; set; }
        public double totalAmount { get; set; }
        public double amountCA { get; set; }
        public string description { get; set; }
        public string receiptNo { get; set; }
        public DateTimeOffset? receiptDate { get; set; }

        public ICollection<GarmentShippingNoteItemViewModel> items { get; set; }
    }
}
