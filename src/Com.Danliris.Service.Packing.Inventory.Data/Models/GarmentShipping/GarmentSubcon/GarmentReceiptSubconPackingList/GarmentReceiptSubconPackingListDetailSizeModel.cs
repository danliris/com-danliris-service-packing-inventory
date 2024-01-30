using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListDetailSizeModel : StandardEntity
    {
        public int PackingListDetailId { get; private set; }
        public Guid PackingOutItemId { get; private set; }
        public int SizeId { get; private set; }
        public string Size { get; private set; }
        public int SizeIdx { get; private set; }
        public double Quantity { get; private set; }
        public string Color { get; private set; }
        public GarmentReceiptSubconPackingListDetailSizeModel()
        {
        }

        public GarmentReceiptSubconPackingListDetailSizeModel(int sizeId, string size, int sizeIdx, double quantity ,string color,Guid packingOutItemId)
        {
            SizeId = sizeId;
            Size = size;
            SizeIdx = sizeIdx;
            Quantity = quantity;
            Color = color;
            PackingOutItemId = packingOutItemId;
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

        public void SetColor(string newValue, string userName, string userAgent)
        {
            if (Color != newValue)
            {
                Color = newValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetPackingOutItemId(Guid packingOutItemId, string userName, string userAgent)
        {
            if (PackingOutItemId != packingOutItemId)
            {
                PackingOutItemId = packingOutItemId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
