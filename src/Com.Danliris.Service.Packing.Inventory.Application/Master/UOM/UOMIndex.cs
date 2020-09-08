using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UOM
{
    public class UOMIndex
    {
        public List<UOMIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public UOMIndex(List<UOMIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}