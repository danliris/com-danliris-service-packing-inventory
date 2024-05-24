using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse
{
    public interface IDPWarehouseOutputRepository : IRepository<DPWarehouseOutputModel>
    {
        IQueryable<DPWarehouseOutputModel> GetDbSet();
        IQueryable<DPWarehouseOutputModel> ReadAllIgnoreQueryFilter();
    }
}
