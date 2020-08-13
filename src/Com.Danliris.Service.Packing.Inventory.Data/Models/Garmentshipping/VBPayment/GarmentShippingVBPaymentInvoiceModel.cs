using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment
{
    public class GarmentShippingVBPaymentInvoiceModel : StandardEntity
    {

        public int VBPaymentId { get; private set; }
        public int InvoiceId { get; private set; }
        public string InvoiceNo { get; private set; }
        public GarmentShippingVBPaymentInvoiceModel(int invoiceId, string invoiceNo)
        {
            InvoiceId = invoiceId;
            InvoiceNo = invoiceNo;
        }
    }
}
