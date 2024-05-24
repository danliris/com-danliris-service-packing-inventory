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
    public class DPWarehouseOutputRepository : IDPWarehouseOutputRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<DPWarehouseOutputModel> _dbSet;
        public DPWarehouseOutputRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DPWarehouseOutputModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<DPWarehouseOutputModel> GetDbSet()
        {
            return _dbSet;
        }
        public IQueryable<DPWarehouseOutputModel> ReadAll()
        {
            return _dbSet.AsNoTracking();

        }

        public IQueryable<DPWarehouseOutputModel> ReadAllIgnoreQueryFilter()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking();

        }

        public Task<DPWarehouseOutputModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(s => s.DPWarehouseOutputItems).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> InsertAsync(DPWarehouseOutputModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, DPWarehouseOutputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
