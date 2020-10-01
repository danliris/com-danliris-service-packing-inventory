using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionItemViewModel : BaseViewModel
    {
        public int insuranceDispositionId { get; set; }
        public DateTimeOffset policyDate { get; set; }
        public string policyNo { get; set; }
        public string invoiceNo { get; set; }
        public int invoiceId { get; set; }

        public BuyerAgent BuyerAgent { get; set; }
        public decimal amount { get; set; }
        public decimal currencyRate { get; set; }
    }
}
