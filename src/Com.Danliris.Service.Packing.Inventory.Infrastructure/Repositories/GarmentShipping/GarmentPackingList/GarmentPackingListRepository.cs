using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListRepository : IGarmentPackingListRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentPackingListModel> _dbSet;

        public GarmentPackingListRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentPackingListModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach (var item in model.Items)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.FlagForDelete(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForDelete(_identityProvider.Username, UserAgent);
                    }
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentPackingListModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.FlagForCreate(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForCreate(_identityProvider.Username, UserAgent);
                    }
                }
            }
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentPackingListModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentPackingListModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentPackingListModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetPackingListType(model.PackingListType, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
