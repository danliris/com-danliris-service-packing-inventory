﻿using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDOTS;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentSubcon.ShippingLocalSalesNoteTS;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDOTS
{
    public class GarmentShippingLocalSalesDOTSRepository : IGarmentShippingLocalSalesDOTSRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLocalSalesDOTSModel> _dbSet;
        private readonly DbSet<GarmentShippingLocalSalesNoteTSModel> _salesNoteDbSet;

        public GarmentShippingLocalSalesDOTSRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbSet = dbContext.Set<GarmentShippingLocalSalesDOTSModel>(); 
            _salesNoteDbSet= dbContext.Set<GarmentShippingLocalSalesNoteTSModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);
            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsDO(false, _identityProvider.Username, UserAgent);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLocalSalesDOTSModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsDO(true, _identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalSalesDOTSModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingLocalSalesDOTSModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingLocalSalesDOTSModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTo(model.To, _identityProvider.Username, UserAgent);
            modelToUpdate.SetStorageDivision(model.StorageDivision, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            
            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetGrossWeight(item.GrossWeight, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetNettWeight(item.NettWeight, _identityProvider.Username, UserAgent);
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
