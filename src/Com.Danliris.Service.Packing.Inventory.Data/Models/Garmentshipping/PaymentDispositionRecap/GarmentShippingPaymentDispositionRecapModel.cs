using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapModel : StandardEntity
    {
        public string RecapNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int EmklId { get; private set; }
        public string EMKLCode { get; private set; }
        public string EMKLName { get; private set; }
        public string EMKLAddress { get; private set; }
        public string EMKLNPWP { get; private set; }

        public ICollection<GarmentShippingPaymentDispositionRecapItemModel> Items { get; set; }

        public GarmentShippingPaymentDispositionRecapModel(string recapNo, DateTimeOffset date, int emklId, string emklCode, string emklName, string emklAddress, string emklNPWP, ICollection<GarmentShippingPaymentDispositionRecapItemModel> items)
        {
            RecapNo = recapNo;
            Date = date;
            EmklId = emklId;
            EMKLCode = emklCode;
            EMKLName = emklName;
            EMKLAddress = emklAddress;
            EMKLNPWP = emklNPWP;
            Items = items;
        }

        public GarmentShippingPaymentDispositionRecapModel()
        {
        }

        public void SetDate(DateTimeOffset value, string userName, string userAgent)
        {
            if (Date != value)
            {
                Date = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
