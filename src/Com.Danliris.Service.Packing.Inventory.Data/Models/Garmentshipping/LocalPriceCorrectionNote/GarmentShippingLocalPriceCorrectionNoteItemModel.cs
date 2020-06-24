using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteItemModel : StandardEntity
    {
        public int PriceCorrectionNoteId { get; private set; }
        public int SalesNoteItemId { get; private set; }
        public GarmentShippingLocalSalesNoteItemModel SalesNoteItem { get; private set; }
        public double PriceCorrection { get; private set; }

        public GarmentShippingLocalPriceCorrectionNoteItemModel()
        {
        }

        public GarmentShippingLocalPriceCorrectionNoteItemModel(int salesNoteItemId, double priceCorrection)
        {
            SalesNoteItemId = salesNoteItemId;
            SalesNoteItem = new GarmentShippingLocalSalesNoteItemModel();
            PriceCorrection = priceCorrection;
        }
    }
}

