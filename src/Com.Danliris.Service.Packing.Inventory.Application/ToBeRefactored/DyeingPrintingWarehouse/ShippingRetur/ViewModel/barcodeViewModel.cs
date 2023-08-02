using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur.ViewModel
{
    public class barcodeViewModel :BaseViewModel
    {
        public string Code { get;  set; }
        public int FabricProductSKUId { get;  set; }
        public int ProductSKUId { get;  set; }
        public int ProductPackingId { get;  set; }
        public int FabricPackingId { get; set; }
    }
}
