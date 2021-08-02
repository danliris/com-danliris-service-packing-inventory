using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceAdjustmentRepository : IGarmentShippingInvoiceAdjustmentRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentShippingInvoiceAdjustmentModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingInvoiceAdjustmentRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingInvoiceAdjustmentModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingInvoiceAdjustmentModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentShippingInvoiceAdjustmentModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingInvoiceAdjustmentModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetAdjustmentDescription(model.AdjustmentDescription, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetAdjustmentValue(model.AdjustmentValue, _identityProvider.Username, USER_AGENT);
 
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingInvoiceAdjustmentModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
