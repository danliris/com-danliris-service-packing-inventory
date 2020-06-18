using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product
{
    public class ProductSKURepository : IRepository<ProductSKUModel>
    {
        private const string UserAgent = "inventory-packing-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public ProductSKURepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.ProductSKUs.FirstOrDefault(entity => entity.Id == id);
            if (model != null)
            {
                EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
                _dbContext.ProductSKUs.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(ProductSKUModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductSKUs.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public IQueryable<ProductSKUModel> ReadAll()
        {
            return _dbContext.ProductSKUs;
        }

        public Task<ProductSKUModel> ReadByIdAsync(int id)
        {
            return _dbContext.ProductSKUs.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, ProductSKUModel model)
        {
            EntityExtension.FlagForUpdate(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductSKUs.Update(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
