using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaMovementHistoryRepository : IRepository<DyeingPrintingAreaMovementHistoryModel>
    {
        IQueryable<DyeingPrintingAreaMovementHistoryModel> ReadByDyeingPrintingAreaMovement(int dyeingPrintingAreaMovementId);
        Task<int> UpdateAsyncFromParent(int dyeingPrintingAreaMovementId, AreaEnum index, DateTimeOffset newDate, string shift);
    }
}
