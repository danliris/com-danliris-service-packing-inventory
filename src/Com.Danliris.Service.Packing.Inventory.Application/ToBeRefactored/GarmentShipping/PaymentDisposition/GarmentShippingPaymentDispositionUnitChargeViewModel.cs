using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionUnitChargeViewModel : BaseViewModel
    {
        public int paymentDispositionId { get; set; }
        public Unit unit { get; set; }
        public decimal amountPercentage { get; set; }
        public decimal billAmount { get; set; }
    }
}
