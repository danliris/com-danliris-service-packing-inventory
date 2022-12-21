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
	public class GarmentShippingInvoiceItemRepository : IGarmentShippingInvoiceItemRepository
    {
		private const string USER_AGENT = "Repository";
		private readonly PackingInventoryDbContext _dbContext;
		private readonly DbSet<GarmentShippingInvoiceItemModel> _dbSet;
		private readonly IIdentityProvider _identityProvider;

        public GarmentShippingInvoiceItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingInvoiceItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingInvoiceItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentShippingInvoiceItemModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingInvoiceItemModel model)
        {
            var modelToUpdate = _dbSet
                 .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetCMTPrice(model.CMTPrice, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetComodityDesc(model.ComodityDesc, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetMarketingName(model.MarketingName, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetPrice(model.Price, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetDesc2(model.Desc2, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetDesc3(model.Desc3, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetDesc4(model.Desc4, _identityProvider.Username, USER_AGENT);


            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingInvoiceItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
