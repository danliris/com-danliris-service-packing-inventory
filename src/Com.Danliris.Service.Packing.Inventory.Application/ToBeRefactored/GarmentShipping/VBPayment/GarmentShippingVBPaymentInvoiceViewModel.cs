using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentInvoiceViewModel : BaseViewModel
    {
        public int vbPaymentId { get; set; }
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
    }
}
