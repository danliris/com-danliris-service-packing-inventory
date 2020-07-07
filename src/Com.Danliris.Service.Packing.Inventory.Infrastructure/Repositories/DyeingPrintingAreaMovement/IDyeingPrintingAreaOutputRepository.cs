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
        Task<int> UpdateIMArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> UpdateAdjustmentData(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteIMArea(DyeingPrintingAreaOutputModel model);
        Task<int> DeleteTransitArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateTransitArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        Task<int> DeleteShippingArea(DyeingPrintingAreaOutputModel model);
        Task<int> UpdateShippingArea(int id, DyeingPrintingAreaOutputModel model, DyeingPrintingAreaOutputModel dbModel);
        IQueryable<DyeingPrintingAreaOutputModel> GetDbSet();
        IQueryable<DyeingPrintingAreaOutputModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument);
        Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument, List<int> idSpp);

        Task<int> UpdateFromInputNextAreaFlagParentOnlyAsync(int id, bool hasNextAreaDocument);

        Task<int> UpdateHasSalesInvoice(int id, bool hasSalesInvoice);
    }
}
