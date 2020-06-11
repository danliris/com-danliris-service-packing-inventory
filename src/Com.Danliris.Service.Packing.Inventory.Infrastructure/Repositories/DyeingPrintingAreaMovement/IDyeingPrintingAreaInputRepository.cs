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
    public interface IDyeingPrintingAreaInputRepository : IRepository<DyeingPrintingAreaInputModel>
    {
        Task<int> UpdateIMArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel);
        Task<int> DeleteIMArea(DyeingPrintingAreaInputModel model);
        Task<int> DeleteTransitArea(DyeingPrintingAreaInputModel model);
        Task<int> UpdateTransitArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel);
        Task<int> DeleteShippingArea(DyeingPrintingAreaInputModel model);
        Task<int> UpdateShippingArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel);
        Task<int> DeleteAvalTransformationArea(DyeingPrintingAreaInputModel model);
        Task<int> UpdateAvalTransformationArea(int id, DyeingPrintingAreaInputModel model, DyeingPrintingAreaInputModel dbModel);
        IQueryable<DyeingPrintingAreaInputModel> GetDbSet();
        IQueryable<DyeingPrintingAreaInputModel> ReadAllIgnoreQueryFilter();
    }
}
