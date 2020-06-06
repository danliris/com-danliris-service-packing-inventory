using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailSizeModel : StandardEntity
    {
        public int PackingListDetailId { get; private set; }
        public int SizeId { get; private set; }
        public string Size { get; private set; }
        public double Quantity { get; private set; }

        public GarmentPackingListDetailSizeModel()
        {
        }

        public GarmentPackingListDetailSizeModel(int sizeId, string size, double quantity)
        {
            SizeId = sizeId;
            Size = size;
            Quantity = quantity;
        }
    }
}
