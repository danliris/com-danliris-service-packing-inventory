using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public class GarmentShippingExportSalesDOItemViewModel : BaseViewModel
    {
        public ProductViewModel product { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public int exportSalesDOId { get; set; }
        public int exportSalesNoteItemId { get; set; }
        public string description { get; set; }
        public double packQuantity { get; set; }
        public UnitOfMeasurement packUom { get; set; }
        public double grossWeight { get; set; }
        public double nettWeight { get; set; }
    }
}
