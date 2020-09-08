using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory
{
    public class GreigeProductSKURepository : IRepository<GreigeProductSKUModel>
    {
        private const string UserAgent = "inventory-packing-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public GreigeProductSKURepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.GreigeProductSKUs.FirstOrDefault(entity => entity.Id == id);
            if (model != null)
            {
                EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
                _dbContext.GreigeProductSKUs.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(GreigeProductSKUModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);
            _dbContext.GreigeProductSKUs.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public IQueryable<GreigeProductSKUModel> ReadAll()
        {
            return _dbContext.GreigeProductSKUs;
        }

        public Task<GreigeProductSKUModel> ReadByIdAsync(int id)
        {
            return _dbContext.GreigeProductSKUs.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, GreigeProductSKUModel model)
        {
            EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
            _dbContext.GreigeProductSKUs.Update(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
