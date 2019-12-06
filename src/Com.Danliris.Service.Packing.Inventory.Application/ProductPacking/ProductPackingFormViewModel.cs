using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPacking
{
    public class ProductPackingFormViewModel
    {
        public string PackingType { get; set; }
        public decimal? Quantity { get; set; }
        public int? SKUId { get; set; }
    }
}
