using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote
{
	public class GarmentShippingNoteItemRepository : IGarmentShippingNoteItemRepository
	{
		private const string USER_AGENT = "Repository";
		private readonly PackingInventoryDbContext _dbContext;
		private readonly DbSet<GarmentShippingNoteItemModel> _dbSet;
		private readonly IIdentityProvider _identityProvider;

        public GarmentShippingNoteItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingNoteItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingNoteItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentShippingNoteItemModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingNoteItemModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetCurrencyId(model.CurrencyId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetCurrencyCode(model.CurrencyCode, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingNoteItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
