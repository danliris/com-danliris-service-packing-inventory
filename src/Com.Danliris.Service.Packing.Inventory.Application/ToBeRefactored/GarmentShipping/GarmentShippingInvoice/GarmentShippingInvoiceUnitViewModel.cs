using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceUnitViewModel : BaseViewModel
    {
        public int GarmentShippingInvoiceId { get; set; }
        public Unit Unit { get; set; }
        public decimal AmountPercentage { get; set; }
        public decimal QuantityPercentage { get; set; }
    }
}
