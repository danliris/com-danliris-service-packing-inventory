using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment
{
    public class GarmentShippingVBPaymentUnitModel : StandardEntity
    {

        public int VBPaymentId { get; private set; }
        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }
        public string UnitName { get; private set; }
        public double BillValue { get; private set; }
        public GarmentShippingVBPaymentUnitModel(int unitId, string unitCode, string unitName, double billValue)
        {
            UnitId = unitId;
            UnitCode = unitCode;
            UnitName = unitName;
            BillValue = billValue;
        }


        public void SetIncomeTaxRate(double billValue, string userName, string userAgent)
        {
            if (BillValue != billValue)
            {
                BillValue = billValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
