using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU
{
    public class ProductSKURepository : IProductSKURepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ProductSKUModel> _productSKUDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ProductSKURepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _productSKUDbSet = dbContext.Set<ProductSKUModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _productSKUDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _productSKUDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ProductSKUModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate(6);
            } while (_productSKUDbSet.Any(entity => entity.Code == model.Code));
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _productSKUDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<bool> IsExist(string name)
        {
            return _productSKUDbSet.AnyAsync(entity => entity.Name.ToUpper() == name.ToUpper());
        }

        public IQueryable<ProductSKUModel> ReadAll()
        {
            return _productSKUDbSet.AsNoTracking();
        }

        public Task<ProductSKUModel> ReadByIdAsync(int id)
        {
            return _productSKUDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, ProductSKUModel model)
        {
            var modelToUpdate = _productSKUDbSet.FirstOrDefault(entity => entity.Id == id);

            var isModified = false;
            if (modelToUpdate.Composition != model.Composition)
            {
                modelToUpdate.Composition = model.Composition;
                isModified = true;
            }

            if (modelToUpdate.Construction != model.Construction)
            {
                modelToUpdate.Construction = model.Construction;
                isModified = true;
            }

            if (modelToUpdate.Design != model.Design)
            {
                modelToUpdate.Design = model.Design;
                isModified = true;
            }

            if (modelToUpdate.Grade != model.Grade)
            {
                modelToUpdate.Grade = model.Grade;
                isModified = true;
            }

            if (modelToUpdate.LotNo != model.LotNo)
            {
                modelToUpdate.LotNo = model.LotNo;
                isModified = true;
            }

            if (modelToUpdate.Width != model.Width)
            {
                modelToUpdate.Width = model.Width;
                isModified = true;
            }

            if (modelToUpdate.WovenType != model.WovenType)
            {
                modelToUpdate.WovenType = model.WovenType;
                isModified = true;
            }

            if (modelToUpdate.YarnType1 != model.YarnType1)
            {
                modelToUpdate.YarnType1 = model.YarnType1;
                isModified = true;
            }

            if (modelToUpdate.YarnType2 != model.YarnType2)
            {
                modelToUpdate.YarnType2 = model.YarnType2;
                isModified = true;
            }

            if (isModified)
            {
                EntityExtension.FlagForUpdate(model, _identityProvider.Username, USER_AGENT);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}