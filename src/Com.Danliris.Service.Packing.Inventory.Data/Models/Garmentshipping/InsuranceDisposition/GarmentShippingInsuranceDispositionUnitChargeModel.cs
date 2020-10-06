using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionUnitChargeModel : StandardEntity
    {
        public GarmentShippingInsuranceDispositionUnitChargeModel(int unitId, string unitCode, decimal amount)
        {
            UnitId = unitId;
            UnitCode = unitCode;
            Amount = amount;
        }

        public int InsuranceDispositionId { get; set; }

        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public decimal Amount { get; set; }

        public void SetUnitId(int unitId, string username, string uSER_AGENT)
        {
            if (UnitId != unitId)
            {
                UnitId = unitId;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetUnitCode(string unitCode, string username, string uSER_AGENT)
        {
            if (UnitCode != unitCode)
            {
                UnitCode = unitCode;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetAmount(decimal amount, string username, string uSER_AGENT)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
    }
}
