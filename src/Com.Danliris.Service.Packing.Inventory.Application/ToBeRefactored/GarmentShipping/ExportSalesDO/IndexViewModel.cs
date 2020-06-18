using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO
{
    public class IndexViewModel
    {
        public int id { get; set; }
        public string exportSalesDONo { get; set; }
        public string invoiceNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyerAgent { get; set; }
        public string to { get; set; }
        public Unit unit { get; set; }
    }
}
