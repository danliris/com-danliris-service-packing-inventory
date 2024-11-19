using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD
{
    public class GarmentMDLocalSalesContractRepository : IGarmentMDLocalSalesContractRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentMDLocalSalesContractModel> _dbSet;

        public GarmentMDLocalSalesContractRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentMDLocalSalesContractModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .FirstOrDefault(s => s.Id == id);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentMDLocalSalesContractModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentMDLocalSalesContractModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentMDLocalSalesContractModel> ReadByIdAsync(int id)
        {
            return _dbSet
              .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentMDLocalSalesContractModel model)
        {
            var modelToUpdate = _dbSet
            .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetBuyerAddress(model.BuyerAddress, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerCode(model.BuyerCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerId(model.BuyerId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerName(model.BuyerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerNPWP(model.BuyerNPWP, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsUseVat(model.IsUseVat, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerAddress(model.SellerAddress, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerName(model.SellerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerNPWP(model.SellerNPWP, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSellerPosition(model.SellerPosition, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSubTotal(model.SubTotal, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatId(model.VatId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatRate(model.VatRate, _identityProvider.Username, UserAgent);

            modelToUpdate.SetComodityName(model.ComodityName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemainingQuantity(model.RemainingQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPrice(model.Price, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
