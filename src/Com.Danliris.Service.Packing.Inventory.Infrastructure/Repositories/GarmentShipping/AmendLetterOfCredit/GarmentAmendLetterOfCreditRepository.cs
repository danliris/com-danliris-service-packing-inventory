using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditRepository : IGarmentAmendLetterOfCreditRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingAmendLetterOfCreditModel> _dbSet;

        public GarmentAmendLetterOfCreditRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingAmendLetterOfCreditModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingAmendLetterOfCreditModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingAmendLetterOfCreditModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingAmendLetterOfCreditModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingAmendLetterOfCreditModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
