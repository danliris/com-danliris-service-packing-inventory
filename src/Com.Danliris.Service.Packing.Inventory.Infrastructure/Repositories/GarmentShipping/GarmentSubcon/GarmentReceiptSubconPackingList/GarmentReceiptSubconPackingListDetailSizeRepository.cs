using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListDetailSizeRepository : IGarmentReceiptSubconPackingListDetailSizeRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentReceiptSubconPackingListDetailSizeModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentReceiptSubconPackingListDetailSizeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentReceiptSubconPackingListDetailSizeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentReceiptSubconPackingListDetailSizeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentReceiptSubconPackingListDetailSizeModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<GarmentReceiptSubconPackingListDetailSizeModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }


        public Task<int> UpdateAsync(int id, GarmentReceiptSubconPackingListDetailSizeModel model)
        {
            var modelToUpdate = _dbSet
               .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetSizeIdx(model.SizeIdx, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }
    }
}
