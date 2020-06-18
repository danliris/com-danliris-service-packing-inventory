using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDORepository : IGarmentShippingExportSalesDORepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingExportSalesDOModel> _dbSet;

        public GarmentShippingExportSalesDORepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbSet = dbContext.Set<GarmentShippingExportSalesDOModel>(); ;
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

        public Task<int> InsertAsync(GarmentShippingExportSalesDOModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingExportSalesDOModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingExportSalesDOModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingExportSalesDOModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTo(model.To, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnitCode(model.UnitCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnitId(model.UnitId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnitName(model.UnitName, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetGrossWeight(item.GrossWeight, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetNettWeight(item.NettWeight, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetCartonQuantity(item.CartonQuantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityId(item.ComodityId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityCode(item.ComodityCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityName(item.ComodityName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetDescription(item.Description, _identityProvider.Username, UserAgent);

                }
                else
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {
                modelToUpdate.Items.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
