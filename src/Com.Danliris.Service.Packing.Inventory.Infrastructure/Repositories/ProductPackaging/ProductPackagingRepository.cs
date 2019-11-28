using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPackaging
{
    public class ProductPackagingRepository : IProductPackagingRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ProductPackagingModel> _productPackagingDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ProductPackagingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _productPackagingDbSet = dbContext.Set<ProductPackagingModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _productPackagingDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _productPackagingDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ProductPackagingModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
             _productPackagingDbSet.Add(model);
             return _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProductPackagingModel> ReadAll()
        {
            return _productPackagingDbSet;
        }

        public Task<ProductPackagingModel> ReadByIdAsync(int id)
        {
            return _productPackagingDbSet.Where(entity => entity.Id == id).FirstAsync();
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, ProductPackagingModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}