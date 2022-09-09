using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionPaymentDetailModel : StandardEntity
    {
        public int PaymentDispositionId { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string PaymentDescription { get; set; }
        public decimal Amount { get; set; }
        public GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset paymentDate, string paymentDescription, decimal amount)
        {
            PaymentDate = paymentDate;
            PaymentDescription = paymentDescription;
            Amount = amount;
        }

        public GarmentShippingPaymentDispositionPaymentDetailModel()
        {
        }

        public void SetPaymentDate(DateTimeOffset paymentDate, string username, string uSER_AGENT)
        {
            if (PaymentDate != paymentDate)
            {
                PaymentDate = paymentDate;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetPaymentDescription(string paymentDescription, string userName, string userAgent)
        {
            if (PaymentDescription != paymentDescription)
            {
                PaymentDescription = paymentDescription;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmount(decimal amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
