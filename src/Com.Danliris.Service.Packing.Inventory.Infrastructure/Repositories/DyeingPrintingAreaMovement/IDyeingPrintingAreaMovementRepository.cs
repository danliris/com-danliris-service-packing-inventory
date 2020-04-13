using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaMovementRepository : IRepository<DyeingPrintingAreaMovementModel>
    {
        IQueryable<DyeingPrintingAreaMovementModel> GetDbSet();
        IQueryable<DyeingPrintingAreaMovementModel> ReadAllIgnoreQueryFilter();
        Task<int> InsertFromTransitAsync(DyeingPrintingAreaMovementModel model);
        Task<int> DeleteFromTransitAsync(int id);
        Task<int> UpdateFromTransitAsync(int id, string shift, string remark);
        Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked);
    }
}
