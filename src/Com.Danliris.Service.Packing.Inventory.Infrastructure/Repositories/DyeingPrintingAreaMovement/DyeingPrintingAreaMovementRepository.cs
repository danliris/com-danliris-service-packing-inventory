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
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementRepository : IDyeingPrintingAreaMovementRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DyeingPrintingAreaMovementModel> _dyeingPrintingAreaMovementDbSet;
        private readonly DbSet<DyeingPrintingAreaMovementHistoryModel> _historyDbSet;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<FabricQualityControlModel> _fqcDbSet;

        public DyeingPrintingAreaMovementRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dyeingPrintingAreaMovementDbSet = dbContext.Set<DyeingPrintingAreaMovementModel>();
            _historyDbSet = dbContext.Set<DyeingPrintingAreaMovementHistoryModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _fqcDbSet = dbContext.Set<FabricQualityControlModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dyeingPrintingAreaMovementDbSet.Include(s => s.DyeingPrintingAreaMovementHistories).FirstOrDefault(entity => entity.Id == id);

            var fqcData = _fqcDbSet.FirstOrDefault(s => s.DyeingPrintingAreaMovementId == id);

            if (fqcData != null)
                throw new Exception("Masih ada data di Pemeriksaan Kain");

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach(var item in model.DyeingPrintingAreaMovementHistories)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }
            _dyeingPrintingAreaMovementDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteFromTransitAsync(int id)
        {
            var histories = _historyDbSet.Where(s => s.DyeingPrintingAreaMovementId == id);
            var previousHistory = histories.FirstOrDefault(s => s.Index == AreaEnum.IM);
            var model = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(s => s.Id == id);
            model.SetShift(previousHistory.Shift, _identityProvider.Username, UserAgent);
            model.SetSourceArea(null, _identityProvider.Username, UserAgent);
            model.SetArea(previousHistory.Area, _identityProvider.Username, UserAgent);
            model.SetRemark(null, _identityProvider.Username, UserAgent);

            model.FlagForUpdate(_identityProvider.Username, UserAgent);
            var currentHistory = histories.FirstOrDefault(s => s.Index == AreaEnum.TRANSIT);
            currentHistory.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaMovementModel> GetDbSet()
        {
            return _dyeingPrintingAreaMovementDbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingAreaMovementModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var item in model.DyeingPrintingAreaMovementHistories)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }
            _dyeingPrintingAreaMovementDbSet.Add(model);
            
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertFromTransitAsync(int dyeingPrintingAreaMovementId, string shift, DateTimeOffset date, string area, string remark, DyeingPrintingAreaMovementHistoryModel history)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(s => s.Id == dyeingPrintingAreaMovementId);
            modelToUpdate.SetShift(shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSourceArea(modelToUpdate.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetArea(area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(remark, _identityProvider.Username, UserAgent);
           
            history.FlagForCreate(_identityProvider.Username, UserAgent);
            modelToUpdate.DyeingPrintingAreaMovementHistories.Add(history);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaMovementModel> ReadAll()
        {
            return _dyeingPrintingAreaMovementDbSet.Include(s => s.DyeingPrintingAreaMovementHistories).AsNoTracking();
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
            modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderCode, model.ProductionOrderNo,
                model.ProductionOrderType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetProductionOrderQuantity(model.ProductionOrderQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyer(model.Buyer, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPackingInstruction(model.PackingInstruction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartNumber(model.CartNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetConstructionData(model.MaterialId, model.MaterialCode, model.MaterialName, model.MaterialConstructionId,
                model.MaterialConstructionCode, model.MaterialConstructionName, model.MaterialWidth, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.UnitId, model.UnitCode, model.UnitName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMutation(model.Mutation, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLength(model.MeterLength, model.YardsLength, model.UOMUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetGrade(model.Grade, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetSourceArea(model.SourceArea, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromFabricQualityControlAsync(int id, string grade, bool isChecked)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetGrade(grade, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsChecked(isChecked, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateFromTransitAsync(int id, string shift, string remark)
        {
            var modelToUpdate = _dyeingPrintingAreaMovementDbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetShift(shift, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(remark, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
