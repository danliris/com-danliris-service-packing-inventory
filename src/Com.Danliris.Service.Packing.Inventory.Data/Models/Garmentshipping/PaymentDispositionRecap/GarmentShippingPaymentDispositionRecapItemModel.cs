using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapItemModel : StandardEntity
    {
        public int RecapId { get; private set; }

        public int PaymentDispositionId { get; private set; }
        public GarmentShippingPaymentDispositionModel PaymentDisposition { get; private set; }

        public double Service { get; private set; }

        public GarmentShippingPaymentDispositionRecapItemModel(int paymentDispositionId, double service)
        {
            PaymentDispositionId = paymentDispositionId;
            Service = service;
        }

        public GarmentShippingPaymentDispositionRecapItemModel()
        {
        }

        public void SetPaymentDisposition(GarmentShippingPaymentDispositionModel paymentDisposition)
        {
            PaymentDisposition = paymentDisposition;
        }

        public void SetService(double value, string userName, string userAgent)
        {
            if (Service != value)
            {
                Service = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
