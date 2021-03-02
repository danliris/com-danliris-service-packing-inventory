using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapItemModel : StandardEntity
    {
        public int RecapId { get; set; }

        public int PaymentDispositionId { get; private set; }
        public GarmentShippingPaymentDispositionModel PaymentDisposition { get; private set; }

        public double Service { get; private set; }
        public double OthersPayment { get; private set; }
        public double TruckingPayment { get; private set; }
        public double VatService { get; private set; }
        public double AmountService { get; private set; }

        public GarmentShippingPaymentDispositionRecapItemModel(int paymentDispositionId, double service, double othersPayment, double truckingPayment, double vatService, double amountService)
        {
            PaymentDispositionId = paymentDispositionId;
            Service = service;
            OthersPayment = othersPayment;
            TruckingPayment = truckingPayment;
            VatService = vatService;
            AmountService = amountService;
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

        public void SetOthersPayment(double value, string userName, string userAgent)
        {
            if (OthersPayment != value)
            {
                OthersPayment = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTruckingPayment(double value, string userName, string userAgent)
        {
            if (TruckingPayment != value)
            {
                TruckingPayment = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetVatService(double value, string userName, string userAgent)
        {
            if (VatService != value)
            {
                VatService = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmountService(double value, string userName, string userAgent)
        {
            if (AmountService != value)
            {
                AmountService = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
