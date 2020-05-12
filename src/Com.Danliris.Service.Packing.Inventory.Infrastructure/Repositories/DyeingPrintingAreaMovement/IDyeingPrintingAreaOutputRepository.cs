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
        IQueryable<DyeingPrintingAreaOutputModel> GetDbSet();
        IQueryable<DyeingPrintingAreaOutputModel> ReadAllIgnoreQueryFilter();
        Task<int> UpdateFromInputAsync(int id, bool hasNextAreaDocument);
        Task<int> UpdateFromInputNextAreaFlagParentOnlyAsync(int id, bool hasNextAreaDocument);
    }
}
