using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricSKUIndex
    {
        public List<FabricSKUIndexInfo> data;
        public int total;
        public int page;
        public int size;

        public FabricSKUIndex(List<FabricSKUIndexInfo> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}