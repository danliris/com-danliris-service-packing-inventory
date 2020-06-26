using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteItemModel : StandardEntity
    {
        public int PriceCuttingNoteId { get; private set; }
        public int SalesNoteId { get; private set; }
        public string SalesNoteNo { get; private set; }
        public double SalesAmount { get; private set; }
        public double CuttingAmount { get; private set; }

        public GarmentShippingLocalPriceCuttingNoteItemModel()
        {
        }

        public GarmentShippingLocalPriceCuttingNoteItemModel(int salesNoteId, string salesNoteNo, double salesAmount, double cuttingAmount)
        {
            SalesNoteId = salesNoteId;
            SalesNoteNo = salesNoteNo;
            SalesAmount = salesAmount;
            CuttingAmount = cuttingAmount;
        }
    }
}

