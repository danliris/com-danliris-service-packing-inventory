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
    public class ProductPackingInventoryMovementRepository : IRepository<ProductPackingInventoryMovementModel>
    {
        private const string UserAgent = "inventory-packing-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public ProductPackingInventoryMovementRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.ProductPackingInventoryMovements.FirstOrDefault(entity => entity.Id == id);
            if (model != null)
            {
                EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
                _dbContext.ProductPackingInventoryMovements.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(ProductPackingInventoryMovementModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductPackingInventoryMovements.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public IQueryable<ProductPackingInventoryMovementModel> ReadAll()
        {
            return _dbContext.ProductPackingInventoryMovements;
        }

        public Task<ProductPackingInventoryMovementModel> ReadByIdAsync(int id)
        {
            return _dbContext.ProductPackingInventoryMovements.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, ProductPackingInventoryMovementModel model)
        {
            EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
            _dbContext.ProductPackingInventoryMovements.Update(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
