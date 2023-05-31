using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
    public interface IDyeingPrintingStockOpnameMutationItemRepository : IRepository<DyeingPrintingStockOpnameMutationItemModel>
    {
        IQueryable<DyeingPrintingStockOpnameMutationItemModel> GetDbSet();
        IQueryable<DyeingPrintingStockOpnameMutationItemModel> ReadAllIgnoreQueryFilter();
    }
}
