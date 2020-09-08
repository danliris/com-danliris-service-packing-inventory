using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class PackingDataViewModel
    {
        public string Type { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public double PackagingLength { get; set; }
        public double Balance { get; set; }
    }
}
