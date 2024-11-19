using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD
{
    public class GarmentMDLocalSalesContractRepository : IGarmentMDLocalSalesContractRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentMDLocalSalesContractModel> _dbSet;

        public GarmentMDLocalSalesContractRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentMDLocalSalesContractModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .FirstOrDefault(s => s.Id == id);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentMDLocalSalesContractModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentMDLocalSalesContractModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentMDLocalSalesContractModel> ReadByIdAsync(int id)
        {
            return _dbSet
              .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentMDLocalSalesContractModel model)
        {
            throw new NotImplementedException();
        }
    }
}
