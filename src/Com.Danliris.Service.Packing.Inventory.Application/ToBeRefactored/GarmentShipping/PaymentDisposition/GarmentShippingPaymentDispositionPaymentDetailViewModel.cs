using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionPaymentDetailViewModel : BaseViewModel
    {
        public int paymentDispositionId { get; set; }
        public DateTimeOffset paymentDate { get; set; }
        public string paymentDescription { get; set; }
        public decimal amount { get; set; }
    }
}
