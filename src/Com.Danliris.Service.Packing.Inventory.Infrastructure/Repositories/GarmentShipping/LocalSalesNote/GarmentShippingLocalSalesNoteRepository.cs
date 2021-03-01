﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteRepository : IGarmentShippingLocalSalesNoteRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLocalSalesNoteModel> _dbSet;
        private readonly DbSet<GarmentShippingLocalSalesContractItemModel> _salesContractDbSet;

        public GarmentShippingLocalSalesNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLocalSalesNoteModel>();
            _salesContractDbSet= dbContext.Set<GarmentShippingLocalSalesContractItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            //var sc= _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);

            //sc.SetIsUsed(false, _identityProvider.Username, UserAgent);
            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == item.LocalSalesContractItemId);
                sc.SetRemainingQuantity(sc.RemainingQuantity + item.Quantity, _identityProvider.Username, UserAgent);

                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLocalSalesNoteModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            //var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
            //sc.SetIsUsed(true, _identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == item.LocalSalesContractItemId);
                sc.SetRemainingQuantity(sc.RemainingQuantity - item.Quantity, _identityProvider.Username, UserAgent);

                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalSalesNoteModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingLocalSalesNoteModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingLocalSalesNoteModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTempo(model.Tempo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExpenditureNo(model.ExpenditureNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDispositionNo(model.DispositionNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUseVat(model.UseVat, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentType(model.PaymentType, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(m => m.Id == itemToUpdate.Id);
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == item.LocalSalesContractItemId);
                

                if (item != null)
                {

                    var qty = sc.RemainingQuantity + itemToUpdate.Quantity - item.Quantity;
                    sc.SetRemainingQuantity(qty, _identityProvider.Username, UserAgent);

                    itemToUpdate.SetProductId(item.ProductId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetProductCode(item.ProductCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetProductName(item.ProductName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPrice(item.Price, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPackageUomUnit(item.PackageUomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPackageUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPackageQuantity(item.PackageQuantity, _identityProvider.Username, UserAgent);

                }
                else
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);

                    sc.SetRemainingQuantity(sc.RemainingQuantity + itemToUpdate.Quantity, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == item.LocalSalesContractItemId);
                sc.SetRemainingQuantity(sc.RemainingQuantity - item.Quantity, _identityProvider.Username, UserAgent);

                modelToUpdate.Items.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
