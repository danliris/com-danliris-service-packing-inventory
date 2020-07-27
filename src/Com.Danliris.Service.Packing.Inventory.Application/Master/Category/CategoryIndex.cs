using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Category
{
    public class CategoryIndex
    {
        public List<CategoryIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public CategoryIndex(List<CategoryIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}