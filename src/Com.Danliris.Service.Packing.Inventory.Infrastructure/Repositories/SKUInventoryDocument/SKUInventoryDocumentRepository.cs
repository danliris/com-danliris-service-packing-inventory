using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.SKUInventoryDocument
{
    public class SKUInventoryDocumentRepository : ISKUInventoryDocumentRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<SKUInventoryDocumentModel> _skuInventoryDocumentDbSet;
        private readonly DbSet<SKUInventoryDocumentItemModel> _skuInventoryDocumentItemDbSet;
        private readonly IIdentityProvider _identityProvider;

        public SKUInventoryDocumentRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _skuInventoryDocumentDbSet = dbContext.Set<SKUInventoryDocumentModel>();
            _skuInventoryDocumentItemDbSet = dbContext.Set<SKUInventoryDocumentItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var packagingInventoryDocument = _skuInventoryDocumentDbSet.FirstOrDefault(entity => entity.Id == id);
            var packagingInventoryDocumentItems = _skuInventoryDocumentItemDbSet.Where(entity => entity.SkuInventoryDocumentId == id).ToList();

            EntityExtension.FlagForDelete(packagingInventoryDocument, _identityProvider.Username, USER_AGENT);
            packagingInventoryDocumentItems = packagingInventoryDocumentItems.Select(entity =>
            {
                EntityExtension.FlagForDelete(entity, _identityProvider.Username, USER_AGENT);
                return entity;
            }).ToList();

            _skuInventoryDocumentDbSet.Update(packagingInventoryDocument);
            _skuInventoryDocumentItemDbSet.UpdateRange(packagingInventoryDocumentItems);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(SKUInventoryDocumentModel model)
        {
            do
            {
                model.DocumentNo = CodeGenerator.Generate(8);
            } while (_skuInventoryDocumentDbSet.Any(entity => entity.DocumentNo == model.DocumentNo));

            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            model.Items = model.Items.Select(entity => 
            {
                EntityExtension.FlagForCreate(entity, _identityProvider.Username, USER_AGENT);
                return entity;
            }).ToList();

            _skuInventoryDocumentDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<SKUInventoryDocumentModel> ReadAll()
        {
            return _skuInventoryDocumentDbSet;
        }

        public Task<SKUInventoryDocumentModel> ReadByIdAsync(int id)
        {
            return _skuInventoryDocumentDbSet.Where(entity => entity.Id == id).Include(entity => entity.Items).AsNoTracking().FirstAsync();
        }

        public Task<int> UpdateAsync(int id, SKUInventoryDocumentModel model)
        {
            throw new NotImplementedException();
        }
    }
}