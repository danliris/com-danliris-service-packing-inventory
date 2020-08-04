using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteItemRepository : IGarmentShippingLocalSalesNoteItemRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentShippingLocalSalesNoteItemModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentShippingLocalSalesNoteItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLocalSalesNoteItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLocalSalesNoteItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentShippingLocalSalesNoteItemModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingLocalSalesNoteItemModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetProductId(model.ProductId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetProductCode(model.ProductCode, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetProductName(model.ProductName, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPrice(model.Price, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPackageQuantity(model.PackageQuantity, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPackageUomId(model.PackageUomId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPackageUomUnit(model.PackageUomUnit, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalSalesNoteItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
