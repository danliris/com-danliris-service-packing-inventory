using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class ProductionOrderGroup
    {
        public long ProductionOrderId { get; set; }
        public string Grade { get; set; }
        public string PackagingType { get; set; }
    }
}
