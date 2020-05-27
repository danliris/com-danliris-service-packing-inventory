using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaOutputProductionOrderRepository : IRepository<DyeingPrintingAreaOutputProductionOrderModel>
    {
        IQueryable<DyeingPrintingAreaOutputProductionOrderModel> GetDbSet();
        IQueryable<DyeingPrintingAreaOutputProductionOrderModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromInputNextAreaFlagAsync(int id, bool hasNextAreaDocument);
        Task<int> UpdateFromInputAsync(IEnumerable<int> ids, bool hasNextAreaDocument);
        Task<int> UpdateHasSalesInvoice(int id, bool hasSalesInvoice);
    }
}
