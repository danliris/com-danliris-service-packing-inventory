using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking
{
    public class ProductPackingIndexInfo
    {
        public ProductPackingIndexInfo()
        {
        }

        public DateTime LastModifiedUtc { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UOMUnit { get; set; }
        public double PackingSize { get; set; }
        public int Id { get; set; }
        public string ProductSKUName { get; set; }
    }
}