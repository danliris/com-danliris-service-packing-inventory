using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.AcceptingPackaging
{
    public interface IAcceptingPackagingRepository :IRepository<AcceptingPackagingModel>
    {
        AcceptingPackagingModel ReadByDyeingPrintingAreaMovement(int dyeingPrintingAreaMovementId);
        AcceptingPackagingModel ReadByBonNo(string bonNo);
        Task<int> InsertAsync(int idDyeingMovementArea, AcceptingPackagingModel data);
        Task<int> InsertAsync(string BonNoDyeingMovementArea, AcceptingPackagingModel data);
        //Task<int> UpdateAsync(int dyeingPrintingAreaMovementId, AcceptingPackagingModel data);
        Task<int> UpdateAsync(string BonNoDyeingPrintingAreaMovement, AcceptingPackagingModel data);
        //Task<int> DeleteAsync(int dyeingPrintingAreaMovementId);
        //IQueryable<AcceptingPackagingModel> ReadAll();
    }
}
