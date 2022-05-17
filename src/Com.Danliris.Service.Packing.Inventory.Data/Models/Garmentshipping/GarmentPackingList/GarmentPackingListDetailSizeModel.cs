using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListDetailSizeModel : StandardEntity
    {
        public int PackingListDetailId { get; private set; }
        public int SizeId { get; private set; }
        public string Size { get; private set; }
        public int SizeIdx { get; private set; }
        public double Quantity { get; private set; }

        public GarmentPackingListDetailSizeModel()
        {
        }

        public GarmentPackingListDetailSizeModel(int sizeId, string size, int sizeIdx, double quantity)
        {
            SizeId = sizeId;
            Size = size;
            SizeIdx = sizeIdx;
            Quantity = quantity;
        }

        public void SetSizeId(int newValue, string userName, string userAgent)
        {
            if (SizeId != newValue)
            {
                SizeId = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSize(string newValue, string userName, string userAgent)
        {
            if (Size != newValue)
            {
                Size = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSizeIdx(int newValue, string userName, string userAgent)
        {
            if (SizeIdx != newValue)
            {
                SizeIdx = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantity(double newValue, string userName, string userAgent)
        {
            if (Quantity != newValue)
            {
                Quantity = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
