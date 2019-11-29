using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU
{
    public class ProductSKURepository : IProductSKURepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ProductSKUModel> _productSKUDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ProductSKURepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _productSKUDbSet = dbContext.Set<ProductSKUModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _productSKUDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _productSKUDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ProductSKUModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _productSKUDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProductSKUModel> ReadAll()
        {
            return _productSKUDbSet;
        }

        public Task<ProductSKUModel> ReadByIdAsync(int id)
        {
            return _productSKUDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, ProductSKUModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}