using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
    public interface IDyeingPrintingStockOpnameMutationRepository :  IRepository<DyeingPrintingStockOpnameMutationModel>
    {
        IQueryable<DyeingPrintingStockOpnameMutationModel> GetDbSet();
        IQueryable<DyeingPrintingStockOpnameMutationModel> ReadAllIgnoreQueryFilter();
    }
}
