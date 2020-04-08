using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse
{
    public class GoodsWarehouseDocumentsViewModel
    {
        public DateTimeOffset Date { get; set; }
        public string Group { get; set; }
        public string Activities { get; set; }
        public string Mutation { get; set; }
        public string NoOrder { get; set; }
        public string Construction { get; set; }
        public string Motif { get; set; }
        public string Color { get; set; }
        public string Grade { get; set; }
        public string QtyPacking { get; set; }
        public string Packaging { get; set; }
        public string Qty { get; set; }
        public string Satuan { get; set; }
        public string Balance { get; set; }
    }
}
