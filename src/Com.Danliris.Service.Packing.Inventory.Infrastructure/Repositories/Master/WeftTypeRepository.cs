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
    public class WeftTypeRepository : IWeftTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<WeftTypeModel> _dbSet;

        public WeftTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<WeftTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }


        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<WeftTypeModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(WeftTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> MultipleInsertAsync(IEnumerable<WeftTypeModel> models)
        {
            foreach(var model in models)
            {
                model.FlagForCreate(_identityProvider.Username, UserAgent);
                _dbSet.Add(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<WeftTypeModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<WeftTypeModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, WeftTypeModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetType(model.Type, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCode(model.Code, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
