using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionBillDetailModel : StandardEntity
    {
        public int PaymentDispositionId { get; set; }
        public string BillDescription { get; set; }
        public decimal Amount { get; set; }
        public GarmentShippingPaymentDispositionBillDetailModel(string billDescription, decimal amount)
        {
            BillDescription = billDescription;
            Amount = amount;
        }

        public GarmentShippingPaymentDispositionBillDetailModel()
        {
        }

        public void SetBillDescription(string billDescription, string userName, string userAgent)
        {
            if (BillDescription != billDescription)
            {
                BillDescription = billDescription;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmount(decimal amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
