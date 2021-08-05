using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceRepository : IGarmentDebiturBalanceRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentDebiturBalanceModel> _dbSet;

        public GarmentDebiturBalanceRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentDebiturBalanceModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentDebiturBalanceModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentDebiturBalanceModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentDebiturBalanceModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentDebiturBalanceModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetBalanceDate(model.BalanceDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentId(model.BuyerAgentId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentCode(model.BuyerAgentCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentName(model.BuyerAgentName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBalanceAmount(model.BalanceAmount, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
