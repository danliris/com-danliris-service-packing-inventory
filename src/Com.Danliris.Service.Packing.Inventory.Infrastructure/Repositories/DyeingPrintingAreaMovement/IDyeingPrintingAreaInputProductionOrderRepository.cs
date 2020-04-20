using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaInputProductionOrderRepository : IRepository<DyeingPrintingAreaInputProductionOrderModel>
    {
        IQueryable<DyeingPrintingAreaInputProductionOrderModel> GetDbSet();
        IQueryable<DyeingPrintingAreaInputProductionOrderModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked);
        Task<int> UpdateFromOutputAsync(int id, bool hasOutputDocument);
    }
}
