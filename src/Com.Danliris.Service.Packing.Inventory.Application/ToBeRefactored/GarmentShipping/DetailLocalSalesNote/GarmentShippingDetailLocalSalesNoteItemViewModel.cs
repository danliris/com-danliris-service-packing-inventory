using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemViewModel : BaseViewModel
    {
        public int detaiLocalSalesNoteId { get; set; }
        public Unit Unit { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double amount { get; set; }
    }
}
