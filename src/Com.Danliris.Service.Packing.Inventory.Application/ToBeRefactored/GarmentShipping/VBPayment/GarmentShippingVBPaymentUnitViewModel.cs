using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentUnitViewModel : BaseViewModel
    {
        public int vbPaymentId { get; set; }
        public Unit unit { get; set; }
        public double billValue { get; set; }
    }
}
