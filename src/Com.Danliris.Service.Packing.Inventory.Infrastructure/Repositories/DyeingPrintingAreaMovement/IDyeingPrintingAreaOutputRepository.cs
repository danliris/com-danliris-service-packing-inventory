using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaOutputRepository : IRepository<DyeingPrintingAreaOutputModel>
    {
        Task<int> UpdateAdjustmentData(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteAdjustment(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateAdjustmentDataAval(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteAdjustmentAval(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateIMArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteIMArea(DyeingPrintingAreaOutputModel model);
        Task<int> DeleteTransitArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateTransitArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeletePackingArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdatePackingArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteShippingArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateShippingArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteWarehouseArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateWarehouseArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> UpdateAvalArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteAvalArea(DyeingPrintingAreaOutputModel model);
        IQueryable<DyeingPrintingAreaOutputModel> GetDbSet();
        IQueryable<DyeingPrintingAreaOutputModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument);
        Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument, List<int> idSpp);

        Task<int> UpdateFromInputNextAreaFlagParentOnlyAsync(int id, bool hasNextAreaDocument);

        Task<int> UpdateHasSalesInvoice(int id, bool hasSalesInvoice);
    }
}
