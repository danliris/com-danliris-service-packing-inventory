//using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
//using Microsoft.EntityFrameworkCore;
//using System;
//using Com.Moonlay.Models;
//using Microsoft.Extensions.DependencyInjection;
//using System.Threading.Tasks;
//using System.Linq;
//using Com.Danliris.Service.Packing.Inventory.Data;
//using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNoteTS;

//namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS
//{
//    public class GarmentShippingLocalSalesNoteTSItemRepository : IGarmentShippingLocalSalesNoteTSItemRepository
//    {
//        private const string USER_AGENT = "Repository";
//        private readonly PackingInventoryDbContext _dbContext;
//        private readonly DbSet<GarmentShippingLocalSalesNoteTSItemModel> _dbSet;
//        private readonly IIdentityProvider _identityProvider;

//        public GarmentShippingLocalSalesNoteTSItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
//        {
//            _dbContext = dbContext;
//            _dbSet = dbContext.Set<GarmentShippingLocalSalesNoteTSItemModel>();
//            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
//        }

//        public Task<int> DeleteAsync(int id)
//        {
//            var model = _dbSet
//               .FirstOrDefault(s => s.Id == id);

//            model.FlagForDelete(_identityProvider.Username, USER_AGENT);

//            return _dbContext.SaveChangesAsync();
//        }

//        public Task<int> InsertAsync(GarmentShippingLocalSalesNoteTSItemModel model)
//        {
//            model.FlagForCreate(_identityProvider.Username, USER_AGENT);

//            _dbSet.Add(model);

//            return _dbContext.SaveChangesAsync();
//        }

//        public Task<GarmentShippingLocalSalesNoteTSItemModel> ReadByIdAsync(int id)
//        {
//            return _dbSet
//                .FirstOrDefaultAsync(s => s.Id == id);
//        }

//        public Task<int> UpdateAsync(int id, GarmentShippingLocalSalesNoteTSItemModel model)
//        {
//            var modelToUpdate = _dbSet
//                 .FirstOrDefault(s => s.Id == id);

//            //modelToUpdate.SetProductId(model.ProductId, _identityProvider.Username, USER_AGENT);
//            //modelToUpdate.SetProductCode(model.ProductCode, _identityProvider.Username, USER_AGENT);
//            //modelToUpdate.SetProductName(model.ProductName, _identityProvider.Username, USER_AGENT);

//            modelToUpdate.SetInvoice(model.InvoiceNo, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetPackingListId(model.PackingListId, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetPrice(model.Price, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetPackageQuantity(model.PackageQuantity, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetPackageUomId(model.PackageUomId, _identityProvider.Username, USER_AGENT);
//            modelToUpdate.SetPackageUomUnit(model.PackageUomUnit, _identityProvider.Username, USER_AGENT);

//            return _dbContext.SaveChangesAsync();
//        }

//        public IQueryable<GarmentShippingLocalSalesNoteTSItemModel> ReadAll()
//        {
//            return _dbSet.AsNoTracking();
//        }
//    }
//}
