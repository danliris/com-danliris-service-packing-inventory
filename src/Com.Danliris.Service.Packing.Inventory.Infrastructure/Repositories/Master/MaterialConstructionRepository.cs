using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master
{
    public class MaterialConstructionRepository : IMaterialConstructionRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<MaterialConstructionModel> _dbSet;

        public MaterialConstructionRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<MaterialConstructionModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<MaterialConstructionModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(MaterialConstructionModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> MultipleInsertAsync(IEnumerable<MaterialConstructionModel> models)
        {
            foreach (var model in models)
            {
                model.FlagForCreate(_identityProvider.Username, UserAgent);
                _dbSet.Add(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<MaterialConstructionModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<MaterialConstructionModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, MaterialConstructionModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetType(model.Type, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCode(model.Code, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
