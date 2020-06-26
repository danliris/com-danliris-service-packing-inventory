using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteModel : StandardEntity
    {
        public string CuttingPriceNoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public bool UseVat { get; private set; }
        public string Remark { get; private set; }

        public ICollection<GarmentShippingLocalPriceCuttingNoteItemModel> Items { get; private set; }

        public GarmentShippingLocalPriceCuttingNoteModel()
        {
        }

        public GarmentShippingLocalPriceCuttingNoteModel(string cuttingPriceNoteNo, DateTimeOffset date, int buyerId, string buyerCode, string buyerName, bool useVat, string remark, ICollection<GarmentShippingLocalPriceCuttingNoteItemModel> items)
        {
            CuttingPriceNoteNo = cuttingPriceNoteNo;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            UseVat = useVat;
            Remark = remark;
            Items = items;
        }
    }
}
