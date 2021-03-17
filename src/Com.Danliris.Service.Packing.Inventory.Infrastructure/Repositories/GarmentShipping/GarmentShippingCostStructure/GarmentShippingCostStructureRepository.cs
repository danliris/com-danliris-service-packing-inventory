using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureRepository : IGarmentShippingCostStructureRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingCostStructureModel> _dbSet;

        public GarmentShippingCostStructureRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingCostStructureModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public IQueryable<GarmentShippingCostStructureModel> Query => _dbSet.AsQueryable();

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingCostStructureModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingCostStructureModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingCostStructureModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<GarmentShippingCostStructureModel> ReadByInvoiceNo(string no)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.InvoiceNo == no);
        }

        public Task<int> SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(int id, GarmentShippingCostStructureModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetComodityId(model.ComodityId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetComodityCode(model.ComodityCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetComodityName(model.ComodityName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetHsCode(model.HsCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestination(model.Destination, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFabricType(model.FabricType, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
