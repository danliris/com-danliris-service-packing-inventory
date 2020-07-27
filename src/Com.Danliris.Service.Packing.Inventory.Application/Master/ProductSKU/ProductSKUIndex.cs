using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU
{
    public class ProductSKUIndex
    {
        public List<ProductSKUIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public ProductSKUIndex(List<ProductSKUIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}