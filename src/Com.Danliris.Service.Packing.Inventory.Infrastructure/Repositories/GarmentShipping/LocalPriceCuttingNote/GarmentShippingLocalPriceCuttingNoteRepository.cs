using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteRepository : IGarmentShippingLocalPriceCuttingNoteRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLocalPriceCuttingNoteModel> _dbSet;
        private readonly DbSet<GarmentShippingLocalPriceCuttingNoteItemModel> _dbSetItem;

        public GarmentShippingLocalPriceCuttingNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLocalPriceCuttingNoteModel>();
            _dbSetItem = dbContext.Set<GarmentShippingLocalPriceCuttingNoteItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLocalPriceCuttingNoteModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalPriceCuttingNoteModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<GarmentShippingLocalPriceCuttingNoteItemModel> ReadItemAll()
        {
            return _dbSetItem.AsNoTracking();
        }

        public Task<GarmentShippingLocalPriceCuttingNoteModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingLocalPriceCuttingNoteModel model)
        {
            return Task.FromResult(1);
        }
    }
}
