using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionUnitChargeViewModel : BaseViewModel
    {
        public int insuranceDispositionId { get; set; }

        public Unit unit { get; set; }
        public decimal amount { get; set; }
    }
}
