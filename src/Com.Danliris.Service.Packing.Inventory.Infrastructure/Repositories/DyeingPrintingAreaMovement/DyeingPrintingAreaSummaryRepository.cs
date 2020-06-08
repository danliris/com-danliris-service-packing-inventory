using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
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
    public class DyeingPrintingAreaSummaryRepository : IDyeingPrintingAreaSummaryRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DyeingPrintingAreaSummaryModel> _dbSet;

        public DyeingPrintingAreaSummaryRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DyeingPrintingAreaSummaryModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        //public Task<int> DeleteAreaIM(int bonId, int bonItemId, string type)
        //{
        //    var model = _dbSet.FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == bonId && s.DyeingPrintingAreaProductionOrderDocumentId == bonItemId && s.Type == type);
            
        //    if(model != null)
        //    {
        //        model.FlagForDelete(_identityProvider.Username, UserAgent);
        //        _dbSet.Update(model);
        //    }
            
        //    return _dbContext.SaveChangesAsync();
        //}

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaSummaryModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(DyeingPrintingAreaSummaryModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<DyeingPrintingAreaSummaryModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<DyeingPrintingAreaSummaryModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking();
        }

        public Task<DyeingPrintingAreaSummaryModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        //public Task<int> UpdateAreaIM(DyeingPrintingAreaSummaryModel model)
        //{
        //    var modelToUpdate = _dbSet.FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == model.DyeingPrintingAreaDocumentId && s.DyeingPrintingAreaProductionOrderDocumentId == model.DyeingPrintingAreaProductionOrderDocumentId && s.Type == model.Type);

        //    if (modelToUpdate == null)
        //    {
        //        model.FlagForCreate(_identityProvider.Username, UserAgent);
        //        _dbSet.Add(model);
        //    }
        //    else
        //    {
        //        modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
        //        modelToUpdate.SetCartNo(model.CartNo, _identityProvider.Username, UserAgent);
        //        modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
        //    }


        //    return _dbContext.SaveChangesAsync();
        //}

        public Task<int> UpdateAsync(int id, DyeingPrintingAreaSummaryModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetBalance(model.Balance, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyer(model.Buyer, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartNo(model.CartNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetColor(model.Color, _identityProvider.Username, UserAgent);
            modelToUpdate.SetConstruction(model.Construction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMotif(model.Motif, _identityProvider.Username, UserAgent);
            modelToUpdate.SetProductionOrder(model.ProductionOrderId, model.ProductionOrderNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.Unit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetArea(model.Area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDyeingPrintingAreaDocument(model.DyeingPrintingAreaDocumentId, model.DyeingPrintingAreaDocumentBonNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetType(model.Type, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDyeingPrintingAreaProductionOrderDocumentId(model.DyeingPrintingAreaProductionOrderDocumentId, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateToAvalAsync(DyeingPrintingAreaSummaryModel model, DateTimeOffset date, string area, string type)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == model.Id);
            modelToUpdate.SetDate(date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetArea(area, _identityProvider.Username, UserAgent);
            modelToUpdate.SetType(type, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
