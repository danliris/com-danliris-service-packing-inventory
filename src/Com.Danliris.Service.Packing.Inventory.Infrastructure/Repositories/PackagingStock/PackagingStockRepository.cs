using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.PackagingStock
{
    public class PackagingStockRepository : IPackagingStockRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<PackagingStockModel> _packagingStockDbSet;
        private readonly IIdentityProvider _identityProvider;

        public PackagingStockRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _packagingStockDbSet = dbContext.Set<PackagingStockModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _packagingStockDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _packagingStockDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(PackagingStockModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _packagingStockDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<PackagingStockModel> ReadAll()
        {
            return _packagingStockDbSet.AsNoTracking();
        }

        public Task<PackagingStockModel> ReadByIdAsync(int id)
        {
            return _packagingStockDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, PackagingStockModel model)
        {
            var modelToUpdate = _packagingStockDbSet.FirstOrDefault(entity => entity.Id == id);

            EntityExtension.FlagForUpdate(model, _identityProvider.Username, USER_AGENT);
            _packagingStockDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }
    }
}
