using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit
{
    public class GarmentShippingAmendLetterOfCreditModel : StandardEntity
    {

        public string DocumentCreditNo { get; private set; }
        public int LetterOfCreditId { get; private set; }
        public int AmendNumber { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Description { get; private set; }
        public double Amount { get; private set; }

        public GarmentShippingAmendLetterOfCreditModel(string documentCreditNo, int letterOfCreditId, int amendNumber, DateTimeOffset date, string description, double amount)
        {
            DocumentCreditNo = documentCreditNo;
            LetterOfCreditId = letterOfCreditId;
            AmendNumber = amendNumber;
            Date = date;
            Description = description;
            Amount = amount;
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
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
