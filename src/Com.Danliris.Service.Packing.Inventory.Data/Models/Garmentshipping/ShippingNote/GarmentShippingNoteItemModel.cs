using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote
{
    public class GarmentShippingNoteItemModel : StandardEntity
    {
        public int ShippingNoteId { get; private set; }
        public string Description { get; private set; }
        public int CurrencyId { get; private set; }
        public string CurrencyCode { get; private set; }
        public double Amount { get; private set; }

        public GarmentShippingNoteItemModel()
        {
        }

        public GarmentShippingNoteItemModel(string description, int currencyId, string currencyCode, double amount)
        {
            Description = description;
            CurrencyId = currencyId;
            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCurrencyId(int currencyId, string userName, string userAgent)
        {
            if (CurrencyId != currencyId)
            {
                CurrencyId = currencyId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCurrencyCode(string currencyCode, string userName, string userAgent)
        {
            if (CurrencyCode != currencyCode)
            {
                CurrencyCode = currencyCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmount(double amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}

