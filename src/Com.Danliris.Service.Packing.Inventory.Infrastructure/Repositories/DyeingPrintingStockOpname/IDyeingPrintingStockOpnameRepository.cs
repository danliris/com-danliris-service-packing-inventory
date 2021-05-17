using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname
{
  public  interface IDyeingPrintingStockOpnameRepository : IRepository<DyeingPrintingStockOpnameModel>
    {
        IQueryable<DyeingPrintingStockOpnameModel> GetDbSet();
        IQueryable<DyeingPrintingStockOpnameModel> ReadAllIgnoreQueryFilter();
        

    }
}
