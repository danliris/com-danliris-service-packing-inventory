using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<CriteriaModel> _criteriaDbSet;
        private readonly IIdentityProvider _identityProvider;

        public CriteriaRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _criteriaDbSet = dbContext.Set<CriteriaModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _criteriaDbSet.FirstOrDefault(entity => entity.Id == id);

            _criteriaDbSet.Remove(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<CriteriaModel> GetDbSet()
        {
            return _criteriaDbSet;
        }

        public Task<int> InsertAsync(CriteriaModel model)
        {
            _criteriaDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<CriteriaModel> ReadAll()
        {
            return _criteriaDbSet.AsNoTracking();
        }

        public IQueryable<CriteriaModel> ReadAllIgnoreQueryFilter()
        {
            return _criteriaDbSet.IgnoreQueryFilters().AsNoTracking();
        }

        public Task<CriteriaModel> ReadByIdAsync(int id)
        {
            return _criteriaDbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, CriteriaModel model)
        {
            var modelToUpdate = _criteriaDbSet.FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetCode(model.Code);
            modelToUpdate.SetGroup(model.Group);
            modelToUpdate.SetIndex(model.Index);
            modelToUpdate.SetName(model.Name);
            modelToUpdate.SetScoreA(model.ScoreA);
            modelToUpdate.SetScoreB(model.ScoreB);
            modelToUpdate.SetScoreC(model.ScoreC);
            modelToUpdate.SetScoreD(model.ScoreD);

            return _dbContext.SaveChangesAsync();
        }
    }
}
