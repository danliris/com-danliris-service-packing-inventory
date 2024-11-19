using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesDO;
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
    public class GarmentMDLocalSalesDORepository : IGarmentMDLocalSalesDORepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentMDLocalSalesDOModel> _dbSet;
        private readonly DbSet<GarmentMDLocalSalesContractModel> _salesContractDbSet;

        public GarmentMDLocalSalesDORepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentMDLocalSalesDOModel>();
            _salesContractDbSet = dbContext.Set<GarmentMDLocalSalesContractModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id); ;

            var salesContract = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
            salesContract.SetIsLocalSalesDOCreated(false, _identityProvider.Username, UserAgent);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentMDLocalSalesDOModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            var salesContract = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
            salesContract.SetIsLocalSalesDOCreated(true, _identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentMDLocalSalesDOModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentMDLocalSalesDOModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentMDLocalSalesDOModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTo(model.To, _identityProvider.Username, UserAgent);
            modelToUpdate.SetStorageDivision(model.StorageDivision, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            modelToUpdate.SetGrossWeight(model.GrossWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettWeight(model.NettWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, UserAgent);

            modelToUpdate.FlagForUpdate(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
