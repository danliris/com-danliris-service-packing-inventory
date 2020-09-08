using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaSummaryRepository : IRepository<DyeingPrintingAreaSummaryModel>
    {
        //Task<int> UpdateAreaIM(DyeingPrintingAreaSummaryModel model);
        //Task<int> DeleteAreaIM(int bonId, int bonItemId, string type);
        IQueryable<DyeingPrintingAreaSummaryModel> GetDbSet();
        IQueryable<DyeingPrintingAreaSummaryModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateToAvalAsync(DyeingPrintingAreaSummaryModel model, DateTimeOffset date, string area, string type);
    }
}
