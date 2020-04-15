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

        Task<int> InsertFromTransitAsync(int dyeingPrintingAreaMovementId, string shift, DateTimeOffset date, string area, string remark, DyeingPrintingAreaMovementHistoryModel history);
        Task<int> UpdateFromTransitAsync(int id, string shift, string remark);
        Task<int> DeleteFromTransitAsync(int id);

        Task<int> InsertFromAvalAsync(int dyeingPrintingAreaMovementId, string area, string shift, string uomUnit, double productionOrderQuantity, double qtyKg, DyeingPrintingAreaMovementHistoryModel history);
        Task<int> UpdateFromAvalAsync(int id, string area, string shift, string uomUnit, double productionOrderQuantity, double qtyKg);
        Task<int> DeleteFromAvalAsync(int id);

        Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked);
    }
}
