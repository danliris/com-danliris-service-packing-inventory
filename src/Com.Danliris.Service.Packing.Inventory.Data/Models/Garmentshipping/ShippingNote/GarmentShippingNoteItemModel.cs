using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote
{
    public class GarmentShippingNoteItemModel : StandardEntity
    {
        public int ShippingNoteId { get; private set; }
        public string Description { get; private set; }
        public int CurrencyId { get; private set; }
        public string CurrencyCode { get; private set; }
        public int DebitCreditNoteId { get; private set; }
        public string TypeDebitCreditNote { get; private set; }
        public string ItemTypeDebitCreditNote { get; private set; }
        public double Amount { get; private set; }

        public GarmentShippingNoteItemModel()
        {
        }

        public GarmentShippingNoteItemModel(string description, int currencyId, string currencyCode, double amount/*, int debitCreditNoteId, string typeDebitCreditNote, string itemTypeDebitCreditNote*/)
        {
            Description = description;
            CurrencyId = currencyId;
            CurrencyCode = currencyCode;
            Amount = amount;
            //DebitCreditNoteId = debitCreditNoteId;
            //TypeDebitCreditNote = typeDebitCreditNote;
            //ItemTypeDebitCreditNote = itemTypeDebitCreditNote;
        }

        public GarmentShippingNoteItemModel(string description, int currencyId, string currencyCode, double amount, int debitCreditNoteId, string typeDebitCreditNote, string itemTypeDebitCreditNote)
        {
            Description = description;
            CurrencyId = currencyId;
            CurrencyCode = currencyCode;
            Amount = amount;
            DebitCreditNoteId = debitCreditNoteId;
            TypeDebitCreditNote = typeDebitCreditNote;
            ItemTypeDebitCreditNote = itemTypeDebitCreditNote;
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

        public void SetDebitCreditNoteId(int debitCreditNoteId, string userName, string userAgent)
        {
            if (DebitCreditNoteId != debitCreditNoteId)
            {
                DebitCreditNoteId = debitCreditNoteId;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetTypeDebitCreditNote(string typeDebitCreditNote, string userName, string userAgent)
        {
            if (TypeDebitCreditNote != typeDebitCreditNote)
            {
                TypeDebitCreditNote = typeDebitCreditNote;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetItemTypeDebitCreditNote(string itemTypeDebitCreditNote, string userName, string userAgent)
        {
            if (ItemTypeDebitCreditNote != itemTypeDebitCreditNote)
            {
                ItemTypeDebitCreditNote = itemTypeDebitCreditNote;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}

