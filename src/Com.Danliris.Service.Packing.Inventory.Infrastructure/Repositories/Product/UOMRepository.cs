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
    public class UOMRepository : IRepository<UnitOfMeasurementModel>
    {
        private const string UserAgent = "inventory-packing-service";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public UOMRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.IPUnitOfMeasurements.FirstOrDefault(entity => entity.Id == id);
            if (model != null)
            {
                EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
                _dbContext.IPUnitOfMeasurements.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(UnitOfMeasurementModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);
            _dbContext.IPUnitOfMeasurements.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }

        public IQueryable<UnitOfMeasurementModel> ReadAll()
        {
            return _dbContext.IPUnitOfMeasurements;
        }

        public Task<UnitOfMeasurementModel> ReadByIdAsync(int id)
        {
            return _dbContext.IPUnitOfMeasurements.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, UnitOfMeasurementModel model)
        {
            EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);
            _dbContext.IPUnitOfMeasurements.Update(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
