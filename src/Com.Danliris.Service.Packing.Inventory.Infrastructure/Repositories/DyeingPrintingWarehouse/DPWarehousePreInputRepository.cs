using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse
{
    public class DPWarehousePreInputRepository : IDPWarehousePreInputRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DPWarehousePreInputModel> _dbSet;

        public DPWarehousePreInputRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DPWarehousePreInputModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<DPWarehousePreInputModel> GetDbSet()
        {
            return _dbSet;
        }
        public IQueryable<DPWarehousePreInputModel> ReadAll()
        {
            return _dbSet.AsNoTracking();

        }

        public IQueryable<DPWarehousePreInputModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking();

        }

        public Task<DPWarehousePreInputModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> InsertAsync(DPWarehousePreInputModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalance(int id, double balance)
        {



            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalance = modelToUpdate.Balance + balance;
                modelToUpdate.SetBalance(newBalance, _identityProvider.Username, UserAgent);

            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateBalanceRemainsIn(int id, double balanceRemains)
        {



            var modelToUpdate = _dbSet.FirstOrDefault(entity => entity.Id == id);

            if (modelToUpdate != null)
            {
                var newBalanceRemains = modelToUpdate.BalanceRemains + balanceRemains;
                modelToUpdate.SetBalanceRemains(newBalanceRemains, _identityProvider.Username, UserAgent);

            }

            return _dbContext.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, DPWarehousePreInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
