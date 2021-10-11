using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListDetailSizeModel : StandardEntity
    {
        public int PackingListDetailId { get; private set; }
        public int SizeId { get; private set; }
        public string Size { get; private set; }
        public double Quantity { get; private set; }

        public GarmentDraftPackingListDetailSizeModel()
        {
        }

        public GarmentDraftPackingListDetailSizeModel(int sizeId, string size, double quantity)
        {
            SizeId = sizeId;
            Size = size;
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
