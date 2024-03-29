﻿
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListDetailRepository : IGarmentReceiptSubconPackingListDetailRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<GarmentReceiptSubconPackingListDetailModel> _dbSet;
        private readonly IIdentityProvider _identityProvider;

        public GarmentReceiptSubconPackingListDetailRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentReceiptSubconPackingListDetailModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
               .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentReceiptSubconPackingListDetailModel model)
        {
            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<GarmentReceiptSubconPackingListDetailModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<GarmentReceiptSubconPackingListDetailModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }


        public Task<int> UpdateAsync(int id, GarmentReceiptSubconPackingListDetailModel model)
        {
            var modelToUpdate = _dbSet
              .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetLength(model.Length, _identityProvider.Username, USER_AGENT);

            return _dbContext.SaveChangesAsync();
        }
    }
}
