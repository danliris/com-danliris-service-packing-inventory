using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU
{
    public class ProductSKUIndexInfo
    {
        public ProductSKUIndexInfo()
        {
        }

        public DateTime LastModifiedUtc { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UOMUnit { get; set; }
        public int UOMId { get; set; }
        public string CategoryName { get; set; }
        public int Id { get; set; }
    }
}