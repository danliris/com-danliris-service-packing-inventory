using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product.Packing
{
    public class PackingRepository : IPackingRepository
    {
        private const string UserAgent = "PackingInventoryService";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<PackingModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public PackingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<PackingModel>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var modelToDelete = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToDelete != null)
            {
                EntityExtension.FlagForDelete(modelToDelete, _identityProvider.Username, UserAgent);
                _dbSet.Update(modelToDelete);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(PackingModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<PackingModel> ReadAll()
        {
            return _dbSet;
        }

        public Task<PackingModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, PackingModel model)
        {
            if (_dbSet.Any(entity => entity.Id == id))
            {
                EntityExtension.FlagForUpdate(model, _identityProvider.Username, UserAgent);
                _dbSet.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
