using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaSummaryRepository : IRepository<DyeingPrintingAreaSummaryModel>
    {
        IQueryable<DyeingPrintingAreaSummaryModel> GetDbSet();
        IQueryable<DyeingPrintingAreaSummaryModel> ReadAllIgnoreQueryFilter();
    }
}
