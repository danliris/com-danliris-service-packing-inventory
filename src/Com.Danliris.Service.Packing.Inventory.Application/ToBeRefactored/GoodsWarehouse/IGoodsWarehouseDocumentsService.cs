using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse
{
    public interface IGoodsWarehouseDocumentsService
    {
         List<IndexViewModel> GetList(DateTimeOffset? date, string group, string zona, int timeOffSet);
         MemoryStream GetExcel(DateTimeOffset? date, string group, string zona, int timeOffSet);
    }
}
