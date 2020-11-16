using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapDetailModel : StandardEntity
    {
        public int RecapItemId { get; private set; }

        public int PaymentDispositionInvoiceDetailId { get; private set; }
        public int InvoiceId { get; private set; }

        public double Service { get; private set; }

        public GarmentShippingPaymentDispositionRecapDetailModel(int paymentDispositionInvoiceDetailId, int invoiceId, double service)
        {
            PaymentDispositionInvoiceDetailId = paymentDispositionInvoiceDetailId;
            InvoiceId = invoiceId;
            Service = service;
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
