using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD.LocalSalesNote
{
    public class GarmentMDLocalSalesNoteItemRepository : IGarmentMDLocalSalesNoteItemRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentMDLocalSalesNoteItemModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentMDLocalSalesNoteItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentMDLocalSalesNoteItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentMDLocalSalesNoteItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentMDLocalSalesNoteItemModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentMDLocalSalesNoteItemModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);


            modelToUpdate.SetComodityName(model.ComodityName, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPrice(model.Price, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentMDLocalSalesNoteItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
