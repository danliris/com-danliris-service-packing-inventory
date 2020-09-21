using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractItemViewModel : BaseViewModel
    {
        public ProductViewModel product { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double price { get; set; }
    }
}
