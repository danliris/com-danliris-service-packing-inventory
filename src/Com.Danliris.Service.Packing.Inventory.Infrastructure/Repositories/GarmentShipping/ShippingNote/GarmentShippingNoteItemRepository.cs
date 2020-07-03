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
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(GarmentShippingNoteItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GarmentShippingNoteItemModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, GarmentShippingNoteItemModel model)
        {
            throw new NotImplementedException();
        }

        IQueryable<GarmentShippingNoteItemModel> IRepository<GarmentShippingNoteItemModel>.ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
