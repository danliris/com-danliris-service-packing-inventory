using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameBarcodeFormDto
    {
        public StockOpnameBarcodeFormDto()
        {
            Data = new List<string>();
        }
        public List<string> Data { get; set; }
    }
}