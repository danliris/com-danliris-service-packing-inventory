using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionUnitChargeModel : StandardEntity
    {

        public int PaymentDispositionId { get; set; }
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public decimal AmountPercentage { get; set; }
        public decimal BillAmount { get; set; }
        public GarmentShippingPaymentDispositionUnitChargeModel(int unitId, string unitCode, decimal amountPercentage, decimal billAmount)
        {
            UnitId = unitId;
            UnitCode = unitCode;
            AmountPercentage = amountPercentage;
            BillAmount = billAmount;
        }

        public GarmentShippingPaymentDispositionUnitChargeModel()
        {
        }

        public void SetUnitId(int UnitId, string username, string uSER_AGENT)
        {
            if (this.UnitId != UnitId)
            {
                this.UnitId = UnitId;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetUnitCode(string UnitCode, string username, string uSER_AGENT)
        {
            if (this.UnitCode != UnitCode)
            {
                this.UnitCode = UnitCode;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetAmountPercentage(decimal AmountPercentage, string username, string uSER_AGENT)
        {
            if (this.AmountPercentage != AmountPercentage)
            {
                this.AmountPercentage = AmountPercentage;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetBillAmount(decimal BillAmount, string username, string uSER_AGENT)
        {
            if (this.BillAmount != BillAmount)
            {
                this.BillAmount = BillAmount;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
    }
}
