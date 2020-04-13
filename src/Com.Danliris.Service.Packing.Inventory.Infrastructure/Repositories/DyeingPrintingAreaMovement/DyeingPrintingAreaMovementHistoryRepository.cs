using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementHistoryRepository : IDyeingPrintingAreaMovementHistoryRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DyeingPrintingAreaMovementHistoryModel> _dyeingPrintingAreaMovementHistoryDbSet;
        private readonly IIdentityProvider _identityProvider;

        public DyeingPrintingAreaMovementHistoryRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dyeingPrintingAreaMovementHistoryDbSet = dbContext.Set<DyeingPrintingAreaMovementHistoryModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dyeingPrintingAreaMovementHistoryDbSet.FirstOrDefault(entity => entity.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dyeingPrintingAreaMovementHistoryDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(DyeingPrintingAreaMovementHistoryModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaMovementHistoryModel> ReadAll()
        {
            return _dyeingPrintingAreaMovementHistoryDbSet.AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaMovementHistoryModel> ReadByDyeingPrintintAreaMovement(int dyeingPrintingAreaMovementId)
        {
            return _dyeingPrintingAreaMovementHistoryDbSet.AsNoTracking().Where(s => s.DyeingPrintingAreaMovementId == dyeingPrintingAreaMovementId);
        }

        public Task<DyeingPrintingAreaMovementHistoryModel> ReadByIdAsync(int id)
        {
            return _dyeingPrintingAreaMovementHistoryDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaMovementHistoryModel model)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementHistoryDbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }


        public Task<int> UpdateAsyncFromParent(int dyeingPrintingAreaMovementId, AreaEnum index, DateTimeOffset newDate, string shift)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementHistoryDbSet.FirstOrDefault(s => s.DyeingPrintingAreaMovementId == dyeingPrintingAreaMovementId && s.Index == index);
            modelToUpdate.SetDate(newDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShift(shift, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }
    }
}
