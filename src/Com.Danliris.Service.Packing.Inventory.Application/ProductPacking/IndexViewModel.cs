using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPacking
{
    public class IndexViewModel
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PackingType { get; set; }
        public decimal Quantity { get; set; }
    }
}
