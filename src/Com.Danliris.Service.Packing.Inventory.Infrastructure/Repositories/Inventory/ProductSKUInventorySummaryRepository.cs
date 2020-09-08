using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory
{
    public class ProductSKUInventorySummaryRepository : IRepository<ProductSKUInventorySummaryModel>
    {
        private const string UserAgent = "inventory-packing-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public ProductSKUInventorySummaryRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.ProductSKUInventorySummaries.FirstOrDefault(entity => entity.Id == id);
            if (model != null)
            {
                EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
                _dbContext.ProductSKUInventorySummaries.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(ProductSKUInventorySummaryModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductSKUInventorySummaries.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public IQueryable<ProductSKUInventorySummaryModel> ReadAll()
        {
            return _dbContext.ProductSKUInventorySummaries;
        }

        public Task<ProductSKUInventorySummaryModel> ReadByIdAsync(int id)
        {
            return _dbContext.ProductSKUInventorySummaries.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, ProductSKUInventorySummaryModel model)
        {
            EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductSKUInventorySummaries.Update(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
