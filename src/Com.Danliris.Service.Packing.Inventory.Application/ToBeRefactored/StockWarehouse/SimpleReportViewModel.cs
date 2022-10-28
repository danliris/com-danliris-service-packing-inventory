using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class SimpleReportViewModel
    {
        public long ProductionOrderId { get; set; }
        public string NoSpp { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Motif { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Grade { get; set; }
        public string Jenis { get; set; }
        public string Ket { get; set; }
        public string Satuan { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public string ProcessTypeName { get; set; }
        public string InventoryType { get; set; }
        public string ProductTextileName { get; set; }
        public string ProductTextileCode { get; set; }
    }
}
