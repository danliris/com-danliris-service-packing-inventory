using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition.PaymentDispositionEMKLs
{
    public class GarmentShippingPaymentDispositionEMKLInvoiceDetailViewModel : BaseViewModel
    {
        public int paymentDispositionId { get; set; }
        public string invoiceNo { get; set; }
        public int invoiceId { get; set; }
        public decimal quantity { get; set; }
        public decimal amount { get; set; }
        public decimal volume { get; set; }
        public decimal grossWeight { get; set; }
        public decimal chargeableWeight { get; set; }
        public decimal totalCarton { get; set; }
        public BuyerAgent buyerAgent { get; set; }
    }
}
