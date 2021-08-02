using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceUnitModel : StandardEntity
    {
        public GarmentShippingInvoiceUnitModel(int unitId, string unitCode, decimal amountPercentage, decimal quantityPercentage)
        {
            UnitId = unitId;
            UnitCode = unitCode;
            AmountPercentage = amountPercentage;
            QuantityPercentage = quantityPercentage;
        }

        public int GarmentShippingInvoiceId { get; set; }

        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public decimal AmountPercentage { get; set; }
        public decimal QuantityPercentage { get; set; }

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

        public void SetQuantityPercentage(decimal QuantityPercentage, string username, string uSER_AGENT)
        {
            if (this.QuantityPercentage != QuantityPercentage)
            {
                this.QuantityPercentage = QuantityPercentage;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
    }
}
