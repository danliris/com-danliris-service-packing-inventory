using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID
{
    public class ProductRFIDIndex
    {
        public List<ProductRFIDIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public ProductRFIDIndex(List<ProductRFIDIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}
