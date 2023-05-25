using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailShippingLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemRepository : IGarmentShippingDetailLocalSalesNoteItemRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentShippingDetailLocalSalesNoteItemModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingDetailLocalSalesNoteItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingDetailLocalSalesNoteItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingDetailLocalSalesNoteItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentShippingDetailLocalSalesNoteItemModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingDetailLocalSalesNoteItemModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetUnitId(model.UnitId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUnitCode(model.UnitCode, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUnitName(model.UnitName, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingDetailLocalSalesNoteItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
