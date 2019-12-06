using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking
{
    public class ProductPackingRepository : IProductPackingRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ProductPackingModel> _productPackingDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ProductPackingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _productPackingDbSet = dbContext.Set<ProductPackingModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _productPackingDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _productPackingDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ProductPackingModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate(6);
            } while (_productPackingDbSet.Any(entity => entity.Code == model.Code));
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _productPackingDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProductPackingModel> ReadAll()
        {
            return _productPackingDbSet.AsNoTracking();
        }

        public Task<ProductPackingModel> ReadByIdAsync(int id)
        {
            return _productPackingDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, ProductPackingModel model)
        {
            throw new NotImplementedException();
        }
    }
}