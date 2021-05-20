using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote
{
    public class GarmentShippingNoteRepository : IGarmentShippingNoteRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingNoteModel> _dbSet;

        public GarmentShippingNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingNoteModel>();
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

        public Task<int> InsertAsync(GarmentShippingNoteModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingNoteModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingNoteModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingNoteModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetReceiptDate(model.ReceiptDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalAmount(model.TotalAmount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, UserAgent);
            modelToUpdate.SetReceiptNo(model.ReceiptNo, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(m => m.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetDescription(item.Description, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetCurrencyId(item.CurrencyId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetCurrencyCode(item.CurrencyCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, UserAgent);
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
