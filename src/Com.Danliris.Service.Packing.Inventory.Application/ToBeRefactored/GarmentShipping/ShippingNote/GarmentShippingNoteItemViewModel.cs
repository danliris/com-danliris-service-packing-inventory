using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote
{
    public class GarmentShippingNoteItemViewModel : BaseViewModel
    {
        public string description { get; set; }
        public Currency currency { get; set; }
        public DebitCreditNote debitCreditNote { get; set; }
        public double amount { get; set; }
    }
}
