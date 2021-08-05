using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOItemViewModel : BaseViewModel
    {
        public int exportSalesDOId { get;  set; }
        public Comodity comodity { get;  set; }
        public string description { get;  set; }
        public double quantity { get;  set; }
        public UnitOfMeasurement uom { get;  set; }
        public double cartonQuantity { get;  set; }
        public double grossWeight { get;  set; }
        public double nettWeight { get;  set; }
        public double volume { get;  set; }
    }
}
