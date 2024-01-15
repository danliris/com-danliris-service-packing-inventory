using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSItemViewModel : BaseViewModel
    {
        public int packingListId { get; set; }
        public string invoiceNo { get; set; }
        //public ProductViewModel product { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double price { get; set; }
        public double packageQuantity { get; set; }
        public UnitOfMeasurement packageUom { get; set; }

        public double remQty { get; set; }
        public string remark { get; set; }
    }
}
