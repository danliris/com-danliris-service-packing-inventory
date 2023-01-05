using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractRepository : IGarmentShippingLocalSalesContractRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLocalSalesContractModel> _dbSet;

        public GarmentShippingLocalSalesContractRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLocalSalesContractModel>();
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

        public Task<int> InsertAsync(GarmentShippingLocalSalesContractModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalSalesContractModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingLocalSalesContractModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingLocalSalesContractModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetBuyerAddress(model.BuyerAddress, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerCode(model.BuyerCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerId(model.BuyerId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerName(model.BuyerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerNPWP(model.BuyerNPWP, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsUsed(model.IsUsed, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsUseVat(model.IsUseVat, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerAddress(model.SellerAddress, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerName(model.SellerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerNPWP(model.SellerNPWP, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerPosition(model.SellerPosition, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSubTotal(model.SubTotal, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatId(model.VatId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatRate(model.VatRate, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetProductId(item.ProductId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetProductCode(item.ProductCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetProductName(item.ProductName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPrice(item.Price, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetRemainingQuantity(item.RemainingQuantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityId(item.ComodityId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityCode(item.ComodityCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityName(item.ComodityName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetRemark(item.Remark, _identityProvider.Username, UserAgent);

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
