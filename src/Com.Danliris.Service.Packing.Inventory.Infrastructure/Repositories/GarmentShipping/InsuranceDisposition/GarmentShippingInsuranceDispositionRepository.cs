using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionRepository : IGarmentShippingInsuranceDispositionRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingInsuranceDispositionModel> _dbSet;
        private readonly DbSet<GarmentShippingInsuranceDispositionItemModel> _dbSetItem;

        public GarmentShippingInsuranceDispositionRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingInsuranceDispositionModel>();
            _dbSetItem = dbContext.Set<GarmentShippingInsuranceDispositionItemModel>();
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

        public Task<int> InsertAsync(GarmentShippingInsuranceDispositionModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingInsuranceDispositionModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<GarmentShippingInsuranceDispositionItemModel> ReadItemAll()
        {
            return _dbSetItem.AsNoTracking();
        }

        public Task<GarmentShippingInsuranceDispositionModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingInsuranceDispositionModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetBankName(model.BankName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInsuranceCode(model.InsuranceCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInsuranceId(model.InsuranceId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInsuranceName(model.InsuranceName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentDate(model.PaymentDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRate(model.Rate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetCurrencyRate(item.CurrencyRate, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPolicyDate(item.PolicyDate, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPolicyNo(item.PolicyNo, _identityProvider.Username, UserAgent);
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
