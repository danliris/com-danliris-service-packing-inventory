using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking
{
    public class ProductPackingIndex
    {
        public List<ProductPackingIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public ProductPackingIndex(List<ProductPackingIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}