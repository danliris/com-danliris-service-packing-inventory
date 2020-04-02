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
    public class DyeingPrintingAreaMovementRepository : IDyeingPrintingAreaMovementRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DyeingPrintingAreaMovementModel> _dyeingPrintingAreaMovementDbSet;
        private readonly IIdentityProvider _identityProvider;

        public DyeingPrintingAreaMovementRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dyeingPrintingAreaMovementDbSet = dbContext.Set<DyeingPrintingAreaMovementModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(entity => entity.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dyeingPrintingAreaMovementDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(DyeingPrintingAreaMovementModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dyeingPrintingAreaMovementDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaMovementModel> ReadAll()
        {
            return _dyeingPrintingAreaMovementDbSet.AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaMovementModel> ReadAllIgnoreQueryFilter()
        {
            return _dyeingPrintingAreaMovementDbSet.IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaMovementModel> ReadByIdAsync(int id)
        {
            return _dyeingPrintingAreaMovementDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaMovementModel model)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShift(model.Shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderCode, model.ProductionOrderNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartNumber(model.CartNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetConstructionData(model.MaterialId, model.MaterialCode, model.MaterialName, model.MaterialConstructionId,
                model.MaterialConstructionCode, model.MaterialConstructionName, model.MaterialWidth, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.UnitId, model.UnitCode, model.UnitName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMutation(model.Mutation, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLength(model.MeterLength, model.YardsLength, model.UOMUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGrade(model.Grade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSourceArea(model.SourceArea, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }
    }
}
