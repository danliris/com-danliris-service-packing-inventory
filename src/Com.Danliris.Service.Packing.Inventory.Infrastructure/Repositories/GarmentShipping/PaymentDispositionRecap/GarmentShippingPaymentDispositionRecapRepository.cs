using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapRepository : IGarmentShippingPaymentDispositionRecapRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingPaymentDispositionRecapModel> _dbSet;
        private readonly DbSet<GarmentShippingPaymentDispositionRecapItemModel> _dbSetItem;
        private readonly DbSet<GarmentShippingPaymentDispositionModel> _garmentShippingPaymentDispositionDbSet;
        public GarmentShippingPaymentDispositionRecapRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingPaymentDispositionRecapModel>();
            _dbSetItem = dbContext.Set<GarmentShippingPaymentDispositionRecapItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _garmentShippingPaymentDispositionDbSet = dbContext.Set<GarmentShippingPaymentDispositionModel>();
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

        public Task<int> InsertAsync(GarmentShippingPaymentDispositionRecapModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                var paymentDisposition = _garmentShippingPaymentDispositionDbSet.FirstOrDefault(entity => entity.Id == item.PaymentDispositionId);
                if (paymentDisposition != null)
                {
                    paymentDisposition.SetIncomeTaxValue(item.PaymentDisposition.IncomeTaxValue, _identityProvider.Username, UserAgent);
                }
            }

            _dbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingPaymentDispositionRecapModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<GarmentShippingPaymentDispositionRecapItemModel> ReadItemAll()
        {
            return _dbSetItem.AsNoTracking();
        }

        public Task<GarmentShippingPaymentDispositionRecapModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingPaymentDispositionRecapModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.FlagForUpdate(_identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item == null)
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
                else
                {
                    itemToUpdate.SetService(item.Service, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetOthersPayment(item.OthersPayment, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetTruckingPayment(item.TruckingPayment, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetVatService(item.VatService, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetAmountService(item.AmountService, _identityProvider.Username, UserAgent);
                    
                    var paymentDisposition = _garmentShippingPaymentDispositionDbSet.FirstOrDefault(entity => entity.Id == itemToUpdate.PaymentDispositionId);
                    if(paymentDisposition != null)
                    {
                        paymentDisposition.SetIncomeTaxValue(item.PaymentDisposition.IncomeTaxValue, _identityProvider.Username, UserAgent);
                    }
                    itemToUpdate.FlagForUpdate(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);

                modelToUpdate.Items.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
