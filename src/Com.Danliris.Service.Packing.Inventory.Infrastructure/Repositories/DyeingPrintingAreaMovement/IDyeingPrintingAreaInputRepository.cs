using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
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
        Task<int> UpdateHeaderAvalTransform(DyeingPrintingAreaInputModel model, double avalQuantity, double weightQuantity);
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
        Task<Tuple<int,List<AvalData>>> UpdateAvalTransformationFromOut(string avalType, double avalQuantity, double weightQuantity);
        Task<int> RestoreAvalTransformation(List<AvalData> avalData);
    }
}
