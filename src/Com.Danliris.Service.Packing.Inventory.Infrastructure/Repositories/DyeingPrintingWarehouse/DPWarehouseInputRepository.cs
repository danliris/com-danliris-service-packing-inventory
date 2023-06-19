using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingWarehouse
{
    public class DPWarehouseInputRepository : IDPWarehouseInputRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DPWarehouseInputModel> _dbSet;

        public DPWarehouseInputRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DPWarehouseInputModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<DPWarehouseInputModel> GetDbSet()
        {
            return _dbSet;
        }
        public IQueryable<DPWarehouseInputModel> ReadAll()
        {
            return _dbSet.AsNoTracking();

        }

        public IQueryable<DPWarehouseInputModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking();

        }

        public Task<DPWarehouseInputModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> InsertAsync(DPWarehouseInputModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, DPWarehouseInputModel model)
        {
            throw new NotImplementedException();
        }

    }
}
