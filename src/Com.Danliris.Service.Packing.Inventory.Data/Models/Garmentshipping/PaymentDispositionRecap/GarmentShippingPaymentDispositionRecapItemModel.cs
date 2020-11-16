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

        public ICollection<GarmentShippingPaymentDispositionRecapDetailModel> Details { get; set; }

        public GarmentShippingPaymentDispositionRecapItemModel(int paymentDispositionId, ICollection<GarmentShippingPaymentDispositionRecapDetailModel> details)
        {
            PaymentDispositionId = paymentDispositionId;
            Details = details;
        }

        public GarmentShippingPaymentDispositionRecapItemModel()
        {
        }

        public void SetPaymentDisposition(GarmentShippingPaymentDispositionModel paymentDisposition)
        {
            PaymentDisposition = paymentDisposition;
        }
    }
}
