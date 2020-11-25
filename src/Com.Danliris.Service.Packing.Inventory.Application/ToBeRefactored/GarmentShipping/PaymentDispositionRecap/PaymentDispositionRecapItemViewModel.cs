using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapItemViewModel : BaseViewModel
    {
        public GarmentShippingPaymentDispositionViewModel paymentDisposition { get; set; }
        public double service { get; set; }
    }

    public class GarmentShippingPaymentDispositionViewModel : BaseViewModel {
        public string dispositionNo { get; set; }

        public string invoiceNumber { get; set; }
        public string invoiceTaxNumber { get; set; }
        public DateTimeOffset invoiceDate { get; set; }

        public decimal amount { get; set; }

        public decimal billValue { get; set; }
        public decimal vatValue { get; set; }
        public decimal incomeTaxValue { get; set; }
        public decimal paid { get; set; }

        public Dictionary<string, double> percentage { get; set; }
        public Dictionary<string, double> amountPerUnit { get; set; }

        public ICollection<GarmentShippingPaymentDispositionInvoiceDetailViewModel> invoiceDetails { get; set; }
    }

    public class GarmentShippingPaymentDispositionInvoiceDetailViewModel : BaseViewModel
    {
        public string invoiceNo { get; set; }
        public int invoiceId { get; set; }
        public decimal quantity { get; set; }
        public decimal volume { get; set; }
        public decimal grossWeight { get; set; }
        public decimal chargeableWeight { get; set; }
        public decimal totalCarton { get; set; }
        public Invoice invoice { get; set; }
        public PackingList packingList { get; set; }
    }

    public class Invoice
    {
        public int packingListId { get; set; }
        public BuyerAgent BuyerAgent { get; set; }
        public List<InvoiceItem> items { get; set; }
        public string unit { get; set; }
    }

    public class InvoiceItem
    {
        public string unit { get; set; }
        public double quantity { get; set; }
    }

    public class PackingList
    {
        public double totalCBM { get; set; }
    }
}
