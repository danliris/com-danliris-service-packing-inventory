using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaMovementRepository : IRepository<DyeingPrintingAreaMovementModel>
    {
        IQueryable<DyeingPrintingAreaMovementModel> GetDbSet();
        IQueryable<DyeingPrintingAreaMovementModel> ReadAllIgnoreQueryFilter();
    }
}
